using Hiywin.Common;
using Hiywin.Common.Data;
using Hiywin.Common.Helpers;
using Hiywin.Common.IoC;
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
    public class PositionService : IPositionService
    {
        public async Task<DataResult<List<ISysPositionModel>>> GetPositionsAllAsync(QueryData<SysPositionQuery> query)
        {
            var lr = new DataResult<List<ISysPositionModel>>();

            StringBuilder builder = new StringBuilder();
            string sqlCondition = string.Empty;

            StringHelper.ParameterAdd(builder, "PositionNo = @PositionNo", query.Criteria.PositionNo);
            StringHelper.ParameterAdd(builder, "PositionName like concat('%',@PositionName,'%')", query.Criteria.PositionName);
            StringHelper.ParameterAdd(builder, "CompanyNo = @CompanyNo", query.Criteria.CompanyNo);
            StringHelper.ParameterAdd(builder, "IsDelete = @IsDelete", query.Criteria.IsDelete);
            if (builder.Length > 0)
            {
                sqlCondition = " where " + builder.ToString();
            }
            string sql = @"select Id,PositionNo,PositionName,CompanyNo,Descr,Access,Sort,Creator,CreateName,CreateTime,Updator,UpdateName,UpdateTime,IsDelete
                from sys_position"
                + sqlCondition;
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlSearchConn))
            {
                try
                {
                    var modelList = await MysqlHelper.QueryListAsync<SysPositionModel>(dbConn, sql, "Sort asc,PositionName asc", query.Criteria);
                    lr.Data = modelList.ToList<ISysPositionModel>();
                }
                catch (Exception ex)
                {
                    lr.SetErr(ex, -500);
                    lr.Data = null;
                }
            }

            return lr;
        }

        public async Task<DataResult<List<ISysPositionModel>>> GetPositionsPageAsync(QueryData<SysPositionQuery> query)
        {
            var lr = new DataResult<List<ISysPositionModel>>();

            StringBuilder builder = new StringBuilder();
            string sqlCondition = string.Empty;

            StringHelper.ParameterAdd(builder, "PositionNo = @PositionNo", query.Criteria.PositionNo);
            StringHelper.ParameterAdd(builder, "PositionName like concat('%',@PositionName,'%')", query.Criteria.PositionName);
            StringHelper.ParameterAdd(builder, "CompanyNo = @CompanyNo", query.Criteria.CompanyNo);
            StringHelper.ParameterAdd(builder, "IsDelete = @IsDelete", query.Criteria.IsDelete);
            if (builder.Length > 0)
            {
                sqlCondition = " where " + builder.ToString();
            }
            string sql = @"select Id,PositionNo,PositionName,CompanyNo,Descr,Access,Sort,Creator,CreateName,CreateTime,Updator,UpdateName,UpdateTime,IsDelete
                from sys_position"
                + sqlCondition;
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlSearchConn))
            {
                try
                {
                    var modelList = await MysqlHelper.QueryPageAsync<SysPositionModel>(dbConn, "Sort asc,PositionName asc", sql, query.PageModel, query.Criteria);
                    lr.Data = modelList.ToList<ISysPositionModel>();
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

        public async Task<DataResult<int>> PositionSaveOrUpdateAsync(QueryData<SysPositionSaveOrUpdateQuery> query)
        {
            var result = new DataResult<int>();

            string sqli = @"insert into sys_position(PositionNo,PositionName,CompanyNo,Descr,Access,Sort,Creator,CreateName,CreateTime,IsDelete)
                values(@PositionNo,@PositionName,@CompanyNo,@Descr,@Access,@Sort,@Creator,@CreateName,@CreateTime,@IsDelete)";
            string sqlu = @"update sys_position set PositionName=@PositionName,CompanyNo=@CompanyNo,Descr=@Descr,Access=@Access,Sort=@Sort,Updator=@Updator,UpdateName=@UpdateName,UpdateTime=@UpdateTime,IsDelete=@IsDelete
                where PositionNo=@PositionNo";
            string sqlc = @"select Id from sys_position where PositionNo=@PositionNo";
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlOptConn))
            {
                try
                {
                    // 新增
                    if (string.IsNullOrEmpty(query.Criteria.PositionNo))
                    {
                        query.Criteria.PositionNo = Guid.NewGuid().ToString("N");
                        result.Data = await MysqlHelper.ExecuteSqlAsync(dbConn, sqli, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("新增职位信息失败！", -101);
                            return result;
                        }
                        else
                        {
                            result.SetErr("新增职位信息成功！", 200);
                        }
                    }
                    else // 更新
                    {
                        result.Data = await MysqlHelper.QueryCountAsync(dbConn, sqlc, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("职位不存在或已被删除，请重试！", -101);
                            return result;
                        }
                        result.Data = await MysqlHelper.ExecuteSqlAsync(dbConn, sqlu, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("更新职位信息失败！", -101);
                            return result;
                        }
                        result.SetErr("更新职位信息成功！", 200);
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

        public async Task<DataResult<int>> PositionDeleteAsync(QueryData<SysPositionDeleteQuery> query)
        {
            var result = new DataResult<int>();

            string sqld = @"delete from sys_position where PositionNo=@PositionNo";
            string sqlu = @"update sys_position set IsDelete=@IsDelete where PositionNo=@PositionNo";
            string sqlc = @"select Id from sys_position where PositionNo=@PositionNo";
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlOptConn))
            {
                try
                {
                    result.Data = await MysqlHelper.QueryCountAsync(dbConn, sqlc, query.Criteria);
                    if (result.Data <= 0)
                    {
                        result.SetErr("职位不存在或已被删除，请重试！", -101);
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

        public async Task<DataResult<List<ISysPositionRoleModel>>> GetPositionRolesAllAsync(QueryData<SysPositionRoleQuery> query)
        {
            var lr = new DataResult<List<ISysPositionRoleModel>>();

            StringBuilder builder = new StringBuilder();
            string sqlCondition = string.Empty;

            StringHelper.ParameterAdd(builder, "PositionNo = @PositionNo", query.Criteria.PositionNo);
            StringHelper.ParameterAdd(builder, "RoleName like concat('%',@RoleName,'%')", query.Criteria.RoleName);
            if (builder.Length > 0)
            {
                sqlCondition = " where " + builder.ToString();
            }
            string sql = @"select a.Id,PositionNo,a.RoleNo,RoleName,AppNo,a.Creator,a.CreateName,a.CreateTime 
                from sys_positionrole a
                left join sys_role b 
                on a.RoleNo=b.RoleNo"
                + sqlCondition;
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlSearchConn))
            {
                try
                {
                    var modelList = await MysqlHelper.QueryListAsync<SysPositionRoleModel>(dbConn, sql, "AppNo asc,RoleName asc", query.Criteria);
                    lr.Data = modelList.ToList<ISysPositionRoleModel>();
                }
                catch (Exception ex)
                {
                    lr.SetErr(ex, -500);
                    lr.Data = null;
                }
            }

            return lr;
        }

        public async Task<DataResult<int>> PositionRoleSaveOrUpdateAsync(QueryData<SysPositionRoleSaveOrUpdateQuery> query)
        {
            var result = new DataResult<int>();

            string sqli = @"insert into sys_positionrole(PositionNo,RoleNo,Creator,CreateName,CreateTime)
                values(@PositionNo,@RoleNo,@Creator,@CreateName,@CreateTime)";
            string sqld = @"delete from sys_positionrole where PositionNo=@PositionNo and RoleNo=@RoleNo";
            string sqlc = @"select PositionNo,a.RoleNo,RoleName
                from sys_positionrole a left join sys_role b on a.RoleNo=b.RoleNo
                where PositionNo=@PositionNo and AppNo=@AppNo";
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlOptConn))
            {
                IDbTransaction transaction = dbConn.BeginTransaction();
                try
                {
                    if (!string.IsNullOrEmpty(query.Criteria.PositionNo))
                    {
                        var positionRoles = await MysqlHelper.QueryListAsync<SysPositionRoleModel>(dbConn, sqlc, query.Criteria);
                        var prs = positionRoles.ToList<ISysPositionRoleModel>();
                        // 先删除职业所属平台关联的角色
                        foreach (var model in prs)
                        {
                            result.Data = await MysqlHelper.ExecuteSqlAsync(dbConn, sqld, model, transaction);
                            if (result.Data < 0)
                            {
                                result.SetErr(string.Format("更新 {0} 角色失败！", model.RoleName), -101);
                                transaction.Rollback();
                                return result;
                            }
                        }
                        // 新增选中的角色
                        foreach (var model in query.Criteria.LstPositionRole)
                        {
                            result.Data = await MysqlHelper.ExecuteSqlAsync(dbConn, sqli, model, transaction);
                            if (result.Data < 0)
                            {
                                result.SetErr(string.Format("更新 {0} 角色失败！", model.RoleName), -101);
                                transaction.Rollback();
                                return result;
                            }
                        }
                    }
                    else
                    {
                        result.SetErr("未选择职业无法更新角色！", -101);
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    result.SetErr(ex, -500);
                    result.Data = -1;
                }
            }

            return result;
        }

        public async Task<DataResult<int>> PositionRoleDeleteAsync(QueryData<SysPositionRoleDeleteQuery> query)
        {
            var result = new DataResult<int>();

            string sqld = @"delete from sys_positionrole where PositionNo=@PositionNo and RoleNo=@RoleNo";
            string sqlc = @"select Id from sys_positionrole where PositionNo=@PositionNo and RoleNo=@RoleNo";
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlOptConn))
            {
                try
                {
                    result.Data = await MysqlHelper.QueryCountAsync(dbConn, sqlc, query.Criteria);
                    if (result.Data <= 0)
                    {
                        result.SetErr("角色不存在或已被删除，请重试！", -101);
                        return result;
                    }
                    result.Data = await MysqlHelper.ExecuteSqlAsync(dbConn, sqld, query.Criteria);
                    if (result.Data <= 0)
                    {
                        result.SetErr("删除失败！", -101);
                        return result;
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
