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
                
            },

            // code flow + PKCE + private key JWT auth
            new Client
            {
                ClientId = "notesmvcappprivatekeyjwt",
                ClientSecrets =
                    {
                        new Secret
                        {
                            Type = IdentityServerConstants.SecretTypes.X509CertificateBase64,
                            Value = "MIICwDCCAaigAwIBAgIIUMTNpnXxoG8wDQYJKoZIhvcNAQELBQAwDzENMAsGA1UEAxMEdGVzdDAe\n\rFw0yMzA5MDQxNTU5MTZaFw0yODA5MDQxNTU5MTZaMA8xDTALBgNVBAMTBHRlc3QwggEiMA0GCSqG\n\rSIb3DQEBAQUAA4IBDwAwggEKAoIBAQCqFhV0LjNyO1/7rp5HGaY+dnSttzN4yDliM8DSbMjMpwI3\n\roQXOG1p4fCxjbzZrLd9Hc+BihVHBEV4kzfVTAJ6rN1ptH0NOCk5Kri+6HJUXKgErOVFzJl+UCdJa\n\r7Zxj+lp8nVHGyhxfE5NHHE+rfGcy33auu5Emq+tqouMLf7imGaRzPoAotKgTaajSZkWog2JYzza6\n\rRaJ+LXTBgjuQae1IF3SiUIQbAO8KzTe+tOgpDdAxBP9c5gNoi84FH4Smmkv/6gmaxerf2ICn0Mdm\n\rMC1jZu7ZwDhtCS8kBMXlHN09X6sPn47pj25ycwoZTdar1M3L8KuZ8PLThq+WJ9UC9qiJAgMBAAGj\n\rIDAeMAsGA1UdDwQEAwIEsDAPBgNVHREECDAGggR0ZXN0MA0GCSqGSIb3DQEBCwUAA4IBAQCPAVwc\n\roZXt0UXilRJd9yfmdKvvG8hui3bFgZn9YZx6zV1A9PqX8B/+udSonXZ6LW8XuzZajEXy85BtFbU2\n\rqIVrp3b2UcNyLtn0NenurvCj6bmFZP+QjIKk/MUpYiFzqVShLcwYigoDrVfcruM9Fa++qbRoxb9/\n\rFEGdw5W/1odGmvhbc7CynfJFOCHtO1F5HiEvJ1ciBhEDoA2PHHtuBpEw9GF9aAidHB7XIUgrzfOh\n\ryYE+pDCQq/CdG0Hdek4o/vJ9uK5Q9/Mc9yUcuwzkotukjQ+8rg5VQoQQ8UqdFzL1++pI9ufCzbC1\n\r6ItzyXTerkfZ3twjBqb3ch8GBNZtK4la"
                        }
                    },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://localhost:7123/signin-codeflowprivatekeyjwt" },
                FrontChannelLogoutUri = "https://localhost:7123/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:7123/signout-callback-oidc" },
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
            },
            // code flow + PKCE + JAR
            new Client
            {
                ClientId = "notesmvcappjar",
                ClientSecrets =
                {
                    new Secret("secret".Sha256()),
                    new Secret
                    {
                        Type = IdentityServerConstants.SecretTypes.X509CertificateBase64,
                        Value = "MIICwDCCAaigAwIBAgIIUMTNpnXxoG8wDQYJKoZIhvcNAQELBQAwDzENMAsGA1UEAxMEdGVzdDAe\n\rFw0yMzA5MDQxNTU5MTZaFw0yODA5MDQxNTU5MTZaMA8xDTALBgNVBAMTBHRlc3QwggEiMA0GCSqG\n\rSIb3DQEBAQUAA4IBDwAwggEKAoIBAQCqFhV0LjNyO1/7rp5HGaY+dnSttzN4yDliM8DSbMjMpwI3\n\roQXOG1p4fCxjbzZrLd9Hc+BihVHBEV4kzfVTAJ6rN1ptH0NOCk5Kri+6HJUXKgErOVFzJl+UCdJa\n\r7Zxj+lp8nVHGyhxfE5NHHE+rfGcy33auu5Emq+tqouMLf7imGaRzPoAotKgTaajSZkWog2JYzza6\n\rRaJ+LXTBgjuQae1IF3SiUIQbAO8KzTe+tOgpDdAxBP9c5gNoi84FH4Smmkv/6gmaxerf2ICn0Mdm\n\rMC1jZu7ZwDhtCS8kBMXlHN09X6sPn47pj25ycwoZTdar1M3L8KuZ8PLThq+WJ9UC9qiJAgMBAAGj\n\rIDAeMAsGA1UdDwQEAwIEsDAPBgNVHREECDAGggR0ZXN0MA0GCSqGSIb3DQEBCwUAA4IBAQCPAVwc\n\roZXt0UXilRJd9yfmdKvvG8hui3bFgZn9YZx6zV1A9PqX8B/+udSonXZ6LW8XuzZajEXy85BtFbU2\n\rqIVrp3b2UcNyLtn0NenurvCj6bmFZP+QjIKk/MUpYiFzqVShLcwYigoDrVfcruM9Fa++qbRoxb9/\n\rFEGdw5W/1odGmvhbc7CynfJFOCHtO1F5HiEvJ1ciBhEDoA2PHHtuBpEw9GF9aAidHB7XIUgrzfOh\n\ryYE+pDCQq/CdG0Hdek4o/vJ9uK5Q9/Mc9yUcuwzkotukjQ+8rg5VQoQQ8UqdFzL1++pI9ufCzbC1\n\r6ItzyXTerkfZ3twjBqb3ch8GBNZtK4la"
                    }
                },
                RequireRequestObject = true,
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://localhost:7123/signin-codeflowjar" },
                FrontChannelLogoutUri = "https://localhost:7123/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:7123/signout-callback-oidc" },
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
            },
            // code flow + PKCE + JAR + private key JWT auth
            new Client
            {
                ClientId = "notesmvcappjwtandjar",
                RequireRequestObject = true,
                // Use below for verifying signed JAR and client auth via private key JWT
                ClientSecrets =
                {
                    new Secret
                    {
                        Type = IdentityServerConstants.SecretTypes.X509CertificateBase64,
                        Value = "MIICwDCCAaigAwIBAgIIUMTNpnXxoG8wDQYJKoZIhvcNAQELBQAwDzENMAsGA1UEAxMEdGVzdDAe\n\rFw0yMzA5MDQxNTU5MTZaFw0yODA5MDQxNTU5MTZaMA8xDTALBgNVBAMTBHRlc3QwggEiMA0GCSqG\n\rSIb3DQEBAQUAA4IBDwAwggEKAoIBAQCqFhV0LjNyO1/7rp5HGaY+dnSttzN4yDliM8DSbMjMpwI3\n\roQXOG1p4fCxjbzZrLd9Hc+BihVHBEV4kzfVTAJ6rN1ptH0NOCk5Kri+6HJUXKgErOVFzJl+UCdJa\n\r7Zxj+lp8nVHGyhxfE5NHHE+rfGcy33auu5Emq+tqouMLf7imGaRzPoAotKgTaajSZkWog2JYzza6\n\rRaJ+LXTBgjuQae1IF3SiUIQbAO8KzTe+tOgpDdAxBP9c5gNoi84FH4Smmkv/6gmaxerf2ICn0Mdm\n\rMC1jZu7ZwDhtCS8kBMXlHN09X6sPn47pj25ycwoZTdar1M3L8KuZ8PLThq+WJ9UC9qiJAgMBAAGj\n\rIDAeMAsGA1UdDwQEAwIEsDAPBgNVHREECDAGggR0ZXN0MA0GCSqGSIb3DQEBCwUAA4IBAQCPAVwc\n\roZXt0UXilRJd9yfmdKvvG8hui3bFgZn9YZx6zV1A9PqX8B/+udSonXZ6LW8XuzZajEXy85BtFbU2\n\rqIVrp3b2UcNyLtn0NenurvCj6bmFZP+QjIKk/MUpYiFzqVShLcwYigoDrVfcruM9Fa++qbRoxb9/\n\rFEGdw5W/1odGmvhbc7CynfJFOCHtO1F5HiEvJ1ciBhEDoA2PHHtuBpEw9GF9aAidHB7XIUgrzfOh\n\ryYE+pDCQq/CdG0Hdek4o/vJ9uK5Q9/Mc9yUcuwzkotukjQ+8rg5VQoQQ8UqdFzL1++pI9ufCzbC1\n\r6ItzyXTerkfZ3twjBqb3ch8GBNZtK4la"
                    }
                },
                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:7123/signin-codeflowjarandprivatekeyjwt" },
                FrontChannelLogoutUri = "https://localhost:7123/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:7123/signout-callback-oidc" },

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
            },
            // code flow + PKCE + token encryption
            new Client
            {
                ClientId = "notesmvcapptokenencryption",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:7123/signin-codeflowtokenencryption" },
                FrontChannelLogoutUri = "https://localhost:7123/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:7123/signout-callback-oidc" },

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
            },
            // code flow + PKCE + DPoP (Demonstrating proof of possession)
            new Client
            {
                ClientId = "notesmvcappdpopcodeflow",
                ClientSecrets = { new Secret("secret".Sha256())},
                AllowedGrantTypes = GrantTypes.Code,

                RequireDPoP = true,

                RedirectUris = { "https://localhost:7123/signin-codeflowdpop" },
                FrontChannelLogoutUri = "https://localhost:7123/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:7123/signout-callback-oidc" },

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

            },
            // bff for js client 
            new Client
            {
                ClientId = "bff",
                ClientSecrets = { new Secret("secret".Sha256()) },
            
                AllowedGrantTypes = GrantTypes.Code,
                
                // where to redirect to after login
                RedirectUris = { "https://localhost:5003/signin-oidc" },
            
                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:5003/signout-callback-oidc" },
            
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
            },
            new Client
            {
                ClientId = "reactclientappbff",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
                
                // where to redirect to after login
                RedirectUris =
                {
                    "https://localhost:7249/signin-oidc",
                    "https://localhost:5173/signin-oidc"
                },

                // where to redirect to after logout
                PostLogoutRedirectUris =
                {
                    "https://localhost:7249/signout-callback-oidc",
                    "https://localhost:5173/signout-callback-oidc"
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
            }
        };
}