using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using CookComputing.XmlRpc;

using Voodoo;
using Voodoo.Model;
using Voodoo.Net;
using Voodoo.IO;


namespace Voodoo.Basement.Client
{
   
    public class RpcBookHelper
    {

        public void test()
        {
            IMath im = XmlRpcProxyGen.Create<IMath>();
            im.Url = "http://www.fuck.com/e/api/xmlrpcV2.aspx";
            var r = im.SearchBook("极品仙府", "", "");
            
        }
    }



    public interface IMath : IXmlRpcProxy
    {
        [XmlRpcMethod("SearchBook")]
        [return: XmlRpcReturnValue(Description = "返回两个值加法的结果")]
        string SearchBook(string a,string b,string c);
    }
}
