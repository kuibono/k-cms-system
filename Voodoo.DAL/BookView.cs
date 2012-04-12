using System;
using System.Text;
using System.Data;
using System.Linq;
using System.Data.Common;
using System.Collections.Generic;
using Voodoo;
using Voodoo.Model;
using Voodoo.Data;
using Voodoo.Data.DbHelper;
using Voodoo.Setting;
namespace Voodoo.DAL
{
    public partial class BookView
    {
        public static Class GetClass(Book b)
        {
            return ClassView.GetModelByID(b.ClassID.ToS());
        }

        public static Class GetClass(BookChapter bc)
        {
            return ClassView.GetModelByID(bc.ClassID.ToS());
        }

        public static Book GetBook(BookChapter bc)
        {
            return BookView.GetModelByID(bc.BookID.ToS());
        }

        public static void SaveBookRoles(List<BookRole> roles)
        {
            foreach (var r in roles)
            {
                var newR = BookRoleView.Find(string.Format("RoleName='{0}' and BookID={1}", r.RoleName, r.BookID.ToS()));
                if (newR.Id > 0)
                {
                    BookRoleView.Update(r);
                }
                else
                {
                    BookRoleView.Insert(r);
                }
            }
        }
    }
}
