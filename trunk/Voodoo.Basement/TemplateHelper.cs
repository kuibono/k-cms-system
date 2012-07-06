using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Voodoo;
using Voodoo.Model;
using Voodoo.DAL;
using Voodoo.Security;
using Voodoo.IO;

using System.Text.RegularExpressions;
using System.Reflection;

namespace Voodoo.Basement
{
    public class TemplateHelper : System.Web.UI.Page
    {

        #region 首页
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public static string GetIndex()
        {
            SysSetting setting = BasePage.SystemSetting;

            string IndexFile = "~/" + setting.ClassFolder + "/index" + setting.ExtName;

            string Content = "";
            Content = GetTempateString(1, TempType.首页);

            //替换三层公共模版变量
            Content = ReplacePublicTemplate(Content);
            Content = ReplacePublicTemplate(Content);
            Content = ReplacePublicTemplate(Content);


            Content = ReplaceSystemSetting(Content);

            PageAttribute pa = new PageAttribute() { Title = "首页", UpdateTime = DateTime.Now.ToString(), Keyword = BasePage.SystemSetting.KeyWords, Description = BasePage.SystemSetting.Description };

            Content = ReplacePageAttribute(Content, pa);

            Content = ReplaceTagContent(Content);

            return Content;
        }
        #endregion

        #region 创建列表页面
        /// <summary>
        /// 创建列表页面
        /// </summary>
        /// <param name="c"></param>
        /// <param name="page"></param>
        public static string CreateListPage(Class c, int page)
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
            Content = ReplacePublicTemplate(Content);
            Content = ReplacePublicTemplate(Content);

            Content = ReplaceSystemSetting(Content);

            PageAttribute pa = new PageAttribute() { Title = c.ClassName, UpdateTime = DateTime.Now.ToString(), Description = c.ClassDescription, Keyword = c.ClassKeywords };

            Content = Content.Replace("[!--class.description--]", c.ClassDescription);
            Content = Content.Replace("[!--class.url--]", BasePage.GetClassUrl(c));
            Content = Content.Replace("[!--class.classname--]", c.ClassName);
            Content = Content.Replace("[!--class.id--]", c.ID.ToS());
            Content = ReplacePageAttribute(Content, pa);

            //此处要区分系统模型
            #region 替换列表

            #region 新闻系统模板
            if (c.ModelID == 1)//新闻系统模板
            {
                StringBuilder sb_list = new StringBuilder();
                List<News> ns = NewsView.GetModelList(string.Format("ClassID in(select {0} union select id from Class where ParentID={0})  and Audit=1 order by SetTop desc, id desc", c.ID)).ToList();

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
                    str_lst = str_lst.Replace("[!--titleurl--]", BasePage.GetNewsUrl(n, NewsView.GetNewsClass(n)));
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
                List<ImageAlbum> ns = ImageAlbumView.GetModelList(string.Format("ClassID in(select {0} union select id from Class where ParentID={0}) order by SetTop desc, id desc", c.ID)).ToList();
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
                List<Question> qs = QuestionView.GetModelList(string.Format("ClassID in(select {0} union select id from Class where ParentID={0}) order by id desc", c.ID));
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
                List<Book> qs = BookView.GetModelList(string.Format("Enable=1 and ClassID in(select id from Class where ID={0} union select id from Class where ParentID={0}) order by id desc", c.ID));
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

            #region 分类系统
            else if (c.ModelID == 5)
            {

            }
            #endregion

            #region 影视
            else if (c.ModelID == 6)
            {
                StringBuilder sb_list = new StringBuilder();
                List<MovieInfo> qs = MovieInfoView.GetModelList(string.Format("Enable=1 and ClassID in(select id from Class where ID={0} union select id from Class where ParentID={0}) order by id desc", c.ID));
                pagecount = (Convert.ToDouble(qs.Count) / Convert.ToDouble(temp.ShowRecordCount)).YueShu();
                recordCount = qs.Count;

                qs = qs.Skip((page - 1) * temp.ShowRecordCount).Take(temp.ShowRecordCount).ToList();
                foreach (MovieInfo m in qs)
                {
                    string str_lst = temp.ListVar;
                    str_lst = str_lst.Replace("[!--movie.url--]", BasePage.GetMovieUrl(m, MovieInfoView.GetClass(m)));
                    str_lst = str_lst.Replace("[!--movie.actors--]", m.Actors);
                    str_lst = str_lst.Replace("[!--movie.classid--]", m.ClassID.ToS());
                    str_lst = str_lst.Replace("[!--movie.classname--]", m.ClassName);
                    str_lst = str_lst.Replace("[!--movie.director--]", m.Director);
                    str_lst = str_lst.Replace("[!--movie.enable--]", m.Enable.ToInt32().ToS());
                    str_lst = str_lst.Replace("[!--movie.faceimage--]", m.FaceImage);
                    str_lst = str_lst.Replace("[!--movie.id--]", m.Id.ToS());
                    str_lst = str_lst.Replace("[!--movie.inserttime--]", m.InsertTime.ToString(temp.TimeFormat));
                    str_lst = str_lst.Replace("[!--movie.intro--]", m.Intro.TrimHTML());
                    str_lst = str_lst.Replace("[!--movie.ismove--]", m.IsMove.ToInt32().ToS());
                    str_lst = str_lst.Replace("[!--movie.lastdramatitle--]", m.LastDramaTitle);
                    str_lst = str_lst.Replace("[!--movie.location--]", m.Location);
                    str_lst = str_lst.Replace("[!--movie.publicyear--]", m.PublicYear);
                    str_lst = str_lst.Replace("[!--movie.status--]", m.Status.ToS());
                    str_lst = str_lst.Replace("[!--movie.tags--]", m.Tags);
                    str_lst = str_lst.Replace("[!--movie.title--]", m.Title);
                    str_lst = str_lst.Replace("[!--movie.updatetimetime--]", m.UpdateTime.ToString(temp.TimeFormat));
                    sb_list.AppendLine(str_lst);
                }
                Content = Content.Replace("<!--list.var-->", sb_list.ToString());
            }
            #endregion

            #endregion


            //替换标签变量
            Content = ReplaceTagContent(Content);

            #region 替换分页模板

            string tmp_pager = GetTempateString(1, TempType.列表分页);



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

            return Content;
        }
        #endregion

