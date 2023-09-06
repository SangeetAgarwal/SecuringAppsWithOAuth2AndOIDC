using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Notes.MvcApp.Services.OidcServices;

namespace Notes.MvcApp.OidcEvents
{
    public class WebClientJwtEvent : OpenIdConnectEvents
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ITokenGenerator _tokenGenerator;

        public WebClientJwtEvent(IHttpClientFactory clientFactory, ITokenGenerator tokenGenerator)
        {
            _clientFactory = clientFactory;
            _tokenGenerator = tokenGenerator;
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

            var signedToken = _tokenGenerator.GenerateSignedToken("notesmvcappprivatekeyjwt",
                discoveryDocumentResponse.TokenEndpoint);

            context.TokenEndpointRequest.ClientAssertion = signedToken;
        }
    }
}
