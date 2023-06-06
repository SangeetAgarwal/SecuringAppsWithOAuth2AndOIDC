using System.Security.Claims;
using Duende.IdentityServer;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using MakeBitByte.IDP.DbContexts;

namespace MakeBitByte.IDP.Services
{
    public class LocalUserProfileService : IProfileService
    {
        private readonly ILocalUserService _localUserService;

        public LocalUserProfileService(ILocalUserService localUserService)
        {
            _localUserService = localUserService;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {

            switch (context.Caller)
            {
                //  case IdentityServerConstants.ProfileDataCallers.ClaimsProviderIdentityToken:
                //  case IdentityServerConstants.ProfileDataCallers.ClaimsProviderAccessToken:
                //  case IdentityServerConstants.ProfileDataCallers.ClaimsProviderIdentityToken:
                default:
                    var subjectId = context.Subject.GetSubjectId();
                    var userClaims = await _localUserService.GetUserClaimsBySubjectAsync(subjectId);

                    var claims = userClaims.ToList().Select(r => new Claim(r.Type, r.Value));
                    //context.AddRequestedClaims(claims);
                    context.IssuedClaims.AddRange(claims);                   
                    break;
                }
    
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {

            //var subjectId = context.Subject.GetSubjectId();
            //context.IsActive = await _localUserService.IsUserActive(subjectId);
            context.IsActive = true;
        }
    }
}
