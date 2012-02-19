using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KCMDCollector.Book
{
    public class QidianRule
    {
        /// <summary>
        /// CharSet
        /// </summary>
        public string CharSet { get; set; }

        /// <summary>
        /// 搜索页的Refer
        /// </summary>
        public string SearchRefer { get; set; }

        /// <summary>
        /// 搜索页的地址
        /// </summary>
        public string SearchPageUrl { get; set; }

        /// <summary>
        /// 章节列表地址规则
        /// </summary>
        public string ChapterListUrl { get; set; }

        /// <summary>
        /// 章节名称规则
        /// </summary>
        public string ChapterTitle { get; set; }
    }
}
