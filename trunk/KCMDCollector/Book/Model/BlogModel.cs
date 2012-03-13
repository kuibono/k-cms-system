using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KCMDCollector.Book
{
    /// <summary>
    /// 博客类型
    /// </summary>
    public enum BlogType
    {
        Sina,
        Sohu,
        Neasy,
        WordPress
    }

    public class BlogModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public BlogType type { get; set; }

        public string BlogName { get; set; }

        public string BlogUrl { get; set; }
    }
}
