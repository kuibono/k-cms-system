using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;


namespace SimpleCollector
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(collect);
            t.Start();
        }

        public void collect()
        {
            CollectBook cb = new CollectBook(this);
            cb.Collect(txt_ListUrl.Text, txt_BookTitle.Text, txt_ChapterTitleRule.Text, txt_ContentRule.Text);
        }
    }
}
