using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KCMDCollector.Book
{
    public class CollectBook : CollectBase
    {
        public MainForm Parent { get; set; }

        /// <summary>
        /// 实例化类
        /// </summary>
        /// <param name="parent"></param>
        public CollectBook(MainForm parent)
        {
            this.Parent = parent;
            this.CollectStatus = new StatusObject();
        }

        /// <summary>
        /// 状态变化
        /// </summary>
        protected override void Status_Chage()
        {
            Parent.Invoke(new MethodInvoker(delegate
            {
                try
                {
                    //Parent.Text = this.CollectStatus.ChapterTitle;
                    Parent.label1.Text = this.CollectStatus.BookTitle;
                    Parent.label2.Text = this.CollectStatus.ChapterTitle;
                    Parent.label3.Text = this.CollectStatus.Status;

                    Parent.progress_Book.Maximum = this.CollectStatus.BookCount;
                    Parent.progress_Book.Value = this.CollectStatus.BookCount - this.CollectStatus.BookLeftCount;

                    Parent.progress_Chapter.Maximum = this.CollectStatus.ChapterCount;
                    Parent.progress_Chapter.Value = this.CollectStatus.ChapterCount - this.CollectStatus.ChapterleftCout;
                }
                catch
                {}
                
            }));
        }
    }
}
