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
        public async Task<DataResult<List<ISysUserModel>>> GetSysUserAllAsync(QueryData<SysUserGetQuery> query)
        {
            var result = new DataResult<List<ISysUserModel>>();

            StringBuilder builder = new StringBuilder();
            string sqlCondition = string.Empty;

            StringHelper.ParameterAdd(builder, "UserName = @UserName", query.Criteria.UserName);
            StringHelper.ParameterAdd(builder, "Pwd = @Pwd", query.Criteria.Pwd);
            StringHelper.ParameterAdd(builder, "IsDelete = @IsDelete", query.Criteria.IsDelete);

            if (builder.Length > 0)
            {
                sqlCondition = " where " + builder.ToString();
            }
            string sql = @"select Id,UserNo,UserName,Pwd,Mobile,Email,Icon,App,RegisterTime,ApprovedBy,ApprovedTime,Descr,RejectedBy,
                RejectedTime,RejectedReason,Access,Creator,CreateName,CreateTime,Updator,UpdateName,UpdateTime,IsDelete 
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

        public async Task<DataResult<int>> SysUserSaveOrUpdateAsync(QueryData<SysUserSaveOrUpdateQuery> query)
        {
            var result = new DataResult<int>();

            string sqli = @"insert into sys_user(UserNo,UserName,Pwd,Mobile,Email,Icon,App,RegisterTime,Access,Creator,CreateName,CreateTime)
                values(@UserNo,@UserName,@Pwd,@Mobile,@Email,@Icon,@App,@RegisterTime,@Access,@Creator,@CreateName,@CreateTime)";
            string sqlu = @"update sys_user set UserName=@UserName,Pwd=@Pwd,Mobile=@Mobile,Mobile=@Mobile,Icon=@Icon,App=@App,RegisterTime=@RegisterTime,
                Access=@Access,Creator=@Creator,CreateName=@CreateName,CreateTime=@CreateTime
                where UserNo=@UserNo";
            string sqlc = @"select Id from sys_user where UserName=@UserName";
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlOptConn))
            {
                try
                {
                    result.Data = await MysqlHelper.QueryCountAsync(dbConn, sqlc, query.Criteria);
                    if (result.Data > 0)
                    {
                        result.SetErr("用户名已存在，请重试！", -102);
                        return result;
                    }
                    // 新增
                    if (string.IsNullOrEmpty(query.Criteria.UserNo))
                    {
                        query.Criteria.UserNo = Guid.NewGuid().ToString("N");
                        result.Data = await MysqlHelper.ExecuteSqlAsync(dbConn, sqli, query.Criteria);
                        if (result.Data < 0)
                        {
                            result.SetErr("新增用户信息失败！", -101);
                            return result;
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
