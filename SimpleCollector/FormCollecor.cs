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
                rm.BookInfo.Replace("\n", ""),
                rm.ChapterListPageUrl,
                rm.Encoding,
                rm.ChapterTitleAndUrl,
                rm.Content.Replace("\n", ""),
                rm.NextContentUrl.Replace("\n", ""));
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Voodoo.Basement.Client.BookHelper bh = new Voodoo.Basement.Client.BookHelper("http://aizr.net");
            Voodoo.Basement.Client.BookHelper bh2 = new Voodoo.Basement.Client.BookHelper("http://zuoaiai.net");

            var books = bh.SearchBook("沧澜曲", "", "");

            foreach (var book in books)
            {
                var bs = bh2.SearchBook(book.Title, book.Author, "");
                Voodoo.Model.Book b;
                if (bs.Count == 0)
                {
                    var cls = bh2.GetClass("传世经典");

                    b = bh2.BookAdd(book.Title, book.Author, cls.ID, book.Intro, @int.GetRandomNumber(40000, 900000));
                }
                else
                {
                    b = bs.First();
                }

                var chapters = bh.ChapterSearch(b.Title, "", false);
                foreach (var chapter in chapters)
                {
                    if (bh2.ChapterSearch(b.Title, chapter.Title, false).Count == 0)
                    {
                        string content = bh.GetChapterContent(chapter.ID);
                        var c = bh2.ChapterAdd(b.ID, chapter.Title, content, false);
                    }
                    
                }
                bh2.CreateChapters(b.ID);
                bh2.CreateBook(b.ID);
            }
        }
    }
}
