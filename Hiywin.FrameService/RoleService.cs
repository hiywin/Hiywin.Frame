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
    public class RoleService : IRoleService
    {
        public async Task<DataResult<List<ISysRoleModel>>> GetRolesAllAsync(QueryData<SysRoleQuery> query)
        {
            var lr = new DataResult<List<ISysRoleModel>>();

            StringBuilder builder = new StringBuilder();
            string sqlCondition = string.Empty;

            StringHelper.ParameterAdd(builder, "RoleNo = @RoleNo", query.Criteria.RoleNo);
            StringHelper.ParameterAdd(builder, "AppNo = @AppNo", query.Criteria.AppNo);
            StringHelper.ParameterAdd(builder, "RoleName like concat('%',@RoleName,'%')", query.Criteria.RoleName);
            StringHelper.ParameterAdd(builder, "IsDelete = @IsDelete", query.Criteria.IsDelete);
            if (builder.Length > 0)
            {
                sqlCondition = " where " + builder.ToString();
            }
            string sql = "select Id,RoleNo,RoleName,Descr,AppNo,Access,Creator,CreateName,CreateTime,Updator,UpdateName,UpdateTime,IsDelete" +
                " from sys_role"
                + sqlCondition;
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlSearchConn))
            {
                try
                {
                    var modelList = await MysqlHelper.QueryListAsync<SysRoleModel>(dbConn, sql, "RoleName asc", query.Criteria);
                    lr.Data = modelList.ToList<ISysRoleModel>();
                }
                catch (Exception ex)
                {
                    lr.SetErr(ex, -500);
                    lr.Data = null;
                }
            }

            return lr;
        }

        public async Task<DataResult<List<ISysRoleModel>>> GetRolesPageAsync(QueryData<SysRoleQuery> query)
        {
            var lr = new DataResult<List<ISysRoleModel>>();

            StringBuilder builder = new StringBuilder();
            string sqlCondition = string.Empty;

            StringHelper.ParameterAdd(builder, "RoleNo = @RoleNo", query.Criteria.RoleNo);
            StringHelper.ParameterAdd(builder, "AppNo = @AppNo", query.Criteria.AppNo);
            StringHelper.ParameterAdd(builder, "RoleName like concat('%',@RoleName,'%')", query.Criteria.RoleName);
            StringHelper.ParameterAdd(builder, "IsDelete = @IsDelete", query.Criteria.IsDelete);
            if (builder.Length > 0)
            {
                sqlCondition = " where " + builder.ToString();
            }
            string sql = "select Id,RoleNo,RoleName,Descr,AppNo,Access,Creator,CreateName,CreateTime,Updator,UpdateName,UpdateTime,IsDelete" +
                " from sys_role"
                + sqlCondition;
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlSearchConn))
            {
                try
                {
                    var modelList = await MysqlHelper.QueryPageAsync<SysRoleModel>(dbConn, "RoleName asc", sql, query.PageModel, query.Criteria);
                    lr.Data = modelList.ToList<ISysRoleModel>();
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

        public async Task<DataResult<int>> RoleSaveOrUpdateAsync(QueryData<SysRoleSaveOrUpdateQuery> query)
        {
            var result = new DataResult<int>();

            string sqli = @"insert into sys_role(RoleNo,RoleName,Descr,AppNo,Access,Creator,CreateName,CreateTime,IsDelete)
                values(@RoleNo,@RoleName,@Descr,@AppNo,@Access,@Creator,@CreateName,@CreateTime,@IsDelete)";
            string sqlu = @"update sys_role set RoleName=@RoleName,Descr=@Descr,AppNo=@AppNo,Access=@Access,Updator=@Updator,UpdateName=@UpdateName,UpdateTime=@UpdateTime,IsDelete=@IsDelete
                where RoleNo=@RoleNo";
            string sqlc = @"select Id from sys_role where RoleNo=@RoleNo";
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlOptConn))
            {
                try
                {
                    // 新增
                    if (string.IsNullOrEmpty(query.Criteria.RoleNo))
                    {
                        query.Criteria.RoleNo = Guid.NewGuid().ToString("N");
                        result.Data = await MysqlHelper.ExecuteSqlAsync(dbConn, sqli, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("新增角色失败！", -101);
                            return result;
                        }
                        else
                        {
                            result.SetErr("新增角色成功！", 200);
                        }
                    }
                    else // 更新
                    {
                        result.Data = await MysqlHelper.QueryCountAsync(dbConn, sqlc, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("角色不存在或已被删除，请重试！", -101);
                            return result;
                        }
                        result.Data = await MysqlHelper.ExecuteSqlAsync(dbConn, sqlu, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("更新角色失败！", -101);
                            return result;
                        }
                        result.SetErr("更新角色成功！", 200);
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

        public async Task<DataResult<int>> RoleDeleteAsync(QueryData<SysRoleDeleteQuery> query)
        {
            var result = new DataResult<int>();

            string sqld = @"delete from sys_role where RoleNo=@RoleNo";
            string sqlu = @"update sys_role set IsDelete=@IsDelete where RoleNo=@RoleNo";
            string sqlc = @"select Id from sys_role where RoleNo=@RoleNo";
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

        public async Task<DataResult<List<ISysRoleModuleModel>>> GetRoleModulesPageAsync(QueryData<SysRoleModuleQuery> query)
        {
            var lr = new DataResult<List<ISysRoleModuleModel>>();

            StringBuilder builder = new StringBuilder();
            string sqlCondition = string.Empty;

            StringHelper.ParameterAdd(builder, "a.RoleNo = @RoleNo", query.Criteria.RoleNo);
            StringHelper.ParameterAdd(builder, "ModuleName like concat('%',@ModuleName,'%')", query.Criteria.ModuleName);
            if (builder.Length > 0)
            {
                sqlCondition = " where " + builder.ToString();
            }
            string sql = @"select a.Id,a.RoleNo,a.ModuleNo,ModuleName,a.Creator,a.CreateName,a.CreateTime
                from sys_rolemodule a
                left join sys_module b
                on a.ModuleNo=b.ModuleNo"
                + sqlCondition;
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlSearchConn))
            {
                try
                {
                    var modelList = await MysqlHelper.QueryPageAsync<SysRoleModuleModel>(dbConn, "ModuleName asc", sql, query.PageModel, query.Criteria);
                    lr.Data = modelList.ToList<ISysRoleModuleModel>();
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

        public async Task<DataResult<List<ISysRolePowerModel>>> GetRolePowersAllAsync(QueryData<SysRolePowerQuery> query)
        {
            var lr = new DataResult<List<ISysRolePowerModel>>();

            StringBuilder builder = new StringBuilder();
            string sqlCondition = string.Empty;

            StringHelper.ParameterAdd(builder, "RoleNo = @RoleNo", query.Criteria.RoleNo);
            StringHelper.ParameterAdd(builder, "a.PowerNo = @PowerNo", query.Criteria.PowerNo);
            StringHelper.ParameterAdd(builder, "ModuleNo = @ModuleNo", query.Criteria.ModuleNo);
            if (builder.Length > 0)
            {
                sqlCondition = " where " + builder.ToString();
            }
            string sql = @"select a.Id,RoleNo,a.PowerNo,ModuleNo,Content,b.Creator,b.CreateName,b.CreateTime
                from sys_power a
                left join sys_rolepower b
                on a.PowerNo=b.PowerNo"
                + sqlCondition;
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlSearchConn))
            {
                try
                {
                    var modelList = await MysqlHelper.QueryListAsync<SysRolePowerModel>(dbConn, sql, "Id asc", query.Criteria);
                    lr.Data = modelList.ToList<ISysRolePowerModel>();
                }
                catch (Exception ex)
                {
                    lr.SetErr(ex, -500);
                    lr.Data = null;
                }
            }

            return lr;
        }
    }
}
