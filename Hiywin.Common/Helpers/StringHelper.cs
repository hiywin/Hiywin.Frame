using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Common.Helpers
{
    public class StringHelper
    {
        public static void StringAdd(StringBuilder builder, string formater, object param, string condition=" and ")
        {
            switch (param?.GetType()?.Name)
            {
                case "String":
                    if (!string.IsNullOrEmpty(param.ToString()))
                    {
                        if (builder.Length > 0) builder.Append(condition);
                        builder.Append(string.Format(formater, param));
                    }
                    break;
                case "Int32":
                    if ((int)param > 0)
                    {
                        if (builder.Length > 0) builder.Append(condition);
                        builder.Append(string.Format(formater, param));
                    }
                    break;
                case "Boolean":
                    if (builder.Length > 0) builder.Append(condition);
                    builder.Append(string.Format(formater, param));
                    break;
                default:
                    break;
            }
        }
    }
}
