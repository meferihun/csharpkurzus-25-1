using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Borrow_App
{
    public class LibraryRepository
    {
        private List<Book> books = new List<Book>();
        private List<User> users = new List<User>();

        public void AddBook(Book book) => books.Add(book);
        public void AddUser(User user) => users.Add(user);

        public Book GetBookById(int id) => books.FirstOrDefault(b => b.Info.Id == id);
        public User GetUserById(int id) => users.FirstOrDefault(u => u.Id == id);

        public List<Book> GetAllBooks() => books;
        public List<User> GetAllUsers() => users;
    }

}
