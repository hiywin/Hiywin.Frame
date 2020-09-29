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
    public class PowerService : IPowerService
    {
        public async Task<DataResult<List<ISysPowerModel>>> GetPowersAllAsync(QueryData<SysPowerQuery> query)
        {
            var lr = new DataResult<List<ISysPowerModel>>();

            StringBuilder builder = new StringBuilder();
            string sqlCondition = string.Empty;

            StringHelper.ParameterAdd(builder, "PowerNo = @PowerNo", query.Criteria.PowerNo);
            StringHelper.ParameterAdd(builder, "ModuleNo = @ModuleNo", query.Criteria.ModuleNo);
            StringHelper.ParameterAdd(builder, "IsDelete = @IsDelete", query.Criteria.IsDelete);

            if (builder.Length > 0)
            {
                sqlCondition = " where " + builder.ToString();
            }
            string sql = @"select Id,PowerNo,ModuleNo,PowerID,Content,Type,Style,FuncName,Icon,Sort,Access,Creator,CreateName,CreateTime,Updator,UpdateName,UpdateTime,IsDelete,IsPlain,IsRound,IsCircle
                from sys_power"
                + sqlCondition;
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlSearchConn))
            {
                try
                {
                    var modelList = await MysqlHelper.QueryListAsync<SysPowerModel>(dbConn, sql, "Sort asc", query.Criteria);
                    lr.Data = modelList.ToList<ISysPowerModel>();
                }
                catch (Exception ex)
                {
                    lr.SetErr(ex, -101);
                    lr.Data = null;
                }
            }

            return lr;
        }

        public async Task<DataResult<List<ISysPowerModel>>> GetPowersPageAsync(QueryData<SysPowerQuery> query)
        {
            var lr = new DataResult<List<ISysPowerModel>>();

            StringBuilder builder = new StringBuilder();
            string sqlCondition = string.Empty;

            StringHelper.ParameterAdd(builder, "PowerNo = @PowerNo", query.Criteria.PowerNo);
            StringHelper.ParameterAdd(builder, "ModuleNo = @ModuleNo", query.Criteria.ModuleNo);
            StringHelper.ParameterAdd(builder, "IsDelete = @IsDelete", query.Criteria.IsDelete);

            if (builder.Length > 0)
            {
                sqlCondition = " where " + builder.ToString();
            }
            string sql = @"select Id,PowerNo,ModuleNo,PowerID,Content,Type,Style,FuncName,Icon,Sort,Access,Creator,CreateName,CreateTime,Updator,UpdateName,UpdateTime,IsDelete,IsPlain,IsRound,IsCircle
                from sys_power"
                + sqlCondition;
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlSearchConn))
            {
                try
                {
                    var modelList = await MysqlHelper.QueryPageAsync<SysPowerModel>(dbConn, "Sort asc", sql, query.PageModel, query.Criteria);
                    lr.Data = modelList.ToList<ISysPowerModel>();
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

        public async Task<DataResult<int>> PowerSaveOrUpdateAsync(QueryData<SysPowerSaveOrUpdateQuery> query)
        {
            var result = new DataResult<int>();

            string sqli = @"insert into sys_power(PowerNo,ModuleNo,PowerID,Content,Type,Style,FuncName,Icon,Sort,Access,Creator,CreateName,CreateTime,IsDelete,IsPlain,IsRound,IsCircle)
                values(@PowerNo,@ModuleNo,@PowerID,@Content,@Type,@Style,@FuncName,@Icon,@Sort,@Access,@Creator,@CreateName,@CreateTime,@IsDelete,@IsPlain,@IsRound,@IsCircle)";
            string sqlu = @"update sys_power set ModuleNo=@ModuleNo,PowerID=@PowerID,Content=@Content,Type=@Type,Style=@Style,FuncName=@FuncName,Icon=@Icon,Sort=@Sort,Access=@Access,
                Updator=@Updator,UpdateName=@UpdateName,UpdateTime=@UpdateTime,IsDelete=@IsDelete,IsPlain=@IsPlain,IsRound=@IsRound,IsCircle=@IsCircle
                where PowerNo=@PowerNo";
            string sqlc = @"select Id from sys_power where PowerNo=@PowerNo";
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlOptConn))
            {
                try
                {
                    // 新增
                    if (string.IsNullOrEmpty(query.Criteria.PowerNo))
                    {
                        query.Criteria.PowerNo = Guid.NewGuid().ToString("N");
                        result.Data = await MysqlHelper.ExecuteSqlAsync(dbConn, sqli, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("新增按钮信息失败！", -101);
                            return result;
                        }
                        else
                        {
                            result.SetErr("新增按钮信息成功！", 200);
                        }
                    }
                    else // 更新
                    {
                        result.Data = await MysqlHelper.QueryCountAsync(dbConn, sqlc, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("按钮不存在或已被删除，请重试！", -101);
                            return result;
                        }
                        result.Data = await MysqlHelper.ExecuteSqlAsync(dbConn, sqlu, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("更新按钮信息失败！", -101);
                            return result;
                        }
                        result.SetErr("更新按钮信息成功！", 200);
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

        public async Task<DataResult<int>> PowerDeleteAsync(QueryData<SysPowerDeleteQuery> query)
        {
            var result = new DataResult<int>();

            string sqld = @"delete from sys_power where PowerNo=@PowerNo";
            string sqlu = @"update sys_power set IsDelete=@IsDelete where PowerNo=@PowerNo";
            string sqlc = @"select Id from sys_power where PowerNo=@PowerNo";
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlOptConn))
            {
                try
                {
                    result.Data = await MysqlHelper.QueryCountAsync(dbConn, sqlc, query.Criteria);
                    if (result.Data <= 0)
                    {
                        result.SetErr("按钮不存在或已被删除，请重试！", -101);
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
