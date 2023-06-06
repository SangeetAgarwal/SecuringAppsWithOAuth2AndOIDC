using System.IdentityModel.Tokens.Jwt;
using JavaScriptClient.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

var identityServerConfiguration = new IdentityServerConfiguration();
builder.Configuration.GetSection(nameof(IdentityServerConfiguration)).Bind(identityServerConfiguration);

var clientConfiguration = new ClientConfiguration();
builder.Configuration.GetSection(nameof(ClientConfiguration)).Bind(clientConfiguration);

builder.Services.AddControllers();
builder.Services.AddAuthorization();

builder.Services.AddBff();

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
    options.Authority = identityServerConfiguration.BaseUrl;

    options.ClientId = clientConfiguration.ClientId;
    options.ClientSecret = clientConfiguration.ClientSecret;
    options.ResponseType = "code";
        
    options.Scope.Add("api1");
     //default, so no need to mention here again
    options.Scope.Add("openid");
    options.Scope.Add("profile");
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

app.UseEndpoints(endpoints =>
{
   
    //The call to the AsBffApiEndpoint() fluent helper method
    //adds BFF support to the local APIs. This includes anti-forgery
    //protection as well as suppressing login redirects on authentication failures and
    //instead returning 401 and 403 status codes under the appropriate circumstances.
    endpoints.MapControllers()
    .AsBffApiEndpoint();

    endpoints.MapBffManagementEndpoints();

    // proxy any call to local /remote to the actual remote api
    // passing the access token
    endpoints.MapRemoteBffApiEndpoint("/remote", "https://locathost:6001")
        .RequireAccessToken(Duende.Bff.TokenType.User);
});


app.Run();
