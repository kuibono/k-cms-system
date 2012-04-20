using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using Voodoo;
using Voodoo.Model;
using Voodoo.Net;
using System.Text.RegularExpressions;

namespace TextCollector.Common
{
    public abstract class CollectBase
    {
        public StatusObject status { get; set; }

        public CollectBase()
        {
            status = new StatusObject();
        }

        /// <summary>
        /// 状态改变
        /// </summary>
        protected abstract void Status_Chage();

        #region  获取书籍标题和URL
        /// <summary>
        /// 获取书籍标题和URL
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public List<ChapterTitleAndUrl> GetChapterTitleAndUrl(string book)
        {
            List<ChapterTitleAndUrl> chapters = new List<ChapterTitleAndUrl>();


            //1.搜索
            NameValueCollection nv = new NameValueCollection();
            nv.Add("searchkey", book);
            nv.Add("searchtype", "articlename");

            var SearchResult = Voodoo.Net.Url.PostGetCookieAndHtml(nv, "http://www.wcxiaoshuo.com/modules/article/search.php", Encoding.GetEncoding("gbk"));

            //2.打开书籍信息页面
            string hrml_BookInfo = "";//书籍信息页内容

            var match_Url = SearchResult.Html.GetMatchGroup("td class=\"odd\"><a href=\"(?<url>.*?)\">(?<title>.*?)</a></td>");


            if (match_Url.Success)
            {
                string url = match_Url.Groups["url"].Value.AppendToDomain(SearchResult.url);
                hrml_BookInfo = Url.GetHtml(url, "gbk");
            }
            else
            {
                //直接进入了书籍信息页
                hrml_BookInfo = SearchResult.Html;
            }


            //3.打开章节列表页

            //没有找到这本书
            if (!Regex.IsMatch(hrml_BookInfo, "<a href=\"(?<key>[^\"']*?)\" title=\"开始阅读\"><span>开始阅读</span></a>"))
            {
                return new List<ChapterTitleAndUrl>();
            }
            string list_url = hrml_BookInfo.GetMatch("<a href=\"(?<key>[^\"']*?)\" title=\"开始阅读\"><span>开始阅读</span></a>").First().AppendToDomain(SearchResult.url);

            WebInfo listInfo = Url.PostGetCookieAndHtml(new NameValueCollection(), list_url, Encoding.GetEncoding("gbk"));

            //4。获得
            var matchresult = listInfo.Html.GetMatchGroup("<li.*?><a.*?href=\"(?<url>.*?)\">(?<title>.+?)</a.*?></li.*?>");
            while (matchresult.Success)
            {
                chapters.Add(new ChapterTitleAndUrl()
                {
                    Title = matchresult.Groups["title"].Value,
                    Url = matchresult.Groups["url"].Value.AppendToDomain(listInfo.url)
                });
                matchresult = matchresult.NextMatch();
            }

            return chapters;
        }
        #endregion

        #region 搜索需要替换的章节
        /// <summary>
        /// 搜索需要替换的章节
        /// </summary>
        /// <param name="BookTitle">书籍名称</param>
        /// <returns></returns>
        public List<BookChapter> chapterNeedCollect(string BookTitle)
        {
            ClientServices.ClientServiceClient cs = new ClientServices.ClientServiceClient();
            return cs.ChapterSearch(string.Format("BookTitle=N'{0}' and IsImageChapter=1", BookTitle)).ToList();
        }
        #endregion

        #region  获取章节正文
        /// <summary>
        /// 获取章节正文
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetContent(string url)
        {
            string html_content = Url.GetHtml(url, "gb2312");

            string content = html_content.GetMatch("<div id=\"htmlContent\" class=\"contentbox\" >[\\s]*?<div align=\"center\"><script type=\"text/javascript\" src=\"/js/nrh.js\"></script></div>(?<key>[\\s\\S]*?)</div>").First();

            return FilterContent(content);
        }
        #endregion

