using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Voodoo.Model;

namespace Web.e.api
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IClientService”。
    [ServiceContract]
    interface IClientService
    {
        [OperationContract]

        List<Book> BookSearch(string str_sql);

        bool BookExist(string str_sql);

        Book BookFind(string str_sql);

        Book GetBookByID(int id);

        void BookInsert(Book book);

        void BookUpdate(Book book);

        void BookDelete(string str_sql);


        List<Class> ClassSearch(string str_sql);

        Class ClassFind(string str_sql);

        Class GetClassByID(int id);

        void ClassInsert(Class cls);

        void ClassUpdate(Class cls);

        void ClassDelete(string str_sql);



        List<BookChapter> ChapterSearch(string str_sql);

        bool ChapterExist(string str_sql);

        BookChapter ChapterFind(string str_sql);

        BookChapter GetChapterByID(long id);

        void ChapterInsert(BookChapter chapter, string Content);

        void ChapterUpdate(BookChapter chapter, string Content);

        void ChapterDelete(string str_sql);


    }
}
