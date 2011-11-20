/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码，如需要添加方法，请创建同名类，并在该类中添加新的方法。
*生成时间：2011/11/19 19:00:23
*生成者：kuibono
*/
using System;
using System.Text;
using System.Data;
using System.Linq;
using System.Data.Common;
using System.Collections.Generic;
using Voodoo;
using Voodoo.Model;
using Voodoo.Data;
using Voodoo.Data.DbHelper;
using Voodoo.Setting;

namespace Voodoo.DAL
{

    ///<summary>
    ///表User的数据操作类
    ///</summary>
    public partial class UserView
    {

        #region 创建DbHelper
        /// <summary>
        /// 创建DbHelper
        /// </summary>
        /// <returns></returns>
        protected static IDbHelper GetHelper()
        {
            return Voodoo.Setting.DataBase.GetHelper();
        }
        #endregion

        #region DataTable转换为list
        /// <summary>
        /// DataTable转换为list
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        protected static List<User> DataTableToList(DataTable dt)
        {
            List<User> Ms = new List<User>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                User M = new User();
                M.ID = dt.Rows[i]["ID"].ToInt32();
                M.UserName = dt.Rows[i]["UserName"].ToString();
                M.UserPass = dt.Rows[i]["UserPass"].ToString();
                M.Email = dt.Rows[i]["Email"].ToString();
                M.ChineseName = dt.Rows[i]["ChineseName"].ToString();
                M.QQ = dt.Rows[i]["QQ"].ToString();
                M.MSN = dt.Rows[i]["MSN"].ToString();
                M.Tel = dt.Rows[i]["Tel"].ToString();
                M.Mobile = dt.Rows[i]["Mobile"].ToString();
                M.WebSite = dt.Rows[i]["WebSite"].ToString();
                M.Image = dt.Rows[i]["Image"].ToString();
                M.Address = dt.Rows[i]["Address"].ToString();
                M.ZipCode = dt.Rows[i]["ZipCode"].ToString();
                M.Intro = dt.Rows[i]["Intro"].ToString();
                M.RegTime = dt.Rows[i]["RegTime"].ToDateTime();
                M.RegIP = dt.Rows[i]["RegIP"].ToString();
                M.LoginCount = dt.Rows[i]["LoginCount"].ToInt32();
                M.LastLoginTime = dt.Rows[i]["LastLoginTime"].ToDateTime();
                M.LastLoginIP = dt.Rows[i]["LastLoginIP"].ToString();
                M.Cent = dt.Rows[i]["Cent"].ToInt32();
                M.PostCount = dt.Rows[i]["PostCount"].ToInt32();
                M.GTalk = dt.Rows[i]["GTalk"].ToString();
                M.Twitter = dt.Rows[i]["Twitter"].ToString();
                M.Weibo = dt.Rows[i]["weibo"].ToString();
                M.ICQ = dt.Rows[i]["ICQ"].ToString();
                M.Group = dt.Rows[i]["Group"].ToInt32();
                M.Enable = dt.Rows[i]["Enable"].ToBoolean();
                M.StudentNo = dt.Rows[i]["StudentNo"].ToString();
                M.TeachNo = dt.Rows[i]["TeachNo"].ToString();

                Ms.Add(M);
            }
            return Ms;
        }
        #endregion