        #region  静态页面
        /// <summary>
        /// 静态页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetStatisPage(int id)
        {
            TemplatePage tp = TemplatePageView.GetModelByID(id.ToS());
            string Content = tp.Content;
            //替换三层公共模版变量
            Content = ReplacePublicTemplate(Content);
            Content = ReplacePublicTemplate(Content);
            Content = ReplacePublicTemplate(Content);

            Content = ReplaceSystemSetting(Content);

            Content = ReplaceTagContent(Content);
            return Content;
        }
        #endregion

        #region  生成内容页--影视
        /// <summary>
        /// 生成内容页--影视
        /// </summary>
        /// <param name="album"></param>
        /// <param name="cls"></param>
        public static string CreateContentPage(MovieInfo movie, Class cls)
        {
            string Content = "";

            TemplateContent temp = TemplateContentView.Find(string.Format("SysModel={0}", cls.ModelID));
            int tmpid = temp.ID;



            Content = GetTempateString(tmpid, TempType.内容);
            Content = ReplacePublicTemplate(Content);
            Content = ReplacePublicTemplate(Content);

            Content = ReplacePublicTemplate(Content);

            Content = ReplaceSystemSetting(Content);

            PageAttribute pa = new PageAttribute() { Title = movie.Title, UpdateTime = DateTime.Now.ToString(), Description = movie.Intro.TrimHTML() };

            Content = ReplacePageAttribute(Content, pa);

            //MovieUrlKuaib firstKuaibo = MovieUrlKuaibView.Find(string.Format("MovieID={0} order by id asc", movie.Id));
            //MovieUrlKuaib nextKuaibo = BasePage.GetNextKuaibo(firstKuaibo);
            //if (nextKuaibo == null)
            //{
            //    nextKuaibo = firstKuaibo.Clone();
            //    nextKuaibo.Url = "";
            //}

            #region 替换内容

            Content = Content.Replace("[!--class.id--]", cls.ID.ToString());
            Content = Content.Replace("[!--class.name--]", cls.ClassName);
            Content = Content.Replace("[!--class.url--]", BasePage.GetClassUrl(cls));
            //if (nextKuaibo.Id > 0)
            //{
            //    Content = Content.Replace("[!--movie.nextpageurl--]", BasePage.GetMovieDramaUrl(nextKuaibo, MovieInfoView.GetClass(nextKuaibo)));
            //}

            //if (firstKuaibo != null)
            //{
            //    Content = Content.Replace("[!--drama.title--]", firstKuaibo.Title);
            //    Content = Content.Replace("[!--drama.url--]", firstKuaibo.Url);
            //    Content = Content.Replace("[!--drama.updatetime--]", firstKuaibo.UpdateTime.ToString(temp.TimeFormat));
            //}

            Content = Content.Replace("[!--movie.url--]", BasePage.GetMovieUrl(movie, MovieInfoView.GetClass(movie)));
            Content = Content.Replace("[!--movie.actors--]", movie.Actors);
            Content = Content.Replace("[!--movie.classid--]", movie.ClassID.ToS());
            Content = Content.Replace("[!--movie.classname--]", movie.ClassName);
            Content = Content.Replace("[!--movie.director--]", movie.Director);
            Content = Content.Replace("[!--movie.enable--]", movie.Enable.ToInt32().ToS());
            Content = Content.Replace("[!--movie.faceimage--]", movie.FaceImage);
            Content = Content.Replace("[!--movie.id--]", movie.Id.ToS());
            Content = Content.Replace("[!--movie.inserttime--]", movie.InsertTime.ToString(temp.TimeFormat));
            Content = Content.Replace("[!--movie.intro--]", movie.Intro);
            Content = Content.Replace("[!--movie.info--]", movie.Info);
            Content = Content.Replace("[!--movie.description--]", movie.Intro.TrimHTML());
            Content = Content.Replace("[!--movie.ismove--]", movie.IsMove.ToInt32().ToS());
            Content = Content.Replace("[!--movie.lastdramatitle--]", movie.LastDramaTitle);
            Content = Content.Replace("[!--movie.location--]", movie.Location);
            Content = Content.Replace("[!--movie.publicyear--]", movie.PublicYear);
            Content = Content.Replace("[!--movie.status--]", movie.Status.ToS());
            Content = Content.Replace("[!--movie.tags--]", movie.Tags);
            Content = Content.Replace("[!--movie.title--]", movie.Title);
            Content = Content.Replace("[!--movie.updatetimetime--]", movie.UpdateTime.ToString(temp.TimeFormat));

            StringBuilder sb = new StringBuilder();
            List<MovieUrlKuaib> qb = MovieUrlKuaibView.GetModelList(string.Format("MovieID={0}", movie.Id.ToS()));
            string list_tmp = GetTempateString(1, TempType.下载地址);
            foreach (MovieUrlKuaib q in qb)
            {
                string row = list_tmp.Replace("[!--url.id--]", q.Id.ToS());
                row = row.Replace("[!--url.url--]", q.Url);
                row = row.Replace("[!--url.title--]", q.Title);
                row = row.Replace("[!--url.movieid--]", q.MovieID.ToS());
                row = row.Replace("[!--url.movietitle--]", q.MovieTitle);
                row = row.Replace("[!--url--]", BasePage.GetMovieDramaUrl(q, MovieInfoView.GetClass(q)));
                sb.Append(row);
            }
            Content = Content.Replace("[!--movie.kuib--]", sb.ToS());


            sb = new StringBuilder();
            List<MovieUrlBaidu> baidu = MovieUrlBaiduView.GetModelList(string.Format("MovieID={0}", movie.Id.ToS()));
            foreach (MovieUrlBaidu q in baidu)
            {
                string row = list_tmp.Replace("[!--url.id--]", q.Id.ToS());
                row = row.Replace("[!--url.url--]", q.Url);
                row = row.Replace("[!--url.title--]", q.Title);
                row = row.Replace("[!--url.movieid--]", q.MovieID.ToS());
                row = row.Replace("[!--url.movietitle--]", q.MovieTitle);
                row = row.Replace("[!--url--]", BasePage.GetMovieDramaUrl(q, MovieInfoView.GetClass(q)));
                sb.Append(row);
            }
            Content = Content.Replace("[!--movie.baidu--]", sb.ToS());

            sb = new StringBuilder();
            List<MovieUrlMag> mag = MovieUrlMagView.GetModelList(string.Format("MovieID={0}", movie.Id.ToS()));
            foreach (MovieUrlMag q in mag)
            {
                string row = list_tmp.Replace("[!--url.id--]", q.Id.ToS());
                row = row.Replace("[!--url.url--]", q.Url);
                row = row.Replace("[!--url.title--]", q.Title);
                row = row.Replace("[!--url.movieid--]", q.MovieID.ToS());
                row = row.Replace("[!--url.movietitle--]", q.MovieTitle);
                sb.Append(row);
            }
            Content = Content.Replace("[!--movie.mag--]", sb.ToS());


            #endregion

            Content = ReplaceTagContent(Content);

            //替换导航条
            Content = Content.Replace("[!--newsnav--]", BuildClassNavString(cls));

            return Content;
        }
        #endregion

