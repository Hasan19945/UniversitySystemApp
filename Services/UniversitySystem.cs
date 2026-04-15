using System;
using System.Collections.Generic;
using System.Linq;
using UniversitySystemApp.Interfaces;
using UniversitySystemApp.Models;

namespace UniversitySystemApp.Services
{
    public class UniversitySystem : IUserAuthentication
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

        private void SeedData()
        {
            Students.Add(new Student("S1001", "Hasan", "hasan@email.com", "hasan", "1234"));
            Students.Add(new Student("S1002", "Ali", "ali@email.com", "ali", "1234"));

            Students.Add(new ExchangeStudent(
                "S2001",
                "Elif",
                "elif@email.com",
                "elif",
                "1234",
                "University of Oslo",
                "Norway",
                new ExchangePeriod(new DateTime(2026, 1, 1), new DateTime(2026, 6, 1))));

            Staff teacher = new Staff(
                "T1001",
                "Ola",
                "ola@email.com",
                "ola",
                "1234",
                UserRole.Teacher,
                "Teacher",
                "IT");

            Staff librarian = new Staff(
                "L1001",
                "Kari",
                "kari@email.com",
                "kari",
                "1234",
                UserRole.Librarian,
                "Librarian",
                "Library");

            StaffMembers.Add(teacher);
            StaffMembers.Add(librarian);

            Courses.Add(new Course("CS101", "Programming", 10, 2, teacher));
            Courses.Add(new Course("DB202", "Databases", 10, 3, teacher));

            Books.Add(new Book("B1", "Clean Code", "Robert C. Martin", 2008, 2));
            Books.Add(new Book("B2", "Design Patterns", "GoF", 1994, 1));
        }

        public User? Login(string username, string password)
        {
            User? student = Students.FirstOrDefault(s =>
                s.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                s.CheckPassword(password));

            if (student != null)
            {
                return student;
            }

            User? staff = StaffMembers.FirstOrDefault(s =>
                s.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                s.CheckPassword(password));

            return staff;
        }

