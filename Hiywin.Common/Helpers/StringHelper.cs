using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Common.Helpers
{
    public class StringHelper
    {
        public static void StringAdd(StringBuilder builder, string formater, object condition)
        {
            switch (condition?.GetType()?.Name)
            {
                case "String":
                        builder.Append(string.Format(formater, condition));
                    break;
                case "Int32":
                    if ((int)condition>0)
                    {
                        builder.Append(string.Format(formater, condition));
                    }
                    break;
                case "Boolean":
                    builder.Append(string.Format(formater, condition));
                    break;
                default:
                    break;
            }
        }
    }
}
