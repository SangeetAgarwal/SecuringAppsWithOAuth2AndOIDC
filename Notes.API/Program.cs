using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Notes.Api.Authorization;
using Notes.Api.ClaimsPrincipal;
using Notes.Api.Configuration;
using Notes.API.Datalayer.Configuration;
using Notes.API.Servicelayer.Configuration;


var builder = WebApplication.CreateBuilder(args);

var identityServerConfiguration = new IdentityServerConfiguration();

builder.Configuration.GetSection(nameof(identityServerConfiguration)).Bind(identityServerConfiguration);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDatalayer();

builder.Services.AddServicelayer();

builder.Services.AddScoped(serviceProvider =>
{
    var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
    var claimsProvider = new ClaimsProvider(httpContextAccessor?.HttpContext?.User);
    return claimsProvider;
});

builder.Services.AddScoped<IAuthorizationHandler, MustOwnResourceHandler>();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

/*
 1. Reads metadata from IDP
 2. caches results and 
 3. validates the bearer token
*/
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     .AddJwtBearer(options =>
     {
        options.Authority = identityServerConfiguration.BaseUrl;

        // ensures ApiName is checked for audience in the token
        options.Audience = identityServerConfiguration.ApiName;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "given_name",
            RoleClaimType ="role",

            // prevents confusion attacks where arbitrary tokens were accepted by api
            ValidTypes = new [] {"at+jwt"}
        };
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
    authorizationOptions.AddPolicy("ClientAppCanWrite", policyBuilder =>
    {
        policyBuilder.RequireClaim("scope", "notesapi.write");
    });
    authorizationOptions.AddPolicy("MustOwnResource", policyBuilder =>
    {
        policyBuilder.RequireAuthenticatedUser();
        policyBuilder.AddRequirements(new MustOwnResourceRequirement());
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();