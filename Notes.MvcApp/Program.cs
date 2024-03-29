using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Notes.MvcApp.Configuration;
using Notes.MvcApp.Helpers;
using Notes.MvcApp.Notes.Api.Configuration;
using Notes.MvcApp.OidcEvents;
using Notes.MvcApp.Services.Configuration;
using Notes.MvcApp.Services.OidcServices;


var builder = WebApplication.CreateBuilder(args);

var identityServerConfiguration = new IdentityServerConfiguration();

var clientConfiguration = new ClientConfiguration();
var notesApiConfiguration = new NotesApiConfiguration();

builder.Configuration.GetSection(nameof(NotesApiConfiguration)).Bind(notesApiConfiguration);

builder.Configuration.GetSection(nameof(IdentityServerConfiguration))
    .Bind(identityServerConfiguration);

builder.Configuration.GetSection(nameof(ClientConfiguration))
    .Bind(clientConfiguration);

// Add services to the container.
builder.Services.AddControllersWithViews(); 
builder.Services.AddNotesService();

builder.Services.AddSingleton<IdentityServerConfiguration>(_ => identityServerConfiguration);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

// IDP client
builder.Services.AddHttpClient("IdpClient", client =>
{
    client.BaseAddress = new Uri(identityServerConfiguration.BaseUrl.ToLower());
});

builder.Services.AddSingleton<ITokenGenerator, TokenGenerator>();
builder.Services.AddSingleton<IAuthorizationRequestSigner, AuthorizationRequestSigner>();

