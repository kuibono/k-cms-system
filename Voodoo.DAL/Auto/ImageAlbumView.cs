/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码，如需要添加方法，请创建同名类，并在该类中添加新的方法。
*生成时间：2011/11/27 13:42:58
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
    ///表ImageAlbum的数据操作类
    ///</summary>
    public partial class ImageAlbumView
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
        protected static List<ImageAlbum> DataTableToList(DataTable dt)
        {
            List<ImageAlbum> Ms = new List<ImageAlbum>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ImageAlbum M = new ImageAlbum();
                M.ID = dt.Rows[i]["ID"].ToInt32();
                M.ClassID = dt.Rows[i]["ClassID"].ToInt32();
                M.ZtID = dt.Rows[i]["ZtID"].ToInt32();
                M.AuthorID = dt.Rows[i]["AuthorID"].ToInt32();
                M.Author = dt.Rows[i]["Author"].ToString();
                M.Title = dt.Rows[i]["Title"].ToString();
                M.CreateTime = dt.Rows[i]["CreateTime"].ToDateTime();
                M.UpdateTime = dt.Rows[i]["UpdateTime"].ToDateTime();
                M.Intro = dt.Rows[i]["Intro"].ToString();
                M.Size = dt.Rows[i]["Size"].ToInt32();
                M.Folder = dt.Rows[i]["Folder"].ToString();
                M.ClickCount = dt.Rows[i]["ClickCount"].ToInt32();
                M.ReplyCount = dt.Rows[i]["ReplyCount"].ToInt32();
                M.SetTop = dt.Rows[i]["SetTop"].ToBoolean();
                M.ShotCut = dt.Rows[i]["ShotCut"].ToString();
                M.KeyWords = dt.Rows[i]["KeyWords"].ToString();

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
        public static void Insert(ImageAlbum M)
        {
            IDbHelper Sql = GetHelper();
            StringBuilder sb = new StringBuilder();

            sb.Append("insert into [ImageAlbum]([ClassID],[ZtID],[AuthorID],[Author],[Title],[CreateTime],[UpdateTime],[Intro],[Size],[Folder],[ClickCount],[ReplyCount],[SetTop],[ShotCut],[KeyWords]) values(");
            sb.Append(M.ClassID.ToS());
            sb.Append(",");
            sb.Append(M.ZtID.ToS());
            sb.Append(",");
            sb.Append(M.AuthorID.ToS());
            sb.Append(",");
            sb.Append("'" + M.Author + "'");
            sb.Append(",");
            sb.Append("'" + M.Title + "'");
            sb.Append(",");
            sb.Append("'" + M.CreateTime + "'");
            sb.Append(",");
            sb.Append("'" + M.UpdateTime + "'");
            sb.Append(",");
            sb.Append("'" + M.Intro + "'");
            sb.Append(",");
            sb.Append(M.Size.ToS());
            sb.Append(",");
            sb.Append("'" + M.Folder + "'");
            sb.Append(",");
            sb.Append(M.ClickCount.ToS());
            sb.Append(",");
            sb.Append(M.ReplyCount.ToS());
            sb.Append(",");
            sb.Append(M.SetTop.ToS());
            sb.Append(",");
            sb.Append("'" + M.ShotCut + "'");
            sb.Append(",");
            sb.Append("'" + M.KeyWords + "'");
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
                sb.Append(";select max(ID) from ImageAlbum");
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
        public static int Update(ImageAlbum M)
        {
            IDbHelper Sql = GetHelper();
            StringBuilder sb = new StringBuilder();
            sb.Append("update [ImageAlbum] set ");

            sb.Append("[ClassID]=" + M.ClassID.ToS());
            sb.Append(",");
            sb.Append("[ZtID]=" + M.ZtID.ToS());
            sb.Append(",");
            sb.Append("[AuthorID]=" + M.AuthorID.ToS());
            sb.Append(",");
            sb.Append("[Author]='" + M.Author + "'");
            sb.Append(",");
            sb.Append("[Title]='" + M.Title + "'");
            sb.Append(",");
            sb.Append("[CreateTime]='" + M.CreateTime + "'");
            sb.Append(",");
            sb.Append("[UpdateTime]='" + M.UpdateTime + "'");
            sb.Append(",");
            sb.Append("[Intro]='" + M.Intro + "'");
            sb.Append(",");
            sb.Append("[Size]=" + M.Size.ToS());
            sb.Append(",");
            sb.Append("[Folder]='" + M.Folder + "'");
            sb.Append(",");
            sb.Append("[ClickCount]=" + M.ClickCount.ToS());
            sb.Append(",");
            sb.Append("[ReplyCount]=" + M.ReplyCount.ToS());
            sb.Append(",");
            sb.Append("[SetTop]=" + M.SetTop.ToS());
            sb.Append(",");
            sb.Append("[ShotCut]='" + M.ShotCut + "'");
            sb.Append(",");
            sb.Append("[KeyWords]='" + M.KeyWords + "'");

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
        public static void Update(List<ImageAlbum> Ms)
        {
            foreach (ImageAlbum M in Ms)
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
        public static ImageAlbum GetModelByID(string id)
        {
            IDbHelper Sql = GetHelper();
            ImageAlbum M = new ImageAlbum();
            DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select [ID],[ClassID],[ZtID],[AuthorID],[Author],[Title],[CreateTime],[UpdateTime],[Intro],[Size],[Folder],[ClickCount],[ReplyCount],[SetTop],[ShotCut],[KeyWords] from [ImageAlbum] where ID='" + id.ToString() + "'", true);
            if (!Rs.Read())
            {
                M.ID = 0;
            }
            else
            {
                M.ID = Rs["ID"].ToInt32();
                M.ClassID = Rs["ClassID"].ToInt32();
                M.ZtID = Rs["ZtID"].ToInt32();
                M.AuthorID = Rs["AuthorID"].ToInt32();
                M.Author = Rs["Author"].ToString();
                M.Title = Rs["Title"].ToString();
                M.CreateTime = Rs["CreateTime"].ToDateTime();
                M.UpdateTime = Rs["UpdateTime"].ToDateTime();
                M.Intro = Rs["Intro"].ToString();
                M.Size = Rs["Size"].ToInt32();
                M.Folder = Rs["Folder"].ToString();
                M.ClickCount = Rs["ClickCount"].ToInt32();
                M.ReplyCount = Rs["ReplyCount"].ToInt32();
                M.SetTop = Rs["SetTop"].ToBoolean();
                M.ShotCut = Rs["ShotCut"].ToString();
                M.KeyWords = Rs["KeyWords"].ToString();
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
        public static ImageAlbum Find(string m_where)
        {
            IDbHelper Sql = GetHelper();
            ImageAlbum M = new ImageAlbum();
            DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select [ID],[ClassID],[ZtID],[AuthorID],[Author],[Title],[CreateTime],[UpdateTime],[Intro],[Size],[Folder],[ClickCount],[ReplyCount],[SetTop],[ShotCut],[KeyWords] from [ImageAlbum] where " + m_where, true);
            if (!Rs.Read())
            {
                M.ID = 0;
            }
            else
            {
                M.ID = Rs["ID"].ToInt32();
                M.ClassID = Rs["ClassID"].ToInt32();
                M.ZtID = Rs["ZtID"].ToInt32();
                M.AuthorID = Rs["AuthorID"].ToInt32();
                M.Author = Rs["Author"].ToString();
                M.Title = Rs["Title"].ToString();
                M.CreateTime = Rs["CreateTime"].ToDateTime();
                M.UpdateTime = Rs["UpdateTime"].ToDateTime();
                M.Intro = Rs["Intro"].ToString();
                M.Size = Rs["Size"].ToInt32();
                M.Folder = Rs["Folder"].ToString();
                M.ClickCount = Rs["ClickCount"].ToInt32();
                M.ReplyCount = Rs["ReplyCount"].ToInt32();
                M.SetTop = Rs["SetTop"].ToBoolean();
                M.ShotCut = Rs["ShotCut"].ToString();
                M.KeyWords = Rs["KeyWords"].ToString();
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
            return Sql.ExecuteDataTable(CommandType.Text, "select [ID],[ClassID],[ZtID],[AuthorID],[Author],[Title],[CreateTime],[UpdateTime],[Intro],[Size],[Folder],[ClickCount],[ReplyCount],[SetTop],[ShotCut],[KeyWords] from [ImageAlbum] where " + m_where);
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
            DataTable dt = Sql.ExecuteDataTable(CommandType.Text, "select top " + top.ToString() + "  [ID],[ClassID],[ZtID],[AuthorID],[Author],[Title],[CreateTime],[UpdateTime],[Intro],[Size],[Folder],[ClickCount],[ReplyCount],[SetTop],[ShotCut],[KeyWords] from [ImageAlbum] where " + m_where);
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
            return Convert.ToInt32(Sql.ExecuteScalar(CommandType.Text, "select count(0) from [ImageAlbum] where " + m_where));
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
            sd = Sql.ExecuteReader(CommandType.Text, "select 1 from [ImageAlbum] where " + m_where, true);
            if (sd.Read())
            {
                returnValue = true;
            }
            sd.Close();
            sd.Dispose();
            return returnValue;


        }
        #endregion

        #region List<ImageAlbum>获取符合条件记录的实体列表,慎用！！！！
        /// <summary>
        /// List<ImageAlbum>获取符合条件记录的实体列表,慎用！！！！
        /// </summary>
        /// <param name="m_where">条件语句，不包含“where”</param>
        /// <returns></returns>
        public static List<ImageAlbum> GetModelList(string m_where)
        {
            return DataTableToList(getTable(m_where));
        }
        public static List<ImageAlbum> GetModelList(string m_where, int top)
        {
            return DataTableToList(getTable(m_where, top));
        }
        public static List<ImageAlbum> GetModelList()
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
                Sql.ExecuteNonQuery(CommandType.Text, "delete from [ImageAlbum] where " + m_where);
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
