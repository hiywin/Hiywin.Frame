using Hiywin.Common.IoC;
using Hiywin.IFrameManager;
using Microsoft.AspNetCore.Mvc;

namespace Hiywin.Api.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        IModuleManager _manager;
        public TestController()
        {
            _manager = IoCContainer.Resolve<IModuleManager>();
        }

        [HttpGet, HttpPost,Route("get_modules")]
        public ActionResult GetModules()
        {
            var result = _manager.GetModuleManager();

            return Ok(result);
        }
    }
}
