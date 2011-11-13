/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码，如需要添加方法，请创建同名类，并在该类中添加新的方法。
*生成时间：2011-11-8 15:30:47
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
	///表UserGroup的数据操作类
	///</summary>
    public partial class UserGroupView
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
        protected static List<UserGroup> DataTableToList(DataTable dt)
        {
            List<UserGroup> Ms = new List<UserGroup>();
			
			for (int i = 0; i < dt.Rows.Count; i++)
            {
				UserGroup M = new UserGroup();
					
					M.ID=dt.Rows[i]["ID"].ToInt32();
					
					
					M.GroupName=dt.Rows[i]["GroupName"].ToString();
					
					
					M.Grade=dt.Rows[i]["Grade"].ToInt32();
					
					
					M.MaxPost=dt.Rows[i]["MaxPost"].ToInt32();
					
					
					M.PostAotuAudit=dt.Rows[i]["PostAotuAudit"].ToBoolean();
					
					
					M.RegForm=dt.Rows[i]["RegForm"].ToInt32();
					
					
					M.EnableReg=dt.Rows[i]["EnableReg"].ToBoolean();
					
					
					M.RegAutoAudit=dt.Rows[i]["RegAutoAudit"].ToBoolean();
					
				
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
		public static void Insert(UserGroup M)
        {
            IDbHelper Sql = GetHelper();
            StringBuilder sb = new StringBuilder();			
			
			sb.Append("insert into UserGroup(GroupName,Grade,MaxPost,PostAotuAudit,RegForm,EnableReg,RegAutoAudit) values(");
			sb.Append("'"+M.GroupName+"'");
			sb.Append(",");	
			sb.Append(M.Grade.ToS());
			sb.Append(",");	
			sb.Append(M.MaxPost.ToS());
			sb.Append(",");	
			sb.Append(M.PostAotuAudit.ToS());
			sb.Append(",");	
			sb.Append(M.RegForm.ToS());
			sb.Append(",");	
			sb.Append(M.EnableReg.ToS());
			sb.Append(",");	
			sb.Append(M.RegAutoAudit.ToS());
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
				sb.Append(";select max(ID) from UserGroup");	
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
		public static int Update(UserGroup M)
        {
            IDbHelper Sql = GetHelper();
			StringBuilder sb = new StringBuilder();
            sb.Append("update UserGroup set ");
			
			sb.Append("GroupName='"+M.GroupName+"'");
			sb.Append(",");
			sb.Append("Grade="+M.Grade.ToS());
			sb.Append(",");
			sb.Append("MaxPost="+M.MaxPost.ToS());
			sb.Append(",");
			sb.Append("PostAotuAudit="+M.PostAotuAudit.ToS());
			sb.Append(",");
			sb.Append("RegForm="+M.RegForm.ToS());
			sb.Append(",");
			sb.Append("EnableReg="+M.EnableReg.ToS());
			sb.Append(",");
			sb.Append("RegAutoAudit="+M.RegAutoAudit.ToS());
			
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
        public static void Update(List<UserGroup> Ms)
        {
            foreach (UserGroup M in Ms)
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
		public static UserGroup GetModelByID(string id)
		{
			IDbHelper Sql = GetHelper();
			UserGroup M = new UserGroup();
			DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select ID,GroupName,Grade,MaxPost,PostAotuAudit,RegForm,EnableReg,RegAutoAudit from UserGroup where ID='" + id.ToString()+"'", true);
			if (!Rs.Read())
			{
					M.ID=0;
			}
			else
			{
				M.ID=Rs["ID"].ToInt32();
				M.GroupName=Rs["GroupName"].ToString();
				M.Grade=Rs["Grade"].ToInt32();
				M.MaxPost=Rs["MaxPost"].ToInt32();
				M.PostAotuAudit=Rs["PostAotuAudit"].ToBoolean();
				M.RegForm=Rs["RegForm"].ToInt32();
				M.EnableReg=Rs["EnableReg"].ToBoolean();
				M.RegAutoAudit=Rs["RegAutoAudit"].ToBoolean();
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
		public static UserGroup Find(string m_where)
		{
			IDbHelper Sql = GetHelper();
            UserGroup M = new UserGroup();
            DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select ID,GroupName,Grade,MaxPost,PostAotuAudit,RegForm,EnableReg,RegAutoAudit from UserGroup where " + m_where, true);
			if (!Rs.Read())
            {
					M.ID=0;
            }
			else
			{
				M.ID=Rs["ID"].ToInt32();
				M.GroupName=Rs["GroupName"].ToString();
				M.Grade=Rs["Grade"].ToInt32();
				M.MaxPost=Rs["MaxPost"].ToInt32();
				M.PostAotuAudit=Rs["PostAotuAudit"].ToBoolean();
				M.RegForm=Rs["RegForm"].ToInt32();
				M.EnableReg=Rs["EnableReg"].ToBoolean();
				M.RegAutoAudit=Rs["RegAutoAudit"].ToBoolean();
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
            return Sql.ExecuteDataTable(CommandType.Text, "select ID,GroupName,Grade,MaxPost,PostAotuAudit,RegForm,EnableReg,RegAutoAudit from UserGroup where "+ m_where);
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
            DataTable dt = Sql.ExecuteDataTable(CommandType.Text, "select top "+ top.ToString() +"  ID,GroupName,Grade,MaxPost,PostAotuAudit,RegForm,EnableReg,RegAutoAudit from UserGroup where "+ m_where);
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
			return Convert.ToInt32(Sql.ExecuteScalar(CommandType.Text,"select count(0) from UserGroup where "+m_where));
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
            sd = Sql.ExecuteReader(CommandType.Text, "select 1 from UserGroup where " + m_where, true);
            if (sd.Read())
            {
                returnValue = true;
            }
            sd.Close();
            sd.Dispose();
            return returnValue;
			
			
		}
		#endregion
		
		#region List<UserGroup>获取符合条件记录的实体列表,慎用！！！！
		/// <summary>
        /// List<UserGroup>获取符合条件记录的实体列表,慎用！！！！
        /// </summary>
        /// <param name="m_where">条件语句，不包含“where”</param>
        /// <returns></returns>
		public static List<UserGroup> GetModelList(string m_where)
		{	
			return DataTableToList(getTable(m_where));
		}
		public static List<UserGroup> GetModelList(string m_where,int top)
		{	
			return DataTableToList(getTable(m_where, top));
		}
		public static List<UserGroup> GetModelList()
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
				Sql.ExecuteNonQuery(CommandType.Text, "delete from UserGroup where "+ m_where);
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
