using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hiywin.Api.Models.App;
using Hiywin.Common.Data;
using Hiywin.Common.IoC;
using Hiywin.IFrameManager;
using Hiywin.IFrameService.Structs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hiywin.Api.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class AppController : BaseController
    {
        private readonly IAppManager _manager;
        public AppController()
        {
            _manager = IoCContainer.Resolve<IAppManager>();
        }

        [Authorize,HttpPost,Route("get_apps_all")]
        public async Task<ActionResult> GetAppsAllAsync(GetAppAllViewModel model)
        {
            var query = new QueryData<SysAppQuery>()
            {
                Criteria = new SysAppQuery()
                {
                    AppNo = model.AppNo,
                    AppName = model.AppName,
                    Leader = model.Leader,
                    IsDelete = model.IsDelete
                }
            };
            var result = await _manager.GetAppAllAsync(query);

            return Ok(result);
        }

        [Authorize,HttpPost,Route("get_apps_page")]
        public async Task<ActionResult> GetAppsPageAsync(GetAppPageViewModel model)
        {
            var query = new QueryData<SysAppQuery>()
            {
                Criteria = new SysAppQuery()
                {
                    AppNo = model.AppNo,
                    AppName = model.AppName,
                    Leader = model.Leader,
                    IsDelete = model.IsDelete
                },
                PageModel = model.PageModel
            };
            var result = await _manager.GetAppPageAsync(query);

            return Ok(result);
        }

        [Authorize,HttpPost,Route("app_save_or_update")]
        public async Task<ActionResult> AppSaveOrUpdateAsync(AppSaveOrUpdateViewModel model)
        {
            var query = new QueryData<SysAppSaveOrUpdateQuery>()
            {
                Criteria = new SysAppSaveOrUpdateQuery()
                {
                    AppNo = model.AppNo,
                    AppName = model.AppName,
                    Leader = model.Leader,
                    Deploy = model.Deploy,
                    Remark = model.Remark,
                    IsDelete = model.IsDelete
                },
                Extend = new QueryExtend()
                {
                    UserNo = CurrentUser.UserNo,
                    UserName = CurrentUser.UserName
                }
            };
            var result = await _manager.AppSaveOrUpdateAsync(query);

            return Ok(result);
        }

        [Authorize,HttpPost,Route("app_delete")]
        public async Task<ActionResult> AppDeleteAsync(AppDeleteViewModel model)
        {
            var query = new QueryData<SysAppDeleteQuery>()
            {
                Criteria = new SysAppDeleteQuery()
                {
                    AppNo = model.AppNo,
                    IsDelete = model.IsDelete
                }
            };
            var result = await _manager.AppDeleteAsync(query);

            return Ok(result);
        }
    }
}
