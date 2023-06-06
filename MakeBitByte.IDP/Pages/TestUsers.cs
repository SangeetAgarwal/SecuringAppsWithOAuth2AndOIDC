// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using IdentityModel;
using System.Security.Claims;
using System.Text.Json;
using Duende.IdentityServer;
using Duende.IdentityServer.Test;

namespace MakeBitByte.IDP;

public class TestUsers
{
    public static List<TestUser> Users =>
        new()
        {
            new TestUser
            {
                SubjectId = "36c434ff-888f-4820-a316-1a025f8a4f0c",
                Username = "Vinita",
                Password = "Sourdough@0621",
                Claims =
                {
                    new Claim(JwtClaimTypes.GivenName, "Vinita"),
                    new Claim(JwtClaimTypes.FamilyName, "Agarwal"),
                    new Claim("role", "pro"),
                    new Claim("subscriberSince", JsonSerializer.Serialize(new DateTime(2021, 06, 21)))
                }
            },
           new TestUser
            {
                SubjectId = "da0d5f9a-8217-4a68-8398-db8506fdf618",
                Username = "Appa",
                Password = "Sourdough@0621",
                Claims =
                {
                    new Claim(JwtClaimTypes.GivenName, "Appa"),
                    new Claim(JwtClaimTypes.FamilyName, "Agarwal"),
                    new Claim("role", "pro"),
                    new Claim("subscriberSince", JsonSerializer.Serialize(new DateTime(2023, 01, 21)))
                }
            },
            new TestUser
            {
                SubjectId = "1c94f897-2af3-46d4-a131-8a147d18a2f2",
                Username = "Arjun",
                Password = "Sourdough@0621",
                Claims =
                {
                    new Claim(JwtClaimTypes.GivenName, "Arjun"),
                    new Claim(JwtClaimTypes.FamilyName, "Agarwal"),
                    new Claim("role", "none"),
                    new Claim("subscriberSince", JsonSerializer.Serialize(DateTime.MaxValue))
                }
            }
        };
}