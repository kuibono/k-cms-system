using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using Voodoo;
using Voodoo.Model;
using Voodoo.Basement.Client;
using Voodoo.Net;

namespace SimpleCollector
{
    public class CollectBook
    {
        FormMain Parent;
        public CollectBook(FormMain fm)
        {
            this.Parent = fm;
        }

        protected void SetStatus(string str)
        {
            Parent.Invoke(new MethodInvoker(delegate {

                Parent.lb_Status.Text = str;
            }));
        }

        public void Collect(string ListUrl, string BookTitle, string UrlTitleRule, string ContentRule)
        {
            string enciding = "utf-8";

            BookHelper bh = new BookHelper("http://aizr.net/");
            SetStatus("获取本地书籍");
            Book b = bh.SearchBook(BookTitle, "", "").First();

            SetStatus("打开列表页面");
            string str_ListHtml = Url.GetHtml(ListUrl, enciding);

            Match m_urls = str_ListHtml.GetMatchGroup(UrlTitleRule);
            while (m_urls.Success)
            {
                string url = m_urls.Groups["url"].Value.AppendToDomain(ListUrl);
                string Title = m_urls.Groups["title"].Value;

                SetStatus("判断章节是否存在-"+Title);
                var chapter = bh.ChapterSearch(b.Title, Title, false);
                if (chapter.Count > 0)
                {
                    m_urls = m_urls.NextMatch();
                    continue;
                }

                SetStatus("打开章节：" + Title);
                string str_HtmlContent = Url.GetHtml(url, enciding);

                string content = str_HtmlContent.GetMatch(ContentRule).First();
                content = Filter(content);

                SetStatus("保存章节");
                bh.ChapterAdd(b.ID, Title, content, false);


                m_urls = m_urls.NextMatch();
            }

            
        }


        #region 过滤
        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        public string Filter(string Content)
        {

            //去除特殊字符
            Content = Regex.Replace(Content, "[§№☆★○●◎◇◆□■△▲※→←↑↓〓＃＆＠＼＾＿￣―♂♀‘’々～‖＂〃〔〕〈〉《》「」『』〖〗【】（）［｛｝°＄￡￥‰％℃¤￠]{1,}?", "");
            Content = Regex.Replace(Content, "[~@#$%^*()_=\\-\\+\\[\\]]{1,}?", "");

            //全角转半角
            Content = Content.ToDBC();

            //英文转小写
            Content = Content.ToLower();

            //删除脚本
            Content = Regex.Replace(Content, "<script [\\s\\S]*?</script>", "", RegexOptions.IgnoreCase);

            //删除链接
            Content = Regex.Replace(Content, "<a [\\s\\S]*?</a>", "", RegexOptions.IgnoreCase);

            //删除不需要的HTML
            Content = Regex.Replace(Content, "<[/]?table>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "<[/]?tr>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "<[/]?td>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "<[/]?div>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "<[/]?span>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "<[/]?font>", "", RegexOptions.IgnoreCase);
            //Content = Regex.Replace(Content, "<[/]?p>", "", RegexOptions.IgnoreCase);

            //删除网址
            Content = Regex.Replace(Content, "http://", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "https://", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "[\\\\\\w\\./。]{3,20}\\.[com|net|org|cn|co|info|us|cc|xxx|tv|ws|hk|tw]+", "", RegexOptions.IgnoreCase);//有些网址会出现带掺杂特殊符号的行为如 www/.aizr\。net



            return Content;
        }
        #endregion 过滤
    }

}
