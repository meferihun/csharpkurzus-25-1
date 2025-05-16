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
                string json = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
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
                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                return JsonSerializer.Deserialize<List<T>>(json, options) ?? new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba betöltés közben: {ex.Message}");
                return new List<T>();
            }
        }

        public static List<BookInfo> LoadBooks() => Load<BookInfo>(BooksPath);
        public static void SaveBooks(List<BookInfo> books) => Save(books, BooksPath);

        public static List<User> LoadUsers() => Load<User>(UsersPath);
        public static void SaveUsers(List<User> users) => Save(users, UsersPath);
    }
}

