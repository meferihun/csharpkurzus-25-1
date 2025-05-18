using System.Text.Json;

namespace Book_Borrow_App
{
    public static class FileManager
    {
        private static readonly string BooksPath = Path.Combine(AppContext.BaseDirectory, "books.json");
        private static readonly string UsersPath = Path.Combine(AppContext.BaseDirectory, "users.json");

        public static void Save<T>(List<T> list, string path)
        {
            try
            {
                string json;
                if (typeof(T) == typeof(BookInfo))
                {
                    var bookList = list
                        .Cast<BookInfo>()
                        .Select(b => new BookInfoDto
                        {
                            Id = b.Id,
                            Title = b.Title,
                            Author = b.Author,
                            Year = b.Year,
                            Genre = b.Genre,
                            BorrowedBy = b.borrowedBy?.Name
                        })
                        .ToList();
                    json = JsonSerializer.Serialize(bookList, new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                }
                else if (typeof(T) == typeof(User))
                {
                    var userList = list
                        .Cast<User>()
                        .Select(u => new UserDto
                        {
                            Id = u.Id,
                            Name = u.Name,
                            Sex = u.Sex,
                            BorrowedBookTitles = u.BorrowedBooks?.Select(b => b.Info.Title).ToList() ?? new List<string>()
                        })
                        .ToList();
                    json = JsonSerializer.Serialize(userList, new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                }
                else
                {
                    json = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                }
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba mentés közben: {ex.Message}");
            }
        }


        public static List<T> Load<T>(string path)
        {
            try
            {
                if (!File.Exists(path)) return new List<T>();
                using var json = File.OpenRead(path);

                if (typeof(T) == typeof(BookInfo))
                {
                    var bookDtos = JsonSerializer.Deserialize<List<BookInfoDto>>(json, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }) ?? new List<BookInfoDto>();
                    var books = bookDtos.Select(dto =>
                    {
                        var info = new BookInfo
                        {
                            Id = dto.Id,
                            Title = dto.Title,
                            Author = dto.Author,
                            Year = dto.Year,
                            Genre = dto.Genre,
                            borrowedBy = null
                        };
                        return info;
                    }).ToList();
                    return books.Cast<T>().ToList();
                }
                else if (typeof(T) == typeof(User))
                {
                    var userDtos = JsonSerializer.Deserialize<List<UserDto>>(json, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }) ?? new List<UserDto>();
                    var allBooks = LoadBooks().Select(info => new Book(info)).ToList();

                    var users = userDtos.Select(dto =>
                    {
                        var listOfBooks = allBooks.Where(b => dto.BorrowedBookTitles.Contains(b.Info.Title)).ToList();
                        var user = new User(dto.Id, dto.Name, dto.Sex, listOfBooks);
                        return user;
                    }).ToList();

                    return users.Cast<T>().ToList();
                }
                else
                {
                    var options = new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };
                    return JsonSerializer.Deserialize<List<T>>(json, options) ?? new List<T>();
                }
            }
            catch (Exception ex)
            {
                var menu = new Dictionary<string, Action>() { { $"Hiba betöltés közben: {ex.Message}", null } };
                var print = new Print(menu);
                print.PrintMenu();
                return new List<T>();
            }
        }

        public static List<BookInfo> LoadBooks() => Load<BookInfo>(BooksPath);
        public static void SaveBooks(List<BookInfo> books) => Save(books, BooksPath);

        public static List<User> LoadUsers() => Load<User>(UsersPath);
        public static void SaveUsers(List<User> users) => Save(users, UsersPath);
    }
}

