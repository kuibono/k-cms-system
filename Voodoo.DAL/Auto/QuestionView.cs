/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码，如需要添加方法，请创建同名类，并在该类中添加新的方法。
*生成时间：2012/1/3 2:20:22
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
	///表Question的数据操作类
	///</summary>
    public partial class QuestionView
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
        protected static List<Question> DataTableToList(DataTable dt)
        {
            List<Question> Ms = new List<Question>();
			
			for (int i = 0; i < dt.Rows.Count; i++)
            {
				Question M = new Question();
				M.ID=dt.Rows[i]["ID"].ToInt32();
				M.ClassID=dt.Rows[i]["ClassID"].ToInt32();
				M.ZtID=dt.Rows[i]["ZtID"].ToInt32();
				M.Title=dt.Rows[i]["Title"].ToString();
				M.Content=dt.Rows[i]["Content"].ToString();
				M.UserID=dt.Rows[i]["UserID"].ToInt32();
				M.UserName=dt.Rows[i]["UserName"].ToString();
				M.AskTime=dt.Rows[i]["AskTime"].ToDateTime();
				M.ClickCount=dt.Rows[i]["ClickCount"].ToInt32();
				
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
		public static void Insert(Question M)
        {
            IDbHelper Sql = GetHelper();
            StringBuilder sb = new StringBuilder();			
			
			sb.Append("insert into [Question]([ClassID],[ZtID],[Title],[Content],[UserID],[UserName],[AskTime],[ClickCount]) values(");
			sb.Append(M.ClassID.ToS());
			sb.Append(",");	
			sb.Append(M.ZtID.ToS());
			sb.Append(",");	
			sb.Append("N'"+M.Title+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Content+"'");
			sb.Append(",");	
			sb.Append(M.UserID.ToS());
			sb.Append(",");	
			sb.Append("N'"+M.UserName+"'");
			sb.Append(",");	
			sb.Append("N'"+M.AskTime+"'");
			sb.Append(",");	
			sb.Append(M.ClickCount.ToS());
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
				sb.Append(";select max(ID) from Question");	
			}
			if(DataBase.CmsDbType==DataBase.DbType.Oracle)
			{
				sb.Append(";select LAST_INSERT_ID()");	
			}
			

            M.ID=Sql.ExecuteScalar(CommandType.Text, sb.ToString()).ToInt32();
        }
		
		#endregion			
		
		#region Update将修改过的实体修改到数据库
		/// <summary>
        /// 将修改过的实体修改到数据库
        /// </summary>
        /// <param name="M">赋值后的实体</param>
        /// <returns></returns>
		public static int Update(Question M)
        {
            IDbHelper Sql = GetHelper();
			StringBuilder sb = new StringBuilder();
            sb.Append("update [Question] set ");
			
			sb.Append("[ClassID]="+M.ClassID.ToS());
			sb.Append(",");
			sb.Append("[ZtID]="+M.ZtID.ToS());
			sb.Append(",");
			sb.Append("[Title]=N'"+M.Title+"'");
			sb.Append(",");
			sb.Append("[Content]=N'"+M.Content+"'");
			sb.Append(",");
			sb.Append("[UserID]="+M.UserID.ToS());
			sb.Append(",");
			sb.Append("[UserName]=N'"+M.UserName+"'");
			sb.Append(",");
			sb.Append("[AskTime]=N'"+M.AskTime+"'");
			sb.Append(",");
			sb.Append("[ClickCount]="+M.ClickCount.ToS());
			
			sb.Append(" where ID='" + M.ID + "'");
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
        public static void Update(List<Question> Ms)
        {
            foreach (Question M in Ms)
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
		public static Question GetModelByID(string id)
		{
			IDbHelper Sql = GetHelper();
			Question M = new Question();
			DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select [ID],[ClassID],[ZtID],[Title],[Content],[UserID],[UserName],[AskTime],[ClickCount] from [Question] where ID=" + id.ToString(), true);
			if (!Rs.Read())
			{
					M.ID=0;
			}
			else
			{
				M.ID=Rs["ID"].ToInt32();
				M.ClassID=Rs["ClassID"].ToInt32();
				M.ZtID=Rs["ZtID"].ToInt32();
				M.Title=Rs["Title"].ToString();
				M.Content=Rs["Content"].ToString();
				M.UserID=Rs["UserID"].ToInt32();
				M.UserName=Rs["UserName"].ToString();
				M.AskTime=Rs["AskTime"].ToDateTime();
				M.ClickCount=Rs["ClickCount"].ToInt32();
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
		public static Question Find(string m_where)
		{
			IDbHelper Sql = GetHelper();
            Question M = new Question();
            DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select [ID],[ClassID],[ZtID],[Title],[Content],[UserID],[UserName],[AskTime],[ClickCount] from [Question] where " + m_where, true);
			if (!Rs.Read())
            {
					M.ID=0;
            }
			else
			{
				M.ID=Rs["ID"].ToInt32();
				M.ClassID=Rs["ClassID"].ToInt32();
				M.ZtID=Rs["ZtID"].ToInt32();
				M.Title=Rs["Title"].ToString();
				M.Content=Rs["Content"].ToString();
				M.UserID=Rs["UserID"].ToInt32();
				M.UserName=Rs["UserName"].ToString();
				M.AskTime=Rs["AskTime"].ToDateTime();
				M.ClickCount=Rs["ClickCount"].ToInt32();
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
            return Sql.ExecuteDataTable(CommandType.Text, "select [ID],[ClassID],[ZtID],[Title],[Content],[UserID],[UserName],[AskTime],[ClickCount] from [Question] where "+ m_where);
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
            DataTable dt = Sql.ExecuteDataTable(CommandType.Text, "select top "+ top.ToString() +"  [ID],[ClassID],[ZtID],[Title],[Content],[UserID],[UserName],[AskTime],[ClickCount] from [Question] where "+ m_where);
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
			return Convert.ToInt32(Sql.ExecuteScalar(CommandType.Text,"select count(0) from [Question] where "+m_where));
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
            sd = Sql.ExecuteReader(CommandType.Text, "select 1 from [Question] where " + m_where, true);
            if (sd.Read())
            {
                returnValue = true;
            }
            sd.Close();
            sd.Dispose();
            return returnValue;
			
			
		}
		#endregion
		
		#region List<Question>获取符合条件记录的实体列表,慎用！！！！
		/// <summary>
        /// List<Question>获取符合条件记录的实体列表,慎用！！！！
        /// </summary>
        /// <param name="m_where">条件语句，不包含“where”</param>
        /// <returns></returns>
		public static List<Question> GetModelList(string m_where)
		{	
			return DataTableToList(getTable(m_where));
		}
		public static List<Question> GetModelList(string m_where,int top)
		{	
			return DataTableToList(getTable(m_where, top));
		}
		public static List<Question> GetModelList()
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
				Sql.ExecuteNonQuery(CommandType.Text, "delete from [Question] where "+ m_where);
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
			return Del("ID="+ID.ToString());
		}
		#endregion
		
		
	}
	
	
}
