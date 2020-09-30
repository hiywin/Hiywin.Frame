using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hiywin.Api.Models.Company;
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
    public class CompanyController : BaseController
    {
        private readonly ICompanyManager _manager;
        public CompanyController()
        {
            _manager = IoCContainer.Resolve<ICompanyManager>();
        }

        [Authorize,HttpPost,Route("get_companys_all")]
        public async Task<ActionResult> GetCompanysAllAsync(GetCompanyAllViewModel model)
        {
            var query = new QueryData<SysCompanyQuery>()
            {
                Criteria = new SysCompanyQuery()
                {
                    CompanyNo = model.CompanyNo,
                    CompanyName = model.CompanyName,
                    Address = model.Address,
                    Mobile = model.Mobile,
                    Industry = model.Industry,
                    LegalPerson = model.LegalPerson,
                    IsDelete = model.IsDelete
                }
            };
            var result = await _manager.GetCompanyAllAsync(query);

            return Ok(result);
        }

        [Authorize,HttpPost,Route("get_companys_page")]
        public async Task<ActionResult> GetCompanysPageAsync(GetCompanyPageViewModel model)
        {
            var query = new QueryData<SysCompanyQuery>()
            {
                Criteria = new SysCompanyQuery()
                {
                    CompanyNo = model.CompanyNo,
                    CompanyName = model.CompanyName,
                    Address = model.Address,
                    Mobile = model.Mobile,
                    Industry = model.Industry,
                    LegalPerson = model.LegalPerson,
                    IsDelete = model.IsDelete
                },
                PageModel = model.PageModel
            };
            var result = await _manager.GetCompanyPageAsync(query);

            return Ok(result);
        }

        [Authorize,HttpPost,Route("company_save_or_update")]
        public async Task<ActionResult> CompanySaveOrUpdateAsync(CompanySaveOrUpdateViewModel model)
        {
            var query = new QueryData<SysCompanySaveOrUpdateQuery>()
            {
                Criteria = new SysCompanySaveOrUpdateQuery()
                {
                    CompanyNo = model.CompanyNo,
                    CompanyName = model.CompanyName,
                    Abbreviation = model.Abbreviation,
                    Address = model.Address,
                    Industry = model.Industry,
                    LegalPerson = model.LegalPerson,
                    Contact = model.Contact,
                    Phone = model.Phone,
                    Mobile = model.Mobile,
                    Email = model.Email,
                    Website = model.Website,
                    Access = model.Access,
                    IsDelete = model.IsDelete
                },
                Extend = new QueryExtend()
                {
                    UserNo = CurrentUser.UserNo,
                    UserName = CurrentUser.UserName
                }
            };
            var result = await _manager.CompanySaveOrUpdateAsync(query);

            return Ok(result);
        }

        [Authorize,HttpPost,Route("company_delete")]
        public async Task<ActionResult> CompanyDeleteAsync(CompanyDeleteViewModel model)
        {
            var query = new QueryData<SysCompanyDeleteQuery>()
            {
                Criteria = new SysCompanyDeleteQuery()
                {
                    CompanyNo = model.CompanyNo,
                    IsDelete = model.IsDelete
                }
            };
            var result = await _manager.CompanyDeleteAsync(query);

            return Ok(result);
        }
    }
}
