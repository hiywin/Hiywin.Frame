using Hiywin.Common.Data;
using Hiywin.Entities.Frame;
using Hiywin.IFrameService.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiywin.IFrameService
{
    public interface ICompanyService
    {
        /// <summary>
        /// 获取所有公司列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<ISysCompanyModel>>> GetCompanysAllAsync(QueryData<SysCompanyQuery> query);

        /// <summary>
        /// 分页获取公司列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<ISysCompanyModel>>> GetCompanysPageAsync(QueryData<SysCompanyQuery> query);

        /// <summary>
        /// 新增或更新公司信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> CompanySaveOrUpdateAsync(QueryData<SysCompanySaveOrUpdateQuery> query);

        /// <summary>
        /// 删除公司
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> CompanyDeleteAsync(QueryData<SysCompanyDeleteQuery> query);
    }
}
