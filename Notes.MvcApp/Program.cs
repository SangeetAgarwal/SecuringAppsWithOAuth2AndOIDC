    using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Notes.MvcApp.Configuration;
using Notes.MvcApp.Services.Configuration;

var builder = WebApplication.CreateBuilder(args);

var identityServerConfiguration = new IdentityServerConfiguration();

var clientConfiguration = new ClientConfiguration();

builder.Configuration.GetSection(nameof(IdentityServerConfiguration))
    .Bind(identityServerConfiguration);

builder.Configuration.GetSection(nameof(ClientConfiguration))
    .Bind(clientConfiguration);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddNotesService();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddAccessTokenManagement();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.AccessDeniedPath = "/Authentication/AccessDenied";
})
.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.Authority = identityServerConfiguration.BaseUrl;
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
