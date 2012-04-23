using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Voodoo;
using Voodoo.Model;
using Voodoo.Data;
using Voodoo.Data.DbHelper;
using Voodoo.Setting;

namespace Voodoo.DAL
{
    public partial class MovieInfoView
    {
        public static Class GetClass(MovieInfo m)
        {
            return ClassView.GetModelByID(m.ClassID.ToS());
        }

        public static Class GetClass(MovieUrlBaidu mu)
        {
            return ClassView.GetModelByID(MovieInfoView.GetModelByID(mu.MovieID.ToS()).ClassID.ToS());
        }

        public static Class GetClass(MovieUrlKuaib mu)
        {
            return ClassView.GetModelByID(MovieInfoView.GetModelByID(mu.MovieID.ToS()).ClassID.ToS());
        }
    }
}
