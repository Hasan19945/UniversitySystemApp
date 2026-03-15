using System;
using System.Collections.Generic;
using System.Linq;
using UniversitySystemApp.Models;

namespace UniversitySystemApp.Services
{
    /*
     * UniversitySystem contains the main business logic of the application.
     * 
     * Responsibilities:
     * - store users, courses, books, and loans
     * - create courses
     * - enroll students
     * - search courses
     * - search books
     * - borrow and return books
     * - print loan information
     * 
     * This keeps Program.cs cleaner because the logic is separated from the menu.
     */
    public class UniversitySystem
    {
        public List<Student> Students { get; set; }
        public List<Staff> StaffMembers { get; set; }
        public List<Course> Courses { get; set; }
        public List<Book> Books { get; set; }
        public List<Loan> Loans { get; set; }

        public UniversitySystem()
        {
            Students = new List<Student>();
            StaffMembers = new List<Staff>();
            Courses = new List<Course>();
            Books = new List<Book>();
            Loans = new List<Loan>();

            SeedData();
        }

        /*
         * SeedData adds some sample data.
         * 
         * This allows quick testing without manually entering everything.
         */
        private void SeedData()
        {
            Students.Add(new Student("S1001", "Hasan", "hasan@email.com"));
            Students.Add(new Student("S1002", "Ali", "ali@email.com"));

            Students.Add(new ExchangeStudent(
                "S2001",
                "Elif",
                "elif@email.com",
                "University of Oslo",
                "Norway",
                new ExchangePeriod("2026-01-01", "2026-06-01")));

            StaffMembers.Add(new Staff("A1001", "Kari", "kari@email.com", "Librarian", "Library"));

            Courses.Add(new Course("CS101", "Programming", 10, 2));
            Courses.Add(new Course("DB202", "Databases", 10, 3));

            Books.Add(new Book("B1", "Clean Code", "Robert C. Martin", 2008, 2));
            Books.Add(new Book("B2", "Design Patterns", "GoF", 1994, 1));
        }

        /*
         * Creates a new course if the course code is unique.
         */
        public void CreateCourse(string code, string name, int credits, int maxStudents)
        {
            Course existingCourse = Courses.FirstOrDefault(c =>
                c.Code.Equals(code, StringComparison.OrdinalIgnoreCase));

            if (existingCourse != null)
            {
                Console.WriteLine("This course code already exists.");
                return;
            }

            Course course = new Course(code, name, credits, maxStudents);
            Courses.Add(course);

            Console.WriteLine("Course created successfully.");
        }

