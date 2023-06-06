using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JavaScriptClient.Controllers
{
    public class LocalApiController : ControllerBase
    {

        [Route("local/identity")]
        [Authorize]
        public  IActionResult Get()
        {
            var user = User.Claims.SingleOrDefault(claim => claim.Type == "sub")?.Value;

            return new JsonResult(new { message = "local api success!" });
        }
    }
}
