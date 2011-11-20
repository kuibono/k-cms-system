using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Voodoo;
using Voodoo.Model;
using Voodoo.DAL;
using Voodoo.Security;


namespace Voodoo.Basement
{
    public class NewsAction
    {
        /// <summary>
        /// 系统中的所有栏目
        /// </summary>
        public static List<Voodoo.Model.Class> NewsClass
        {
            get
            {
                if (Voodoo.Cache.Cache.GetCache("_NewClassList") == null)
                {
                    Cache.Cache.SetCache("_NewClassList", ClassView.GetModelList(), 10);
                }
                return (List<Voodoo.Model.Class>)Voodoo.Cache.Cache.GetCache("_NewClassList");
            }
        }

        public static List<Voodoo.Model.Zt> NewsZt
        {
            get
            {
                if (Voodoo.Cache.Cache.GetCache("_NewsZtList") == null)
                {
                    Cache.Cache.SetCache("_NewsZtList", ZtView.GetModelList(), 10);
                }
                return (List<Voodoo.Model.Zt>)Voodoo.Cache.Cache.GetCache("_NewsZtList");
            }

        }

        #region  用户投稿
        /// <summary>
        /// 用户投稿
        /// </summary>
        /// <param name="news"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static Result UserPost(News news, User user)
        {
            Result r = new Result();

            UserGroup g = UserGroupView.GetModelByID(user.Group.ToS());

            int maxPost = 0;
            try
            {
                maxPost = g.MaxPost;
            }
            catch { }


            //验证用户是否允许投稿
            if (maxPost <= 0)
            {
                r.Success = false;
                r.Text = "对不起，您没有投稿的权限！";
                return r;
            }

            //验证本日投稿数是否已经过多
            int todayPost = NewsView.Count(string.Format("AutorID={0}", user.ID));
            if (todayPost > maxPost && news.ID <= 0)
            {
                r.Success = false;
                r.Text = "对不起，您本日投稿数量已经达到最大限制，请明天继续！";
                return r;
            }

            //验证标题是否为空
            if (news.Title.IsNullOrEmpty())
            {
                r.Success = false;
                r.Text = "文章标题不能为空";
                return r;
            }

            //验证栏目
            if (news.ClassID <= 0)
            {
                r.Success = false;
                r.Text = "栏目不能为空！";
                return r;
            }

            news.Audit = g.PostAotuAudit;
            news.AutorID = user.ID;
            if (news.Author.IsNullOrEmpty())
            {
                news.Author = user.UserName;
            }

            if (news.ID > 0)
            {
                NewsView.Update(news);
            }
            else
            {
                NewsView.Insert(news);
            }



            user.Cent += NewsView.GetNewsClass(news).PostAddCent;
            UserView.Update(user);

            r.Success = true;
            r.Text = "投递成功！";

            return r;
        }
        #endregion

    }
}
