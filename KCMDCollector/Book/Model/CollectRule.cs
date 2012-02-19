using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KCMDCollector.Book
{
    public class CollectRule
    {
        /// <summary>
        /// 默认规则
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// 站点名称
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Chaset编码
        /// </summary>
        public string CharSet { get; set; }

        /// <summary>
        /// 搜索页地址
        /// </summary>
        public string SearchPageUrl { get; set; }

        /// <summary>
        /// 搜索参数
        /// </summary>
        public string SearchPars { get; set; }

        /// <summary>
        /// 书籍信息页地址
        /// </summary>
        public string BookInfoUrl { get; set; }

        /// <summary>
        /// 书籍信息规则
        /// </summary>
        public string BookInfoRule { get; set; }

        /// <summary>
        /// 章节列表页地址规则
        /// </summary>
        public string ChapterListUrl { get; set; }

        /// <summary>
        /// 章节名和地址规则
        /// </summary>
        public string ChapterNameAndUrl { get; set; }

        /// <summary>
        /// 内容规则
        /// </summary>
        public string ChapterContent { get; set; }
    }
}
