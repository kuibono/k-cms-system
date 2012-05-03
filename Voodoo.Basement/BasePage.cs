using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Voodoo.Data;
using Voodoo.Model;
using Voodoo.DAL;
using System.Web;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

using Voodoo.IO;
using System.IO;

namespace Voodoo.Basement
{
    public class BasePage : System.Web.UI.Page
    {
        #region 静态系统参数
        /// <summary>
        /// 静态系统参数
        /// </summary>
        public static SysSetting SystemSetting
        {
            get
            {
                return Setting.SysSettingDAL.GetSetting();
            }
        }
        #endregion

        #region 获取数据访问对象GetHelper
        /// <summary>
        /// 获取数据访问对象
        /// </summary>
        public IDbHelper GetHelper()
        {
            return Voodoo.Setting.DataBase.GetHelper();
        }
        #endregion

        #region 通过ID获取群组
        /// <summary>
        /// 通过ID获取群组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysGroup GetGroupByID(int id)
        {
            return SysUserAction.GetGroupNameByID(id);
        }
        #endregion

        #region  根据ID获得部门名
        /// <summary>
        /// 根据ID获得部门名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetDepartmentByID(int id)
        {
            return SysUserAction.GetDepartmentByID(id);
        }
        #endregion

        #region 获取系统分组
        /// <summary>
        /// 获取系统分组
        /// </summary>
        /// <returns></returns>
        public List<SysGroup> GetSysGroup()
        {
            return SysUserAction.GetSysGroup();
        }
        #endregion

        #region 获取系统部门
        /// <summary>
        /// 获取系统部门
        /// </summary>
        /// <returns></returns>
        public List<SysDepartment> GetSysDepartment()
        {
            return SysUserAction.GetSysDepartment();
        }
        #endregion

        #region 页面生成选项
        /// <summary>
        /// 页面生成选项
        /// </summary>
        public static ListItemCollection CreatePageOptions
        {
            get
            {
                ListItemCollection nv = new ListItemCollection();
                nv.Add(new ListItem("不生成", "0"));
                nv.Add(new ListItem("生成当前栏目", "1"));
                nv.Add(new ListItem("生成首页", "2"));
                nv.Add(new ListItem("生成父栏目", "3"));
                nv.Add(new ListItem("生成当前栏目与父栏目", "4"));
                nv.Add(new ListItem("生成父栏目与首页", "5"));
                nv.Add(new ListItem("生成当前栏目、父栏目与首页", "6"));
                return nv;
            }
        }
        #endregion

        #region 管理投稿选项
        /// <summary>
        /// 管理投稿选项
        /// </summary>
        public static NameValueCollection PostManagementOptions
        {
            get
            {
                NameValueCollection nv = new NameValueCollection();
                nv.Add("不能管理信息", "0");
                nv.Add("可管理未审核信息", "1");
                nv.Add("只可编辑未审核信息", "2");
                nv.Add("只可删除未审核信息", "3");
                nv.Add("可管理所有信息", "4");
                nv.Add("只可编辑所有信息", "5");
                nv.Add("只可删除所有信息", "6");
                return nv;
            }
        }
        #endregion


        #region 获取信息地址
        /// <summary>
        /// 获取信息地址
        /// </summary>
        /// <param name="news">信息</param>
        /// <param name="cls">栏目</param>
        /// <returns></returns>
        public static string GetNewsUrl(News news, Class cls)
        {

            string result = "";
            if (news.NavUrl.Length > 0) //如果是外部链接
            {
                result = news.NavUrl;
            }
            else
            {
                //网站地址 栏目父目录 栏目目录 文件目录 文件名 扩展名

                string fileName = news.FileName;
                if (news.FileName.IsNullOrEmpty())
                {
                    fileName = news.ID.ToString();//此处需要修改
                }

                string sitrurl = "/";
                //string sitrurl=BasePage.SystemSetting.SiteUrl;
                //if(sitrurl.IsNullOrEmpty())
                //{
                //    sitrurl="/";
                //}

                string parentForder = cls.ClassForder;
                if (!parentForder.IsNullOrEmpty())
                {
                    parentForder += "/";
                }
                string newsFolder = news.FileForder;
                if (!newsFolder.IsNullOrEmpty())
                {
                    newsFolder += "/";
                }

                result = string.Format("{0}{1}{2}/{3}{4}{5}",
                    sitrurl,
                    cls.ParentClassForder.IsNullOrEmpty() ? "" : cls.ParentClassForder + "/",
                    cls.ClassForder,
                    newsFolder,
                    fileName,
                    BasePage.SystemSetting.ExtName
                    );
                result = Regex.Replace(result, "[/]{2,}", "/");
            }


            return result;
        }
        #endregion

