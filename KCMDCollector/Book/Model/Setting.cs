using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KCMDCollector.Book
{
    public class Setting
    {
        /// <summary>
        /// 目标站点URL
        /// </summary>
        public string TargetUrl { get; set; }

        /// <summary>
        /// 公共URL
        /// </summary>
        public string PublicUrl { get; set; }

        /// <summary>
        /// API
        /// </summary>
        public string APIKey { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
    }
}
