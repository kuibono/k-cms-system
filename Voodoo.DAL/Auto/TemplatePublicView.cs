/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码，如需要添加方法，请创建同名类，并在该类中添加新的方法。
*生成时间：2011-11-29 13:49:07
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
    ///表TemplatePublic的数据操作类
    ///</summary>
    public partial class TemplatePublicView
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
        protected static List<TemplatePublic> DataTableToList(DataTable dt)
        {
            List<TemplatePublic> Ms = new List<TemplatePublic>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TemplatePublic M = new TemplatePublic();
                M.ID = dt.Rows[i]["ID"].ToInt32();
                M.GroupID = dt.Rows[i]["GroupID"].ToInt32();
                M.IndexContent = dt.Rows[i]["IndexContent"].ToString();
                M.Controlcontent = dt.Rows[i]["Controlcontent"].ToString();
                M.SiteSearchContent = dt.Rows[i]["SiteSearchContent"].ToString();
                M.AdvancedSearch = dt.Rows[i]["AdvancedSearch"].ToString();
                M.HorizontaSearch = dt.Rows[i]["HorizontaSearch"].ToString();
                M.VerticalSearch = dt.Rows[i]["VerticalSearch"].ToString();
                M.RelationInfo = dt.Rows[i]["RelationInfo"].ToString();
                M.MessageBoard = dt.Rows[i]["MessageBoard"].ToString();
                M.Reply = dt.Rows[i]["Reply"].ToString();
                M.FinalDown = dt.Rows[i]["FinalDown"].ToString();
                M.DownAddress = dt.Rows[i]["DownAddress"].ToString();
                M.OLPlayaddress = dt.Rows[i]["OLPlayaddress"].ToString();
                M.ListPager = dt.Rows[i]["ListPager"].ToString();
                M.LoginStatus = dt.Rows[i]["LoginStatus"].ToString();
                M.JSLogin = dt.Rows[i]["JSLogin"].ToString();
                M.ImageList = dt.Rows[i]["ImageList"].ToString();
                M.AnswerList = dt.Rows[i]["AnswerList"].ToString();

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
        public static void Insert(TemplatePublic M)
        {
            IDbHelper Sql = GetHelper();
            StringBuilder sb = new StringBuilder();

            sb.Append("insert into [TemplatePublic]([GroupID],[IndexContent],[Controlcontent],[SiteSearchContent],[AdvancedSearch],[HorizontaSearch],[VerticalSearch],[RelationInfo],[MessageBoard],[Reply],[FinalDown],[DownAddress],[OLPlayaddress],[ListPager],[LoginStatus],[JSLogin],[ImageList],[AnswerList]) values(");
            sb.Append(M.GroupID.ToS());
            sb.Append(",");
            sb.Append("'" + M.IndexContent + "'");
            sb.Append(",");
            sb.Append("'" + M.Controlcontent + "'");
            sb.Append(",");
            sb.Append("'" + M.SiteSearchContent + "'");
            sb.Append(",");
            sb.Append("'" + M.AdvancedSearch + "'");
            sb.Append(",");
            sb.Append("'" + M.HorizontaSearch + "'");
            sb.Append(",");
            sb.Append("'" + M.VerticalSearch + "'");
            sb.Append(",");
            sb.Append("'" + M.RelationInfo + "'");
            sb.Append(",");
            sb.Append("'" + M.MessageBoard + "'");
            sb.Append(",");
            sb.Append("'" + M.Reply + "'");
            sb.Append(",");
            sb.Append("'" + M.FinalDown + "'");
            sb.Append(",");
            sb.Append("'" + M.DownAddress + "'");
            sb.Append(",");
            sb.Append("'" + M.OLPlayaddress + "'");
            sb.Append(",");
            sb.Append("'" + M.ListPager + "'");
            sb.Append(",");
            sb.Append("'" + M.LoginStatus + "'");
            sb.Append(",");
            sb.Append("'" + M.JSLogin + "'");
            sb.Append(",");
            sb.Append("'" + M.ImageList + "'");
            sb.Append(",");
            sb.Append("'" + M.AnswerList + "'");
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
                sb.Append(";select max(ID) from TemplatePublic");
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
        public static int Update(TemplatePublic M)
        {
            IDbHelper Sql = GetHelper();
            StringBuilder sb = new StringBuilder();
            sb.Append("update [TemplatePublic] set ");

            sb.Append("[GroupID]=" + M.GroupID.ToS());
            sb.Append(",");
            sb.Append("[IndexContent]='" + M.IndexContent + "'");
            sb.Append(",");
            sb.Append("[Controlcontent]='" + M.Controlcontent + "'");
            sb.Append(",");
            sb.Append("[SiteSearchContent]='" + M.SiteSearchContent + "'");
            sb.Append(",");
            sb.Append("[AdvancedSearch]='" + M.AdvancedSearch + "'");
            sb.Append(",");
            sb.Append("[HorizontaSearch]='" + M.HorizontaSearch + "'");
            sb.Append(",");
            sb.Append("[VerticalSearch]='" + M.VerticalSearch + "'");
            sb.Append(",");
            sb.Append("[RelationInfo]='" + M.RelationInfo + "'");
            sb.Append(",");
            sb.Append("[MessageBoard]='" + M.MessageBoard + "'");
            sb.Append(",");
            sb.Append("[Reply]='" + M.Reply + "'");
            sb.Append(",");
            sb.Append("[FinalDown]='" + M.FinalDown + "'");
            sb.Append(",");
            sb.Append("[DownAddress]='" + M.DownAddress + "'");
            sb.Append(",");
            sb.Append("[OLPlayaddress]='" + M.OLPlayaddress + "'");
            sb.Append(",");
            sb.Append("[ListPager]='" + M.ListPager + "'");
            sb.Append(",");
            sb.Append("[LoginStatus]='" + M.LoginStatus + "'");
            sb.Append(",");
            sb.Append("[JSLogin]='" + M.JSLogin + "'");
            sb.Append(",");
            sb.Append("[ImageList]='" + M.ImageList + "'");
            sb.Append(",");
            sb.Append("[AnswerList]='" + M.AnswerList + "'");

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
        public static void Update(List<TemplatePublic> Ms)
        {
            foreach (TemplatePublic M in Ms)
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
        public static TemplatePublic GetModelByID(string id)
        {
            IDbHelper Sql = GetHelper();
            TemplatePublic M = new TemplatePublic();
            DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select [ID],[GroupID],[IndexContent],[Controlcontent],[SiteSearchContent],[AdvancedSearch],[HorizontaSearch],[VerticalSearch],[RelationInfo],[MessageBoard],[Reply],[FinalDown],[DownAddress],[OLPlayaddress],[ListPager],[LoginStatus],[JSLogin],[ImageList],[AnswerList] from [TemplatePublic] where ID='" + id.ToString() + "'", true);
            if (!Rs.Read())
            {
                M.ID = 0;
            }
            else
            {
                M.ID = Rs["ID"].ToInt32();
                M.GroupID = Rs["GroupID"].ToInt32();
                M.IndexContent = Rs["IndexContent"].ToString();
                M.Controlcontent = Rs["Controlcontent"].ToString();
                M.SiteSearchContent = Rs["SiteSearchContent"].ToString();
                M.AdvancedSearch = Rs["AdvancedSearch"].ToString();
                M.HorizontaSearch = Rs["HorizontaSearch"].ToString();
                M.VerticalSearch = Rs["VerticalSearch"].ToString();
                M.RelationInfo = Rs["RelationInfo"].ToString();
                M.MessageBoard = Rs["MessageBoard"].ToString();
                M.Reply = Rs["Reply"].ToString();
                M.FinalDown = Rs["FinalDown"].ToString();
                M.DownAddress = Rs["DownAddress"].ToString();
                M.OLPlayaddress = Rs["OLPlayaddress"].ToString();
                M.ListPager = Rs["ListPager"].ToString();
                M.LoginStatus = Rs["LoginStatus"].ToString();
                M.JSLogin = Rs["JSLogin"].ToString();
                M.ImageList = Rs["ImageList"].ToString();
                M.AnswerList = Rs["AnswerList"].ToString();
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
        public static TemplatePublic Find(string m_where)
        {
            IDbHelper Sql = GetHelper();
            TemplatePublic M = new TemplatePublic();
            DbDataReader Rs = Sql.ExecuteReader(CommandType.Text, "select [ID],[GroupID],[IndexContent],[Controlcontent],[SiteSearchContent],[AdvancedSearch],[HorizontaSearch],[VerticalSearch],[RelationInfo],[MessageBoard],[Reply],[FinalDown],[DownAddress],[OLPlayaddress],[ListPager],[LoginStatus],[JSLogin],[ImageList],[AnswerList] from [TemplatePublic] where " + m_where, true);
            if (!Rs.Read())
            {
                M.ID = 0;
            }
            else
            {
                M.ID = Rs["ID"].ToInt32();
                M.GroupID = Rs["GroupID"].ToInt32();
                M.IndexContent = Rs["IndexContent"].ToString();
                M.Controlcontent = Rs["Controlcontent"].ToString();
                M.SiteSearchContent = Rs["SiteSearchContent"].ToString();
                M.AdvancedSearch = Rs["AdvancedSearch"].ToString();
                M.HorizontaSearch = Rs["HorizontaSearch"].ToString();
                M.VerticalSearch = Rs["VerticalSearch"].ToString();
                M.RelationInfo = Rs["RelationInfo"].ToString();
                M.MessageBoard = Rs["MessageBoard"].ToString();
                M.Reply = Rs["Reply"].ToString();
                M.FinalDown = Rs["FinalDown"].ToString();
                M.DownAddress = Rs["DownAddress"].ToString();
                M.OLPlayaddress = Rs["OLPlayaddress"].ToString();
                M.ListPager = Rs["ListPager"].ToString();
                M.LoginStatus = Rs["LoginStatus"].ToString();
                M.JSLogin = Rs["JSLogin"].ToString();
                M.ImageList = Rs["ImageList"].ToString();
                M.AnswerList = Rs["AnswerList"].ToString();
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
            return Sql.ExecuteDataTable(CommandType.Text, "select [ID],[GroupID],[IndexContent],[Controlcontent],[SiteSearchContent],[AdvancedSearch],[HorizontaSearch],[VerticalSearch],[RelationInfo],[MessageBoard],[Reply],[FinalDown],[DownAddress],[OLPlayaddress],[ListPager],[LoginStatus],[JSLogin],[ImageList],[AnswerList] from [TemplatePublic] where " + m_where);
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
            DataTable dt = Sql.ExecuteDataTable(CommandType.Text, "select top " + top.ToString() + "  [ID],[GroupID],[IndexContent],[Controlcontent],[SiteSearchContent],[AdvancedSearch],[HorizontaSearch],[VerticalSearch],[RelationInfo],[MessageBoard],[Reply],[FinalDown],[DownAddress],[OLPlayaddress],[ListPager],[LoginStatus],[JSLogin],[ImageList],[AnswerList] from [TemplatePublic] where " + m_where);
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
            return Convert.ToInt32(Sql.ExecuteScalar(CommandType.Text, "select count(0) from [TemplatePublic] where " + m_where));
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
            sd = Sql.ExecuteReader(CommandType.Text, "select 1 from [TemplatePublic] where " + m_where, true);
            if (sd.Read())
            {
                returnValue = true;
            }
            sd.Close();
            sd.Dispose();
            return returnValue;


        }
        #endregion

        #region List<TemplatePublic>获取符合条件记录的实体列表,慎用！！！！
        /// <summary>
        /// List<TemplatePublic>获取符合条件记录的实体列表,慎用！！！！
        /// </summary>
        /// <param name="m_where">条件语句，不包含“where”</param>
        /// <returns></returns>
        public static List<TemplatePublic> GetModelList(string m_where)
        {
            return DataTableToList(getTable(m_where));
        }
        public static List<TemplatePublic> GetModelList(string m_where, int top)
        {
            return DataTableToList(getTable(m_where, top));
        }
        public static List<TemplatePublic> GetModelList()
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
                Sql.ExecuteNonQuery(CommandType.Text, "delete from [TemplatePublic] where " + m_where);
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
