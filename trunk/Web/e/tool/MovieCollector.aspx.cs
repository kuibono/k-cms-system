using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text.RegularExpressions;

using Voodoo;
using Voodoo.Model;
using Voodoo.DAL;
using Voodoo.Net;
using Voodoo.Basement;

namespace Web.e.tool
{
    public partial class MovieCollector : System.Web.UI.Page
    {
        protected MovieInfo DefaultMovie;

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Buffer = false;


            //打开列表页面
            Response.Write("打开列表页面<br/>");
            string listUrl = "http://kuaib.tv.sohu.com/html/more_list21.htm";

        openurl:
            string html_List = Url.GetHtml(listUrl, "utf-8");

            //打开信息页面
            Match m_list = html_List.GetMatchGroup("<img src=\"(?<image>.*?)\" width=\"120\" height=\"165\" alt=\"\" />[\\s\\S]*?<h4><a href=\"(?<url>.*?)\" target=\"_blank\">(?<title>.*?)</a></h4>");
            while (m_list.Success)
            {
                //判断是否存在
                if (MovieInfoView.Exist(string.Format("Title=N'{0}'", m_list.Groups["title"].Value)))
                {
                    m_list = m_list.NextMatch();
                    continue;
                }

                Response.Write("下载封面<br/>");
                //如果不存在，则先下载封面，内容页面是没有封面的
                try
                {
                    Url.DownFile(m_list.Groups["image"].Value, Server.MapPath("~/config/movieface.jpg"));
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message + "<br/>");
                }
                //打开内容页面
                Response.Write("打开内容页面<br/>");
                string html_content = Url.GetHtml(m_list.Groups["url"].Value, "utf-8");
                Match m_movie = html_content.GetMatchGroup("<em id='specialID'>《(?<title>.*?)》</em>[\\s\\S]*?<param name='URL' value='(?<url>.*?)'>[\\s\\S]*?<div id=\"introID\">[\\s]*?<p>(?<intro>[\\s\\S]*?)</p>[\\s\\S]*?var VRS_DIRECTOR=\"(?<director>.*?)\";[\\s\\S]*?var VRS_CATEGORY=\"(?<tags>.*?)\";[\\s\\S]*?var VRS_ACTOR=\"(?<actor>.*?)\";[\\s\\S]*?var VRS_AREA=\"(?<location>.*?)\";[\\s\\S]*?var VRS_PLAY_YEAR=\"(?<year>.*?)\";");
                if (!m_movie.Success)
                {
                    Response.Write("!!!!!内容匹配失败<br/>");
                }
                string title = m_movie.Groups["title"].Value;
                string intro = m_movie.Groups["intro"].Value;
                string director = m_movie.Groups["director"].Value;
                string actor = m_movie.Groups["actor"].Value;
                string location = m_movie.Groups["location"].Value;
                string url = m_movie.Groups["url"].Value;
                string year = m_movie.Groups["year"].Value;
                string tags = m_movie.Groups["tags"].Value;

                Response.Write("处理类别:" + location + "<br/>");
                Class cls = ClassView.Find(string.Format("classname=N'{0}'", location));
                if (cls.ID <= 0)
                {
                    cls.Alter = location;
                    cls.ClassKeywords = location + "在线观看";
                    cls.ClassName = location;
                    cls.IsLeafClass = true;
                    cls.ShowInNav = true;
                    cls.ClassForder = location;
                    cls.ModelID = 6;

                    ClassView.Insert(cls);
                }

                MovieInfo mv = new MovieInfo();
                mv.Actors = actor;
                mv.ClassID = cls.ID;
                mv.ClassName = cls.ClassName;
                mv.ClickCount = 0;
                mv.Director = director;
                mv.Enable = true;
                mv.InsertTime = DateTime.Now;
                mv.Intro = intro;
                mv.IsMove = true;
                mv.Location = location;
                mv.PublicYear = year;
                mv.ReplyCount = 0;
                mv.ScoreAvg = 10;
                mv.ScoreTime = 0;
                mv.Status = 1;
                mv.Tags = tags;
                mv.Title = title;
                mv.TjCount = 0;
                mv.UpdateTime = DateTime.Now;

