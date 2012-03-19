using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KCMDCollector.Book
{
    public class Chapter
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 是否VIP章节
        /// </summary>
        public bool IsVip { get; set; }

        public bool IsImageChapter { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Index { get; set; }

        #region 字数
        /// <summary>
        /// 字数
        /// </summary>
        public int Length
        {
            get
            {
                if(this.Content==null)
                {
                    return 0;
                }
                else
                {
                    return Content.Length;
                }

            }
        }
        #endregion
    }
}