        #region 生成播放页面--快播
        /// <summary>
        /// 生成播放页面--快播
        /// </summary>
        /// <param name="kuaib">快播地址</param>
        /// <param name="cls">分类</param>
        public static string CreateDramapage(MovieUrlKuaib kuaib, Class cls)
        {
            MovieInfo movie = MovieInfoView.GetModelByID(kuaib.MovieID.ToS());

            string FileName = BasePage.GetMovieDramaUrl(kuaib, cls);
            string Content = "";

            TemplateContent temp = TemplateContentView.Find(string.Format("SysModel={0}", cls.ModelID));
            int tmpid = temp.ID;



            Content = GetTempateString(1, TempType.快播页面);

            Content = ReplacePublicTemplate(Content);
            Content = ReplacePublicTemplate(Content);
            Content = ReplacePublicTemplate(Content);

            Content = ReplaceSystemSetting(Content);

            PageAttribute pa = new PageAttribute() { Title = movie.Title + "-" + kuaib.Title + "快播在线播放", UpdateTime = DateTime.Now.ToString(), Description = string.Format("电影《{0}》{1} 快播在线播放页面,《{0}》{1}高清版下载。", movie.Title, kuaib.Title) };

            Content = ReplacePageAttribute(Content, pa);



            #region 替换内容

            MovieUrlKuaib next = BasePage.GetNextKuaibo(kuaib);
            if (next == null)
            {
                next = kuaib.Clone();
                next.Url = "";
            }

            Content = Content.Replace("[!--class.id--]", cls.ID.ToString());
            Content = Content.Replace("[!--class.name--]", cls.ClassName);
            Content = Content.Replace("[!--class.url--]", BasePage.GetClassUrl(cls));

            Content = Content.Replace("[!--movie.url--]", BasePage.GetMovieUrl(movie, MovieInfoView.GetClass(movie)));
            Content = Content.Replace("[!--movie.nextpageurl--]", BasePage.SystemSetting.SiteUrl + BasePage.GetMovieDramaUrl(next, MovieInfoView.GetClass(next)));

            Content = Content.Replace("[!--drama.title--]", kuaib.Title);
            Content = Content.Replace("[!--drama.url--]", kuaib.Url);
            Content = Content.Replace("[!--drama.updatetime--]", kuaib.UpdateTime.ToString(temp.TimeFormat));

            Content = Content.Replace("[!--movie.actors--]", movie.Actors);
            Content = Content.Replace("[!--movie.classid--]", movie.ClassID.ToS());
            Content = Content.Replace("[!--movie.classname--]", movie.ClassName);
            Content = Content.Replace("[!--movie.director--]", movie.Director);
            Content = Content.Replace("[!--movie.enable--]", movie.Enable.ToInt32().ToS());
            Content = Content.Replace("[!--movie.faceimage--]", movie.FaceImage);
            Content = Content.Replace("[!--movie.id--]", movie.Id.ToS());
            Content = Content.Replace("[!--movie.inserttime--]", movie.InsertTime.ToString(temp.TimeFormat));
            Content = Content.Replace("[!--movie.intro--]", movie.Intro);
            Content = Content.Replace("[!--movie.info--]", movie.Info);
            Content = Content.Replace("[!--movie.ismove--]", movie.IsMove.ToInt32().ToS());
            Content = Content.Replace("[!--movie.lastdramatitle--]", movie.LastDramaTitle);
            Content = Content.Replace("[!--movie.location--]", movie.Location);
            Content = Content.Replace("[!--movie.publicyear--]", movie.PublicYear);
            Content = Content.Replace("[!--movie.status--]", movie.Status.ToS());
            Content = Content.Replace("[!--movie.tags--]", movie.Tags);
            Content = Content.Replace("[!--movie.title--]", movie.Title);
            Content = Content.Replace("[!--movie.updatetimetime--]", movie.UpdateTime.ToString(temp.TimeFormat));


            StringBuilder sb = new StringBuilder();
            List<MovieUrlKuaib> qb = MovieUrlKuaibView.GetModelList(string.Format("MovieID={0}", movie.Id.ToS()));
            string list_tmp = GetTempateString(1, TempType.下载地址);
            foreach (MovieUrlKuaib q in qb)
            {
                string row = list_tmp.Replace("[!--url.id--]", q.Id.ToS());
                row = row.Replace("[!--url.url--]", q.Url);
                row = row.Replace("[!--url.title--]", q.Title);
                row = row.Replace("[!--url.movieid--]", q.MovieID.ToS());
                row = row.Replace("[!--url.movietitle--]", q.MovieTitle);
                row = row.Replace("[!--url--]", BasePage.GetMovieDramaUrl(q, MovieInfoView.GetClass(q)));
                sb.Append(row);
            }
            Content = Content.Replace("[!--movie.kuib--]", sb.ToS());


            sb = new StringBuilder();
            List<MovieUrlBaidu> baidu = MovieUrlBaiduView.GetModelList(string.Format("MovieID={0}", movie.Id.ToS()));
            foreach (MovieUrlBaidu q in baidu)
            {
                string row = list_tmp.Replace("[!--url.id--]", q.Id.ToS());
                row = row.Replace("[!--url.url--]", q.Url);
                row = row.Replace("[!--url.title--]", q.Title);
                row = row.Replace("[!--url.movieid--]", q.MovieID.ToS());
                row = row.Replace("[!--url.movietitle--]", q.MovieTitle);
                row = row.Replace("[!--url--]", BasePage.GetMovieDramaUrl(q, MovieInfoView.GetClass(q)));
                sb.Append(row);
            }
            Content = Content.Replace("[!--movie.baidu--]", sb.ToS());

            sb = new StringBuilder();
            List<MovieUrlMag> mag = MovieUrlMagView.GetModelList(string.Format("MovieID={0}", movie.Id.ToS()));
            foreach (MovieUrlMag q in mag)
            {
                string row = list_tmp.Replace("[!--url.id--]", q.Id.ToS());
                row = row.Replace("[!--url.url--]", q.Url);
                row = row.Replace("[!--url.title--]", q.Title);
                row = row.Replace("[!--url.movieid--]", q.MovieID.ToS());
                row = row.Replace("[!--url.movietitle--]", q.MovieTitle);
                sb.Append(row);
            }
            Content = Content.Replace("[!--movie.mag--]", sb.ToS());


            #endregion

            Content = ReplaceTagContent(Content);

            //替换导航条
            Content = Content.Replace("[!--newsnav--]", BuildClassNavString(cls));

            return Content;
        }
        #endregion 生成播放页面--快播

