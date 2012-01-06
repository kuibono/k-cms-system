using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using Voodoo;
using Voodoo.IO;
using Voodoo.Model;
using System.Threading;


namespace NoverCollectService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }
        CollectRule rule = new CollectRule();
        Log _log = new Log(System.AppDomain.CurrentDomain.BaseDirectory+"\\Log");
        protected override void OnStart(string[] args)
        {
            //throw new Exception(System.AppDomain.CurrentDomain.BaseDirectory);

            rule = (CollectRule)Voodoo.IO.XML.DeSerialize(
                typeof(CollectRule),
                Voodoo.IO.File.Read(System.AppDomain.CurrentDomain.BaseDirectory + "\\rules\\shouda8.xml", Voodoo.IO.File.EnCode.UTF8)
                );
        }

        protected override void OnStop()
        {
        }

        protected bool SysBusy = false;
        Thread th_Collect;

        private void timer2_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _log.Debug("跑！");
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

        /// <summary>
        /// 开始采集
        /// </summary>
        void collect()
        {
            List<string> urls = rule.BookUrls;
            foreach (string url in urls)
            {
                _log.Debug(url);
                GetBookByUrl(url, rule.BookInfo, rule.ChapterList, rule.Content);
            }
        }

        protected string Domain = "http://soso888.net/";

        

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
            if (info_html.Length == 0)
            {
                _log.Debug("打不开被采集网站的制定地址");
            }

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

                _log.Debug(b.Title);

                Class cls = (Class)Voodoo.IO.XML.DeSerialize(typeof(Class), Voodoo.Net.Url.GetHtml(Domain + "e/api/getClass.aspx?class=" + b.Class, "utf-8"));

                bool bookExist = (bool)Voodoo.IO.XML.DeSerialize(typeof(bool), Voodoo.Net.Url.GetHtml(Domain + "e/api/BookExist.aspx?title=" + b.Title + "&author=" + b.Author, "utf-8"));
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

                    book = (Book)Voodoo.IO.XML.DeSerialize(typeof(Book), Voodoo.Net.Url.Post(nv, Domain + "e/api/BookAdd.aspx", Encoding.UTF8));
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
                BookChapter bc = (BookChapter)Voodoo.IO.XML.DeSerialize(typeof(BookChapter), Voodoo.Net.Url.GetHtml(Domain + "e/api/GetChapter.aspx?id=" + book.LastChapterID, "utf-8"));
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

                        string nouse = Voodoo.Net.Url.Post(nv, Domain + "e/api/ChapterAdd.aspx", Encoding.UTF8);



                        m_content = m_content.NextMatch();
                    }
                }
                //采集完成 
                //开始生成
                if (cs.Count > 0)
                {
                    Voodoo.Net.Url.GetHtml(Domain + "e/api/CreatePage.aspx?action=createindex");

                    Voodoo.Net.Url.GetHtml(Domain + "e/api/CreatePage.aspx?action=createclasspage");

                    Voodoo.Net.Url.GetHtml(Domain + "e/api/CreatePage.aspx?action=createbook&id=" + book.ID);

                    Voodoo.Net.Url.GetHtml(Domain + "e/api/CreatePage.aspx?action=createchapters&id=" + book.ID);
                }

            }

        }
        #endregion


    }
}
