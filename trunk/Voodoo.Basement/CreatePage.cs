using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Voodoo;
using Voodoo.Model;
using Voodoo.DAL;
using Voodoo.Security;
using Voodoo.IO;

using System.Reflection;
using System.Text.RegularExpressions;

namespace Voodoo.Basement
{
    public class CreatePage
    {
        #region 默认模板组
        /// <summary>
        /// 默认模板组
        /// </summary>
        public static TemplateGroup DefaultGroup
        {
            get
            {
                return TemplateGroupView.Find("IsDefault=1");
            }
        }
        #endregion

        #region 页面模板类型 enum TempType
        /// <summary>
        /// 页面模板类型 enum TempType
        /// </summary>
        public enum TempType
        {
            首页,
            控制面板,
            全站搜索,
            高级搜索,
            横向搜索JS,
            纵向搜索JS,
            相关信息,
            留言板,
            评论js调用,
            最终下载页,
            下载地址,
            在线播放地址,
            列表分页,
            登陆状态,
            JS调用登陆,
            封面,
            内容,
            列表,
            搜索,
            相册图片列表,
            问答回答列表,
            小说章节列表,
            小说章节
        }
        #endregion


        #region  创建首页
        /// <summary>
        /// 创建首页
        /// </summary>
        /// <returns></returns>
        public static string GreateIndexPage()
        {
            SysSetting setting = BasePage.SystemSetting;

            string IndexFile = "~/Book/index" + setting.ExtName;

            string Content = "";
            Content = GetTempateString(1, TempType.首页);

            Content = ReplacePublicTemplate(Content);

            Content = ReplaceSystemSetting(Content);

            PageAttribute pa = new PageAttribute() { Title = "首页", UpdateTime = DateTime.Now.ToString(), Keyword = BasePage.SystemSetting.KeyWords, Description = BasePage.SystemSetting.Description };

            Content = ReplacePageAttribute(Content, pa);

            Content = ReplaceTagContent(Content);

            //BasePage.SystemSetting.ExtName
            Voodoo.IO.File.Write(System.Web.HttpContext.Current.Server.MapPath("~/Book/Index") + BasePage.SystemSetting.ExtName, Content);


            return Content;
        }
        #endregion

        #region 生成内容页--新闻
        /// <summary>
        /// 生成内容页
        /// </summary>
        /// <param name="news"></param>
        /// <param name="cls"></param>
        public static void CreateContentPage(News news, Class cls)
        {
            if (news.NavUrl.Trim().Length > 0)//如果是外部连接新闻 则不需要生成
            {
                return;
            }

            string FileName = BasePage.GetNewsUrl(news, cls);
            string Content = "";
            TemplateContent temp = TemplateContentView.Find(string.Format("SysModel={0}", cls.ModelID));
            int tmpid = temp.ID;

            //int tmpid = news.ModelID;

            //TemplateContent temp = new TemplateContent();
            //if (tmpid <= 0)
            //{
            //    //没有选择模版
            //    tmpid = TemplateContentView.Find("id>0 order by id desc").ID;
            //}
            //temp = TemplateContentView.GetModelByID(tmpid.ToS());

            Content = GetTempateString(tmpid, TempType.内容);

            Content = ReplacePublicTemplate(Content);

            Content = ReplaceSystemSetting(Content);

            PageAttribute pa = new PageAttribute() { Title = news.Title, UpdateTime = DateTime.Now.ToString(), Description = news.Description, Keyword = news.KeyWords };

            Content = ReplacePageAttribute(Content, pa);



            #region 替换新闻内容

            string title = "<font color='#" + news.TitleColor + "'>" + news.Title + "</font>";
            if (news.TitleB)
            {
                title = "<strong>" + title + "</strong>";
            }
            if (news.TitleI)
            {
                title = "<I>" + title + "</I>";
            }
            if (news.TitleS)
            {
                title = "<STRIKE>" + title + "</STRIKE>";
            }
            Content = Content.Replace("[!--class.id--]", cls.ID.ToString());
            Content = Content.Replace("[!--class.name--]", cls.ClassName);

            Content = Content.Replace("[!--news.id--]", news.ID.ToString());
            Content = Content.Replace("[!--news.title--]", title);
            Content = Content.Replace("[!--news.newstime--]", news.NewsTime.ToString(temp.TimeFormat));
            Content = Content.Replace("[!--news.source--]", news.Source);
            Content = Content.Replace("[!--news.author--]", news.Author);
            Content = Content.Replace("[!--news.content--]", news.Content);
            Content = Content.Replace("[!--news.keywords--]", news.KeyWords);
            Content = Content.Replace("[!--news.keywordswithspance--]", news.KeyWords.Replace(",", " "));
            Content = Content.Replace("[!--news.description--]", news.Description);
            #endregion

            Content = ReplaceTagContent(Content);

            #region 上一篇  下一篇 链接
            News news_pre = BasePage.GetPreNews(news, cls);
            News news_next = BasePage.GetNextNews(news, cls);

            //上一篇
            string pre_link = "<a href=\"javascript:void(0)\">没有了</a>";
            if (news_pre != null)
            {
                pre_link = string.Format("<a href=\"{0}\">{1}</a>", BasePage.GetNewsUrl(news_pre, cls), news_pre.Title);
            }
            Content = Content.Replace("[!--news.prelink--]", pre_link);

            //下一篇
            string next_link = "<a href=\"javascript:void(0)\">没有了</a>";
            if (news_next != null)
            {
                next_link = string.Format("<a href=\"{0}\">{1}</a>", BasePage.GetNewsUrl(news_next, cls), news_next.Title);
            }
            Content = Content.Replace("[!--news.nextlink--]", next_link);

            #endregion

            //替换导航条
            Content = Content.Replace("[!--newsnav--]", BuildClassNavString(cls));

            Voodoo.IO.File.Write(System.Web.HttpContext.Current.Server.MapPath("~" + FileName), Content);
        }
        #endregion

        #region  生成内容页--相册
        /// <summary>
        /// 生成内容页--图片
        /// </summary>
        /// <param name="album"></param>
        /// <param name="cls"></param>
        public static void CreateContentPage(ImageAlbum album, Class cls)
        {


            string FileName = BasePage.GetImageUrl(album, cls);
            string Content = "";

            TemplateContent temp = TemplateContentView.Find(string.Format("SysModel={0}", cls.ModelID));
            int tmpid = temp.ID;



            Content = GetTempateString(tmpid, TempType.内容);

            Content = ReplacePublicTemplate(Content);

            Content = ReplaceSystemSetting(Content);

            PageAttribute pa = new PageAttribute() { Title = album.Title, UpdateTime = DateTime.Now.ToString(), Description = album.Intro };

            Content = ReplacePageAttribute(Content, pa);



            #region 替换内容

            Content = Content.Replace("[!--class.id--]", cls.ID.ToString());
            Content = Content.Replace("[!--class.name--]", cls.ClassName);

            Content = Content.Replace("[!--image.author--]", album.Author);
            Content = Content.Replace("[!--image.authorid--]", album.AuthorID.ToS());
            Content = Content.Replace("[!--image.clickcount--]", album.ClickCount.ToS());
            Content = Content.Replace("[!--image.createtime--]", album.CreateTime.ToString(temp.TimeFormat));
            Content = Content.Replace("[!--image.folder--]", album.Folder);
            Content = Content.Replace("[!--image.id--]", album.ID.ToS());
            Content = Content.Replace("[!--image.intro--]", album.Intro);
            Content = Content.Replace("[!--image.replycount--]", album.ReplyCount.ToS());
            Content = Content.Replace("[!--image.size--]", album.Size.ToS());
            Content = Content.Replace("[!--image.title--]", album.Title);
            Content = Content.Replace("[!--image.uploadtime--]", album.UpdateTime.ToString(temp.TimeFormat));
            Content = Content.Replace("[!--image.ztid--]", album.ZtID.ToS());

            List<Images> imgs = ImagesView.GetModelList(string.Format("AlbumID={0}", album.ID.ToS()));
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<ul>");
            foreach (Images img in imgs)
            {
                string Description = img.Intro;
                if (Description.IsNullOrEmpty())
                {
                    Description = album.Intro;
                }
                if (Description.IsNullOrEmpty())
                {
                    Description = img.Title;
                }
                if (Description.IsNullOrEmpty())
                {
                    Description = album.Title;
                }
                sb.AppendLine(string.Format("<li><a rel=\"example_group\" href=\"{0}\" title=\"{1}\"><img src=\"{2}\" /><br/>{3}</a></li>",
                    img.FilePath,
                    Description,
                    img.SmallPath,
                    img.Title.IsNullOrEmpty() ? album.Title.CutString(6) : img.Title.CutString(6)
                    ));
            }
            sb.AppendLine("</ul>");

            Content = Content.Replace("[!--image.content--]", sb.ToS());
            #endregion

            Content = ReplaceTagContent(Content);

            #region 上一篇  下一篇 链接
            ImageAlbum news_pre = BasePage.GetPreImage(album, cls);
            ImageAlbum news_next = BasePage.GetNextImages(album, cls);

            //上一篇
            string pre_link = "<a href=\"javascript:void(0)\">没有了</a>";
            if (news_pre != null)
            {
                pre_link = string.Format("<a href=\"{0}\">{1}</a>", BasePage.GetImageUrl(news_pre, cls), news_pre.Title);
            }
            Content = Content.Replace("[!--news.prelink--]", pre_link);

            //下一篇
            string next_link = "<a href=\"javascript:void(0)\">没有了</a>";
            if (news_next != null)
            {
                next_link = string.Format("<a href=\"{0}\">{1}</a>", BasePage.GetImageUrl(news_next, cls), news_next.Title);
            }
            Content = Content.Replace("[!--news.nextlink--]", next_link);

            #endregion

            //替换导航条
            Content = Content.Replace("[!--newsnav--]", BuildClassNavString(cls));

            Voodoo.IO.File.Write(System.Web.HttpContext.Current.Server.MapPath("~" + FileName), Content);
        }
        #endregion

