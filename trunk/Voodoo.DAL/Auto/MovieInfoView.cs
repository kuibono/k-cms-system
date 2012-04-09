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
	///表MovieInfo的数据操作类
	///</summary>
    public partial class MovieInfoView
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
        protected static List<MovieInfo> DataTableToList(DataTable dt)
        {
            List<MovieInfo> Ms = new List<MovieInfo>();
			
			for (int i = 0; i < dt.Rows.Count; i++)
            {
				MovieInfo M = new MovieInfo();
				M.Id=dt.Rows[i]["id"].ToInt32();
				M.ClassID=dt.Rows[i]["ClassID"].ToInt32();
				M.ClassName=dt.Rows[i]["ClassName"].ToString();
				M.Title=dt.Rows[i]["Title"].ToString();
				M.Director=dt.Rows[i]["Director"].ToString();
				M.Actors=dt.Rows[i]["Actors"].ToString();
				M.Tags=dt.Rows[i]["Tags"].ToString();
				M.Location=dt.Rows[i]["Location"].ToString();
				M.PublicYear=dt.Rows[i]["PublicYear"].ToString();
				M.Intro=dt.Rows[i]["Intro"].ToString();
				M.IsMove=dt.Rows[i]["IsMove"].ToBoolean();
				M.FaceImage=dt.Rows[i]["FaceImage"].ToString();
				M.InsertTime=dt.Rows[i]["InsertTime"].ToDateTime();
				M.LastDramaTitle=dt.Rows[i]["LastDramaTitle"].ToDateTime();
				M.LastDramaID=dt.Rows[i]["LastDramaID"].ToInt64();
				M.UpdateTime=dt.Rows[i]["UpdateTime"].ToDateTime();
				M.Status=dt.Rows[i]["Status"].ToInt32();
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
		public static void Insert(MovieInfo M)
        {
            IDbHelper Sql = GetHelper();
            StringBuilder sb = new StringBuilder();			
			
			sb.Append("insert into [MovieInfo]([ClassID],[ClassName],[Title],[Director],[Actors],[Tags],[Location],[PublicYear],[Intro],[IsMove],[FaceImage],[InsertTime],[LastDramaTitle],[LastDramaID],[UpdateTime],[Status],[Enable]) values(");
			sb.Append(M.ClassID.ToS());
			sb.Append(",");	
			sb.Append("N'"+M.ClassName+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Title+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Director+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Actors+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Tags+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Location+"'");
			sb.Append(",");	
			sb.Append("N'"+M.PublicYear+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Intro+"'");
			sb.Append(",");	
			sb.Append(M.IsMove.ToS());
			sb.Append(",");	
			sb.Append("N'"+M.FaceImage+"'");
			sb.Append(",");	
			sb.Append("N'"+M.InsertTime+"'");
			sb.Append(",");	
			sb.Append("N'"+M.LastDramaTitle+"'");
			sb.Append(",");	
			sb.Append(M.LastDramaID.ToS());
			sb.Append(",");	
			sb.Append("N'"+M.UpdateTime+"'");
			sb.Append(",");	
			sb.Append(M.Status.ToS());
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
				sb.Append(";select max(id) from MovieInfo");	
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
		public static int Update(MovieInfo M)
        {
            IDbHelper Sql = GetHelper();
			StringBuilder sb = new StringBuilder();
            sb.Append("update [MovieInfo] set ");
			
			sb.Append("[ClassID]="+M.ClassID.ToS());
			sb.Append(",");
			sb.Append("[ClassName]=N'"+M.ClassName+"'");
			sb.Append(",");
			sb.Append("[Title]=N'"+M.Title+"'");
			sb.Append(",");
			sb.Append("[Director]=N'"+M.Director+"'");
			sb.Append(",");
			sb.Append("[Actors]=N'"+M.Actors+"'");
			sb.Append(",");
			sb.Append("[Tags]=N'"+M.Tags+"'");
			sb.Append(",");
			sb.Append("[Location]=N'"+M.Location+"'");
			sb.Append(",");
			sb.Append("[PublicYear]=N'"+M.PublicYear+"'");
			sb.Append(",");
			sb.Append("[Intro]=N'"+M.Intro+"'");
			sb.Append(",");
			sb.Append("[IsMove]="+M.IsMove.ToS());
			sb.Append(",");
			sb.Append("[FaceImage]=N'"+M.FaceImage+"'");
			sb.Append(",");
			sb.Append("[InsertTime]=N'"+M.InsertTime+"'");
			sb.Append(",");
			sb.Append("[LastDramaTitle]=N'"+M.LastDramaTitle+"'");
			sb.Append(",");
			sb.Append("[LastDramaID]="+M.LastDramaID.ToS());
			sb.Append(",");
			sb.Append("[UpdateTime]=N'"+M.UpdateTime+"'");
			sb.Append(",");
			sb.Append("[Status]="+M.Status.ToS());
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
        public static void Update(List<MovieInfo> Ms)
        {
            foreach (MovieInfo M in Ms)
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
		public static MovieInfo GetModelByID(string id)
		{
			IDbHelper Sql = GetHelper();
			MovieInfo M = new MovieInfo();
			DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select [id],[ClassID],[ClassName],[Title],[Director],[Actors],[Tags],[Location],[PublicYear],[Intro],[IsMove],[FaceImage],[InsertTime],[LastDramaTitle],[LastDramaID],[UpdateTime],[Status],[Enable] from [MovieInfo] where Id='" + id.ToString()+"'", true);
			if (!Rs.Read())
			{
					M.Id=0;
			}
			else
			{
				M.Id=Rs["id"].ToInt32();
				M.ClassID=Rs["ClassID"].ToInt32();
				M.ClassName=Rs["ClassName"].ToString();
				M.Title=Rs["Title"].ToString();
				M.Director=Rs["Director"].ToString();
				M.Actors=Rs["Actors"].ToString();
				M.Tags=Rs["Tags"].ToString();
				M.Location=Rs["Location"].ToString();
				M.PublicYear=Rs["PublicYear"].ToString();
				M.Intro=Rs["Intro"].ToString();
				M.IsMove=Rs["IsMove"].ToBoolean();
				M.FaceImage=Rs["FaceImage"].ToString();
				M.InsertTime=Rs["InsertTime"].ToDateTime();
				M.LastDramaTitle=Rs["LastDramaTitle"].ToDateTime();
				M.LastDramaID=Rs["LastDramaID"].ToInt64();
				M.UpdateTime=Rs["UpdateTime"].ToDateTime();
				M.Status=Rs["Status"].ToInt32();
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
		public static MovieInfo Find(string m_where)
		{
			IDbHelper Sql = GetHelper();
            MovieInfo M = new MovieInfo();
            DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select [id],[ClassID],[ClassName],[Title],[Director],[Actors],[Tags],[Location],[PublicYear],[Intro],[IsMove],[FaceImage],[InsertTime],[LastDramaTitle],[LastDramaID],[UpdateTime],[Status],[Enable] from [MovieInfo] where " + m_where, true);
			if (!Rs.Read())
            {
					M.Id=0;
            }
			else
			{
				M.Id=Rs["id"].ToInt32();
				M.ClassID=Rs["ClassID"].ToInt32();
				M.ClassName=Rs["ClassName"].ToString();
				M.Title=Rs["Title"].ToString();
				M.Director=Rs["Director"].ToString();
				M.Actors=Rs["Actors"].ToString();
				M.Tags=Rs["Tags"].ToString();
				M.Location=Rs["Location"].ToString();
				M.PublicYear=Rs["PublicYear"].ToString();
				M.Intro=Rs["Intro"].ToString();
				M.IsMove=Rs["IsMove"].ToBoolean();
				M.FaceImage=Rs["FaceImage"].ToString();
				M.InsertTime=Rs["InsertTime"].ToDateTime();
				M.LastDramaTitle=Rs["LastDramaTitle"].ToDateTime();
				M.LastDramaID=Rs["LastDramaID"].ToInt64();
				M.UpdateTime=Rs["UpdateTime"].ToDateTime();
				M.Status=Rs["Status"].ToInt32();
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
            return Sql.ExecuteDataTable(CommandType.Text, "select [id],[ClassID],[ClassName],[Title],[Director],[Actors],[Tags],[Location],[PublicYear],[Intro],[IsMove],[FaceImage],[InsertTime],[LastDramaTitle],[LastDramaID],[UpdateTime],[Status],[Enable] from [MovieInfo] where "+ m_where);
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
            DataTable dt = Sql.ExecuteDataTable(CommandType.Text, "select top "+ top.ToString() +"  [id],[ClassID],[ClassName],[Title],[Director],[Actors],[Tags],[Location],[PublicYear],[Intro],[IsMove],[FaceImage],[InsertTime],[LastDramaTitle],[LastDramaID],[UpdateTime],[Status],[Enable] from [MovieInfo] where "+ m_where);
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
			return Convert.ToInt32(Sql.ExecuteScalar(CommandType.Text,"select count(0) from [MovieInfo] where "+m_where));
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
            sd = Sql.ExecuteReader(CommandType.Text, "select 1 from [MovieInfo] where " + m_where, true);
            if (sd.Read())
            {
                returnValue = true;
            }
            sd.Close();
            sd.Dispose();
            return returnValue;
			
			
		}
		#endregion
		
		#region List<MovieInfo>获取符合条件记录的实体列表,慎用！！！！
		/// <summary>
        /// List<MovieInfo>获取符合条件记录的实体列表,慎用！！！！
        /// </summary>
        /// <param name="m_where">条件语句，不包含“where”</param>
        /// <returns></returns>
		public static List<MovieInfo> GetModelList(string m_where)
		{	
			return DataTableToList(getTable(m_where));
		}
		public static List<MovieInfo> GetModelList(string m_where,int top)
		{	
			return DataTableToList(getTable(m_where, top));
		}
		public static List<MovieInfo> GetModelList()
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
				Sql.ExecuteNonQuery(CommandType.Text, "delete from [MovieInfo] where "+ m_where);
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
