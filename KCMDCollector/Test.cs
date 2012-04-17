using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Web;
using System.Net;
using Voodoo.Net.XmlRpc;
using System.Collections.Specialized;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

using System.Threading;
using Voodoo;
using System.Text;
using Voodoo.Net.BlogHelper;
using Voodoo.Model;

namespace KCMDCollector
{
    public partial class Test : Form
    {
        protected bool IsCompleted { get; set; }

        public Test()
        {
            InitializeComponent();
        }



        private void btn_sina_Click(object sender, EventArgs e)
        {


            Voodoo.Net.BlogHelper.Sina s = new Voodoo.Net.BlogHelper.Sina("bigcuibing@tom.com", "Admin@123", "http://blog.sina.com.cn/aizrnet/");
            s.Login();
            //s.Post("测试自动发送", "自动发送的内容自动发送的内容自动发送的内容");
            var ps = s.GetRecentPosts(100);


            var p = s.GetPost(ps.First().id);

            MessageBox.Show(p.Content);


        }

        private void btn_Baidu_Click(object sender, EventArgs e)
        {

            Voodoo.Net.BlogHelper.Sohu s = new Voodoo.Net.BlogHelper.Sohu("kuibono@sohu.com", "4264269");
            s.Login();
            s.Post("测试自动发送", "自动发送的内容自动发送的内容自动发送的内容");




        }

        private void btn_Neasy_Click(object sender, EventArgs e)
        {

            Voodoo.Net.BlogHelper.Neasy n = new Voodoo.Net.BlogHelper.Neasy("aizrnet@163.com", "Admin@123");
            n.Login();
            n.Post("测试自动发送", "自动发送的内容自动发送的内容自动发送的内容");

        }

        private void btn_Cnblogs_Click(object sender, EventArgs e)
        {
            //获取登录表单
            var flResult = Voodoo.Net.Url.PostGetCookieAndHtml(new NameValueCollection(),
                "http://passport.cnblogs.com/login.aspx?ReturnUrl=http://www.cnblogs.com/",
                Encoding.UTF8,
                new System.Net.CookieContainer(),
                "http://www.cnblogs.com");

            //登陆
            NameValueCollection nv = flResult.Html.SerializeForm("#frmLogin");
            nv["tbPassword"] = "4264269";
            nv["tbUserName"] = "kuibono";
            nv["__EVENTTARGET"] = "btnLogin";

            var lResult = Voodoo.Net.Url.PostGetCookieAndHtml(nv,
                "http://passport.cnblogs.com/login.aspx?ReturnUrl=http%3a%2f%2fwww.cnblogs.com%2f",
                Encoding.UTF8,
                new System.Net.CookieContainer(),
                "http://passport.cnblogs.com/login.aspx?ReturnUrl=http://www.cnblogs.com/");

            //获取表单
            var fResult = Voodoo.Net.Url.PostGetCookieAndHtml(new NameValueCollection(),
                "http://www.cnblogs.com/kuibono/admin/EditPosts.aspx?opt=1",
                Encoding.UTF8,
                lResult.cookieContainer,
                "http://www.cnblogs.com");

            NameValueCollection aha = fResult.Html.SerializeForm("#frmMain");
            aha["Editor$Edit$EditorBody"] = "文章内容文章内容文章内容";
            aha["Editor$Edit$txbTitle"] = " 测试标题";
            aha["Editor$Edit$lkbPost"] = "发布";
            aha["name_site_categroy"] = "";

            aha.Add("Editor$Edit$Advanced$chkComments", "on");
            aha.Add("Editor$Edit$Advanced$chkMainSyndication", "on");
            aha.Add("Editor$Edit$Advanced$ckbPublished", "on");
            aha.Add("Editor$Edit$Advanced$tbEnryPassword", "");
            aha.Add("Editor$Edit$Advanced$txbEntryName", "");
            aha.Add("Editor$Edit$Advanced$txbExcerpt", "");
            aha.Add("Editor$Edit$Advanced$txbTag", "");
            aha.Add("Editor$Edit$APOptions$APSiteHome$chkDisplayHomePage", "on");
            aha.Add("Editor$Edit$EditorBody$ClientState", "");
            aha.Add("Editor$Edit$EditorBody$PostBackHandler", "");


            //发布
            var pResult = Voodoo.Net.Url.PostGetCookieAndHtml(aha,
                "http://www.cnblogs.com/kuibono/admin/EditPosts.aspx?opt=1",
                Encoding.UTF8,
                lResult.cookieContainer,
                "http://www.cnblogs.com/kuibono/admin/EditPosts.aspx?opt=1");

            richTextBox1.Text = pResult.Html;
        }

