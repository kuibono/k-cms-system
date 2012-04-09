/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码，如需要添加方法，请创建同名类，并在该类中添加新的方法。
*生成时间：2012/4/9 20:37:00
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
	///表InfoType的数据操作类
	///</summary>
    public partial class InfoTypeView
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
        protected static List<InfoType> DataTableToList(DataTable dt)
        {
            List<InfoType> Ms = new List<InfoType>();
			
			for (int i = 0; i < dt.Rows.Count; i++)
            {
				InfoType M = new InfoType();
				M.Id=dt.Rows[i]["id"].ToInt32();
				M.TypeName=dt.Rows[i]["TypeName"].ToString();
				M.TemplateIndex=dt.Rows[i]["TemplateIndex"].ToString();
				M.TemplateList=dt.Rows[i]["TemplateList"].ToString();
				M.TemplateContent=dt.Rows[i]["TemplateContent"].ToString();
				M.Num1=dt.Rows[i]["num1"].ToString();
				M.Num2=dt.Rows[i]["num2"].ToString();
				M.Num3=dt.Rows[i]["num3"].ToString();
				M.Num4=dt.Rows[i]["num4"].ToString();
				M.Num5=dt.Rows[i]["num5"].ToString();
				M.Num6=dt.Rows[i]["num6"].ToString();
				M.Num7=dt.Rows[i]["num7"].ToString();
				M.Num8=dt.Rows[i]["num8"].ToString();
				M.Num9=dt.Rows[i]["num9"].ToString();
				M.Num10=dt.Rows[i]["num10"].ToString();
				M.Nvarchar1=dt.Rows[i]["nvarchar1"].ToString();
				M.Nvarchar2=dt.Rows[i]["nvarchar2"].ToString();
				M.Nvarchar3=dt.Rows[i]["nvarchar3"].ToString();
				M.Nvarchar4=dt.Rows[i]["nvarchar4"].ToString();
				M.Nvarchar5=dt.Rows[i]["nvarchar5"].ToString();
				M.Nvarchar6=dt.Rows[i]["nvarchar6"].ToString();
				M.Nvarchar7=dt.Rows[i]["nvarchar7"].ToString();
				M.Nvarchar8=dt.Rows[i]["nvarchar8"].ToString();
				M.Nvarchar9=dt.Rows[i]["nvarchar9"].ToString();
				M.Nvarchar10=dt.Rows[i]["nvarchar10"].ToString();
				M.Decimal1=dt.Rows[i]["decimal1"].ToString();
				M.Decimal2=dt.Rows[i]["decimal2"].ToString();
				M.Decimal3=dt.Rows[i]["decimal3"].ToString();
				M.Decimal4=dt.Rows[i]["decimal4"].ToString();
				M.Decimal5=dt.Rows[i]["decimal5"].ToString();
				M.Text1=dt.Rows[i]["text1"].ToString();
				M.Text2=dt.Rows[i]["text2"].ToString();
				M.Text3=dt.Rows[i]["text3"].ToString();
				M.Text4=dt.Rows[i]["text4"].ToString();
				M.Text5=dt.Rows[i]["text5"].ToString();
				M.Bit1=dt.Rows[i]["bit1"].ToString();
				M.Bit2=dt.Rows[i]["bit2"].ToString();
				M.Bit3=dt.Rows[i]["bit3"].ToString();
				M.Bit4=dt.Rows[i]["bit4"].ToString();
				M.Bit5=dt.Rows[i]["bit5"].ToString();
				
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
		public static void Insert(InfoType M)
        {
            IDbHelper Sql = GetHelper();
            StringBuilder sb = new StringBuilder();			
			
			sb.Append("insert into [InfoType]([TypeName],[TemplateIndex],[TemplateList],[TemplateContent],[num1],[num2],[num3],[num4],[num5],[num6],[num7],[num8],[num9],[num10],[nvarchar1],[nvarchar2],[nvarchar3],[nvarchar4],[nvarchar5],[nvarchar6],[nvarchar7],[nvarchar8],[nvarchar9],[nvarchar10],[decimal1],[decimal2],[decimal3],[decimal4],[decimal5],[text1],[text2],[text3],[text4],[text5],[bit1],[bit2],[bit3],[bit4],[bit5]) values(");
			sb.Append("N'"+M.TypeName+"'");
			sb.Append(",");	
			sb.Append("N'"+M.TemplateIndex+"'");
			sb.Append(",");	
			sb.Append("N'"+M.TemplateList+"'");
			sb.Append(",");	
			sb.Append("N'"+M.TemplateContent+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Num1+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Num2+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Num3+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Num4+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Num5+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Num6+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Num7+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Num8+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Num9+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Num10+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Nvarchar1+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Nvarchar2+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Nvarchar3+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Nvarchar4+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Nvarchar5+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Nvarchar6+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Nvarchar7+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Nvarchar8+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Nvarchar9+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Nvarchar10+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Decimal1+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Decimal2+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Decimal3+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Decimal4+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Decimal5+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Text1+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Text2+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Text3+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Text4+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Text5+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Bit1+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Bit2+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Bit3+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Bit4+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Bit5+"'");
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
				sb.Append(";select max(id) from InfoType");	
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
		public static int Update(InfoType M)
        {
            IDbHelper Sql = GetHelper();
			StringBuilder sb = new StringBuilder();
            sb.Append("update [InfoType] set ");
			
			sb.Append("[TypeName]=N'"+M.TypeName+"'");
			sb.Append(",");
			sb.Append("[TemplateIndex]=N'"+M.TemplateIndex+"'");
			sb.Append(",");
			sb.Append("[TemplateList]=N'"+M.TemplateList+"'");
			sb.Append(",");
			sb.Append("[TemplateContent]=N'"+M.TemplateContent+"'");
			sb.Append(",");
			sb.Append("[num1]=N'"+M.Num1+"'");
			sb.Append(",");
			sb.Append("[num2]=N'"+M.Num2+"'");
			sb.Append(",");
			sb.Append("[num3]=N'"+M.Num3+"'");
			sb.Append(",");
			sb.Append("[num4]=N'"+M.Num4+"'");
			sb.Append(",");
			sb.Append("[num5]=N'"+M.Num5+"'");
			sb.Append(",");
			sb.Append("[num6]=N'"+M.Num6+"'");
			sb.Append(",");
			sb.Append("[num7]=N'"+M.Num7+"'");
			sb.Append(",");
			sb.Append("[num8]=N'"+M.Num8+"'");
			sb.Append(",");
			sb.Append("[num9]=N'"+M.Num9+"'");
			sb.Append(",");
			sb.Append("[num10]=N'"+M.Num10+"'");
			sb.Append(",");
			sb.Append("[nvarchar1]=N'"+M.Nvarchar1+"'");
			sb.Append(",");
			sb.Append("[nvarchar2]=N'"+M.Nvarchar2+"'");
			sb.Append(",");
			sb.Append("[nvarchar3]=N'"+M.Nvarchar3+"'");
			sb.Append(",");
			sb.Append("[nvarchar4]=N'"+M.Nvarchar4+"'");
			sb.Append(",");
			sb.Append("[nvarchar5]=N'"+M.Nvarchar5+"'");
			sb.Append(",");
			sb.Append("[nvarchar6]=N'"+M.Nvarchar6+"'");
			sb.Append(",");
			sb.Append("[nvarchar7]=N'"+M.Nvarchar7+"'");
			sb.Append(",");
			sb.Append("[nvarchar8]=N'"+M.Nvarchar8+"'");
			sb.Append(",");
			sb.Append("[nvarchar9]=N'"+M.Nvarchar9+"'");
			sb.Append(",");
			sb.Append("[nvarchar10]=N'"+M.Nvarchar10+"'");
			sb.Append(",");
			sb.Append("[decimal1]=N'"+M.Decimal1+"'");
			sb.Append(",");
			sb.Append("[decimal2]=N'"+M.Decimal2+"'");
			sb.Append(",");
			sb.Append("[decimal3]=N'"+M.Decimal3+"'");
			sb.Append(",");
			sb.Append("[decimal4]=N'"+M.Decimal4+"'");
			sb.Append(",");
			sb.Append("[decimal5]=N'"+M.Decimal5+"'");
			sb.Append(",");
			sb.Append("[text1]=N'"+M.Text1+"'");
			sb.Append(",");
			sb.Append("[text2]=N'"+M.Text2+"'");
			sb.Append(",");
			sb.Append("[text3]=N'"+M.Text3+"'");
			sb.Append(",");
			sb.Append("[text4]=N'"+M.Text4+"'");
			sb.Append(",");
			sb.Append("[text5]=N'"+M.Text5+"'");
			sb.Append(",");
			sb.Append("[bit1]=N'"+M.Bit1+"'");
			sb.Append(",");
			sb.Append("[bit2]=N'"+M.Bit2+"'");
			sb.Append(",");
			sb.Append("[bit3]=N'"+M.Bit3+"'");
			sb.Append(",");
			sb.Append("[bit4]=N'"+M.Bit4+"'");
			sb.Append(",");
			sb.Append("[bit5]=N'"+M.Bit5+"'");
			
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
        public static void Update(List<InfoType> Ms)
        {
            foreach (InfoType M in Ms)
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
		public static InfoType GetModelByID(string id)
		{
			IDbHelper Sql = GetHelper();
			InfoType M = new InfoType();
			DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select [id],[TypeName],[TemplateIndex],[TemplateList],[TemplateContent],[num1],[num2],[num3],[num4],[num5],[num6],[num7],[num8],[num9],[num10],[nvarchar1],[nvarchar2],[nvarchar3],[nvarchar4],[nvarchar5],[nvarchar6],[nvarchar7],[nvarchar8],[nvarchar9],[nvarchar10],[decimal1],[decimal2],[decimal3],[decimal4],[decimal5],[text1],[text2],[text3],[text4],[text5],[bit1],[bit2],[bit3],[bit4],[bit5] from [InfoType] where Id='" + id.ToString()+"'", true);
			if (!Rs.Read())
			{
					M.Id=0;
			}
			else
			{
				M.Id=Rs["id"].ToInt32();
				M.TypeName=Rs["TypeName"].ToString();
				M.TemplateIndex=Rs["TemplateIndex"].ToString();
				M.TemplateList=Rs["TemplateList"].ToString();
				M.TemplateContent=Rs["TemplateContent"].ToString();
				M.Num1=Rs["num1"].ToString();
				M.Num2=Rs["num2"].ToString();
				M.Num3=Rs["num3"].ToString();
				M.Num4=Rs["num4"].ToString();
				M.Num5=Rs["num5"].ToString();
				M.Num6=Rs["num6"].ToString();
				M.Num7=Rs["num7"].ToString();
				M.Num8=Rs["num8"].ToString();
				M.Num9=Rs["num9"].ToString();
				M.Num10=Rs["num10"].ToString();
				M.Nvarchar1=Rs["nvarchar1"].ToString();
				M.Nvarchar2=Rs["nvarchar2"].ToString();
				M.Nvarchar3=Rs["nvarchar3"].ToString();
				M.Nvarchar4=Rs["nvarchar4"].ToString();
				M.Nvarchar5=Rs["nvarchar5"].ToString();
				M.Nvarchar6=Rs["nvarchar6"].ToString();
				M.Nvarchar7=Rs["nvarchar7"].ToString();
				M.Nvarchar8=Rs["nvarchar8"].ToString();
				M.Nvarchar9=Rs["nvarchar9"].ToString();
				M.Nvarchar10=Rs["nvarchar10"].ToString();
				M.Decimal1=Rs["decimal1"].ToString();
				M.Decimal2=Rs["decimal2"].ToString();
				M.Decimal3=Rs["decimal3"].ToString();
				M.Decimal4=Rs["decimal4"].ToString();
				M.Decimal5=Rs["decimal5"].ToString();
				M.Text1=Rs["text1"].ToString();
				M.Text2=Rs["text2"].ToString();
				M.Text3=Rs["text3"].ToString();
				M.Text4=Rs["text4"].ToString();
				M.Text5=Rs["text5"].ToString();
				M.Bit1=Rs["bit1"].ToString();
				M.Bit2=Rs["bit2"].ToString();
				M.Bit3=Rs["bit3"].ToString();
				M.Bit4=Rs["bit4"].ToString();
				M.Bit5=Rs["bit5"].ToString();
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
		public static InfoType Find(string m_where)
		{
			IDbHelper Sql = GetHelper();
            InfoType M = new InfoType();
            DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select [id],[TypeName],[TemplateIndex],[TemplateList],[TemplateContent],[num1],[num2],[num3],[num4],[num5],[num6],[num7],[num8],[num9],[num10],[nvarchar1],[nvarchar2],[nvarchar3],[nvarchar4],[nvarchar5],[nvarchar6],[nvarchar7],[nvarchar8],[nvarchar9],[nvarchar10],[decimal1],[decimal2],[decimal3],[decimal4],[decimal5],[text1],[text2],[text3],[text4],[text5],[bit1],[bit2],[bit3],[bit4],[bit5] from [InfoType] where " + m_where, true);
			if (!Rs.Read())
            {
					M.Id=0;
            }
			else
			{
				M.Id=Rs["id"].ToInt32();
				M.TypeName=Rs["TypeName"].ToString();
				M.TemplateIndex=Rs["TemplateIndex"].ToString();
				M.TemplateList=Rs["TemplateList"].ToString();
				M.TemplateContent=Rs["TemplateContent"].ToString();
				M.Num1=Rs["num1"].ToString();
				M.Num2=Rs["num2"].ToString();
				M.Num3=Rs["num3"].ToString();
				M.Num4=Rs["num4"].ToString();
				M.Num5=Rs["num5"].ToString();
				M.Num6=Rs["num6"].ToString();
				M.Num7=Rs["num7"].ToString();
				M.Num8=Rs["num8"].ToString();
				M.Num9=Rs["num9"].ToString();
				M.Num10=Rs["num10"].ToString();
				M.Nvarchar1=Rs["nvarchar1"].ToString();
				M.Nvarchar2=Rs["nvarchar2"].ToString();
				M.Nvarchar3=Rs["nvarchar3"].ToString();
				M.Nvarchar4=Rs["nvarchar4"].ToString();
				M.Nvarchar5=Rs["nvarchar5"].ToString();
				M.Nvarchar6=Rs["nvarchar6"].ToString();
				M.Nvarchar7=Rs["nvarchar7"].ToString();
				M.Nvarchar8=Rs["nvarchar8"].ToString();
				M.Nvarchar9=Rs["nvarchar9"].ToString();
				M.Nvarchar10=Rs["nvarchar10"].ToString();
				M.Decimal1=Rs["decimal1"].ToString();
				M.Decimal2=Rs["decimal2"].ToString();
				M.Decimal3=Rs["decimal3"].ToString();
				M.Decimal4=Rs["decimal4"].ToString();
				M.Decimal5=Rs["decimal5"].ToString();
				M.Text1=Rs["text1"].ToString();
				M.Text2=Rs["text2"].ToString();
				M.Text3=Rs["text3"].ToString();
				M.Text4=Rs["text4"].ToString();
				M.Text5=Rs["text5"].ToString();
				M.Bit1=Rs["bit1"].ToString();
				M.Bit2=Rs["bit2"].ToString();
				M.Bit3=Rs["bit3"].ToString();
				M.Bit4=Rs["bit4"].ToString();
				M.Bit5=Rs["bit5"].ToString();
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
            return Sql.ExecuteDataTable(CommandType.Text, "select [id],[TypeName],[TemplateIndex],[TemplateList],[TemplateContent],[num1],[num2],[num3],[num4],[num5],[num6],[num7],[num8],[num9],[num10],[nvarchar1],[nvarchar2],[nvarchar3],[nvarchar4],[nvarchar5],[nvarchar6],[nvarchar7],[nvarchar8],[nvarchar9],[nvarchar10],[decimal1],[decimal2],[decimal3],[decimal4],[decimal5],[text1],[text2],[text3],[text4],[text5],[bit1],[bit2],[bit3],[bit4],[bit5] from [InfoType] where "+ m_where);
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
            DataTable dt = Sql.ExecuteDataTable(CommandType.Text, "select top "+ top.ToString() +"  [id],[TypeName],[TemplateIndex],[TemplateList],[TemplateContent],[num1],[num2],[num3],[num4],[num5],[num6],[num7],[num8],[num9],[num10],[nvarchar1],[nvarchar2],[nvarchar3],[nvarchar4],[nvarchar5],[nvarchar6],[nvarchar7],[nvarchar8],[nvarchar9],[nvarchar10],[decimal1],[decimal2],[decimal3],[decimal4],[decimal5],[text1],[text2],[text3],[text4],[text5],[bit1],[bit2],[bit3],[bit4],[bit5] from [InfoType] where "+ m_where);
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
			return Convert.ToInt32(Sql.ExecuteScalar(CommandType.Text,"select count(0) from [InfoType] where "+m_where));
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
            sd = Sql.ExecuteReader(CommandType.Text, "select 1 from [InfoType] where " + m_where, true);
            if (sd.Read())
            {
                returnValue = true;
            }
            sd.Close();
            sd.Dispose();
            return returnValue;
			
			
		}
		#endregion
		
		#region List<InfoType>获取符合条件记录的实体列表,慎用！！！！
		/// <summary>
        /// List<InfoType>获取符合条件记录的实体列表,慎用！！！！
        /// </summary>
        /// <param name="m_where">条件语句，不包含“where”</param>
        /// <returns></returns>
		public static List<InfoType> GetModelList(string m_where)
		{	
			return DataTableToList(getTable(m_where));
		}
		public static List<InfoType> GetModelList(string m_where,int top)
		{	
			return DataTableToList(getTable(m_where, top));
		}
		public static List<InfoType> GetModelList()
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
				Sql.ExecuteNonQuery(CommandType.Text, "delete from [InfoType] where "+ m_where);
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
