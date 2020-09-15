using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hiywin.Api.Models.Module;
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
    public class ModuleController : BaseController
    {
        private readonly IModuleManager _manager;
        public ModuleController()
        {
            _manager = IoCContainer.Resolve<IModuleManager>();
        }

        [Authorize,HttpPost,Route("get_modules_page")]
        public async Task<ActionResult> GetModulesPageAsync(GetModulePageViewModel model)
        {
            var query = new QueryData<SysModuleQuery>()
            {
                Criteria = new SysModuleQuery()
                {
                    ModuleNo = model.ModuleNo,
                    ModuleName = model.ModuleName,
                    ParentNo = model.ParentNo,
                    App = model.App,
                    IsDelete = model.IsDelete,
                    IsParentNo = true,
                },
                PageModel = model.PageModel
            };

            var result = await _manager.GetModlulePageAsync(query);

            return Ok(result);
        }

        [Authorize,HttpPost,Route("module_save_or_update")]
        public async Task<ActionResult> ModuleSaveOrUpdateAsync(ModuleSaveOrUpdateViewModel model)
        {
            var query = new QueryData<SysModuleSaveOrUpdateQuery>()
            {
                Criteria = new SysModuleSaveOrUpdateQuery()
                {
                    ModuleNo = model.ModuleNo,
                    ModuleName = model.ModuleName,
                    ParentNo = model.ParentNo,
                    Icon = model.Icon,
                    Url = model.Url,
                    Category = model.Category,
                    Target = model.Target,
                    IsResource = model.IsResource,
                    App = model.App,
                    IsDelete = model.IsDelete,
                    Sort = model.Sort
                },
                Extend = new QueryExtend()
                {
                    UserNo = CurrentUser.UserNo,
                    UserName = CurrentUser.UserName
                }
            };
            var result = await _manager.ModuleSaveOrUpdateAsync(query);

            return Ok(result);
        }
    }
}
