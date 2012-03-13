using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Voodoo.Cache;
using Voodoo.IO;

namespace KCMDCollector.Book
{
    public class RulesOperate
    {
        /// <summary>
        /// 当前系统路径
        /// </summary>
        private static string LD = System.Environment.CurrentDirectory;

        private static string CFG = System.Environment.CurrentDirectory + "\\Config";

        #region 起点规则
        /// <summary>
        /// 获取起点规则
        /// </summary>
        /// <returns></returns>
        public static QidianRule GetQidianRule()
        {
            if (Cache.GetCache("QidianRule") == null)
            {
                Cache.SetCache("QidianRule",
                     (QidianRule)XML.DeSerialize(typeof(QidianRule), File.Read(CFG + "\\QidianRule.xml")),
                     CFG + "\\QidianRule.xml"
                     );
            }
            return (QidianRule)Cache.GetCache("QidianRule");
        }

        /// <summary>
        /// 保存起点规则
        /// </summary>
        /// <param name="Rule"></param>
        public static void SaveQidianRule(QidianRule Rule)
        {
            XML.SaveSerialize(Rule, CFG + "\\QidianRule.xml");
        }
        #endregion

        #region 采集规则
        /// <summary>
        /// 获取采集规则
        /// </summary>
        /// <returns></returns>
        public static List<CollectRule> GetBookRules()
        {
            if (Cache.GetCache("BookRules") == null)
            {
                Cache.SetCache("BookRules",
                     (List<CollectRule>)XML.DeSerialize(typeof(List<CollectRule>), File.Read(CFG + "\\BookRules.xml")),
                     CFG + "\\BookRules.xml"
                     );
            }
            return (List<CollectRule>)Cache.GetCache("BookRules");
        }

        /// <summary>
        /// 保存采集规则
        /// </summary>
        /// <param name="Rules"></param>
        public static void SaveBookRules(List<CollectRule> Rules)
        {
            XML.SaveSerialize(Rules, CFG + "\\BookRules.xml");
        }
        #endregion

        #region 过滤规则
        /// <summary>
        /// 获取过滤规则
        /// </summary>
        /// <returns></returns>
        public static List<string> GetFilter()
        {
            if (Cache.GetCache("Filter") == null)
            {
                Cache.SetCache("Filter",
                     File.Read(CFG+"\\Filter.txt"),
                     CFG + "\\Filter.txt"
                 );
            }
            string[] fi = Cache.GetCache("Filter").ToString().Split('\n');
            List<string> result = new List<string>();
            foreach (string f in fi)
            {
                result.Add(f.Replace("\r", ""));
            }
            return result;
        }

        /// <summary>
        /// 保存过滤规则
        /// </summary>
        /// <param name="Filter"></param>
        public static void SaveFilter(List<string> Filter)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string str in Filter)
            {
                sb.AppendLine(str);
            }
            File.Write(CFG + "\\Filter.txt", sb.ToString());
        }
        #endregion

        #region 系统参数
        /// <summary>
        /// 获取系统设置
        /// </summary>
        /// <returns></returns>
        public static Setting GetSetting()
        {

            if (Cache.GetCache("Setting") == null)
            {
                Cache.SetCache("Setting",
                     (Setting)XML.DeSerialize(typeof(Setting), File.Read(CFG + "\\Setting.xml")),
                     CFG + "\\Setting.xml"
                     );
            }
            return (Setting)Cache.GetCache("Setting");
        }

        public static void SaveSetting(Setting set)
        {
            XML.SaveSerialize(set, CFG + "\\Setting.xml");
        }
        #endregion

        #region 书籍列表
        /// <summary>
        /// 获取书籍列表
        /// </summary>
        /// <returns></returns>
        public static string[] GetBooks()
        {
            if (Cache.GetCache("Books") == null)
            {
                Cache.SetCache("Books",
                     File.Read(CFG + "\\Books.txt"),
                     CFG + "\\Books.txt"
                 );
            }
            return  Cache.GetCache("Books").ToString().Split('\n');
        }

        /// <summary>
        /// 保存过滤规则
        /// </summary>
        /// <param name="Books"></param>
        public static void SaveBooks(string[] Books)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string str in Books)
            {
                sb.AppendLine(str);
            }
            File.Write(CFG + "\\Books.txt", sb.ToString());
        }
        #endregion


        #region 邮件发博规则

        public static void  SaveMailBlogRule(List<MailBlog> ss)
        {
            XML.SaveSerialize(ss, CFG + "\\MailBlog.xml");
        }

        public static List<MailBlog> GetMailBlogRule()
        {
            if (Cache.GetCache("MailBlog") == null)
            {
                Cache.SetCache("MailBlog",
                     (List<MailBlog>)XML.DeSerialize(typeof(List<MailBlog>), File.Read(CFG + "\\MailBlog.xml")),
                     CFG + "\\MailBlog.xml"
                     );
            }
            return (List<MailBlog>)Cache.GetCache("MailBlog");
        } 
        #endregion

        #region  发博规则
        public static void SaveBlogModel(List<BlogModel> ss)
        {
            XML.SaveSerialize(ss, CFG + "\\BlogModel.xml");
        }

        public static List<BlogModel> GetBlogModel()
        {
            if (Cache.GetCache("BlogModel") == null)
            {
                Cache.SetCache("BlogModel",
                     (List<BlogModel>)XML.DeSerialize(typeof(List<BlogModel>), File.Read(CFG + "\\BlogModel.xml")),
                     CFG + "\\BlogModel.xml"
                     );
            }
            return (List<BlogModel>)Cache.GetCache("BlogModel");
        }

        #endregion 
    }
}
