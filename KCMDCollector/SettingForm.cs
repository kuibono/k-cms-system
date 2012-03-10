using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Voodoo;
namespace KCMDCollector
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            //List<Book.CollectRule> rs = new List<KCMDCollector.Book.CollectRule>();
            //rs.Add(new Book.CollectRule()
            //{
            //    SiteName = "手打吧",
            //    CharSet = "gb2312",
            //    ChapterContent = "<div id=\"chapter_content\">(?<content>[\\s\\S]*?)</div>",
            //    ChapterNameAndUrl = "<div class=\"chapter_list_chapter\"><a href=\"(?<url>.*?)\" [\\s\\S]*?>(?<title>.*?)</a></div>",
            //    IsDefault = true,
            //    ChapterListUrl = "<a class=\"searchbtn03\" href=\"(?<url>.*?)\" target=\"_blank\"></a>",
            //    SearchPageUrl = "http://www.shouda8.com/EBook/index.aspx",
            //    SearchPars = "searchkey={0}&SearchClass=1",
            //    Url = "http://www.shouda8.com",
            //    BookInfoRule = "<h1 class=\"h1title\">[\\s\\S]*?《(?<title>.*?)》</a></h1>[\\s\\S]*?作者：(?<author>.*?)</span></li>[\\s\\S]*?<li class=\"a3\">总字数：(?<length>[\\d]*?)[\\s\\S]*?作品类别：[\\s\\S]*?\">(?<class>.*?)</a>[\\s\\S]*?<li id=\"articledesc\" class=\"a2\">(?<intro>[\\s\\S]*?)</li>[\\s\\S]*?<a class=\"searchbtn03\" href=\"(?<url>.*?)\" target=\"_blank\"></a>",
            //    BookInfoUrl = "<li class=\"storelistbt5a\"> <a target=\"_blank\" href=\"(?<url>.*?)\"> <img"
            //});

            //rs.Add(new Book.CollectRule()
            //{
            //    SiteName = "75小说",
            //    CharSet = "gbk",
            //    ChapterContent = "<td id=\"table_container\">(?<content>[\\s\\S]*?)</td>",
            //    ChapterNameAndUrl = "<dd><a href=\"(?<url>.*?)\">(?<title>.*?)</a></dd>",
            //    IsDefault = false,
            //    ChapterListUrl = "<a href=\"(?<url>.*?)\">阅读本书</a></li>",
            //    SearchPageUrl = "http://www.75dr.com/modules/article/search.php",
            //    SearchPars = "searchtype=articlename&searchkey={0}&submit=",
            //    Url = "http://www.75dr.com",
            //    BookInfoRule = "<h1 style=\"display:inline\">(?<title>.*?)</h1>[\\s\\S]*?小说作者：(?<autor>.*?)&nbsp;[\\s\\S]*?<div class=\"contents\" id=\"c01\">(?<intro>[\\s\\S]*?)</div>[\\s\\S]*?作品大类：<A .*?>(?<class>.*?)</A>",
            //    BookInfoUrl = "<td class=\"odd\"><a href=\"(?<url>.*?)\">.*?</a></td>"
            //});

            ////rs.Add(new Book.CollectRule()
            ////{
            ////    SiteName = "35小说网",
            ////    CharSet = "gbk",
            ////    ChapterContent = "<div id=\"content\" align=center>(?<content>[\\s\\S]*?)</div>",
            ////    ChapterNameAndUrl = "<a.*? href=\"(?<url>[0-9\\.html]*?)\".*?>(?<title>.*?)</a>",
            ////    IsDefault = false,
            ////    ChapterListUrl = "<a href=\"(?<url>.*?)\" target=\"_blank\">点击阅读</a>",
            ////    SearchPageUrl = "http://www.xiaoshuo555.cn/modules/article/search.php",
            ////    SearchPars = "searchtype=articlename&searchkey={0}&submit=精确搜索",
            ////    Url = "http://www.xiaoshuo555.cn",
            ////    BookInfoRule = "<span style=\"display:inline\" class=\"articlename\">(?<title>.*?)</span>[\\s\\S]*?小说作者：<a href=\".*?\">(?<author>.*?)</a>[\\s\\S]*?<div style=\"padding-left:10px;padding-right:10px;padding-top:5px;line-height:20px;font-size:12px;\">(?<intro>[\\s\\S]*?)&#9758;[\\s\\S]*?<strong>本书类别：</strong>(?<class>.*?)</td>",
            ////    BookInfoUrl = "<DIV class=mov-pic><A href=\"(?<url>.*?)\" ><IMG"
            ////});
            //rs.Add(new Book.CollectRule());
            //dataGridView1.DataSource = rs;

            ////Book.RulesOperate.SaveBookRules(rs);

            Book.QidianRule r = new KCMDCollector.Book.QidianRule();
            r.ChapterListUrl = "http://www.qidian.com/BookReader/{0}.aspx";
            r.ChapterTitle = "<li style='width:.*?%;'><a[\\s\\S]*?href=\"(?<url>.*?)\"[\\s\\S]*?>(?<title>.*?)</a>";
            r.CharSet = "UTF-8";
            r.SearchPageUrl = "http://sosu.qidian.com/ajax/search.ashx?method=getbooksearchlist&searchtype=%E4%B9%A6%E5%90%8D&searchkey={0}";
            r.SearchRefer = "http://sosu.qidian.com/searchresult.aspx?searchkey={0}&searchtype=%E4%B9%A6%E5%90%8D";

            Book.RulesOperate.SaveQidianRule(r);

            //Book.Setting set = new KCMDCollector.Book.Setting();
            //set.APIKey = "111111";
            //set.PassWord = "123456";
            //set.TargetUrl = "http://localhost/";
            //set.UserName = "admin";
            //Book.RulesOperate.SaveSetting(set);

            var rs = Book.RulesOperate.GetBookRules();
            dataGridView1.DataSource = rs;

            var mb = Book.RulesOperate.GetMailBlogRule();
            dataGridView2.DataSource = mb;
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var rs = new List<Book.CollectRule>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                rs.Add(new Book.CollectRule()
                {
                    SiteName = row.Cells["SiteName"].Value.ToString(),
                    CharSet = row.Cells["CharSet"].Value.ToString(),
                    ChapterContent = row.Cells["ChapterContent"].Value.ToString(),
                    ChapterNameAndUrl = row.Cells["ChapterNameAndUrl"].Value.ToString(),
                    IsDefault = false,
                    SearchMethod = row.Cells["SearchMethod"].Value.ToString(),
                    ChapterListUrl = row.Cells["ChapterListUrl"].Value.ToString(),
                    SearchPageUrl = row.Cells["SearchPageUrl"].Value.ToString(),
                    SearchPars = row.Cells["SearchPars"].Value.ToString(),
                    Url = row.Cells["Url"].Value.ToString(),
                    BookInfoRule = row.Cells["BookInfoRule"].Value.ToString(),
                    BookInfoUrl = row.Cells["BookInfoUrl"].Value.ToString()
                });
            }

            Book.RulesOperate.SaveBookRules(rs);

            var mb = new List<Book.MailBlog>();
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                mb.Add(new KCMDCollector.Book.MailBlog()
                {
                    Enable = row.Cells["Enable"].Value.ToBoolean(),
                    BlogAddress = row.Cells["BlogAddress"].Value.ToString(),
                    BlogName = row.Cells["BlogName"].Value.ToString(),
                    Email = row.Cells["Email"].Value.ToString(),
                    Password = row.Cells["Password"].Value.ToString(),
                    RecMail = row.Cells["RecMail"].Value.ToString(),
                    Smtp = row.Cells["Smtp"].Value.ToString()
                });
            }
            Book.RulesOperate.SaveMailBlogRule(mb);

            MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void 新增ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var rs = Book.RulesOperate.GetBookRules();
            rs.Add(new KCMDCollector.Book.CollectRule()
            {
                SiteName = "",
                CharSet = "",
                ChapterContent = "",
                ChapterNameAndUrl = "",
                IsDefault = false,
                ChapterListUrl = "",
                SearchPageUrl = "",
                SearchPars = "",
                SearchMethod = "",
                Url = "",
                BookInfoRule = "",
                BookInfoUrl = ""
            });
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = rs;
            dataGridView1.Refresh();
        }

        private void 新增邮件规则ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mb = Book.RulesOperate.GetMailBlogRule();

            mb.Add(new KCMDCollector.Book.MailBlog()
            {
                Enable = false,
                BlogAddress = "",
                BlogName = "",
                Email = "",
                Password = "",
                RecMail = "",
                Smtp = ""
            });

            dataGridView2.DataSource = null;
            dataGridView2.DataSource = mb;
            dataGridView2.Refresh();
        }
    }
}
