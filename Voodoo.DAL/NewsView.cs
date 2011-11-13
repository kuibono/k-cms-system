using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Voodoo.Model;


namespace Voodoo.DAL
{
    public partial class NewsView
    {
        /// <summary>
        /// 获取新闻的栏目
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        public static Class GetNewsClass(News news)
        {
            Class c = ClassView.GetModelByID(news.ClassID.ToString());
            news.Class = c;
            return c;
        }
    }
}