        #region  生成内容页--问答
        /// <summary>
        /// 生成内容页--图片
        /// </summary>
        /// <param name="album"></param>
        /// <param name="cls"></param>
        public static void CreateContentPage(Question qs, Class cls)
        {


            string FileName = BasePage.GetQuestionUrl(qs, cls);
            string Content = "";

            TemplateContent temp = TemplateContentView.Find(string.Format("SysModel={0}", cls.ModelID));
            int tmpid = temp.ID;



            Content = GetTempateString(tmpid, TempType.内容);

            Content = ReplacePublicTemplate(Content);

            Content = ReplaceSystemSetting(Content);

            PageAttribute pa = new PageAttribute() { Title = qs.Title, UpdateTime = DateTime.Now.ToString(), Description = qs.Title };

            Content = ReplacePageAttribute(Content, pa);



            #region 替换内容

            Content = Content.Replace("[!--class.id--]", cls.ID.ToString());
            Content = Content.Replace("[!--class.name--]", cls.ClassName);

            Content = Content.Replace("[!--question.url--]", BasePage.GetQuestionUrl(qs, cls));//问题地址
            Content = Content.Replace("[!--question.asktime--]", qs.AskTime.ToString(temp.TimeFormat));
            Content = Content.Replace("[!--question.classid--]", qs.ClassID.ToS());
            Content = Content.Replace("[!--question.clickcount--]", qs.ClickCount.ToS());
            Content = Content.Replace("[!--question.content--]", qs.Content);
            Content = Content.Replace("[!--question.id--]", qs.ID.ToS());
            Content = Content.Replace("[!--question.title--]", qs.Title);
            Content = Content.Replace("[!--question.userid--]", qs.UserID.ToS());
            Content = Content.Replace("[!--question.username--]", qs.UserName);
            Content = Content.Replace("[!--question.ztid--]", qs.ZtID.ToS());

            List<Answer> ans = AnswerView.GetModelList(string.Format("QuestionID={0}", qs.ID.ToS()));
            StringBuilder sb = new StringBuilder();
            string list_tmp = GetTempateString(1, TempType.问答回答列表);
            //sb.AppendLine("<ul>");
            foreach (Answer an in ans)
            {
                string row = list_tmp.Replace("[!--answer.agree--]", an.Agree.ToS());
                row = row.Replace("[!--answer.answertime--]", an.AnswerTime.ToString());
                row = row.Replace("[!--answer.content--]", an.Content);
                row = row.Replace("[!--answer.id--]", an.ID.ToS());
                row = row.Replace("[!--answer.questionid--]", an.QuestionID.ToS());
                row = row.Replace("[!--answer.userid--]", an.UserID.ToS());
                row = row.Replace("[!--answer.username--]", an.UserName);

                sb.AppendLine(row);
            }
            //sb.AppendLine("</ul>");

            Content = Content.Replace("[!--answer.list--]", sb.ToS());
            #endregion

            Content = ReplaceTagContent(Content);

            #region 上一篇  下一篇 链接
            Question news_pre = BasePage.GetPreQuestion(qs, cls);
            Question news_next = BasePage.GetNextQuestion(qs, cls);

            //上一篇
            string pre_link = "<a href=\"javascript:void(0)\">没有了</a>";
            if (news_pre != null)
            {
                pre_link = string.Format("<a href=\"{0}\" title=\"{1}\">{2}</a>", BasePage.GetQuestionUrl(news_pre, cls), news_pre.Title, news_pre.Title.CutString(20));
            }
            Content = Content.Replace("[!--news.prelink--]", pre_link);

            //下一篇
            string next_link = "<a href=\"javascript:void(0)\">没有了</a>";
            if (news_next != null)
            {
                next_link = string.Format("<a href=\"{0}\" title=\"{1}\">{2}</a>", BasePage.GetQuestionUrl(news_next, cls), news_next.Title, news_next.Title.CutString(20));
            }
            Content = Content.Replace("[!--news.nextlink--]", next_link);

            #endregion

            //替换导航条
            Content = Content.Replace("[!--newsnav--]", BuildClassNavString(cls));

            Voodoo.IO.File.Write(System.Web.HttpContext.Current.Server.MapPath("~" + FileName), Content);
        }
        #endregion


        #region  生成内容页--书籍
        /// <summary>
        /// 生成内容页--图片
        /// </summary>
        /// <param name="album"></param>
        /// <param name="cls"></param>
        public static void CreateContentPage(Book b, Class cls)
        {


            string FileName = BasePage.GetBookUrl(b, cls);
            string Content = "";

            TemplateContent temp = TemplateContentView.Find(string.Format("SysModel={0}", cls.ModelID));
            int tmpid = temp.ID;



            Content = GetTempateString(tmpid, TempType.内容);

            Content = ReplacePublicTemplate(Content);

            Content = ReplaceSystemSetting(Content);

            PageAttribute pa = new PageAttribute()
            {
                Title = string.Format("{0}-{1}-{0}在线阅读-{0}txt下载", b.Title, b.Author),
                UpdateTime = DateTime.Now.ToString(),
                Description = string.Format("{0}书籍信息页面。{0}章节列表，{0}在线阅读以及txt电子书下载，为您提供包括{0}最新章节的所有章节及{0}全文阅读链接。{1}", b.Title, b.Intro.Replace("\n", "")),
                Keyword = string.Format("{0},{0}在线阅读,{0}最新章节,{0}txt下载,{1}", b.Title, b.Author)
            };

            Content = ReplacePageAttribute(Content, pa);



            #region 替换内容

            Content = Content.Replace("[!--class.id--]", cls.ID.ToString());
            Content = Content.Replace("[!--class.name--]", cls.ClassName);
            Content = Content.Replace("[!--class.url--]", BasePage.GetClassUrl(cls));

            Content = Content.Replace("[!--book.url--]", BasePage.GetBookUrl(b, cls));//问题地址
            Content = Content.Replace("[!--book.id--]", b.ID.ToString());
            Content = Content.Replace("[!--book.classid--]", b.ClassID.ToS());
            Content = Content.Replace("[!--book.classname--]", b.ClassName);
            Content = Content.Replace("[!--book.ztid--]", b.ZtID.ToS());
            Content = Content.Replace("[!--book.ztname--]", b.ZtName);
            Content = Content.Replace("[!--book.title--]", b.Title);
            Content = Content.Replace("[!--book.oldtitle--]", b.Title);
            Content = Content.Replace("[!--book.author--]", b.Author);
            Content = Content.Replace("[!--book.intro--]", b.Intro.Replace("\n", "<br />"));
            Content = Content.Replace("[!--book.length--]", b.Length.ToS());
            Content = Content.Replace("[!--book.replycount--]", b.ReplyCount.ToS());
            Content = Content.Replace("[!--book.addtime--]", b.Addtime.ToString(temp.TimeFormat));
            Content = Content.Replace("[!--book.status--]", b.Status.ToS());
            Content = Content.Replace("[!--book.isvip--]", b.IsVip.ToChinese());
            Content = Content.Replace("[!--book.faceimage--]", b.FaceImage);
            Content = Content.Replace("[!--book.savecount--]", b.SaveCount.ToS());
            Content = Content.Replace("[!--book.enable--]", b.Enable.ToChinese());
            Content = Content.Replace("[!--book.isfirstpost--]", b.IsFirstPost.ToChinese());
            Content = Content.Replace("[!--book.lastchapterid--]", b.LastChapterID.ToS());
            Content = Content.Replace("[!--book.lastchaptertitle--]", b.LastChapterTitle);
            Content = Content.Replace("[!--book.updatetime--]", b.UpdateTime.ToString(temp.TimeFormat));
            Content = Content.Replace("[!--book.lastvipchapterid--]", b.LastVipChapterID.ToS());
            Content = Content.Replace("[!--book.lastvipchaptertitle--]", b.LastVipChapterTitle);
            Content = Content.Replace("[!--book.vipupdatetime--]", b.VipUpdateTime.ToString(temp.TimeFormat));
            Content = Content.Replace("[!--book.corpusid--]", b.CorpusID.ToS());
            Content = Content.Replace("[!--book.corpustitle--]", b.CorpusTitle);

            List<BookChapter> chapters = BookChapterView.GetModelList(string.Format("BookID={0}", b.ID.ToS()));

            StringBuilder sb = new StringBuilder();
            string list_tmp = GetTempateString(1, TempType.小说章节列表);
            //sb.AppendLine("<ul>");
            foreach (BookChapter cp in chapters)
            {
                string row = list_tmp.Replace("[!--chapter.url--]", BasePage.GetBookChapterUrl(cp, cls));
                row = row.Replace("[!--chapter.title--]", cp.Title);

                sb.AppendLine(row);
            }
            //sb.AppendLine("</ul>");

            Content = Content.Replace("[!--chapter.list--]", sb.ToS());
            #endregion

            Content = ReplaceTagContent(Content);

            #region 上一篇  下一篇 链接
            Book news_pre = BasePage.GetPreBook(b, cls);
            Book news_next = BasePage.GetNextBook(b, cls);

            //上一篇
            string pre_link = "<a href=\"javascript:void(0)\">没有了</a>";
            if (news_pre != null)
            {
                pre_link = string.Format("<a href=\"{0}\" title=\"{1}\">{2}</a>", BasePage.GetBookUrl(news_pre, cls), news_pre.Title, news_pre.Title.CutString(20));
            }
            Content = Content.Replace("[!--news.prelink--]", pre_link);

            //下一篇
            string next_link = "<a href=\"javascript:void(0)\">没有了</a>";
            if (news_next != null)
            {
                next_link = string.Format("<a href=\"{0}\" title=\"{1}\">{2}</a>", BasePage.GetBookUrl(news_next, cls), news_next.Title, news_next.Title.CutString(20));
            }
            Content = Content.Replace("[!--news.nextlink--]", next_link);

            #endregion

            //替换导航条
            Content = Content.Replace("[!--newsnav--]", BuildClassNavString(cls));

            Voodoo.IO.File.Write(System.Web.HttpContext.Current.Server.MapPath("~" + FileName), Content);
        }
        #endregion

