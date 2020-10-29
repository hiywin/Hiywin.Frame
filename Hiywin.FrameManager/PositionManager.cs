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
    public class PositionManager : IPositionManager
    {
        private readonly IPositionService _service;
        public PositionManager(IPositionService service)
        {
            _service = service;
        }

        public async Task<ListResult<ISysPositionModel>> GetPositionAllAsync(QueryData<SysPositionQuery> query)
        {
            var lr = new ListResult<ISysPositionModel>();
            var dt = DateTime.Now;

            var res = await _service.GetPositionsAllAsync(query);
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

        public async Task<ListResult<ISysPositionModel>> GetPositionPageAsync(QueryData<SysPositionQuery> query)
        {
            var lr = new ListResult<ISysPositionModel>();
            var dt = DateTime.Now;

            var res = await _service.GetPositionsPageAsync(query);
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

        public async Task<ErrData<bool>> PositionSaveOrUpdateAsync(QueryData<SysPositionSaveOrUpdateQuery> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;

            query.Criteria.Creator = query.Extend.UserNo;
            query.Criteria.CreateName = query.Extend.UserName;
            query.Criteria.CreateTime = DateTime.Now;
            query.Criteria.Updator = query.Extend.UserNo;
            query.Criteria.UpdateName = query.Extend.UserName;
            query.Criteria.UpdateTime = DateTime.Now;

            var res = await _service.PositionSaveOrUpdateAsync(query);
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

        public async Task<ErrData<bool>> PositionDeleteAsync(QueryData<SysPositionDeleteQuery> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;

            var res = await _service.PositionDeleteAsync(query);
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

        public async Task<ListResult<ISysPositionRoleModel>> GetPositionRoleAllAsync(QueryData<SysPositionRoleQuery> query)
        {
            var lr = new ListResult<ISysPositionRoleModel>();
            var dt = DateTime.Now;

            var res = await _service.GetPositionRolesAllAsync(query);
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

        public async Task<ErrData<bool>> PositionRoleSaveOrUpdateAsync(QueryData<SysPositionRoleParams> param)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;

            var lstInfo = new List<ISysPositionRoleModel>();
            foreach (var item in param.Criteria.LstPositionRole)
            {
                var info = IoCContainer.Resolve<ISysPositionRoleModel>();
                info.PositionNo = param.Criteria.PositionNo;
                info.RoleNo = item.RoleNo;
                info.RoleName = item.RoleName;
                info.Creator = param.Extend.UserNo;
                info.CreateName = param.Extend.UserName;
                info.CreateTime = dt;
                lstInfo.Add(info);
            }
            var query = new QueryData<SysPositionRoleSaveOrUpdateQuery>()
            {
                Criteria = new SysPositionRoleSaveOrUpdateQuery()
                {
                    PositionNo = param.Criteria.PositionNo,
                    AppNo = param.Criteria.AppNo,
                    LstPositionRole = lstInfo
                }
            };
            var res = await _service.PositionRoleSaveOrUpdateAsync(query);
            if (res.HasErr)
            {
                result.SetInfo(false, res.ErrMsg, res.ErrCode);
            }
            else
            {
                result.SetInfo(true, "更新职业角色成功！", 200);
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }

        public async Task<ErrData<bool>> PositionRoleDeleteAsync(QueryData<SysPositionRoleDeleteQuery> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;

            var res = await _service.PositionRoleDeleteAsync(query);
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
