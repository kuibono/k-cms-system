using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KCMDCollector.Book
{
    public class BookAndChapter
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 书名
        /// </summary>
        public string BookTitle { get; set; }

        /// <summary>
        /// 类别ID
        /// </summary>
        public int ClassID { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        /// 章节列表
        /// </summary>
        public List<Chapter> Chapters { get; set; }

        /// <summary>
        /// 最新章节
        /// </summary>
        public Chapter LastChapter { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// 有更新，需要重新生成
        /// </summary>
        public bool Changed { get; set; }
    }
}