        #region  创建章节页面
        /// <summary>
        /// 创建章节页面
        /// </summary>
        /// <param name="cp"></param>
        /// <param name="b"></param>
        /// <param name="cls"></param>
        public static void CreateBookChapterPage(BookChapter cp, Book b, Class cls)
        {
            string FileName = BasePage.GetBookChapterUrl(cp, cls);
            string Content = GetTempateString(1, TempType.小说章节);

            Content = ReplacePublicTemplate(Content);

            Content = ReplaceSystemSetting(Content);

            string ChapterContent = Voodoo.IO.File.Read(System.Web.HttpContext.Current.Server.MapPath(BasePage.GetBookChapterTxtUrl(cp, cls)));
            ChapterContent = ChapterContent.Replace("<<", "<br />");

            PageAttribute pa = new PageAttribute()
            {
                Title = string.Format("{0}-{1}-{2}", b.Title, cp.Title, b.Author),
                UpdateTime = DateTime.Now.ToString(),
                Description = ChapterContent.TrimHTML().Replace("\n", "").CutString(100),
                Keyword = string.Format("{0},{1}最新章节,{1}txt下载,{1}在线阅读", cp.Title, b.Title)
            };

            Content = ReplacePageAttribute(Content, pa);

            #region 替换内容

            //替换栏目
            Content = Content.Replace("[!--class.id--]", cls.ID.ToString());
            Content = Content.Replace("[!--class.name--]", cls.ClassName);
            Content = Content.Replace("[!--class.url--]", BasePage.GetClassUrl(cls));

            //替换书籍信息
            Content = Content.Replace("[!--book.url--]", BasePage.GetBookUrl(b, cls));//问题地址
            Content = Content.Replace("[!--book.id--]", b.ID.ToString());
            Content = Content.Replace("[!--book.classid--]", b.ClassID.ToS());
            Content = Content.Replace("[!--book.classname--]", b.ClassName);
            Content = Content.Replace("[!--book.ztid--]", b.ZtID.ToS());
            Content = Content.Replace("[!--book.ztname--]", b.ZtName);
            Content = Content.Replace("[!--book.title--]", b.Title);
            Content = Content.Replace("[!--book.oldtitle--]", b.Title);
            Content = Content.Replace("[!--book.author--]", b.Author);
            Content = Content.Replace("[!--book.intro--]", b.Intro);
            Content = Content.Replace("[!--book.length--]", b.Length.ToS());
            Content = Content.Replace("[!--book.replycount--]", b.ReplyCount.ToS());
            Content = Content.Replace("[!--book.addtime--]", b.Addtime.ToString());
            Content = Content.Replace("[!--book.status--]", b.Status.ToS());
            Content = Content.Replace("[!--book.isvip--]", b.IsVip.ToChinese());
            Content = Content.Replace("[!--book.faceimage--]", b.FaceImage);
            Content = Content.Replace("[!--book.savecount--]", b.SaveCount.ToS());
            Content = Content.Replace("[!--book.enable--]", b.Enable.ToChinese());
            Content = Content.Replace("[!--book.isfirstpost--]", b.IsFirstPost.ToChinese());
            Content = Content.Replace("[!--book.lastchapterid--]", b.LastChapterID.ToS());
            Content = Content.Replace("[!--book.lastchaptertitle--]", b.LastChapterTitle);
            Content = Content.Replace("[!--book.updatetime--]", b.UpdateTime.ToString());
            Content = Content.Replace("[!--book.lastvipchapterid--]", b.LastVipChapterID.ToS());
            Content = Content.Replace("[!--book.lastvipchaptertitle--]", b.LastVipChapterTitle);
            Content = Content.Replace("[!--book.vipupdatetime--]", b.VipUpdateTime.ToString());
            Content = Content.Replace("[!--book.corpusid--]", b.CorpusID.ToS());
            Content = Content.Replace("[!--book.corpustitle--]", b.CorpusTitle);

            //替换章节信息

            Content = Content.Replace("[!--chapter.id--]", cp.ID.ToS());
            Content = Content.Replace("[!--chapter.bookid--]", cp.BookID.ToS());
            Content = Content.Replace("[!--chapter.booktitle--]", cp.BookTitle);
            Content = Content.Replace("[!--chapter.classid--]", cp.ClassID.ToS());
            Content = Content.Replace("[!--chapter.classname--]", cp.ClassName);
            Content = Content.Replace("[!--chapter.valumeid--]", cp.ValumeID.ToS());
            Content = Content.Replace("[!--chapter.valumename--]", cp.ValumeName);
            Content = Content.Replace("[!--chapter.title--]", cp.Title);
            Content = Content.Replace("[!--chapter.isvipchapter--]", cp.IsVipChapter.ToChinese());
            Content = Content.Replace("[!--chapter.textlength--]", cp.TextLength.ToS());
            Content = Content.Replace("[!--chapter.updatetime--]", cp.UpdateTime.ToS());
            Content = Content.Replace("[!--chapter.enable--]", cp.Enable.ToChinese());
            Content = Content.Replace("[!--chapter.istemp--]", cp.IsTemp.ToChinese());
            Content = Content.Replace("[!--chapter.isfree--]", cp.IsFree.ToChinese());
            Content = Content.Replace("[!--chapter.chapterindex--]", cp.ChapterIndex.ToS());
            Content = Content.Replace("[!--chapter.isimagechapter--]", cp.IsImageChapter.ToChinese());
            Content = Content.Replace("[!--chapter.content--]", ChapterContent);
            Content = Content.Replace("[!--chapter.clickcount--]", cp.ClickCount.ToS());

            #endregion

            Content = ReplaceTagContent(Content);

            #region 上一篇  下一篇 链接
            BookChapter news_pre = BasePage.GetPreChapter(cp, b);
            BookChapter news_next = BasePage.GetNextChapter(cp, b);

            //上一篇
            string pre_link = "<a href=\"javascript:void(0)\">上章：没有了</a>";
            if (news_pre != null)
            {
                pre_link = string.Format("<a href=\"{0}\" title=\"{1}\">上章：{2}</a>", BasePage.GetBookChapterUrl(news_pre, cls), news_pre.Title, news_pre.Title.CutString(20));
            }
            Content = Content.Replace("[!--news.prelink--]", pre_link);

            //下一篇
            string next_link = "<a href=\"javascript:void(0)\">下章：没有了</a>";
            if (news_next != null)
            {
                next_link = string.Format("<a href=\"{0}\" title=\"{1}\">下章：{2}</a>", BasePage.GetBookChapterUrl(news_next, cls), news_next.Title, news_next.Title.CutString(20));
            }
            Content = Content.Replace("[!--news.nextlink--]", next_link);

            #endregion

            //替换导航条
            Content = Content.Replace("[!--newsnav--]", BuildClassNavString(cls));

            Voodoo.IO.File.Write(System.Web.HttpContext.Current.Server.MapPath("~" + FileName), Content);

        }
        #endregion


