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
    public class DictionaryService : IDictionaryService
    {
        public async Task<DataResult<List<ISysDictionaryModel>>> GetDictionarysAllAsync(QueryData<SysDictionaryQuery> query)
        {
            var lr = new DataResult<List<ISysDictionaryModel>>();

            StringBuilder builder = new StringBuilder();
            string sqlCondition = string.Empty;

            StringHelper.ParameterAdd(builder, "Type = @Type", query.Criteria.Type);
            StringHelper.ParameterAdd(builder, "TypeName like concat('%',@TypeName,'%')", query.Criteria.TypeName);
            StringHelper.ParameterAdd(builder, "IsDelete = @IsDelete", query.Criteria.IsDelete);
            StringHelper.StringAdd(builder, "ParentNo = @ParentNo", query.Criteria.ParentNo);
            StringHelper.StringAdd(builder, "App = @App", query.Criteria.App);
            if (builder.Length > 0)
            {
                sqlCondition = " where " + builder.ToString();
            }
            string sql = "select Id,DictionaryNo,Type,TypeName,Content,Code,ParentNo,Descr,App,Sort,Creator,CreateName,CreateTime,Updator,UpdateName,UpdateTime,IsDelete" +
                " from sys_dictionary"
                + sqlCondition;
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlSearchConn))
            {
                try
                {
                    var modelList = await MysqlHelper.QueryListAsync<SysDictionaryModel>(dbConn, sql, "Id asc", query.Criteria);
                    lr.Data = modelList.ToList<ISysDictionaryModel>();
                }
                catch (Exception ex)
                {
                    lr.SetErr(ex, -500);
                    lr.Data = null;
                }
            }

            return lr;
        }

        public async Task<DataResult<List<ISysDictionaryModel>>> GetDictionarysPageAsync(QueryData<SysDictionaryQuery> query)
        {
            var lr = new DataResult<List<ISysDictionaryModel>>();

            StringBuilder builder = new StringBuilder();
            string sqlCondition = string.Empty;

            StringHelper.ParameterAdd(builder, "App = @App", query.Criteria.App);
            StringHelper.ParameterAdd(builder, "Type = @Type", query.Criteria.Type);
            StringHelper.ParameterAdd(builder, "TypeName like concat('%',@TypeName,'%')", query.Criteria.TypeName);
            StringHelper.ParameterAdd(builder, "Content like concat('%',@Content,'%')", query.Criteria.Content);
            StringHelper.ParameterAdd(builder, "IsDelete = @IsDelete", query.Criteria.IsDelete);
            StringHelper.StringAdd(builder, "ParentNo = @ParentNo", query.Criteria.ParentNo);
            StringHelper.StringAdd(builder, "App = @App", query.Criteria.App);
            if (builder.Length > 0)
            {
                sqlCondition = " where " + builder.ToString();
            }
            string sql = "select Id,DictionaryNo,Type,TypeName,Content,Code,ParentNo,Descr,App,Sort,Creator,CreateName,CreateTime,Updator,UpdateName,UpdateTime,IsDelete" +
                " from sys_dictionary"
                + sqlCondition;
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlSearchConn))
            {
                try
                {
                    var modelList = await MysqlHelper.QueryPageAsync<SysDictionaryModel>(dbConn, "Id desc", sql, query.PageModel, query.Criteria);
                    lr.Data = modelList.ToList<ISysDictionaryModel>();
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

        public async Task<DataResult<int>> DictionarySaveOrUpdateAsync(QueryData<SysDictionarySaveOrUpdateQuery> query)
        {
            var result = new DataResult<int>();

            string sqli = @"insert into sys_dictionary(DictionaryNo,Type,TypeName,Content,Code,ParentNo,Descr,App,Sort,Creator,CreateName,CreateTime,IsDelete)
                values(@DictionaryNo,@Type,@TypeName,@Content,@Code,@ParentNo,@Descr,@App,@Sort,@Creator,@CreateName,@CreateTime,@IsDelete)";
            string sqlu = @"update sys_dictionary set Type=@Type,TypeName=@TypeName,Content=@Content,Code=@Code,ParentNo=@ParentNo,Descr=@Descr,App=@App,Sort=@Sort,
                Updator=@Updator,UpdateName=@UpdateName,UpdateTime=@UpdateTime,IsDelete=@IsDelete
                where DictionaryNo=@DictionaryNo";
            string sqlc = @"select Id from sys_dictionary where DictionaryNo=@DictionaryNo";
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlOptConn))
            {
                try
                {
                    // 新增
                    if (string.IsNullOrEmpty(query.Criteria.DictionaryNo))
                    {
                        query.Criteria.DictionaryNo = Guid.NewGuid().ToString("N");
                        result.Data = await MysqlHelper.ExecuteSqlAsync(dbConn, sqli, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("新增字典信息失败！", -101);
                            return result;
                        }
                        else
                        {
                            result.SetErr("新增字典信息成功！", 200);
                        }
                    }
                    else // 更新
                    {
                        result.Data = await MysqlHelper.QueryCountAsync(dbConn, sqlc, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("字典项不存在或已被删除，请重试！", -101);
                            return result;
                        }
                        result.Data = await MysqlHelper.ExecuteSqlAsync(dbConn, sqlu, query.Criteria);
                        if (result.Data <= 0)
                        {
                            result.SetErr("更新字典信息失败！", -101);
                            return result;
                        }
                        result.SetErr("更新字典信息成功！", 200);
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

        public async Task<DataResult<int>> DictionaryDeleteAsync(QueryData<SysDictionaryDeleteQuery> query)
        {
            var result = new DataResult<int>();

            string sqld = @"delete from sys_dictionary where DictionaryNo=@DictionaryNo";
            string sqlu = @"update sys_dictionary set IsDelete=@IsDelete where DictionaryNo=@DictionaryNo";
            string sqlc = @"select Id from sys_dictionary where DictionaryNo=@DictionaryNo";
            using (IDbConnection dbConn = MysqlHelper.OpenMysqlConnection(ConfigOptions.MysqlOptConn))
            {
                try
                {
                    result.Data = await MysqlHelper.QueryCountAsync(dbConn, sqlc, query.Criteria);
                    if (result.Data <= 0)
                    {
                        result.SetErr("字典项不存在或已被删除，请重试！", -101);
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
