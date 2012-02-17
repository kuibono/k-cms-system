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

        public string BaiduCharSet { get; set; }

        public string ChapterUrl { get; set; }
    }
}