        private void btn_Baidu2_Click(object sender, EventArgs e)
        {
            Voodoo.Net.Connection.Baidu baidu = new Voodoo.Net.Connection.Baidu("崔冰", "4264269");
            baidu.Login();


        }

        private void btn_Renren_Click(object sender, EventArgs e)
        {
            Voodoo.Net.BlogHelper.Renren r = new Voodoo.Net.BlogHelper.Renren("kuibono@163.com", "123456");
            r.Login();
            r.Post("大大大大大大", "惺惺惜惺惺惺惺惜惺惺谢谢惺惺惜惺惺惺惺惜惺惺谢谢");

        }

        private void btn_Blogbus_Click(object sender, EventArgs e)
        {
            NameValueCollection nv = new NameValueCollection();
            nv.Add("username", "aizrnet");
            nv.Add("password", "Admin@123");
            var lResult = Voodoo.Net.Url.PostGetCookieAndHtml(nv,
                "http://passport.blogbus.com/login?goto=http%3A%2F%2Fwww.blogbus.com%2Fuser%2F",
                Encoding.UTF8,
                new System.Net.CookieContainer(),
                "http://www.blogbus.com/",
                true
                );

            //http://blog.home.blogbus.com/posts/form

            Voodoo.Net.Url.GetHtml("http://blog.home.blogbus.com/posts/form", "UTF-8", lResult.cookieContainer);

            var fResult = lResult.Click("<a class=\"submenuNewPost\" href=\"(?<key>.*?)\">写新日志</a>", Encoding.UTF8);


            string action = fResult.Html.FindString("<form method=\"post\" action=\"(?<key>.*?)\" name=\"EditorForm\">").AppendToDomain("http://blog.home.blogbus.com/");

            var aha = fResult.Html.SerializeForm("@EditorForm");
            aha["content"] = "博客内容博客内容博客内容博客内容";
            aha["tags"] = "标签";
            aha["title"] = "测试标题";

            Voodoo.Net.Url.PostGetCookieAndHtml(aha,
                action,
                Encoding.UTF8,
                lResult.cookieContainer);

        }

        private void btn_Wp_Click(object sender, EventArgs e)
        {

            Voodoo.Net.BlogHelper.WordPress wp = new Voodoo.Net.BlogHelper.WordPress("http://aizr.net/wiki/", "kuibono", "4264269");
            wp.Login();
            //wp.Post("测试标题", "文章内容文章内容文章内容文章内容");
            var ps = wp.GetRecentPosts(100);

            string str = "";
            foreach (var p in ps)
            {
                str += p.Title + ",";
            }
            MessageBox.Show(str);

        }

