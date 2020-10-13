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
    public class UserService : IUserService
    {
        public async Task<DataResult<List<ISysUserModel>>> GetUserAllAsync(QueryData<SysUserQuery> query)
        {
            var result = new DataResult<List<ISysUserModel>>();

            StringBuilder builder = new StringBuilder();
            string sqlCondition = string.Empty;

            StringHelper.ParameterAdd(builder, "UserNo = @UserNo", query.Criteria.UserNo);
            StringHelper.ParameterAdd(builder, "UserName = @UserName", query.Criteria.UserName);
            StringHelper.ParameterAdd(builder, "StaffNo = @StaffNo", query.Criteria.StaffNo);
            StringHelper.ParameterAdd(builder, "AdAccount = @AdAccount", query.Criteria.AdAccount);
            StringHelper.ParameterAdd(builder, "Mobile = @Mobile", query.Criteria.Mobile);
            StringHelper.ParameterAdd(builder, "Email = @Email", query.Criteria.Email);
            StringHelper.ParameterAdd(builder, "Pwd = @Pwd", query.Criteria.Pwd);
            StringHelper.ParameterAdd(builder, "CompanyNo = @CompanyNo", query.Criteria.CompanyNo);
            StringHelper.ParameterAdd(builder, "Access = @Access", query.Criteria.Access);
            StringHelper.ParameterAdd(builder, "IsDelete = @IsDelete", query.Criteria.IsDelete);

            if (builder.Length > 0)
            {
                sqlCondition = " where " + builder.ToString();
            }
            string sql = @"select Id,UserNo,UserName,Pwd,RealName,Mobile,Email,AdAccount,StaffNo,CompanyNo,Icon,AppNo,
                RegisterTime,ApprovedBy,ApprovedTime,Descr,RejectedBy,RejectedTime,RejectedReason,
                IsAdmin,Access,Creator,CreateName,CreateTime,Updator,UpdateName,UpdateTime,IsDelete                
                from sys_user"
                + sqlCondition;
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlSearchConn))
            {
                try
                {
                    var modelList = await MysqlHelper.QueryListAsync<SysUserModel>(dbConn, sql, "Id desc", query.Criteria);
                    result.Data = modelList.ToList<ISysUserModel>();
                }
                catch (Exception ex)
                {
                    result.SetErr(ex, -500);
                    result.Data = null;
                }
            }

            return result;
        }

        public async Task<DataResult<List<ISysUserModel>>> GetUserPageAsync(QueryData<SysUserQuery> query)
        {
            var lr = new DataResult<List<ISysUserModel>>();

            StringBuilder builder = new StringBuilder();
            string sqlCondition = string.Empty;

            StringHelper.ParameterAdd(builder, "UserNo = @UserNo", query.Criteria.UserNo);
            StringHelper.ParameterAdd(builder, "UserName like concat('%',@UserName,'%')", query.Criteria.UserName);
            StringHelper.ParameterAdd(builder, "StaffNo like concat('%',@StaffNo,'%')", query.Criteria.StaffNo);
            StringHelper.ParameterAdd(builder, "AdAccount like concat('%',@AdAccount,'%')", query.Criteria.AdAccount);
            StringHelper.ParameterAdd(builder, "Mobile like concat('%',@Mobile,'%')", query.Criteria.Mobile);
            StringHelper.ParameterAdd(builder, "Email like concat('%',@Email,'%')", query.Criteria.Email);
            StringHelper.ParameterAdd(builder, "RealName like concat('%',@RealName,'%')", query.Criteria.RealName);
            StringHelper.ParameterAdd(builder, "CompanyNo = @CompanyNo", query.Criteria.CompanyNo);
            StringHelper.ParameterAdd(builder, "IsDelete = @IsDelete", query.Criteria.IsDelete);
            if (builder.Length > 0)
            {
                sqlCondition = " where " + builder.ToString();
            }
            string sql = @"select Id,UserNo,UserName,Pwd,RealName,Mobile,Email,AdAccount,StaffNo,CompanyNo,Icon,AppNo,
                RegisterTime,ApprovedBy,ApprovedTime,Descr,RejectedBy,RejectedTime,RejectedReason,
                IsAdmin,Access,Creator,CreateName,CreateTime,Updator,UpdateName,UpdateTime,IsDelete                
                from sys_user"
               + sqlCondition;
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlSearchConn))
            {
                try
                {
                    var modelList = await MysqlHelper.QueryPageAsync<SysUserModel>(dbConn, "Id desc", sql, query.PageModel, query.Criteria);
                    lr.Data = modelList.ToList<ISysUserModel>();
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

        public async Task<DataResult<int>> UserSaveOrUpdateAsync(QueryData<SysUserSaveOrUpdateQuery> query)
        {
            var result = new DataResult<int>();

            string sqli = @"insert into sys_user(UserNo,UserName,Pwd,RealName,Mobile,Email,AdAccount,StaffNo,CompanyNo,Icon,AppNo,RegisterTime,
                ApprovedBy,ApprovedTime,Descr,Access,Creator,CreateName,CreateTime)
                values(@UserNo,@UserName,@Pwd,@RealName,@Mobile,@Email,@AdAccount,@StaffNo,@CompanyNo,@Icon,@AppNo,@RegisterTime,
                @ApprovedBy,@ApprovedTime,@Descr,@Access,@Creator,@CreateName,@CreateTime)";
            string sqlu = @"update sys_user set UserName=@UserName,Pwd=@Pwd,RealName=@RealName,Mobile=@Mobile,Email=@Email,AdAccount=@AdAccount,StaffNo=@StaffNo,
                CompanyNo=@CompanyNo,Icon=@Icon,ApprovedBy=@ApprovedBy,ApprovedTime=@ApprovedTime,Descr=@Descr,
                RejectedBy=@RejectedBy,RejectedTime=@RejectedTime,RejectedReason=@RejectedReason,IsAdmin=@IsAdmin,
                Access=@Access,Updator=@Updator,UpdateName=@UpdateName,UpdateTime=@UpdateTime,IsDelete=@IsDelete                
                where UserNo=@UserNo";
            string sqlcu = @"select Id from sys_user where UserName=@UserName and IsDelete=false";
            string sqlcm = @"select Id from sys_user where Mobile=@Mobile and IsDelete=false";
            string sqlce = @"select Id from sys_user where Email=@Email and IsDelete=false";
            string sqlcc = @"select Id from sys_user where StaffNo=@StaffNo and CompanyNo=@CompanyNo and IsDelete=false";
            string sqlca = @"select Id from sys_user where AdAccount=@AdAccount and CompanyNo=@CompanyNo and IsDelete=false";
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlOptConn))
            {
                try
                {
                    #region 注册用户验证
                    result.Data = await MysqlHelper.QueryCountAsync(dbConn, sqlcu, query.Criteria);
                    if (result.Data > 0)
                    {
                        result.SetErr("用户名已存在，请重试！", -102);
                        return result;
                    }
                    if (!string.IsNullOrEmpty(query.Criteria.Mobile))
                    {
                        result.Data = await MysqlHelper.QueryCountAsync(dbConn, sqlcm, query.Criteria);
                        if (result.Data > 0)
                        {
                            result.SetErr("手机号已存在，请重试！", -102);
                            return result;
                        }
                    }
                    if (!string.IsNullOrEmpty(query.Criteria.Email))
                    {
                        result.Data = await MysqlHelper.QueryCountAsync(dbConn, sqlce, query.Criteria);
                        if (result.Data > 0)
                        {
                            result.SetErr("邮箱已存在，请重试！", -102);
                            return result;
                        }
                    }
                    if (!string.IsNullOrEmpty(query.Criteria.StaffNo))
                    {
                        result.Data = await MysqlHelper.QueryCountAsync(dbConn, sqlcc, query.Criteria);
                        if (result.Data > 0)
                        {
                            result.SetErr("工号已存在，请重试！", -102);
                            return result;
                        }
                    }
                    if (!string.IsNullOrEmpty(query.Criteria.AdAccount))
                    {
                        result.Data = await MysqlHelper.QueryCountAsync(dbConn, sqlca, query.Criteria);
                        if (result.Data > 0)
                        {
                            result.SetErr("AD账号已存在，请重试！", -102);
                            return result;
                        }
                    }
                    #endregion

                    // 新增
                    if (string.IsNullOrEmpty(query.Criteria.UserNo))
                    {
                        query.Criteria.UserNo = Guid.NewGuid().ToString("N");
                        result.Data = await MysqlHelper.ExecuteSqlAsync(dbConn, sqli, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("新增用户信息失败！", -101);
                            return result;
                        }
                        else
                        {
                            result.SetErr("新增用户信息成功！", 200);
                        }
                    }
                    else // 更新
                    {
                        result.Data = await MysqlHelper.ExecuteSqlAsync(dbConn, sqlu, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("更新用户信息失败！", -101);
                            return result;
                        }
                        else
                        {
                            result.SetErr("更新用户信息成功！", 200);
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

        public async Task<DataResult<int>> UserDeleteAsync(QueryData<SysUserDeleteQuery> query)
        {
            var result = new DataResult<int>();

            string sqld = @"delete from sys_user where UserNo=@UserNo";
            string sqlu = @"update sys_user set IsDelete=@IsDelete where UserNo=@UserNo";
            string sqlc = @"select Id from sys_user where UserNo=@UserNo";
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
