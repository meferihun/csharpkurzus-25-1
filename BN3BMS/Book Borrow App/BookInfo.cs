namespace Book_Borrow_App
{
    public record class BookInfo
    {
        public required int Id { get; init; }
        public required string Title { get; init; }
        public required string Author { get; init; }
        public required int Year { get; init; }
        public required string Genre { get; init; }
        public User borrowedBy { get; set; }


    }


}
