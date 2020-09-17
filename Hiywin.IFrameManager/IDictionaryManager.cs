using Hiywin.Common.Data;
using Hiywin.Entities.Frame;
using Hiywin.IFrameService.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiywin.IFrameManager
{
    public interface IDictionaryManager
    {
        Task<ListResult<ISysDictionaryModel>> GetDictionaryAllAsync(QueryData<SysDictionaryQuery> query);

        Task<ErrData<bool>> DictionarySaveOrUpdateAsync(QueryData<SysDictionarySaveOrUpdateQuery> query);

        Task<ErrData<bool>> DictionaryDeleteAsync(QueryData<SysDictionaryDeleteQuery> query);
    }
}