        #region 获取图片内容页面地址 GetImageUrl
        /// <summary>
        /// 获取图片内容页面地址
        /// </summary>
        /// <param name="img"></param>
        /// <param name="cls"></param>
        /// <returns></returns>
        public static string GetImageUrl(ImageAlbum img, Class cls)
        {
            string result = "";
            string fileName = img.ID.ToString();


            string sitrurl = "/";

            string parentForder = cls.ClassForder;
            if (!parentForder.IsNullOrEmpty())
            {
                parentForder += "/";
            }
            string newsFolder = img.Folder;
            if (newsFolder.IsNullOrEmpty())
            {
                newsFolder = "/";
            }

            result = string.Format("{0}{1}{2}/{3}/{4}{5}",
                sitrurl,
                cls.ParentClassForder.IsNullOrEmpty() ? "" : cls.ParentClassForder + "/",
                cls.ClassForder,
                newsFolder,
                fileName,
                BasePage.SystemSetting.ExtName
                );
            result = Regex.Replace(result, "[/]{2,}", "/");
            return result;
        }
        #endregion

        #region 获取问答url地址
        /// <summary>
        /// 获取问答url地址
        /// </summary>
        /// <param name="qs"></param>
        /// <param name="cls"></param>
        /// <returns></returns>
        public static string GetQuestionUrl(Question qs, Class cls)
        {
            string result = "";
            string fileName = qs.ID.ToString();


            string sitrurl = "/";

            string parentForder = cls.ClassForder;
            if (!parentForder.IsNullOrEmpty())
            {
                parentForder += "/";
            }
            

            result = string.Format("{0}{1}{2}/{3}{4}",
                sitrurl,
                cls.ParentClassForder.IsNullOrEmpty() ? "" : cls.ParentClassForder + "/",
                cls.ClassForder,
                fileName,
                BasePage.SystemSetting.ExtName
                );
            result = Regex.Replace(result, "[/]{2,}", "/");
            return result;
        }
        #endregion

        #region 获取书籍url地址
        /// <summary>
        /// 获取问答url地址
        /// </summary>
        /// <param name="qs"></param>
        /// <param name="cls"></param>
        /// <returns></returns>
        public static string GetBookUrl(Book b, Class cls)
        {
            string result = "";
            string fileName = b.Title+"-"+b.Author;//书名+作者


            string sitrurl = "/Book/";

            //string parentForder = cls.ClassForder;
            //if (!parentForder.IsNullOrEmpty())
            //{
            //    parentForder += "/";
            //}


            result = string.Format("{0}{1}/{2}/index{3}",
                sitrurl,
                //cls.ParentClassForder.IsNullOrEmpty() ? "" : cls.ParentClassForder + "/",
                cls.ClassForder,
                fileName,
                BasePage.SystemSetting.ExtName
                );
            result = Regex.Replace(result, "[/]{2,}", "/");
            return result;
        }
        #endregion

        #region 获取书籍章节url地址
        /// <summary>
        /// 获取问答url地址
        /// </summary>
        /// <param name="qs"></param>
        /// <param name="cls"></param>
        /// <returns></returns>
        public static string GetBookChapterUrl(BookChapter cp, Class cls)
        {
            string result = "";
            string fileName = cp.ID.ToString();

            Book b = BookView.GetModelByID(cp.BookID.ToS());


            string sitrurl = "/Book/";

            //string parentForder = cls.ClassForder;
            //if (!parentForder.IsNullOrEmpty())
            //{
            //    parentForder += "/";
            //}


            result = string.Format("{0}{1}/{2}/{3}{4}",
                sitrurl,
                //cls.ParentClassForder.IsNullOrEmpty() ? "" : cls.ParentClassForder + "/",
                cls.ClassForder,
                 b.Title+"-"+b.Author,
                cp.ID,
                BasePage.SystemSetting.ExtName
                );
            result = Regex.Replace(result, "[/]{2,}", "/");
            return result;
        }

