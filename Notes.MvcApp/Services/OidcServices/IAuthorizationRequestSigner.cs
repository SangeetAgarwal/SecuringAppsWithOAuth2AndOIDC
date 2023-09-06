using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Notes.MvcApp.Services.OidcServices
{
    public interface IAuthorizationRequestSigner
    { 
        string SignRequest(OpenIdConnectMessage message, string clientId, string audience);
    }
}