        #region 生成播放页面--百度
        /// <summary>
        /// 生成播放页面--快播
        /// </summary>
        /// <param name="kuaib">百度影音地址</param>
        /// <param name="cls">分类</param>
        public static string CreateDramapage(MovieUrlBaidu kuaib, Class cls)
        {
            MovieInfo movie = MovieInfoView.GetModelByID(kuaib.MovieID.ToS());

            string FileName = BasePage.GetMovieDramaUrl(kuaib, cls);
            string Content = "";

            TemplateContent temp = TemplateContentView.Find(string.Format("SysModel={0}", cls.ModelID));
            int tmpid = temp.ID;



            Content = GetTempateString(1, TempType.百度影音页面);

            Content = ReplacePublicTemplate(Content);
            Content = ReplacePublicTemplate(Content);
            Content = ReplacePublicTemplate(Content);

            Content = ReplaceSystemSetting(Content);

            PageAttribute pa = new PageAttribute() { Title = movie.Title + "-" + kuaib.Title + "快播在线播放", UpdateTime = DateTime.Now.ToString(), Description = string.Format("电影《{0}》{1} 快播在线播放页面,《{0}》{1}高清版下载。", movie.Title, kuaib.Title) };

            Content = ReplacePageAttribute(Content, pa);



            #region 替换内容

            MovieUrlBaidu next = BasePage.GetNextBaidu(kuaib);
            if (next == null)
            {
                next = kuaib.Clone();
                next.Url = "";
            }

            Content = Content.Replace("[!--class.id--]", cls.ID.ToString());
            Content = Content.Replace("[!--class.name--]", cls.ClassName);
            Content = Content.Replace("[!--class.url--]", BasePage.GetClassUrl(cls));

            Content = Content.Replace("[!--movie.url--]", BasePage.GetMovieUrl(movie, MovieInfoView.GetClass(movie)));
            Content = Content.Replace("[!--movie.nextpageurl--]",
                ReplaceAll(
                BasePage.SystemSetting.SiteUrl + BasePage.GetMovieDramaUrl(next, MovieInfoView.GetClass(next)),
                "[\\u4e00-\\u9fa5]",
                "1")
                );

            Content = Content.Replace("[!--drama.title--]", kuaib.Title);
            Content = Content.Replace("[!--drama.url--]", kuaib.Url);
            Content = Content.Replace("[!--drama.updatetime--]", kuaib.UpdateTime.ToString(temp.TimeFormat));

            Content = Content.Replace("[!--movie.actors--]", movie.Actors);
            Content = Content.Replace("[!--movie.classid--]", movie.ClassID.ToS());
            Content = Content.Replace("[!--movie.classname--]", movie.ClassName);
            Content = Content.Replace("[!--movie.director--]", movie.Director);
            Content = Content.Replace("[!--movie.enable--]", movie.Enable.ToInt32().ToS());
            Content = Content.Replace("[!--movie.faceimage--]", movie.FaceImage);
            Content = Content.Replace("[!--movie.id--]", movie.Id.ToS());
            Content = Content.Replace("[!--movie.inserttime--]", movie.InsertTime.ToString(temp.TimeFormat));
            Content = Content.Replace("[!--movie.intro--]", movie.Intro);
            Content = Content.Replace("[!--movie.info--]", movie.Info);
            Content = Content.Replace("[!--movie.ismove--]", movie.IsMove.ToInt32().ToS());
            Content = Content.Replace("[!--movie.lastdramatitle--]", movie.LastDramaTitle);
            Content = Content.Replace("[!--movie.location--]", movie.Location);
            Content = Content.Replace("[!--movie.publicyear--]", movie.PublicYear);
            Content = Content.Replace("[!--movie.status--]", movie.Status.ToS());
            Content = Content.Replace("[!--movie.tags--]", movie.Tags);
            Content = Content.Replace("[!--movie.title--]", movie.Title);
            Content = Content.Replace("[!--movie.updatetimetime--]", movie.UpdateTime.ToString(temp.TimeFormat));


            StringBuilder sb = new StringBuilder();
            List<MovieUrlKuaib> qb = MovieUrlKuaibView.GetModelList(string.Format("MovieID={0}", movie.Id.ToS()));
            string list_tmp = GetTempateString(1, TempType.下载地址);
            foreach (MovieUrlKuaib q in qb)
            {
                string row = list_tmp.Replace("[!--url.id--]", q.Id.ToS());
                row = row.Replace("[!--url.url--]", q.Url);
                row = row.Replace("[!--url.title--]", q.Title);
                row = row.Replace("[!--url.movieid--]", q.MovieID.ToS());
                row = row.Replace("[!--url.movietitle--]", q.MovieTitle);
                row = row.Replace("[!--url--]", BasePage.GetMovieDramaUrl(q, MovieInfoView.GetClass(q)));
                sb.Append(row);
            }
            Content = Content.Replace("[!--movie.kuib--]", sb.ToS());


            sb = new StringBuilder();
            List<MovieUrlBaidu> baidu = MovieUrlBaiduView.GetModelList(string.Format("MovieID={0}", movie.Id.ToS()));
            foreach (MovieUrlBaidu q in baidu)
            {
                string row = list_tmp.Replace("[!--url.id--]", q.Id.ToS());
                row = row.Replace("[!--url.url--]", q.Url);
                row = row.Replace("[!--url.title--]", q.Title);
                row = row.Replace("[!--url.movieid--]", q.MovieID.ToS());
                row = row.Replace("[!--url.movietitle--]", q.MovieTitle);
                row = row.Replace("[!--url--]", BasePage.GetMovieDramaUrl(q, MovieInfoView.GetClass(q)));
                sb.Append(row);
            }
            Content = Content.Replace("[!--movie.baidu--]", sb.ToS());

            sb = new StringBuilder();
            List<MovieUrlMag> mag = MovieUrlMagView.GetModelList(string.Format("MovieID={0}", movie.Id.ToS()));
            foreach (MovieUrlMag q in mag)
            {
                string row = list_tmp.Replace("[!--url.id--]", q.Id.ToS());
                row = row.Replace("[!--url.url--]", q.Url);
                row = row.Replace("[!--url.title--]", q.Title);
                row = row.Replace("[!--url.movieid--]", q.MovieID.ToS());
                row = row.Replace("[!--url.movietitle--]", q.MovieTitle);
                sb.Append(row);
            }
            Content = Content.Replace("[!--movie.mag--]", sb.ToS());


            #endregion

            Content = ReplaceTagContent(Content);

            //替换导航条
            Content = Content.Replace("[!--newsnav--]", BuildClassNavString(cls));

            return Content;
        }
        #endregion

