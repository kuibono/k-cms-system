using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Voodoo.Html.Public
{
    public class Index : PageCreaterBase
    {
        

        public override string GetWebPath(object Model)
        {
            return base.GetWebPath(Model);
        }

        protected override string GetTemplateString()
        {
            throw new NotImplementedException();
        }
    }
}
