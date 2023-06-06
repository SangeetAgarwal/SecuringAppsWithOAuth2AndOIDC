using Microsoft.AspNetCore.Authorization;

namespace Notes.Api.Authorization
{
    public class MustOwnResourceRequirement : IAuthorizationRequirement
    {
        public MustOwnResourceRequirement()
        {

        }
    }
}