                Response.Write("保存:" + title + "<br/>");
                MovieInfoView.Insert(mv);

                //设置封面
                Response.Write("设置封面<br/>");
                mv.FaceImage = string.Format("/u/MoviekFace/{0}.jpg", mv.Id);
                try
                {
                    Voodoo.IO.File.Move(Server.MapPath("~/config/movieface.jpg"), Server.MapPath(mv.FaceImage));
                }
                catch
                {
                    Voodoo.IO.File.Copy(Server.MapPath("~/config/0.jpg"), Server.MapPath(mv.FaceImage));
                }

                MovieInfoView.Update(mv);

                //添加地址
                Response.Write("添加地址:" + url + "<br/>");
                MovieUrlKuaib mk = new MovieUrlKuaib();
                mk.Enable = true;
                mk.MovieID = mv.Id;
                mk.MovieTitle = mv.Title;
                mk.Title = "全集";
                mk.UpdateTime = DateTime.Now;
                mk.Url = url;
                MovieUrlKuaibView.Insert(mk);

                //生成
                Response.Write("生成<br/>");
                //CreatePage.CreateDramapage(mk,cls);

                //CreatePage.CreateContentPage(mv,cls);

                //CreatePage.CreateListPage(cls,0);

                //CreatePage.GreateIndexPage();

                Response.Write(title + "-完成<br/><br/><br/>");

                m_list = m_list.NextMatch();
            }

            //处理列表下一页