        /*
         * Enrolls a student in a course.
         * 
         * Checks:
         * - student exists
         * - course exists
         * - course has capacity
         * - student is not already enrolled
         */
        public void EnrollStudentInCourse(string studentId, string courseCode)
        {
            Student student = Students.FirstOrDefault(s =>
                s.Id.Equals(studentId, StringComparison.OrdinalIgnoreCase));

            Course course = Courses.FirstOrDefault(c =>
                c.Code.Equals(courseCode, StringComparison.OrdinalIgnoreCase));

            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            if (!course.HasCapacity())
            {
                Console.WriteLine("Course is full.");
                return;
            }

            if (course.Students.Any(s => s.Id.Equals(studentId, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Student is already enrolled in this course.");
                return;
            }

            course.Students.Add(student);
            student.EnrolledCourses.Add(course.Code);

            Console.WriteLine("Student enrolled successfully.");
        }

        /*
         * Removes a student from a course.
         * 
         * The assignment text includes this functionality,
         * so it is implemented here even if it is not directly in the menu.
         */
        public void UnenrollStudentFromCourse(string studentId, string courseCode)
        {
            Student student = Students.FirstOrDefault(s =>
                s.Id.Equals(studentId, StringComparison.OrdinalIgnoreCase));

            Course course = Courses.FirstOrDefault(c =>
                c.Code.Equals(courseCode, StringComparison.OrdinalIgnoreCase));

            if (student == null || course == null)
            {
                Console.WriteLine("Student or course not found.");
                return;
            }

            Student enrolledStudent = course.Students.FirstOrDefault(s =>
                s.Id.Equals(studentId, StringComparison.OrdinalIgnoreCase));

            if (enrolledStudent == null)
            {
                Console.WriteLine("Student is not enrolled in this course.");
                return;
            }

            course.Students.Remove(enrolledStudent);
            student.EnrolledCourses.Remove(course.Code);

            Console.WriteLine("Student unenrolled successfully.");
        }

        /*
         * Prints all courses and the participants in each course.
         */
        public void PrintCoursesAndParticipants()
        {
            if (Courses.Count == 0)
            {
                Console.WriteLine("No courses found.");
                return;
            }

            foreach (var course in Courses)
            {
                Console.WriteLine(course);

                if (course.Students.Count == 0)
                {
                    Console.WriteLine("  No students enrolled.");
                }
                else
                {
                    foreach (var student in course.Students)
                    {
                        Console.WriteLine($"  - {student.Id} | {student.Name}");
                    }
                }

                Console.WriteLine();
            }
        }

        /*
         * Searches for courses by code or name.
         */
        public void SearchCourse(string query)
        {
            var results = Courses.Where(c =>
                c.Code.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                c.Name.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();

            if (results.Count == 0)
            {
                Console.WriteLine("No matching courses found.");
                return;
            }

            foreach (var course in results)
            {
                Console.WriteLine(course);
            }
        }

        /*
         * Searches for books by id or title.
         */
        public void SearchBook(string query)
        {
            var results = Books.Where(b =>
                b.Id.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                b.Title.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();

            if (results.Count == 0)
            {
                Console.WriteLine("No matching books found.");
                return;
            }

            foreach (var book in results)
            {
                Console.WriteLine(book);
            }
        }

        /*
         * Borrows a book for a user.
         * 
         * A user can be:
         * - Student
         * - Staff
         * 
         * Checks:
         * - user exists
         * - book exists
         * - book has available copies
         */
        public void BorrowBook(string userId, string bookId)
        {
            User borrower = Students.FirstOrDefault(s =>
                s.Id.Equals(userId, StringComparison.OrdinalIgnoreCase));

            if (borrower == null)
            {
                borrower = StaffMembers.FirstOrDefault(s =>
                    s.Id.Equals(userId, StringComparison.OrdinalIgnoreCase));
            }

            if (borrower == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            Book book = Books.FirstOrDefault(b =>
                b.Id.Equals(bookId, StringComparison.OrdinalIgnoreCase));

            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            if (book.AvailableCopies <= 0)
            {
                Console.WriteLine("No copies available.");
                return;
            }

            Loan loan = new Loan(borrower, book);
            Loans.Add(loan);
            book.AvailableCopies--;

            Console.WriteLine("Book borrowed successfully.");
        }

        /*
         * Returns a borrowed book.
         * 
         * The method finds the active loan for:
         * - the given user
         * - the given book
         * 
         * Then it sets the return date and increases available copies.
         */
        public void ReturnBook(string userId, string bookId)
        {
            Loan loan = Loans.FirstOrDefault(l =>
                l.Borrower.Id.Equals(userId, StringComparison.OrdinalIgnoreCase) &&
                l.Book.Id.Equals(bookId, StringComparison.OrdinalIgnoreCase) &&
                l.IsActive());

            if (loan == null)
            {
                Console.WriteLine("Active loan not found.");
                return;
            }

            loan.ReturnDate = DateTime.Now;
            loan.Book.AvailableCopies++;

            Console.WriteLine("Book returned successfully.");
        }

        /*
         * Prints all active loans.
         */
        public void PrintActiveLoans()
        {
            var activeLoans = Loans.Where(l => l.IsActive()).ToList();

            Console.WriteLine("Active Loans:");

            if (activeLoans.Count == 0)
            {
                Console.WriteLine("No active loans.");
                return;
            }

            foreach (var loan in activeLoans)
            {
                Console.WriteLine(loan);
            }
        }

        /*
         * Prints all returned loans.
         * This acts as the loan history.
         */
        public void PrintLoanHistory()
        {
            var history = Loans.Where(l => !l.IsActive()).ToList();

            Console.WriteLine("Loan History:");

            if (history.Count == 0)
            {
                Console.WriteLine("No returned loans.");
                return;
            }

            foreach (var loan in history)
            {
                Console.WriteLine(loan);
            }
        }
    }
}