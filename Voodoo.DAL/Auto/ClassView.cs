/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码，如需要添加方法，请创建同名类，并在该类中添加新的方法。
*生成时间：2011-11-8 15:30:46
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
	///表Class的数据操作类
	///</summary>
    public partial class ClassView
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
        protected static List<Class> DataTableToList(DataTable dt)
        {
            List<Class> Ms = new List<Class>();
			
			for (int i = 0; i < dt.Rows.Count; i++)
            {
				Class M = new Class();
					
					M.ID=dt.Rows[i]["ID"].ToInt32();
					
					
					M.ClassName=dt.Rows[i]["ClassName"].ToString();
					
					
					M.Alter=dt.Rows[i]["Alter"].ToString();
					
					
					M.ParentID=dt.Rows[i]["ParentID"].ToInt32();
					
					
					M.IsLeafClass=dt.Rows[i]["IsLeafClass"].ToBoolean();
					
					
					M.ClassForder=dt.Rows[i]["ClassForder"].ToString();
					
					
					M.ModelID=dt.Rows[i]["ModelID"].ToInt32();
					
					
					M.ClassKeywords=dt.Rows[i]["ClassKeywords"].ToString();
					
					
					M.ClassDescription=dt.Rows[i]["ClassDescription"].ToString();
					
					
					M.ClassICON=dt.Rows[i]["ClassICON"].ToString();
					
					
					M.ShowInNav=dt.Rows[i]["ShowInNav"].ToBoolean();
					
					
					M.NavIndex=dt.Rows[i]["NavIndex"].ToInt32();
					
					
					M.VisitRole=dt.Rows[i]["VisitRole"].ToString();
					
					
					M.EnablePost=dt.Rows[i]["EnablePost"].ToBoolean();
					
					
					M.EnableVCode=dt.Rows[i]["EnableVCode"].ToBoolean();
					
					
					M.PostRoles=dt.Rows[i]["PostRoles"].ToString();
					
					
					M.PostcreateList=dt.Rows[i]["PostcreateList"].ToInt32();
					
					
					M.PostAddCent=dt.Rows[i]["PostAddCent"].ToInt32();
					
					
					M.NeedAudit=dt.Rows[i]["NeedAudit"].ToBoolean();
					
					
					M.PostManagement=dt.Rows[i]["PostManagement"].ToInt32();
					
					
					M.EditcreateList=dt.Rows[i]["EditcreateList"].ToInt32();
					
					
					M.EnableSameTitle=dt.Rows[i]["EnableSameTitle"].ToBoolean();
					
					
					M.AutoAudt=dt.Rows[i]["AutoAudt"].ToBoolean();
					
					
					M.EnableReply=dt.Rows[i]["EnableReply"].ToBoolean();
					
					
					M.ReplyNeedAudit=dt.Rows[i]["ReplyNeedAudit"].ToBoolean();
					
					
					M.ListModel=dt.Rows[i]["ListModel"].ToInt32();
					
					
					M.ContentModel=dt.Rows[i]["ContentModel"].ToInt32();
					
					
					M.ReplyModel=dt.Rows[i]["ReplyModel"].ToInt32();
					
					
					M.SearchModel=dt.Rows[i]["SearchModel"].ToInt32();
					
					
					M.ClassPageMode=dt.Rows[i]["ClassPageMode"].ToInt32();
					
					
					M.ContentPageMode=dt.Rows[i]["ContentPageMode"].ToInt32();
					
					
					M.ManagementOrder=dt.Rows[i]["ManagementOrder"].ToString();
					
					
					M.ListPageOrder=dt.Rows[i]["ListPageOrder"].ToString();
					
					
					M.ClassPageExtName=dt.Rows[i]["ClassPageExtName"].ToString();
					
					
					M.ListPageSize=dt.Rows[i]["ListPageSize"].ToInt32();
					
					
					M.ContentPageForder=dt.Rows[i]["ContentPageForder"].ToString();
					
					
					M.ContentPageExtName=dt.Rows[i]["ContentPageExtName"].ToString();
					
					
					M.ContentPageNameMode=dt.Rows[i]["ContentPageNameMode"].ToInt32();
					
					
					M.ContentPageDirMode=dt.Rows[i]["ContentPageDirMode"].ToString();
					
				
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
		public static void Insert(Class M)
        {
            IDbHelper Sql = GetHelper();
            StringBuilder sb = new StringBuilder();			
			
			sb.Append("insert into Class(ClassName,[Alter],ParentID,IsLeafClass,ClassForder,ModelID,ClassKeywords,ClassDescription,ClassICON,ShowInNav,NavIndex,VisitRole,EnablePost,EnableVCode,PostRoles,PostcreateList,PostAddCent,NeedAudit,PostManagement,EditcreateList,EnableSameTitle,AutoAudt,EnableReply,ReplyNeedAudit,ListModel,ContentModel,ReplyModel,SearchModel,ClassPageMode,ContentPageMode,ManagementOrder,ListPageOrder,ClassPageExtName,ListPageSize,ContentPageForder,ContentPageExtName,ContentPageNameMode,ContentPageDirMode) values(");
			sb.Append("'"+M.ClassName+"'");
			sb.Append(",");	
			sb.Append("'"+M.Alter+"'");
			sb.Append(",");	
			sb.Append(M.ParentID.ToS());
			sb.Append(",");	
			sb.Append(M.IsLeafClass.ToS());
			sb.Append(",");	
			sb.Append("'"+M.ClassForder+"'");
			sb.Append(",");	
			sb.Append(M.ModelID.ToS());
			sb.Append(",");	
			sb.Append("'"+M.ClassKeywords+"'");
			sb.Append(",");	
			sb.Append("'"+M.ClassDescription+"'");
			sb.Append(",");	
			sb.Append("'"+M.ClassICON+"'");
			sb.Append(",");	
			sb.Append(M.ShowInNav.ToS());
			sb.Append(",");	
			sb.Append(M.NavIndex.ToS());
			sb.Append(",");	
			sb.Append("'"+M.VisitRole+"'");
			sb.Append(",");	
			sb.Append(M.EnablePost.ToS());
			sb.Append(",");	
			sb.Append(M.EnableVCode.ToS());
			sb.Append(",");	
			sb.Append("'"+M.PostRoles+"'");
			sb.Append(",");	
			sb.Append(M.PostcreateList.ToS());
			sb.Append(",");	
			sb.Append(M.PostAddCent.ToS());
			sb.Append(",");	
			sb.Append(M.NeedAudit.ToS());
			sb.Append(",");	
			sb.Append(M.PostManagement.ToS());
			sb.Append(",");	
			sb.Append(M.EditcreateList.ToS());
			sb.Append(",");	
			sb.Append(M.EnableSameTitle.ToS());
			sb.Append(",");	
			sb.Append(M.AutoAudt.ToS());
			sb.Append(",");	
			sb.Append(M.EnableReply.ToS());
			sb.Append(",");	
			sb.Append(M.ReplyNeedAudit.ToS());
			sb.Append(",");	
			sb.Append(M.ListModel.ToS());
			sb.Append(",");	
			sb.Append(M.ContentModel.ToS());
			sb.Append(",");	
			sb.Append(M.ReplyModel.ToS());
			sb.Append(",");	
			sb.Append(M.SearchModel.ToS());
			sb.Append(",");	
			sb.Append(M.ClassPageMode.ToS());
			sb.Append(",");	
			sb.Append(M.ContentPageMode.ToS());
			sb.Append(",");	
			sb.Append("'"+M.ManagementOrder+"'");
			sb.Append(",");	
			sb.Append("'"+M.ListPageOrder+"'");
			sb.Append(",");	
			sb.Append("'"+M.ClassPageExtName+"'");
			sb.Append(",");	
			sb.Append(M.ListPageSize.ToS());
			sb.Append(",");	
			sb.Append("'"+M.ContentPageForder+"'");
			sb.Append(",");	
			sb.Append("'"+M.ContentPageExtName+"'");
			sb.Append(",");	
			sb.Append(M.ContentPageNameMode.ToS());
			sb.Append(",");	
			sb.Append("'"+M.ContentPageDirMode+"'");
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
				sb.Append(";select max(ID) from Class");	
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
		public static int Update(Class M)
        {
            IDbHelper Sql = GetHelper();
			StringBuilder sb = new StringBuilder();
            sb.Append("update Class set ");
			
			sb.Append("ClassName='"+M.ClassName+"'");
			sb.Append(",");
			sb.Append("[Alter]='"+M.Alter+"'");
			sb.Append(",");
			sb.Append("ParentID="+M.ParentID.ToS());
			sb.Append(",");
			sb.Append("IsLeafClass="+M.IsLeafClass.ToS());
			sb.Append(",");
			sb.Append("ClassForder='"+M.ClassForder+"'");
			sb.Append(",");
			sb.Append("ModelID="+M.ModelID.ToS());
			sb.Append(",");
			sb.Append("ClassKeywords='"+M.ClassKeywords+"'");
			sb.Append(",");
			sb.Append("ClassDescription='"+M.ClassDescription+"'");
			sb.Append(",");
			sb.Append("ClassICON='"+M.ClassICON+"'");
			sb.Append(",");
			sb.Append("ShowInNav="+M.ShowInNav.ToS());
			sb.Append(",");
			sb.Append("NavIndex="+M.NavIndex.ToS());
			sb.Append(",");
			sb.Append("VisitRole='"+M.VisitRole+"'");
			sb.Append(",");
			sb.Append("EnablePost="+M.EnablePost.ToS());
			sb.Append(",");
			sb.Append("EnableVCode="+M.EnableVCode.ToS());
			sb.Append(",");
			sb.Append("PostRoles='"+M.PostRoles+"'");
			sb.Append(",");
			sb.Append("PostcreateList="+M.PostcreateList.ToS());
			sb.Append(",");
			sb.Append("PostAddCent="+M.PostAddCent.ToS());
			sb.Append(",");
			sb.Append("NeedAudit="+M.NeedAudit.ToS());
			sb.Append(",");
			sb.Append("PostManagement="+M.PostManagement.ToS());
			sb.Append(",");
			sb.Append("EditcreateList="+M.EditcreateList.ToS());
			sb.Append(",");
			sb.Append("EnableSameTitle="+M.EnableSameTitle.ToS());
			sb.Append(",");
			sb.Append("AutoAudt="+M.AutoAudt.ToS());
			sb.Append(",");
			sb.Append("EnableReply="+M.EnableReply.ToS());
			sb.Append(",");
			sb.Append("ReplyNeedAudit="+M.ReplyNeedAudit.ToS());
			sb.Append(",");
			sb.Append("ListModel="+M.ListModel.ToS());
			sb.Append(",");
			sb.Append("ContentModel="+M.ContentModel.ToS());
			sb.Append(",");
			sb.Append("ReplyModel="+M.ReplyModel.ToS());
			sb.Append(",");
			sb.Append("SearchModel="+M.SearchModel.ToS());
			sb.Append(",");
			sb.Append("ClassPageMode="+M.ClassPageMode.ToS());
			sb.Append(",");
			sb.Append("ContentPageMode="+M.ContentPageMode.ToS());
			sb.Append(",");
			sb.Append("ManagementOrder='"+M.ManagementOrder+"'");
			sb.Append(",");
			sb.Append("ListPageOrder='"+M.ListPageOrder+"'");
			sb.Append(",");
			sb.Append("ClassPageExtName='"+M.ClassPageExtName+"'");
			sb.Append(",");
			sb.Append("ListPageSize="+M.ListPageSize.ToS());
			sb.Append(",");
			sb.Append("ContentPageForder='"+M.ContentPageForder+"'");
			sb.Append(",");
			sb.Append("ContentPageExtName='"+M.ContentPageExtName+"'");
			sb.Append(",");
			sb.Append("ContentPageNameMode="+M.ContentPageNameMode.ToS());
			sb.Append(",");
			sb.Append("ContentPageDirMode='"+M.ContentPageDirMode+"'");
			
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
        public static void Update(List<Class> Ms)
        {
            foreach (Class M in Ms)
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
		public static Class GetModelByID(string id)
		{
			IDbHelper Sql = GetHelper();
			Class M = new Class();
			DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select ID,[ClassName],[Alter],ParentID,IsLeafClass,ClassForder,ModelID,ClassKeywords,ClassDescription,ClassICON,ShowInNav,NavIndex,VisitRole,EnablePost,EnableVCode,PostRoles,PostcreateList,PostAddCent,NeedAudit,PostManagement,EditcreateList,EnableSameTitle,AutoAudt,EnableReply,ReplyNeedAudit,ListModel,ContentModel,ReplyModel,SearchModel,ClassPageMode,ContentPageMode,ManagementOrder,ListPageOrder,ClassPageExtName,ListPageSize,ContentPageForder,ContentPageExtName,ContentPageNameMode,ContentPageDirMode from Class where ID='" + id.ToString()+"'", true);
			if (!Rs.Read())
			{
					M.ID=0;
			}
			else
			{
				M.ID=Rs["ID"].ToInt32();
				M.ClassName=Rs["ClassName"].ToString();
				M.Alter=Rs["Alter"].ToString();
				M.ParentID=Rs["ParentID"].ToInt32();
				M.IsLeafClass=Rs["IsLeafClass"].ToBoolean();
				M.ClassForder=Rs["ClassForder"].ToString();
				M.ModelID=Rs["ModelID"].ToInt32();
				M.ClassKeywords=Rs["ClassKeywords"].ToString();
				M.ClassDescription=Rs["ClassDescription"].ToString();
				M.ClassICON=Rs["ClassICON"].ToString();
				M.ShowInNav=Rs["ShowInNav"].ToBoolean();
				M.NavIndex=Rs["NavIndex"].ToInt32();
				M.VisitRole=Rs["VisitRole"].ToString();
				M.EnablePost=Rs["EnablePost"].ToBoolean();
				M.EnableVCode=Rs["EnableVCode"].ToBoolean();
				M.PostRoles=Rs["PostRoles"].ToString();
				M.PostcreateList=Rs["PostcreateList"].ToInt32();
				M.PostAddCent=Rs["PostAddCent"].ToInt32();
				M.NeedAudit=Rs["NeedAudit"].ToBoolean();
				M.PostManagement=Rs["PostManagement"].ToInt32();
				M.EditcreateList=Rs["EditcreateList"].ToInt32();
				M.EnableSameTitle=Rs["EnableSameTitle"].ToBoolean();
				M.AutoAudt=Rs["AutoAudt"].ToBoolean();
				M.EnableReply=Rs["EnableReply"].ToBoolean();
				M.ReplyNeedAudit=Rs["ReplyNeedAudit"].ToBoolean();
				M.ListModel=Rs["ListModel"].ToInt32();
				M.ContentModel=Rs["ContentModel"].ToInt32();
				M.ReplyModel=Rs["ReplyModel"].ToInt32();
				M.SearchModel=Rs["SearchModel"].ToInt32();
				M.ClassPageMode=Rs["ClassPageMode"].ToInt32();
				M.ContentPageMode=Rs["ContentPageMode"].ToInt32();
				M.ManagementOrder=Rs["ManagementOrder"].ToString();
				M.ListPageOrder=Rs["ListPageOrder"].ToString();
				M.ClassPageExtName=Rs["ClassPageExtName"].ToString();
				M.ListPageSize=Rs["ListPageSize"].ToInt32();
				M.ContentPageForder=Rs["ContentPageForder"].ToString();
				M.ContentPageExtName=Rs["ContentPageExtName"].ToString();
				M.ContentPageNameMode=Rs["ContentPageNameMode"].ToInt32();
				M.ContentPageDirMode=Rs["ContentPageDirMode"].ToString();
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
		public static Class Find(string m_where)
		{
			IDbHelper Sql = GetHelper();
            Class M = new Class();
            DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select ID,[ClassName],Alter,ParentID,IsLeafClass,ClassForder,ModelID,ClassKeywords,ClassDescription,ClassICON,ShowInNav,NavIndex,VisitRole,EnablePost,EnableVCode,PostRoles,PostcreateList,PostAddCent,NeedAudit,PostManagement,EditcreateList,EnableSameTitle,AutoAudt,EnableReply,ReplyNeedAudit,ListModel,ContentModel,ReplyModel,SearchModel,ClassPageMode,ContentPageMode,ManagementOrder,ListPageOrder,ClassPageExtName,ListPageSize,ContentPageForder,ContentPageExtName,ContentPageNameMode,ContentPageDirMode from Class where " + m_where, true);
			if (!Rs.Read())
            {
					M.ID=0;
            }
			else
			{
				M.ID=Rs["ID"].ToInt32();
				M.ClassName=Rs["ClassName"].ToString();
				M.Alter=Rs["Alter"].ToString();
				M.ParentID=Rs["ParentID"].ToInt32();
				M.IsLeafClass=Rs["IsLeafClass"].ToBoolean();
				M.ClassForder=Rs["ClassForder"].ToString();
				M.ModelID=Rs["ModelID"].ToInt32();
				M.ClassKeywords=Rs["ClassKeywords"].ToString();
				M.ClassDescription=Rs["ClassDescription"].ToString();
				M.ClassICON=Rs["ClassICON"].ToString();
				M.ShowInNav=Rs["ShowInNav"].ToBoolean();
				M.NavIndex=Rs["NavIndex"].ToInt32();
				M.VisitRole=Rs["VisitRole"].ToString();
				M.EnablePost=Rs["EnablePost"].ToBoolean();
				M.EnableVCode=Rs["EnableVCode"].ToBoolean();
				M.PostRoles=Rs["PostRoles"].ToString();
				M.PostcreateList=Rs["PostcreateList"].ToInt32();
				M.PostAddCent=Rs["PostAddCent"].ToInt32();
				M.NeedAudit=Rs["NeedAudit"].ToBoolean();
				M.PostManagement=Rs["PostManagement"].ToInt32();
				M.EditcreateList=Rs["EditcreateList"].ToInt32();
				M.EnableSameTitle=Rs["EnableSameTitle"].ToBoolean();
				M.AutoAudt=Rs["AutoAudt"].ToBoolean();
				M.EnableReply=Rs["EnableReply"].ToBoolean();
				M.ReplyNeedAudit=Rs["ReplyNeedAudit"].ToBoolean();
				M.ListModel=Rs["ListModel"].ToInt32();
				M.ContentModel=Rs["ContentModel"].ToInt32();
				M.ReplyModel=Rs["ReplyModel"].ToInt32();
				M.SearchModel=Rs["SearchModel"].ToInt32();
				M.ClassPageMode=Rs["ClassPageMode"].ToInt32();
				M.ContentPageMode=Rs["ContentPageMode"].ToInt32();
				M.ManagementOrder=Rs["ManagementOrder"].ToString();
				M.ListPageOrder=Rs["ListPageOrder"].ToString();
				M.ClassPageExtName=Rs["ClassPageExtName"].ToString();
				M.ListPageSize=Rs["ListPageSize"].ToInt32();
				M.ContentPageForder=Rs["ContentPageForder"].ToString();
				M.ContentPageExtName=Rs["ContentPageExtName"].ToString();
				M.ContentPageNameMode=Rs["ContentPageNameMode"].ToInt32();
				M.ContentPageDirMode=Rs["ContentPageDirMode"].ToString();
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
            return Sql.ExecuteDataTable(CommandType.Text, "select ID,ClassName,[Alter],ParentID,IsLeafClass,ClassForder,ModelID,ClassKeywords,ClassDescription,ClassICON,ShowInNav,NavIndex,VisitRole,EnablePost,EnableVCode,PostRoles,PostcreateList,PostAddCent,NeedAudit,PostManagement,EditcreateList,EnableSameTitle,AutoAudt,EnableReply,ReplyNeedAudit,ListModel,ContentModel,ReplyModel,SearchModel,ClassPageMode,ContentPageMode,ManagementOrder,ListPageOrder,ClassPageExtName,ListPageSize,ContentPageForder,ContentPageExtName,ContentPageNameMode,ContentPageDirMode from [Class] where "+ m_where);
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
            DataTable dt = Sql.ExecuteDataTable(CommandType.Text, "select top "+ top.ToString() +"  ID,ClassName,[Alter],ParentID,IsLeafClass,ClassForder,ModelID,ClassKeywords,ClassDescription,ClassICON,ShowInNav,NavIndex,VisitRole,EnablePost,EnableVCode,PostRoles,PostcreateList,PostAddCent,NeedAudit,PostManagement,EditcreateList,EnableSameTitle,AutoAudt,EnableReply,ReplyNeedAudit,ListModel,ContentModel,ReplyModel,SearchModel,ClassPageMode,ContentPageMode,ManagementOrder,ListPageOrder,ClassPageExtName,ListPageSize,ContentPageForder,ContentPageExtName,ContentPageNameMode,ContentPageDirMode from [Class] where "+ m_where);
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
			return Convert.ToInt32(Sql.ExecuteScalar(CommandType.Text,"select count(0) from Class where "+m_where));
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
            sd = Sql.ExecuteReader(CommandType.Text, "select 1 from Class where " + m_where, true);
            if (sd.Read())
            {
                returnValue = true;
            }
            sd.Close();
            sd.Dispose();
            return returnValue;
			
			
		}
		#endregion
		
		#region List<Class>获取符合条件记录的实体列表,慎用！！！！
		/// <summary>
        /// List<Class>获取符合条件记录的实体列表,慎用！！！！
        /// </summary>
        /// <param name="m_where">条件语句，不包含“where”</param>
        /// <returns></returns>
		public static List<Class> GetModelList(string m_where)
		{	
			return DataTableToList(getTable(m_where));
		}
		public static List<Class> GetModelList(string m_where,int top)
		{	
			return DataTableToList(getTable(m_where, top));
		}
		public static List<Class> GetModelList()
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
				Sql.ExecuteNonQuery(CommandType.Text, "delete from Class where "+ m_where);
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
