﻿using System;
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

        protected string Domain = "http://aizr.net/";
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
                    if (book.Status == 1)
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {
                            this.Status.Text = b.Title + "-已完结";
                        }));

                        return;//本书已经完结
                    }
                }


                if (book.ID <= 0)//书籍添加失败或者没有这本书
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        this.Status.Text = "书籍添加失败或者没有这本书";
                    }));
                    return;
                }

                this.Invoke(new MethodInvoker(delegate
                {
                    this.Status.Text = "正在处理:"+b.Title;
                }));

                //获取章节列表
                List<ChapterList> cs = new List<ChapterList>();

                string html = Voodoo.Net.Url.GetHtml(b.ListUrl, "gbk");
                Match m_list_Chapter = new Regex(listRegex).Match(html);
                int i = 0;

                if (!m_list_Chapter.Success)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        this.Status.Text = "获取列表失败";
                    }));
                }

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
                this.Invoke(new MethodInvoker(delegate
                {
                    this.Status.Text = "开始采集";
                }));
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
                        content = Regex.Replace(content, "www[\\s\\S]{3,30}?com", "",RegexOptions.IgnoreCase);
                        content = Regex.Replace(content, "www[\\s\\S]{3,30}?net", "", RegexOptions.IgnoreCase);
                        content = Regex.Replace(content, "www[\\s\\S]{3,30}?org", "", RegexOptions.IgnoreCase);
                        content = Regex.Replace(content, "www[\\s\\S]{3,30}?cn", "", RegexOptions.IgnoreCase);
                        content = Regex.Replace(content, "年夜", "大");
                        content = Regex.Replace(content, "老苍生", "老百姓");
                        content = Regex.Replace(content, "德律风", "电话");
                        content = Regex.Replace(content, "luàn", "乱");
                        content = Regex.Replace(content, "/a", "");
                        content = Regex.Replace(content, "br>", "");
                        content = Regex.Replace(content, "mí人", "迷人");
                        content = Regex.Replace(content, "sè", "色");
                        content = Regex.Replace(content, "mō", "摸");
                        content = Regex.Replace(content, "mo", "摸");
                        content = Regex.Replace(content, "chún", "纯");
                        content = Regex.Replace(content, "xìng", "性");
                        content = Regex.Replace(content, "xing", "性");
                        content = Regex.Replace(content, "tuǐ", "腿");
                        content = Regex.Replace(content, "chuáng", "床");
                        content = Regex.Replace(content, "tǐng", "挺");
                        content = Regex.Replace(content, "huā", "花");
                        content = Regex.Replace(content, "感动", "冲动");
                        content = Regex.Replace(content, "\\*\\*局", "公安局");
                        content = Regex.Replace(content, "ting", "挺");
                        content = Regex.Replace(content, "卜", "小");
                        content = Regex.Replace(content, "lu", "露");
                        content = Regex.Replace(content, "中文网", "");
                        content = Regex.Replace(content, "首发", "");
                        content = Regex.Replace(content, "本章节[\\s\\S]{3,50}?手打", "");
                        content = Regex.Replace(content, "手打吧[\\s\\S]{3,50}?手打", "");
                        content = Regex.Replace(content, "yīn", "隂");
                        content = Regex.Replace(content, "méng", "蒙");
                        content = Regex.Replace(content, "méng", "蒙");
                        content = Regex.Replace(content, "wěn", "吻");
                        content = Regex.Replace(content, "〖", "");
                        content = Regex.Replace(content, "〗", "");
                        content = Regex.Replace(content, "xué", "穴");
                        content = Regex.Replace(content, "克不及", "能");
                        content = Regex.Replace(content, "体例", "办法");
                        content = Regex.Replace(content, "dang", "荡");
                        content = Regex.Replace(content, "shouda8。.com", "aizr.net");
                        content = Regex.Replace(content, "颔首", "头");
                        content = Regex.Replace(content, "辅佐", "帮忙");
                        content = Regex.Replace(content, "不消", "不必");
                        content = Regex.Replace(content, "脱手", "动手");
                        content = Regex.Replace(content, "伶俐", "聪明");
                        content = Regex.Replace(content, "si", "私");
                        content = Regex.Replace(content, "工具", "东西");
                        content = Regex.Replace(content, "欠好", "不好");
                        content = Regex.Replace(content, "囘", "");
                        content = Regex.Replace(content, "làng", "浪");
                        content = Regex.Replace(content, "yù", "玉");
                        content = Regex.Replace(content, "yàn", "艳");
                        content = Regex.Replace(content, "nòng", "弄");
                        content = Regex.Replace(content, "nv", "女");
                        content = Regex.Replace(content, "mén", "门");
                        content = Regex.Replace(content, "jiāo", "交");
                        content = Regex.Replace(content, "yào", "药");
                        content = Regex.Replace(content, "1ù", "露");
                        content = Regex.Replace(content, "fǔ", "府");

                        NameValueCollection nv = new NameValueCollection();
                        nv.Add("bookid", book.ID.ToS());
                        nv.Add("booktitle", book.Title);
                        nv.Add("classid", cls.ID.ToS());
                        nv.Add("classname", cls.ClassName);
                        nv.Add("content", content);
                        nv.Add("title", cp.Title);

                        this.Invoke(new MethodInvoker(delegate
                        {
                            this.Status.Text = b.Title + "-" + cp.Title +"正在采集";
                        }));

                        BookChapter nouse = (BookChapter)Voodoo.IO.XML.DeSerialize(typeof(BookChapter), Voodoo.Net.Url.Post(nv, Domain + "e/api/ChapterAdd.aspx", Encoding.UTF8));
                        if (nouse.ID <= 0)//如果章节采集失败，则跳出这本书的采集，进行下一本书
                        {
                            this.Invoke(new MethodInvoker(delegate
                            {
                                this.Status.Text = b.Title + "-" + cp.Title + "-采集失败-" + nouse.Title;
                            }));

                            if (cs.Count > 0)
                            {
                                Voodoo.Net.Url.GetHtml(Domain + "e/api/CreatePage.aspx?action=createindex");

                                Voodoo.Net.Url.GetHtml(Domain + "e/api/CreatePage.aspx?action=createclasspage");

                                Voodoo.Net.Url.GetHtml(Domain + "e/api/CreatePage.aspx?action=createbook&id=" + book.ID);

                                Voodoo.Net.Url.GetHtml(Domain + "e/api/CreatePage.aspx?action=createchapters&id=" + book.ID);
                            }

                            return;
                        }

                        this.Invoke(new MethodInvoker(delegate
                        {
                            this.Status.Text = b.Title + "-" + nouse.Title;
                        }));

                        this.Invoke(new MethodInvoker(delegate
                        {
                            this.Status.Text = b.Title + "-" + cp.Title + "-采集成功-" + nouse.Title;
                        }));

                        m_content = m_content.NextMatch();
                    }
                }

                this.Invoke(new MethodInvoker(delegate
                {
                    this.Status.Text = b.Title + "-采集完成";
                }));
                //采集完成 
                //开始生成
                if (cs.Count > 0)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        this.Status.Text = b.Title + "-正在生成";
                    }));

                    Voodoo.Net.Url.GetHtml(Domain + "e/api/CreatePage.aspx?action=createindex");

                    Voodoo.Net.Url.GetHtml(Domain + "e/api/CreatePage.aspx?action=createclasspage");

                    Voodoo.Net.Url.GetHtml(Domain + "e/api/CreatePage.aspx?action=createbook&id=" + book.ID);

                    Voodoo.Net.Url.GetHtml(Domain + "e/api/CreatePage.aspx?action=createchapters&id=" + book.ID);
                }

            }

        }
        #endregion

        protected void collect()
        {


            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                try
                {
                    GetBookByUrl(listBox1.Items[i].ToS(), txt_InfoReg.Text, textBox2.Text, textBox3.Text);
                    this.Invoke(new MethodInvoker(delegate
                    {
                        this.listBox1.SelectedIndex = i;
                        
                    }));
                    
                }
                catch(Exception ex)
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
            CollectRule r = new CollectRule();
            r.BookUrls = new List<string>();
            string[] urls = Voodoo.IO.File.Read(System.Environment.CurrentDirectory + "\\list.txt").Split('\n');
            foreach (string str in urls)
            {
                r.BookUrls.Add(str.Trim());
            }

            r.BookInfo = txt_InfoReg.Text;
            r.ChapterList = textBox2.Text;
            r.Content = textBox3.Text;
            //Voodoo.IO.XML.Serialize(r);
            Voodoo.IO.File.Write("C:\\2.xml", Voodoo.IO.XML.Serialize(r));
            Voodoo.IO.XML.SaveSerialize(r, "C:\\rule.xml");


            CollectRule rule = (CollectRule)Voodoo.IO.XML.DeSerialize(
               typeof(CollectRule),
               Voodoo.IO.File.Read("C:\\2.xml", Voodoo.IO.File.EnCode.UTF8)
               );
        }
    }
}