        #region 创建列表页面
        /// <summary>
        /// 创建列表页面
        /// </summary>
        /// <param name="c"></param>
        /// <param name="page"></param>
        public static void CreateListPage(Class c, int page)
        {
            int pagecount = 1;
            int recordCount = 0;
            string Content = "";
            int tmpid = 0;
            TemplateList temp = new TemplateList();
            if (tmpid <= 0)
            {
                //没有选择模版
                tmpid = TemplateListView.Find("id>0 order by id").ID;
            }
            temp = TemplateListView.GetModelByID(tmpid.ToS());
            temp = TemplateListView.Find(string.Format("SysModel={0}", c.ModelID));

            Content = GetTempateString(temp.ID, TempType.列表);

            Content = ReplacePublicTemplate(Content);

            Content = ReplaceSystemSetting(Content);

            PageAttribute pa = new PageAttribute() { Title = c.ClassName, UpdateTime = DateTime.Now.ToString(), Description = c.ClassDescription, Keyword = c.ClassKeywords };

            Content = Content.Replace("[!--class.description--]", c.ClassDescription);
            Content = Content.Replace("[!--class.classname--]", c.ClassName);
            Content = Content.Replace("[!--class.id--]", c.ID.ToS());
            Content = ReplacePageAttribute(Content, pa);

            //此处要区分系统模型
            #region 替换列表

            #region 新闻系统模板
            if (c.ModelID == 1)//新闻系统模板
            {
                StringBuilder sb_list = new StringBuilder();
                List<News> ns = NewsView.GetModelList(string.Format("ClassID={0} and Audit=1 order by SetTop desc, id desc", c.ID)).ToList();

                pagecount = (Convert.ToDouble(ns.Count) / Convert.ToDouble(temp.ShowRecordCount)).YueShu();
                recordCount = ns.Count;

                ns = ns.Skip((page - 1) * temp.ShowRecordCount).Take(temp.ShowRecordCount).ToList();
                foreach (News n in ns)
                {
                    //<li><span>[!--newstime--]</span><a href="[!--titleurl--]" title="[!--oldtitle--]">[!--title--]</a> </li>

                    string _title = "<font color='#" + n.TitleColor + "'>" + n.Title + "</font>";
                    if (n.TitleB)
                    {
                        _title = "<strong>" + _title + "</strong>";
                    }
                    if (n.TitleI)
                    {
                        _title = "<I>" + _title + "</I>";
                    }
                    if (n.TitleS)
                    {
                        _title = "<STRIKE>" + _title + "</STRIKE>";
                    }

                    string str_lst = temp.ListVar;
                    str_lst = str_lst.Replace("[!--newstime--]", n.NewsTime.ToString(temp.TimeFormat));
                    str_lst = str_lst.Replace("[!--titleurl--]", BasePage.GetNewsUrl(n, c));
                    str_lst = str_lst.Replace("[!--oldtitle--]", _title);
                    str_lst = str_lst.Replace("[!--description--]", n.Description);
                    str_lst = str_lst.Replace("[!--author--]", n.Author);
                    str_lst = str_lst.Replace("[!--id--]", n.ID.ToS());
                    string title = n.Title;
                    if (temp.CutTitle > 0)
                    {
                        title = title.CutString(temp.CutTitle);
                    }
                    str_lst = str_lst.Replace("[!--title--]", n.Title);
                    sb_list.AppendLine(str_lst);
                }

                Content = Content.Replace("<!--list.var-->", sb_list.ToString());
            }//end 新闻系统模板
            #endregion  新闻系统模板

            #region 图片系统模板
            else if (c.ModelID == 2)//图片系统模板
            {
                StringBuilder sb_list = new StringBuilder();
                List<ImageAlbum> ns = ImageAlbumView.GetModelList(string.Format("ClassID={0} order by SetTop desc, id desc", c.ID)).ToList();
                pagecount = (Convert.ToDouble(ns.Count) / Convert.ToDouble(temp.ShowRecordCount)).YueShu();
                recordCount = ns.Count;

                ns = ns.Skip((page - 1) * temp.ShowRecordCount).Take(temp.ShowRecordCount).ToList();
                foreach (ImageAlbum n in ns)
                {
                    string str_lst = temp.ListVar;
                    str_lst = str_lst.Replace("[!--image.author--]", n.Author);
                    str_lst = str_lst.Replace("[!--image.authorid--]", n.AuthorID.ToS());
                    str_lst = str_lst.Replace("[!--image.classid--]", n.ClassID.ToS());
                    str_lst = str_lst.Replace("[!--image.clickcount--]", n.ClickCount.ToS());
                    str_lst = str_lst.Replace("[!--image.createtime--]", n.CreateTime.ToString());
                    str_lst = str_lst.Replace("[!--image.folder--]", n.Folder);
                    str_lst = str_lst.Replace("[!--image.id--]", n.ID.ToS());
                    str_lst = str_lst.Replace("[!--image.intro--]", n.Intro);
                    str_lst = str_lst.Replace("[!--image.replycount--]", n.ReplyCount.ToS());
                    str_lst = str_lst.Replace("[!--image.size--]", n.Size.ToS());
                    str_lst = str_lst.Replace("[!--image.title--]", n.Title);
                    str_lst = str_lst.Replace("[!--image.updatetime--]", n.UpdateTime.ToS());
                    str_lst = str_lst.Replace("[!--image.ztid--]", n.ZtID.ToS());
                    str_lst = str_lst.Replace("[!--image.url--]", BasePage.GetImageUrl(n, c));

                    sb_list.AppendLine(str_lst);
                }

                Content = Content.Replace("<!--list.var-->", sb_list.ToString());
            }
            #endregion  图片系统模板

            #region 问答系统
            else if (c.ModelID == 3)
            {
                StringBuilder sb_list = new StringBuilder();
                List<Question> qs = QuestionView.GetModelList(string.Format("ClassID={0} order by id desc", c.ID));
                pagecount = (Convert.ToDouble(qs.Count) / Convert.ToDouble(temp.ShowRecordCount)).YueShu();
                recordCount = qs.Count;

                qs = qs.Skip((page - 1) * temp.ShowRecordCount).Take(temp.ShowRecordCount).ToList();
                foreach (Question q in qs)
                {
                    string str_lst = temp.ListVar;
                    str_lst = str_lst.Replace("[!--question.url--]", BasePage.GetQuestionUrl(q, c));//问题地址
                    str_lst = str_lst.Replace("[!--question.asktime--]", q.AskTime.ToString(temp.TimeFormat));
                    str_lst = str_lst.Replace("[!--question.classid--]", q.ClassID.ToS());
                    str_lst = str_lst.Replace("[!--question.clickcount--]", q.ClickCount.ToS());
                    str_lst = str_lst.Replace("[!--question.content--]", q.Content);
                    str_lst = str_lst.Replace("[!--question.id--]", q.ID.ToS());
                    str_lst = str_lst.Replace("[!--question.title--]", temp.CutTitle > 0 ? q.Title.CutString(temp.CutTitle) : q.Title);
                    str_lst = str_lst.Replace("[!--question.oldtitle--]", q.Title);
                    str_lst = str_lst.Replace("[!--question.userid--]", q.UserID.ToS());
                    str_lst = str_lst.Replace("[!--question.username--]", q.UserName);
                    str_lst = str_lst.Replace("[!--question.ztid--]", q.ZtID.ToS());
                    str_lst = str_lst.Replace("[!--question.answercount--]", AnswerView.Count(string.Format("QuestionID={0}", q.ID)).ToS());
                    sb_list.AppendLine(str_lst);
                }
                Content = Content.Replace("<!--list.var-->", sb_list.ToString());
            }
            #endregion 问答系统

            #region 小说系统
            else if (c.ModelID == 4)
            {
                StringBuilder sb_list = new StringBuilder();
                List<Book> qs = BookView.GetModelList(string.Format("ClassID in(select id from Class where ID={0} union select id from Class where ParentID={0}) order by id desc", c.ID));
                pagecount = (Convert.ToDouble(qs.Count) / Convert.ToDouble(temp.ShowRecordCount)).YueShu();
                recordCount = qs.Count;

                qs = qs.Skip((page - 1) * temp.ShowRecordCount).Take(temp.ShowRecordCount).ToList();
                foreach (Book b in qs)
                {
                    string str_lst = temp.ListVar;
                    str_lst = str_lst.Replace("[!--book.url--]", BasePage.GetBookUrl(b, BookView.GetClass(b)));//书籍
                    str_lst = str_lst.Replace("[!--book.lastchapterurl--]", BasePage.GetBookChapterUrl(BookChapterView.GetModelByID(b.LastChapterID.ToS()), BookView.GetClass(b)));//书籍
                    str_lst = str_lst.Replace("[!--book.id--]", b.ID.ToString());
                    str_lst = str_lst.Replace("[!--book.classid--]", b.ClassID.ToS());
                    str_lst = str_lst.Replace("[!--book.classname--]", b.ClassName);
                    str_lst = str_lst.Replace("[!--book.ztid--]", b.ZtID.ToS());
                    str_lst = str_lst.Replace("[!--book.ztname--]", b.ZtName);
                    str_lst = str_lst.Replace("[!--book.title--]", temp.CutTitle > 0 ? b.Title.CutString(temp.CutTitle) : b.Title);
                    str_lst = str_lst.Replace("[!--book.oldtitle--]", b.Title);
                    str_lst = str_lst.Replace("[!--book.author--]", b.Author);
                    str_lst = str_lst.Replace("[!--book.intro--]", b.Intro);
                    str_lst = str_lst.Replace("[!--book.length--]", b.Length.ToS());
                    str_lst = str_lst.Replace("[!--book.replycount--]", b.ReplyCount.ToS());
                    str_lst = str_lst.Replace("[!--book.addtime--]", b.Addtime.ToString(temp.TimeFormat));
                    str_lst = str_lst.Replace("[!--book.status--]", b.Status.ToS());
                    str_lst = str_lst.Replace("[!--book.isvip--]", b.IsVip.ToChinese());
                    str_lst = str_lst.Replace("[!--book.faceimage--]", b.FaceImage);
                    str_lst = str_lst.Replace("[!--book.savecount--]", b.SaveCount.ToS());
                    str_lst = str_lst.Replace("[!--book.enable--]", b.Enable.ToChinese());
                    str_lst = str_lst.Replace("[!--book.isfirstpost--]", b.IsFirstPost.ToChinese());
                    str_lst = str_lst.Replace("[!--book.lastchapterid--]", b.LastChapterID.ToS());
                    str_lst = str_lst.Replace("[!--book.lastchaptertitle--]", b.LastChapterTitle);
                    str_lst = str_lst.Replace("[!--book.updatetime--]", b.UpdateTime.ToString(temp.TimeFormat));
                    str_lst = str_lst.Replace("[!--book.lastvipchapterid--]", b.LastVipChapterID.ToS());
                    str_lst = str_lst.Replace("[!--book.lastvipchaptertitle--]", b.LastVipChapterTitle);
                    str_lst = str_lst.Replace("[!--book.vipupdatetime--]", b.VipUpdateTime.ToString(temp.TimeFormat));
                    str_lst = str_lst.Replace("[!--book.corpusid--]", b.CorpusID.ToS());
                    str_lst = str_lst.Replace("[!--book.corpustitle--]", b.CorpusTitle);
                    str_lst = str_lst.Replace("[!--book.clickcount--]", b.ClickCount.ToS());
                    str_lst = str_lst.Replace("[!--book.tjcount--]", b.TjCount.ToS());
                    sb_list.AppendLine(str_lst);
                }
                Content = Content.Replace("<!--list.var-->", sb_list.ToString());
            }
            #endregion 小说系统

            #endregion


            //替换标签变量
            Content = ReplaceTagContent(Content);

            #region 替换分页模板

            string tmp_pager = GetTempateString(tmpid, TempType.列表分页);
            tmp_pager = tmp_pager.Replace("[!--thispage--]", page.ToS());
            tmp_pager = tmp_pager.Replace("[!--pagenum--]", pagecount.ToS());
            tmp_pager = tmp_pager.Replace("[!--lencord--]", temp.ShowRecordCount.ToS());
            tmp_pager = tmp_pager.Replace("[!--num--]", recordCount.ToS());
            tmp_pager = tmp_pager.Replace("[!--pagelink--]", BuildPagerLink(c, page));
            tmp_pager = tmp_pager.Replace("[!--options--]", BuidPagerOption(c, page));

            if (recordCount <= temp.ShowRecordCount)
            {
                tmp_pager = "";
            }

            Content = Content.Replace("[!--show.listpage--]", tmp_pager);

            #endregion

            //替换导航条
            Content = Content.Replace("[!--newsnav--]", BuildClassNavString(c));

            string FileName = BasePage.GetClassUrl(c, page);
            Voodoo.IO.File.Write(System.Web.HttpContext.Current.Server.MapPath(FileName), Content);


            //下一页链接
            if (pagecount > page)
            {
                CreateListPage(c, page + 1);
            }
        }
        #endregion


