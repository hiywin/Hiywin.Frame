using Hiywin.Api.Models;
using Hiywin.Common.Data;
using Hiywin.Common.IoC;
using Hiywin.Common.Jwt;
using Hiywin.IFrameManager;
using Hiywin.IFrameService.Structs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Hiywin.Api.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class TestController : BaseController
    {
        private readonly IModuleManager _manager;
        private readonly IJwtFactory _jwtFactory;
        public TestController()
        {
            _manager = IoCContainer.Resolve<IModuleManager>();
            _jwtFactory = IoCContainer.Resolve<IJwtFactory>();
        }

        /// <summary>
        /// 启动提示
        /// </summary>
        /// <returns></returns>
        [HttpGet,Route("app_loaded")]
        public ActionResult AppLoaded()
        {
            string result = "程序启动成功...";

            return Ok(result);
        }

        /// <summary>
        /// 测试登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("login")]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            var result = new ErrData<string>();

            var user = new LoginUser();
            user.UserNo = "L001";
            user.UserName = model.UserName;
            user.AdAccount = "luo.wen.hai";
            user.IsAdmin = true;

            var claimsIndentity = _jwtFactory.GenerateClaimsIdentity(user);
            var tokenJson = await _jwtFactory.GenerateEncodedToken(user.UserNo, claimsIndentity);
            var token = JsonConvert.DeserializeObject<TokenModel>(tokenJson);

            result.SetInfo(token.auth_token, "成功", 200);

            return Ok(result);
        }

        /// <summary>
        /// 登录后通过token获取当前用户信息
        /// </summary>
        /// <returns></returns>
        [Authorize,HttpGet,Route("get_current_user")]
        public ActionResult GetCurrentUser()
        {
            var user = CurrentUser;

            return Ok(user);
        }

        [HttpGet, HttpPost,Route("get_modules")]
        public async Task<ActionResult> GetModules(ModuleViewModel model)
        {
            var query = new QueryData<SysModuleQuery>()
            {
                Criteria = new SysModuleQuery
                {
                    ModuleName = model.ModuleName,
                    IsDelete = false
                }
            };
            var result = await _manager.GetModluleAllAsync(query);

            return Ok(result);
        }
    }
}
