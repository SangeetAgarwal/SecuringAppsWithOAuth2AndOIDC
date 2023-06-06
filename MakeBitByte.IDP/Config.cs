using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace MakeBitByte.IDP;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("roles", new [] {"role"}),
            new IdentityResource("subscriberSince", new [] {"subscriberSince"})
        };

    public static IEnumerable<ApiResource> ApiResources { get; } = new ApiResource[]
    {
        new ApiResource()
        {
            Name = "notesapi",
            Description = "notes api",
            Scopes = new[]
            {
                "notesapi.fullaccess",
                "notesapi.read",
                "notesapi.write"
            },
            //UserClaims = new List<string>
            //{
            //    "role",
            //    "subscriberSince"
            //}
        }
    };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            {   
                new ApiScope
                {
                    Name = "notesapi.fullaccess"
                },
                new ApiScope
                {
                    Name = "notesapi.read"
                },
                new ApiScope
                {
                    Name = "notesapi.write"
                }

            };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientName = "Notes Mvc App",
                ClientId = "notesmvcapp",
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris =
                {
                    "https://localhost:7123/signin-oidc"
                },
                PostLogoutRedirectUris =
                {
                    "https://localhost:7123/signout-callback-oidc"
                },

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "roles",
                    "notesapi.fullaccess",
                    "notesapi.read",
                    "notesapi.write",
                    "subscriberSince"
                },
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                //RequireConsent = false,
                
            }
        };
}