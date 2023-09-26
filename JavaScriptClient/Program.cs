using System.IdentityModel.Tokens.Jwt;
using Duende.Bff;
using Duende.Bff.Yarp;
using JavaScriptClient.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var identityServerConfiguration = new IdentityServerConfiguration();
builder.Configuration.GetSection(nameof(IdentityServerConfiguration)).Bind(identityServerConfiguration);

var clientConfiguration = new ClientConfiguration();
builder.Configuration.GetSection(nameof(ClientConfiguration)).Bind(clientConfiguration);

builder.Services.AddControllers();
builder.Services.AddAuthorization();

builder.Services.AddBff()
    .AddRemoteApis();


JwtSecurityTokenHandler.DefaultMapInboundClaims = false;


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = "oidc";
    options.DefaultSignOutScheme = "oidc";

})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
.AddOpenIdConnect("oidc", options =>
{
    options.Authority = identityServerConfiguration.BaseUrl.ToLower();

    options.ClientId = clientConfiguration.ClientId;
    options.ClientSecret = clientConfiguration.ClientSecret;
    options.ResponseType = "code";

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
    options.SaveTokens = true;
    options.GetClaimsFromUserInfoEndpoint = true;

});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseBff();
app.UseAuthorization();

//The call to the AsBffApiEndpoint() fluent helper method
//adds BFF support to the local APIs. This includes anti-forgery
//protection as well as suppressing login redirects on authentication failures and
//instead returning 401 and 403 status codes under the appropriate circumstances.
app.MapControllers()
.AsBffApiEndpoint();

app.MapBffManagementEndpoints();

// proxy any call to local /remote to the actual remote api
// passing the access token
app.MapRemoteBffApiEndpoint(
        "/api", "https://localhost:7094/api")
    .RequireAccessToken(TokenType.User);

app.Run();
