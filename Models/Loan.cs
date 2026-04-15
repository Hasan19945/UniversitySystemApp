using System;

namespace UniversitySystemApp.Models
{
    public class Loan
    {
        public User Borrower { get; set; }
        public Book Book { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public Loan(User borrower, Book book)
        {
            Borrower = borrower;
            Book = book;
            BorrowDate = DateTime.Now;
            ReturnDate = null;
        }

        public bool IsActive()
        {
            return ReturnDate == null;
        }

        public void ReturnBook()
        {
            ReturnDate = DateTime.Now;
        }

        public override string ToString()
        {
            string status = ReturnDate == null
                ? "Active"
                : $"Returned: {ReturnDate}";

            return $"{Borrower.Name} borrowed \"{Book.Title}\" on {BorrowDate}. Status: {status}";
        }
    }
}