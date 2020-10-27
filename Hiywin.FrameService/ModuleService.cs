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

            StringBuilder builder = new StringBuilder();
            string sqlCondition = string.Empty;

            StringHelper.ParameterAdd(builder, "ModuleNo = @ModuleNo", query.Criteria.ModuleNo);
            StringHelper.ParameterAdd(builder, "ModuleName like concat('%',@ModuleName,'%')", query.Criteria.ModuleName);
            StringHelper.ParameterAdd(builder, "IsDelete = @IsDelete", query.Criteria.IsDelete);
            StringHelper.ParameterAdd(builder, "AppNo = @AppNo", query.Criteria.AppNo);
            StringHelper.StringAdd(builder, "ParentNo = @ParentNo", query.Criteria.ParentNo);

            if (builder.Length > 0)
            {
                sqlCondition = " where " + builder.ToString();
            }
            string sql = @"select Id,ModuleNo,ModuleName,ParentNo,Icon,Url,Category,Target,AppNo,Creator,CreateName,CreateTime,Updator,UpdateName,UpdateTime,IsDelete,Sort,RouterName,
                (select count(Id) from sys_module where ParentNo=a.ModuleNo) ChildrenCount
                from sys_module a"
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
                    lr.SetErr(ex, -500);
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
            StringHelper.ParameterAdd(builder, "ModuleName like concat('%',@ModuleName,'%')", query.Criteria.ModuleName);
            StringHelper.ParameterAdd(builder, "IsDelete = @IsDelete", query.Criteria.IsDelete);
            StringHelper.ParameterAdd(builder, "AppNo = @AppNo", query.Criteria.AppNo);
            StringHelper.StringAdd(builder, "ParentNo = @ParentNo", query.Criteria.ParentNo);

            if (builder.Length > 0)
            {
                sqlCondition = " where " + builder.ToString();
            }
            string sql = @"select Id,ModuleNo,ModuleName,ParentNo,Icon,Url,Category,Target,AppNo,Creator,CreateName,CreateTime,Updator,UpdateName,UpdateTime,IsDelete,Sort,RouterName,
                (select count(Id) from sys_module where ParentNo=a.ModuleNo) ChildrenCount
                from sys_module a"
                + sqlCondition;
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlSearchConn))
            {
                try
                {
                    var modelList = await MysqlHelper.QueryPageAsync<SysModuleModel>(dbConn, "AppNo asc,Sort asc", sql, query.PageModel, query.Criteria);
                    lr.Data = modelList.ToList<ISysModuleModel>();
                    lr.PageInfo = query.PageModel;
                }
                catch (Exception ex)
                {
                    lr.SetErr(ex, -500);
                    lr.Data = null;
                }
            }

            return lr;
        }

        public async Task<DataResult<int>> ModuleSaveOrUpdateAsync(QueryData<SysModuleSaveOrUpdateQuery> query)
        {
            var result = new DataResult<int>();

            string sqli = @"insert into sys_module(ModuleNo,ModuleName,ParentNo,Icon,Url,Category,Target,AppNo,Creator,CreateName,CreateTime,Sort,IsDelete,RouterName)
                values(@ModuleNo,@ModuleName,@ParentNo,@Icon,@Url,@Category,@Target,@AppNo,@Creator,@CreateName,@CreateTime,@Sort,@IsDelete,@RouterName)";
            string sqlu = @"update sys_module set ModuleName=@ModuleName,ParentNo=@ParentNo,Icon=@Icon,Url=@Url,Category=@Category,Target=@Target,
                AppNo=@AppNo,Updator=@Updator,UpdateName=@UpdateName,UpdateTime=@UpdateTime,Sort=@Sort,IsDelete=@IsDelete,RouterName=@RouterName
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
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlOptConn))
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
