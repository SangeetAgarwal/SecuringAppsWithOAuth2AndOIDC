using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes.API.Common.Interfaces.Servicelayer;
using Notes.Api.Configuration;
using Notes.API.Controllers;

namespace Notes.Api.Controllers
{
    public class ClaimController : BaseController
    {
        private readonly IdentityServerConfiguration _identityServerConfiguration;
        private readonly IHttpClientFactory _clientFactory;

        public ClaimController(IdentityServerConfiguration identityServerConfiguration, IHttpClientFactory clientFactory, ILogger<BaseController> logger) : base(logger)
        {
            _identityServerConfiguration = identityServerConfiguration;
            _clientFactory = clientFactory;
        }

        [HttpGet("GetClaims")]
        [Authorize]
        public async Task<ActionResult<UserInfoResponse>> GetClaims()
        {
            var client = _clientFactory.CreateClient();
            var token = await HttpContext.GetTokenAsync("access_token");
            var disco = await client.GetDiscoveryDocumentAsync(_identityServerConfiguration.BaseUrl);
            var response = await client.GetUserInfoAsync(new UserInfoRequest
            {
                Address = disco.UserInfoEndpoint,
                Token = token
            });

            if (response.IsError)
            {
                Logger.LogCritical("failed to fetch claims {responseError}", response.Error);
            }

            return response;
        }
    }
}