        #region 将数据插入表
        /// <summary>
        /// 将数据插入表
        /// </summary>
        /// <param name="M">赋值后的实体</param>
        /// <returns></returns>
        public static void Insert(User M)
        {
            IDbHelper Sql = GetHelper();
            StringBuilder sb = new StringBuilder();

            sb.Append("insert into [User]([UserName],[UserPass],[Email],[ChineseName],[QQ],[MSN],[Tel],[Mobile],[WebSite],[Image],[Address],[ZipCode],[Intro],[RegTime],[RegIP],[LoginCount],[LastLoginTime],[LastLoginIP],[Cent],[PostCount],[GTalk],[Twitter],[weibo],[ICQ],[Group],[Enable],[StudentNo],[TeachNo]) values(");
            sb.Append("'" + M.UserName + "'");
            sb.Append(",");
            sb.Append("'" + M.UserPass + "'");
            sb.Append(",");
            sb.Append("'" + M.Email + "'");
            sb.Append(",");
            sb.Append("'" + M.ChineseName + "'");
            sb.Append(",");
            sb.Append("'" + M.QQ + "'");
            sb.Append(",");
            sb.Append("'" + M.MSN + "'");
            sb.Append(",");
            sb.Append("'" + M.Tel + "'");
            sb.Append(",");
            sb.Append("'" + M.Mobile + "'");
            sb.Append(",");
            sb.Append("'" + M.WebSite + "'");
            sb.Append(",");
            sb.Append("'" + M.Image + "'");
            sb.Append(",");
            sb.Append("'" + M.Address + "'");
            sb.Append(",");
            sb.Append("'" + M.ZipCode + "'");
            sb.Append(",");
            sb.Append("'" + M.Intro + "'");
            sb.Append(",");
            sb.Append("'" + M.RegTime + "'");
            sb.Append(",");
            sb.Append("'" + M.RegIP + "'");
            sb.Append(",");
            sb.Append(M.LoginCount.ToS());
            sb.Append(",");
            sb.Append("'" + M.LastLoginTime + "'");
            sb.Append(",");
            sb.Append("'" + M.LastLoginIP + "'");
            sb.Append(",");
            sb.Append(M.Cent.ToS());
            sb.Append(",");
            sb.Append(M.PostCount.ToS());
            sb.Append(",");
            sb.Append("'" + M.GTalk + "'");
            sb.Append(",");
            sb.Append("'" + M.Twitter + "'");
            sb.Append(",");
            sb.Append("'" + M.Weibo + "'");
            sb.Append(",");
            sb.Append("'" + M.ICQ + "'");
            sb.Append(",");
            sb.Append(M.Group.ToS());
            sb.Append(",");
            sb.Append(M.Enable.ToS());
            sb.Append(",");
            sb.Append("'" + M.StudentNo + "'");
            sb.Append(",");
            sb.Append("'" + M.TeachNo + "'");
            sb.Append(")");

            if (DataBase.CmsDbType == DataBase.DbType.SqlServer)
            {
                sb.Append(";select @@Identity");
            }
            if (DataBase.CmsDbType == DataBase.DbType.SQLite)
            {
                sb.Append(";select last_insert_rowid()");
            }
            if (DataBase.CmsDbType == DataBase.DbType.MySql)
            {
                sb.Append(";select LAST_INSERT_ID()");
            }
            if (DataBase.CmsDbType == DataBase.DbType.Access)
            {
                sb.Append(";select max(ID) from User");
            }
            if (DataBase.CmsDbType == DataBase.DbType.Oracle)
            {
                sb.Append(";select LAST_INSERT_ID()");
            }


            M.ID = Sql.ExecuteScalar(CommandType.Text, sb.ToString()).ToInt32();
        }

        #endregion

