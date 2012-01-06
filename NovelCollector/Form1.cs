using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;

using Voodoo;
using Voodoo.Model;
using Voodoo.DAL;

using System.Threading;

using System.Text.RegularExpressions;


namespace NovelCollector
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = Voodoo.Net.Url.GetHtml(textBox1.Text, "gbk");
        }

        protected string Domain = "http://soso888.net/";
        //protected string Domain = "http://localhost/";

        #region 根据书籍地址采集
        /// <summary>
        /// 根据书籍地址采集
        /// </summary>
        /// <param name="url"></param>
        /// <param name="InfoRegex"></param>
        /// <param name="listRegex"></param>
        /// <param name="ContentRegex"></param>
        protected void GetBookByUrl(string url, string InfoRegex, string listRegex, string ContentRegex)
        {
            string info_html = Voodoo.Net.Url.GetHtml(url, "gbk");
            Match m = new Regex(InfoRegex).Match(info_html);
            if (m.Success)
            {
                BookInfo b = new BookInfo();
                b.Author = m.Groups["author"].Value;
                b.Class = m.Groups["class"].Value;
                b.Intro = m.Groups["intro"].Value;
                b.Length = m.Groups["length"].Value.ToInt32();
                b.ListUrl = "http://" + new Uri(url).Host + m.Groups["url"].Value;
                b.Title = m.Groups["title"].Value;

                Class cls = (Class)Voodoo.IO.XML.DeSerialize(typeof(Class), Voodoo.Net.Url.GetHtml(Domain + "e/api/getClass.aspx?class="+b.Class, "utf-8"));

                bool bookExist = (bool)Voodoo.IO.XML.DeSerialize(typeof(bool), Voodoo.Net.Url.GetHtml(Domain + "e/api/BookExist.aspx?title=" + b.Title+"&author="+b.Author, "utf-8"));
                Book book = new Book();
                if (!bookExist)
                {

                    //创建这本书
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add("title", b.Title);
                    nv.Add("author", b.Author);
                    nv.Add("classid", cls.ID.ToS());
                    nv.Add("intro", b.Intro);
                    nv.Add("length", b.Length.ToS());

                    book = (Book)Voodoo.IO.XML.DeSerialize(typeof(Book), Voodoo.Net.Url.Post(nv, Domain + "e/api/BookAdd.aspx",Encoding.UTF8));
                }
                else
                {
                    book = (Book)Voodoo.IO.XML.DeSerialize(typeof(Book), Voodoo.Net.Url.GetHtml(Domain + "e/api/getBook.aspx?title=" + b.Title + "&author=" + b.Author, "utf-8"));
                }
                if (book.ID <= 0)//书籍添加失败或者没有这本书
                {
                    return;
                }

                //获取章节列表
                List<ChapterList> cs = new List<ChapterList>();

                string html = Voodoo.Net.Url.GetHtml(b.ListUrl, "gbk");
                Match m_list_Chapter = new Regex(listRegex).Match(html);
                int i = 0;

                while (m_list_Chapter.Success)
                {
                    cs.Add(new ChapterList() { Url = b.ListUrl + m_list_Chapter.Groups["key"].Value, Title = m_list_Chapter.Groups["key2"].Value, Index = i });
                    i++;
                    m_list_Chapter = m_list_Chapter.NextMatch();
                }

                //判断哪些没有采集
                BookChapter bc = (BookChapter)Voodoo.IO.XML.DeSerialize(typeof(BookChapter), Voodoo.Net.Url.GetHtml(Domain + "e/api/GetChapter.aspx?id="+book.LastChapterID, "utf-8"));
                if (bc.ID > 0)
                {
                    //这本书在数据库中有章节 找出这个章节在列表中的位置
                    ChapterList cl = cs.Where(p => p.Title == bc.Title).First();

                    //把这个位置之前的全都去掉
                    cs = cs.Where(p => p.Index > cl.Index).ToList();

                }

                //开始采集
                foreach (ChapterList cp in cs)
                {
                    html = Voodoo.Net.Url.GetHtml(cp.Url, "gbk");
                    Match m_content = new Regex(ContentRegex).Match(html);
                    while (m_content.Success)
                    {
                        string content = m_content.Groups["key"].Value;
                        content = Regex.Replace(content, "<script[\\s\\S]*?</script>", "").TrimDbDangerousChar();
                        content = Regex.Replace(content, "<a[\\s\\S]*?</a>", "");
                        content = Regex.Replace(content, "\\([\\s\\S]{3,50}?\\)", "");
                        content = Regex.Replace(content, "\\[[\\s\\S]{3,50}?\\]", "");
                        content = Regex.Replace(content, "{[\\s\\S]{3,50}?}", "");
                        content = Regex.Replace(content, "「[\\s\\S]{3,50}?」", "");
                        content = Regex.Replace(content, "『[\\s\\S]{3,50}?』", "");
                        content = Regex.Replace(content, "〖[\\s\\S]{3,50}?〗", "");
                        content = Regex.Replace(content, "【[\\s\\S]{3,50}?】", "");
                        content = Regex.Replace(content, "（[\\s\\S]{3,50}?）", "");
                        content = Regex.Replace(content, "www[\\s\\S]{3,10}?com", "");
                        content = Regex.Replace(content, "www[\\s\\S]{3,10}?net", "");
                        content = Regex.Replace(content, "www[\\s\\S]{3,10}?org", "");
                        content = Regex.Replace(content, "www[\\s\\S]{3,10}?cn", "");
                        content = Regex.Replace(content, "年夜", "大");
                        content = Regex.Replace(content, "/a", "");

                        NameValueCollection nv = new NameValueCollection();
                        nv.Add("bookid", book.ID.ToS());
                        nv.Add("booktitle", book.Title);
                        nv.Add("classid", cls.ID.ToS());
                        nv.Add("classname", cls.ClassName);
                        nv.Add("content", content);
                        nv.Add("title", cp.Title);

                        BookChapter  nouse=(BookChapter)Voodoo.IO.XML.DeSerialize(typeof(BookChapter),Voodoo.Net.Url.Post(nv, Domain + "e/api/ChapterAdd.aspx", Encoding.UTF8));
                        if (nouse.ID <= 0)//如果章节采集失败，则跳出这本书的采集，进行下一本书
                        {
                            return;
                        }


                        m_content = m_content.NextMatch();
                    }
                }
                //采集完成 
                //开始生成
                if (cs.Count > 0)
                {
                    Voodoo.Net.Url.GetHtml(Domain+"e/api/CreatePage.aspx?action=createindex");

                    Voodoo.Net.Url.GetHtml(Domain+"e/api/CreatePage.aspx?action=createclasspage");

                    Voodoo.Net.Url.GetHtml(Domain + "e/api/CreatePage.aspx?action=createbook&id=" + book.ID);

                    Voodoo.Net.Url.GetHtml(Domain + "e/api/CreatePage.aspx?action=createchapters&id=" + book.ID);
                }

            }

        }
        #endregion

        protected void collect()
        {
            foreach (var item in listBox1.Items)
            {
                try
                {
                    GetBookByUrl(item.ToS(), txt_InfoReg.Text, textBox2.Text, textBox3.Text);
                }
                catch
                {
                    
                }
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            GetBookByUrl(textBox1.Text, txt_InfoReg.Text, textBox2.Text, textBox3.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] urls = Voodoo.IO.File.Read(System.Environment.CurrentDirectory + "\\list.txt").Split('\n');
            foreach (string str in urls)
            {
                listBox1.Items.Add(str);
            }
        }

        protected bool SysBusy = false;

        Thread th_Collect;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (SysBusy == false)
            {
                SysBusy = true;
                if (th_Collect == null || th_Collect.IsAlive == false)
                {
                    th_Collect = new Thread(collect);
                    th_Collect.Start();
                }
                SysBusy = false;

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //CollectRule r = new CollectRule();
            //r.BookUrls = new List<string>();
            //string[] urls = Voodoo.IO.File.Read(System.Environment.CurrentDirectory + "\\list.txt").Split('\n');
            //foreach (string str in urls)
            //{
            //    r.BookUrls.Add(str.Trim());
            //}

            //r.BookInfo = txt_InfoReg.Text;
            //r.ChapterList = textBox2.Text;
            //r.Content = textBox3.Text;
            ////Voodoo.IO.XML.Serialize(r);
            //Voodoo.IO.File.Write("C:\\2.xml", Voodoo.IO.XML.Serialize(r));
            //Voodoo.IO.XML.SaveSerialize(r, "C:\\rule.xml");


            CollectRule rule = (CollectRule)Voodoo.IO.XML.DeSerialize(
               typeof(CollectRule),
               Voodoo.IO.File.Read("C:\\2.xml", Voodoo.IO.File.EnCode.UTF8)
               );
        }
    }
}
