using Hiywin.Common.Data;
using Hiywin.Entities.Frame;
using Hiywin.IFrameService.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiywin.IFrameManager
{
    public interface ICompanyManager
    {
        /// <summary>
        /// 获取全部公司列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<ISysCompanyModel>> GetCompanyAllAsync(QueryData<SysCompanyQuery> query);

        /// <summary>
        /// 分页获取公司列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<ISysCompanyModel>> GetCompanyPageAsync(QueryData<SysCompanyQuery> query);

        /// <summary>
        /// 新增或更新公司信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> CompanySaveOrUpdateAsync(QueryData<SysCompanySaveOrUpdateQuery> query);

        /// <summary>
        /// 删除公司信息（IsDelete=true为软删除，IsDelete=false为物理删除）
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> CompanyDeleteAsync(QueryData<SysCompanyDeleteQuery> query);
    }
}
