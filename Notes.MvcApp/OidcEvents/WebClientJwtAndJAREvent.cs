using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Notes.MvcApp.Configuration;
using Notes.MvcApp.Services.OidcServices;

namespace Notes.MvcApp.OidcEvents
{
    public class WebClientJwtAndJAREvent : OpenIdConnectEvents
    {

        private readonly IAuthorizationRequestSigner _authorizationRequestSigner;
        private readonly IdentityServerConfiguration _identityServerConfiguration;

        private readonly IHttpClientFactory _clientFactory;
        private readonly ITokenGenerator _tokenGenerator;
        public WebClientJwtAndJAREvent(IHttpClientFactory clientFactory,
            ITokenGenerator tokenGenerator,
            IAuthorizationRequestSigner authorizationRequestSigner,
            IdentityServerConfiguration identityServerConfiguration)
        {
            _clientFactory = clientFactory;
            _tokenGenerator = tokenGenerator;
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
        public override async Task AuthorizationCodeReceived(AuthorizationCodeReceivedContext context)
        {
            var idpClient = _clientFactory.CreateClient("IdpClient");
            var discoveryDocumentResponse = await idpClient.GetDiscoveryDocumentAsync();
            if (discoveryDocumentResponse.IsError)
            {
                throw new Exception(discoveryDocumentResponse.Error);
            }

            context.TokenEndpointRequest.ClientAssertionType = OidcConstants.ClientAssertionTypes.JwtBearer;

            var signedToken = _tokenGenerator.GenerateSignedToken("notesmvcappjwtandjar",
                discoveryDocumentResponse.TokenEndpoint);

            context.TokenEndpointRequest.ClientAssertion = signedToken;
        }
    }
}
