using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.Script.Serialization;

using Voodoo;

namespace NovelCollector
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //BookInfoRule bi = new BookInfoRule();
            //bi.CharSet = "UTF-8";
            //bi.SearchRefer = "http://sosu.qidian.com/searchresult.aspx?searchkey={0}&searchtype=%E4%B9%A6%E5%90%8D";
            //bi.SearchPageUrl = "http://sosu.qidian.com/ajax/search.ashx?method=getbooksearchlist&searchtype=%E4%B9%A6%E5%90%8D&searchkey={0}";
            //bi.BookInfoUrl = "http://www.qidian.com/Book/{0}.aspx";
            //bi.BookInfoUrl = "<div class=\"title\">[\\s\\S]*?<h1>(?<title>[\\s\\S]*?)</h1>[\\s]*?<b>小说作者：</b>[\\s\\S]*?\">(?<author>[\\s\\S]*?)</a>[\\s\\S]*?<b>总字数：</b>(?<charcount>[\\s\\S]*?)</td>[\\s\\S]*?<div class=\"txt\">(?<intro>[\\s\\S]*?)<span[\\s\\S]*?<b>小说类别：</b><a href=\"[\\s\\S]*?target=\"_blank\">(?<class>[\\s\\S]*?)</a>[\\s\\S]*?<b>写作进程：</b>(?<status>[\\s\\S]*?)</td>";
            //bi.ChapterListUrl = "http://www.qidian.com/BookReader/{0}.aspx";
            //bi.ChapterTitle = "<li style='width:33%;'><a [\\s\\S]*?>(?<title>.*?)</a>";
            //bi.BaiduCharSet = "UTF-8";
            //bi.ChapterUrl = "<table[\\s\\S]*?<h3 class=\"t\"><a[\\s\\S]*? href=\"(?<url>.*?)\"";

            //Voodoo.IO.XML.SaveSerialize(bi, "C:\\BookInfoRule.xml");
            ////Voodoo.IO.XML.Serialize(bi);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BookInfoRule bi = (BookInfoRule)Voodoo.IO.XML.DeSerialize(typeof(BookInfoRule), Voodoo.IO.File.Read(System.Environment.CurrentDirectory + "\\BookInfoRule.xml"));
            string Title = "校花的贴身高手";
            Title = Title.toUtf8String();

            string QidianSearchUrl = string.Format(bi.SearchPageUrl, Title);
            string QidianRefer = string.Format(bi.SearchRefer, Title);

            string SearchList = Voodoo.Net.Url.Post(new System.Collections.Specialized.NameValueCollection(),
                QidianSearchUrl,
                Encoding.GetEncoding(bi.CharSet),
                new System.Net.CookieContainer(),
                "*.*",
                QidianRefer,
                "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.2 Safari/535.11");
            

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var json = serializer.DeserializeObject(SearchList);

            var j = ((System.Collections.Generic.Dictionary<string, object>)((object[])(((object[])(json))[0]))[0]);


            string bookID = j["BookId"].ToS();
            string bookTitle = j["BookName"].ToS().TrimHTML();
            string Description = j["Description"].ToS();
            string CategoryName = j["CategoryName"].ToS();
            string SubCategoryName = j["SubCategoryName"].ToS();
            string AuthorName = j["AuthorName"].ToS();


            string ChapterListUrl = string.Format(bi.ChapterListUrl, bookID);

            string ListContent = Voodoo.Net.Url.Post(new System.Collections.Specialized.NameValueCollection(),
                ChapterListUrl,
                Encoding.GetEncoding(bi.CharSet),
                new System.Net.CookieContainer(),
                "*.*",
                SearchList,
                "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.2 Safari/535.11");


            richTextBox1.Text = ListContent;

        }
    }
}
