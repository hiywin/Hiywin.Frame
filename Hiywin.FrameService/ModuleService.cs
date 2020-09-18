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
            StringHelper.ParameterAdd(builder, "ParentNo = @ParentNo", query.Criteria.IsParentNo);
            StringHelper.ParameterAdd(builder, "App = @App", query.Criteria.App);

            if (builder.Length > 0)
            {
                sqlCondition = " where " + builder.ToString();
            }
            #endregion

            string sql = "select Id,ModuleNo,ModuleName,ParentNo,Icon,Url,Category,Target,IsResource,App,Creator,CreateName,CreateTime,Updator,UpdateName,UpdateTime,IsDelete,Sort,RouterName " +
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

        public async Task<DataResult<List<ISysModuleModel>>> GetModulesPageAsync(QueryData<SysModuleQuery> query)
        {
            var lr = new DataResult<List<ISysModuleModel>>();

            StringBuilder builder = new StringBuilder();
            string sqlCondition = string.Empty;

            StringHelper.ParameterAdd(builder, "ModuleNo = @ModuleNo", query.Criteria.ModuleNo);
            StringHelper.ParameterAdd(builder, "ModuleName = @ModuleName", query.Criteria.ModuleName);
            StringHelper.ParameterAdd(builder, "IsDelete = @IsDelete", query.Criteria.IsDelete);
            StringHelper.ParameterAdd(builder, "ParentNo = @ParentNo", query.Criteria.IsParentNo);
            StringHelper.ParameterAdd(builder, "App = @App", query.Criteria.App);

            if (builder.Length > 0)
            {
                sqlCondition = " where " + builder.ToString();
            }
            string sql = "select Id,ModuleNo,ModuleName,ParentNo,Icon,Url,Category,Target,IsResource,App,Creator,CreateName,CreateTime,Updator,UpdateName,UpdateTime,IsDelete,Sort,RouterName " +
                "from sys_module"
                + sqlCondition;
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlSearchConn))
            {
                try
                {
                    var modelList = await MysqlHelper.QueryPageAsync<SysModuleModel>(dbConn, "App asc,Sort asc", sql, query.PageModel, query.Criteria);
                    lr.Data = modelList.ToList<ISysModuleModel>();
                    lr.PageInfo = query.PageModel;
                }
                catch (Exception ex)
                {
                    lr.SetErr(ex, -101);
                    lr.Data = null;
                }
            }

            return lr;
        }

        public async Task<DataResult<int>> ModuleSaveOrUpdateAsync(QueryData<SysModuleSaveOrUpdateQuery> query)
        {
            var result = new DataResult<int>();

            string sqli = @"insert into sys_module(ModuleNo,ModuleName,ParentNo,Icon,Url,Category,Target,IsResource,App,Creator,CreateName,CreateTime,Sort,IsDelete,RouterName)
                values(@ModuleNo,@ModuleName,@ParentNo,@Icon,@Url,@Category,@Target,@IsResource,@App,@Creator,@CreateName,@CreateTime,@Sort,@IsDelete,@RouterName)";
            string sqlu = @"update sys_module set ModuleName=@ModuleName,ParentNo=@ParentNo,Icon=@Icon,Url=@Url,Category=@Category,Target=@Target,IsResource=@IsResource,
                App=@App,Updator=@Updator,UpdateName=@UpdateName,UpdateTime=@UpdateTime,Sort=@Sort,IsDelete=@IsDelete,RouterName=@RouterName
                where ModuleNo=@ModuleNo";
            string sqlc = @"select Id from sys_module where ModuleNo=@ModuleNo";
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlOptConn))
            {
                try
                {
                    // 新增
                    if (string.IsNullOrEmpty(query.Criteria.ModuleNo))
                    {
                        query.Criteria.ModuleNo = Guid.NewGuid().ToString("N");
                        result.Data = await MysqlHelper.ExecuteSqlAsync(dbConn, sqli, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("新增模块信息失败！", -101);
                            return result;
                        }
                        else
                        {
                            result.SetErr("新增模块信息成功！", 200);
                        }
                    }
                    else // 更新
                    {
                        result.Data = await MysqlHelper.QueryCountAsync(dbConn, sqlc, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("模块不存在或已被删除，请重试！", -101);
                            return result;
                        }
                        result.Data = await MysqlHelper.ExecuteSqlAsync(dbConn, sqlu, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("更新模块信息失败！", -101);
                            return result;
                        }
                        result.SetErr("更新模块信息成功！", 200);
                    }
                }
                catch (Exception ex)
                {
                    result.SetErr(ex, -500);
                    result.Data = -1;
                }
            }

            return result;
        }

        public async Task<DataResult<int>> ModuleDeleteAsync(QueryData<SysModuleDeleteQuery> query)
        {
            var result = new DataResult<int>();

            string sqld = @"delete from sys_module where ModuleNo=@ModuleNo";
            string sqlu = @"update sys_module set IsDelete=@IsDelete where ModuleNo=@ModuleNo";
            string sqlc = @"select Id from sys_module where ModuleNo=@ModuleNo";
            using (IDbConnection dbConn=MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlOptConn))
            {
                try
                {
                    result.Data = await MysqlHelper.QueryCountAsync(dbConn, sqlc, query.Criteria);
                    if (result.Data <= 0)
                    {
                        result.SetErr("模块不存在或已被删除，请重试！", -101);
                        return result;
                    }
                    if (query.Criteria.IsDelete)
                    {
                        result.Data = await MysqlHelper.ExecuteSqlAsync(dbConn, sqlu, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("删除失败！", -101);
                            return result;
                        }
                    }
                    else
                    {
                        result.Data = await MysqlHelper.ExecuteSqlAsync(dbConn, sqld, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("删除失败！", -101);
                            return result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.SetErr(ex, -500);
                    result.Data = -1;
                }
            }

            return result;
        }
    }
}
