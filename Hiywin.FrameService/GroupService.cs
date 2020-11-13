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
    public class GroupService : IGroupService
    {
        public async Task<DataResult<List<ISysGroupModel>>> GetGroupsAllAsync(QueryData<SysGroupQuery> query)
        {
            var lr = new DataResult<List<ISysGroupModel>>();

            StringBuilder builder = new StringBuilder();
            string sqlCondition = string.Empty;

            StringHelper.ParameterAdd(builder, "GroupNo = @GroupNo", query.Criteria.GroupNo);
            StringHelper.ParameterAdd(builder, "GroupName like concat('%',@GroupName,'%')", query.Criteria.GroupName);
            StringHelper.ParameterAdd(builder, "Code = @Code", query.Criteria.Code);
            StringHelper.ParameterAdd(builder, "ParentNo = @ParentNo", query.Criteria.ParentNo);
            StringHelper.ParameterAdd(builder, "AppNo = @AppNo", query.Criteria.AppNo);
            StringHelper.ParameterAdd(builder, "IsDelete = @IsDelete", query.Criteria.IsDelete);
            if (builder.Length > 0)
            {
                sqlCondition = " where " + builder.ToString();
            }
            string sql = @"select Id,GroupNo,GroupName,Code,Descr,ParentNo,AppNo,Access,Creator,CreateName,CreateTime,Updator,UpdateName,UpdateTime,IsDelete
                from sys_group"
                + sqlCondition;
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlSearchConn))
            {
                try
                {
                    var modelList = await MysqlHelper.QueryListAsync<SysGroupModel>(dbConn, sql, "GroupName asc", query.Criteria);
                    lr.Data = modelList.ToList<ISysGroupModel>();
                }
                catch (Exception ex)
                {
                    lr.SetErr(ex, -500);
                    lr.Data = null;
                }
            }

            return lr;
        }

        public async Task<DataResult<List<ISysGroupModel>>> GetGroupsPageAsync(QueryData<SysGroupQuery> query)
        {
            var lr = new DataResult<List<ISysGroupModel>>();

            StringBuilder builder = new StringBuilder();
            string sqlCondition = string.Empty;

            StringHelper.ParameterAdd(builder, "GroupNo = @GroupNo", query.Criteria.GroupNo);
            StringHelper.ParameterAdd(builder, "GroupName like concat('%',@GroupName,'%')", query.Criteria.GroupName);
            StringHelper.ParameterAdd(builder, "Code = @Code", query.Criteria.Code);
            StringHelper.ParameterAdd(builder, "ParentNo = @ParentNo", query.Criteria.ParentNo);
            StringHelper.ParameterAdd(builder, "AppNo = @AppNo", query.Criteria.AppNo);
            StringHelper.ParameterAdd(builder, "IsDelete = @IsDelete", query.Criteria.IsDelete);
            if (builder.Length > 0)
            {
                sqlCondition = " where " + builder.ToString();
            }
            string sql = @"select Id,GroupNo,GroupName,Code,Descr,ParentNo,AppNo,Access,Creator,CreateName,CreateTime,Updator,UpdateName,UpdateTime,IsDelete
                from sys_group"
                + sqlCondition;
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlSearchConn))
            {
                try
                {
                    var modelList = await MysqlHelper.QueryPageAsync<SysGroupModel>(dbConn, "GroupName asc", sql, query.PageModel, query.Criteria);
                    lr.Data = modelList.ToList<ISysGroupModel>();
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

        public async Task<DataResult<int>> GroupSaveOrUpdateAsync(QueryData<SysGroupSaveOrUpdateQuery> query)
        {
            var result = new DataResult<int>();

            string sqli = @"insert into sys_group(GroupNo,GroupName,Code,Descr,ParentNo,AppNo,Access,Creator,CreateName,CreateTime,IsDelete)
                values(@GroupNo,@GroupName,@Code,@Descr,@ParentNo,@AppNo,@Access,@Creator,@CreateName,@CreateTime,@IsDelete)";
            string sqlu = @"update sys_group set GroupName=@GroupName,Code=@Code,Descr=@Descr,ParentNo=@ParentNo,AppNo=@AppNo,Access=@Access,
                Updator=@Updator,UpdateName=@UpdateName,UpdateTime=@UpdateTime,IsDelete=@IsDelete
                where GroupNo=@GroupNo";
            string sqlc = @"select Id from sys_group where GroupNo=@GroupNo";
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlOptConn))
            {
                try
                {
                    // 新增
                    if (string.IsNullOrEmpty(query.Criteria.GroupNo))
                    {
                        query.Criteria.GroupNo = Guid.NewGuid().ToString("N");
                        result.Data = await MysqlHelper.ExecuteSqlAsync(dbConn, sqli, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("新增组织信息失败！", -101);
                            return result;
                        }
                        else
                        {
                            result.SetErr("新增组织信息成功！", 200);
                        }
                    }
                    else // 更新
                    {
                        result.Data = await MysqlHelper.QueryCountAsync(dbConn, sqlc, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("组织不存在或已被删除，请重试！", -101);
                            return result;
                        }
                        result.Data = await MysqlHelper.ExecuteSqlAsync(dbConn, sqlu, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("更新组织信息失败！", -101);
                            return result;
                        }
                        result.SetErr("更新组织信息成功！", 200);
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

        public async Task<DataResult<int>> GroupDeleteAsync(QueryData<SysGroupDeleteQuery> query)
        {
            var result = new DataResult<int>();

            string sqld = @"delete from sys_group where GroupNo=@GroupNo";
            string sqlu = @"update sys_group set IsDelete=@IsDelete where GroupNo=@GroupNo";
            string sqlc = @"select Id from sys_group where GroupNo=@GroupNo";
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlOptConn))
            {
                try
                {
                    result.Data = await MysqlHelper.QueryCountAsync(dbConn, sqlc, query.Criteria);
                    if (result.Data <= 0)
                    {
                        result.SetErr("组织不存在或已被删除，请重试！", -101);
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

        public async Task<DataResult<List<ISysGroupRoleModel>>> GetGroupRolesAllAsync(QueryData<SysGroupRoleQuery> query)
        {
            var lr = new DataResult<List<ISysGroupRoleModel>>();

            StringBuilder builder = new StringBuilder();
            string sqlCondition = string.Empty;

            StringHelper.ParameterAdd(builder, "GroupNo = @GroupNo", query.Criteria.GroupNo);
            StringHelper.ParameterAdd(builder, "RoleName like concat('%',@RoleName,'%')", query.Criteria.RoleName);
            if (builder.Length > 0)
            {
                sqlCondition = " where " + builder.ToString();
            }
            string sql = @"select a.Id,GroupNo,a.RoleNo,RoleName,a.Creator,a.CreateName,a.CreateTime
                from sys_grouprole a
                left join sys_role b
                on a.RoleNo=b.RoleNo"
                + sqlCondition;
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlSearchConn))
            {
                try
                {
                    var modelList = await MysqlHelper.QueryListAsync<SysGroupRoleModel>(dbConn, sql, "RoleName asc", query.Criteria);
                    lr.Data = modelList.ToList<ISysGroupRoleModel>();
                }
                catch (Exception ex)
                {
                    lr.SetErr(ex, -500);
                    lr.Data = null;
                }
            }

            return lr;
        }

        public async Task<DataResult<int>> GroupRoleSaveOrUpdateAsync(QueryData<SysGroupRoleSaveOrUpdateQuery> query)
        {
            var result = new DataResult<int>();

            string sqli = @"insert into sys_grouprole(GroupNo,RoleNo,Creator,CreateName,CreateTime)
                values(@GroupNo,@RoleNo,@Creator,@CreateName,@CreateTime)";
            string sqld = @"delete from sys_grouprole where GroupNo=@GroupNo and RoleNo=@RoleNo";
            string sqlc = @"select GroupNo,a.RoleNo,RoleName
                from sys_grouprole a left join sys_role b on a.RoleNo=b.RoleNo
                where GroupNo=@GroupNo and AppNo=@AppNo";
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlOptConn))
            {
                IDbTransaction transaction = dbConn.BeginTransaction();
                try
                {
                    if (!string.IsNullOrEmpty(query.Criteria.GroupNo) || !string.IsNullOrEmpty(query.Criteria.AppNo))
                    {
                        var groupRoles = await MysqlHelper.QueryListAsync<SysGroupRoleModel>(dbConn, sqlc, query.Criteria);
                        var grs = groupRoles.ToList<ISysGroupRoleModel>();
                        // 先删除职位所属平台关联的角色
                        foreach (var model in grs)
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
                        foreach (var model in query.Criteria.LstGroupRole)
                        {
                            result.Data = await MysqlHelper.ExecuteSqlAsync(dbConn, sqli, model, transaction);
                            if (result.Data <= 0)
                            {
                                result.SetErr(string.Format("更新 {0} 角色失败！", model.RoleName), -101);
                                transaction.Rollback();
                                return result;
                            }
                        }
                    }
                    else
                    {
                        result.SetErr("未选择组织或平台无法更新角色！", -101);
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

        public async Task<DataResult<int>> GroupRoleDeleteAsync(QueryData<SysGroupRoleDeleteQuery> query)
        {
            var result = new DataResult<int>();

            string sqld = @"delete from sys_grouprole where GroupNo=@GroupNo and RoleNo=@RoleNo";
            string sqlc = @"select Id from sys_grouprole where GroupNo=@GroupNo and RoleNo=@RoleNo";
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

        public async Task<DataResult<List<ISysGroupUserModel>>> GetGroupUsersAllAsync(QueryData<SysGroupUserQuery> query)
        {
            var lr = new DataResult<List<ISysGroupUserModel>>();

            StringBuilder builder = new StringBuilder();
            string sqlCondition = string.Empty;

            StringHelper.ParameterAdd(builder, "GroupNo = @GroupNo", query.Criteria.GroupNo);
            StringHelper.ParameterAdd(builder, "UserName like concat('%',@UserName,'%')", query.Criteria.UserName);
            if (builder.Length > 0)
            {
                sqlCondition = " where " + builder.ToString();
            }
            string sql = @"select a.Id,GroupNo,a.UserNo,UserName,GroupMaster,GroupManager,a.Creator,a.CreateName,a.CreateTime
                from sys_groupuser a
                left join sys_user b
                on a.UserNo=b.UserNo"
                + sqlCondition;
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlSearchConn))
            {
                try
                {
                    var modelList = await MysqlHelper.QueryListAsync<SysGroupUserModel>(dbConn, sql, "UserName asc", query.Criteria);
                    lr.Data = modelList.ToList<ISysGroupUserModel>();
                }
                catch (Exception ex)
                {
                    lr.SetErr(ex, -500);
                    lr.Data = null;
                }
            }

            return lr;
        }

        public async Task<DataResult<int>> GroupUserSaveOrUpdateAsync(QueryData<SysGroupUserSaveOrUpdateQuery> query)
        {
            var result = new DataResult<int>();

            string sqli = @"insert into sys_groupuser(GroupNo,UserNo,GroupMaster,GroupManager,Creator,CreateName,CreateTime)
                values(@GroupNo,@UserNo,@GroupMaster,@GroupManager,@Creator,@CreateName,@CreateTime)";
            string sqld = @"delete from sys_groupuser where GroupNo=@GroupNo";
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlOptConn))
            {
                IDbTransaction transaction = dbConn.BeginTransaction();
                try
                {
                    if (!string.IsNullOrEmpty(query.Criteria.GroupNo))
                    {
                        result.Data = await MysqlHelper.ExecuteSqlAsync(dbConn, sqld, query.Criteria, transaction);
                        if (result.Data < 0)
                        {
                            result.SetErr("删除所属用户失败！");
                            transaction.Rollback();
                            return result;
                        }
                        // 新增选中的角色
                        foreach (var model in query.Criteria.LstGroupUser)
                        {
                            result.Data = await MysqlHelper.ExecuteSqlAsync(dbConn, sqli, model, transaction);
                            if (result.Data < 0)
                            {
                                result.SetErr(string.Format("更新 {0} 用户失败！", model.UserName), -101);
                                transaction.Rollback();
                                return result;
                            }
                        }
                    }
                    else
                    {
                        result.SetErr("未选择组织无法更新用户！", -101);
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

        public async Task<DataResult<int>> GroupUserDeleteAsync(QueryData<SysGroupUserDeleteQuery> query)
        {
            var result = new DataResult<int>();

            string sqld = @"delete from sys_groupuser where GroupNo=@GroupNo and UserNo=@UserNo";
            string sqlc = @"select Id from sys_groupuser where GroupNo=@GroupNo and UserNo=@UserNo";
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlOptConn))
            {
                try
                {
                    result.Data = await MysqlHelper.QueryCountAsync(dbConn, sqlc, query.Criteria);
                    if (result.Data <= 0)
                    {
                        result.SetErr("用户不存在或已被删除，请重试！", -101);
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
