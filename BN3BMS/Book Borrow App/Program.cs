using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using static System.Reflection.Metadata.BlobBuilder;

namespace Book_Borrow_App
{
    internal class Program
    {
        static void Main()
        {
            List<Book> books = FileManager.LoadBooks().Select(info => new Book(info)).ToList();
            List<User> users = FileManager.LoadUsers();

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
                menuActions[booksString.ElementAt(i)] = () => OpenBookInfoPanel(books.ElementAt(bookIndex));
            }
            menuActions["Back to Main Menu"] = () => Main(); 

            var print = new Print(menuActions);
            print.PrintMenu();
        }

        private static void ManageUsers(List<User> users)
        {
            var usersString = users.Select(book => book.Print()).ToList();
            usersString.Insert(0, "Manage Users");
            var menuActions = new Dictionary<string, Action>();
            for (int i = 0; i <= users.Count; i++)
            {
                int userIndex = i - 1;
                menuActions[usersString.ElementAt(i)] = () => OpenUserInfoPanel(users.ElementAt(userIndex));
            }
            menuActions["Back to Main Menu"] = () => Main();

            var print = new Print(menuActions);
            print.PrintMenu();
        }

        private static void OpenBookInfoPanel(Book book)
        {
            var bookList = new List<string> { book.InfoPanel() };
            bookList.Insert(0, book.Info.Title);

            var menuActions = bookList.ToDictionary(book => book, act => (Action)(null));
            menuActions["Back to Main Menu"] = () => Main();
            var print = new Print(menuActions);
            print.PrintMenu();
        }

        private static void OpenUserInfoPanel(User user)
        {
            var userList = new List<string> { user.InfoPanel() };
            userList.Insert(0, user.Name);

            var menuActions = userList.ToDictionary(book => book, act => (Action)(null));
            menuActions["Manage User Books"] = () => ManageUserBooks(user);
            menuActions["Back to Main Menu"] = () => Main();

            var print = new Print(menuActions);
            print.PrintMenu();
        }

        private static void ManageUserBooks(User user)
        {
            var books = user.BorrowedBooks.Select(b => b).ToList();

            var menuActions = new Dictionary<string, Action>();
            menuActions.Add(user.Name, (Action) null);

            for (int i = 0; i < books.Count; i++)
            {
                int bookIndex = i;
                menuActions[books.ElementAt(i).Print()] = () => user.Return(books.ElementAt(bookIndex));
            }
            menuActions.Add("Borrow Book", () =>
            {
                var bookList = FileManager.LoadBooks().Select(info => new Book(info)).ToList();
                var bookActions = new Dictionary<string, Action>();
                bookActions.Add("Borrow Book", (Action)null);
                for (int i = 0; i < bookList.Count; i++)
                {
                    int bookIndex = i;
                    bookActions[bookList.ElementAt(i).Print()] = () => user.Borrow(bookList.ElementAt(bookIndex));
                }
                bookActions.Add("Back to User Menu", () => ManageUserBooks(user));
                var print = new Print(bookActions);
                print.PrintMenu();
            });
            menuActions.Add("Back to Main Menu", () => Main());

            var print = new Print(menuActions);
            print.PrintMenu();
        }
    }
}
