/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码，如需要添加方法，请创建同名类，并在该类中添加新的方法。
*生成时间：2011-11-17 9:30:06
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
    ///表File的数据操作类
    ///</summary>
    public partial class FileView
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
        protected static List<File> DataTableToList(DataTable dt)
        {
            List<File> Ms = new List<File>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                File M = new File();
                M.ID = dt.Rows[i]["ID"].ToInt32();
                M.UpTime = dt.Rows[i]["UpTime"].ToDateTime();
                M.FileType = dt.Rows[i]["FileType"].ToInt32();
                M.FileSize = dt.Rows[i]["FileSize"].ToInt64();
                M.FileDirectory = dt.Rows[i]["FileDirectory"].ToString();
                M.FileName = dt.Rows[i]["FileName"].ToString();
                M.FileExtName = dt.Rows[i]["FileExtName"].ToString();
                M.FilePath = dt.Rows[i]["FilePath"].ToString();
                M.SmallPath = dt.Rows[i]["SmallPath"].ToString();

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
        public static void Insert(File M)
        {
            IDbHelper Sql = GetHelper();
            StringBuilder sb = new StringBuilder();

            sb.Append("insert into [File](UpTime,FileType,FileSize,FileDirectory,FileName,FileExtName,FilePath,SmallPath) values(");
            sb.Append("'" + M.UpTime + "'");
            sb.Append(",");
            sb.Append(M.FileType.ToS());
            sb.Append(",");
            sb.Append(M.FileSize.ToS());
            sb.Append(",");
            sb.Append("'" + M.FileDirectory + "'");
            sb.Append(",");
            sb.Append("'" + M.FileName + "'");
            sb.Append(",");
            sb.Append("'" + M.FileExtName + "'");
            sb.Append(",");
            sb.Append("'" + M.FilePath + "'");
            sb.Append(",");
            sb.Append("'" + M.SmallPath + "'");
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
                sb.Append(";select max(ID) from File");
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
        public static int Update(File M)
        {
            IDbHelper Sql = GetHelper();
            StringBuilder sb = new StringBuilder();
            sb.Append("update [File] set ");

            sb.Append("[UpTime]='" + M.UpTime + "'");
            sb.Append(",");
            sb.Append("[FileType]=" + M.FileType.ToS());
            sb.Append(",");
            sb.Append("[FileSize]=" + M.FileSize.ToS());
            sb.Append(",");
            sb.Append("[FileDirectory]='" + M.FileDirectory + "'");
            sb.Append(",");
            sb.Append("[FileName]='" + M.FileName + "'");
            sb.Append(",");
            sb.Append("[FileExtName]='" + M.FileExtName + "'");
            sb.Append(",");
            sb.Append("[FilePath]='" + M.FilePath + "'");
            sb.Append(",");
            sb.Append("[SmallPath]='" + M.SmallPath + "'");

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
        public static void Update(List<File> Ms)
        {
            foreach (File M in Ms)
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
        public static File GetModelByID(string id)
        {
            IDbHelper Sql = GetHelper();
            File M = new File();
            DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select [ID],[UpTime],[FileType],[FileSize],[FileDirectory],[FileName],[FileExtName],[FilePath],[SmallPath] from [File] where ID='" + id.ToString() + "'", true);
            if (!Rs.Read())
            {
                M.ID = 0;
            }
            else
            {
                M.ID = Rs["ID"].ToInt32();
                M.UpTime = Rs["UpTime"].ToDateTime();
                M.FileType = Rs["FileType"].ToInt32();
                M.FileSize = Rs["FileSize"].ToInt64();
                M.FileDirectory = Rs["FileDirectory"].ToString();
                M.FileName = Rs["FileName"].ToString();
                M.FileExtName = Rs["FileExtName"].ToString();
                M.FilePath = Rs["FilePath"].ToString();
                M.SmallPath = Rs["SmallPath"].ToString();
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
        public static File Find(string m_where)
        {
            IDbHelper Sql = GetHelper();
            File M = new File();
            DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select [ID],[UpTime],[FileType],[FileSize],[FileDirectory],[FileName],[FileExtName],[FilePath],[SmallPath] from [File] where " + m_where, true);
            if (!Rs.Read())
            {
                M.ID = 0;
            }
            else
            {
                M.ID = Rs["ID"].ToInt32();
                M.UpTime = Rs["UpTime"].ToDateTime();
                M.FileType = Rs["FileType"].ToInt32();
                M.FileSize = Rs["FileSize"].ToInt64();
                M.FileDirectory = Rs["FileDirectory"].ToString();
                M.FileName = Rs["FileName"].ToString();
                M.FileExtName = Rs["FileExtName"].ToString();
                M.FilePath = Rs["FilePath"].ToString();
                M.SmallPath = Rs["SmallPath"].ToString();
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
            return Sql.ExecuteDataTable(CommandType.Text, "select [ID],[UpTime],[FileType],[FileSize],[FileDirectory],[FileName],[FileExtName],[FilePath],[SmallPath] from [File] where " + m_where);
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
            DataTable dt = Sql.ExecuteDataTable(CommandType.Text, "select top " + top.ToString() + "  [ID],[UpTime],[FileType],[FileSize],[FileDirectory],[FileName],[FileExtName],[FilePath],[SmallPath] from [File] where " + m_where);
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
            return Convert.ToInt32(Sql.ExecuteScalar(CommandType.Text, "select count(0) from [File] where " + m_where));
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
            sd = Sql.ExecuteReader(CommandType.Text, "select 1 from [File] where " + m_where, true);
            if (sd.Read())
            {
                returnValue = true;
            }
            sd.Close();
            sd.Dispose();
            return returnValue;


        }
        #endregion

        #region List<File>获取符合条件记录的实体列表,慎用！！！！
        /// <summary>
        /// List<File>获取符合条件记录的实体列表,慎用！！！！
        /// </summary>
        /// <param name="m_where">条件语句，不包含“where”</param>
        /// <returns></returns>
        public static List<File> GetModelList(string m_where)
        {
            return DataTableToList(getTable(m_where));
        }
        public static List<File> GetModelList(string m_where, int top)
        {
            return DataTableToList(getTable(m_where, top));
        }
        public static List<File> GetModelList()
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
                Sql.ExecuteNonQuery(CommandType.Text, "delete from [File] where " + m_where);
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
