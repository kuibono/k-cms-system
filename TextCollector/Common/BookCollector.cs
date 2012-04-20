using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TextCollector.Common
{
    class BookCollector : CollectBase
    {
        public FormMain Parent;

        public BookCollector(FormMain parent)
        {
            this.Parent = parent;
        }

        protected override void Status_Chage()
        {
            //throw new NotImplementedException();
            Parent.Invoke(new MethodInvoker(delegate
            {
                try
                {
                    Parent.lb_Book.Text = this.status.BookTitle;
                    Parent.lb_Chapter.Text = this.status.ChapterTitle;
                    Parent.lb_Status.Text = this.status.Status;
                }
                catch
                { }
            }));
        }
    }
}