        public bool UsernameExists(string username)
        {
            return Students.Any(s => s.Username.Equals(username, StringComparison.OrdinalIgnoreCase)) ||
                   StaffMembers.Any(s => s.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public bool RegisterStudent(string id, string name, string email, string username, string password)
        {
            if (Students.Any(s => s.Id.Equals(id, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            if (UsernameExists(username))
            {
                return false;
            }

            Students.Add(new Student(id, name, email, username, password));
            return true;
        }

        public bool RegisterStaff(
            string id,
            string name,
            string email,
            string username,
            string password,
            UserRole role,
            string position,
            string department)
        {
            if (role != UserRole.Teacher && role != UserRole.Librarian)
            {
                return false;
            }

            if (StaffMembers.Any(s => s.Id.Equals(id, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            if (UsernameExists(username))
            {
                return false;
            }

            StaffMembers.Add(new Staff(id, name, email, username, password, role, position, department));
            return true;
        }

        public Student? GetStudentById(string studentId)
        {
            return Students.FirstOrDefault(s =>
                s.Id.Equals(studentId, StringComparison.OrdinalIgnoreCase));
        }

        public Staff? GetStaffById(string staffId)
        {
            return StaffMembers.FirstOrDefault(s =>
                s.Id.Equals(staffId, StringComparison.OrdinalIgnoreCase));
        }

        public User? GetUserById(string userId)
        {
            User? student = Students.FirstOrDefault(s =>
                s.Id.Equals(userId, StringComparison.OrdinalIgnoreCase));

            if (student != null)
            {
                return student;
            }

            return StaffMembers.FirstOrDefault(s =>
                s.Id.Equals(userId, StringComparison.OrdinalIgnoreCase));
        }

        public Course? GetCourseByCode(string courseCode)
        {
            return Courses.FirstOrDefault(c =>
                c.Code.Equals(courseCode, StringComparison.OrdinalIgnoreCase));
        }

        public Book? GetBookById(string bookId)
        {
            return Books.FirstOrDefault(b =>
                b.Id.Equals(bookId, StringComparison.OrdinalIgnoreCase));
        }

        public bool RegisterBook(string id, string title, string author, int year, int copies)
        {
            if (Books.Any(b => b.Id.Equals(id, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            if (copies <= 0)
            {
                return false;
            }

            Books.Add(new Book(id, title, author, year, copies));
            return true;
        }

        public bool CreateCourse(string code, string name, int credits, int maxStudents, string teacherId)
        {
            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            if (Courses.Any(c => c.Code.Equals(code, StringComparison.OrdinalIgnoreCase) ||
                                 c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            Staff? teacher = GetStaffById(teacherId);

            if (teacher == null || teacher.Role != UserRole.Teacher)
            {
                return false;
            }

            Courses.Add(new Course(code, name, credits, maxStudents, teacher));
            return true;
        }

        public bool EnrollStudentInCourse(string studentId, string courseCode)
        {
            Student? student = GetStudentById(studentId);
            Course? course = GetCourseByCode(courseCode);

            if (student == null || course == null)
            {
                return false;
            }

            return course.EnrollStudent(student);
        }

        public bool UnenrollStudentFromCourse(string studentId, string courseCode)
        {
            Course? course = GetCourseByCode(courseCode);

            if (course == null)
            {
                return false;
            }

            return course.UnenrollStudent(studentId);
        }

        public bool SetGrade(string teacherId, string courseCode, string studentId, string grade)
        {
            Staff? teacher = GetStaffById(teacherId);
            Course? course = GetCourseByCode(courseCode);

            if (teacher == null || teacher.Role != UserRole.Teacher)
            {
                return false;
            }

            if (course == null)
            {
                return false;
            }

            if (course.Teacher == null || !course.Teacher.Id.Equals(teacherId, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return course.SetGrade(studentId, grade);
        }

        public bool SetCurriculum(string teacherId, string courseCode, string curriculum)
        {
            Staff? teacher = GetStaffById(teacherId);
            Course? course = GetCourseByCode(courseCode);

            if (teacher == null || teacher.Role != UserRole.Teacher)
            {
                return false;
            }

            if (course == null)
            {
                return false;
            }

            if (course.Teacher == null || !course.Teacher.Id.Equals(teacherId, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            course.Curriculum = curriculum;
            return true;
        }

        public List<Course> SearchCourse(string query)
        {
            query = query?.Trim() ?? "";

            return Courses.Where(c =>
                c.Code.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                c.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Book> SearchBook(string query)
        {
            query = query?.Trim() ?? "";

            return Books.Where(b =>
                b.Id.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                b.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public bool BorrowBook(string userId, string bookId)
        {
            User? borrower = GetUserById(userId);
            Book? book = GetBookById(bookId);

            if (borrower == null || book == null)
            {
                return false;
            }

            if (!book.CanBorrow())
            {
                return false;
            }

            bool alreadyBorrowed = Loans.Any(l =>
                l.Borrower.Id.Equals(userId, StringComparison.OrdinalIgnoreCase) &&
                l.Book.Id.Equals(bookId, StringComparison.OrdinalIgnoreCase) &&
                l.IsActive());

            if (alreadyBorrowed)
            {
                return false;
            }

            Loans.Add(new Loan(borrower, book));
            book.BorrowCopy();
            return true;
        }

        public bool ReturnBook(string userId, string bookId)
        {
            Loan? loan = Loans.FirstOrDefault(l =>
                l.Borrower.Id.Equals(userId, StringComparison.OrdinalIgnoreCase) &&
                l.Book.Id.Equals(bookId, StringComparison.OrdinalIgnoreCase) &&
                l.IsActive());

            if (loan == null)
            {
                return false;
            }

            loan.ReturnBook();
            loan.Book.ReturnCopy();
            return true;
        }

        public List<Loan> GetActiveLoans()
        {
            return Loans.Where(l => l.IsActive()).ToList();
        }

        public List<Loan> GetLoanHistory()
        {
            return Loans.Where(l => !l.IsActive()).ToList();
        }

        public List<Course> GetCoursesForStudent(string studentId)
        {
            return Courses.Where(c => c.HasStudent(studentId)).ToList();
        }

        public Dictionary<string, string> GetGradesForStudent(string studentId)
        {
            Student? student = GetStudentById(studentId);

            if (student == null)
            {
                return new Dictionary<string, string>();
            }

            return student.Grades;
        }

        public void PrintCoursesAndParticipants()
        {
            if (Courses.Count == 0)
            {
                Console.WriteLine("No courses found.");
                return;
            }

            foreach (Course course in Courses)
            {
                Console.WriteLine(course);

                if (course.Enrollments.Count == 0)
                {
                    Console.WriteLine("  No students enrolled.");
                }
                else
                {
                    foreach (CourseEnrollment enrollment in course.Enrollments)
                    {
                        Console.WriteLine($"  - {enrollment}");
                    }
                }
            }
        }

        public void PrintCourses(List<Course> courses)
        {
            if (courses.Count == 0)
            {
                Console.WriteLine("No matching courses found.");
                return;
            }

            foreach (Course course in courses)
            {
                Console.WriteLine(course);
            }
        }

        public void PrintBooks(List<Book> books)
        {
            if (books.Count == 0)
            {
                Console.WriteLine("No matching books found.");
                return;
            }

            foreach (Book book in books)
            {
                Console.WriteLine(book);
            }
        }

        public void PrintActiveLoans()
        {
            List<Loan> activeLoans = GetActiveLoans();

            Console.WriteLine("Active Loans:");
            if (activeLoans.Count == 0)
            {
                Console.WriteLine("No active loans.");
                return;
            }

            foreach (Loan loan in activeLoans)
            {
                Console.WriteLine(loan);
            }
        }

        public void PrintLoanHistory()
        {
            List<Loan> history = GetLoanHistory();

            Console.WriteLine("Loan History:");
            if (history.Count == 0)
            {
                Console.WriteLine("No returned loans.");
                return;
            }

            foreach (Loan loan in history)
            {
                Console.WriteLine(loan);
            }
        }
    }
}