        #region 搜索页面
        /// <summary>
        /// 搜索页面
        /// </summary>
        /// <param name="SysModel"></param>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetSearchResult(int SysModel, int page, string key)
        {
            #region 搜索关键词保存到数据库中

            BasePage.InsertKeyWords(SysModel, key);
            #endregion

            int pagecount = 1;
            int recordCount = 0;

            string Content = GetTempateString(1, TempType.全站搜索);
            Content = ReplacePublicTemplate(Content);

            Content = ReplaceSystemSetting(Content);

            PageAttribute pa = new PageAttribute() { Title = key, UpdateTime = DateTime.Now.ToString(), Description = "全站搜索" };

            Content = ReplacePageAttribute(Content, pa);

            Content = Content.Replace("[!--search.key--]", key);

            //此处要区分系统模型
            #region 替换列表

            #region 未完成
            //#region 新闻系统模板
            //if (SysModel == 1)//新闻系统模板
            //{
            //    StringBuilder sb_list = new StringBuilder();
            //    List<News> ns = NewsView.GetModelList(string.Format("ClassID={0} and Audit=1 order by SetTop desc, id desc", c.ID)).ToList();

            //    pagecount = (Convert.ToDouble(ns.Count) / Convert.ToDouble(20)).YueShu();
            //    recordCount = ns.Count;

            //    ns = ns.Skip((page - 1) * 20).Take(20).ToList();
            //    foreach (News n in ns)
            //    {
            //        //<li><span>[!--newstime--]</span><a href="[!--titleurl--]" title="[!--oldtitle--]">[!--title--]</a> </li>

            //        string _title = "<font color='#" + n.TitleColor + "'>" + n.Title + "</font>";
            //        if (n.TitleB)
            //        {
            //            _title = "<strong>" + _title + "</strong>";
            //        }
            //        if (n.TitleI)
            //        {
            //            _title = "<I>" + _title + "</I>";
            //        }
            //        if (n.TitleS)
            //        {
            //            _title = "<STRIKE>" + _title + "</STRIKE>";
            //        }

            //        string str_lst = temp.ListVar;
            //        str_lst = str_lst.Replace("[!--newstime--]", n.NewsTime.ToString(temp.TimeFormat));
            //        str_lst = str_lst.Replace("[!--titleurl--]", BasePage.GetNewsUrl(n, c));
            //        str_lst = str_lst.Replace("[!--oldtitle--]", _title);
            //        str_lst = str_lst.Replace("[!--description--]", n.Description);
            //        str_lst = str_lst.Replace("[!--author--]", n.Author);
            //        str_lst = str_lst.Replace("[!--id--]", n.ID.ToS());
            //        string title = n.Title;
            //        str_lst = str_lst.Replace("[!--title--]", n.Title);
            //        sb_list.AppendLine(str_lst);
            //    }

            //    Content = Content.Replace("<!--list.var-->", sb_list.ToString());
            //}//end 新闻系统模板
            //#endregion  新闻系统模板

            //#region 图片系统模板
            //else if (SysModel == 2)//图片系统模板
            //{
            //    StringBuilder sb_list = new StringBuilder();
            //    List<ImageAlbum> ns = ImageAlbumView.GetModelList(string.Format("ClassID={0} order by SetTop desc, id desc", c.ID)).ToList();
            //    pagecount = (Convert.ToDouble(ns.Count) / Convert.ToDouble(20)).YueShu();
            //    recordCount = ns.Count;

            //    ns = ns.Skip((page - 1) * 20).Take(20).ToList();
            //    foreach (ImageAlbum n in ns)
            //    {
            //        string str_lst = temp.ListVar;
            //        str_lst = str_lst.Replace("[!--image.author--]", n.Author);
            //        str_lst = str_lst.Replace("[!--image.authorid--]", n.AuthorID.ToS());
            //        str_lst = str_lst.Replace("[!--image.classid--]", n.ClassID.ToS());
            //        str_lst = str_lst.Replace("[!--image.clickcount--]", n.ClickCount.ToS());
            //        str_lst = str_lst.Replace("[!--image.createtime--]", n.CreateTime.ToString());
            //        str_lst = str_lst.Replace("[!--image.folder--]", n.Folder);
            //        str_lst = str_lst.Replace("[!--image.id--]", n.ID.ToS());
            //        str_lst = str_lst.Replace("[!--image.intro--]", n.Intro);
            //        str_lst = str_lst.Replace("[!--image.replycount--]", n.ReplyCount.ToS());
            //        str_lst = str_lst.Replace("[!--image.size--]", n.Size.ToS());
            //        str_lst = str_lst.Replace("[!--image.title--]", n.Title);
            //        str_lst = str_lst.Replace("[!--image.updatetime--]", n.UpdateTime.ToS());
            //        str_lst = str_lst.Replace("[!--image.ztid--]", n.ZtID.ToS());
            //        str_lst = str_lst.Replace("[!--image.url--]", BasePage.GetImageUrl(n, c));

            //        sb_list.AppendLine(str_lst);
            //    }

            //    Content = Content.Replace("<!--list.var-->", sb_list.ToString());
            //}
            //#endregion  图片系统模板

            //#region 问答系统
            //else if (SysModel == 3)
            //{
            //    StringBuilder sb_list = new StringBuilder();
            //    List<Question> qs = QuestionView.GetModelList(string.Format("ClassID={0} order by id desc", c.ID));
            //    pagecount = (Convert.ToDouble(qs.Count) / Convert.ToDouble(20)).YueShu();
            //    recordCount = qs.Count;

            //    qs = qs.Skip((page - 1) * 20).Take(20).ToList();
            //    foreach (Question q in qs)
            //    {
            //        string str_lst = temp.ListVar;
            //        str_lst = str_lst.Replace("[!--question.url--]", BasePage.GetQuestionUrl(q, c));//问题地址
            //        str_lst = str_lst.Replace("[!--question.asktime--]", q.AskTime.ToString(temp.TimeFormat));
            //        str_lst = str_lst.Replace("[!--question.classid--]", q.ClassID.ToS());
            //        str_lst = str_lst.Replace("[!--question.clickcount--]", q.ClickCount.ToS());
            //        str_lst = str_lst.Replace("[!--question.content--]", q.Content);
            //        str_lst = str_lst.Replace("[!--question.id--]", q.ID.ToS());
            //        str_lst = str_lst.Replace("[!--question.title--]", temp.CutTitle > 0 ? q.Title.CutString(temp.CutTitle) : q.Title);
            //        str_lst = str_lst.Replace("[!--question.oldtitle--]", q.Title);
            //        str_lst = str_lst.Replace("[!--question.userid--]", q.UserID.ToS());
            //        str_lst = str_lst.Replace("[!--question.username--]", q.UserName);
            //        str_lst = str_lst.Replace("[!--question.ztid--]", q.ZtID.ToS());
            //        str_lst = str_lst.Replace("[!--question.answercount--]", AnswerView.Count(string.Format("QuestionID={0}", q.ID)).ToS());
            //        sb_list.AppendLine(str_lst);
            //    }
            //    Content = Content.Replace("<!--list.var-->", sb_list.ToString());
            //}
            //#endregion 问答系统
            #endregion

            #region 小说系统
            if (SysModel == 4)
            {
                StringBuilder sb_list = new StringBuilder();
                List<Book> qs = BookView.GetModelList(string.Format("Title like N'%{0}%' or Author like N'%{0}%' or Intro like N'%{0}%' order by id desc", key));
                pagecount = (Convert.ToDouble(qs.Count) / Convert.ToDouble(20)).YueShu();
                recordCount = qs.Count;

                qs = qs.Skip((page - 1) * 20).Take(20).ToList();
                foreach (Book b in qs)
                {
                    TemplateList temp = TemplateListView.Find("SysModel=4");
                    string str_lst = temp.ListVar;
                    str_lst = str_lst.Replace("[!--book.url--]", BasePage.GetBookUrl(b, BookView.GetClass(b)));//书籍
                    str_lst = str_lst.Replace("[!--book.lastchapterurl--]", BasePage.GetBookChapterUrl(BookChapterView.GetModelByID(b.LastChapterID.ToS()), BookView.GetClass(b)));//书籍
                    str_lst = str_lst.Replace("[!--book.id--]", b.ID.ToString());
                    str_lst = str_lst.Replace("[!--book.classid--]", b.ClassID.ToS());
                    str_lst = str_lst.Replace("[!--book.classname--]", b.ClassName);
                    str_lst = str_lst.Replace("[!--book.ztid--]", b.ZtID.ToS());
                    str_lst = str_lst.Replace("[!--book.ztname--]", b.ZtName);
                    str_lst = str_lst.Replace("[!--book.title--]", temp.CutTitle > 0 ? b.Title.CutString(temp.CutTitle) : b.Title);
                    str_lst = str_lst.Replace("[!--book.oldtitle--]", b.Title);
                    str_lst = str_lst.Replace("[!--book.author--]", b.Author);
                    str_lst = str_lst.Replace("[!--book.intro--]", b.Intro);
                    str_lst = str_lst.Replace("[!--book.length--]", b.Length.ToS());
                    str_lst = str_lst.Replace("[!--book.replycount--]", b.ReplyCount.ToS());
                    str_lst = str_lst.Replace("[!--book.addtime--]", b.Addtime.ToString(temp.TimeFormat));
                    str_lst = str_lst.Replace("[!--book.status--]", b.Status.ToS());
                    str_lst = str_lst.Replace("[!--book.isvip--]", b.IsVip.ToChinese());
                    str_lst = str_lst.Replace("[!--book.faceimage--]", b.FaceImage);
                    str_lst = str_lst.Replace("[!--book.savecount--]", b.SaveCount.ToS());
                    str_lst = str_lst.Replace("[!--book.enable--]", b.Enable.ToChinese());
                    str_lst = str_lst.Replace("[!--book.isfirstpost--]", b.IsFirstPost.ToChinese());
                    str_lst = str_lst.Replace("[!--book.lastchapterid--]", b.LastChapterID.ToS());
                    str_lst = str_lst.Replace("[!--book.lastchaptertitle--]", b.LastChapterTitle);
                    str_lst = str_lst.Replace("[!--book.updatetime--]", b.UpdateTime.ToString(temp.TimeFormat));
                    str_lst = str_lst.Replace("[!--book.lastvipchapterid--]", b.LastVipChapterID.ToS());
                    str_lst = str_lst.Replace("[!--book.lastvipchaptertitle--]", b.LastVipChapterTitle);
                    str_lst = str_lst.Replace("[!--book.vipupdatetime--]", b.VipUpdateTime.ToString(temp.TimeFormat));
                    str_lst = str_lst.Replace("[!--book.corpusid--]", b.CorpusID.ToS());
                    str_lst = str_lst.Replace("[!--book.corpustitle--]", b.CorpusTitle);

                    str_lst = str_lst.Replace("[!--book.clickcount--]", b.ClickCount.ToS());
                    str_lst = str_lst.Replace("[!--book.tjcount--]", b.TjCount.ToS());
                    sb_list.AppendLine(str_lst);
                }
                Content = Content.Replace("<!--list.var-->", sb_list.ToString());
            }
            #endregion 小说系统

            #endregion

            //替换标签变量
            Content = ReplaceTagContent(Content);

            #region 替换分页模板

            string tmp_pager = GetTempateString(1, TempType.列表分页);
            tmp_pager = tmp_pager.Replace("[!--thispage--]", page.ToS());
            tmp_pager = tmp_pager.Replace("[!--pagenum--]", pagecount.ToS());
            tmp_pager = tmp_pager.Replace("[!--lencord--]", "20");
            tmp_pager = tmp_pager.Replace("[!--num--]", recordCount.ToS());
            tmp_pager = tmp_pager.Replace("[!--pagelink--]", BuildPagerLink(key, page));
            tmp_pager = tmp_pager.Replace("[!--options--]", BuidPagerOption(key, page));

            if (recordCount <= 20)
            {
                tmp_pager = "";
            }

            Content = Content.Replace("[!--show.listpage--]", tmp_pager);

            #endregion

            return Content;

        }
        #endregion