        /// <summary>
        /// 获取章节txt文件路径
        /// </summary>
        /// <param name="cp"></param>
        /// <param name="cls"></param>
        /// <returns></returns>
        public static string GetBookChapterTxtUrl(BookChapter cp, Class cls)
        {
            string result = "";
            string fileName = cp.ID.ToString();

            Book b = BookView.GetModelByID(cp.BookID.ToS());


            string sitrurl = "/Txt/";

            //string parentForder = cls.ClassForder;
            //if (!parentForder.IsNullOrEmpty())
            //{
            //    parentForder += "/";
            //}


            result = string.Format("{0}{1}{2}/{3}/{4}{5}",
                sitrurl,
                cls.ParentClassForder.IsNullOrEmpty() ? "" : cls.ParentClassForder + "/",
                cls.ClassForder,
                 b.Title + "-" + b.Author,
                cp.ID,
                ".txt"
                );
            result = Regex.Replace(result, "[/]{2,}", "/");
            return result;
        }
        #endregion

        #region 获取影视地址
        /// <summary>
        /// 获取影视地址
        /// </summary>
        /// <param name="qs"></param>
        /// <param name="cls"></param>
        /// <returns></returns>
        public static string GetMovieUrl(MovieInfo b, Class cls)
        {
            string result = "";
            string fileName = b.Title;//书名+作者


            string sitrurl = "/Movie/";


            result = string.Format("{0}{1}/{2}/index{3}",
                sitrurl,
                cls.ClassForder,
                fileName,
                BasePage.SystemSetting.ExtName
                );
            result = Regex.Replace(result, "[/]{2,}", "/");
            return result;
        }
        #endregion

        #region 获取影视播放页面地址
        /// <summary>
        /// 获取影视地址
        /// </summary>
        /// <param name="qs"></param>
        /// <param name="cls"></param>
        /// <returns></returns>
        public static string GetMovieDramaUrl(MovieUrlBaidu b, Class cls)
        {
            if (b == null)
            {
                return "";
            }
            string result = "";


            string sitrurl = "/Movie/";


            result = string.Format("{0}{1}/{2}/Baidu/{3}{4}",
                sitrurl,
                cls.ClassForder,
                b.MovieTitle,
                b.Id,
                BasePage.SystemSetting.ExtName
                );
            result = Regex.Replace(result, "[/]{2,}", "/");
            return result;
        }

        public static string GetMovieDramaUrl(MovieUrlKuaib b, Class cls)
        {
            if (b == null)
            {
                return "";
            }

            MovieInfo movie = MovieInfoView.GetModelByID(b.MovieID.ToS());

            string result = "";


            string sitrurl = "/Movie/";


            result = string.Format("{0}{1}/{2}/Kuaib/{3}{4}",
                sitrurl,
                cls.ClassForder,
                movie.Title,
                b.Id,
                BasePage.SystemSetting.ExtName
                );
            result = Regex.Replace(result, "[/]{2,}", "/");
            return result;
        }

        public static string GetMovieDramaUrl(MovieDrama b, Class cls)
        {
            if (b == null)
            {
                return "";
            }
            string result = "";


            string sitrurl = "/Movie/";


            result = string.Format("{0}{1}/{2}/urls/{3}{4}",
                sitrurl,
                cls.ClassForder,
                b.MovieTitle,
                b.Id,
                BasePage.SystemSetting.ExtName
                );
            result = Regex.Replace(result, "[/]{2,}", "/");
            return result;
        }

        #endregion


        #region 获取栏目地址
        /// <summary>
        /// 获取栏目地址
        /// </summary>
        /// <param name="cls">栏目</param>
        /// <returns></returns>
        public static string GetClassUrl(Class cls)
        {

            string sitrurl = "/Book/";
            if (cls.ModelID != 4)
            {
                sitrurl = "";
            }
            string result = string.Format("{0}/{1}/index{2}",
                sitrurl,
                cls.ClassForder,
                SystemSetting.ExtName
                );

            result = Regex.Replace(result, "[/]{2,}", "/");
            return result;
        }

