using Hiywin.Common.Data;
using Hiywin.Dtos.Frame;
using Hiywin.Dtos.Structs;
using Hiywin.Entities.Frame;
using Hiywin.IFrameManager;
using Hiywin.IFrameService;
using Hiywin.IFrameService.Structs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hiywin.FrameManager
{
    public class ModuleManager : IModuleManager
    {
        private readonly IModuleService _service;

        public ModuleManager(IModuleService service)
        {
            _service = service;
        }

        public async Task<ListResult<ISysModuleModel>> GetModuleAllAsync(QueryData<SysModuleQuery> query)
        {
            var lr = new ListResult<ISysModuleModel>();
            var dt = DateTime.Now;

            var res = await _service.GetModulesAllAsync(query);
            if (res.HasErr)
            {
                lr.SetInfo(res.ErrMsg, res.ErrCode);
            }
            else
            {
                foreach (var item in res.Data)
                {
                    lr.Results.Add(item);
                }
                lr.SetInfo("成功", 200);
            }

            lr.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return lr;
        }

        public async Task<ListResult<ISysModuleModel>> GetModulePageAsync(QueryData<SysModuleQuery> query)
        {
            var lr = new ListResult<ISysModuleModel>();
            var dt = DateTime.Now;

            var res = await _service.GetModulesPageAsync(query);
            if (res.HasErr)
            {
                lr.SetInfo(res.ErrMsg, res.ErrCode);
            }
            else
            {
                foreach (var item in res.Data)
                {
                    lr.Results.Add(item);
                }
                lr.PageModel = res.PageInfo;
                lr.SetInfo("成功", 200);
            }

            lr.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return lr;
        }

        public async Task<ErrData<bool>> ModuleSaveOrUpdateAsync(QueryData<SysModuleSaveOrUpdateQuery> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;

            query.Criteria.Creator = query.Extend.UserNo;
            query.Criteria.CreateName = query.Extend.UserName;
            query.Criteria.CreateTime = DateTime.Now;
            query.Criteria.Updator = query.Extend.UserNo;
            query.Criteria.UpdateName = query.Extend.UserName;
            query.Criteria.UpdateTime = DateTime.Now;

            var res = await _service.ModuleSaveOrUpdateAsync(query);
            if (res.HasErr)
            {
                result.SetInfo(false, res.ErrMsg, res.ErrCode);
            }
            else
            {
                result.SetInfo(true, res.ErrMsg, 200);
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }

        public async Task<ErrData<bool>> ModuleDeleteAsync(QueryData<SysModuleDeleteQuery> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;

            var res = await _service.ModuleDeleteAsync(query);
            if (res.HasErr)
            {
                result.SetInfo(false, res.ErrMsg, res.ErrCode);
            }
            else
            {
                result.SetInfo(true, "删除成功！", 200);
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }

        public async Task<ListResult<SysModuleTreeDto>> GetModuleTreeAsync(QueryData<SysModuleTreeParam> query)
        {
            var lr = new ListResult<SysModuleTreeDto>();
            var dt = DateTime.Now;

            var queryAll = new QueryData<SysModuleQuery>()
            {
                Criteria = new SysModuleQuery()
                {
                    ParentNo = string.Empty,
                    IsParentNo = null,
                    IsDelete = false,
                    AppNo = query.Criteria.AppNo
                }
            };
            var resAll = await GetModuleAllAsync(queryAll);
            if (resAll.HasErr)
            {
                lr.SetInfo("获取父模块列表失败，请重试！", -201);
            }
            else
            {
                var resParent = resAll.Results.FindAll(p => p.ParentNo == string.Empty);
                if (!string.IsNullOrEmpty(query.Criteria.ModuleName))
                {
                    resParent = resParent.FindAll(p => p.ModuleName.Contains(query.Criteria.ModuleName));
                }
                foreach (var parentModule in resParent)
                {
                    var module = new SysModuleTreeDto();
                    module.ModuleNo = parentModule.ModuleNo;
                    module.ModuleName = parentModule.ModuleName;
                    module.ParentNo = parentModule.ParentNo;
                    module.Icon = parentModule.Icon;
                    module.AppNo = parentModule.AppNo;
                    module.Sort = parentModule.Sort;
                    module.IsDelete = parentModule.IsDelete;

                    var childrenModules = GetModuleByParentSync(module, resAll);
                    if (childrenModules.Count > 0)
                    {
                        module.Children = childrenModules;
                    }

                    lr.Results.Add(module);
                }
                lr.SetInfo("获取模块树成功！", 200);
            }

            lr.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return lr;
        }

        private List<SysModuleTreeDto> GetModuleByParentSync(SysModuleTreeDto parentModule, ListResult<ISysModuleModel> resAll)
        {
            var lr = new List<SysModuleTreeDto>();

            var  resChildren = resAll.Results.FindAll(p => p.ParentNo == parentModule.ModuleNo);
            foreach (var childrenModule in resChildren)
            {
                var module = new SysModuleTreeDto();
                module.ModuleNo = childrenModule.ModuleNo;
                module.ModuleName = childrenModule.ModuleName;
                module.ParentNo = childrenModule.ParentNo;
                module.Icon = childrenModule.Icon;
                module.AppNo = childrenModule.AppNo;
                module.Sort = childrenModule.Sort;
                module.IsDelete = childrenModule.IsDelete;

                var childrenModules = GetModuleByParentSync(module, resAll);
                if (childrenModules.Count > 0)
                {
                    module.Children = childrenModules;
                }

                lr.Add(module);
            }

            return lr;
        }
    }
}
