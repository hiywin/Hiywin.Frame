using Hiywin.Common.Data;
using Hiywin.Entities.Frame;
using Hiywin.IFrameService.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiywin.IFrameService
{
    public interface IUserService
    {
        Task<DataResult<List<ISysUserModel>>> GetSysUserAllAsync(QueryData<SysUserGetQuery> query);

        Task<DataResult<int>> SysUserSaveOrUpdateAsync(QueryData<SysUserSaveOrUpdateQuery> query);
    }
}
