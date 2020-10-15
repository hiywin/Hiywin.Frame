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
    public class UserManager : IUserManager
    {
        private readonly IUserService _service;
        public UserManager(IUserService service)
        {
            _service = service;
        }

        public async Task<ListResult<ISysUserModel>> GetUserAllAsync(QueryData<SysUserQuery> query)
        {
            var lr = new ListResult<ISysUserModel>();
            var dt = DateTime.Now;

            var res = await _service.GetUserAllAsync(query);
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

        public async Task<ListResult<ISysUserModel>> GetUserPageAsync(QueryData<SysUserQuery> query)
        {
            var lr = new ListResult<ISysUserModel>();
            var dt = DateTime.Now;

            var res = await _service.GetUserPageAsync(query);
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

        public async Task<ErrData<bool>> UserSaveOrUpdateAsync(QueryData<SysUserSaveOrUpdateQuery> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;

            #region 验证输入合法性
            if (query.Criteria.IsAdmin && !query.Extend.IsAdmin)
            {
                result.SetInfo(false, "您无权限设置管理员账号信息！", -201);
                result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
                return result;
            }
            if (string.IsNullOrEmpty(query.Criteria.CompanyNo))
            {
                if (!string.IsNullOrEmpty(query.Criteria.StaffNo))
                {
                    result.SetInfo(false, "工号不为空时，请先选择所属公司！", -201);
                    result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
                    return result;
                }
                if (!string.IsNullOrEmpty(query.Criteria.AdAccount))
                {
                    result.SetInfo(false, "AD账号不为空时，请先选择所属公司！", -201);
                    result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
                    return result;
                }
            }
            if (query.Criteria.Access && string.IsNullOrEmpty(query.Criteria.Descr))
            {
                result.SetInfo(false, "设置账户为有效时，请输入审批描述！", -201);
                result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
                return result;
            }
            if (!query.Criteria.Access && string.IsNullOrEmpty(query.Criteria.RejectedReason))
            {
                result.SetInfo(false, "设置账户为无效时，请输入拒绝理由！", -201);
                result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
                return result;
            }
            #endregion

            query.Criteria.Creator = query.Extend.UserNo;
            query.Criteria.CreateName = query.Extend.UserName;
            query.Criteria.CreateTime = DateTime.Now;
            query.Criteria.Updator = query.Extend.UserNo;
            query.Criteria.UpdateName = query.Extend.UserName;
            query.Criteria.UpdateTime = DateTime.Now;
            query.Criteria.RegisterTime = DateTime.Now;            
            if (!string.IsNullOrEmpty(query.Criteria.Descr))
            {
                query.Criteria.ApprovedBy = query.Extend.UserNo;
                query.Criteria.ApprovedName = query.Extend.UserName;
                query.Criteria.ApprovedTime = DateTime.Now;
            }
            if (!string.IsNullOrEmpty(query.Criteria.RejectedReason))
            {
                query.Criteria.RejectedBy = query.Extend.UserNo;
                query.Criteria.RejectedName = query.Extend.UserName;
                query.Criteria.RejectedTime = DateTime.Now;
            }
            
            var res = await _service.UserSaveOrUpdateAsync(query);
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

        public async Task<ErrData<bool>> UserDeleteAsync(QueryData<SysUserDeleteQuery> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;

            var res = await _service.UserDeleteAsync(query);
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
