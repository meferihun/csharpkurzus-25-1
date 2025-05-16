using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Borrow_App
{
    public class User
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public List<BookInfo> BorrowedBookInfos { get; private set; }

        public User(int id, string name)
        {
            Id = id;
            Name = name;
            BorrowedBookInfos = new();
        }

        public void Borrow(Book book)
        {
            book.Borrow();
            BorrowedBookInfos.Add(book.Info);
        }

        public void Return(Book book)
        {
            book.Return();
            BorrowedBookInfos.Remove(book.Info);
        }

        public string Print()
        {
            return $"(ID: {Id}) - {Name} - Borrowed Books: {string.Join(", ", BorrowedBookInfos.Select(b => b.Title))}";
        }
    }


}
