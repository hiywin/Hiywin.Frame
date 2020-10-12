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
    public class AppService : IAppService
    {
        public async Task<DataResult<List<ISysAppModel>>> GetAppsAllAsync(QueryData<SysAppQuery> query)
        {
            var lr = new DataResult<List<ISysAppModel>>();

            StringBuilder builder = new StringBuilder();
            string sqlCondition = string.Empty;

            StringHelper.ParameterAdd(builder, "AppNo = @AppNo", query.Criteria.AppNo);
            StringHelper.ParameterAdd(builder, "AppName like concat('%',@AppName,'%')", query.Criteria.AppName);
            StringHelper.ParameterAdd(builder, "Leader like concat('%',@Leader,'%')", query.Criteria.Leader);
            StringHelper.ParameterAdd(builder, "IsDelete = @IsDelete", query.Criteria.IsDelete);
            if (builder.Length > 0)
            {
                sqlCondition = " where " + builder.ToString();
            }
            string sql = @"select Id,AppNo,AppName,Leader,Deploy,Remark,Creator,CreateName,CreateTime,Updator,UpdateName,UpdateTime,IsDelete
                from sys_app"
                + sqlCondition;
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlSearchConn))
            {
                try
                {
                    var modelList = await MysqlHelper.QueryListAsync<SysAppModel>(dbConn, sql, "Id desc", query.Criteria);
                    lr.Data = modelList.ToList<ISysAppModel>();
                }
                catch (Exception ex)
                {
                    lr.SetErr(ex, -500);
                    lr.Data = null;
                }
            }

            return lr;
        }

        public async Task<DataResult<List<ISysAppModel>>> GetAppsPageAsync(QueryData<SysAppQuery> query)
        {
            var lr = new DataResult<List<ISysAppModel>>();

            StringBuilder builder = new StringBuilder();
            string sqlCondition = string.Empty;

            StringHelper.ParameterAdd(builder, "AppNo = @AppNo", query.Criteria.AppNo);
            StringHelper.ParameterAdd(builder, "AppName like concat('%',@AppName,'%')", query.Criteria.AppName);
            StringHelper.ParameterAdd(builder, "Leader like concat('%',@Leader,'%')", query.Criteria.Leader);
            StringHelper.ParameterAdd(builder, "IsDelete = @IsDelete", query.Criteria.IsDelete);
            if (builder.Length > 0)
            {
                sqlCondition = " where " + builder.ToString();
            }
            string sql = @"select Id,AppNo,AppName,Leader,Deploy,Remark,Creator,CreateName,CreateTime,Updator,UpdateName,UpdateTime,IsDelete
                from sys_app"
                + sqlCondition;
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlSearchConn))
            {
                try
                {
                    var modelList = await MysqlHelper.QueryPageAsync<SysAppModel>(dbConn, "Id desc", sql, query.PageModel, query.Criteria);
                    lr.Data = modelList.ToList<ISysAppModel>();
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

        public async Task<DataResult<int>> AppSaveOrUpdateAsync(QueryData<SysAppSaveOrUpdateQuery> query)
        {
            var result = new DataResult<int>();

            string sqli = @"insert into sys_app(AppNo,AppName,Leader,Deploy,Remark,Creator,CreateName,CreateTime,IsDelete)
                values(@AppNo,@AppName,@Leader,@Deploy,@Remark,@Creator,@CreateName,@CreateTime,@IsDelete)";
            string sqlu = @"update sys_app set AppName=@AppName,Leader=@Leader,Deploy=@Deploy,Remark=@Remark,Updator=@Updator,UpdateName=@UpdateName,UpdateTime=@UpdateTime,IsDelete=@IsDelete
                where AppNo=@AppNo";
            string sqlc = @"select Id from sys_app where AppNo=@AppNo";
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlOptConn))
            {
                try
                {
                    // 新增
                    if (string.IsNullOrEmpty(query.Criteria.AppNo))
                    {
                        query.Criteria.AppNo = Guid.NewGuid().ToString("N");
                        result.Data = await MysqlHelper.ExecuteSqlAsync(dbConn, sqli, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("新增平台信息失败！", -101);
                            return result;
                        }
                        else
                        {
                            result.SetErr("新增平台信息成功！", 200);
                        }
                    }
                    else // 更新
                    {
                        result.Data = await MysqlHelper.QueryCountAsync(dbConn, sqlc, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("平台不存在或已被删除，请重试！", -101);
                            return result;
                        }
                        result.Data = await MysqlHelper.ExecuteSqlAsync(dbConn, sqlu, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("更新平台信息失败！", -101);
                            return result;
                        }
                        result.SetErr("更新平台信息成功！", 200);
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

        public async Task<DataResult<int>> AppDeleteAsync(QueryData<SysAppDeleteQuery> query)
        {
            var result = new DataResult<int>();

            string sqld = @"delete from sys_app where AppNo=@AppNo";
            string sqlu = @"update sys_app set IsDelete=@IsDelete where AppNo=@AppNo";
            string sqlc = @"select Id from sys_app where AppNo=@AppNo";
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlOptConn))
            {
                try
                {
                    result.Data = await MysqlHelper.QueryCountAsync(dbConn, sqlc, query.Criteria);
                    if (result.Data <= 0)
                    {
                        result.SetErr("平台不存在或已被删除，请重试！", -101);
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
