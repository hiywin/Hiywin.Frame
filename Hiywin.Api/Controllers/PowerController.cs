using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hiywin.Api.Models.Power;
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
    public class PowerController : BaseController
    {
        IPowerManager _manager;
        public PowerController()
        {
            _manager = IoCContainer.Resolve<IPowerManager>();
        }

        [Authorize,HttpPost,Route("get_powers_all")]
        public async Task<ActionResult> GetPowersAllAsync(GetPowerAllViewModel model)
        {
            var query = new QueryData<SysPowerQuery>()
            {
                Criteria = new SysPowerQuery()
                {
                    ModuleNo = model.ModudelNo,
                    PowerNo = model.PowerNo,
                    IsDelete = model.IsDelete
                }
            };
            var result = await _manager.GetPowerAllAsync(query);

            return Ok(result);
        }

        [Authorize,HttpPost,Route("get_powers_page")]
        public async Task<ActionResult> GetPowersPageAsync(GetPowerPageViewModel model)
        {
            var query = new QueryData<SysPowerQuery>()
            {
                Criteria = new SysPowerQuery()
                {
                    ModuleNo = model.ModudelNo,
                    PowerNo = model.PowerNo,
                    IsDelete = model.IsDelete
                },
                PageModel =model.PageModel
            };
            var result = await _manager.GetPowerPageAsync(query);

            return Ok(result);
        }

        [Authorize,HttpPost,Route("power_save_or_update")]
        public async Task<ActionResult> PowerSaveOrUpdateAsync(PowerSaveOrUpdateViewModel model)
        {
            var query = new QueryData<SysPowerSaveOrUpdateQuery>()
            {
                Criteria = new SysPowerSaveOrUpdateQuery()
                {
                    PowerNo = model.PowerNo,
                    ModuleNo = model.ModuleNo,
                    PowerID = model.PowerID,
                    Content = model.Content,
                    Type = model.Type,
                    Style = model.Style,
                    FuncName = model.FuncName,
                    Icon = model.Icon,
                    Sort = model.Sort,
                    Access = model.Access,
                    IsDelete = model.IsDelete,
                    IsPlain = model.IsPlain,
                    IsRound = model.IsRound,
                    IsCircle = model.IsCircle
                },
                Extend = new QueryExtend()
                {
                    UserNo = CurrentUser.UserNo,
                    UserName = CurrentUser.UserName
                }
            };
            var result = await _manager.PowerSaveOrUpdateAsync(query);

            return Ok(result);
        }

        [Authorize,HttpPost,Route("power_delete")]
        public async Task<ActionResult> PowerDeleteAsync(PowerDeleteViewModel model)
        {
            var query = new QueryData<SysPowerDeleteQuery>()
            {
                Criteria = new SysPowerDeleteQuery()
                {
                    PowerNo = model.PowerNo,
                    IsDelete = model.IsDelete
                }
            };

            var result = await _manager.PowerDeleteAsync(query);

            return Ok(result);
        }
    }
}
