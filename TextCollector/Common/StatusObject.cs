using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextCollector.Common
{
    public class StatusObject
    {
        public string SourceSite { get; set; }

        public string BookTitle { get; set; }

        public string ChapterTitle { get; set; }

        public string Status { get; set; }

        /// <summary>
        /// 章节数量
        /// </summary>
        public int ChapterCount { get; set; }

        /// <summary>
        /// 剩余章节数量
        /// </summary>
        public int ChapterleftCout { get; set; }

        /// <summary>
        /// 书籍数量
        /// </summary>
        public int BookCount { get; set; }

        /// <summary>
        /// 剩余书籍数量
        /// </summary>
        public int BookLeftCount { get; set; }
    }
}
