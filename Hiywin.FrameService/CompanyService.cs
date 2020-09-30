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
    public class CompanyService : ICompanyService
    {
        public async Task<DataResult<List<ISysCompanyModel>>> GetCompanysAllAsync(QueryData<SysCompanyQuery> query)
        {
            var lr = new DataResult<List<ISysCompanyModel>>();

            StringBuilder builder = new StringBuilder();
            string sqlCondition = string.Empty;

            StringHelper.ParameterAdd(builder, "CompanyNo = @CompanyNo", query.Criteria.CompanyNo);
            StringHelper.ParameterAdd(builder, "CompanyName like concat('%',@CompanyName,'%')", query.Criteria.CompanyName);
            StringHelper.ParameterAdd(builder, "Address like concat('%',@Address,'%')", query.Criteria.Address);
            StringHelper.ParameterAdd(builder, "Mobile like concat('%',@Mobile,'%')", query.Criteria.Mobile);
            StringHelper.ParameterAdd(builder, "Industry like concat('%',@Industry,'%')", query.Criteria.Industry);
            StringHelper.ParameterAdd(builder, "LegalPerson like concat('%',@LegalPerson,'%')", query.Criteria.LegalPerson);
            StringHelper.ParameterAdd(builder, "IsDelete = @IsDelete", query.Criteria.IsDelete);

            if (builder.Length > 0)
            {
                sqlCondition = " where " + builder.ToString();
            }
            string sql = @"select Id,CompanyNo,CompanyName,Abbreviation,Address,Industry,LegalPerson,Contact,Phone,Mobile,Email,Website,Access,
                Creator,CreateName,CreateTime,Updator,UpdateName,UpdateTime,IsDelete
                from sys_company"
                + sqlCondition;
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlSearchConn))
            {
                try
                {
                    var modelList = await MysqlHelper.QueryListAsync<SysCompanyModel>(dbConn, sql, "Id desc", query.Criteria);
                    lr.Data = modelList.ToList<ISysCompanyModel>();
                }
                catch (Exception ex)
                {
                    lr.SetErr(ex, -500);
                    lr.Data = null;
                }
            }

            return lr;
        }

        public async Task<DataResult<List<ISysCompanyModel>>> GetCompanysPageAsync(QueryData<SysCompanyQuery> query)
        {
            var lr = new DataResult<List<ISysCompanyModel>>();

            StringBuilder builder = new StringBuilder();
            string sqlCondition = string.Empty;

            StringHelper.ParameterAdd(builder, "CompanyNo = @CompanyNo", query.Criteria.CompanyNo);
            StringHelper.ParameterAdd(builder, "CompanyName like concat('%',@CompanyName,'%')", query.Criteria.CompanyName);
            StringHelper.ParameterAdd(builder, "Address like concat('%',@Address,'%')", query.Criteria.Address);
            StringHelper.ParameterAdd(builder, "Mobile like concat('%',@Mobile,'%')", query.Criteria.Mobile);
            StringHelper.ParameterAdd(builder, "Industry like concat('%',@Industry,'%')", query.Criteria.Industry);
            StringHelper.ParameterAdd(builder, "LegalPerson like concat('%',@LegalPerson,'%')", query.Criteria.LegalPerson);
            StringHelper.ParameterAdd(builder, "IsDelete = @IsDelete", query.Criteria.IsDelete);

            if (builder.Length > 0)
            {
                sqlCondition = " where " + builder.ToString();
            }
            string sql = @"select Id,CompanyNo,CompanyName,Abbreviation,Address,Industry,LegalPerson,Contact,Phone,Mobile,Email,Website,Access,
                Creator,CreateName,CreateTime,Updator,UpdateName,UpdateTime,IsDelete
                from sys_company"
                + sqlCondition;
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlSearchConn))
            {
                try
                {
                    var modelList = await MysqlHelper.QueryPageAsync<SysCompanyModel>(dbConn, "Id desc", sql, query.PageModel, query.Criteria);
                    lr.Data = modelList.ToList<ISysCompanyModel>();
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

        public async Task<DataResult<int>> CompanySaveOrUpdateAsync(QueryData<SysCompanySaveOrUpdateQuery> query)
        {
            var result = new DataResult<int>();

            string sqli = @"insert into sys_company(CompanyNo,CompanyName,Abbreviation,Address,Industry,LegalPerson,Contact,Phone,Mobile,Email,Website,Access,Creator,CreateName,CreateTime,IsDelete)
                values(@CompanyNo,@CompanyName,@Abbreviation,@Address,@Industry,@LegalPerson,@Contact,@Phone,@Mobile,@Email,@Website,@Access,@Creator,@CreateName,@CreateTime,@IsDelete)";
            string sqlu = @"update sys_company set CompanyName=@CompanyName,Abbreviation=@Abbreviation,Address=@Address,Industry=@Industry,LegalPerson=@LegalPerson,Contact=@Contact,Phone=@Phone,
                Mobile=@Mobile,Email=@Email,Website=@Website,Access=@Access,Updator=@Updator,UpdateName=@UpdateName,UpdateTime=@UpdateTime,IsDelete=@IsDelete
                where CompanyNo=@CompanyNo";
            string sqlc = @"select Id from sys_company where CompanyNo=@CompanyNo";
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlOptConn))
            {
                try
                {
                    // 新增
                    if (string.IsNullOrEmpty(query.Criteria.CompanyNo))
                    {
                        query.Criteria.CompanyNo = Guid.NewGuid().ToString("N");
                        result.Data = await MysqlHelper.ExecuteSqlAsync(dbConn, sqli, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("新增公司信息失败！", -101);
                            return result;
                        }
                        else
                        {
                            result.SetErr("新增公司信息成功！", 200);
                        }
                    }
                    else // 更新
                    {
                        result.Data = await MysqlHelper.QueryCountAsync(dbConn, sqlc, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("公司不存在或已被删除，请重试！", -101);
                            return result;
                        }
                        result.Data = await MysqlHelper.ExecuteSqlAsync(dbConn, sqlu, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("更新公司信息失败！", -101);
                            return result;
                        }
                        result.SetErr("更新公司信息成功！", 200);
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

        public async Task<DataResult<int>> CompanyDeleteAsync(QueryData<SysCompanyDeleteQuery> query)
        {
            var result = new DataResult<int>();

            string sqld = @"delete from sys_company where CompanyNo=@CompanyNo";
            string sqlu = @"update sys_company set IsDelete=@IsDelete where CompanyNo=@CompanyNo";
            string sqlc = @"select Id from sys_company where CompanyNo=@CompanyNo";
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlOptConn))
            {
                try
                {
                    result.Data = await MysqlHelper.QueryCountAsync(dbConn, sqlc, query.Criteria);
                    if (result.Data <= 0)
                    {
                        result.SetErr("公司不存在或已被删除，请重试！", -101);
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
