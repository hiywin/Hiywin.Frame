using Hiywin.Common.Data;
using Hiywin.Entities.Frame;
using Hiywin.IFrameManager;
using Hiywin.IFrameService;
using Hiywin.IFrameService.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiywin.FrameManager
{
    public class AccountManager : IAccountManager
    {
        private readonly IUserService _service;
        public AccountManager(IUserService service)
        {
            _service = service;
        }

        public async Task<ErrData<bool>> RegisterAsync(QueryData<SysUserSaveOrUpdateQuery> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;

            query.Criteria.Creator = "Register";
            query.Criteria.CreateName = "注册";
            query.Criteria.CreateTime = DateTime.Now;
            query.Criteria.RegisterTime = DateTime.Now;
            query.Criteria.Access = false;

            var res = await _service.UserSaveOrUpdateAsync(query);
            if (res.HasErr)
            {
                if (res.ErrCode == -101)
                {
                    result.SetInfo(false, "注册新用户失败，请重试！", res.ErrCode);
                }
                else
                {
                    result.SetInfo(false, res.ErrMsg, res.ErrCode);
                }
            }
            else
            {
                result.SetInfo(true, "注册新用户成功！", 200);
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }

        public async Task<ErrData<ISysUserModel>> LoginAsync(QueryData<SysUserQuery> query)
        {
            var result = new ErrData<ISysUserModel>();
            var dt = DateTime.Now;

            query.Criteria.IsDelete = false;
            query.Criteria.Access = true;
            var res = await _service.GetUserAllAsync(query);
            if (res.HasErr)
            {
                result.SetInfo(null, res.ErrMsg, res.ErrCode);
            }
            else
            {
                if (res.Data.Count > 0)
                {
                    result.SetInfo(res.Data.FirstOrDefault(), "登录成功！", 200);
                }
                else
                {
                    result.SetInfo(null, "用户名或密码不正确！", -201);
                }
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }
    }
}
