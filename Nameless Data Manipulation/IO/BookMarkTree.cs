using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSE_Framework.IO
{
    [Serializable()]
    public class BookMarkTree
    {
        public string Name;
        public List<BookMarkTree> ChildTrees;
        public List<BookMark> BookMarks;

        public BookMarkTree Parent = null;

        public void AddBookMark(BookMark bookMark)
        {
            bookMark.Parent = this;
            this.BookMarks.Add(bookMark);           
        }

        public void AddTree(BookMarkTree tree)
        {
            tree.Parent = this;
            this.ChildTrees.Add(tree);
        }

        public BookMarkTree(string Name)
        {
            this.Name = Name;
            this.ChildTrees = new List<BookMarkTree>();
            this.BookMarks = new List<BookMark>();
        }
    }
}