        /// <summary>
        /// 获取栏目地址
        /// </summary>
        /// <param name="cls"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static string GetClassUrl(Class cls, int page)
        {
            
            string sitrurl = "/Book/";
            if (cls.ModelID != 4)
            {
                sitrurl = "";
            }
            return string.Format("{0}/{1}/index{2}",
                sitrurl,
                cls.ClassForder,
                page > 1 ? "_" + page.ToS() + SystemSetting.ExtName : SystemSetting.ExtName
                );
        }
        #endregion

        #region 获取上一篇文章、图片、提问
        /// <summary>
        /// 获取上一篇文章 
        /// </summary>
        /// <param name="news"></param>
        /// <param name="cls"></param>
        /// <returns></returns>
        public static News GetPreNews(News news, Class cls)
        {
            List<News> lresult = NewsView.GetModelList(string.Format("classID={0} and ID<{1} order by ID Desc", cls.ID, news.ID), 1);
            if (lresult.Count == 0)
            {
                return null;
            }
            else
            {
                return lresult.First();
            }
        }

        /// <summary>
        /// 获取上一个图片
        /// </summary>
        /// <param name="img"></param>
        /// <param name="cls"></param>
        /// <returns></returns>
        public static ImageAlbum GetPreImage(ImageAlbum img, Class cls)
        {
            List<ImageAlbum> lresult = ImageAlbumView.GetModelList(string.Format("classID={0} and ID<{1} order by ID Desc", cls.ID, img.ID), 1);
            if (lresult.Count == 0)
            {
                return null;
            }
            else
            {
                return lresult.First();
            }
        }

        /// <summary>
        /// 获取上一篇提问
        /// </summary>
        /// <param name="qs"></param>
        /// <param name="cls"></param>
        /// <returns></returns>
        public static Question GetPreQuestion(Question qs, Class cls)
        {
            List<Question> lresult = QuestionView.GetModelList(string.Format("classID={0} and ID<{1} order by ID Desc", cls.ID, qs.ID), 1);
            if (lresult.Count == 0)
            {
                return null;
            }
            else
            {
                return lresult.First();
            }
        }

        /// <summary>
        /// 获取上一本小说
        /// </summary>
        /// <param name="b"></param>
        /// <param name="cls"></param>
        /// <returns></returns>
        public static Book GetPreBook(Book b, Class cls)
        {
            List<Book> lresult = BookView.GetModelList(string.Format("classID={0} and ID<{1} order by ID Desc", cls.ID, b.ID), 1);
            if (lresult.Count == 0)
            {
                return null;
            }
            else
            {
                return lresult.First();
            }
        }

        /// <summary>
        /// 获取上一章节
        /// </summary>
        /// <param name="cp"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static BookChapter GetPreChapter(BookChapter cp, Book b)
        {
            List<BookChapter> lresult = BookChapterView.GetModelList(string.Format("BookID={0} and ID<{1} order by ID Desc", b.ID, cp.ID), 1);
            if (lresult.Count == 0)
            {
                return null;
            }
            else
            {
                return lresult.First();
            }
        }

        /// <summary>
        /// 获取快播上一集地址
        /// </summary>
        /// <param name="kuai"></param>
        /// <returns></returns>
        public static MovieUrlKuaib GetPreKuaibo(MovieUrlKuaib kuai)
        {
            List<MovieUrlKuaib> lresult = MovieUrlKuaibView.GetModelList(string.Format("MovieID={0} and id<{1} order by id desc", kuai.MovieID, kuai.Id));
            if (lresult.Count == 0)
            {
                return null;
            }
            else
            {
                return lresult.First();
            }
        }

        /// <summary>
        /// 获取百度上一集地址
        /// </summary>
        /// <param name="kuai"></param>
        /// <returns></returns>
        public static MovieUrlBaidu GetPreBaidu(MovieUrlBaidu kuai)
        {
            List<MovieUrlBaidu> lresult = MovieUrlBaiduView.GetModelList(string.Format("MovieID={0} and id<{1} order by id desc", kuai.MovieID, kuai.Id));
            if (lresult.Count == 0)
            {
                return null;
            }
            else
            {
                return lresult.First();
            }
        }

