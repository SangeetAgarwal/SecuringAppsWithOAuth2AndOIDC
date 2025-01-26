using System.Security.Claims;
using Duende.IdentityServer;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Test;
using MakeBitByte.IDP.DbContexts;

namespace MakeBitByte.IDP.Services
{
    public class LocalUserProfileService : IProfileService
    {
        private readonly TestUserStore _users;

        public LocalUserProfileService(TestUserStore users)
        {
            _users = users;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {

            switch (context.Caller)
            {
                // case IdentityServerConstants.ProfileDataCallers.ClaimsProviderIdentityToken:
                // case IdentityServerConstants.ProfileDataCallers.ClaimsProviderAccessToken:
                // case IdentityServerConstants.ProfileDataCallers.UserInfoEndpoint:
                default:
                    var subjectId = context.Subject.GetSubjectId();
                    var u = _users.FindBySubjectId(subjectId);

                    var claims = u.Claims.ToList().Select(r => new Claim(r.Type, r.Value));
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
