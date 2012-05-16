/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码，如需要添加方法，请创建同名类，并在该类中添加新的方法。
*生成时间：2012/4/9 20:37:01
*生成者：Kuibono
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
	///表MovieUrl的数据操作类
	///</summary>
    public partial class MovieUrlView
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
        protected static List<MovieUrl> DataTableToList(DataTable dt)
        {
            List<MovieUrl> Ms = new List<MovieUrl>();
			
			for (int i = 0; i < dt.Rows.Count; i++)
            {
				MovieUrl M = new MovieUrl();
				M.Id=dt.Rows[i]["id"].ToInt64();
				M.MoviewID=dt.Rows[i]["MoviewID"].ToInt32();
				M.MoviewTitle=dt.Rows[i]["MoviewTitle"].ToString();
				M.Title=dt.Rows[i]["Title"].ToString();
				M.UpdateTime=dt.Rows[i]["UpdateTime"].ToDateTime();
				M.Enable=dt.Rows[i]["Enable"].ToBoolean();
				M.HttpUrl=dt.Rows[i]["HttpUrl"].ToString();
				M.MagUrl=dt.Rows[i]["MagUrl"].ToString();
				M.KuaibUrl=dt.Rows[i]["KuaibUrl"].ToString();
				M.BaiduUrl=dt.Rows[i]["BaiduUrl"].ToString();
				
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
		public static void Insert(MovieUrl M)
        {
            IDbHelper Sql = GetHelper();
            StringBuilder sb = new StringBuilder();			
			
			sb.Append("insert into [MovieUrl]([id],[MoviewID],[MoviewTitle],[Title],[UpdateTime],[Enable],[HttpUrl],[MagUrl],[KuaibUrl],[BaiduUrl]) values(");
			sb.Append(M.MoviewID.ToS());
			sb.Append(",");	
			sb.Append("N'"+M.MoviewTitle+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Title+"'");
			sb.Append(",");	
			sb.Append("N'"+M.UpdateTime+"'");
			sb.Append(",");	
			sb.Append(M.Enable.ToS());
			sb.Append(",");	
			sb.Append("N'"+M.HttpUrl+"'");
			sb.Append(",");	
			sb.Append("N'"+M.MagUrl+"'");
			sb.Append(",");	
			sb.Append("N'"+M.KuaibUrl+"'");
			sb.Append(",");	
			sb.Append("N'"+M.BaiduUrl+"'");
			sb.Append(")");
			
			if(DataBase.CmsDbType==DataBase.DbType.SqlServer)
			{
				sb.Append(";select @@Identity");	
			}
			if(DataBase.CmsDbType==DataBase.DbType.SQLite)
			{
				sb.Append(";select last_insert_rowid()");	
			}
			if(DataBase.CmsDbType==DataBase.DbType.MySql)
			{
				sb.Append(";select LAST_INSERT_ID()");	
			}
			if(DataBase.CmsDbType==DataBase.DbType.Access)
			{
				sb.Append(";select max(id) from MovieUrl");	
			}
			if(DataBase.CmsDbType==DataBase.DbType.Oracle)
			{
				sb.Append(";select LAST_INSERT_ID()");	
			}
			

            M.Id=Sql.ExecuteScalar(CommandType.Text, sb.ToString()).ToInt32();
        }
		
		#endregion			
		
		#region Update将修改过的实体修改到数据库
		/// <summary>
        /// 将修改过的实体修改到数据库
        /// </summary>
        /// <param name="M">赋值后的实体</param>
        /// <returns></returns>
		public static int Update(MovieUrl M)
        {
            IDbHelper Sql = GetHelper();
			StringBuilder sb = new StringBuilder();
            sb.Append("update [MovieUrl] set ");
			
			sb.Append("[MoviewID]="+M.MoviewID.ToS());
			sb.Append(",");
			sb.Append("[MoviewTitle]=N'"+M.MoviewTitle+"'");
			sb.Append(",");
			sb.Append("[Title]=N'"+M.Title+"'");
			sb.Append(",");
			sb.Append("[UpdateTime]=N'"+M.UpdateTime+"'");
			sb.Append(",");
			sb.Append("[Enable]="+M.Enable.ToS());
			sb.Append(",");
			sb.Append("[HttpUrl]=N'"+M.HttpUrl+"'");
			sb.Append(",");
			sb.Append("[MagUrl]=N'"+M.MagUrl+"'");
			sb.Append(",");
			sb.Append("[KuaibUrl]=N'"+M.KuaibUrl+"'");
			sb.Append(",");
			sb.Append("[BaiduUrl]=N'"+M.BaiduUrl+"'");
			
			sb.Append(" where Id='" + M.Id + "'");
			sb.Append("");
			
			if(DataBase.CmsDbType==DataBase.DbType.SqlServer)
			{
				sb.Append(";select @@ROWCOUNT");	
			}
			if(DataBase.CmsDbType==DataBase.DbType.SQLite)
			{
				sb.Append(";select 0");	
			}
			if(DataBase.CmsDbType==DataBase.DbType.MySql)
			{
				sb.Append(";SELECT ROW_COUNT()");	
			}
			if(DataBase.CmsDbType==DataBase.DbType.Access)
			{
				sb.Append(";select 0");	
			}
			if(DataBase.CmsDbType==DataBase.DbType.Oracle)
			{
				sb.Append(";select SQL%ROWCOUNT");	
			}
			
			
			return Sql.ExecuteScalar(CommandType.Text, sb.ToString()).ToInt32();
        }
		
		/// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="Ms"></param>
        public static void Update(List<MovieUrl> Ms)
        {
            foreach (MovieUrl M in Ms)
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
		public static MovieUrl GetModelByID(string id)
		{
			IDbHelper Sql = GetHelper();
			MovieUrl M = new MovieUrl();
			DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select [id],[MoviewID],[MoviewTitle],[Title],[UpdateTime],[Enable],[HttpUrl],[MagUrl],[KuaibUrl],[BaiduUrl] from [MovieUrl] where Id='" + id.ToString()+"'", true);
			if (!Rs.Read())
			{
					M.Id=long.MinValue;
			}
			else
			{
				M.Id=Rs["id"].ToInt64();
				M.MoviewID=Rs["MoviewID"].ToInt32();
				M.MoviewTitle=Rs["MoviewTitle"].ToString();
				M.Title=Rs["Title"].ToString();
				M.UpdateTime=Rs["UpdateTime"].ToDateTime();
				M.Enable=Rs["Enable"].ToBoolean();
				M.HttpUrl=Rs["HttpUrl"].ToString();
				M.MagUrl=Rs["MagUrl"].ToString();
				M.KuaibUrl=Rs["KuaibUrl"].ToString();
				M.BaiduUrl=Rs["BaiduUrl"].ToString();
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
		public static MovieUrl Find(string m_where)
		{
			IDbHelper Sql = GetHelper();
            MovieUrl M = new MovieUrl();
            DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select [id],[MoviewID],[MoviewTitle],[Title],[UpdateTime],[Enable],[HttpUrl],[MagUrl],[KuaibUrl],[BaiduUrl] from [MovieUrl] where " + m_where, true);
			if (!Rs.Read())
            {
					M.Id=long.MinValue;
            }
			else
			{
				M.Id=Rs["id"].ToInt64();
				M.MoviewID=Rs["MoviewID"].ToInt32();
				M.MoviewTitle=Rs["MoviewTitle"].ToString();
				M.Title=Rs["Title"].ToString();
				M.UpdateTime=Rs["UpdateTime"].ToDateTime();
				M.Enable=Rs["Enable"].ToBoolean();
				M.HttpUrl=Rs["HttpUrl"].ToString();
				M.MagUrl=Rs["MagUrl"].ToString();
				M.KuaibUrl=Rs["KuaibUrl"].ToString();
				M.BaiduUrl=Rs["BaiduUrl"].ToString();
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
            return Sql.ExecuteDataTable(CommandType.Text, "select [id],[MoviewID],[MoviewTitle],[Title],[UpdateTime],[Enable],[HttpUrl],[MagUrl],[KuaibUrl],[BaiduUrl] from [MovieUrl] where "+ m_where);
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
		public static DataTable getTable(string m_where,int top)
        {   
            IDbHelper Sql = GetHelper();
            DataTable dt = Sql.ExecuteDataTable(CommandType.Text, "select top "+ top.ToString() +"  [id],[MoviewID],[MoviewTitle],[Title],[UpdateTime],[Enable],[HttpUrl],[MagUrl],[KuaibUrl],[BaiduUrl] from [MovieUrl] where "+ m_where);
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
			return Convert.ToInt32(Sql.ExecuteScalar(CommandType.Text,"select count(0) from [MovieUrl] where "+m_where));
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
            sd = Sql.ExecuteReader(CommandType.Text, "select 1 from [MovieUrl] where " + m_where, true);
            if (sd.Read())
            {
                returnValue = true;
            }
            sd.Close();
            sd.Dispose();
            return returnValue;
			
			
		}
		#endregion
		
		#region List<MovieUrl>获取符合条件记录的实体列表,慎用！！！！
		/// <summary>
        /// List<MovieUrl>获取符合条件记录的实体列表,慎用！！！！
        /// </summary>
        /// <param name="m_where">条件语句，不包含“where”</param>
        /// <returns></returns>
		public static List<MovieUrl> GetModelList(string m_where)
		{	
			return DataTableToList(getTable(m_where));
		}
		public static List<MovieUrl> GetModelList(string m_where,int top)
		{	
			return DataTableToList(getTable(m_where, top));
		}
		public static List<MovieUrl> GetModelList()
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
				Sql.ExecuteNonQuery(CommandType.Text, "delete from [MovieUrl] where "+ m_where);
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
			return Del("Id="+ID.ToString());
		}
		#endregion
		
		
	}
	
	
}