        #region Update将修改过的实体修改到数据库
        /// <summary>
        /// 将修改过的实体修改到数据库
        /// </summary>
        /// <param name="M">赋值后的实体</param>
        /// <returns></returns>
        public static int Update(User M)
        {
            IDbHelper Sql = GetHelper();
            StringBuilder sb = new StringBuilder();
            sb.Append("update [User] set ");

            sb.Append("[UserName]='" + M.UserName + "'");
            sb.Append(",");
            sb.Append("[UserPass]='" + M.UserPass + "'");
            sb.Append(",");
            sb.Append("[Email]='" + M.Email + "'");
            sb.Append(",");
            sb.Append("[ChineseName]='" + M.ChineseName + "'");
            sb.Append(",");
            sb.Append("[QQ]='" + M.QQ + "'");
            sb.Append(",");
            sb.Append("[MSN]='" + M.MSN + "'");
            sb.Append(",");
            sb.Append("[Tel]='" + M.Tel + "'");
            sb.Append(",");
            sb.Append("[Mobile]='" + M.Mobile + "'");
            sb.Append(",");
            sb.Append("[WebSite]='" + M.WebSite + "'");
            sb.Append(",");
            sb.Append("[Image]='" + M.Image + "'");
            sb.Append(",");
            sb.Append("[Address]='" + M.Address + "'");
            sb.Append(",");
            sb.Append("[ZipCode]='" + M.ZipCode + "'");
            sb.Append(",");
            sb.Append("[Intro]='" + M.Intro + "'");
            sb.Append(",");
            sb.Append("[RegTime]='" + M.RegTime + "'");
            sb.Append(",");
            sb.Append("[RegIP]='" + M.RegIP + "'");
            sb.Append(",");
            sb.Append("[LoginCount]=" + M.LoginCount.ToS());
            sb.Append(",");
            sb.Append("[LastLoginTime]='" + M.LastLoginTime + "'");
            sb.Append(",");
            sb.Append("[LastLoginIP]='" + M.LastLoginIP + "'");
            sb.Append(",");
            sb.Append("[Cent]=" + M.Cent.ToS());
            sb.Append(",");
            sb.Append("[PostCount]=" + M.PostCount.ToS());
            sb.Append(",");
            sb.Append("[GTalk]='" + M.GTalk + "'");
            sb.Append(",");
            sb.Append("[Twitter]='" + M.Twitter + "'");
            sb.Append(",");
            sb.Append("[weibo]='" + M.Weibo + "'");
            sb.Append(",");
            sb.Append("[ICQ]='" + M.ICQ + "'");
            sb.Append(",");
            sb.Append("[Group]=" + M.Group.ToS());
            sb.Append(",");
            sb.Append("[Enable]=" + M.Enable.ToS());
            sb.Append(",");
            sb.Append("[StudentNo]='" + M.StudentNo + "'");
            sb.Append(",");
            sb.Append("[TeachNo]='" + M.TeachNo + "'");

            sb.Append(" where ID='" + M.ID + "'");
            sb.Append("");

            if (DataBase.CmsDbType == DataBase.DbType.SqlServer)
            {
                sb.Append(";select @@ROWCOUNT");
            }
            if (DataBase.CmsDbType == DataBase.DbType.SQLite)
            {
                sb.Append(";select 0");
            }
            if (DataBase.CmsDbType == DataBase.DbType.MySql)
            {
                sb.Append(";SELECT ROW_COUNT()");
            }
            if (DataBase.CmsDbType == DataBase.DbType.Access)
            {
                sb.Append(";select 0");
            }
            if (DataBase.CmsDbType == DataBase.DbType.Oracle)
            {
                sb.Append(";select SQL%ROWCOUNT");
            }


            return Sql.ExecuteScalar(CommandType.Text, sb.ToString()).ToInt32();
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="Ms"></param>
        public static void Update(List<User> Ms)
        {
            foreach (User M in Ms)
            {
                Update(M);
            }
        }
        #endregion

        #region 根据ID取得实体
        /// <summary>
        /// 根据ID取得实体
        /// </summary>
        /// <param name="id">id,即编号主键</param>
        /// <returns></returns>
        public static User GetModelByID(string id)
        {
            IDbHelper Sql = GetHelper();
            User M = new User();
            DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select [ID],[UserName],[UserPass],[Email],[ChineseName],[QQ],[MSN],[Tel],[Mobile],[WebSite],[Image],[Address],[ZipCode],[Intro],[RegTime],[RegIP],[LoginCount],[LastLoginTime],[LastLoginIP],[Cent],[PostCount],[GTalk],[Twitter],[weibo],[ICQ],[Group],[Enable],[StudentNo],[TeachNo] from [User] where ID='" + id.ToString() + "'", true);
            if (!Rs.Read())
            {
                M.ID = 0;
            }
            else
            {
                M.ID = Rs["ID"].ToInt32();
                M.UserName = Rs["UserName"].ToString();
                M.UserPass = Rs["UserPass"].ToString();
                M.Email = Rs["Email"].ToString();
                M.ChineseName = Rs["ChineseName"].ToString();
                M.QQ = Rs["QQ"].ToString();
                M.MSN = Rs["MSN"].ToString();
                M.Tel = Rs["Tel"].ToString();
                M.Mobile = Rs["Mobile"].ToString();
                M.WebSite = Rs["WebSite"].ToString();
                M.Image = Rs["Image"].ToString();
                M.Address = Rs["Address"].ToString();
                M.ZipCode = Rs["ZipCode"].ToString();
                M.Intro = Rs["Intro"].ToString();
                M.RegTime = Rs["RegTime"].ToDateTime();
                M.RegIP = Rs["RegIP"].ToString();
                M.LoginCount = Rs["LoginCount"].ToInt32();
                M.LastLoginTime = Rs["LastLoginTime"].ToDateTime();
                M.LastLoginIP = Rs["LastLoginIP"].ToString();
                M.Cent = Rs["Cent"].ToInt32();
                M.PostCount = Rs["PostCount"].ToInt32();
                M.GTalk = Rs["GTalk"].ToString();
                M.Twitter = Rs["Twitter"].ToString();
                M.Weibo = Rs["weibo"].ToString();
                M.ICQ = Rs["ICQ"].ToString();
                M.Group = Rs["Group"].ToInt32();
                M.Enable = Rs["Enable"].ToBoolean();
                M.StudentNo = Rs["StudentNo"].ToString();
                M.TeachNo = Rs["TeachNo"].ToString();
            }
            Rs.Close();
            Rs = null;
            return M;

        }
        #endregion

        #region 根据条件语句取得第一个实体
        /// <summary>
        /// 根据条件语句取得第一个实体
        /// </summary>
        /// <param name="m_where">条件语句，不包含“where”</param>
        /// <returns></returns>
        public static User Find(string m_where)
        {
            IDbHelper Sql = GetHelper();
            User M = new User();
            DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select [ID],[UserName],[UserPass],[Email],[ChineseName],[QQ],[MSN],[Tel],[Mobile],[WebSite],[Image],[Address],[ZipCode],[Intro],[RegTime],[RegIP],[LoginCount],[LastLoginTime],[LastLoginIP],[Cent],[PostCount],[GTalk],[Twitter],[weibo],[ICQ],[Group],[Enable],[StudentNo],[TeachNo] from [User] where " + m_where, true);
            if (!Rs.Read())
            {
                M.ID = 0;
            }
            else
            {
                M.ID = Rs["ID"].ToInt32();
                M.UserName = Rs["UserName"].ToString();
                M.UserPass = Rs["UserPass"].ToString();
                M.Email = Rs["Email"].ToString();
                M.ChineseName = Rs["ChineseName"].ToString();
                M.QQ = Rs["QQ"].ToString();
                M.MSN = Rs["MSN"].ToString();
                M.Tel = Rs["Tel"].ToString();
                M.Mobile = Rs["Mobile"].ToString();
                M.WebSite = Rs["WebSite"].ToString();
                M.Image = Rs["Image"].ToString();
                M.Address = Rs["Address"].ToString();
                M.ZipCode = Rs["ZipCode"].ToString();
                M.Intro = Rs["Intro"].ToString();
                M.RegTime = Rs["RegTime"].ToDateTime();
                M.RegIP = Rs["RegIP"].ToString();
                M.LoginCount = Rs["LoginCount"].ToInt32();
                M.LastLoginTime = Rs["LastLoginTime"].ToDateTime();
                M.LastLoginIP = Rs["LastLoginIP"].ToString();
                M.Cent = Rs["Cent"].ToInt32();
                M.PostCount = Rs["PostCount"].ToInt32();
                M.GTalk = Rs["GTalk"].ToString();
                M.Twitter = Rs["Twitter"].ToString();
                M.Weibo = Rs["weibo"].ToString();
                M.ICQ = Rs["ICQ"].ToString();
                M.Group = Rs["Group"].ToInt32();
                M.Enable = Rs["Enable"].ToBoolean();
                M.StudentNo = Rs["StudentNo"].ToString();
                M.TeachNo = Rs["TeachNo"].ToString();
            }
            Rs.Close();
            Rs = null;
            return M;
        }
        #endregion

        #region 根据条件语句取得符合条件的数据表
        /// <summary>
        /// 根据条件语句取得符合条件的数据表,慎用！！！！
        /// </summary>
        /// <param name="m_where">条件语句，不包含“where”</param>
        /// <returns></returns>
        public static DataTable getTable(string m_where)
        {
            IDbHelper Sql = GetHelper();
            return Sql.ExecuteDataTable(CommandType.Text, "select [ID],[UserName],[UserPass],[Email],[ChineseName],[QQ],[MSN],[Tel],[Mobile],[WebSite],[Image],[Address],[ZipCode],[Intro],[RegTime],[RegIP],[LoginCount],[LastLoginTime],[LastLoginIP],[Cent],[PostCount],[GTalk],[Twitter],[weibo],[ICQ],[Group],[Enable],[StudentNo],[TeachNo] from [User] where " + m_where);
        }

        /// <summary>
        /// 根据条件语句取得符合条件的数据表,慎用！！！！
        /// </summary>
        /// <returns></returns>
        public static DataTable getTable()
        {
            return getTable("1=1");
        }

        /// <summary>
        /// 根据条件语句取得符合条件的数据表,慎用！！！！
        /// </summary>
        /// <param name="top">前多少条数据</param>
        /// <param name="m_where">条件语句，不包含“where”</param>
        /// <returns></returns>
        public static DataTable getTable(string m_where, int top)
        {
            IDbHelper Sql = GetHelper();
            DataTable dt = Sql.ExecuteDataTable(CommandType.Text, "select top " + top.ToString() + "  [ID],[UserName],[UserPass],[Email],[ChineseName],[QQ],[MSN],[Tel],[Mobile],[WebSite],[Image],[Address],[ZipCode],[Intro],[RegTime],[RegIP],[LoginCount],[LastLoginTime],[LastLoginIP],[Cent],[PostCount],[GTalk],[Twitter],[weibo],[ICQ],[Group],[Enable],[StudentNo],[TeachNo] from [User] where " + m_where);
            return dt;
        }
        #endregion

        #region 根据条件语句取得符合条件的数据集数据集
        /// <summary>
        /// 根据条件语句取得符合条件的数据集数据集,慎用！！！！
        /// </summary>
        /// <param name="m_where">条件语句，不包含“where”</param>
        /// <returns></returns>
        public static DataSet getDs(string m_where)
        {
            return getTable(m_where).DataSet;
        }

        /// <summary>
        /// 根据条件语句取得符合条件的数据集数据集,慎用！！！！
        /// </summary>
        /// <param name="m_where">条件语句，不包含“where”</param>
        /// <returns></returns>
        public static DataSet getDs()
        {
            return getTable().DataSet;
        }
        #endregion

        #region 获取符合条件记录的条数
        /// <summary>
        /// 获取符合条件记录的条数
        /// </summary>
        /// <param name="m_where">条件语句，不包含“where”</param>
        /// <returns></returns>
        public static int Count(string m_where)
        {
            IDbHelper Sql = GetHelper();
            return Convert.ToInt32(Sql.ExecuteScalar(CommandType.Text, "select count(0) from [User] where " + m_where));
        }
        #endregion

        #region 验证符合条件的记录是否存在
        /// <summary>
        /// 验证符合条件的记录是否存在
        /// </summary>
        /// <param name="m_where">条件语句，不包含“where”</param>
        /// <returns></returns>
        public static bool Exist(string m_where)
        {
            bool returnValue = false;
            IDbHelper Sql = GetHelper();
            DbDataReader sd = null;
            sd = Sql.ExecuteReader(CommandType.Text, "select 1 from [User] where " + m_where, true);
            if (sd.Read())
            {
                returnValue = true;
            }
            sd.Close();
            sd.Dispose();
            return returnValue;


        }
        #endregion

        #region List<User>获取符合条件记录的实体列表,慎用！！！！
        /// <summary>
        /// List<User>获取符合条件记录的实体列表,慎用！！！！
        /// </summary>
        /// <param name="m_where">条件语句，不包含“where”</param>
        /// <returns></returns>
        public static List<User> GetModelList(string m_where)
        {
            return DataTableToList(getTable(m_where));
        }
        public static List<User> GetModelList(string m_where, int top)
        {
            return DataTableToList(getTable(m_where, top));
        }
        public static List<User> GetModelList()
        {
            return DataTableToList(getTable());
        }
        #endregion

        #region 删除符合条件记录
        /// <summary>
        /// 删除符合条件记录
        /// </summary>
        /// <param name="m_where">条件语句，不包含“where”</param>
        /// <returns></returns>
        public static bool Del(string m_where)
        {
            IDbHelper Sql = GetHelper();
            try
            {
                Sql.ExecuteNonQuery(CommandType.Text, "delete from [User] where " + m_where);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除符合条件记录
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns></returns>
        public static bool DelByID(int ID)
        {
            return Del("ID=" + ID.ToString());
        }
        #endregion


    }


}