        #region 生成播放页面--单集列表页面
        /// <summary>
        /// 生成播放页面--单集列表页面
        /// </summary>
        /// <param name="kuaib">百度影音地址</param>
        /// <param name="cls">分类</param>
        public static string CreateDramapage(MovieDrama drama, Class cls)
        {
            MovieInfo movie = MovieInfoView.GetModelByID(drama.MovieID.ToS());

            string FileName = BasePage.GetMovieDramaUrl(drama, cls);
            string Content = "";

            TemplateContent temp = TemplateContentView.Find(string.Format("SysModel={0}", cls.ModelID));
            int tmpid = temp.ID;



            Content = GetTempateString(1, TempType.单集列表页面);

            Content = ReplacePublicTemplate(Content);
            Content = ReplacePublicTemplate(Content);
            Content = ReplacePublicTemplate(Content);

            Content = ReplaceSystemSetting(Content);

            PageAttribute pa = new PageAttribute() { Title = movie.Title + "-" + drama.Title + "快播在线播放", UpdateTime = DateTime.Now.ToString(), Description = string.Format("电影《{0}》{1} 快播在线播放页面,《{0}》{1}高清版下载。", movie.Title, drama.Title) };

            Content = ReplacePageAttribute(Content, pa);



            #region 替换内容

            MovieDrama next = BasePage.GetNextDrama(drama);
            if (next == null)
            {
                next = drama.Clone();
            }

            Content = Content.Replace("[!--class.id--]", cls.ID.ToString());
            Content = Content.Replace("[!--class.name--]", cls.ClassName);
            Content = Content.Replace("[!--class.url--]", BasePage.GetClassUrl(cls));

            Content = Content.Replace("[!--movie.url--]", BasePage.GetMovieUrl(movie, MovieInfoView.GetClass(movie)));
            Content = Content.Replace("[!--movie.nextpageurl--]", BasePage.GetMovieDramaUrl(next, MovieInfoView.GetClass(movie)));

            Content = Content.Replace("[!--drama.title--]", drama.Title);
            Content = Content.Replace("[!--drama.id--]", drama.Id.ToS());
            Content = Content.Replace("[!--drama.updatetime--]", drama.UpdateTime.ToString(temp.TimeFormat));

            Content = Content.Replace("[!--movie.actors--]", movie.Actors);
            Content = Content.Replace("[!--movie.classid--]", movie.ClassID.ToS());
            Content = Content.Replace("[!--movie.classname--]", movie.ClassName);
            Content = Content.Replace("[!--movie.director--]", movie.Director);
            Content = Content.Replace("[!--movie.enable--]", movie.Enable.ToInt32().ToS());
            Content = Content.Replace("[!--movie.faceimage--]", movie.FaceImage);
            Content = Content.Replace("[!--movie.id--]", movie.Id.ToS());
            Content = Content.Replace("[!--movie.inserttime--]", movie.InsertTime.ToString(temp.TimeFormat));
            Content = Content.Replace("[!--movie.intro--]", movie.Intro);
            Content = Content.Replace("[!--movie.info--]", movie.Info);
            Content = Content.Replace("[!--movie.ismove--]", movie.IsMove.ToInt32().ToS());
            Content = Content.Replace("[!--movie.lastdramatitle--]", movie.LastDramaTitle);
            Content = Content.Replace("[!--movie.location--]", movie.Location);
            Content = Content.Replace("[!--movie.publicyear--]", movie.PublicYear);
            Content = Content.Replace("[!--movie.status--]", movie.Status.ToS());
            Content = Content.Replace("[!--movie.tags--]", movie.Tags);
            Content = Content.Replace("[!--movie.title--]", movie.Title);
            Content = Content.Replace("[!--movie.updatetimetime--]", movie.UpdateTime.ToString(temp.TimeFormat));



            #endregion

            Content = ReplaceTagContent(Content);

            //替换导航条
            Content = Content.Replace("[!--newsnav--]", BuildClassNavString(cls));

            return Content;
        }
        #endregion





