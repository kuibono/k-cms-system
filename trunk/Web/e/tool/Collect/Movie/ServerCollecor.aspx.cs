﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Voodoo;
using Voodoo.Cache;
using Voodoo.Model;
using Voodoo.DAL;
using Voodoo.Net;
using Voodoo.Basement.Collect;
using Voodoo.Basement;

using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Web.e.tool.Collect.Movie
{
    public partial class ServerCollecor : System.Web.UI.Page
    {
        #region 所有规则
        protected List<MovieRule> Rules
        {
            get
            {
                if (Voodoo.Cache.Cache.GetCache("movierules") == null)
                {
                    List<MovieRule> rules = new List<MovieRule>();
                    FileInfo[] files = new DirectoryInfo(Server.MapPath("~/Config/MovieRule")).GetFiles();
                    foreach (var file in files)
                    {
                        try
                        {
                            MovieRule r = (MovieRule)Voodoo.IO.XML.DeSerialize(typeof(MovieRule), Voodoo.IO.File.Read(file.FullName));
                            rules.Add(r);
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message);
                        }
                    }
                    Voodoo.Cache.Cache.SetCache("movierules", rules);
                }

                return (List<MovieRule>)Voodoo.Cache.Cache.GetCache("movierules");
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            string action = WS.RequestString("a");
            switch (action)
            {
                case "getrules":
                    GetRules();
                    break;
                case "getlistmovies":
                    GetListMovie(WS.RequestString("rulename"), WS.RequestString("listurl"));
                    break;
                case "getmovieinfo":
                    string rulename = WS.RequestString("rulename");
                    string url = WS.RequestString("url");
                    string cls = WS.RequestString("cls");
                    string title = WS.RequestString("title");
                    string director = WS.RequestString("director");
                    string actors = WS.RequestString("actors");
                    string tags = WS.RequestString("tags");
                    string location = WS.RequestString("location");
                    string publicyear = WS.RequestString("publicyear");
                    string intro = WS.RequestString("intro");
                    string image = WS.RequestString("image");

                    GetMovieInfo(rulename, url, cls, title, director, actors, tags, location, publicyear, intro, image);
                    break;
                case "getbaidudrama":
                    rulename = WS.RequestString("rulename");
                    url = WS.RequestString("url");
                    title = WS.RequestString("title");
                    int movieid = WS.RequestInt("movieid");
                    string movietitle = WS.RequestString("movietitle");
                    GetBaiduDrama(rulename, url, title, movieid, movietitle);
                    break;
                case "getkuaibodrama":
                    rulename = WS.RequestString("rulename");
                    url = WS.RequestString("url");
                    title = WS.RequestString("title");
                    movieid = WS.RequestInt("movieid");
                    movietitle = WS.RequestString("movietitle");
                    GetKuaiboDrama(rulename, url, title, movieid, movietitle);
                    break;
                case "getbaidusourceurl":
                    rulename = WS.RequestString("rulename");
                    url = WS.RequestString("url");
                    title = WS.RequestString("title");
                    movieid = WS.RequestInt("movieid");
                    movietitle = WS.RequestString("movietitle");
                    GetBaiduSourceUrl(rulename, url, title, movieid, movietitle);
                    break;
                case "getkuaibosourceurl":
                    rulename = WS.RequestString("rulename");
                    url = WS.RequestString("url");
                    title = WS.RequestString("title");
                    movieid = WS.RequestInt("movieid");
                    movietitle = WS.RequestString("movietitle");
                    GetKuaiboSourceUrl(rulename, url, title, movieid, movietitle);
                    break;
            }
        }

        #region 获取所有规则
        /// <summary>
        /// 获取所有规则
        /// </summary>
        protected void GetRules()
        {
            Response.Clear();

            Response.Write(
            Newtonsoft.Json.JsonConvert.SerializeObject(Rules)

                );
        }
        #endregion

        #region 获取列表的内容信息
        /// <summary>
        /// 获取列表的内容信息
        /// </summary>
        /// <param name="RuleName">规则名称</param>
        /// <param name="ListUrl">列表页面</param>
        protected void GetListMovie(string RuleName, string ListUrl)
        {
            MovieRule _r = Rules.Where(p => p.Name == RuleName).First();
            MovieRule r = _r.Clone();
            r.ListPageUrl = ListUrl;

            List<MovieInfo> result = new List<MovieInfo>();
            string listHtml = Url.GetHtml(r.ListPageUrl, r.Encoding);
            Match match_moviesinfo = listHtml.GetMatchGroup(r.ListInfoRule);
            while (match_moviesinfo.Success)
            {
                result.Add(new MovieInfo()
                {
                    Actors = match_moviesinfo.Groups["actors"].Value,
                    ClassName = match_moviesinfo.Groups["class"].Value,
                    Director = match_moviesinfo.Groups["director"].Value,
                    FaceImage = match_moviesinfo.Groups["image"].Value,
                    Intro = match_moviesinfo.Groups["intro"].Value,
                    Location = match_moviesinfo.Groups["location"].Value,
                    PublicYear = match_moviesinfo.Groups["publicyear"].Value,
                    Tags = match_moviesinfo.Groups["tags"].Value,
                    Title = match_moviesinfo.Groups["title"].Value,
                    Url = match_moviesinfo.Groups["url"].Value.AppendToDomain(r.ListPageUrl),
                    IsMove = r.IsMovie
                });

                match_moviesinfo = match_moviesinfo.NextMatch();
            }

            MovieListAndNextPage mvs = new MovieListAndNextPage();
            mvs.Movies = result;

            if (Regex.IsMatch(listHtml, r.NextListRule))
            {
                mvs.NextPageUrl = listHtml.GetMatch(r.NextListRule)[0].AppendToDomain(ListUrl);
            }

            Response.Clear();
            Response.Write(JsonConvert.SerializeObject(mvs));
        }
        #endregion

        #region 获取保存电影详细信息
        /// <summary>
        /// 获取保存电影详细信息
        /// </summary>
        /// <param name="rulename">规则名称</param>
        /// <param name="url">地址</param>
        /// <param name="cls">分类名称</param>
        /// <param name="title">标题</param>
        /// <param name="director">导演</param>
        /// <param name="actors">演员</param>
        /// <param name="tags">tag</param>
        /// <param name="location">位置</param>
        /// <param name="publicyear">上映年代</param>
        /// <param name="intro">简介</param>
        /// <param name="image">图片</param>
        protected void GetMovieInfo(string rulename, string url, string cls, string title, string director, string actors, string tags, string location, string publicyear, string intro, string image)
        {
            MovieRule _r = Rules.Where(p => p.Name == rulename).First();
            MovieRule r = _r.Clone();

            Class c = new Class();

            string html = Url.GetHtml(url, r.Encoding);
            MovieInfo mv = MovieInfoView.Find(string.Format("title=N'{0}'", title));
            if (mv.Id <= 0)
            {
                Match m_info = html.GetMatchGroup(r.InfoRule);
                if (m_info.Success)
                {
                    //开始获取信息
                    mv.Actors = m_info.Groups["actors"].Value.IsNull(actors);
                    mv.ClassName = m_info.Groups["class"].Value.IsNull(cls).IsNull(r.DefaultClass);
                    mv.Director = m_info.Groups["director"].Value.IsNull(director);
                    mv.Enable = true;
                    mv.FaceImage = m_info.Groups["image"].Value.IsNull(image).AppendToDomain(url);
                    mv.Intro = m_info.Groups["intro"].Value.IsNull(intro);
                    mv.Location = m_info.Groups["location"].Value.IsNull(location);
                    mv.PublicYear = m_info.Groups["publicyear"].Value.IsNull(publicyear);
                    mv.Tags = m_info.Groups["tags"].Value.IsNull(tags);
                    mv.Title = m_info.Groups["title"].Value.IsNull(title);

                    mv.Intro = Regex.Replace(mv.Intro, "<a.*?>", "", RegexOptions.IgnoreCase);
                    mv.Intro = Regex.Replace(mv.Intro, "</a>","",RegexOptions.IgnoreCase);

                    mv.ClickCount = 0;
                    mv.InsertTime = DateTime.UtcNow.AddHours(8);
                    mv.IsMove = r.IsMovie;
                    mv.ReplyCount = 0;
                    mv.ScoreAvg = 10;
                    mv.ScoreTime = 0;
                    mv.Status = 0;
                    mv.TjCount = 0;
                    mv.UpdateTime = DateTime.UtcNow.AddHours(8);

                    #region 处理分类
                    c = ClassView.Find(string.Format("ClassName=N'{0}'", mv.ClassName));
                    if (c.ID <= 0)
                    {
                        c.IsLeafClass = true;
                        c.Alter = mv.ClassName;
                        c.ClassForder = mv.ClassName;
                        c.ShowInNav = true;
                        c.ParentID = 0;
                        c.ClassName = mv.ClassName;
                        c.ModelID = 6;

                        ClassView.Insert(c);
                    }
                    mv.ClassID = c.ID;
                    #endregion


                    //不存在这个电影就要保存到数据库
                    MovieInfoView.Insert(mv);

                    #region 下载封面
                    try
                    {
                        Url.DownFile(mv.FaceImage, Server.MapPath(string.Format("~/u/MoviekFace/{0}.jpg", mv.Id)));
                        mv.FaceImage = string.Format("/u/MoviekFace/{0}.jpg", mv.Id);
                    }
                    catch
                    {
                        mv.FaceImage = "/u/MoviekFace/0.jpg";
                    }
                    MovieInfoView.Update(mv);
                    #endregion


                }
                else
                {
                    throw new Exception("电影信息匹配失败！");
                }
            }
            else
            {
                c = ClassView.GetModelByID(mv.ClassID.ToString());
            }


            mv.BaiduDramas = new List<MovieUrlBaidu>();
            mv.KuaiboDramas = new List<MovieUrlKuaib>();


            #region 获取快播剧集
            Match m_kuaiboArea = html.GetMatchGroup(r.KuaibAreaRule);
            if (m_kuaiboArea.Success)
            {
                string html_kuaiboArea = m_kuaiboArea.Groups[1].Value;

                Match m_kuaibo = html_kuaiboArea.GetMatchGroup(r.KuaibDramaRule);
                while (m_kuaibo.Success)
                {
                    //判断是够存在
                    if (MovieUrlKuaibView.Exist(string.Format("MovieId={0} and Title=N'{1}'", mv.Id, m_kuaibo.Groups["title"].Value)))
                    {
                        m_kuaibo = m_kuaibo.NextMatch();
                        continue;
                    }
                    mv.KuaiboDramas.Add(new MovieUrlKuaib()
                    {
                        MovieID = mv.Id,
                        Enable = true,
                        PlayUrl = m_kuaibo.Groups["playurl"].Value.AppendToDomain(url),
                        MovieTitle = mv.Title,
                        Title = m_kuaibo.Groups["title"].Value,
                        UpdateTime = DateTime.UtcNow.AddHours(8),
                        Url = m_kuaibo.Groups["url"].Value.IsNullOrEmpty() ? "" : m_kuaibo.Groups["url"].Value.AppendToDomain(url)
                    });
                    m_kuaibo = m_kuaibo.NextMatch();
                }
            }
            #endregion

            #region 获取百度剧集
            Match m_baiduArea = html.GetMatchGroup(r.BaiduAreaRule);
            if (m_baiduArea.Success)
            {
                string html_baiduArea = m_baiduArea.Groups[1].Value;

                Match m_baidu = html_baiduArea.GetMatchGroup(r.BaiduDramaRule);
                while (m_baidu.Success)
                {
                    //判断是够存在
                    if (MovieUrlBaiduView.Exist(string.Format("MovieId={0} and Title=N'{1}'", mv.Id, m_baidu.Groups["title"].Value)))
                    {
                        m_baidu = m_baidu = m_baidu.NextMatch();
                        continue;
                    }
                    mv.BaiduDramas.Add(new MovieUrlBaidu()
                    {
                        MovieID = mv.Id,
                        Enable = true,
                        PlayUrl = m_baidu.Groups["playurl"].Value.AppendToDomain(url),
                        MovieTitle = mv.Title,
                        Title = m_baidu.Groups["title"].Value,
                        UpdateTime = DateTime.UtcNow.AddHours(8),
                        Url = m_baidu.Groups["url"].Value.IsNullOrEmpty() ? "" : m_baidu.Groups["url"].Value.AppendToDomain(url)
                    });
                    m_baidu = m_baidu = m_baidu.NextMatch();
                }
            }
            #endregion

            #region 如果是搜索系统，则只保存播放地址即可
            if (r.IsSearchRule)
            {
                foreach (var drama in mv.KuaiboDramas)
                {
                    #region 处理剧集
                    var sysDrama = MovieDramaView.Find(string.Format("MovieTitle=N'{0}' and Title=N'{1}'", drama.MovieTitle, drama.Title));
                    if (sysDrama.Id <= 0)
                    {
                        sysDrama.Enable = true;
                        sysDrama.MovieID = mv.Id;
                        sysDrama.MovieTitle = mv.Title;
                        sysDrama.Title = drama.Title;
                        sysDrama.UpdateTime = drama.UpdateTime;
                        MovieDramaView.Insert(sysDrama);
                    }
                    #endregion

                    #region 处理播放地址
                    var playUrl = MovieDramaUrlView.Find(string.Format("MovieDramaID={0} and Url=N'{1}'", sysDrama.Id, drama.PlayUrl.IsNull(url)));
                    if (playUrl.Id <= 0)
                    {
                        playUrl.Enable = true;
                        playUrl.MovieDramaID = sysDrama.Id;
                        playUrl.MovieDramaTitle = sysDrama.Title;
                        playUrl.MovieID = mv.Id;
                        playUrl.MovieTitle = mv.Title;
                        playUrl.SourceSite = r.SiteName;
                        playUrl.Title = r.SiteName;
                        playUrl.UpdateTime = DateTime.Now.AddHours(8);
                        playUrl.Url = drama.PlayUrl.IsNull(url);
                        MovieDramaUrlView.Insert(playUrl);
                    }
                    #endregion

                }

                foreach (var drama in mv.BaiduDramas)
                {
                    #region 处理剧集
                    var sysDrama = MovieDramaView.Find(string.Format("MovieTitle=N'{0}' and Title=N'{1}'", drama.MovieTitle, drama.Title));
                    if (sysDrama.Id <= 0)
                    {
                        sysDrama.Enable = true;
                        sysDrama.MovieID = mv.Id;
                        sysDrama.MovieTitle = mv.Title;
                        sysDrama.Title = drama.Title;
                        sysDrama.UpdateTime = drama.UpdateTime;
                        MovieDramaView.Insert(sysDrama);
                    }
                    #endregion

                    #region 处理播放地址
                    var playUrl = MovieDramaUrlView.Find(string.Format("MovieDramaID={0} and Url=N'{1}'", sysDrama.Id, drama.PlayUrl.IsNull(url)));
                    if (playUrl.Id <= 0)
                    {
                        playUrl.Enable = true;
                        playUrl.MovieDramaID = sysDrama.Id;
                        playUrl.MovieDramaTitle = sysDrama.Title;
                        playUrl.MovieID = mv.Id;
                        playUrl.MovieTitle = mv.Title;
                        playUrl.SourceSite = r.SiteName;
                        playUrl.Title = r.SiteName;
                        playUrl.UpdateTime = DateTime.Now.AddHours(8);
                        playUrl.Url = drama.PlayUrl.IsNull(url);
                        MovieDramaUrlView.Insert(playUrl);
                    }
                    #endregion

                }
            }
            #endregion

            #region 处理快播和百度影音
            else
            {
                foreach (var drama in mv.KuaiboDramas)
                {
                    if (drama.Url.IsNullOrEmpty() == false)
                    {
                        var sysDrama = MovieUrlKuaibView.Find(string.Format("MovieID={0} and Title=N'{1}'", mv.Id, drama.Title));
                        if (sysDrama.Id <= 0)
                        {
                            MovieUrlKuaibView.Insert(drama);

                            //保存完成 生成
                            CreatePage.CreateDramapage(drama, c);
                        }
                    }
                }

                foreach (var drama in mv.BaiduDramas)
                {
                    if (drama.Url.IsNullOrEmpty() == false)
                    {
                        var sysDrama = MovieUrlBaiduView.Find(string.Format("MovieID={0} and Title=N'{1}'", mv.Id, drama.Title));
                        if (sysDrama.Id <= 0)
                        {
                            MovieUrlBaiduView.Insert(drama);
                            //保存完成 生成
                            CreatePage.CreateDramapage(drama, c);
                        }
                    }
                }
            }
            #endregion

            // 完成咯
            Response.Clear();
            Response.Write(JsonConvert.SerializeObject(mv));
        }
        #endregion

        #region 获取播放页面的url-source ----快播
        /// <summary>
        /// 获取播放页面的url-source ----快播
        /// </summary>
        /// <param name="RuleName"></param>
        /// <param name="url"></param>
        /// <param name="Title"></param>
        /// <param name="MovieID"></param>
        /// <param name="MovieTitle"></param>
        protected void GetKuaiboDrama(string RuleName, string url, string Title, int MovieID, string MovieTitle)
        {
            MovieRule _r = Rules.Where(p => p.Name == RuleName).First();
            MovieRule r = _r.Clone();

            string html = Url.GetHtml(url, r.Encoding);
            Match m = html.GetMatchGroup(r.DramaPageKuaibRule);
            var sysDrama = MovieUrlKuaibView.Find(string.Format("MovieID={0} and Title=N'{1}'", MovieID, Title));
            if (m.Success)
            {
                if (sysDrama.Id <= 0)
                {
                    sysDrama.Enable = true;
                    sysDrama.MovieID = MovieID;
                    sysDrama.MovieTitle = MovieTitle;
                    sysDrama.Title = Title;
                    sysDrama.UpdateTime = DateTime.UtcNow.AddHours(8);
                    sysDrama.Url = m.Groups["url"].Value.IsNullOrEmpty() ? "" : m.Groups["url"].Value.AppendToDomain(url);
                    sysDrama.SourceUrl = m.Groups["source"].Value.AppendToDomain(url);

                    if (m.Groups["url"].Value.IsNullOrEmpty() == false)
                    {
                        //如果url为空，则不写入数据库，不为空，就写入数据库
                        MovieUrlKuaibView.Insert(sysDrama);
                    }
                }

            }
            Response.Clear();
            Response.Write(JsonConvert.SerializeObject(sysDrama));
        }
        #endregion

        #region 获取播放页面的url-source ----百度影音
        /// <summary>
        /// 获取播放页面的url-source ----百度影音
        /// </summary>
        /// <param name="RuleName"></param>
        /// <param name="url"></param>
        /// <param name="Title"></param>
        /// <param name="MovieID"></param>
        /// <param name="MovieTitle"></param>
        protected void GetBaiduDrama(string RuleName, string url, string Title, int MovieID, string MovieTitle)
        {
            MovieRule _r = Rules.Where(p => p.Name == RuleName).First();
            MovieRule r = _r.Clone();

            string html = Url.GetHtml(url, r.Encoding);
            Match m = html.GetMatchGroup(r.DramaPageBaiduRule);
            var sysDrama = MovieUrlBaiduView.Find(string.Format("MovieID={0} and Title=N'{1}'", MovieID, Title));
            if (m.Success)
            {
                if (sysDrama.Id <= 0)
                {
                    sysDrama.Enable = true;
                    sysDrama.MovieID = MovieID;
                    sysDrama.MovieTitle = MovieTitle;
                    sysDrama.Title = Title;
                    sysDrama.UpdateTime = DateTime.UtcNow.AddHours(8);
                    sysDrama.Url = m.Groups["url"].Value.IsNullOrEmpty() ? "" : m.Groups["url"].Value.AppendToDomain(url);
                    sysDrama.SourceUrl = m.Groups["source"].Value.AppendToDomain(url);

                    if (m.Groups["url"].Value.IsNullOrEmpty() == false)
                    {
                        //如果url为空，则不写入数据库，不为空，就写入数据库
                        MovieUrlBaiduView.Insert(sysDrama);
                    }
                }
            }
            Response.Clear();
            Response.Write(JsonConvert.SerializeObject(sysDrama));

        }
        #endregion

        #region 获取Source文件地址----百度影音
        /// <summary>
        /// 获取Source文件地址----百度影音
        /// </summary>
        /// <param name="RuleName"></param>
        /// <param name="url"></param>
        /// <param name="Title"></param>
        /// <param name="MovieID"></param>
        /// <param name="MovieTitle"></param>
        protected void GetBaiduSourceUrl(string RuleName, string url, string Title, int MovieID, string MovieTitle)
        {
            MovieRule _r = Rules.Where(p => p.Name == RuleName).First();
            MovieRule r = _r.Clone();

            string html = Url.GetHtml(url, r.Encoding);
            Match m = html.GetMatchGroup(r.SourceBaiduRule);
            var sysDrama = MovieUrlBaiduView.Find(string.Format("MovieID={0} and Title=N'{1}'", MovieID, Title));
            if (sysDrama.Id <= 0)
            {
                if (m.Success)
                {
                    sysDrama.Enable = true;
                    sysDrama.MovieID = MovieID;
                    sysDrama.MovieTitle = MovieTitle;
                    sysDrama.Title = Title;
                    sysDrama.UpdateTime = DateTime.UtcNow.AddHours(8);
                    sysDrama.Url = m.Groups["url"].Value;
                    sysDrama.Url = Regex.Replace(sysDrama.Url, "\\\\u.*?\\.", string.Format("{0}-{1}.", sysDrama.MovieTitle, sysDrama.Title));
                    MovieUrlBaiduView.Insert(sysDrama);
                    CreatePage.CreateDramapage(sysDrama, MovieInfoView.GetClass(sysDrama));
                }
            }
            Response.Clear();
            Response.Write(JsonConvert.SerializeObject(sysDrama));

        }
        #endregion

        #region 获取Source文件地址----快播
        /// <summary>
        /// 获取Source文件地址----快播
        /// </summary>
        /// <param name="RuleName"></param>
        /// <param name="url"></param>
        /// <param name="Title"></param>
        /// <param name="MovieID"></param>
        /// <param name="MovieTitle"></param>
        protected void GetKuaiboSourceUrl(string RuleName, string url, string Title, int MovieID, string MovieTitle)
        {
            MovieRule _r = Rules.Where(p => p.Name == RuleName).First();
            MovieRule r = _r.Clone();

            string html = Url.GetHtml(url, r.Encoding);
            Match m = html.GetMatchGroup(r.SourceKuaibRule);
            var sysDrama = MovieUrlKuaibView.Find(string.Format("MovieID={0} and Title=N'{1}'", MovieID, Title));
            if (sysDrama.Id <= 0)
            {
                if (m.Success)
                {
                    sysDrama.Enable = true;
                    sysDrama.MovieID = MovieID;
                    sysDrama.MovieTitle = MovieTitle;
                    sysDrama.Title = Title;
                    sysDrama.UpdateTime = DateTime.UtcNow.AddHours(8);
                    sysDrama.Url = m.Groups["url"].Value;
                    sysDrama.Url = Regex.Replace(sysDrama.Url, "\\\\u.*?\\.", string.Format("{0}-{1}.",sysDrama.MovieTitle,sysDrama.Title));

                    MovieUrlKuaibView.Insert(sysDrama);

                    CreatePage.CreateDramapage(sysDrama, MovieInfoView.GetClass(sysDrama));
                }
            }
            Response.Clear();
            Response.Write(JsonConvert.SerializeObject(sysDrama));

        }
        #endregion
    }

    public class MovieListAndNextPage
    {
        public List<MovieInfo> Movies { get; set; }

        public string NextPageUrl { get; set; }
    }
}