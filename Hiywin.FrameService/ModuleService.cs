using Dapper;
using Hiywin.Common;
using Hiywin.Common.Data;
using Hiywin.Common.Helpers;
using Hiywin.Entities.Frame;
using Hiywin.IFrameService;
using Hiywin.IFrameService.Structs;
using Hiywin.Models.Frame;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiywin.FrameService
{
    public class ModuleService : IModuleService
    {
        public async Task<DataResult<List<ISysModuleModel>>> GetModulesAllAsync(QueryData<SysModuleQuery> query)
        {
            var lr = new DataResult<List<ISysModuleModel>>();

            #region SQL拼接写法1 - 参数、值直接拼接
            //StringBuilder builder = new StringBuilder();
            //string sqlCondition = string.Empty;

            //StringHelper.StringAdd(builder, " ModuleNo = '{0}' ", query.Criteria.ModuleNo);
            //StringHelper.StringAdd(builder, " ModuleName = '{0}' ", query.Criteria.ModuleName);
            //StringHelper.StringAdd(builder, " IsDelete = {0} ", query.Criteria.IsDelete);

            //if (builder.Length > 0)
            //{
            //    sqlCondition = " where " + builder.ToString();
            //}
            #endregion
            #region SQL拼接写法2 - 使用匿名参数，防注入漏洞
            StringBuilder builder = new StringBuilder();
            string sqlCondition = string.Empty;

            StringHelper.ParameterAdd(builder, "ModuleNo = @ModuleNo", query.Criteria.ModuleNo);
            StringHelper.ParameterAdd(builder, "ModuleName = @ModuleName", query.Criteria.ModuleName);
            StringHelper.ParameterAdd(builder, "IsDelete = @IsDelete", query.Criteria.IsDelete);

            if (builder.Length > 0)
            {
                sqlCondition = " where " + builder.ToString();
            }
            #endregion

            string sql = "select Id,ModuleNo,ModuleName,ParentNo,Icon,Url,Category,Target,IsResource,App,Creator,CreateName,CreateTime,Updator,UpdateName,UpdateTime,IsDelete,Sort " +
                "from sys_module"
                + sqlCondition;
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlSearchConn))
            {
                try
                {
                    var modelList = await MysqlHelper.QueryListAsync<SysModuleModel>(dbConn, sql, "Sort asc", query.Criteria);
                    lr.Data = modelList.ToList<ISysModuleModel>();
                }
                catch (Exception ex)
                {
                    lr.SetErr(ex, -101);
                    lr.Data = null;
                }
            }

            return lr;
        }
    }
}