            Match m_next = html_List.GetMatchGroup("<a href='(?<key>[^'/]*?)'>下一页</a>");
            if (m_next.Success)
            {
                listUrl = m_next.Groups["key"].Value.AppendToDomain(listUrl);
                goto openurl;
            }


        }

        #region 获取所有剧集的信息
        /// <summary>
        /// 获取所有剧集的信息
        /// </summary>
        /// <param name="Encoding">编码</param>
        /// <param name="ListPageUrl">列表地址</param>
        /// <param name="NextUrlRule">下一页规则</param>
        /// <param name="InfoRule">信息规则</param>
        /// <returns></returns>
        protected List<MovieInfo> GetAllMovies(string Encoding, string ListPageUrl, string NextUrlRule, string InfoRule)
        {
            List<MovieInfo> result = new List<MovieInfo>();

        OpenListPage:
            string listHtml = Url.GetHtml(ListPageUrl, Encoding);
            Match match_moviesinfo = listHtml.GetMatchGroup(InfoRule);
            while (match_moviesinfo.Success)
            {
                result.Add(new MovieInfo()
                {
                    Actors = match_moviesinfo.Groups["actors"].Value,
                    ClassName = match_moviesinfo.Groups["class"].Value,
                    Director = match_moviesinfo.Groups["director"].Value,
                    FaceImage = match_moviesinfo.Groups["image"].Value.AppendToDomain(ListPageUrl),
                    Intro = match_moviesinfo.Groups["intro"].Value,
                    Location = match_moviesinfo.Groups["location"].Value,
                    PublicYear = match_moviesinfo.Groups["publicyear"].Value,
                    Tags = match_moviesinfo.Groups["tags"].Value,
                    Title = match_moviesinfo.Groups["title"].Value,
                    Url = match_moviesinfo.Groups["url"].Value.AppendToDomain(ListPageUrl)
                });

                match_moviesinfo = match_moviesinfo.NextMatch();
            }

            if (Regex.IsMatch(listHtml, NextUrlRule))
            {
                //需要key参数
                ListPageUrl = listHtml.GetMatch(NextUrlRule)[0].AppendToDomain(ListPageUrl);
                goto OpenListPage;
            }


            return result;
        }
        #endregion

        #region 获取单个剧集的详细信息
        /// <summary>
        /// 获取单个剧集的详细信息
        /// </summary>
        /// <param name="Encoding">编码</param>
        /// <param name="mv">剧集的大概信息</param>
        /// <param name="InfoRule">剧集信息规则</param>
        /// <param name="KuaibAreaRule">快播区域规则</param>
        /// <param name="BaiduAreaRule">百度影音区域规则</param>
        /// <param name="KuaibDramaRule">快播单集规则</param>
        /// <param name="BaiduDramaRule">百度影音单集规则</param>
        /// <returns></returns>
        protected MovieInfo GetMovieInfo(string Encoding, MovieInfo mv, string InfoRule, string KuaibAreaRule, string BaiduAreaRule, string KuaibDramaRule, string BaiduDramaRule)
        {
            string html = Url.GetHtml(mv.Url, Encoding);
            mv.Html = html;
            Match m_info = html.GetMatchGroup(InfoRule);
            if (m_info.Success)
            {
                //开始获取信息
                mv.Actors = mv.Actors.IsNull(m_info.Groups["actors"].Value);
                mv.ClassName = mv.ClassName.IsNull(m_info.Groups["class"].Value);
                mv.Director = mv.Director.IsNull(m_info.Groups["director"].Value);
                mv.Enable = true;
                mv.FaceImage = mv.FaceImage.IsNull(m_info.Groups["image"].Value);
                mv.Intro = mv.Intro.IsNull(m_info.Groups["intro"].Value);
                mv.Location = mv.Location.IsNull(m_info.Groups["location"].Value);
                mv.PublicYear = mv.PublicYear.IsNull(m_info.Groups["publicyear"].Value);
                mv.Tags = mv.Tags.IsNull(m_info.Groups["tags"].Value);
                mv.Title = mv.Title.IsNull(m_info.Groups["title"].Value);
            }
            else
            {
                throw new Exception("电影信息匹配失败！");
            }
            mv.KuaiboDramas = new List<MovieDrama>();
            mv.BaiduDramas = new List<MovieDrama>();

            #region 获取快播剧集地址
            Match m_kuaiboArea = html.GetMatchGroup(KuaibAreaRule);
            if (m_kuaiboArea.Success)
            {
                Match m_drama = html.GetMatchGroup(m_kuaiboArea.Groups[1].Value);
                while (m_drama.Success)
                {
                    mv.KuaiboDramas.Add(new MovieDrama()
                    {
                        Title = m_drama.Groups["title"].Value,
                        PlayUrl = m_drama.Groups["playurl"].Value,
                        Url = m_drama.Groups["url"].Value

                    });
                    m_drama = m_drama.NextMatch();
                }
            }
            #endregion

            #region 获取百度影音剧集地址
            Match m_baiduArea = html.GetMatchGroup(BaiduAreaRule);
            if (m_baiduArea.Success)
            {
                Match m_drama = html.GetMatchGroup(m_kuaiboArea.Groups[1].Value);
                while (m_drama.Success)
                {
                    mv.BaiduDramas.Add(new MovieDrama()
                    {
                        Title = m_drama.Groups["title"].Value,
                        PlayUrl = m_drama.Groups["playurl"].Value,
                        Url = m_drama.Groups["url"].Value

                    });
                    m_drama = m_drama.NextMatch();
                }
            }
            #endregion

            return mv;

        }

        #endregion
    }

    public class CollectRule
    {
        public string SiteName { get; set; }

        public string Encoding { get; set; }

        public string ListPageUrl { get; set; }

        public string UrlTitleImageRule { get; set; }

        /// <summary>
        /// 信息规则，可以有url也可以没有url
        /// </summary>
        public string MovieInfoRule { get; set; }

        public string DramaPageRule { get; set; }

        public string DramaUrlAndTitle { get; set; }


    }
}