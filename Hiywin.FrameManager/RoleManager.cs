using Hiywin.Common.Data;
using Hiywin.Entities.Frame;
using Hiywin.IFrameManager;
using Hiywin.IFrameService;
using Hiywin.IFrameService.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiywin.FrameManager
{
    public class RoleManager : IRoleManager
    {
        private readonly IRoleService _service;
        public RoleManager(IRoleService service)
        {
            _service = service;
        }

        public async Task<ListResult<ISysRoleModel>> GetRoleAllAsync(QueryData<SysRoleQuery> query)
        {
            var lr = new ListResult<ISysRoleModel>();
            var dt = DateTime.Now;

            var res = await _service.GetRolesAllAsync(query);
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

        public async Task<ListResult<ISysRoleModel>> GetRolePageAsync(QueryData<SysRoleQuery> query)
        {
            var lr = new ListResult<ISysRoleModel>();
            var dt = DateTime.Now;

            var res = await _service.GetRolesPageAsync(query);
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

        public async Task<ErrData<bool>> RoleSaveOrUpdateAsync(QueryData<SysRoleSaveOrUpdateQuery> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;

            query.Criteria.Creator = query.Extend.UserNo;
            query.Criteria.CreateName = query.Extend.UserName;
            query.Criteria.CreateTime = DateTime.Now;
            query.Criteria.Updator = query.Extend.UserNo;
            query.Criteria.UpdateName = query.Extend.UserName;
            query.Criteria.UpdateTime = DateTime.Now;

            var res = await _service.RoleSaveOrUpdateAsync(query);
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

        public async Task<ErrData<bool>> RoleDeleteAsync(QueryData<SysRoleDeleteQuery> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;

            var res = await _service.RoleDeleteAsync(query);
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

        public async Task<ListResult<ISysRoleModuleModel>> GetRoleModulePageAsync(QueryData<SysRoleModuleQuery> query)
        {
            var lr = new ListResult<ISysRoleModuleModel>();
            var dt = DateTime.Now;

            var res = await _service.GetRoleModulesPageAsync(query);
            if (res.HasErr)
            {
                lr.SetInfo(res.ErrMsg, res.ErrCode);
            }
            else
            {
                foreach (var item in res.Data)
                {
                    var queryPower = new QueryData<SysRolePowerQuery>()
                    {
                        Criteria = new SysRolePowerQuery()
                        {
                            ModuleNo = item.ModuleNo
                        }
                    };
                    var powers = await GetRolePowerAllAsync(queryPower);
                    item.RolePowers = powers.Results;
                    lr.Results.Add(item);
                }
                lr.PageModel = res.PageInfo;
                lr.SetInfo("成功", 200);
            }

            lr.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return lr;
        }

        public async Task<ListResult<ISysRolePowerModel>> GetRolePowerAllAsync(QueryData<SysRolePowerQuery> query)
        {
            var lr = new ListResult<ISysRolePowerModel>();
            var dt = DateTime.Now;

            var res = await _service.GetRolePowersAllAsync(query);
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
    }
}
