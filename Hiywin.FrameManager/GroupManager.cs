using Hiywin.Common.Data;
using Hiywin.Common.IoC;
using Hiywin.Dtos.Structs;
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
    public class GroupManager : IGroupManager
    {
        private readonly IGroupService _service;
        public GroupManager(IGroupService service)
        {
            _service = service;
        }

        public async Task<ListResult<ISysGroupModel>> GetGroupAllAsync(QueryData<SysGroupQuery> query)
        {
            var lr = new ListResult<ISysGroupModel>();
            var dt = DateTime.Now;

            var res = await _service.GetGroupsAllAsync(query);
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

        public async Task<ListResult<ISysGroupModel>> GetGroupPageAsync(QueryData<SysGroupQuery> query)
        {
            var lr = new ListResult<ISysGroupModel>();
            var dt = DateTime.Now;

            var res = await _service.GetGroupsPageAsync(query);
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

        public async Task<ErrData<bool>> GroupSaveOrUpdateAsync(QueryData<SysGroupSaveOrUpdateQuery> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;

            query.Criteria.Creator = query.Extend.UserNo;
            query.Criteria.CreateName = query.Extend.UserName;
            query.Criteria.CreateTime = DateTime.Now;
            query.Criteria.Updator = query.Extend.UserNo;
            query.Criteria.UpdateName = query.Extend.UserName;
            query.Criteria.UpdateTime = DateTime.Now;

            var res = await _service.GroupSaveOrUpdateAsync(query);
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

        public async Task<ErrData<bool>> GroupDeleteAsync(QueryData<SysGroupDeleteQuery> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;

            var res = await _service.GroupDeleteAsync(query);
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

        public async Task<ListResult<ISysGroupRoleModel>> GetGroupRolesAllAsync(QueryData<SysGroupRoleQuery> query)
        {
            var lr = new ListResult<ISysGroupRoleModel>();
            var dt = DateTime.Now;

            var res = await _service.GetGroupRolesAllAsync(query);
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

        public async Task<ErrData<bool>> GroupRoleSaveOrUpdateAsync(QueryData<SysGroupRoleParams> param)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;

            var lstInfo = new List<ISysGroupRoleModel>();
            foreach (var item in param.Criteria.LstGroupRole)
            {
                var info = IoCContainer.Resolve<ISysGroupRoleModel>();
                info.GroupNo = param.Criteria.GroupNo;
                info.RoleNo = item.RoleNo;
                info.RoleName = item.RoleName;
                info.Creator = param.Extend.UserNo;
                info.CreateName = param.Extend.UserName;
                info.CreateTime = dt;
                lstInfo.Add(info);
            }
            var query = new QueryData<SysGroupRoleSaveOrUpdateQuery>()
            {
                Criteria = new SysGroupRoleSaveOrUpdateQuery()
                {
                    GroupNo = param.Criteria.GroupNo,
                    AppNo = param.Criteria.AppNo,
                    LstGroupRole = lstInfo
                }
            };
            var res = await _service.GroupRoleSaveOrUpdateAsync(query);
            if (res.HasErr)
            {
                result.SetInfo(false, res.ErrMsg, res.ErrCode);
            }
            else
            {
                result.SetInfo(true, "更新组织角色成功！", 200);
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }

        public async Task<ErrData<bool>> GroupRoleDeleteAsync(QueryData<SysGroupRoleDeleteQuery> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;

            var res = await _service.GroupRoleDeleteAsync(query);
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

        public async Task<ListResult<ISysGroupUserModel>> GetGroupUsersAllAsync(QueryData<SysGroupUserQuery> query)
        {
            var lr = new ListResult<ISysGroupUserModel>();
            var dt = DateTime.Now;

            var res = await _service.GetGroupUsersAllAsync(query);
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

        public async Task<ErrData<bool>> GroupUserSaveOrUpdateAsync(QueryData<SysGroupUserParams> param)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;

            var lstInfo = new List<ISysGroupUserModel>();
            foreach (var item in param.Criteria.LstGroupUser)
            {
                if (item.GroupMaster)//组织总负责人只能有一个
                {
                    lstInfo.ForEach(p => p.GroupMaster = false);
                }
                var info = IoCContainer.Resolve<ISysGroupUserModel>();
                info.GroupNo = param.Criteria.GroupNo;
                info.UserNo = item.UserNo;
                info.UserName = item.UserName;
                info.GroupMaster = item.GroupMaster;
                info.GroupManager = item.GroupManager;
                info.Creator = param.Extend.UserNo;
                info.CreateName = param.Extend.UserName;
                info.CreateTime = dt;
                lstInfo.Add(info);
            }
            var query = new QueryData<SysGroupUserSaveOrUpdateQuery>()
            {
                Criteria = new SysGroupUserSaveOrUpdateQuery()
                {
                    GroupNo = param.Criteria.GroupNo,
                    LstGroupUser = lstInfo
                }
            };
            var res = await _service.GroupUserSaveOrUpdateAsync(query);
            if (res.HasErr)
            {
                result.SetInfo(false, res.ErrMsg, res.ErrCode);
            }
            else
            {
                result.SetInfo(true, "更新组织用户成功！", 200);
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }

        public async Task<ErrData<bool>> GroupUserDeleteAsync(QueryData<SysGroupUserDeleteQuery> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;

            var res = await _service.GroupUserDeleteAsync(query);
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
    }
}