        #region  创建页头和页尾 CreateHeaderString CreateFooterString
        public static string CreateHeaderString(string title)
        {
            string Content = "";
            Content += GetPublicTemplate("header");


            PageAttribute pa = new PageAttribute() { Title = title, UpdateTime = DateTime.Now.ToString() };

            Content = ReplacePageAttribute(Content, pa);

            Content = ReplaceTagContent(Content);

            Content = ReplaceSystemSetting(Content);

            return Content;
        }

        public static string CreateFooterString()
        {
            string Content = "";
            Content += GetPublicTemplate("footer");



            Content = ReplaceTagContent(Content);

            Content = ReplaceSystemSetting(Content);

            return Content;
        }
        #endregion

        #region  创建用户功能菜单
        public static string BuildUserMenuString()
        {
            StringBuilder sb = new StringBuilder();
            //sb.AppendLine("<li><a href=\"#\">会员登陆</a></li>");
            sb.AppendLine("<li><a href=\"/e/member/ChangeRegister.aspx\">注册帐号</a></li>");
            sb.AppendLine("<li><a href=\"/e/post/PostList.aspx\">我的文章</a></li>");
            sb.AppendLine("<li><a href=\"/e/post/PostNews.aspx\">发布投稿</a></li>");

            return sb.ToString();
        }
        #endregion

