using Hiywin.Common.Data;
using Hiywin.Entities.Frame;
using Hiywin.IFrameService.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiywin.IFrameService
{
    public interface IDictionaryService
    {
        /// <summary>
        /// 获取所有字典列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<ISysDictionaryModel>>> GetDictionaryAllAsync(QueryData<SysDictionaryQuery> query);

        /// <summary>
        /// 新增或修改字典项
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> DictionarySaveOrUpdateAsync(QueryData<SysDictionarySaveOrUpdateQuery> query);

        /// <summary>
        /// 删除字典项
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> DictionaryDeleteAsync(QueryData<SysDictionaryDeleteQuery> query);
    }
}