        private void btn_CreateBook_Click(object sender, EventArgs e)
        {
            Voodoo.Basement.Client.BookHelper BH = new Voodoo.Basement.Client.BookHelper("http://aizr.net/");
            List<Voodoo.Model.Book> bs = (List<Voodoo.Model.Book>)Voodoo.IO.XML.DeSerialize(typeof(List<Voodoo.Model.Book>), Voodoo.Net.Url.GetHtml("http://aizr.net/e/api/xmlrpc.aspx?A=booksearch", "utf-8"));
            foreach (var b in bs)
            {
                BH.CreateBook(b.ID);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Voodoo.Basement.Client.BookHelper BH = new Voodoo.Basement.Client.BookHelper("http://zuoaiai.net/");
            Book.CollectBook cb = new Book.CollectBook(new MainForm());

            var books = BH.SearchBook("", "", "");
            foreach (var book in books)
            {
                //cb.UploadBookFace(book);
                Voodoo.IO.ImageHelper.MakeThumbnail(GetRandImage(),
                                    System.Environment.CurrentDirectory + "\\face.jpg",
                                    120,
                                    150,
                                    "Cut");
                Voodoo.IO.ImageHelper.AddTextToImg(System.Environment.CurrentDirectory + "\\face.jpg", book.Title, System.Environment.CurrentDirectory + "\\stand.jpg");
                BH.SetBookFace(book.ID, System.Environment.CurrentDirectory + "\\stand.jpg");
                BH.CreateBook(book.ID);
            }



        }

        public string GetRandImage()
        {
            DirectoryInfo dir = new DirectoryInfo(System.Environment.CurrentDirectory + "\\girl");
            var file = dir.GetFiles().Where(p => p.FullName.EndsWith(".jpg")).ToList();

            return file[@int.GetRandomNumber(0, file.Count - 1)].FullName;
        }

        private void Tieba_Click(object sender, EventArgs e)
        {
            testc();

        }

        protected void testc()
        {
            Voodoo.Basement.Client.BookHelper bh = new Voodoo.Basement.Client.BookHelper("http://aizr.net/");
            var books = bh.SearchBook("权财", "", "");
            foreach (var book in books)
            {
                var chapters = bh.ChapterSearch(book.Title, "", true);//获取所有图片章节
                if (chapters.Count == 0)
                {
                    continue;
                }

                foreach (var c in chapters)
                {
                    string url = SearchChapterFromTieba(book.Title + " " + c.Title);
                    Thread.Sleep(1000);
                    if (url.Length == 0)
                    {
                        continue;
                    }

                    string content = GetContentFromTieba(url);
                    Thread.Sleep(800);
                    content = Filter(content);
                    if (content.Length > 0)
                    {

                        bh.ChapterEdit(c.ID, c.Title, content, false);
                    }

                }
                bh.CreateChapters(book.ID);

            }
        }

        #region 从贴吧按照标题搜索
        /// <summary>
        /// 从贴吧按照标题搜索
        /// </summary>
        /// <param name="Title">标题</param>
        /// <returns>地址</returns>
        protected string SearchChapterFromTieba(string Title)
        {
            //if (!Regex.IsMatch(Title, "第[1234567890一二三四五六七八九〇零十千百万两壹贰叁肆伍陆柒捌玖]*?章"))
            if (!Regex.IsMatch(Title, ".*?第*?[1234567890一二三四五六七八九〇零十千百万两壹贰叁肆伍陆柒捌玖-]+?章*?[\\w]+?.+"))
            {
                return "";
            }

            Title = StandardTitle(Title);

            string str_EncodeTitle = Title.UrlEncode(Encoding.GetEncoding("gb2312"));
            //string str_url = string.Format("http://www.baidu.com/s?tn=baiduhome_pg&bs={0}&f=8&rsv_bp=1&rsv_spt=1&wd={0}+site%3Atieba.baidu.com&inputT=6519", str_EncodeTitle);

            string str_url = "http://tieba.baidu.com/f/search/res?ie=utf-8&qw=" + Title;
            string html = Voodoo.Net.Url.GetHtml(str_url, "gbk");
            List<Book.Chapter> cs = new List<Book.Chapter>();


            //Match m = html.GetMatchGroup("<span class=\"p_title\"><a href=\"(?<url>.*?)\" class=\"bluelink\" target=\"_blank\" >(?<title>.*?)</a></span>");
            Match m = html.GetMatchGroup("<h3 class=\"t\"><a onmousedown=\".*?\" href=\"(?<url>.*?)\"target=\"_blank\">(?<title>.*?)</a>");
            while (m.Success)
            {
                cs.Add(new Book.Chapter()
                {
                    Title = StandardTitle(m.Groups["title"].Value),
                    Url = m.Groups["url"].Value.AppendToDomain(str_url)
                });
                m = m.NextMatch();
            }

            var chapter_NeedCollect = (from n in cs select new { n.Index, n.Url, n.Length, n.Title, n.Content, weight = n.Title.GetSimilarityWith(Title) }).OrderByDescending(p => p.weight).ToList();

            if (chapter_NeedCollect.Count() > 0 && chapter_NeedCollect.First().weight > (0.25).ToDecimal())//相似度大于0.3的才进行采集
            {
                return Regex.Replace(chapter_NeedCollect.First().Url, "\\?pn=.*?", "");
            }
            return "";
        }
        #endregion

        #region 从贴吧获得内容
        /// <summary>
        /// 从贴吧获得内容
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        protected string GetContentFromTieba(string Url)
        {
            string html = Voodoo.Net.Url.GetHtml(Url, "gbk");
            Match m = html.GetMatchGroup("<p id=\"post_content_.*?\" class=\"d_post_content\">(?<key>[\\s\\S]*?)</div>");

            string result = "";
            while (m.Success)
            {
                string v = m.Groups["key"].Value;
                if (v.Length < 300)
                {
                    m = m.NextMatch();
                    continue;
                }
                if (v.Contains("<img") || v.Contains("div>"))
                {
                    m = m.NextMatch();
                    continue;
                }
                //总连载贴
                if (v.Contains("总连载贴："))
                {
                    m = m.NextMatch();
                    continue;
                }
                if (v.ToLower().CountString("<br") + v.ToLower().CountString("<p") < 3)
                {
                    m = m.NextMatch();
                    continue;
                }

                result += v;

                m = m.NextMatch();
            }
            return result;
        }
        #endregion

        #region  过滤
        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        protected string Filter(string content)
        {
            content = content.Replace("【启航】", "");
            content = content.Replace("【更新组】", "");
            content = content.Replace("【启航更新组】", "");
            content = Regex.Replace(content, "【.*?更新.*?】", "");
            content = Regex.Replace(content, "<a .*?</a>", "");
            return content;
        }
        #endregion

        #region 标题标准化
        /// <summary>
        /// 标题标准化
        /// </summary>
        /// <param name="Title"></param>
        /// <returns></returns>
        protected string StandardTitle(string Title)
        {
            Title = Regex.Replace(Title, "&.{2,10};", "");
            Title = Regex.Replace(Title, "\\(.*?\\).*?", "");
            Title = Regex.Replace(Title, "（.*?）.*", "");
            Title = Regex.Replace(Title, "[`~@#$%^&*()_+:;'|><?,./]{1,}", "");
            return Title;
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            //Voodoo.Net.BlogHelper.WordPress wp = new WordPress("http://www.aizr.net/wiki/", "kuibono", "4264269");
            //wp.Login();
            //wp.Post(new Post()
            //{
            //    Class = "测试",
            //    Content = "xml-rpc测试发送内容",
            //    CreateTime = DateTime.Now,
            //    Tags = "标签".Split(','),
            //    Title = "测试标题"


            //});

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.fuck.com/e/api/xmlrpcV2.aspx");
            request.Method = "Post";
            request.ContentType = "application/x-www-form-urlencoded; charset=GBK";

            methodCall mc = new methodCall();
            mc.methodName = "BookExit";
            mc.@params = new List<param>();
            mc.@params.Add(new param { value = "1=1".SerializeToXML(), type = "System.String" });

            string str_data = Voodoo.IO.XML.Serialize(mc);
            request.ContentLength = str_data.Length;
            byte[] data = Encoding.UTF8.GetBytes(str_data);

            Stream newStream = request.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            HttpWebResponse responseSorce = (HttpWebResponse)request.GetResponse();
            Stream stream = responseSorce.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string content = reader.ReadToEnd();

            stream.Close();

            methodResponse mr = (methodResponse)content.DeSerializeTo(typeof(methodResponse));

            var result = mr.result.DeSerializeTo(Type.GetType(mr.type));

            //Voodoo.Net.XmlRpc.methodCall mc = new Voodoo.Net.XmlRpc.methodCall();
            //mc.methodName = "method.name";
            //mc.@params = new List<Voodoo.Net.XmlRpc.param>();
            //mc.@params.Add(new Voodoo.Net.XmlRpc.param() { value = new Voodoo.Net.XmlRpc.ttring() { @string = "kuibono" } });
            //mc.@params.Add(new Voodoo.Net.XmlRpc.param() { value = new Voodoo.Net.XmlRpc.ttring() { @string = "4264269" } });
            //string s = Voodoo.IO.XML.Serialize(mc);



            //var d = (Voodoo.Net.XmlRpc.methodCall)Voodoo.IO.XML.DeSerialize(typeof(Voodoo.Net.XmlRpc.methodCall), str_data);

            //AlexJamesBrown.JoeBlogs.WordPressWrapper wp = new AlexJamesBrown.JoeBlogs.WordPressWrapper("http://www.fuck.com/e/test.aspx", "kuibono", "4264269");

            //Voodoo.Basement.Client.RpcBookHelper bh = new Voodoo.Basement.Client.RpcBookHelper();
            //bh.test();
        }
    }
}
