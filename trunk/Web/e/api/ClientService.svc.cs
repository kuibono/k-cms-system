using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Voodoo.Model;
using Voodoo.DAL;

namespace Web.e.api
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“ClientService”。
    public class ClientService : IClientService
    {
        public List<Book> BookSearch(string str_sql)
        {
            return BookView.GetModelList(str_sql);
        }

        public bool BookExist(string str_sql)
        {
            return BookView.Exist(str_sql); 
        }

        public Book BookFind(string str_sql)
        {
            return BookView.Find(str_sql);
        }

        public Book GetBookByID(int id)
        {
            return BookView.GetModelByID(id.ToString());
        }

        public void BookInsert(Book book)
        {
            BookView.Insert(book);
        }

        public void BookUpdate(Book book)
        {
            BookView.Update(book);
        }

        public void BookDelete(string str_sql)
        {
            BookView.Del(str_sql);
        }


        public List<Class> ClassSearch(string str_sql)
        {
            return ClassView.GetModelList(str_sql);
        }

        public Class ClassFind(string str_sql)
        {
            return ClassView.Find(str_sql);
        }

        public Class GetClassByID(int id)
        {
            return ClassView.GetModelByID(id.ToString());
        }

        public void ClassInsert(Class cls)
        {
            ClassView.Insert(cls);
        }

        public void ClassUpdate(Class cls)
        {
            ClassView.Update(cls);
        }

        public void ClassDelete(string str_sql)
        {
            ClassView.Del(str_sql);
        }



        public List<BookChapter> ChapterSearch(string str_sql)
        {
            return BookChapterView.GetModelList(str_sql);
        }

        public bool ChapterExist(string str_sql)
        {
            return BookChapterView.Exist(str_sql);

        }

        public BookChapter ChapterFind(string str_sql)
        {
            return BookChapterView.Find(str_sql);
        }

        public BookChapter GetChapterByID(long id)
        {
            return BookChapterView.GetModelByID(id.ToString());
        }

        public void ChapterInsert(BookChapter chapter, string Content)
        {
            BookChapterView.Insert(chapter);
            ///保存文件
        }

        public void ChapterUpdate(BookChapter chapter, string Content)
        {
            BookChapterView.Update(chapter);
            //保存文件
        }

        public void ChapterDelete(string str_sql)
        {
            //删除文件

            BookChapterView.Del(str_sql);

        }
    }
}
