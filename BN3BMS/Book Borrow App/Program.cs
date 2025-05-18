using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using static System.Reflection.Metadata.BlobBuilder;

namespace Book_Borrow_App
{
    internal class Program
    {
        private static List<Book> books;
        private static List<User> users;
       

        static void Main()
        {

            

            books = FileManager.LoadBooks().Select(info => new Book(info)).ToList();
            users = FileManager.LoadUsers();

            var menuActions = new Dictionary<string, Action>
                {
                    {"Library Book Borrow", () => { } },
                    { "Manage Books", () => ManageBooks(books) },
                    { "Manage Users", () => ManageUsers(users) },
                    { "Exit", () => Environment.Exit(0) }
                };

            var print = new Print(menuActions);
            print.PrintMenu();
        }

        private static void ManageBooks(List<Book> books)
        {
            var booksString = books.Select(book => book.Print()).ToList();
            booksString.Insert(0, "Manage Books");

            var menuActions = new Dictionary<string, Action>();
            for (int i = 0; i <= books.Count; i++)
            {
                int bookIndex = i - 1;
                menuActions[booksString.ElementAt(i)] = () =>
                {
                    if (bookIndex >= 0)
                        OpenBookInfoPanel(books.ElementAt(bookIndex));
                };
            }
            menuActions["Back to Main Menu"] = () => Main();

            var print = new Print(menuActions);
            print.PrintMenu();
        }

        private static void ManageUsers(List<User> users)
        {
            var usersString = users.Select(user => user.Print()).ToList();
            usersString.Insert(0, "Manage Users");
            var menuActions = new Dictionary<string, Action>();
            for (int i = 0; i <= users.Count; i++)
            {
                int userIndex = i - 1;
                menuActions[usersString.ElementAt(i)] = () =>
                {
                    if (userIndex >= 0)
                        OpenUserInfoPanel(users.ElementAt(userIndex));
                };
            }
            menuActions["Back to Main Menu"] = () => Main();

            var print = new Print(menuActions);
            print.PrintMenu();
        }

        private static void OpenBookInfoPanel(Book book)
        {
            var bookList = new List<string> { book.InfoPanel() };
            bookList.Insert(0, "Book");

            var menuActions = bookList.ToDictionary(book => book, act => (Action)null);
            menuActions["Back to Main Menu"] = () => Main();
            var print = new Print(menuActions);
            print.PrintMenu();
        }

        private static void OpenUserInfoPanel(User user)
        {
            var userList = new List<string> { user.InfoPanel() };
            userList.Insert(0, "User");

            var menuActions = userList.ToDictionary(user => user, act => (Action)null);
            menuActions["Manage User Books"] = () => ManageUserBooks(user);
            menuActions["Back to Main Menu"] = () => Main();

            var print = new Print(menuActions);
            print.PrintMenu();
        }

        private static void ManageUserBooks(User user)
        {
            var books = user.BorrowedBooks.ToList();

            var menuActions = new Dictionary<string, Action>();
            menuActions.Add(user.Name, null);

            for (int i = 0; i < books.Count; i++)
            {
                int bookIndex = i;
                var book = books.ElementAt(bookIndex);
                menuActions[book.Print()] = null;
            }
            menuActions.Add("Borrow Book", () =>
            {
                var bookList = FileManager.LoadBooks().Select(info => new Book(info)).Where(b => !b.IsBorrowed).ToList();
                var bookActions = new Dictionary<string, Action>();
                bookActions.Add("Borrow Book", null);
                for (int i = 0; i < bookList.Count; i++)
                {
                    int bookIndex = i;
                    var book = bookList.ElementAt(bookIndex);
                    bookActions[book.Print()] = () => ManageBookBorrowing(user, book);
                }
                bookActions.Add("Back to User Menu", () => ManageUserBooks(user));
                var print = new Print(bookActions);
                print.PrintMenu();
            });
            menuActions.Add("Back to Main Menu", () => Main());

            var print = new Print(menuActions);
            print.PrintMenu();
        }

        private static void ManageBookBorrowing(User user, Book book)
        {
            user.Borrow(ref book);
            book.Info.borrowedBy = user;

            FileManager.SaveUsers(users);
            FileManager.SaveBooks(books.Select(b => b.Info).ToList());

            var menuActions = new Dictionary<string, Action>() { { "Book Borrowed", null }, { "Back to User Menu", () => ManageUserBooks(user) } };
            var print = new Print(menuActions);
            print.PrintMenu();
        }
    }
}
