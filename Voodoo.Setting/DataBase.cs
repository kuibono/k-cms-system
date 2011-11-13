using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Voodoo.Data;
using Voodoo.Data.DbHelper;

namespace Voodoo.Setting
{
    public class DataBase
    {
        public static string ConnStr
        {
            get
            {
                return Voodoo.Config.Info.GetAppSetting("ConnStr");
            }
        }

        /// <summary>
        /// 获取数据访问器
        /// </summary>
        /// <returns></returns>
        public static IDbHelper GetHelper()
        {
            return new SqlHelper(ConnStr);
        }

        #region 数据库类型
        /// <summary>
        /// 本系统的数据库类型
        /// </summary>
        public static DbType CmsDbType
        {
            get
            {
                return DbType.SqlServer;
            }
        }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public enum DbType
        {
            SqlServer,
            MySql,
            Access,
            SQLite,
            Oracle
        }
        #endregion

    }


}
