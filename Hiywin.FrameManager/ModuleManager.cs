using Hiywin.Common.Data;
using Hiywin.Entities.Frame;
using Hiywin.IFrameManager;
using Hiywin.IFrameService;
using Hiywin.IFrameService.Structs;
using Hiywin.Models.Frame;
using System;
using System.Threading.Tasks;

namespace Hiywin.FrameManager
{
    public class ModuleManager : IModuleManager
    {
        IModuleService _service;

        public ModuleManager(IModuleService service)
        {
            _service = service;
        }
        public async Task<ListResult<ISysModuleModel>> GetModluleAllAsync()
        {
            var lr = new ListResult<ISysModuleModel>();
            var dt = DateTime.Now;

            var queryEx = new QueryData<SysModuleQuery>()
            {
                Criteria = new SysModuleQuery()
                {
                    IsDelete = false
                }
            };
            var res = await _service.GetModulesAllAsync(queryEx);
            if (res.HasErr)
            {
                lr.SetInfo(res.ErrMsg, res.ErrCode);
            }
            else
            {
                foreach (var item in res.Data)
                {
                    var info = new SysModuleModel();
                    info.ModuleNo = item.ModuleNo;
                    info.ModuleName = item.ModuleName;
                    info.ParentNo = item.ParentNo;
                    info.Icon = item.Icon;
                    info.Url = item.Url;
                    info.Category = item.Category;
                    info.Target = item.Target;
                    info.IsResource = item.IsResource;
                    info.App = item.App;
                    info.Sort = item.Sort;
                    lr.Results.Add(info);
                }
                lr.SetInfo("成功", 200);
            }

            lr.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return lr;
        }
    }
}
