using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Notes.MvcApp.Configuration;
using Notes.MvcApp.Services.OidcServices;

namespace Notes.MvcApp.OidcEvents
{
    public class WebClientJAREvent : OpenIdConnectEvents
    {
        private readonly IAuthorizationRequestSigner _authorizationRequestSigner;
        private readonly IdentityServerConfiguration _identityServerConfiguration;

        public WebClientJAREvent(IAuthorizationRequestSigner authorizationRequestSigner, IdentityServerConfiguration identityServerConfiguration)
        {
            _authorizationRequestSigner = authorizationRequestSigner;
            _identityServerConfiguration = identityServerConfiguration;
        }

        public override Task RedirectToIdentityProvider(RedirectContext context)
        {
            var request = _authorizationRequestSigner.SignRequest(
                context.ProtocolMessage,
                context.ProtocolMessage.ClientId,
                _identityServerConfiguration.BaseUrl
            );

            var clientId = context.ProtocolMessage.ClientId;
            var redirectUri = context.ProtocolMessage.RedirectUri;

            context.ProtocolMessage.Parameters.Clear();
            context.ProtocolMessage.ClientId = clientId;
            context.ProtocolMessage.RedirectUri = redirectUri;
            context.ProtocolMessage.SetParameter("request", request);

            return Task.CompletedTask;
        }
    }
}
