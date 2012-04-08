using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SimpleCollector.Common;
using Voodoo.Net;
using Voodoo;
using System.Text.RegularExpressions;

namespace SimpleCollector
{
    public partial class FormRuleEdit : Form
    {

        public RuleModel CurrentRule { get; set; }

        public FormRuleEdit()
        {
            InitializeComponent();
            CurrentRule = new RuleModel();
        }

        protected static string bookUrl = "";
        protected static string str_chapterUrl = "";

        private void wizardControl1_NextButtonClick(WizardBase.WizardControl sender, WizardBase.WizardNextButtonClickEventArgs args)
        {
            try
            {
                int stepIndex = wizardControl1.CurrentStepIndex;


                switch (stepIndex)
                {
                    case 1:
                        //输入了网站名称
                        CurrentRule.SiteName = txt_SiteName.Text;
                        CurrentRule.Encoding = com_Encoding.Text;
                        if (txt_SiteName.Text.IsNullOrEmpty())
                        {
                            MessageBox.Show("请输入网站名称");
                            args.Cancel = true;
                        }
                        break;
                    case 2:
                        //输入了列表页地址
                        CurrentRule.ListPageUrl = txt_ListPageUrl.Text;
                        CurrentRule.NextPageUrl = txt_NextPageUrl.Text;
                        txt_ListPageHtml.Text = Url.GetHtml(CurrentRule.ListPageUrl, CurrentRule.Encoding);
                        if (txt_ListPageUrl.Text.IsNullOrEmpty())
                        {
                            MessageBox.Show("请输入列表页地址");
                            args.Cancel = true;
                        }
                        break;
                    case 3:
                        //输入了书籍信息页地址规则
                        CurrentRule.BookPageUrl = txt_BookInfoUrl.Text;
                        Match m_bookUrlAndTitle = txt_ListPageHtml.Text.GetMatchGroup(CurrentRule.BookPageUrl);
                        if (m_bookUrlAndTitle.Success)
                        {
                            bookUrl = m_bookUrlAndTitle.Groups["url"].Value.AppendToDomain(CurrentRule.ListPageUrl);
                            txt_BookInfoHtml.Text = Url.GetHtml(bookUrl, CurrentRule.Encoding);
                        }
                        else
                        {
                            MessageBox.Show("未能打开书籍信息页，请检查规则和网路连接");
                            args.Cancel = true;
                        }
                        break;
                    case 4:
                        //输入书籍信息规则
                        CurrentRule.BookInfo = txt_BookInfoRule.Text;
                        Match m = txt_BookInfoHtml.Text.GetMatchGroup(CurrentRule.BookInfo);
                        if (m.Success)
                        {
                            txt_BookInfos.Text = string.Format("\n\n标题：{0}\n\n作者：{1}\n\n分类：{2}\n\n字数：{3}\n\n简介：{4}\n\n",
                                m.Groups["title"].Value,
                                m.Groups["author"].Value,
                                m.Groups["class"].Value,
                                m.Groups["length"].Value,
                                m.Groups["intro"].Value
                                );
                        }
                        else
                        {
                            MessageBox.Show("书籍信息规则有误，请检查");
                            args.Cancel = true;
                        }
                        break;
                    case 5:
                        //输入章节列表地址规则 
                        CurrentRule.ChapterListPageUrl = txt_ChapterListUrl.Text;
                        str_chapterUrl = txt_BookInfoHtml.Text.GetMatch(CurrentRule.ChapterListPageUrl).First().AppendToDomain(bookUrl);
                        txt_ChapterListHtml.Text = Url.GetHtml(str_chapterUrl, CurrentRule.Encoding);
                        if (txt_ChapterListHtml.Text.IsNullOrEmpty())
                        {
                            MessageBox.Show("未能打开章节列表，请检查规则和网路连接");
                            args.Cancel = true;
                        }
                        break;
                    case 6:
                        //输入了章节标题地址规则
                        CurrentRule.ChapterTitleAndUrl = txt_ChapterTitleAndUrl.Text;
                        Match m_ChapterTitleAndUrl = txt_ChapterListHtml.Text.GetMatchGroup(CurrentRule.ChapterTitleAndUrl);
                        if (m_ChapterTitleAndUrl.Success)
                        {
                            //标题不管，直接打开章节正文
                            string chapterContentUrl = m_ChapterTitleAndUrl.Groups["url"].Value.AppendToDomain(str_chapterUrl);
                            txt_ChapterContentHtml.Text = Url.GetHtml(chapterContentUrl, CurrentRule.Encoding);
                        }
                        else
                        {
                            MessageBox.Show("未能打开章节页面，请检查规则和网路连接");
                            args.Cancel = true;
                        }
                        break;
                    case 7:
                        //输入了正文规则
                        CurrentRule.Content = txt_ChapterContentRule.Text;
                        txt_Content.Text = txt_ChapterContentHtml.Text.GetMatch(CurrentRule.Content).First();
                        if (txt_Content.Text.IsNullOrEmpty())
                        {
                            MessageBox.Show("可能是规则不对了");
                            args.Cancel = true;
                        }
                        break;
                    case 8:
                        CurrentRule.NextContentUrl = txt_NextContent.Text;
                        
                        break;
                    case 9:
                        string[] books = txt_BookList.Text.Split('\n');
                        CurrentRule.BookNeedCollect = new List<string>();
                        foreach (string str in books)
                        {
                            CurrentRule.BookNeedCollect.Add(str);
                        }
                        break;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                args.Cancel = true;

            }
        }

        private void wizardControl1_FinishButtonClick(object sender, EventArgs e)
        {
            Voodoo.IO.XML.SaveSerialize(CurrentRule, System.Environment.CurrentDirectory + "\\Rules\\" + CurrentRule.SiteName + ".xml");
            MessageBox.Show("规则已经保存");
        }

        private void FormRuleEdit_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            MessageBox.Show("本采集器是闲人KUIBONO闲着没事做出来的蛋疼作品，\n\n其中wizard插件使用的是一款开源产品，具体产品名称已经无从考证。", "关于", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void wizardControl1_CancelButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
