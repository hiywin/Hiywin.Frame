using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hiywin.Api.Models.Dictionary;
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
    public class DictionaryController : BaseController
    {
        IDictionaryManager _manager;
        public DictionaryController()
        {
            _manager = IoCContainer.Resolve<IDictionaryManager>();
        }

        [Authorize,HttpPost,Route("get_dictionarys_all")]
        public async Task<ActionResult> GetDictionarysAllAsync(GetDictionaryAllViewModel model)
        {
            var query = new QueryData<SysDictionaryQuery>()
            {
                Criteria = new SysDictionaryQuery()
                {
                    Type = model.Type,
                    TypeName = model.TypeName,
                    ParentNo = model.ParentNo,
                    IsDelete = model.IsDelete
                }
            };
            var result = await _manager.GetDictionaryAllAsync(query);


            return Ok(result);
        }

        [Authorize, HttpPost, Route("get_dictionarys_page")]
        public async Task<ActionResult> GetDictionarysPageAsync(GetDictionaryPageViewModel model)
        {
            var query = new QueryData<SysDictionaryQuery>()
            {
                Criteria = new SysDictionaryQuery()
                {
                    Type = model.Type,
                    TypeName = model.TypeName,
                    Content = model.Content,
                    ParentNo = model.ParentNo,
                    IsDelete = model.IsDelete
                },
                PageModel = model.PageModel
            };
            var result = await _manager.GetDictionaryPageAsync(query);

            return Ok(result);
        }

        [Authorize, HttpPost, Route("dictionary_save_or_update")]
        public async Task<ActionResult> DictionarySaveOrUpdateAsync(DictionarySaveOrUpdateViewModel model)
        {
            var query = new QueryData<SysDictionarySaveOrUpdateQuery>()
            {
                Criteria = new SysDictionarySaveOrUpdateQuery()
                {
                    DictionaryNo=model.DictionaryNo,
                    Type=model.Type,
                    TypeName=model.TypeName,
                    Content=model.Content,
                    Code=model.Code,
                    ParentNo=model.ParentNo,
                    Descr=model.Descr,
                    CompanyNo=model.CompanyNo,
                    IsDelete=model.IsDelete
                },
                Extend = new QueryExtend()
                {
                    UserNo = CurrentUser.UserNo,
                    UserName = CurrentUser.UserName
                }
            };
            var result = await _manager.DictionarySaveOrUpdateAsync(query);

            return Ok(result);
        }

        [Authorize, HttpPost, Route("dictionary_delete")]
        public async Task<ActionResult> DictionaryDeleteAsync(DictionaryDeleteViewModel model)
        {
            var query = new QueryData<SysDictionaryDeleteQuery>()
            {
                Criteria = new SysDictionaryDeleteQuery()
                {
                    DictionaryNo = model.DictionaryNo,
                    IsDelete = model.IsDelete
                }
            };
            var result = await _manager.DictionaryDeleteAsync(query);

            return Ok(result);
        }
    }
}