        /// <summary>
        /// 获取上一个单集
        /// </summary>
        /// <param name="drama"></param>
        /// <returns></returns>
        public static MovieDrama GetPreDrama(MovieDrama drama)
        {
            List<MovieDrama> lresult = MovieDramaView.GetModelList(string.Format("MovieID={0} and id<{1} order by id desc", drama.MovieID, drama.Id));
            if (lresult.Count == 0)
            {
                return null;
            }
            else
            {
                return lresult.First();
            }
        }

        #endregion

        #region 获取下一篇文章、相册、提问
        /// <summary>
        /// 获取下一篇文章
        /// </summary>
        /// <param name="news"></param>
        /// <param name="cls"></param>
        /// <returns></returns>
        public static News GetNextNews(News news, Class cls)
        {
            List<News> lresult = NewsView.GetModelList(string.Format("classID={0} and ID>{1} order by ID Asc", cls.ID, news.ID), 1);
            if (lresult.Count == 0)
            {
                return null;
            }
            else
            {
                return lresult.First();
            }
        }

        /// <summary>
        /// 获取下一个相册
        /// </summary>
        /// <param name="img"></param>
        /// <param name="cls"></param>
        /// <returns></returns>
        public static ImageAlbum GetNextImages(ImageAlbum img, Class cls)
        {
            List<ImageAlbum> lresult = ImageAlbumView.GetModelList(string.Format("classID={0} and ID>{1} order by ID Asc", cls.ID, img.ID), 1);
            if (lresult.Count == 0)
            {
                return null;
            }
            else
            {
                return lresult.First();
            }
        }

        /// <summary>
        /// 获取下一个提问
        /// </summary>
        /// <param name="img"></param>
        /// <param name="cls"></param>
        /// <returns></returns>
        public static Question GetNextQuestion(Question qs, Class cls)
        {
            List<Question> lresult = QuestionView.GetModelList(string.Format("classID={0} and ID>{1} order by ID Asc", cls.ID, qs.ID), 1);
            if (lresult.Count == 0)
            {
                return null;
            }
            else
            {
                return lresult.First();
            }
        }

        public static Book GetNextBook(Book b, Class cls)
        {
            List<Book> lresult = BookView.GetModelList(string.Format("classID={0} and ID>{1} order by ID Asc", cls.ID, b.ID), 1);
            if (lresult.Count == 0)
            {
                return null;
            }
            else
            {
                return lresult.First();
            }
        }

        /// <summary>
        /// 获取下一章节
        /// </summary>
        /// <param name="cp"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static BookChapter GetNextChapter(BookChapter cp, Book b)
        {
            List<BookChapter> lresult = BookChapterView.GetModelList(string.Format("BookID={0} and ID>{1} order by ID Asc", b.ID, cp.ID), 1);
            if (lresult.Count == 0)
            {
                return null;
            }
            else
            {
                return lresult.First();
            }
        }


        /// <summary>
        /// 获取快播下一集地址
        /// </summary>
        /// <param name="kuai"></param>
        /// <returns></returns>
        public static MovieUrlKuaib GetNextKuaibo(MovieUrlKuaib kuai)
        {
            List<MovieUrlKuaib> lresult = MovieUrlKuaibView.GetModelList(string.Format("MovieID={0} and id>{1} order by id asc", kuai.MovieID, kuai.Id));
            if (lresult.Count == 0)
            {
                return null;
            }
            else
            {
                return lresult.First();
            }
        }

        /// <summary>
        /// 获取百度下一集地址
        /// </summary>
        /// <param name="kuai"></param>
        /// <returns></returns>
        public static MovieUrlBaidu GetNextBaidu(MovieUrlBaidu kuai)
        {
            List<MovieUrlBaidu> lresult = MovieUrlBaiduView.GetModelList(string.Format("MovieID={0} and id>{1} order by id asc", kuai.MovieID, kuai.Id));
            if (lresult.Count == 0)
            {
                return null;
            }
            else
            {
                return lresult.First();
            }
        }

