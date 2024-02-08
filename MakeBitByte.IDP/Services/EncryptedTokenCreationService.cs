using System.Security.Cryptography.X509Certificates;
using Duende.IdentityServer;
using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace MakeBitByte.IDP.Services
{
    public class EncryptedTokenCreationService : DefaultTokenCreationService
    {
        public EncryptedTokenCreationService(
            IClock clock,
            IKeyMaterialService keys,
            IdentityServerOptions options,
            ILogger<DefaultTokenCreationService> logger)
            : base(clock, keys, options, logger)
        {
        }

        public override async Task<string> CreateTokenAsync(Token token)
        {
            if (token.Type == IdentityServerConstants.TokenTypes.IdentityToken)
            {
                var payload = await base.CreatePayloadAsync(token);

                var handler = new JsonWebTokenHandler();
                var jwe = handler.CreateToken(
                    payload,
                    await Keys.GetSigningCredentialsAsync(),
                    new X509EncryptingCredentials(new X509Certificate2("Certificates/test.cer")));

                return jwe;
            }

            return await base.CreateTokenAsync(token);
        }
    }
}
