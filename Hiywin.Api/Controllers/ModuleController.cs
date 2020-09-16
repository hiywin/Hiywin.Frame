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

        /// <summary>
        /// 分页获取模块列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 分页获取模块列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPost, Route("get_modules_all")]
        public async Task<ActionResult> GetModulesAllAsync(GetModuleAllViewModel model)
        {
            var query = new QueryData<SysModuleQuery>()
            {
                Criteria = new SysModuleQuery()
                {
                    ModuleNo = model.ModuleNo,
                    ModuleName = model.ModuleName,
                    IsDelete = model.IsDelete
                }
            };

            var result = await _manager.GetModluleAllAsync(query);

            return Ok(result);
        }

        /// <summary>
        /// 新增或修改模块信息（ModuleNo为空时新增，不为空时修改）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
                    Sort = model.Sort,
                    RouterName = model.RouterName
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

        /// <summary>
        /// 删除模块信息（IsDelete=true为软删除，IsDelete=false为物理删除）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize,HttpPost,Route("module_delete")]
        public async Task<ActionResult> ModuleDeleteAsync(ModuleDeleteViewModel model)
        {
            var query = new QueryData<SysModuleDeleteQuery>()
            {
                Criteria = new SysModuleDeleteQuery()
                {
                    ModuleNo = model.ModuleNo,
                    IsDelete = model.IsDelete
                }
            };
            var result = await _manager.ModuleDeleteAsync(query);

            return Ok(result);
        }
    }
}
