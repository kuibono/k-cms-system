using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Voodoo.Model
{
    public partial class MovieInfo
    {
        public string Url { get; set; }

        //public List<MovieDrama> Dramas { get; set; }

        public List<MovieUrlBaidu> BaiduDramas { get; set; }

        public List<MovieUrlKuaib> KuaiboDramas { get; set; }
    }

    public partial class MovieUrlKuaib
    {
        public string PlayUrl { get; set; }

        public string SourceUrl { get; set; }

    }

    public partial class MovieUrlBaidu
    {
        public string PlayUrl { get; set; }

        public string SourceUrl { get; set; }
    }
}
