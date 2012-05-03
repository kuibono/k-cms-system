/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码，如需要添加方法，请创建同名类，并在该类中添加新的方法。
*生成时间：2012-5-3 10:58:40
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
	///表MovieDrama的数据操作类
	///</summary>
    public partial class MovieDramaView
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
        protected static List<MovieDrama> DataTableToList(DataTable dt)
        {
            List<MovieDrama> Ms = new List<MovieDrama>();
			
			for (int i = 0; i < dt.Rows.Count; i++)
            {
				MovieDrama M = new MovieDrama();
				M.Id=dt.Rows[i]["id"].ToInt64();
				M.MovieID=dt.Rows[i]["MovieID"].ToInt32();
				M.MovieTitle=dt.Rows[i]["MovieTitle"].ToString();
				M.Title=dt.Rows[i]["Title"].ToString();
				M.UpdateTime=dt.Rows[i]["UpdateTime"].ToDateTime();
				M.Enable=dt.Rows[i]["Enable"].ToBoolean();
				
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
		public static void Insert(MovieDrama M)
        {
            IDbHelper Sql = GetHelper();
            StringBuilder sb = new StringBuilder();			
			
			sb.Append("insert into [MovieDrama]([id],[MovieID],[MovieTitle],[Title],[UpdateTime],[Enable]) values(");
			sb.Append(M.MovieID.ToS());
			sb.Append(",");	
			sb.Append("N'"+M.MovieTitle+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Title+"'");
			sb.Append(",");	
			sb.Append("N'"+M.UpdateTime+"'");
			sb.Append(",");	
			sb.Append(M.Enable.ToS());
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
				sb.Append(";select max(id) from MovieDrama");	
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
		public static int Update(MovieDrama M)
        {
            IDbHelper Sql = GetHelper();
			StringBuilder sb = new StringBuilder();
            sb.Append("update [MovieDrama] set ");
			
			sb.Append("[MovieID]="+M.MovieID.ToS());
			sb.Append(",");
			sb.Append("[MovieTitle]=N'"+M.MovieTitle+"'");
			sb.Append(",");
			sb.Append("[Title]=N'"+M.Title+"'");
			sb.Append(",");
			sb.Append("[UpdateTime]=N'"+M.UpdateTime+"'");
			sb.Append(",");
			sb.Append("[Enable]="+M.Enable.ToS());
			
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
        public static void Update(List<MovieDrama> Ms)
        {
            foreach (MovieDrama M in Ms)
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
		public static MovieDrama GetModelByID(string id)
		{
			IDbHelper Sql = GetHelper();
			MovieDrama M = new MovieDrama();
			DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select [id],[MovieID],[MovieTitle],[Title],[UpdateTime],[Enable] from [MovieDrama] where Id='" + id.ToString()+"'", true);
			if (!Rs.Read())
			{
					M.Id=long.MinValue;
			}
			else
			{
				M.Id=Rs["id"].ToInt64();
				M.MovieID=Rs["MovieID"].ToInt32();
				M.MovieTitle=Rs["MovieTitle"].ToString();
				M.Title=Rs["Title"].ToString();
				M.UpdateTime=Rs["UpdateTime"].ToDateTime();
				M.Enable=Rs["Enable"].ToBoolean();
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
		public static MovieDrama Find(string m_where)
		{
			IDbHelper Sql = GetHelper();
            MovieDrama M = new MovieDrama();
            DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select [id],[MovieID],[MovieTitle],[Title],[UpdateTime],[Enable] from [MovieDrama] where " + m_where, true);
			if (!Rs.Read())
            {
					M.Id=long.MinValue;
            }
			else
			{
				M.Id=Rs["id"].ToInt64();
				M.MovieID=Rs["MovieID"].ToInt32();
				M.MovieTitle=Rs["MovieTitle"].ToString();
				M.Title=Rs["Title"].ToString();
				M.UpdateTime=Rs["UpdateTime"].ToDateTime();
				M.Enable=Rs["Enable"].ToBoolean();
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
            return Sql.ExecuteDataTable(CommandType.Text, "select [id],[MovieID],[MovieTitle],[Title],[UpdateTime],[Enable] from [MovieDrama] where "+ m_where);
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
            DataTable dt = Sql.ExecuteDataTable(CommandType.Text, "select top "+ top.ToString() +"  [id],[MovieID],[MovieTitle],[Title],[UpdateTime],[Enable] from [MovieDrama] where "+ m_where);
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
			return Convert.ToInt32(Sql.ExecuteScalar(CommandType.Text,"select count(0) from [MovieDrama] where "+m_where));
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
            sd = Sql.ExecuteReader(CommandType.Text, "select 1 from [MovieDrama] where " + m_where, true);
            if (sd.Read())
            {
                returnValue = true;
            }
            sd.Close();
            sd.Dispose();
            return returnValue;
			
			
		}
		#endregion
		
		#region List<MovieDrama>获取符合条件记录的实体列表,慎用！！！！
		/// <summary>
        /// List<MovieDrama>获取符合条件记录的实体列表,慎用！！！！
        /// </summary>
        /// <param name="m_where">条件语句，不包含“where”</param>
        /// <returns></returns>
		public static List<MovieDrama> GetModelList(string m_where)
		{	
			return DataTableToList(getTable(m_where));
		}
		public static List<MovieDrama> GetModelList(string m_where,int top)
		{	
			return DataTableToList(getTable(m_where, top));
		}
		public static List<MovieDrama> GetModelList()
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
				Sql.ExecuteNonQuery(CommandType.Text, "delete from [MovieDrama] where "+ m_where);
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
