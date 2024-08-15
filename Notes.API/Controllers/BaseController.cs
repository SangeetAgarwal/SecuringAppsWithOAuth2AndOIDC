using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes.API.Common.Interfaces.Servicelayer;

namespace Notes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
        protected IDatalayerService DatalayerService;
        protected ILogger<BaseController> Logger;
        public BaseController(ILogger<BaseController> logger)
        {
            Logger = logger;
        }
        public BaseController(IDatalayerService datalayerService, ILogger<BaseController> logger)
        {
            DatalayerService = datalayerService;
            Logger = logger;
        }
    }
}
