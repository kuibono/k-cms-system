using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KCMDCollector
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri("http://login.sina.com.cn/signup/signin.php");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.GetElementById("username").SetAttribute("value", "bigcuibing@tom.com");
            webBrowser1.Document.GetElementById("password").SetAttribute("value", "Admin@123");

            var inputs = webBrowser1.Document.GetElementsByTagName("input");
            foreach (HtmlElement input in inputs)
            {
                if (input.GetAttribute("type") == "submit")
                {

                    input.InvokeMember("click");
                    //input.Click();
                }
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://control.blog.sina.com.cn/admin/article/article_add.php");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.GetElementById("articleTitle").SetAttribute("value", "爱造人小说阅读");
             webBrowser1.Document.GetElementById("SinaEditorTextarea").SetAttribute("value",Environment.NewLine+ "<a href=\"http://www.aizr.net/\">愛造人小說閱讀(aizr.net)</a>。是專門為華人打造的，免費的，純淨的小說閱讀工具。 愛造人小說閱讀網站，沒有任何彈窗廣告，展示廣告以及文字廣告，旨在為廣大讀者提供一個完全純淨的閱讀空間。 愛造人小說閱讀獨特的界面風格，更適合手機閱讀，其簡介的界面風格專門為手機閱讀設計，小說《很純很曖昧》、《校花的貼身高手》等優秀意淫小說（YY小說）實時更新，敬請關注。 愛造人小說閱讀網站，提供純文本純手打小說閱讀，並將陸續提供TXT小說下載，以及其他常用的手機閱讀功能。");
            webBrowser1.Document.GetElementById("articleTagInput").SetAttribute("value", "小说阅读");
            webBrowser1.Document.GetElementById("SinaEditor_Iframe").GetElementsByTagName("iframe")[0].Document.Body.InnerHtml = "我操";
            //webBrowser1.Document.GetElementById("articlePostBtn").InvokeMember("click");
        }
    }
}
