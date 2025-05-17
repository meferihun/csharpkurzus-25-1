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
        public string BirthDate { get; private set; }
        public char Sex { get; private set; }
        public List<Book> BorrowedBooks { get; private set; }

        public User(int id, string name)
        {
            Id = id;
            Name = name;
            BorrowedBooks = new();
        }

        public void Borrow(Book book)
        {
            book.Borrow();
            BorrowedBooks.Add(book);
        }

        public void Return(Book book)
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
                $"\tName: {Name}\n" +
                $"\tBirth Date: {BirthDate}\n" +
                $"\tSex: {(Sex.Equals('M') ? "Male" : "Female")}\n" +
                $"\tBorrowed Books: {string.Join(", ", BorrowedBooks.Select(b => b.Info.Title))}";
        }
    }


}
