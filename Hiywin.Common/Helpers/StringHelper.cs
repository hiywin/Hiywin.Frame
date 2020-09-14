using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Common.Helpers
{
    public class StringHelper
    {
        /// <summary>
        /// SQL查询参数拼接，将参数、值直接拼接
        /// 示例：StringHelper.StringAdd(builder, " ModuleNo = '{0}' ", query.Criteria.ModuleNo);
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="formater"></param>
        /// <param name="paramValue"></param>
        /// <param name="condition"></param>
        public static void StringAdd(StringBuilder builder, string formater, object paramValue, string condition = " and ")
        {
            switch (paramValue?.GetType()?.Name)
            {
                case "String":
                    if (!string.IsNullOrEmpty(paramValue.ToString()))
                    {
                        if (builder.Length > 0) builder.AppendLine(condition);
                        builder.AppendLine(string.Format(formater, paramValue));
                    }
                    break;
                case "Int32":
                    if ((int)paramValue > 0)
                    {
                        if (builder.Length > 0) builder.AppendLine(condition);
                        builder.AppendLine(string.Format(formater, paramValue));
                    }
                    break;
                case "Boolean":
                    if (builder.Length > 0) builder.AppendLine(condition);
                    builder.AppendLine(string.Format(formater, paramValue));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// SQL查询参数拼接，采用匿名函数拼接，防注入漏洞
        /// 示例：StringHelper.ParameterAdd(builder, "ModuleNo = @ModuleNo", query.Criteria.ModuleNo);
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="formater"></param>
        /// <param name="paramValue"></param>
        /// <param name="condition"></param>
        public static void ParameterAdd(StringBuilder builder, string formater, object paramValue, string condition = " and ")
        {
            if (!string.IsNullOrEmpty(formater))
            {
                switch (paramValue?.GetType()?.Name)
                {
                    case "String":
                        if (!string.IsNullOrEmpty(paramValue.ToString()))
                        {
                            if (builder.Length > 0) builder.AppendLine(condition);
                            builder.AppendLine(formater);
                        }
                        break;
                    case "Int32":
                        if ((int)paramValue > 0)
                        {
                            if (builder.Length > 0) builder.AppendLine(condition);
                            builder.AppendLine(formater);
                        }
                        break;
                    case "Boolean":
                        if (builder.Length > 0) builder.AppendLine(condition);
                        builder.AppendLine(formater);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