        #region 替换图片
        /// <summary>
        /// 替换图片
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string FilterContent(string content)
        {
            content = Regex.Replace(content, "<img src=['\"]/sss/", "");
            content = Regex.Replace(content, ".jpg['\"]>", "");
            content = Regex.Replace(content, "是[\\s]*?由.*?会员手打，更多章节请到网址：www.wcxiaoshuo.com", "");

            content = content.Replace("edajihexr", "大姐");
            content = content.Replace("gstjhranjgwjo", "上午");
            content = content.Replace("shangwo", "上午");
            content = content.Replace("feilrpto", "老婆");
            content = content.Replace("lagong", "老公");
            content = content.Replace("xdifagojge", "小姐");
            content = content.Replace("xianggx", "相公");
            content = content.Replace("gnxnifawhu", "下午");
            content = content.Replace("hjeirerm6eihv", "姐妹");
            content = content.Replace("gfognggzigh", "公子");
            content = content.Replace("fpefnyoturxi", "朋友");
            content = content.Replace("gongzi", "公子");
            content = content.Replace("lapo", "老婆");
            content = content.Replace("tymyigngtyn", "明天");
            content = content.Replace("vfsjgigarn", "时间");
            content = content.Replace("jhiheejeieev", "姐姐");
            content = content.Replace("nvdxfudfjfj", "女婿");
            content = content.Replace("xeieavnfsg", "先生");
            content = content.Replace("dajiex", "大姐");
            content = content.Replace("xiawu", "下午");
            content = content.Replace("fabxianr", "发现");
            content = content.Replace("xiaoje", "小姐");
            content = content.Replace("sjian", "时间");
            content = content.Replace("gxhigfadnoxihnyy", "小心");
            content = content.Replace("gggugolgair", "过来");
            content = content.Replace("gxronfdri", "兄弟");
            content = content.Replace("gxgihftutp", "媳妇");
            content = content.Replace("faxian", "发现");
            content = content.Replace("hxiapxint", "相信");
            content = content.Replace("fzagnggfbl", "丈夫");
            content = content.Replace("mingtn", "明天");
            content = content.Replace("penyouxi", "朋友");
            content = content.Replace("fmgeyimehid", "妹妹");
            content = content.Replace("rgtugoqgugu", "过去");
            content = content.Replace("bkbskhhuka", "说话");
            content = content.Replace("nvxudjj", "女婿");
            content = content.Replace("yuhhfuiuqub", "回去");
            content = content.Replace("jiejiev", "姐姐");
            content = content.Replace("khjukilkaim", "回来");
            content = content.Replace("jiemeiv", "姐妹");
            content = content.Replace("nvdrfenfjfj", "女人");
            content = content.Replace("xiansg", "先生");
            content = content.Replace("frurtefne", "夫人");
            content = content.Replace("xiaoxinyy", "小心");
            content = content.Replace("qdonglxi", "东西");
            content = content.Replace("xiaxin", "相信");
            content = content.Replace("xondi", "兄弟");
            content = content.Replace("guolair", "过来");
            content = content.Replace("guoquu", "过去");
            content = content.Replace("cerztifb", "儿子");
            content = content.Replace("shhua", "说话");
            content = content.Replace("huiqub", "回去");
            content = content.Replace("xifup", "媳妇");
            content = content.Replace("huilaim", "回来");
            content = content.Replace("dongxi", "东西");
            content = content.Replace("zibjib", "自己");
            content = content.Replace("nvrenjj", "女人");
            content = content.Replace("zangfl", "丈夫");
            content = content.Replace("cuoaw", "错");
            content = content.Replace("zzhiedo3", "知道");
            content = content.Replace("ddefr", "");
            content = content.Replace("fezrormre", "怎么");
            content = content.Replace("furene", "夫人");
            content = content.Replace("6shenumev", "什么");
            content = content.Replace("erzib", "儿子");
            content = content.Replace("cuow", "错");
            content = content.Replace("ziji", "自己");
            content = content.Replace("zhido", "知道");
            content = content.Replace("zenme", "怎么");
            content = content.Replace("maoashu", "：“");
            content = content.Replace("shenme", "什么");
            content = content.Replace("maosu", "：“");
            return content;

        }
        #endregion

        #region 替换整本书的图片章节
        /// <summary>
        /// 替换整本书的图片章节
        /// </summary>
        /// <param name="Title">标题</param>
        public void RepalceImageChapter(string Title)
        {
            status.Status = "正在从无错小说搜索书籍";
            status.BookTitle = Title;
            Status_Chage();

            var AllChapter = GetChapterTitleAndUrl(Title);

            status.Status = "正在检查本地图片章节";
            Status_Chage();
            var ChapterNeedCollect = chapterNeedCollect(Title);

            var c = from a in AllChapter
                    from b in ChapterNeedCollect
                    where a.Title == b.Title
                    select new { SourceUrl = a.Url, a.Title, chapter = b };

            foreach (var d in c)
            {
                status.Status = "采集章节";
                status.ChapterTitle = d.Title;
                Status_Chage();
                string content = GetContent(d.SourceUrl);
                ClientServices.ClientServiceClient csc = new ClientServices.ClientServiceClient();

                status.Status = "保存";
                Status_Chage();
                var newchapter = d.chapter;
                newchapter.IsImageChapter = false;
                csc.ChapterUpdate(newchapter, content);
                status.ChapterTitle = "";
                Status_Chage();
            }

        }
        #endregion 

        /// <summary>
        /// 处理整站书籍
        /// </summary>
        public void Main()
        {
            ClientServices.ClientServiceClient csc = new ClientServices.ClientServiceClient();
            var books=csc.BookSearch("id>0");

            foreach (var book in books)
            {
                RepalceImageChapter(book.Title);
            }
        }

    }
}
