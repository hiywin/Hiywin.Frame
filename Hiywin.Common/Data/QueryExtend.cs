using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Common.Data
{
    public class QueryExtend
    {
        /// <summary>
        /// 数据库连接串
        /// </summary>
        public string ConnString { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserNo { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string StaffNo { get; set; }

        /// <summary>
        /// AD账号
        /// </summary>
        public string AdAccount { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 请求接口的平台
        /// </summary>
        public string AppNo { get; set; }
    }
}