        /// <summary>
        /// 获取下一个单集
        /// </summary>
        /// <param name="drama"></param>
        /// <returns></returns>
        public static MovieDrama GetNextDrama(MovieDrama drama)
        {
            List<MovieDrama> lresult = MovieDramaView.GetModelList(string.Format("MovieID={0} and id>{1} order by id asc", drama.MovieID, drama.Id));
            if (lresult.Count == 0)
            {
                return null;
            }
            else
            {
                return lresult.First();
            }
        }

        #endregion

        #region 上传文件
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public static Result UpLoadFile(HttpPostedFile file, int ClassID)
        {
            Result r = new Result();
            SysSetting ss = BasePage.SystemSetting;

            string FileName = file.FileName.GetFileNameFromPath();//文件名
            string ExtName = file.FileName.GetFileExtNameFromPath();//扩展名
            string NewName = @string.GetGuid() + ExtName;//新文件名

            if (!ExtName.Replace(".", "").IsInArray(ss.FileExtNameFilter.Split(',')))
            {
                r.Success = false;
                r.Text = "不允许上传此类文件";
                return r;
            }
            if (file.ContentLength > ss.MaxPostFileSize)
            {
                r.Success = false;
                r.Text = "文件太大";
                return r;
            }

            string Folder = ss.FileDir + "/" + DateTime.Now.ToString("yyyy-MM-dd") + "/";//文件目录
            string FolderShotCut = Folder + "ShortCut/";//缩略图目录

            string FilePath = Folder + NewName;//文件路径
            string FilePath_ShortCut = FolderShotCut + NewName;//缩略图路径

            file.SaveAs(System.Web.HttpContext.Current.Server.MapPath(FilePath), true);

            Voodoo.Model.File f = new Voodoo.Model.File();

            if (ExtName.ToLower().Replace(".", "").IsInArray("jpg,jpeg,png,gif,bmp".Split(',')))
            {
                ImageHelper.MakeThumbnail(System.Web.HttpContext.Current.Server.MapPath(FilePath), System.Web.HttpContext.Current.Server.MapPath(FilePath_ShortCut), 105, 118, "Cut");
            }
            else
            {
                FilePath_ShortCut = "";
                f.FileType = 1;
            }
            FileInfo savedFile = new FileInfo(System.Web.HttpContext.Current.Server.MapPath(FilePath));

           

            f.FileDirectory = ss.FileDir;
            f.FileExtName = ExtName;
            f.FilePath = FilePath;
            f.FileSize = (savedFile.Length / 1024).ToInt32();
            //f.FileType=
            f.SmallPath = FilePath_ShortCut;
            f.UpTime = DateTime.Now;


            FileView.Insert(f);

            r.Success = true;
            r.Text = FilePath;
            return r;
        }
        #endregion 

        #region 创建系统关键词
        /// <summary>
        /// 创建系统关键词
        /// </summary>
        /// <param name="ModelID"></param>
        /// <param name="key"></param>
        public static void InsertKeyWords(int ModelID, string key)
        {
            SysKeyword k = SysKeywordView.Find(string.Format("ModelID={0} and Keyword=N'{1}'",ModelID,key));
            if (k.Id <= 0)
            {
                //不存在
                k.ModelID = ModelID;
                k.Keyword = key;
                k.ClickCount = 0;
                SysKeywordView.Insert(k);
            }
            else
            {
                k.ClickCount++;
                SysKeywordView.Update(k);
            }
        }

        #endregion

        #region 生成随机背景颜色
        /// <summary>
        /// 生成随机背景颜色
        /// </summary>
        /// <returns></returns>
        public static string RandomBGColor()
        {
            string[] str = new string[] { "#DDD", 
                "RGB(164,200,264)",
                "rgb(254,204,68)",
                "rgb(55,149,210)",
                "rgb(167,190,14)",
                "rgb(255,175,53)",
                "rgb(92,192,205)",
                "rgb(195,195,195)",
                "rgb(239,139,112)",
                "rgb(153,198,35)",
                "rgb(170,106,173)",
                "rgb(242,181,31)"
            };

            return str[@int.GetRandomNumber(0, str.Length - 1)];
        }
        #endregion 生成随机背景颜色
    }
}
