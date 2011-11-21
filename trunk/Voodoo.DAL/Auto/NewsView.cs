/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码，如需要添加方法，请创建同名类，并在该类中添加新的方法。
*生成时间：2011-11-21 8:44:55
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
    ///表News的数据操作类
    ///</summary>
    public partial class NewsView
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
        protected static List<News> DataTableToList(DataTable dt)
        {
            List<News> Ms = new List<News>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                News M = new News();
                M.ID = dt.Rows[i]["ID"].ToInt32();
                M.NewsTime = dt.Rows[i]["NewsTime"].ToDateTime();
                M.Title = dt.Rows[i]["Title"].ToString();
                M.TitleB = dt.Rows[i]["TitleB"].ToBoolean();
                M.TitleI = dt.Rows[i]["TitleI"].ToBoolean();
                M.TitleS = dt.Rows[i]["TitleS"].ToBoolean();
                M.TitleColor = dt.Rows[i]["TitleColor"].ToString();
                M.FTitle = dt.Rows[i]["FTitle"].ToString();
                M.Audit = dt.Rows[i]["Audit"].ToBoolean();
                M.Tuijian = dt.Rows[i]["Tuijian"].ToBoolean();
                M.Toutiao = dt.Rows[i]["Toutiao"].ToBoolean();
                M.KeyWords = dt.Rows[i]["KeyWords"].ToString();
                M.NavUrl = dt.Rows[i]["NavUrl"].ToString();
                M.TitleImage = dt.Rows[i]["TitleImage"].ToString();
                M.Description = dt.Rows[i]["Description"].ToString();
                M.Author = dt.Rows[i]["Author"].ToString();
                M.AutorID = dt.Rows[i]["AutorID"].ToInt32();
                M.Content = dt.Rows[i]["Content"].ToString();
                M.SetTop = dt.Rows[i]["SetTop"].ToBoolean();
                M.ModelID = dt.Rows[i]["ModelID"].ToInt32();
                M.ClickCount = dt.Rows[i]["ClickCount"].ToInt32();
                M.DownCount = dt.Rows[i]["DownCount"].ToInt32();
                M.FileForder = dt.Rows[i]["FileForder"].ToString();
                M.FileName = dt.Rows[i]["FileName"].ToString();
                M.ZtID = dt.Rows[i]["ZtID"].ToInt32();
                M.ClassID = dt.Rows[i]["ClassID"].ToInt32();
                M.ReplyCount = dt.Rows[i]["ReplyCount"].ToInt32();
                M.Source = dt.Rows[i]["Source"].ToString();
                M.EnableReply = dt.Rows[i]["EnableReply"].ToBoolean();

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
        public static void Insert(News M)
        {
            IDbHelper Sql = GetHelper();
            StringBuilder sb = new StringBuilder();

            sb.Append("insert into [News]([NewsTime],[Title],[TitleB],[TitleI],[TitleS],[TitleColor],[FTitle],[Audit],[Tuijian],[Toutiao],[KeyWords],[NavUrl],[TitleImage],[Description],[Author],[AutorID],[Content],[SetTop],[ModelID],[ClickCount],[DownCount],[FileForder],[FileName],[ZtID],[ClassID],[ReplyCount],[Source],[EnableReply]) values(");
            sb.Append("'" + M.NewsTime + "'");
            sb.Append(",");
            sb.Append("'" + M.Title + "'");
            sb.Append(",");
            sb.Append(M.TitleB.ToS());
            sb.Append(",");
            sb.Append(M.TitleI.ToS());
            sb.Append(",");
            sb.Append(M.TitleS.ToS());
            sb.Append(",");
            sb.Append("'" + M.TitleColor + "'");
            sb.Append(",");
            sb.Append("'" + M.FTitle + "'");
            sb.Append(",");
            sb.Append(M.Audit.ToS());
            sb.Append(",");
            sb.Append(M.Tuijian.ToS());
            sb.Append(",");
            sb.Append(M.Toutiao.ToS());
            sb.Append(",");
            sb.Append("'" + M.KeyWords + "'");
            sb.Append(",");
            sb.Append("'" + M.NavUrl + "'");
            sb.Append(",");
            sb.Append("'" + M.TitleImage + "'");
            sb.Append(",");
            sb.Append("'" + M.Description + "'");
            sb.Append(",");
            sb.Append("'" + M.Author + "'");
            sb.Append(",");
            sb.Append(M.AutorID.ToS());
            sb.Append(",");
            sb.Append("'" + M.Content + "'");
            sb.Append(",");
            sb.Append(M.SetTop.ToS());
            sb.Append(",");
            sb.Append(M.ModelID.ToS());
            sb.Append(",");
            sb.Append(M.ClickCount.ToS());
            sb.Append(",");
            sb.Append(M.DownCount.ToS());
            sb.Append(",");
            sb.Append("'" + M.FileForder + "'");
            sb.Append(",");
            sb.Append("'" + M.FileName + "'");
            sb.Append(",");
            sb.Append(M.ZtID.ToS());
            sb.Append(",");
            sb.Append(M.ClassID.ToS());
            sb.Append(",");
            sb.Append(M.ReplyCount.ToS());
            sb.Append(",");
            sb.Append("'" + M.Source + "'");
            sb.Append(",");
            sb.Append(M.EnableReply.ToS());
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
                sb.Append(";select max(ID) from News");
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
        public static int Update(News M)
        {
            IDbHelper Sql = GetHelper();
            StringBuilder sb = new StringBuilder();
            sb.Append("update [News] set ");

            sb.Append("[NewsTime]='" + M.NewsTime + "'");
            sb.Append(",");
            sb.Append("[Title]='" + M.Title + "'");
            sb.Append(",");
            sb.Append("[TitleB]=" + M.TitleB.ToS());
            sb.Append(",");
            sb.Append("[TitleI]=" + M.TitleI.ToS());
            sb.Append(",");
            sb.Append("[TitleS]=" + M.TitleS.ToS());
            sb.Append(",");
            sb.Append("[TitleColor]='" + M.TitleColor + "'");
            sb.Append(",");
            sb.Append("[FTitle]='" + M.FTitle + "'");
            sb.Append(",");
            sb.Append("[Audit]=" + M.Audit.ToS());
            sb.Append(",");
            sb.Append("[Tuijian]=" + M.Tuijian.ToS());
            sb.Append(",");
            sb.Append("[Toutiao]=" + M.Toutiao.ToS());
            sb.Append(",");
            sb.Append("[KeyWords]='" + M.KeyWords + "'");
            sb.Append(",");
            sb.Append("[NavUrl]='" + M.NavUrl + "'");
            sb.Append(",");
            sb.Append("[TitleImage]='" + M.TitleImage + "'");
            sb.Append(",");
            sb.Append("[Description]='" + M.Description + "'");
            sb.Append(",");
            sb.Append("[Author]='" + M.Author + "'");
            sb.Append(",");
            sb.Append("[AutorID]=" + M.AutorID.ToS());
            sb.Append(",");
            sb.Append("[Content]='" + M.Content + "'");
            sb.Append(",");
            sb.Append("[SetTop]=" + M.SetTop.ToS());
            sb.Append(",");
            sb.Append("[ModelID]=" + M.ModelID.ToS());
            sb.Append(",");
            sb.Append("[ClickCount]=" + M.ClickCount.ToS());
            sb.Append(",");
            sb.Append("[DownCount]=" + M.DownCount.ToS());
            sb.Append(",");
            sb.Append("[FileForder]='" + M.FileForder + "'");
            sb.Append(",");
            sb.Append("[FileName]='" + M.FileName + "'");
            sb.Append(",");
            sb.Append("[ZtID]=" + M.ZtID.ToS());
            sb.Append(",");
            sb.Append("[ClassID]=" + M.ClassID.ToS());
            sb.Append(",");
            sb.Append("[ReplyCount]=" + M.ReplyCount.ToS());
            sb.Append(",");
            sb.Append("[Source]='" + M.Source + "'");
            sb.Append(",");
            sb.Append("[EnableReply]=" + M.EnableReply.ToS());

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
        public static void Update(List<News> Ms)
        {
            foreach (News M in Ms)
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
        public static News GetModelByID(string id)
        {
            IDbHelper Sql = GetHelper();
            News M = new News();
            DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select [ID],[NewsTime],[Title],[TitleB],[TitleI],[TitleS],[TitleColor],[FTitle],[Audit],[Tuijian],[Toutiao],[KeyWords],[NavUrl],[TitleImage],[Description],[Author],[AutorID],[Content],[SetTop],[ModelID],[ClickCount],[DownCount],[FileForder],[FileName],[ZtID],[ClassID],[ReplyCount],[Source],[EnableReply] from [News] where ID='" + id.ToString() + "'", true);
            if (!Rs.Read())
            {
                M.ID = 0;
            }
            else
            {
                M.ID = Rs["ID"].ToInt32();
                M.NewsTime = Rs["NewsTime"].ToDateTime();
                M.Title = Rs["Title"].ToString();
                M.TitleB = Rs["TitleB"].ToBoolean();
                M.TitleI = Rs["TitleI"].ToBoolean();
                M.TitleS = Rs["TitleS"].ToBoolean();
                M.TitleColor = Rs["TitleColor"].ToString();
                M.FTitle = Rs["FTitle"].ToString();
                M.Audit = Rs["Audit"].ToBoolean();
                M.Tuijian = Rs["Tuijian"].ToBoolean();
                M.Toutiao = Rs["Toutiao"].ToBoolean();
                M.KeyWords = Rs["KeyWords"].ToString();
                M.NavUrl = Rs["NavUrl"].ToString();
                M.TitleImage = Rs["TitleImage"].ToString();
                M.Description = Rs["Description"].ToString();
                M.Author = Rs["Author"].ToString();
                M.AutorID = Rs["AutorID"].ToInt32();
                M.Content = Rs["Content"].ToString();
                M.SetTop = Rs["SetTop"].ToBoolean();
                M.ModelID = Rs["ModelID"].ToInt32();
                M.ClickCount = Rs["ClickCount"].ToInt32();
                M.DownCount = Rs["DownCount"].ToInt32();
                M.FileForder = Rs["FileForder"].ToString();
                M.FileName = Rs["FileName"].ToString();
                M.ZtID = Rs["ZtID"].ToInt32();
                M.ClassID = Rs["ClassID"].ToInt32();
                M.ReplyCount = Rs["ReplyCount"].ToInt32();
                M.Source = Rs["Source"].ToString();
                M.EnableReply = Rs["EnableReply"].ToBoolean();
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
        public static News Find(string m_where)
        {
            IDbHelper Sql = GetHelper();
            News M = new News();
            DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select [ID],[NewsTime],[Title],[TitleB],[TitleI],[TitleS],[TitleColor],[FTitle],[Audit],[Tuijian],[Toutiao],[KeyWords],[NavUrl],[TitleImage],[Description],[Author],[AutorID],[Content],[SetTop],[ModelID],[ClickCount],[DownCount],[FileForder],[FileName],[ZtID],[ClassID],[ReplyCount],[Source],[EnableReply] from [News] where " + m_where, true);
            if (!Rs.Read())
            {
                M.ID = 0;
            }
            else
            {
                M.ID = Rs["ID"].ToInt32();
                M.NewsTime = Rs["NewsTime"].ToDateTime();
                M.Title = Rs["Title"].ToString();
                M.TitleB = Rs["TitleB"].ToBoolean();
                M.TitleI = Rs["TitleI"].ToBoolean();
                M.TitleS = Rs["TitleS"].ToBoolean();
                M.TitleColor = Rs["TitleColor"].ToString();
                M.FTitle = Rs["FTitle"].ToString();
                M.Audit = Rs["Audit"].ToBoolean();
                M.Tuijian = Rs["Tuijian"].ToBoolean();
                M.Toutiao = Rs["Toutiao"].ToBoolean();
                M.KeyWords = Rs["KeyWords"].ToString();
                M.NavUrl = Rs["NavUrl"].ToString();
                M.TitleImage = Rs["TitleImage"].ToString();
                M.Description = Rs["Description"].ToString();
                M.Author = Rs["Author"].ToString();
                M.AutorID = Rs["AutorID"].ToInt32();
                M.Content = Rs["Content"].ToString();
                M.SetTop = Rs["SetTop"].ToBoolean();
                M.ModelID = Rs["ModelID"].ToInt32();
                M.ClickCount = Rs["ClickCount"].ToInt32();
                M.DownCount = Rs["DownCount"].ToInt32();
                M.FileForder = Rs["FileForder"].ToString();
                M.FileName = Rs["FileName"].ToString();
                M.ZtID = Rs["ZtID"].ToInt32();
                M.ClassID = Rs["ClassID"].ToInt32();
                M.ReplyCount = Rs["ReplyCount"].ToInt32();
                M.Source = Rs["Source"].ToString();
                M.EnableReply = Rs["EnableReply"].ToBoolean();
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
            return Sql.ExecuteDataTable(CommandType.Text, "select [ID],[NewsTime],[Title],[TitleB],[TitleI],[TitleS],[TitleColor],[FTitle],[Audit],[Tuijian],[Toutiao],[KeyWords],[NavUrl],[TitleImage],[Description],[Author],[AutorID],[Content],[SetTop],[ModelID],[ClickCount],[DownCount],[FileForder],[FileName],[ZtID],[ClassID],[ReplyCount],[Source],[EnableReply] from [News] where " + m_where);
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
            DataTable dt = Sql.ExecuteDataTable(CommandType.Text, "select top " + top.ToString() + "  [ID],[NewsTime],[Title],[TitleB],[TitleI],[TitleS],[TitleColor],[FTitle],[Audit],[Tuijian],[Toutiao],[KeyWords],[NavUrl],[TitleImage],[Description],[Author],[AutorID],[Content],[SetTop],[ModelID],[ClickCount],[DownCount],[FileForder],[FileName],[ZtID],[ClassID],[ReplyCount],[Source],[EnableReply] from [News] where " + m_where);
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
            return Convert.ToInt32(Sql.ExecuteScalar(CommandType.Text, "select count(0) from [News] where " + m_where));
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
            sd = Sql.ExecuteReader(CommandType.Text, "select 1 from [News] where " + m_where, true);
            if (sd.Read())
            {
                returnValue = true;
            }
            sd.Close();
            sd.Dispose();
            return returnValue;


        }
        #endregion

        #region List<News>获取符合条件记录的实体列表,慎用！！！！
        /// <summary>
        /// List<News>获取符合条件记录的实体列表,慎用！！！！
        /// </summary>
        /// <param name="m_where">条件语句，不包含“where”</param>
        /// <returns></returns>
        public static List<News> GetModelList(string m_where)
        {
            return DataTableToList(getTable(m_where));
        }
        public static List<News> GetModelList(string m_where, int top)
        {
            return DataTableToList(getTable(m_where, top));
        }
        public static List<News> GetModelList()
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
                Sql.ExecuteNonQuery(CommandType.Text, "delete from [News] where " + m_where);
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
