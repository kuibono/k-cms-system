using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Collections.Specialized;

using System.Threading;

namespace KCMDCollector
{
    public partial class Test : Form
    {
        protected bool IsCompleted { get; set; }

        public Test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(DoSomething);
            t.Start();


        }

        void DoSomething()
        {
            IsCompleted = false;
            webBrowser1.Url = new Uri("https://login.sina.com.cn/signup/signupmail.php?entry=blog&src=blog&http=1");



            Do(new MethodInvoker(delegate
            {
                IsCompleted = false;
                webBrowser1.Navigate("http://login.sina.com.cn/signup/signin.php?entry=blog&r=http%3A%2F%2Fcontrol.blog.sina.com.cn%2Fadmin%2Farticle%2Farticle_add.php%3Findex&from=referer:http://control.blog.sina.com.cn/admin/article/article_add.php?index,func:0006");
            }));



            Do(new MethodInvoker(delegate
            {
                SetTimeOut(new MethodInvoker(delegate
                {

                    try
                    {

                        webBrowser1.Document.GetElementById("username").SetAttribute("value", "kuibono@sina.com");
                        webBrowser1.Document.GetElementById("password").SetAttribute("value", "4264269");
                        webBrowser1.Document.GetElementById("username").Focus();

                        var inputs = webBrowser1.Document.GetElementsByTagName("input");
                        foreach (HtmlElement input in inputs)
                        {
                            if (input.GetAttribute("type") == "submit")
                            {

                                input.InvokeMember("click");
                                IsCompleted = false;
                            }
                        }
                    }
                    catch { }
                }), 500);//延迟半秒点击

            }));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.GetElementById("username").SetAttribute("value", "kuibono@sina.com");
            webBrowser1.Document.GetElementById("password").SetAttribute("value", "4264269");

            var inputs = webBrowser1.Document.GetElementsByTagName("input");
            foreach (HtmlElement input in inputs)
            {
                if (input.GetAttribute("type") == "submit")
                {
                    if (IsCompleted == false)
                    {
                        Thread.Sleep(2000);
                    }

                    input.InvokeMember("click");
                    //input.Click();
                }
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            IsCompleted = false;
            webBrowser1.Navigate("http://control.blog.sina.com.cn/admin/article/article_add.php");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var s1 = Voodoo.Net.Url.PostGetCookieAndHtml(new System.Collections.Specialized.NameValueCollection(),
                "https://login.sina.com.cn/signup/signupmail.php?entry=blog&src=blog&http=1",
                Encoding.GetEncoding("gb2312")
                );

            NameValueCollection nv = new NameValueCollection();
            nv.Add("reference", "http://control.blog.sina.com.cn/admin/article/article_add.php?index");
            nv.Add("entry", "blog");
            nv.Add("reg_entry", "blog");
            nv.Add("username", "kuibono@sina.com");
            nv.Add("password", "4264269");
            nv.Add("savestate", "1");
            nv.Add("safe_login", "1");

            var s2 = Voodoo.Net.Url.PostGetCookieAndHtml(nv,
                "https://login.sina.com.cn/sso/login.php?client=ssologin.js(v1.3.20)",
                Encoding.GetEncoding("gb2312"),
                s1.cookieContainer,

                s1.cookieCollection,
                "https://login.sina.com.cn/signup/signupmail.php?entry=blog&src=blog&http=1"
                );

            

            webBrowser1.Document.Body.InnerHtml = s2.Html;
        }



        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            IsCompleted = true;
        }

        private void timer_Exec_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!IsCompleted)
            {
                return;
            }

            Exec(mets[0]);
            mets = mets.Where(p => p != mets[0]).ToList();
            if (mets.Count == 0)
            {
                timer_Exec.Stop();
                timer_Exec.Enabled = false;
            }
        }

        protected List<Delegate> mets = new List<Delegate>();

        private void Exec(Delegate method)
        {
            try
            {
                this.Invoke(method);
            }
            catch { }
        }

        private void SetTimeOut(Delegate method, double time)
        {
            System.Timers.Timer tm = new System.Timers.Timer(time);
            tm.Elapsed += new System.Timers.ElapsedEventHandler(delegate
            {
                this.Invoke(method);
                tm.Stop();
                tm.Dispose();
            });
            tm.Enabled = true;
            tm.Start();
        }

        private void Do(Delegate method)
        {
            this.mets.Add(method);
            timer_Exec.Enabled = true;
            timer_Exec.Start();
        }
    }
}
