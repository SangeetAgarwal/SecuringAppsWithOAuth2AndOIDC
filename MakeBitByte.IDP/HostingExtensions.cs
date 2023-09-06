using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
//using Azure.Security.KeyVault.Certificates;
using Duende.IdentityServer;
using MakeBitByte.IDP.Configuration;
using MakeBitByte.IDP.DbContexts;
using MakeBitByte.IDP.Entities;
using MakeBitByte.IDP.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace MakeBitByte.IDP;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var azureActiveDirectoryConfiguration = new AzureActiveDirectoryConfiguration();
        builder.Configuration.GetSection(nameof(AzureActiveDirectoryConfiguration))
            .Bind(azureActiveDirectoryConfiguration);

        var facebookConfiguration = new FacebookConfiguration();
        builder.Configuration.GetSection(nameof(FacebookConfiguration)).Bind(facebookConfiguration);

        var identityConfiguration = new IdentityConfiguration();
        builder.Configuration.GetSection(nameof(IdentityConfiguration)).Bind(identityConfiguration);

        var keyVaultConfiguration = new KeyVaultConfiguration();
        builder.Configuration.GetSection(nameof(KeyVaultConfiguration)).Bind(keyVaultConfiguration);

        // configures IIS out-of-proc settings (see https://github.com/aspnet/AspNetCore/issues/14882)
        builder.Services.Configure<IISOptions>(iis =>
        {
            iis.AuthenticationDisplayName = "Windows";
            iis.AutomaticAuthentication = false;
        });

        // ..or configures IIS in-proc settings
        builder.Services.Configure<IISServerOptions>(iis =>
        {
            iis.AuthenticationDisplayName = "Windows";
            iis.AutomaticAuthentication = false;
        });

        builder.Services.AddAuthentication()
            .AddOpenIdConnect("AAD", "Azure Active Directory", options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                options.Authority = azureActiveDirectoryConfiguration.BaseUrl;
                options.ClientId = azureActiveDirectoryConfiguration.ClientId;
                options.ClientSecret = azureActiveDirectoryConfiguration.ClientSecret;
                options.CallbackPath = new PathString("/signin-aad/");
                options.SignedOutCallbackPath = new PathString("/signout-aad/");
                options.ResponseType = "code";
                options.Scope.Add("email");
                options.Scope.Add("offline_access");
                options.SaveTokens = true;
            });

        builder.Services.AddAuthentication()
            .AddFacebook("Facebook", options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                options.AppId = facebookConfiguration.ClientId;
                options.AppSecret = facebookConfiguration.ClientSecret;
            });

        var azureCredential = new DefaultAzureCredential();

        //var certificateClient = new CertificateClient(new Uri(keyVaultConfiguration.RootUri), azureCredential);

        //var certificateResponse = certificateClient.GetCertificate(keyVaultConfiguration.CertificateName);

        // var secretClient = new SecretClient(new Uri(keyVaultConfiguration.RootUri), azureCredential);
        //
        // var secretResponse = secretClient.GetSecret(keyVaultConfiguration.CertificateName);
        //
        // var signingCertificate = new X509Certificate2(
        //     Convert.FromBase64String(secretResponse.Value.Value), 
        //     (string)null, 
        //     X509KeyStorageFlags.MachineKeySet);

        // uncomment if you want to add a UI
        builder.Services.AddRazorPages();

        builder.Services.AddScoped<ILocalUserService, LocalUserService>();

        builder.Services.AddScoped<IPasswordHasher<Entities.User>, PasswordHasher<Entities.User>>();

        builder.Services.AddDbContext<UserDbContext>(options =>
        {
            options.UseSqlServer(identityConfiguration.IdentityUserDbConnectionString);
        });

        var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

        builder.Services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                // https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/api_scopes#authorization-based-on-scopes
                options.EmitStaticAudienceClaim = true;
            })
            .AddProfileService<LocalUserProfileService>()

            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddInMemoryApiResources(Config.ApiResources)
            .AddJwtBearerClientAuthentication();
            // .AddConfigurationStore(options =>
            // {
            //     options.ConfigureDbContext = optionsBuilder =>
            //     {
            //         optionsBuilder.UseSqlServer(identityConfiguration.IdentityServerDbConnectionString,
            //             sqlServerOptionsAction =>
            //             {
            //                 sqlServerOptionsAction.MigrationsAssembly(migrationsAssembly);
            //             });
            //     };
            // })
            // //.AddConfigurationStoreCache()
            // .AddOperationalStore(options =>
            // {
            //     options.ConfigureDbContext = optionsBuilder =>
            //     {
            //         optionsBuilder.UseSqlServer(identityConfiguration.IdentityServerDbConnectionString,
            //             sqlServerDbContextOptionsBuilder =>
            //             {
            //                 sqlServerDbContextOptionsBuilder.MigrationsAssembly(migrationsAssembly);
            //             });
            //     };
            //     options.EnableTokenCleanup = true;
            // });
        // .AddSigningCredential(signingCertificate);


        builder.Services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        });

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseForwardedHeaders();

        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // uncomment if you want to add a UI
        app.UseStaticFiles();
        app.UseRouting();

        app.UseIdentityServer();

        // uncomment if you want to add a UI
        app.UseAuthorization();
        app.MapRazorPages().RequireAuthorization();

        return app;
    }
}