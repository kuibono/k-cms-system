/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码，如需要添加方法，请创建同名类，并在该类中添加新的方法。
*生成时间：2012-4-10 14:28:33
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
	///表Info的数据操作类
	///</summary>
    public partial class InfoView
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
        protected static List<Info> DataTableToList(DataTable dt)
        {
            List<Info> Ms = new List<Info>();
			
			for (int i = 0; i < dt.Rows.Count; i++)
            {
				Info M = new Info();
				M.Id=dt.Rows[i]["id"].ToInt32();
				M.InfoTypeID=dt.Rows[i]["InfoTypeID"].ToString();
				M.ClassID=dt.Rows[i]["ClassID"].ToInt32();
				M.ClassName=dt.Rows[i]["ClassName"].ToString();
				M.ZtID=dt.Rows[i]["ZtID"].ToInt32();
				M.ZtName=dt.Rows[i]["ZtName"].ToString();
				M.Title=dt.Rows[i]["Title"].ToString();
				M.UserName=dt.Rows[i]["UserName"].ToString();
				M.UserID=dt.Rows[i]["UserID"].ToInt32();
				M.Contact=dt.Rows[i]["Contact"].ToString();
				M.ContactType=dt.Rows[i]["ContactType"].ToString();
				M.Tel=dt.Rows[i]["Tel"].ToString();
				M.TelLocation=dt.Rows[i]["TelLocation"].ToString();
				M.Address=dt.Rows[i]["Address"].ToString();
				M.Intro=dt.Rows[i]["Intro"].ToString();
				M.ImageCount=dt.Rows[i]["ImageCount"].ToInt32();
				M.ReplyCount=dt.Rows[i]["ReplyCount"].ToInt32();
				M.ClickCount=dt.Rows[i]["ClickCount"].ToInt32();
				M.IsSetTop=dt.Rows[i]["IsSetTop"].ToBoolean();
				M.OutTime=dt.Rows[i]["OutTime"].ToDateTime();
				M.PostTime=dt.Rows[i]["PostTime"].ToDateTime();
				M.Num1=dt.Rows[i]["num1"].ToInt32();
				M.Num2=dt.Rows[i]["num2"].ToInt32();
				M.Num3=dt.Rows[i]["num3"].ToInt32();
				M.Num4=dt.Rows[i]["num4"].ToInt32();
				M.Num5=dt.Rows[i]["num5"].ToInt32();
				M.Num6=dt.Rows[i]["num6"].ToInt32();
				M.Num7=dt.Rows[i]["num7"].ToInt32();
				M.Num8=dt.Rows[i]["num8"].ToInt32();
				M.Num9=dt.Rows[i]["num9"].ToInt32();
				M.Num10=dt.Rows[i]["num10"].ToInt32();
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
				M.Nvarchar11=dt.Rows[i]["nvarchar11"].ToString();
				M.Nvarchar12=dt.Rows[i]["nvarchar12"].ToString();
				M.Nvarchar13=dt.Rows[i]["nvarchar13"].ToString();
				M.Nvarchar14=dt.Rows[i]["nvarchar14"].ToString();
				M.Nvarchar15=dt.Rows[i]["nvarchar15"].ToString();
				M.Decimal1=dt.Rows[i]["decimal1"].ToDecimal();
				M.Decimal2=dt.Rows[i]["decimal2"].ToDecimal();
				M.Decimal3=dt.Rows[i]["decimal3"].ToDecimal();
				M.Decimal4=dt.Rows[i]["decimal4"].ToDecimal();
				M.Decimal5=dt.Rows[i]["decimal5"].ToDecimal();
				M.Text1=dt.Rows[i]["text1"].ToString();
				M.Text2=dt.Rows[i]["text2"].ToString();
				M.Text3=dt.Rows[i]["text3"].ToString();
				M.Text4=dt.Rows[i]["text4"].ToString();
				M.Text5=dt.Rows[i]["text5"].ToString();
				M.Bit1=dt.Rows[i]["bit1"].ToBoolean();
				M.Bit2=dt.Rows[i]["bit2"].ToBoolean();
				M.Bit3=dt.Rows[i]["bit3"].ToBoolean();
				M.Bit4=dt.Rows[i]["bit4"].ToBoolean();
				M.Bit5=dt.Rows[i]["bit5"].ToBoolean();
				
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
		public static void Insert(Info M)
        {
            IDbHelper Sql = GetHelper();
            StringBuilder sb = new StringBuilder();			
			
			sb.Append("insert into [Info]([InfoTypeID],[ClassID],[ClassName],[ZtID],[ZtName],[Title],[UserName],[UserID],[Contact],[ContactType],[Tel],[TelLocation],[Address],[Intro],[ImageCount],[ReplyCount],[ClickCount],[IsSetTop],[OutTime],[PostTime],[num1],[num2],[num3],[num4],[num5],[num6],[num7],[num8],[num9],[num10],[nvarchar1],[nvarchar2],[nvarchar3],[nvarchar4],[nvarchar5],[nvarchar6],[nvarchar7],[nvarchar8],[nvarchar9],[nvarchar10],[nvarchar11],[nvarchar12],[nvarchar13],[nvarchar14],[nvarchar15],[decimal1],[decimal2],[decimal3],[decimal4],[decimal5],[text1],[text2],[text3],[text4],[text5],[bit1],[bit2],[bit3],[bit4],[bit5]) values(");
			sb.Append("N'"+M.InfoTypeID+"'");
			sb.Append(",");	
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
			sb.Append("N'"+M.UserName+"'");
			sb.Append(",");	
			sb.Append(M.UserID.ToS());
			sb.Append(",");	
			sb.Append("N'"+M.Contact+"'");
			sb.Append(",");	
			sb.Append("N'"+M.ContactType+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Tel+"'");
			sb.Append(",");	
			sb.Append("N'"+M.TelLocation+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Address+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Intro+"'");
			sb.Append(",");	
			sb.Append(M.ImageCount.ToS());
			sb.Append(",");	
			sb.Append(M.ReplyCount.ToS());
			sb.Append(",");	
			sb.Append(M.ClickCount.ToS());
			sb.Append(",");	
			sb.Append(M.IsSetTop.ToS());
			sb.Append(",");	
			sb.Append("N'"+M.OutTime+"'");
			sb.Append(",");	
			sb.Append("N'"+M.PostTime+"'");
			sb.Append(",");	
			sb.Append(M.Num1.ToS());
			sb.Append(",");	
			sb.Append(M.Num2.ToS());
			sb.Append(",");	
			sb.Append(M.Num3.ToS());
			sb.Append(",");	
			sb.Append(M.Num4.ToS());
			sb.Append(",");	
			sb.Append(M.Num5.ToS());
			sb.Append(",");	
			sb.Append(M.Num6.ToS());
			sb.Append(",");	
			sb.Append(M.Num7.ToS());
			sb.Append(",");	
			sb.Append(M.Num8.ToS());
			sb.Append(",");	
			sb.Append(M.Num9.ToS());
			sb.Append(",");	
			sb.Append(M.Num10.ToS());
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
			sb.Append("N'"+M.Nvarchar11+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Nvarchar12+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Nvarchar13+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Nvarchar14+"'");
			sb.Append(",");	
			sb.Append("N'"+M.Nvarchar15+"'");
			sb.Append(",");	
			sb.Append(M.Decimal1.ToS());
			sb.Append(",");	
			sb.Append(M.Decimal2.ToS());
			sb.Append(",");	
			sb.Append(M.Decimal3.ToS());
			sb.Append(",");	
			sb.Append(M.Decimal4.ToS());
			sb.Append(",");	
			sb.Append(M.Decimal5.ToS());
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
			sb.Append(M.Bit1.ToS());
			sb.Append(",");	
			sb.Append(M.Bit2.ToS());
			sb.Append(",");	
			sb.Append(M.Bit3.ToS());
			sb.Append(",");	
			sb.Append(M.Bit4.ToS());
			sb.Append(",");	
			sb.Append(M.Bit5.ToS());
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
				sb.Append(";select max(id) from Info");	
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
		public static int Update(Info M)
        {
            IDbHelper Sql = GetHelper();
			StringBuilder sb = new StringBuilder();
            sb.Append("update [Info] set ");
			
			sb.Append("[InfoTypeID]=N'"+M.InfoTypeID+"'");
			sb.Append(",");
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
			sb.Append("[UserName]=N'"+M.UserName+"'");
			sb.Append(",");
			sb.Append("[UserID]="+M.UserID.ToS());
			sb.Append(",");
			sb.Append("[Contact]=N'"+M.Contact+"'");
			sb.Append(",");
			sb.Append("[ContactType]=N'"+M.ContactType+"'");
			sb.Append(",");
			sb.Append("[Tel]=N'"+M.Tel+"'");
			sb.Append(",");
			sb.Append("[TelLocation]=N'"+M.TelLocation+"'");
			sb.Append(",");
			sb.Append("[Address]=N'"+M.Address+"'");
			sb.Append(",");
			sb.Append("[Intro]=N'"+M.Intro+"'");
			sb.Append(",");
			sb.Append("[ImageCount]="+M.ImageCount.ToS());
			sb.Append(",");
			sb.Append("[ReplyCount]="+M.ReplyCount.ToS());
			sb.Append(",");
			sb.Append("[ClickCount]="+M.ClickCount.ToS());
			sb.Append(",");
			sb.Append("[IsSetTop]="+M.IsSetTop.ToS());
			sb.Append(",");
			sb.Append("[OutTime]=N'"+M.OutTime+"'");
			sb.Append(",");
			sb.Append("[PostTime]=N'"+M.PostTime+"'");
			sb.Append(",");
			sb.Append("[num1]="+M.Num1.ToS());
			sb.Append(",");
			sb.Append("[num2]="+M.Num2.ToS());
			sb.Append(",");
			sb.Append("[num3]="+M.Num3.ToS());
			sb.Append(",");
			sb.Append("[num4]="+M.Num4.ToS());
			sb.Append(",");
			sb.Append("[num5]="+M.Num5.ToS());
			sb.Append(",");
			sb.Append("[num6]="+M.Num6.ToS());
			sb.Append(",");
			sb.Append("[num7]="+M.Num7.ToS());
			sb.Append(",");
			sb.Append("[num8]="+M.Num8.ToS());
			sb.Append(",");
			sb.Append("[num9]="+M.Num9.ToS());
			sb.Append(",");
			sb.Append("[num10]="+M.Num10.ToS());
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
			sb.Append("[nvarchar11]=N'"+M.Nvarchar11+"'");
			sb.Append(",");
			sb.Append("[nvarchar12]=N'"+M.Nvarchar12+"'");
			sb.Append(",");
			sb.Append("[nvarchar13]=N'"+M.Nvarchar13+"'");
			sb.Append(",");
			sb.Append("[nvarchar14]=N'"+M.Nvarchar14+"'");
			sb.Append(",");
			sb.Append("[nvarchar15]=N'"+M.Nvarchar15+"'");
			sb.Append(",");
			sb.Append("[decimal1]="+M.Decimal1.ToS());
			sb.Append(",");
			sb.Append("[decimal2]="+M.Decimal2.ToS());
			sb.Append(",");
			sb.Append("[decimal3]="+M.Decimal3.ToS());
			sb.Append(",");
			sb.Append("[decimal4]="+M.Decimal4.ToS());
			sb.Append(",");
			sb.Append("[decimal5]="+M.Decimal5.ToS());
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
			sb.Append("[bit1]="+M.Bit1.ToS());
			sb.Append(",");
			sb.Append("[bit2]="+M.Bit2.ToS());
			sb.Append(",");
			sb.Append("[bit3]="+M.Bit3.ToS());
			sb.Append(",");
			sb.Append("[bit4]="+M.Bit4.ToS());
			sb.Append(",");
			sb.Append("[bit5]="+M.Bit5.ToS());
			
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
        public static void Update(List<Info> Ms)
        {
            foreach (Info M in Ms)
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
		public static Info GetModelByID(string id)
		{
			IDbHelper Sql = GetHelper();
			Info M = new Info();
			DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select [id],[InfoTypeID],[ClassID],[ClassName],[ZtID],[ZtName],[Title],[UserName],[UserID],[Contact],[ContactType],[Tel],[TelLocation],[Address],[Intro],[ImageCount],[ReplyCount],[ClickCount],[IsSetTop],[OutTime],[PostTime],[num1],[num2],[num3],[num4],[num5],[num6],[num7],[num8],[num9],[num10],[nvarchar1],[nvarchar2],[nvarchar3],[nvarchar4],[nvarchar5],[nvarchar6],[nvarchar7],[nvarchar8],[nvarchar9],[nvarchar10],[nvarchar11],[nvarchar12],[nvarchar13],[nvarchar14],[nvarchar15],[decimal1],[decimal2],[decimal3],[decimal4],[decimal5],[text1],[text2],[text3],[text4],[text5],[bit1],[bit2],[bit3],[bit4],[bit5] from [Info] where Id='" + id.ToString()+"'", true);
			if (!Rs.Read())
			{
					M.Id=0;
			}
			else
			{
				M.Id=Rs["id"].ToInt32();
				M.InfoTypeID=Rs["InfoTypeID"].ToString();
				M.ClassID=Rs["ClassID"].ToInt32();
				M.ClassName=Rs["ClassName"].ToString();
				M.ZtID=Rs["ZtID"].ToInt32();
				M.ZtName=Rs["ZtName"].ToString();
				M.Title=Rs["Title"].ToString();
				M.UserName=Rs["UserName"].ToString();
				M.UserID=Rs["UserID"].ToInt32();
				M.Contact=Rs["Contact"].ToString();
				M.ContactType=Rs["ContactType"].ToString();
				M.Tel=Rs["Tel"].ToString();
				M.TelLocation=Rs["TelLocation"].ToString();
				M.Address=Rs["Address"].ToString();
				M.Intro=Rs["Intro"].ToString();
				M.ImageCount=Rs["ImageCount"].ToInt32();
				M.ReplyCount=Rs["ReplyCount"].ToInt32();
				M.ClickCount=Rs["ClickCount"].ToInt32();
				M.IsSetTop=Rs["IsSetTop"].ToBoolean();
				M.OutTime=Rs["OutTime"].ToDateTime();
				M.PostTime=Rs["PostTime"].ToDateTime();
				M.Num1=Rs["num1"].ToInt32();
				M.Num2=Rs["num2"].ToInt32();
				M.Num3=Rs["num3"].ToInt32();
				M.Num4=Rs["num4"].ToInt32();
				M.Num5=Rs["num5"].ToInt32();
				M.Num6=Rs["num6"].ToInt32();
				M.Num7=Rs["num7"].ToInt32();
				M.Num8=Rs["num8"].ToInt32();
				M.Num9=Rs["num9"].ToInt32();
				M.Num10=Rs["num10"].ToInt32();
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
				M.Nvarchar11=Rs["nvarchar11"].ToString();
				M.Nvarchar12=Rs["nvarchar12"].ToString();
				M.Nvarchar13=Rs["nvarchar13"].ToString();
				M.Nvarchar14=Rs["nvarchar14"].ToString();
				M.Nvarchar15=Rs["nvarchar15"].ToString();
				M.Decimal1=Rs["decimal1"].ToDecimal();
				M.Decimal2=Rs["decimal2"].ToDecimal();
				M.Decimal3=Rs["decimal3"].ToDecimal();
				M.Decimal4=Rs["decimal4"].ToDecimal();
				M.Decimal5=Rs["decimal5"].ToDecimal();
				M.Text1=Rs["text1"].ToString();
				M.Text2=Rs["text2"].ToString();
				M.Text3=Rs["text3"].ToString();
				M.Text4=Rs["text4"].ToString();
				M.Text5=Rs["text5"].ToString();
				M.Bit1=Rs["bit1"].ToBoolean();
				M.Bit2=Rs["bit2"].ToBoolean();
				M.Bit3=Rs["bit3"].ToBoolean();
				M.Bit4=Rs["bit4"].ToBoolean();
				M.Bit5=Rs["bit5"].ToBoolean();
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
		public static Info Find(string m_where)
		{
			IDbHelper Sql = GetHelper();
            Info M = new Info();
            DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select [id],[InfoTypeID],[ClassID],[ClassName],[ZtID],[ZtName],[Title],[UserName],[UserID],[Contact],[ContactType],[Tel],[TelLocation],[Address],[Intro],[ImageCount],[ReplyCount],[ClickCount],[IsSetTop],[OutTime],[PostTime],[num1],[num2],[num3],[num4],[num5],[num6],[num7],[num8],[num9],[num10],[nvarchar1],[nvarchar2],[nvarchar3],[nvarchar4],[nvarchar5],[nvarchar6],[nvarchar7],[nvarchar8],[nvarchar9],[nvarchar10],[nvarchar11],[nvarchar12],[nvarchar13],[nvarchar14],[nvarchar15],[decimal1],[decimal2],[decimal3],[decimal4],[decimal5],[text1],[text2],[text3],[text4],[text5],[bit1],[bit2],[bit3],[bit4],[bit5] from [Info] where " + m_where, true);
			if (!Rs.Read())
            {
					M.Id=0;
            }
			else
			{
				M.Id=Rs["id"].ToInt32();
				M.InfoTypeID=Rs["InfoTypeID"].ToString();
				M.ClassID=Rs["ClassID"].ToInt32();
				M.ClassName=Rs["ClassName"].ToString();
				M.ZtID=Rs["ZtID"].ToInt32();
				M.ZtName=Rs["ZtName"].ToString();
				M.Title=Rs["Title"].ToString();
				M.UserName=Rs["UserName"].ToString();
				M.UserID=Rs["UserID"].ToInt32();
				M.Contact=Rs["Contact"].ToString();
				M.ContactType=Rs["ContactType"].ToString();
				M.Tel=Rs["Tel"].ToString();
				M.TelLocation=Rs["TelLocation"].ToString();
				M.Address=Rs["Address"].ToString();
				M.Intro=Rs["Intro"].ToString();
				M.ImageCount=Rs["ImageCount"].ToInt32();
				M.ReplyCount=Rs["ReplyCount"].ToInt32();
				M.ClickCount=Rs["ClickCount"].ToInt32();
				M.IsSetTop=Rs["IsSetTop"].ToBoolean();
				M.OutTime=Rs["OutTime"].ToDateTime();
				M.PostTime=Rs["PostTime"].ToDateTime();
				M.Num1=Rs["num1"].ToInt32();
				M.Num2=Rs["num2"].ToInt32();
				M.Num3=Rs["num3"].ToInt32();
				M.Num4=Rs["num4"].ToInt32();
				M.Num5=Rs["num5"].ToInt32();
				M.Num6=Rs["num6"].ToInt32();
				M.Num7=Rs["num7"].ToInt32();
				M.Num8=Rs["num8"].ToInt32();
				M.Num9=Rs["num9"].ToInt32();
				M.Num10=Rs["num10"].ToInt32();
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
				M.Nvarchar11=Rs["nvarchar11"].ToString();
				M.Nvarchar12=Rs["nvarchar12"].ToString();
				M.Nvarchar13=Rs["nvarchar13"].ToString();
				M.Nvarchar14=Rs["nvarchar14"].ToString();
				M.Nvarchar15=Rs["nvarchar15"].ToString();
				M.Decimal1=Rs["decimal1"].ToDecimal();
				M.Decimal2=Rs["decimal2"].ToDecimal();
				M.Decimal3=Rs["decimal3"].ToDecimal();
				M.Decimal4=Rs["decimal4"].ToDecimal();
				M.Decimal5=Rs["decimal5"].ToDecimal();
				M.Text1=Rs["text1"].ToString();
				M.Text2=Rs["text2"].ToString();
				M.Text3=Rs["text3"].ToString();
				M.Text4=Rs["text4"].ToString();
				M.Text5=Rs["text5"].ToString();
				M.Bit1=Rs["bit1"].ToBoolean();
				M.Bit2=Rs["bit2"].ToBoolean();
				M.Bit3=Rs["bit3"].ToBoolean();
				M.Bit4=Rs["bit4"].ToBoolean();
				M.Bit5=Rs["bit5"].ToBoolean();
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
            return Sql.ExecuteDataTable(CommandType.Text, "select [id],[InfoTypeID],[ClassID],[ClassName],[ZtID],[ZtName],[Title],[UserName],[UserID],[Contact],[ContactType],[Tel],[TelLocation],[Address],[Intro],[ImageCount],[ReplyCount],[ClickCount],[IsSetTop],[OutTime],[PostTime],[num1],[num2],[num3],[num4],[num5],[num6],[num7],[num8],[num9],[num10],[nvarchar1],[nvarchar2],[nvarchar3],[nvarchar4],[nvarchar5],[nvarchar6],[nvarchar7],[nvarchar8],[nvarchar9],[nvarchar10],[nvarchar11],[nvarchar12],[nvarchar13],[nvarchar14],[nvarchar15],[decimal1],[decimal2],[decimal3],[decimal4],[decimal5],[text1],[text2],[text3],[text4],[text5],[bit1],[bit2],[bit3],[bit4],[bit5] from [Info] where "+ m_where);
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
            DataTable dt = Sql.ExecuteDataTable(CommandType.Text, "select top "+ top.ToString() +"  [id],[InfoTypeID],[ClassID],[ClassName],[ZtID],[ZtName],[Title],[UserName],[UserID],[Contact],[ContactType],[Tel],[TelLocation],[Address],[Intro],[ImageCount],[ReplyCount],[ClickCount],[IsSetTop],[OutTime],[PostTime],[num1],[num2],[num3],[num4],[num5],[num6],[num7],[num8],[num9],[num10],[nvarchar1],[nvarchar2],[nvarchar3],[nvarchar4],[nvarchar5],[nvarchar6],[nvarchar7],[nvarchar8],[nvarchar9],[nvarchar10],[nvarchar11],[nvarchar12],[nvarchar13],[nvarchar14],[nvarchar15],[decimal1],[decimal2],[decimal3],[decimal4],[decimal5],[text1],[text2],[text3],[text4],[text5],[bit1],[bit2],[bit3],[bit4],[bit5] from [Info] where "+ m_where);
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
			return Convert.ToInt32(Sql.ExecuteScalar(CommandType.Text,"select count(0) from [Info] where "+m_where));
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
            sd = Sql.ExecuteReader(CommandType.Text, "select 1 from [Info] where " + m_where, true);
            if (sd.Read())
            {
                returnValue = true;
            }
            sd.Close();
            sd.Dispose();
            return returnValue;
			
			
		}
		#endregion
		
		#region List<Info>获取符合条件记录的实体列表,慎用！！！！
		/// <summary>
        /// List<Info>获取符合条件记录的实体列表,慎用！！！！
        /// </summary>
        /// <param name="m_where">条件语句，不包含“where”</param>
        /// <returns></returns>
		public static List<Info> GetModelList(string m_where)
		{	
			return DataTableToList(getTable(m_where));
		}
		public static List<Info> GetModelList(string m_where,int top)
		{	
			return DataTableToList(getTable(m_where, top));
		}
		public static List<Info> GetModelList()
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
				Sql.ExecuteNonQuery(CommandType.Text, "delete from [Info] where "+ m_where);
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
