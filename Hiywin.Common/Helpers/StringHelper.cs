using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Common.Helpers
{
    public class StringHelper
    {
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
