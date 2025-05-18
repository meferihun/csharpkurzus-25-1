using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualBasic;

namespace Book_Borrow_App
{
    public class User
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Sex { get; private set; }
        public List<Book> BorrowedBooks { get; private set; }

        public User() { }
        public User(int id, string name, string sex, List<Book> books)
        {
            Id = id;
            Name = name;
            Sex = sex;
            BorrowedBooks = books;
        }
        public User(int id, string name, string sex)
        {
            Id = id;
            Name = name;
            Sex = sex;
            BorrowedBooks = new();
        }

        public void Borrow(ref Book book)
        {
            book.Borrow();
            BorrowedBooks.Add(book);
        }

        public void Return(ref Book book)
        {
            book.Return();
            BorrowedBooks.Remove(book);
        }

        public string Print()
        {
            return $"(ID: {Id}) - {Name}";
        }

        public string InfoPanel()
        {
            return $"ID: {Id}\n" +
                $"Name: {Name}\n" +
                $"Sex: {(Sex.Equals('M') ? "Male" : "Female")}\n" +
                $"Borrowed Books: {string.Join(", ", BorrowedBooks.Select(b => b.Info.Title))}\n";
        }
    }


}
