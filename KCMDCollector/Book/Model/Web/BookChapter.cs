using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KCMDCollector.Book.Web
{
    ///<summary>
    ///表BookChapter的实体类
    ///</summary>
    public partial class BookChapter
    {
        /// <summary>
        /// 
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int BookID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BookTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long ClassID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long ValumeID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ValumeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsVipChapter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TextLength { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime UpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsTemp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsFree { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ChapterIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsImageChapter { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public string Content{get ; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ClickCount { get; set; }

        ///实体复制
        public BookChapter Clone()
        {
            return (BookChapter)this.MemberwiseClone();
        }
    }
}
