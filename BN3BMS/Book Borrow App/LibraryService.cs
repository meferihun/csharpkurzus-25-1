using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Borrow_App
{
    public class LibraryService(LibraryRepository repository)
    {
        private readonly LibraryRepository repository = repository;

        public void BorrowBook(int userId, int bookId)
        {
            var user = repository.GetUserById(userId);
            var book = repository.GetBookById(bookId);

            if (user != null && book != null && !book.IsBorrowed)
            {
                user.Borrow(book);
                Console.WriteLine($"{user.Name} kikölcsönözte a könyvet: {book.Info.Title}");
            }
            else
            {
                Console.WriteLine("Nem sikerült a kölcsönzés.");
            }
        }

        public void ReturnBook(int userId, int bookId)
        {
            var user = repository.GetUserById(userId);
            var book = repository.GetBookById(bookId);

            if (user != null && book != null && book.IsBorrowed)
            {
                user.Return(book);
                Console.WriteLine($"{user.Name} visszahozta a könyvet: {book.Info.Title}");
            }
            else
            {
                Console.WriteLine("Nem sikerült a visszahozás.");
            }
        }
    }

}
