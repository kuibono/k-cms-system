using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Voodoo.Model;

namespace Voodoo.DAL
{
    public static class ClassExtend
    {
        /// <summary>
        /// 获取栏目子项数量
        /// </summary>
        /// <param name="cls"></param>
        /// <returns></returns>
        public static int CountSubItem(this Class cls)
        {
            if (cls.ModelID == 1)
            {
                return NewsView.Count(string.Format("ClassID={0} and Audit=1", cls.ID));
            }
            else if (cls.ModelID == 2)
            {
                return ImageAlbumView.Count(string.Format("ClassID={0}", cls.ID));
            }
            else if (cls.ModelID == 3)
            {
                return QuestionView.Count(string.Format("ClassID={0}", cls.ID));
            }
            else if (cls.ModelID == 4)
            {
                return BookView.Count(string.Format("ClassID={0}", cls.ID));
            }
            else
            {
                return 0;
            }
        }

        public static Class GetClass(this ImageAlbum album)
        {
            return ClassView.GetModelByID(album.ClassID.ToS());
        }

        public static Class GetClass(this Question qs)
        {
            return ClassView.GetModelByID(qs.ClassID.ToS());
        }
    }
}
