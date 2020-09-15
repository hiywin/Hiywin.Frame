using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hiywin.Api.Models.Account;
using Hiywin.Common.Data;
using Hiywin.Common.IoC;
using Hiywin.Common.Jwt;
using Hiywin.IFrameManager;
using Hiywin.IFrameService.Structs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hiywin.Api.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        IAccountManager _manager;
        IJwtFactory _jwtFactory;
        public AccountController()
        {
            _manager = IoCContainer.Resolve<IAccountManager>();
            _jwtFactory = IoCContainer.Resolve<IJwtFactory>();
        }

        /// <summary>
        /// 启动提示
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("app_loaded")]
        public ActionResult AppLoaded()
        {
            string result = "程序启动成功...";

            return Ok(result);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost,Route("register")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            var query = new QueryData<SysUserSaveOrUpdateQuery>()
            {
                Criteria = new SysUserSaveOrUpdateQuery()
                {
                    UserName = model.UserName,
                    StaffNo = model.StaffNo,
                    AdAccount = model.AdAccount,
                    Pwd = model.Password,
                    App = (int)model.App
                }
            };
            var result = await _manager.RegisterAsync(query);

            return Ok(result);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost,Route("login")]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            var result = new ErrData<string>();

            var query = new QueryData<SysUserGetQuery>()
            {
                Criteria = new SysUserGetQuery()
                {
                    UserName = model.UserName,
                    StaffNo = model.StaffNo,
                    AdAccount = model.AdAccount,
                    Pwd = model.Password,
                    App = (int)model.App
                }
            };
            var res = await _manager.LoginAsync(query);
            if (res.HasErr)
            {
                result.SetInfo(res.Msg, res.Code);
            }
            else
            {
                var user = new LoginUser();
                user.UserNo = res.Data.UserNo;
                user.UserName = res.Data.UserName;
                user.AdAccount = res.Data.AdAccount;
                user.IsAdmin = res.Data.IsAdmin;
                user.StaffNo = res.Data.StaffNo;

                var claimsIdentity = _jwtFactory.GenerateClaimsIdentity(user);
                var tokenJson = await _jwtFactory.GenerateEncodedToken(user.UserNo, claimsIdentity);
                var token = JsonConvert.DeserializeObject<TokenModel>(tokenJson);

                result.SetInfo(token.auth_token, "登录成功！", 200);
            }
            result.ExpandSeconds = res.ExpandSeconds;

            return Ok(result);
        }

        /// <summary>
        /// 登录后通过token获取当前用户信息
        /// </summary>
        /// <returns></returns>
        [Authorize, HttpGet, Route("get_current_user")]
        public ActionResult GetCurrentUser()
        {
            var user = CurrentUser;

            return Ok(user);
        }
    }
}
