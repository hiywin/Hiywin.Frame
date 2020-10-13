using Hiywin.Common.Data;
using Hiywin.Entities.Frame;
using Hiywin.IFrameService.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiywin.IFrameManager
{
    public interface IAccountManager
    {
        Task<ErrData<bool>> RegisterAsync(QueryData<SysUserSaveOrUpdateQuery> query);

        Task<ErrData<ISysUserModel>> LoginAsync(QueryData<SysUserQuery> query);
    }
}