builder.Services.AddTransient<WebClientJwtEvent>();
builder.Services.AddTransient<WebClientJAREvent>();
builder.Services.AddTransient<WebClientJwtAndJAREvent>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    // options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = "CodeFlowWithDPopScheme";
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.AccessDeniedPath = "/Authentication/AccessDenied";
})
.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.Authority = identityServerConfiguration.BaseUrl.ToLower();
    options.ResponseType = "code";
    options.ClientId = clientConfiguration.ClientId;
    options.ClientSecret = clientConfiguration.ClientSecret;
    options.SaveTokens = true;
    options.Scope.Add("roles");
    options.Scope.Add("subscriberSince");
    options.Scope.Add("notesapi.write");
    options.Scope.Add("notesapi.read");
    options.Scope.Add("notesapi.fullaccess");

    // default, so no need to mention here again
    //options.Scope.Add("openid");
    //options.Scope.Add("profile");

    // default, so no need to mention here again
    //options.CallbackPath = new PathString("signin-oidc");

    // default, so no need to mention here again
    // Must match what we have in the post logout client URI in the client config
    //options.SignedOutCallbackPath = new PathString("signout-callback-oidc");

    options.GetClaimsFromUserInfoEndpoint = true;

    options.ClaimActions.Remove("aud");
    options.ClaimActions.DeleteClaim("sid");
    options.ClaimActions.DeleteClaim("idp");

    options.ClaimActions.MapJsonKey("role", "role");
    options.ClaimActions.MapJsonKey("subscriberSince", "subscriberSince");

    options.TokenValidationParameters = new TokenValidationParameters
    {
        RoleClaimType = "role",
        NameClaimType = "given_name"
    };
    options.AccessDeniedPath = "/Authentication/AccessDenied";
})
.AddOpenIdConnect("CodeFlowWithPrivateKeyJWTScheme", options =>
{
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.Authority = identityServerConfiguration.BaseUrl.ToLower();
    options.CallbackPath = "/signin-codeflowprivatekeyjwt";
    options.ResponseType = "code";

    options.SaveTokens = true;

    options.GetClaimsFromUserInfoEndpoint = true;

    options.ClientId = "notesmvcappprivatekeyjwt";

    options.Scope.Add("roles");
    options.Scope.Add("subscriberSince");
    options.Scope.Add("notesapi.write");
    options.Scope.Add("notesapi.read");
    options.Scope.Add("notesapi.fullaccess");

    options.ClaimActions.Remove("aud");
    options.ClaimActions.DeleteClaim("sid");
    options.ClaimActions.DeleteClaim("idp");

    options.ClaimActions.MapJsonKey("role", "role");
    options.ClaimActions.MapJsonKey("subscriberSince", "subscriberSince");

    options.TokenValidationParameters = new TokenValidationParameters
    {
        RoleClaimType = "role",
        NameClaimType = "given_name"
    };

    options.AccessDeniedPath = "/Authentication/AccessDenied";

    options.EventsType = typeof(WebClientJwtEvent);
})
.AddOpenIdConnect("CodeFlowWithJARScheme", options =>
{
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.Authority = identityServerConfiguration.BaseUrl.ToLower();
    options.ClientId = "notesmvcappjar";
    options.ClientSecret = clientConfiguration.ClientSecret;
    options.CallbackPath = "/signin-codeflowjar";
    options.ResponseType = "code";
    options.SaveTokens = true;
    options.GetClaimsFromUserInfoEndpoint = true;

    options.Scope.Add("roles");
    options.Scope.Add("subscriberSince");
    options.Scope.Add("notesapi.write");
    options.Scope.Add("notesapi.read");
    options.Scope.Add("notesapi.fullaccess");

    options.ClaimActions.Remove("aud");
    options.ClaimActions.DeleteClaim("sid");
    options.ClaimActions.DeleteClaim("idp");

    options.ClaimActions.MapJsonKey("role", "role");
    options.ClaimActions.MapJsonKey("subscriberSince", "subscriberSince");

    options.TokenValidationParameters = new TokenValidationParameters
    {
        RoleClaimType = "role",
        NameClaimType = "given_name"
    };

    options.EventsType = typeof(WebClientJAREvent);
})
.AddOpenIdConnect("CodeFlowWithPrivateKeyJWTAndJARScheme", options =>
{
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.Authority = identityServerConfiguration.BaseUrl.ToLower();
    options.CallbackPath = "/signin-codeflowjarandprivatekeyjwt";
    options.ResponseType = "code";
    options.SaveTokens = true;
    options.GetClaimsFromUserInfoEndpoint = true;
    options.ClientId = "notesmvcappjwtandjar";

    options.Scope.Add("roles");
    options.Scope.Add("subscriberSince");
    options.Scope.Add("notesapi.write");
    options.Scope.Add("notesapi.read");
    options.Scope.Add("notesapi.fullaccess");

    options.ClaimActions.Remove("aud");
    options.ClaimActions.DeleteClaim("sid");
    options.ClaimActions.DeleteClaim("idp");

    options.ClaimActions.MapJsonKey("role", "role");
    options.ClaimActions.MapJsonKey("subscriberSince", "subscriberSince");

    options.TokenValidationParameters = new TokenValidationParameters
    {
        RoleClaimType = "role",
        NameClaimType = "given_name"
    };

    options.EventsType = typeof(WebClientJwtAndJAREvent);
})
.AddOpenIdConnect("CodeFlowWithTokenEncryptionScheme", options =>
{
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.Authority = identityServerConfiguration.BaseUrl.ToUpper();
    options.ClientId = "notesmvcapptokenencryption";
    options.ClientSecret = "secret";
    options.ResponseType = "code";
    options.SaveTokens = true;
    options.GetClaimsFromUserInfoEndpoint = true;
    options.CallbackPath = "/signin-codeflowtokenencryption";

    options.Scope.Add("roles");
    options.Scope.Add("subscriberSince");
    options.Scope.Add("notesapi.write");
    options.Scope.Add("notesapi.read");
    options.Scope.Add("notesapi.fullaccess");

    options.ClaimActions.Remove("aud");
    options.ClaimActions.DeleteClaim("sid");
    options.ClaimActions.DeleteClaim("idp");

    options.ClaimActions.MapJsonKey("role", "role");
    options.ClaimActions.MapJsonKey("subscriberSince", "subscriberSince");

    options.TokenValidationParameters = new TokenValidationParameters
    {
        RoleClaimType = "role",
        NameClaimType = "given_name",
        TokenDecryptionKey = new X509SecurityKey(
            new X509Certificate2("Certificates/test.pfx", "P@ssw0rd"))
    };
})
.AddOpenIdConnect("CodeFlowWithDPopScheme", options =>
{
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.Authority = identityServerConfiguration.BaseUrl.ToLower();
    options.CallbackPath = "/signin-codeflowdpop";
    options.ResponseType = "code";
    options.GetClaimsFromUserInfoEndpoint = true;
    options.ClientId = "notesmvcappdpopcodeflow";
    options.ClientSecret = "secret";
    options.SaveTokens = true;

    options.Scope.Add("roles");
    options.Scope.Add("subscriberSince");
    options.Scope.Add("notesapi.write");
    options.Scope.Add("notesapi.read");
    options.Scope.Add("notesapi.fullaccess");

    options.ClaimActions.Remove("aud");
    options.ClaimActions.DeleteClaim("sid");
    options.ClaimActions.DeleteClaim("idp");

    options.ClaimActions.MapJsonKey("role", "role");
    options.ClaimActions.MapJsonKey("subscriberSince", "subscriberSince");

    options.TokenValidationParameters = new TokenValidationParameters
    {
        RoleClaimType = "role",
        NameClaimType = "given_name"
    };
});

// Ensures any protocol requests to obtain access tokens from the token server will automatically include a DPoP proof
// token created from the DPoPJsonWebKey
builder.Services.AddOpenIdConnectAccessTokenManagement(options =>
{ 
    options.ChallengeScheme = "CodeFlowWithDPopScheme";
    options.DPoPJsonWebKey = JwkHelper.GenerateJsonWebKey();
});

builder.Services.AddAuthorization(authorizationOptions =>
{
    authorizationOptions.AddPolicy("CanSearch", policyBuilder =>
    {
        policyBuilder.RequireAssertion(authHandlerContext =>
        {
            var singleOrDefault = authHandlerContext.User.Claims.SingleOrDefault(r => r.Type == "subscriberSince");
            if (authHandlerContext.User.Identity != null && singleOrDefault != null && DateTimeOffset.TryParse(singleOrDefault.Value.Trim('"'), out var value) && authHandlerContext.User.Identity.IsAuthenticated)
            {
                return DateTimeOffset.Compare(value, new DateTime(2023, 1, 1)) < 0;
            }
            else
            {
                return false;
            }
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Note}/{action=Index}/{id?}");

app.Run();
