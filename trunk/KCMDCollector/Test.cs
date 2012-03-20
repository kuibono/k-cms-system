using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Collections.Specialized;
using System.Web.Script.Serialization;

using System.Threading;
using Voodoo;
using System.Text;
using Voodoo.Net.BlogHelper;

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
            ////首先获取用户字符串和服务器时间

            //string Pass = "Admin@123";
            //string user = "bigcuibing@tom.com";

            //string u1 = "http://login.sina.com.cn/sso/prelogin.php?entry=miniblog&callback=sinaSSOController.preloginCallBack&user=" + user + "&client=ssologin.js(v1.3.12)";
            //string h1 = Voodoo.Net.Url.GetHtml(u1).Replace("sinaSSOController.preloginCallBack(", "").Replace(")", "");
            //string sina_retcode = h1.FindString("\"retcode\":(?<key>.*?),");
            //string sina_servertime = h1.FindString("\"servertime\":(?<key>.*?),");
            //string sina_pcid = h1.FindString("\"pcid\":\"(?<key>.*?)\",");
            //string sina_nonce = h1.FindString("\"nonce\":\"(?<key>.*?)\"");

            ////开始wsse加密
            //string p1 = Voodoo.Security.Encrypt.SHA1(Pass);
            //p1 = Voodoo.Security.Encrypt.SHA1(p1);
            //p1 = Voodoo.Security.Encrypt.SHA1(p1 + sina_servertime + sina_nonce);

            ////准备登陆
            //string loginUrl = "http://login.sina.com.cn/sso/login.php?client=ssologin.js(v1.3.12)";
            //NameValueCollection nv = new NameValueCollection();

            //nv.Add("service", "miniblog");
            //nv.Add("client", "ssologin.js%28v1.3.12%29");
            //nv.Add("entry", "miniblog");
            //nv.Add("encoding", "utf-8");
            //nv.Add("gateway", "1");
            //nv.Add("savestate", "0");
            //nv.Add("useticket", "1");
            //nv.Add("username", user);
            //nv.Add("servertime", sina_servertime);
            //nv.Add("nonce", sina_nonce);
            //nv.Add("pwencode", "wsse");
            //nv.Add("password", p1);
            //nv.Add("url", "http://www.aizr.net/");
            //nv.Add("returntype", "META");
            //nv.Add("ssosimplelogin", "1");
            //string U2 = "http://login.sina.com.cn/sso/login.php?entry=miniblog&gateway=1&from=referer%3Awww_index&savestate=0&useticket=0&su="+ user.ToEnBase64() +"&service=sso&servertime="+sina_servertime+"&nonce="+sina_nonce+"&pwencode=wsse&sp="+p1+"&encoding=UTF-8&callback=sinaSSOController.loginCallBack&cdult=3&domain=sina.com.cn&returntype=TEXT&client=ssologin.js(v1.3.19)&_="+myDateTime.GetUnixTime();
            //string pars = "service:miniblog&client:ssologin.js%28v1.3.12%29&entry:miniblog&encoding:utf-8&gateway:1&savestate:0&useticket:1&username:" + user + "&servertime:" + sina_servertime + "&nonce:" + sina_nonce + "&pwencode:wsse&password:" + p1 + "&url:http&//www.aizr.net/&returntype:META&ssosimplelogin:1";
            ////开始登陆

            //var result = Voodoo.Net.Url.PostGetCookieAndHtml(new NameValueCollection(),
            //    //loginUrl + "&" + pars,
            //    U2,
            //    Encoding.GetEncoding("gbk"),
            //    new System.Net.CookieContainer(),
            //    "http://www.sina.com.cn");

            ////打开页面表单
            //var form = Voodoo.Net.Url.PostGetCookieAndHtml(new NameValueCollection(),
            //    "http://control.blog.sina.com.cn/admin/article/article_add.php",
            //    Encoding.UTF8,
            //    result.cookieContainer,
            //    "http://www.sina.com.cn"
            //    );


            //NameValueCollection aha = form.Html.SerializeForm("#editorForm");
            //aha["blog_body"] = "博文内容博文内容博文内容博文内容博文内容博文内容";
            //aha["blog_title"] = "测试标题";
            //aha["blog_class"] = "1";
            //aha["stag"] = "标签";


            //var rPost = Voodoo.Net.Url.PostGetCookieAndHtml(aha,
            //    "http://control.blog.sina.com.cn/admin/article/article_post.php",
            //    Encoding.UTF8,
            //    result.cookieContainer,
            //    "http://control.blog.sina.com.cn/admin/article/article_add.php");

            Voodoo.Net.BlogHelper.Sina s = new Voodoo.Net.BlogHelper.Sina("bigcuibing@tom.com", "Admin@123","http://blog.sina.com.cn/aizrnet/");
            s.Login();
            //s.Post("测试自动发送", "自动发送的内容自动发送的内容自动发送的内容");
            var ps=s.GetRecentPosts(100);


            var p = s.GetPost(ps.First().id);

            MessageBox.Show(p.Content);


        }

        private void btn_Baidu_Click(object sender, EventArgs e)
        {
            //string url = "http://passport.sohu.com/sso/login.jsp?userid=kuibono%40sohu.com&password=2c8377f4a96899410636090ec1e699fe&appid=9999&persistentcookie=1&isSLogin=1&s=" + myDateTime.GetUnixTimestamp() + "&b=6&w=1280&pwdtype=1&v=26";

            //var Result = Voodoo.Net.Url.PostGetCookieAndHtml(new NameValueCollection(),
            //    url,
            //    Encoding.GetEncoding("GBK"),
            //    new System.Net.CookieContainer()
            //    );



            //NameValueCollection nv = new NameValueCollection();
            //nv.Add("allowComment", "0");
            //nv.Add("categoryId", "");
            //nv.Add("content", "博客内容博客内容测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试");
            //nv.Add("keywords", "关键词");
            //nv.Add("oper", "art_ok");
            //nv.Add("title", "测试标题");
            //nv.Add("vcode", "");
            //nv.Add("vcodeEn", "");

            //var r2 = Voodoo.Net.Url.PostGetCookieAndHtml(nv,
            //    "http://i.sohu.com/a/blog/home/entry/save.htm?_input_encode=UTF-8&_output_encode=UTF-8",
            //    Encoding.UTF8,
            //    Result.cookieContainer,
            //    "");
            Voodoo.Net.BlogHelper.Sohu s = new Voodoo.Net.BlogHelper.Sohu("kuibono@sohu.com", "4264269");
            s.Login();
            s.Post("测试自动发送", "自动发送的内容自动发送的内容自动发送的内容");




        }

        private void btn_Neasy_Click(object sender, EventArgs e)
        {
            //登陆

            //NameValueCollection nv = new NameValueCollection();
            //nv.Add("password", "4264269");
            //nv.Add("product", "163");
            //nv.Add("selected", "");
            //nv.Add("type", "1");
            //nv.Add("ursname", "");
            //nv.Add("username", "kuibono@163.com");

            //string loginUrl = "https://reg.163.com/logins.jsp";

            //var lResult = Voodoo.Net.Url.PostGetCookieAndHtml(nv,
            //    loginUrl,
            //    Encoding.UTF8,
            //    new System.Net.CookieContainer(),
            //    "http://www.163.com/"
            //    );

            ////打开表单

            //var fResult = Voodoo.Net.Url.PostGetCookieAndHtml(new NameValueCollection(),
            //    "http://kuibono.blog.163.com/blog/getBlog.do?&username=kuibono@163.com&myMailInfoWrite&fromblogurs",
            //    Encoding.UTF8,
            //    lResult.cookieContainer,
            //    "http://www.163.com/");

            //NameValueCollection aha = fResult.Html.SerializeForm(".ztag");
            //aha["HEContent"] = "内容内容内容内容内容内容内容";
            //aha["tag"] = "标签";
            //aha["title"] = "标题";

            ////发布
            //var pResult = Voodoo.Net.Url.PostGetCookieAndHtml(aha,
            //    "http://api.blog.163.com/kuibono/editBlogNew.do?p=1&n=1",
            //    Encoding.UTF8,
            //    lResult.cookieContainer,
            //    "http://kuibono.blog.163.com/blog/getBlog.do?&username=kuibono@163.com&myMailInfoWrite&fromblogurs");

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

            //var fResult = Voodoo.Net.Url.PostGetCookieAndHtml(new NameValueCollection(),
            //    "http://blog.home.blogbus.com/posts/form",
            //    Encoding.UTF8,
            //    lResult.cookieContainer,
            //    "http://www.blogbus.com",
            //    false
            //    );
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

            List<Voodoo.Model.Book> bs = (List<Voodoo.Model.Book>)Voodoo.IO.XML.DeSerialize(typeof(List<Voodoo.Model.Book>), Voodoo.Net.Url.GetHtml("http://aizr.net/e/api/xmlrpc.aspx?A=booksearch", "utf-8"));
            foreach (var b in bs)
            {
                Voodoo.Net.Url.GetHtml("http://aizr.net/e/api/xmlrpc.aspx?A=createchapters&bookid=" + b.ID, "utf-8");
                this.Invoke(new MethodInvoker(delegate{
                    this.richTextBox1.Text = b.Title;
                }));
            }
        }
    }
}
