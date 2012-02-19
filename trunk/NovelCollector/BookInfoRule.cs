using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NovelCollector
{
    /// <summary>
    /// 在起点获取书籍和章节列表
    /// </summary>
    public class BookInfoRule
    {
        public string CharSet { get; set; }

        public string SearchRefer { get; set; }

        public string SearchPageUrl { get; set; }

        public string BookInfoUrl { get; set; }

        public string BookInfo { get; set; }

        public string ChapterListUrl { get; set; }

        public string ChapterTitle { get; set; }

        public string GoogleDomain { get; set; }

        public string GoogleCharSet { get; set; }

        public string ChapterUrl { get; set; }

        public string TargetSite { get; set; }


        //主站采集规则

        /// <summary>
        /// 首页
        /// </summary>
        public string mDomain { get; set; }

        /// <summary>
        /// 搜索页
        /// </summary>
        public string mSearchPageUrl { get; set; }

        /// <summary>
        /// 搜索参数
        /// </summary>
        public string mSearchPar { get; set; }

        /// <summary>
        /// 列表页地址
        /// </summary>
        public string mUrl { get; set; }

        /// <summary>
        /// 章节列表页上的章节标题 地址
        /// </summary>
        public string mChapter { get; set; }

        /// <summary>
        /// 章节内容
        /// </summary>
        public string mContent { get; set; }


        public string mCharSet { get; set; }
    }
}