        #region 伪原创字符替换--汉字变成拼音
        /// <summary>
        /// 伪原创字符替换--汉字变成拼音
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static string ReplaceContentKey(string Content)
        {
            Content = Content.Replace("垩", "");
            Content = Content.Replace("声音", "shengyin");
            Content = Content.Replace("地方", "difang");
            Content = Content.Replace("这个", "zhege");
            Content = Content.Replace("有些", "youxie");
            Content = Content.Replace("注意", "zhuyi");
            Content = Content.Replace("非常", "feichang");
            Content = Content.Replace("大家", "dajia");
            Content = Content.Replace("衣服", "yifu");
            Content = Content.Replace("自己", "ziji");
            Content = Content.Replace("来不及", "laibuji");
            Content = Content.Replace("意思", "yisi");
            Content = Content.Replace("能力", "nengli");
            Content = Content.Replace("容易", "rongyi");
            Content = Content.Replace("朋友", "pengyou");
            Content = Content.Replace("消息", "xiaoxi");
            Content = Content.Replace("这样", "zheyang");
            Content = Content.Replace("如此", "ruci");
            Content = Content.Replace("说话", "shuohua");
            Content = Content.Replace("：“", "maohaoyinhao");
            Content = Content.Replace("明天", "mingtian");
            Content = Content.Replace("你们", "nimen");
            Content = Content.Replace("没有", "meiyou");
            Content = Content.Replace("激动", "jidong");
            Content = Content.Replace("可以", "keyi");
            Content = Content.Replace("一起", "yiqi");
            Content = Content.Replace("小姐", "xiaojie");
            Content = Content.Replace("不但", "budan");
            Content = Content.Replace("而且", "erqie");
            Content = Content.Replace("其他", "qita");
            Content = Content.Replace("事情", "shiqing");
            Content = Content.Replace("一会", "yihui");
            Content = Content.Replace("一样", "yiyang");
            Content = Content.Replace("儿子", "erzi");
            Content = Content.Replace("应该", "yinggai");
            Content = Content.Replace("时候", "shihou");
            Content = Content.Replace("以前", "yiqian");
            Content = Content.Replace("不是", "bushi");
            Content = Content.Replace("什么", "shenme");
            Content = Content.Replace("怎么", "zenme");
            Content = Content.Replace("成功", "chenggong");
            Content = Content.Replace("如何", "ruhe");
            Content = Content.Replace("过去", "guoqu");
            Content = Content.Replace("出来", "chulai");

            return Content;
        }
        #endregion

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
            小说章节,
            快播页面,
            百度影音页面,
            单集列表页面
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
                case TempType.快播页面:
                    return tp.KuaiboPage;
                    break;
                case TempType.百度影音页面:
                    return tp.BaiduPage;
                    break;
                case TempType.单集列表页面:
                    return tp.SingleDrama;
                    break;
                default:
                    return "";
                    break;
            }
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
                tmpid = TemplateListView.Find(string.Format("SysModel={0}", c.ModelID)).ID;
            }
            temp = TemplateListView.GetModelByID(tmpid.ToS());

            int pagecount = @int.GetPageCount(recordCount, temp.ShowRecordCount); //(Convert.ToDouble(recordCount) / Convert.ToDouble(temp.ShowRecordCount)).YueShu();

            string str_first = string.Format("<a href=\"{0}\">首页</a>", page > 1 ? "index" + BasePage.SystemSetting.ExtName : "javascript:void(0)");
            string str_pre = string.Format("<a href=\"{0}\">上页</a>", page > 1 ? "index" + (page == 2 ? "" : "_" + (page - 1).ToS()) + BasePage.SystemSetting.ExtName : "javascript:void(0)");
            string str_next = string.Format("<a href=\"{0}\">下页</a>", page < pagecount ? "index_" + (page + 1).ToS() + BasePage.SystemSetting.ExtName : "javascript:void(0)");
            string str_end = string.Format("<a href=\"{0}\">尾页</a>", page != pagecount ? "index_" + pagecount.ToS() + BasePage.SystemSetting.ExtName : "javascript:void(0)");
            return string.Format("{0} {1} {2} {3}", str_first, str_pre, str_next, str_end);
        }

        public static string BuildPagerLink(string key, int page)
        {
            int recordCount = BookView.Count(string.Format("Title like '%{0}%' or Author like '%{0}%' or Intro like '%{0}%'", key));
            int tmpid = 0;
            TemplateList temp = new TemplateList();

            temp = TemplateListView.GetModelByID("4");

            int pagecount = @int.GetPageCount(recordCount, 20); //(Convert.ToDouble(recordCount) / Convert.ToDouble(20)).YueShu();

            string str_first = string.Format("<a href=\"{0}\">首页</a>", page > 1 ? "/Search.aspx?m=4&key=" + key : "javascript:void(0)");
            string str_pre = string.Format("<a href=\"{0}\">上页</a>", page > 1 ? "/Search.aspx?m=4&key=" + key + "&p=" + (page - 1).ToS() : "javascript:void(0)");
            string str_next = string.Format("<a href=\"{0}\">下页</a>", page < pagecount ? "/Search.aspx?m=4&key=" + key + "&p=" + (page + 1).ToS() : "javascript:void(0)");
            string str_end = string.Format("<a href=\"{0}\">尾页</a>", page != pagecount ? "/Search.aspx?m=4&key=" + key + "&p=" + pagecount.ToS() : "javascript:void(0)");
            return string.Format("{0} {1} {2} {3}", str_first, str_pre, str_next, str_end);
        }

        public static string BuildPagerLink(string TemplateString, int page, int pagecount)
        {
            //<a href="xx.aspx?p={first}">首页</a> <a href="xxx.aspx?p={pre}">上页</a> <a href="xxx.aspx?p={next}">下页</a> <a href="xxx.aspx?p={end}">尾页</a>

            string result = TemplateString;
            result = result.Replace("{first}", "1");
            result = result.Replace("{pre}", (page > 1 ? page - 1 : 1).ToS());
            result = result.Replace("{next}", (page < pagecount ? page + 1 : page).ToS());
            result = result.Replace("{end}", pagecount.ToS());
            return result;
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


        #region 全部替换
        /// <summary>
        /// 全部替换
        /// </summary>
        /// <param name="str"></param>
        /// <param name="parrten"></param>
        /// <param name="newstr"></param>
        /// <returns></returns>
        protected static string ReplaceAll(string str, string parrten, string newstr)
        {
            while (Regex.IsMatch(str, parrten))
            {
                str = Regex.Replace(str, parrten, newstr, RegexOptions.IgnoreCase);
            }
            return str;
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
