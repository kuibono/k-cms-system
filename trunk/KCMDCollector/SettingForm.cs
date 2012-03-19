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

            Book.QidianRule r = new KCMDCollector.Book.QidianRule();
            r.ChapterListUrl = "http://www.qidian.com/BookReader/{0}.aspx";
            r.ChapterTitle = "<li style='width:.*?%;'><a[\\s\\S]*?href=\"(?<url>.*?)\"[\\s\\S]*?>(?<title>.*?)</a>";
            r.CharSet = "UTF-8";
            r.SearchPageUrl = "http://sosu.qidian.com/ajax/search.ashx?method=getbooksearchlist&searchtype=%E4%B9%A6%E5%90%8D&searchkey={0}";
            r.SearchRefer = "http://sosu.qidian.com/searchresult.aspx?searchkey={0}&searchtype=%E4%B9%A6%E5%90%8D";

            Book.RulesOperate.SaveQidianRule(r);



            var rs = Book.RulesOperate.GetBookRules();
            dataGridView1.DataSource = rs;

            var mb = Book.RulesOperate.GetMailBlogRule();
            dataGridView2.DataSource = mb;

            var br = Book.RulesOperate.GetBlogModel();
            dataGridView3.DataSource = br;
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var rs = new List<Book.CollectRule>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                rs.Add(new Book.CollectRule()
                {
                    IsImageSite = row.Cells["IsImageSite"].Value.ToBoolean(),
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
                    BookInfoUrl = row.Cells["BookInfoUrl"].Value.ToString(),
                    ImageRule = row.Cells["ImageRule"].Value.ToS()
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

            var br = new List<Book.BlogModel>();
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                br.Add(new KCMDCollector.Book.BlogModel()
                {
                    BlogName = row.Cells["BlogName"].Value.ToS(),
                    BlogUrl = row.Cells["BlogUrl"].Value.ToS(),
                    Password = row.Cells["Password"].Value.ToS(),
                    UserName = row.Cells["UserName"].Value.ToS(),
                    type = (Book.BlogType)row.Cells["type"].Value
                });
            }
            Book.RulesOperate.SaveBlogModel(br);

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

        private void 新增博客ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var br = Book.RulesOperate.GetBlogModel();
            br.Add(new KCMDCollector.Book.BlogModel()
                {
                    BlogName = "",
                    BlogUrl = "",
                    Password = "",
                    UserName = "",
                    type = Book.BlogType.Neasy
                });
            dataGridView3.DataSource = null;
            dataGridView3.DataSource = br;
            dataGridView3.Refresh();
        }
    }
}
