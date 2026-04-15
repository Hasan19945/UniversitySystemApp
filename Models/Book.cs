namespace UniversitySystemApp.Models
{
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

        public bool CanBorrow()
        {
            return AvailableCopies > 0;
        }

        public void BorrowCopy()
        {
            if (AvailableCopies > 0)
            {
                AvailableCopies--;
            }
        }

        public void ReturnCopy()
        {
            if (AvailableCopies < NumberOfCopies)
            {
                AvailableCopies++;
            }
        }

        public override string ToString()
        {
            return $"{Id} - {Title} - {Author} - {Year} - Available: {AvailableCopies}/{NumberOfCopies}";
        }
    }
}