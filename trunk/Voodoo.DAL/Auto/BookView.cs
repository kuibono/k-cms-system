/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码，如需要添加方法，请创建同名类，并在该类中添加新的方法。
*生成时间：2012/1/3 2:20:21
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
	///表Book的数据操作类
	///</summary>
    public partial class BookView
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
        protected static List<Book> DataTableToList(DataTable dt)
        {
            List<Book> Ms = new List<Book>();
			
			for (int i = 0; i < dt.Rows.Count; i++)
            {
				Book M = new Book();
				M.ID=dt.Rows[i]["ID"].ToInt32();
				M.ClassID=dt.Rows[i]["ClassID"].ToInt32();
				M.ClassName=dt.Rows[i]["ClassName"].ToString();
				M.ZtID=dt.Rows[i]["ZtID"].ToInt32();
				M.ZtName=dt.Rows[i]["ZtName"].ToString();
				M.Title=dt.Rows[i]["Title"].ToString();
				M.Author=dt.Rows[i]["Author"].ToString();
				M.Intro=dt.Rows[i]["Intro"].ToString();
				M.Length=dt.Rows[i]["Length"].ToInt64();
				M.ReplyCount=dt.Rows[i]["ReplyCount"].ToInt64();
				M.ClickCount=dt.Rows[i]["ClickCount"].ToInt64();
				M.Addtime=dt.Rows[i]["Addtime"].ToDateTime();
				M.Status=dt.Rows[i]["Status"].ToByte();
				M.IsVip=dt.Rows[i]["IsVip"].ToBoolean();
				M.FaceImage=dt.Rows[i]["FaceImage"].ToString();
				M.SaveCount=dt.Rows[i]["SaveCount"].ToInt64();
				M.Enable=dt.Rows[i]["Enable"].ToBoolean();
				M.IsFirstPost=dt.Rows[i]["IsFirstPost"].ToBoolean();
				M.LastChapterID=dt.Rows[i]["LastChapterID"].ToInt64();
				M.LastChapterTitle=dt.Rows[i]["LastChapterTitle"].ToString();
				M.UpdateTime=dt.Rows[i]["UpdateTime"].ToDateTime();
				M.LastVipChapterID=dt.Rows[i]["LastVipChapterID"].ToInt64();
				M.LastVipChapterTitle=dt.Rows[i]["LastVipChapterTitle"].ToString();
				M.VipUpdateTime=dt.Rows[i]["VipUpdateTime"].ToDateTime();
				M.CorpusID=dt.Rows[i]["CorpusID"].ToInt32();
				M.CorpusTitle=dt.Rows[i]["CorpusTitle"].ToString();
				
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
		public static void Insert(Book M)
        {
            IDbHelper Sql = GetHelper();
            StringBuilder sb = new StringBuilder();			
			
			sb.Append("insert into [Book]([ClassID],[ClassName],[ZtID],[ZtName],[Title],[Author],[Intro],[Length],[ReplyCount],[ClickCount],[Addtime],[Status],[IsVip],[FaceImage],[SaveCount],[Enable],[IsFirstPost],[LastChapterID],[LastChapterTitle],[UpdateTime],[LastVipChapterID],[LastVipChapterTitle],[VipUpdateTime],[CorpusID],[CorpusTitle]) values(");
			sb.Append(M.ClassID.ToS());
			sb.Append(",");	
			sb.Append("N'"+M.ClassName+"'");
			sb.Append(",");	
			sb.Append(M.ZtID.ToS());
			sb.Append(",");	
			sb.Append("N'"+M.ZtName+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Title+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Author+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Intro+"'");
			sb.Append(",");	
			sb.Append(M.Length.ToS());
			sb.Append(",");	
			sb.Append(M.ReplyCount.ToS());
			sb.Append(",");	
			sb.Append(M.ClickCount.ToS());
			sb.Append(",");	
			sb.Append("N'"+M.Addtime+"'");
			sb.Append(",");	
			sb.Append(M.Status.ToS());
			sb.Append(",");	
			sb.Append(M.IsVip.ToS());
			sb.Append(",");	
			sb.Append("N'"+M.FaceImage+"'");
			sb.Append(",");	
			sb.Append(M.SaveCount.ToS());
			sb.Append(",");	
			sb.Append(M.Enable.ToS());
			sb.Append(",");	
			sb.Append(M.IsFirstPost.ToS());
			sb.Append(",");	
			sb.Append(M.LastChapterID.ToS());
			sb.Append(",");	
			sb.Append("N'"+M.LastChapterTitle+"'");
			sb.Append(",");	
			sb.Append("N'"+M.UpdateTime+"'");
			sb.Append(",");	
			sb.Append(M.LastVipChapterID.ToS());
			sb.Append(",");	
			sb.Append("N'"+M.LastVipChapterTitle+"'");
			sb.Append(",");	
			sb.Append("N'"+M.VipUpdateTime+"'");
			sb.Append(",");	
			sb.Append(M.CorpusID.ToS());
			sb.Append(",");	
			sb.Append("N'"+M.CorpusTitle+"'");
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
				sb.Append(";select max(ID) from Book");	
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
		public static int Update(Book M)
        {
            IDbHelper Sql = GetHelper();
			StringBuilder sb = new StringBuilder();
            sb.Append("update [Book] set ");
			
			sb.Append("[ClassID]="+M.ClassID.ToS());
			sb.Append(",");
			sb.Append("[ClassName]=N'"+M.ClassName+"'");
			sb.Append(",");
			sb.Append("[ZtID]="+M.ZtID.ToS());
			sb.Append(",");
			sb.Append("[ZtName]=N'"+M.ZtName+"'");
			sb.Append(",");
			sb.Append("[Title]=N'"+M.Title+"'");
			sb.Append(",");
			sb.Append("[Author]=N'"+M.Author+"'");
			sb.Append(",");
			sb.Append("[Intro]=N'"+M.Intro+"'");
			sb.Append(",");
			sb.Append("[Length]="+M.Length.ToS());
			sb.Append(",");
			sb.Append("[ReplyCount]="+M.ReplyCount.ToS());
			sb.Append(",");
			sb.Append("[ClickCount]="+M.ClickCount.ToS());
			sb.Append(",");
			sb.Append("[Addtime]=N'"+M.Addtime+"'");
			sb.Append(",");
			sb.Append("[Status]="+M.Status.ToS());
			sb.Append(",");
			sb.Append("[IsVip]="+M.IsVip.ToS());
			sb.Append(",");
			sb.Append("[FaceImage]=N'"+M.FaceImage+"'");
			sb.Append(",");
			sb.Append("[SaveCount]="+M.SaveCount.ToS());
			sb.Append(",");
			sb.Append("[Enable]="+M.Enable.ToS());
			sb.Append(",");
			sb.Append("[IsFirstPost]="+M.IsFirstPost.ToS());
			sb.Append(",");
			sb.Append("[LastChapterID]="+M.LastChapterID.ToS());
			sb.Append(",");
			sb.Append("[LastChapterTitle]=N'"+M.LastChapterTitle+"'");
			sb.Append(",");
			sb.Append("[UpdateTime]=N'"+M.UpdateTime+"'");
			sb.Append(",");
			sb.Append("[LastVipChapterID]="+M.LastVipChapterID.ToS());
			sb.Append(",");
			sb.Append("[LastVipChapterTitle]=N'"+M.LastVipChapterTitle+"'");
			sb.Append(",");
			sb.Append("[VipUpdateTime]=N'"+M.VipUpdateTime+"'");
			sb.Append(",");
			sb.Append("[CorpusID]="+M.CorpusID.ToS());
			sb.Append(",");
			sb.Append("[CorpusTitle]=N'"+M.CorpusTitle+"'");
			
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
        public static void Update(List<Book> Ms)
        {
            foreach (Book M in Ms)
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
		public static Book GetModelByID(string id)
		{
			IDbHelper Sql = GetHelper();
			Book M = new Book();
			DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select [ID],[ClassID],[ClassName],[ZtID],[ZtName],[Title],[Author],[Intro],[Length],[ReplyCount],[ClickCount],[Addtime],[Status],[IsVip],[FaceImage],[SaveCount],[Enable],[IsFirstPost],[LastChapterID],[LastChapterTitle],[UpdateTime],[LastVipChapterID],[LastVipChapterTitle],[VipUpdateTime],[CorpusID],[CorpusTitle] from [Book] where ID=" + id.ToString(), true);
			if (!Rs.Read())
			{
					M.ID=0;
			}
			else
			{
				M.ID=Rs["ID"].ToInt32();
				M.ClassID=Rs["ClassID"].ToInt32();
				M.ClassName=Rs["ClassName"].ToString();
				M.ZtID=Rs["ZtID"].ToInt32();
				M.ZtName=Rs["ZtName"].ToString();
				M.Title=Rs["Title"].ToString();
				M.Author=Rs["Author"].ToString();
				M.Intro=Rs["Intro"].ToString();
				M.Length=Rs["Length"].ToInt64();
				M.ReplyCount=Rs["ReplyCount"].ToInt64();
				M.ClickCount=Rs["ClickCount"].ToInt64();
				M.Addtime=Rs["Addtime"].ToDateTime();
				M.Status=Rs["Status"].ToByte();
				M.IsVip=Rs["IsVip"].ToBoolean();
				M.FaceImage=Rs["FaceImage"].ToString();
				M.SaveCount=Rs["SaveCount"].ToInt64();
				M.Enable=Rs["Enable"].ToBoolean();
				M.IsFirstPost=Rs["IsFirstPost"].ToBoolean();
				M.LastChapterID=Rs["LastChapterID"].ToInt64();
				M.LastChapterTitle=Rs["LastChapterTitle"].ToString();
				M.UpdateTime=Rs["UpdateTime"].ToDateTime();
				M.LastVipChapterID=Rs["LastVipChapterID"].ToInt64();
				M.LastVipChapterTitle=Rs["LastVipChapterTitle"].ToString();
				M.VipUpdateTime=Rs["VipUpdateTime"].ToDateTime();
				M.CorpusID=Rs["CorpusID"].ToInt32();
				M.CorpusTitle=Rs["CorpusTitle"].ToString();
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
		public static Book Find(string m_where)
		{
			IDbHelper Sql = GetHelper();
            Book M = new Book();
            DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select [ID],[ClassID],[ClassName],[ZtID],[ZtName],[Title],[Author],[Intro],[Length],[ReplyCount],[ClickCount],[Addtime],[Status],[IsVip],[FaceImage],[SaveCount],[Enable],[IsFirstPost],[LastChapterID],[LastChapterTitle],[UpdateTime],[LastVipChapterID],[LastVipChapterTitle],[VipUpdateTime],[CorpusID],[CorpusTitle] from [Book] where " + m_where, true);
			if (!Rs.Read())
            {
					M.ID=0;
            }
			else
			{
				M.ID=Rs["ID"].ToInt32();
				M.ClassID=Rs["ClassID"].ToInt32();
				M.ClassName=Rs["ClassName"].ToString();
				M.ZtID=Rs["ZtID"].ToInt32();
				M.ZtName=Rs["ZtName"].ToString();
				M.Title=Rs["Title"].ToString();
				M.Author=Rs["Author"].ToString();
				M.Intro=Rs["Intro"].ToString();
				M.Length=Rs["Length"].ToInt64();
				M.ReplyCount=Rs["ReplyCount"].ToInt64();
				M.ClickCount=Rs["ClickCount"].ToInt64();
				M.Addtime=Rs["Addtime"].ToDateTime();
				M.Status=Rs["Status"].ToByte();
				M.IsVip=Rs["IsVip"].ToBoolean();
				M.FaceImage=Rs["FaceImage"].ToString();
				M.SaveCount=Rs["SaveCount"].ToInt64();
				M.Enable=Rs["Enable"].ToBoolean();
				M.IsFirstPost=Rs["IsFirstPost"].ToBoolean();
				M.LastChapterID=Rs["LastChapterID"].ToInt64();
				M.LastChapterTitle=Rs["LastChapterTitle"].ToString();
				M.UpdateTime=Rs["UpdateTime"].ToDateTime();
				M.LastVipChapterID=Rs["LastVipChapterID"].ToInt64();
				M.LastVipChapterTitle=Rs["LastVipChapterTitle"].ToString();
				M.VipUpdateTime=Rs["VipUpdateTime"].ToDateTime();
				M.CorpusID=Rs["CorpusID"].ToInt32();
				M.CorpusTitle=Rs["CorpusTitle"].ToString();
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
            return Sql.ExecuteDataTable(CommandType.Text, "select [ID],[ClassID],[ClassName],[ZtID],[ZtName],[Title],[Author],[Intro],[Length],[ReplyCount],[ClickCount],[Addtime],[Status],[IsVip],[FaceImage],[SaveCount],[Enable],[IsFirstPost],[LastChapterID],[LastChapterTitle],[UpdateTime],[LastVipChapterID],[LastVipChapterTitle],[VipUpdateTime],[CorpusID],[CorpusTitle] from [Book] where "+ m_where);
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
            DataTable dt = Sql.ExecuteDataTable(CommandType.Text, "select top "+ top.ToString() +"  [ID],[ClassID],[ClassName],[ZtID],[ZtName],[Title],[Author],[Intro],[Length],[ReplyCount],[ClickCount],[Addtime],[Status],[IsVip],[FaceImage],[SaveCount],[Enable],[IsFirstPost],[LastChapterID],[LastChapterTitle],[UpdateTime],[LastVipChapterID],[LastVipChapterTitle],[VipUpdateTime],[CorpusID],[CorpusTitle] from [Book] where "+ m_where);
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
			return Convert.ToInt32(Sql.ExecuteScalar(CommandType.Text,"select count(0) from [Book] where "+m_where));
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
            sd = Sql.ExecuteReader(CommandType.Text, "select 1 from [Book] where " + m_where, true);
            if (sd.Read())
            {
                returnValue = true;
            }
            sd.Close();
            sd.Dispose();
            return returnValue;
			
			
		}
		#endregion
		
		#region List<Book>获取符合条件记录的实体列表,慎用！！！！
		/// <summary>
        /// List<Book>获取符合条件记录的实体列表,慎用！！！！
        /// </summary>
        /// <param name="m_where">条件语句，不包含“where”</param>
        /// <returns></returns>
		public static List<Book> GetModelList(string m_where)
		{	
			return DataTableToList(getTable(m_where));
		}
		public static List<Book> GetModelList(string m_where,int top)
		{	
			return DataTableToList(getTable(m_where, top));
		}
		public static List<Book> GetModelList()
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
				Sql.ExecuteNonQuery(CommandType.Text, "delete from [Book] where "+ m_where);
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