        #region  创建 上页 下页 首页 尾页链接
        /// <summary>
        /// 创建分页链接
        /// </summary>
        /// <param name="c"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static string BuildPagerLink(Class c, int page)
        {
            int recordCount = c.CountSubItem();
            int tmpid = 0;
            TemplateList temp = new TemplateList();
            if (c.ModelID <= 0)
            {
                //没有选择模版
                tmpid = TemplateListView.Find("id>0 order by id desc").ID;
            }
            else
            {
                tmpid = TemplateListView.Find(string.Format("SysModel={0}",c.ModelID)).ID;
            }
            temp = TemplateListView.GetModelByID(tmpid.ToS());

            int pagecount = @int.GetPageCount(recordCount, temp.ShowRecordCount); //(Convert.ToDouble(recordCount) / Convert.ToDouble(temp.ShowRecordCount)).YueShu();

            string str_first = string.Format("[<a href=\"{0}\">首页</a>]", page > 1 ? "index" + BasePage.SystemSetting.ExtName : "javascript:void(0)");
            string str_pre = string.Format("[<a href=\"{0}\">上页</a>]", page > 1 ? "index" + (page == 2 ? "" : "_" + (page - 1).ToS()) + BasePage.SystemSetting.ExtName : "javascript:void(0)");
            string str_next = string.Format("[<a href=\"{0}\">下页</a>]", page < pagecount ? "index_" + (page + 1).ToS() + BasePage.SystemSetting.ExtName : "javascript:void(0)");
            string str_end = string.Format("[<a href=\"{0}\">尾页</a>]", page != pagecount ? "index_" + pagecount.ToS() + BasePage.SystemSetting.ExtName : "javascript:void(0)");
            return string.Format("{0} {1} {2} {3}", str_first, str_pre, str_next, str_end);
        }

        public static string BuildPagerLink(string key, int page)
        {
            int recordCount = BookView.Count(string.Format("Title like '%{0}%' or Author like '%{0}%' or Intro like '%{0}%'", key));
            int tmpid = 0;
            TemplateList temp = new TemplateList();

            temp = TemplateListView.GetModelByID("4");

            int pagecount = @int.GetPageCount(recordCount, 20); //(Convert.ToDouble(recordCount) / Convert.ToDouble(20)).YueShu();

            string str_first = string.Format("[<a href=\"{0}\">首页</a>]", page > 1 ? "/Search.aspx?m=4&key=" + key : "javascript:void(0)");
            string str_pre = string.Format("[<a href=\"{0}\">上页</a>]", page > 1 ? "/Search.aspx?m=4&key=" + key + "&p=" + (page - 1).ToS() : "javascript:void(0)");
            string str_next = string.Format("[<a href=\"{0}\">下页</a>]", page < pagecount ? "/Search.aspx?m=4&key=" + key + "&p=" + (page + 1).ToS() : "javascript:void(0)");
            string str_end = string.Format("[<a href=\"{0}\">尾页</a>]", page != pagecount ? "/Search.aspx?m=4&key=" + key + "&p=" + pagecount.ToS() : "javascript:void(0)");
            return string.Format("{0} {1} {2} {3}", str_first, str_pre, str_next, str_end);
        }

        #endregion

        #region 创建跳转下拉菜单
        /// <summary>
        /// 创建跳转下拉菜单
        /// </summary>
        /// <param name="c"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static string BuidPagerOption(Class c, int page)
        {
            int recordCount = c.CountSubItem();
            int tmpid = 0;
            TemplateList temp = new TemplateList();
            if (tmpid <= 0)
            {
                //没有选择模版
                tmpid = TemplateListView.Find("id>0 order by id desc").ID;
            }
            temp = TemplateListView.GetModelByID(tmpid.ToS());


            int pagecount = (Convert.ToDouble(recordCount) / Convert.ToDouble(temp.ShowRecordCount)).YueShu();


            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<select onchange='location.href=this.value'>");
            for (int i = 1; i <= pagecount; i++)
            {
                if (page == i)
                {
                    sb.AppendLine(string.Format("<option value='index{0}' selected>{1}</option>", (i > 1 ? "_" + i.ToS() : "") + BasePage.SystemSetting.ExtName, i.ToS()));
                }
                else
                {
                    sb.AppendLine(string.Format("<option value='index{0}'>{1}</option>", (i > 1 ? "_" + i.ToS() : "") + BasePage.SystemSetting.ExtName, i.ToS()));
                }
            }
            sb.AppendLine("</select>");
            return sb.ToS();
        }

        public static string BuidPagerOption(string key, int page)
        {
            int recordCount = BookView.Count(string.Format("Title like '%{0}%' or Author like '%{0}%' or Intro like '%{0}%'", key));
            int tmpid = 0;
            TemplateList temp = new TemplateList();

            temp = TemplateListView.GetModelByID("4");


            int pagecount = (Convert.ToDouble(recordCount) / Convert.ToDouble(20)).YueShu();


            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<select onchange='location.href=this.value'>");
            for (int i = 1; i <= pagecount; i++)
            {
                if (page == i)
                {
                    sb.AppendLine(string.Format("<option value='{0}' selected>{1}</option>", "/Search.aspx?m=4&key=" + key, i));
                }
                else
                {
                    sb.AppendLine(string.Format("<option value='{0}'>{1}</option>", "/Search.aspx?m=4&key=" + key + "&p=" + i, i));
                }
            }
            sb.AppendLine("</select>");
            return sb.ToS();
        }
        #endregion

        #region  创建导航条
        /// <summary>
        /// 创建类导航
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string BuildClassNavString(Class c)
        {
            string str = "";
            //if (c.IsLeafClass)
            //{
                str = string.Format("> <a href=\"{0}\">{1}</a>", BasePage.GetClassUrl(c), c.ClassName);
            //}
            //else
            //{
            //    str = string.Format("> <a href=\"{0}\">{1}</a>", "javascript:void(0);", c.ClassName);
            //}

            var cls = NewsAction.NewsClass.Where(p => p.ID == c.ParentID && c.ShowInNav).ToList();
            if (cls.Count > 0)
            {
                foreach (Class cl in cls)
                {

                    str = BuildClassNavString(cl) + str;
                }
            }
            //str = "<a href=\"/\">首页</a>" + str;
            return str;

        }
        #endregion


        #region 替换公共模版变量
        /// <summary>
        /// 替换公共模版变量
        /// </summary>
        /// <param name="TmpString">模版内容</param>
        /// <returns></returns>
        public static string ReplacePublicTemplate(string TmpString)
        {
            if (TmpString.IsNullOrEmpty())
            {
                return "";
            }
            Match mc_pubic = new Regex("\\[\\!--temp.(?<key>.*?)--\\]", RegexOptions.None).Match(TmpString);
            while (mc_pubic.Success)
            {
                TmpString = Regex.Replace(
                    TmpString,
                    string.Format("\\[\\!--temp\\.{0}--\\]", mc_pubic.Groups["key"].Value),
                    GetPublicTemplate(mc_pubic.Groups["key"].Value)
                    );
                mc_pubic = mc_pubic.NextMatch();
            }

            return TmpString;
        }
        #endregion

        #region 替换系统参数
        /// <summary>
        /// 替换系统参数
        /// </summary>
        /// <param name="TmpString"></param>
        /// <returns></returns>
        public static string ReplaceSystemSetting(string TmpString)
        {
            Match mc_sys = new Regex("\\[\\!--sys.(?<key>.*?)--\\]", RegexOptions.None).Match(TmpString);
            while (mc_sys.Success)
            {
                TmpString = Regex.Replace(
                    TmpString,
                    string.Format("\\[\\!--sys\\.{0}--\\]", mc_sys.Groups["key"].Value),
                    GetSysSettingContent(mc_sys.Groups["key"].Value)
                    );
                mc_sys = mc_sys.NextMatch();
            }

            return TmpString;
        }
        #endregion

