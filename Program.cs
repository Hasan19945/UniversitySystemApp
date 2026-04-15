using System;
using System.Collections.Generic;
using UniversitySystemApp.Models;
using UniversitySystemApp.Services;

namespace UniversitySystemApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UniversitySystem system = new UniversitySystem();
            bool appRunning = true;

            while (appRunning)
            {
                Console.WriteLine("====================================");
                Console.WriteLine("University System");
                Console.WriteLine("====================================");
                Console.WriteLine("[1] Login");
                Console.WriteLine("[2] Register student");
                Console.WriteLine("[0] Exit");
                Console.Write("Choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out int startChoice))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                switch (startChoice)
                {
                    case 1:
                        Login(system);
                        break;

                    case 2:
                        RegisterStudent(system);
                        break;

                    case 0:
                        appRunning = false;
                        Console.WriteLine("Program is closing...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void Login(UniversitySystem system)
        {
            Console.Write("Username: ");
            string username = Console.ReadLine() ?? "";

            Console.Write("Password: ");
            string password = Console.ReadLine() ?? "";

            User? user = system.Login(username, password);

            if (user == null)
            {
                Console.WriteLine("Login failed.");
                return;
            }

            Console.WriteLine($"Welcome, {user.Name} ({user.Role})");

            switch (user.Role)
            {
                case UserRole.Student:
                    StudentMenu(system, (Student)user);
                    break;

                case UserRole.Teacher:
                    TeacherMenu(system, (Staff)user);
                    break;

                case UserRole.Librarian:
                    LibrarianMenu(system, (Staff)user);
                    break;
            }
        }

        static void RegisterStudent(UniversitySystem system)
        {
            Console.Write("Student ID: ");
            string id = Console.ReadLine() ?? "";

            Console.Write("Name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";

            Console.Write("Username: ");
            string username = Console.ReadLine() ?? "";

            Console.Write("Password: ");
            string password = Console.ReadLine() ?? "";

            bool success = system.RegisterStudent(id, name, email, username, password);

            Console.WriteLine(success
                ? "Student registered successfully."
                : "Registration failed. ID or username may already exist.");
        }

        static void StudentMenu(UniversitySystem system, Student student)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("===== Student Menu =====");
                Console.WriteLine("[1] Enroll in course");
                Console.WriteLine("[2] Unenroll from course");
                Console.WriteLine("[3] View my courses");
                Console.WriteLine("[4] View my grades");
                Console.WriteLine("[5] Search book");
                Console.WriteLine("[6] Borrow book");
                Console.WriteLine("[7] Return book");
                Console.WriteLine("[0] Logout");
                Console.Write("Choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Write("Course code: ");
                        string enrollCode = Console.ReadLine() ?? "";

                        bool enrolled = system.EnrollStudentInCourse(student.Id, enrollCode);
                        Console.WriteLine(enrolled ? "Enrolled successfully." : "Could not enroll.");
                        break;

                    case 2:
                        Console.Write("Course code: ");
                        string unenrollCode = Console.ReadLine() ?? "";

                        bool unenrolled = system.UnenrollStudentFromCourse(student.Id, unenrollCode);
                        Console.WriteLine(unenrolled ? "Unenrolled successfully." : "Could not unenroll.");
                        break;

                    case 3:
                        List<Course> myCourses = system.GetCoursesForStudent(student.Id);
                        system.PrintCourses(myCourses);
                        break;

                    case 4:
                        Dictionary<string, string> grades = system.GetGradesForStudent(student.Id);

                        if (grades.Count == 0)
                        {
                            Console.WriteLine("No grades found.");
                        }
                        else
                        {
                            foreach (var grade in grades)
                            {
                                Console.WriteLine($"{grade.Key}: {grade.Value}");
                            }
                        }
                        break;

                    case 5:
                        Console.Write("Search book by id or title: ");
                        string bookQuery = Console.ReadLine() ?? "";
                        system.PrintBooks(system.SearchBook(bookQuery));
                        break;

                    case 6:
                        Console.Write("Book ID: ");
                        string borrowBookId = Console.ReadLine() ?? "";

                        bool borrowed = system.BorrowBook(student.Id, borrowBookId);
                        Console.WriteLine(borrowed ? "Book borrowed successfully." : "Could not borrow book.");
                        break;

                    case 7:
                        Console.Write("Book ID: ");
                        string returnBookId = Console.ReadLine() ?? "";

                        bool returned = system.ReturnBook(student.Id, returnBookId);
                        Console.WriteLine(returned ? "Book returned successfully." : "Could not return book.");
                        break;

                    case 0:
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void TeacherMenu(UniversitySystem system, Staff teacher)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("===== Teacher Menu =====");
                Console.WriteLine("[1] Create course");
                Console.WriteLine("[2] Search course");
                Console.WriteLine("[3] Search book");
                Console.WriteLine("[4] Borrow book");
                Console.WriteLine("[5] Return book");
                Console.WriteLine("[6] Set grade");
                Console.WriteLine("[7] Set curriculum");
                Console.WriteLine("[8] Print courses and participants");
                Console.WriteLine("[0] Logout");
                Console.Write("Choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Write("Course code: ");
                        string code = Console.ReadLine() ?? "";

                        Console.Write("Course name: ");
                        string name = Console.ReadLine() ?? "";

                        Console.Write("Credits: ");
                        if (!int.TryParse(Console.ReadLine(), out int credits))
                        {
                            Console.WriteLine("Invalid credits.");
                            break;
                        }

                        Console.Write("Max students: ");
                        if (!int.TryParse(Console.ReadLine(), out int maxStudents))
                        {
                            Console.WriteLine("Invalid max students.");
                            break;
                        }

                        bool created = system.CreateCourse(code, name, credits, maxStudents, teacher.Id);
                        Console.WriteLine(created ? "Course created successfully." : "Could not create course.");
                        break;

                    case 2:
                        Console.Write("Search course by code or name: ");
                        string courseQuery = Console.ReadLine() ?? "";
                        system.PrintCourses(system.SearchCourse(courseQuery));
                        break;

                    case 3:
                        Console.Write("Search book by id or title: ");
                        string bookQuery = Console.ReadLine() ?? "";
                        system.PrintBooks(system.SearchBook(bookQuery));
                        break;

                    case 4:
                        Console.Write("Book ID: ");
                        string borrowBookId = Console.ReadLine() ?? "";

                        bool borrowed = system.BorrowBook(teacher.Id, borrowBookId);
                        Console.WriteLine(borrowed ? "Book borrowed successfully." : "Could not borrow book.");
                        break;

                    case 5:
                        Console.Write("Book ID: ");
                        string returnBookId = Console.ReadLine() ?? "";

                        bool returned = system.ReturnBook(teacher.Id, returnBookId);
                        Console.WriteLine(returned ? "Book returned successfully." : "Could not return book.");
                        break;

                    case 6:
                        Console.Write("Course code: ");
                        string gradeCourseCode = Console.ReadLine() ?? "";

                        Console.Write("Student ID: ");
                        string gradeStudentId = Console.ReadLine() ?? "";

                        Console.Write("Grade: ");
                        string grade = Console.ReadLine() ?? "";

                        bool gradeSet = system.SetGrade(teacher.Id, gradeCourseCode, gradeStudentId, grade);
                        Console.WriteLine(gradeSet ? "Grade set successfully." : "Could not set grade.");
                        break;

                    case 7:
                        Console.Write("Course code: ");
                        string curriculumCourseCode = Console.ReadLine() ?? "";

                        Console.Write("Curriculum / Pensum: ");
                        string curriculum = Console.ReadLine() ?? "";

                        bool curriculumSet = system.SetCurriculum(teacher.Id, curriculumCourseCode, curriculum);
                        Console.WriteLine(curriculumSet ? "Curriculum updated successfully." : "Could not update curriculum.");
                        break;

                    case 8:
                        system.PrintCoursesAndParticipants();
                        break;

                    case 0:
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void LibrarianMenu(UniversitySystem system, Staff librarian)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("===== Librarian Menu =====");
                Console.WriteLine("[1] Register book");
                Console.WriteLine("[2] Search book");
                Console.WriteLine("[3] View active loans");
                Console.WriteLine("[4] View loan history");
                Console.WriteLine("[0] Logout");
                Console.Write("Choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Write("Book ID: ");
                        string id = Console.ReadLine() ?? "";

                        Console.Write("Title: ");
                        string title = Console.ReadLine() ?? "";

                        Console.Write("Author: ");
                        string author = Console.ReadLine() ?? "";

                        Console.Write("Year: ");
                        if (!int.TryParse(Console.ReadLine(), out int year))
                        {
                            Console.WriteLine("Invalid year.");
                            break;
                        }

                        Console.Write("Number of copies: ");
                        if (!int.TryParse(Console.ReadLine(), out int copies))
                        {
                            Console.WriteLine("Invalid number of copies.");
                            break;
                        }

                        bool success = system.RegisterBook(id, title, author, year, copies);
                        Console.WriteLine(success ? "Book registered successfully." : "Could not register book.");
                        break;

                    case 2:
                        Console.Write("Search book by id or title: ");
                        string bookQuery = Console.ReadLine() ?? "";
                        system.PrintBooks(system.SearchBook(bookQuery));
                        break;

                    case 3:
                        system.PrintActiveLoans();
                        break;

                    case 4:
                        system.PrintLoanHistory();
                        break;

                    case 0:
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }
}