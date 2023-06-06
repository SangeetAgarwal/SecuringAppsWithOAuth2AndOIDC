using Microsoft.AspNetCore.Authorization;
using Notes.API.Common.Interfaces.Servicelayer;

namespace Notes.Api.Authorization
{
    public class MustOwnResourceHandler : AuthorizationHandler<MustOwnResourceRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDatalayerService _datalayerService;

        public MustOwnResourceHandler(
            IHttpContextAccessor httpContextAccessor, IDatalayerService datalayerService)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _datalayerService = datalayerService;
        }
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            MustOwnResourceRequirement requirement)
        {
            var resourceId = _httpContextAccessor.HttpContext?.GetRouteValue("id")?.ToString();
            var userId = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(r => r.Type == "sub")?.Value;

            if (resourceId == null || userId == null || !Guid.TryParse(resourceId, out var resourceIdAsGuid) || !Guid.TryParse(userId, out var userIdAsGuid))
            {
                context.Fail();
                return;
            }
            
            var owns = await _datalayerService.IsNoteOwnerAsync(resourceIdAsGuid, userIdAsGuid);

            if (!owns)
            {
                context.Fail();
                return;
            }

            context.Succeed(requirement);
            
        }
    }
}