        #region 替换标签
        /// <summary>
        /// 替换标签
        /// </summary>
        /// <param name="TmpString"></param>
        /// <returns></returns>
        public static string ReplaceTagContent(string TmpString)
        {
            //Match mc = new Regex("\\[[^\\]]\\][^\\]\\[]*\\[/[^\\[\\]]\\]", RegexOptions.None).Match(TmpString);
            Match mc = new Regex("\\[(?<key>.*?)\\](?<key2>.*?)\\[/(?<key3>.*?)\\]", RegexOptions.None).Match(TmpString);
            while (mc.Success)
            {
                //Match mc_tag = new Regex("\\[(?<key>.*?)\\](?<key2>.*?)\\[/(?<key3>.*?)\\]", RegexOptions.None).Match(mc.Groups[0].Value);

                //if (mc_tag.Success && mc_tag.Groups["key"].Value == mc_tag.Groups["key3"].Value)
                if (mc.Groups["key"].Value == mc.Groups["key3"].Value)
                {
                    TmpString = TmpString.Replace(
                        mc.Groups[0].Value,
                        GetTagContent(string.Format("[{0}]{1}[/{0}]", mc.Groups["key"].Value, mc.Groups["key2"].Value))
                        );

                }

                mc = mc.NextMatch();
            }

            return TmpString;

        }
        #endregion

        #region 替换页面属性 如页面标题等
        /// <summary> 
        /// 替换页面属性
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="pa"></param>
        /// <returns></returns>
        public static string ReplacePageAttribute(string Content, PageAttribute pa)
        {
            Content = Content.Replace("[!--page.title--]", pa.Title);
            Content = Content.Replace("[!--page.updatetime--]", pa.UpdateTime);
            Content = Content.Replace("[!--page.description--]", pa.Description);
            Content = Content.Replace("[!--page.keyword--]", pa.Keyword);
            return Content;
        }
        #endregion








        #region 获取模板内容字符串
        /// <summary>
        /// 获取模板内容字符串
        /// </summary>
        /// <param name="TempID">模板的ID</param>
        /// <param name="PageType">模板页面的类型</param>
        /// <returns></returns>
        public static string GetTempateString(int TempID, TempType PageType)
        {
            TemplatePublic tp = TemplatePublicView.GetModelByID(TempID.ToString());
            if (TempID <= 0)
            {
                tp = TemplatePublicView.Find(string.Format("GroupID={0}", DefaultGroup.ID.ToS()));
            }

            switch (PageType)
            {
                case TempType.JS调用登陆:
                    return tp.JSLogin;
                    break;
                case TempType.登陆状态:
                    return tp.LoginStatus;
                    break;
                case TempType.封面:
                    TemplateFace tf = TemplateFaceView.GetModelByID(TempID.ToS());
                    if (TempID <= 0)
                    {
                        tf = TemplateFaceView.Find(string.Format("GroupID={0}", DefaultGroup.ID.ToS()));
                    }
                    return tf.Content;
                    break;
                case TempType.高级搜索:
                    return tp.AdvancedSearch;
                    break;
                case TempType.横向搜索JS:
                    return tp.HorizontaSearch;
                    break;
                case TempType.控制面板:
                    return tp.Controlcontent;
                    break;
                case TempType.列表:
                    TemplateList tl = TemplateListView.GetModelByID(TempID.ToS());
                    if (TempID <= 0)
                    {
                        tl = TemplateListView.Find(string.Format("GroupID={0}", DefaultGroup.ID.ToS()));
                    }
                    return tl.Content;
                case TempType.列表分页:
                    return tp.ListPager;
                    break;
                case TempType.留言板:
                    return tp.MessageBoard;
                    break;
                case TempType.内容:
                    TemplateContent tc = TemplateContentView.GetModelByID(TempID.ToS());
                    if (TempID <= 0)
                    {
                        tc = TemplateContentView.Find(string.Format("GroupID={0}", DefaultGroup.ID.ToS()));
                    }
                    return tc.Content;
                    break;
                case TempType.评论js调用:
                    return tp.Reply;
                    break;
                case TempType.全站搜索:
                    return tp.SiteSearchContent;
                    break;
                case TempType.首页:
                    return tp.IndexContent;
                    break;
                case TempType.搜索:
                    TemplateSearch ts = TemplateSearchView.GetModelByID(TempID.ToS());
                    if (TempID <= 0)
                    {
                        ts = TemplateSearchView.Find(string.Format("GroupID={0}", DefaultGroup.ID.ToS()));
                    }
                    return ts.Content;
                    break;
                case TempType.下载地址:
                    return tp.DownAddress;
                    break;
                case TempType.相关信息:
                    return tp.RelationInfo;
                    break;
                case TempType.在线播放地址:
                    return tp.OLPlayaddress;
                    break;
                case TempType.纵向搜索JS:
                    return tp.VerticalSearch;
                    break;
                case TempType.最终下载页:
                    return tp.FinalDown;
                    break;
                case TempType.相册图片列表:
                    return tp.ImageList;
                    break;
                case TempType.问答回答列表:
                    return tp.AnswerList;
                    break;
                case TempType.小说章节列表:
                    return tp.ChapterList;
                    break;
                case TempType.小说章节:
                    return tp.BookChapter;
                    break;
                default:
                    return "";
                    break;
            }
        }
        #endregion

        #region 获取公共模板变量字符串
        /// <summary>
        /// 获取公共模板变量字符串
        /// </summary>
        /// <param name="VarName"></param>
        /// <returns></returns>
        public static string GetPublicTemplate(string VarName)
        {
            return TemplateVarView.Find(string.Format("VarName='{0}'", VarName)).Content.ToS();
        }
        #endregion

        #region 系统参数
        /// <summary>
        /// 系统参数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetSysSettingContent(string key)
        {
            SysSetting ss = BasePage.SystemSetting;
            switch (key.ToLower())
            {
                case "siteurl":
                    return ss.SiteUrl;
                    break;
                case "keywords":
                    return ss.KeyWords;
                    break;
                case "description":
                    return ss.Description;
                    break;
                case "copyright":
                    return ss.Copyright;
                    break;
                case "countcode":
                    return ss.CountCode;
                    break;
                case "filedirstring":
                    return ss.FileDirString;
                    break;
                case "extname":
                    return ss.ExtName;
                    break;
                case "siteclosemsg":
                    return ss.SiteCloseMsg;
                    break;
                case "sitename":
                    return ss.SiteName;
                    break;
                default:
                    return "";
            }
        }
        #endregion

        #region 替换标签内容
        /// <summary>
        /// 替换标签内容
        /// </summary>
        /// <param name="tag">标签</param>
        /// <returns>结果</returns>
        public static string GetTagContent(string tag)
        {
            //[方法]xx,xx,xx,xx[/方法]
            string functionName = "";
            object[] pars;

            #region  获取方法名
            Match mc_FunctionName = new Regex("\\[(?<key>.*?)\\]", RegexOptions.None).Match(tag);
            if (mc_FunctionName.Success)
            {
                functionName = mc_FunctionName.Groups["key"].Value;
            }
            Match mc_FunctionNameR = new Regex("\\[/(?<key>.*?)\\]", RegexOptions.None).Match(tag);
            if (mc_FunctionNameR.Success)
            {
                if (mc_FunctionName.Groups["key"].Value != mc_FunctionNameR.Groups["key"].Value)
                {
                    functionName = "ERR";
                }
                else
                {
                    functionName = mc_FunctionName.Groups["key"].Value;
                }
            }
            else
            {
                functionName = "ERR";
            }
            #endregion

            #region 获取参数
            Match mc_pars = new Regex("\\[.*\\](?<key>.*?)\\[/.*\\]", RegexOptions.None).Match(tag);
            if (mc_pars.Success)
            {
                pars = mc_pars.Groups["key"].Value.Split(',');
            }
            else
            {
                pars = new object[] { "" };
            }

            #endregion

            try
            {
                return ExecMethod("Voodoo.Basement.Functions", functionName, pars).ToS();
            }
            catch
            {
                return "标签使用错误";
            }

        }
        #endregion




        #region 执行某个方法
        /// <summary>
        /// 执行某个方法
        /// </summary>
        /// <param name="className">类，包括命名空间</param>
        /// <param name="methodName">方法名</param>
        /// <param name="objParas">参数</param>
        /// <returns></returns>
        protected static object ExecMethod(string className, string methodName, object[] objParas)
        {
            Type t = Type.GetType(className);
            /*实例化这个类*/
            ConstructorInfo constructor = t.GetConstructor(new Type[0]);//将得到的类型传给一个新建的构造器类型变量
            object obj = constructor.Invoke(new object[0]);//使用构造器对象来创建对象
            /*执行Insert方法*/
            MethodInfo m = t.GetMethod(methodName);
            return m.Invoke(obj, objParas);
        }
        #endregion

    }
}
