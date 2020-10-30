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

    }
}
