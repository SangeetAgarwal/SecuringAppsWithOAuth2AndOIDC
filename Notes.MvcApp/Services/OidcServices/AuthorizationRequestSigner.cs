using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Notes.MvcApp.Services.OidcServices
{
    public class AuthorizationRequestSigner : IAuthorizationRequestSigner
    {
        public string SignRequest(OpenIdConnectMessage message, string clientId, string audience)
        {
            var certificate = new X509Certificate2("Certificates/test.pfx", "P@ssw0rd");
            var now = DateTime.UtcNow;

            var claims = new List<Claim>();
            foreach (var parameter in message.Parameters)
            {
                claims.Add(new Claim(parameter.Key, parameter.Value));
            }

            var token = new JwtSecurityToken(
                clientId,
                audience,
                claims,
                now,
                now.AddMinutes(1),
                new SigningCredentials(
                    new X509SecurityKey(certificate),
                    SecurityAlgorithms.RsaSha256
                )
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.OutboundClaimTypeMap.Clear();
            return tokenHandler.WriteToken(token);
        }
    }
}
