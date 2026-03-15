namespace UniversitySystemApp.Models
{
    /*
     * Book represents a library item.
     * 
     * Required properties:
     * - Id
     * - Title
     * - Author
     * - Year
     * - NumberOfCopies
     * 
     * AvailableCopies is used to check whether borrowing is possible.
     */
    public class Book
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public int NumberOfCopies { get; set; }
        public int AvailableCopies { get; set; }

        public Book(string id, string title, string author, int year, int numberOfCopies)
        {
            Id = id;
            Title = title;
            Author = author;
            Year = year;
            NumberOfCopies = numberOfCopies;
            AvailableCopies = numberOfCopies;
        }

        public override string ToString()
        {
            return $"{Id} - {Title} - {Author} - {Year} - Available: {AvailableCopies}/{NumberOfCopies}";
        }
    }
}