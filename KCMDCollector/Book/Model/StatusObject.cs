using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KCMDCollector.Book
{
    public class StatusObject
    {
        /// <summary>
        /// 书籍名称
        /// </summary>
        public string BookTitle { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 章节
        /// </summary>
        public string ChapterTitle { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
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
