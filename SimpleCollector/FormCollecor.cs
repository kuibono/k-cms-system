using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Voodoo;
using SimpleCollector.Common;
using System.IO;
using System.Threading;


namespace SimpleCollector
{
    public partial class FormCollecor : Form
    {
        public FormCollecor()
        {
            InitializeComponent();
        }

        private void FormCollecor_Load(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(System.Environment.CurrentDirectory + "\\Rules\\");
            var files = dir.GetFiles();
            foreach (var file in files)
            {
                comb_Rules.Items.Add(file.Name);
            }
        }

        private void btn_CreateRule_Click(object sender, EventArgs e)
        {
            FormRuleEdit fr = new FormRuleEdit();
            fr.Show();
        }

        RuleModel rm;
        private void btn_Start_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + "\\Rules\\" + comb_Rules.Text;
            rm = (RuleModel)Voodoo.IO.XML.DeSerialize(typeof(RuleModel), Voodoo.IO.File.Read(path));

            Thread t = new Thread(Start);
            t.Start();
            
        }

        protected void Start()
        {
            CollectBase cb = new CollectBase(this);
            cb.CollectBooks(rm.BookNeedCollect,
                rm.ListPageUrl,
                rm.NextPageUrl,
                rm.BookPageUrl,
                rm.BookInfo,
                rm.ChapterListPageUrl,
                rm.Encoding,
                rm.ChapterTitleAndUrl,
                rm.Content);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("../xxxx.html".AppendToDomain("http://baidu.com/book/"));
        }
    }
}
