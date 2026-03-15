using System;
using UniversitySystemApp.Models;
using UniversitySystemApp.Services;

namespace UniversitySystemApp
{
    /*
     * Program.cs handles the console menu and user interaction.
     * 
     * It does not contain the main business logic.
     * Instead, it calls methods from the UniversitySystem service class.
     * 
     * This separation makes the program easier to read and maintain.
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            UniversitySystem system = new UniversitySystem();
            bool running = true;

            while (running)
            {
                PrintMenu();

                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        CreateCourse(system);
                        break;

                    case "2":
                        EnrollStudent(system);
                        break;

                    case "3":
                        system.PrintCoursesAndParticipants();
                        break;

                    case "4":
                        SearchCourse(system);
                        break;

                    case "5":
                        SearchBook(system);
                        break;

                    case "6":
                        BorrowBook(system);
                        break;

                    case "7":
                        ReturnBook(system);
                        break;

                    case "8":
                        RegisterBook(system);
                        break;

                    case "9":
                        system.PrintActiveLoans();
                        break;

                    case "10":
                        system.PrintLoanHistory();
                        break;

                    case "0":
                        running = false;
                        Console.WriteLine("Program is closing...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }

                Console.WriteLine();
            }
        }

        /*
         * Prints the main menu.
         * 
         * The original assignment requires options 1-8 and 0.
         * I also included:
         * - [9] Active loans
         * - [10] Loan history
         * because the assignment says the system should show them.
         */
        static void PrintMenu()
        {
            Console.WriteLine("====================================");
            Console.WriteLine("University System");
            Console.WriteLine("====================================");
            Console.WriteLine("[1] Opprett kurs");
            Console.WriteLine("[2] Meld student til kurs");
            Console.WriteLine("[3] Print kurs og deltagere");
            Console.WriteLine("[4] Søk på kurs");
            Console.WriteLine("[5] Søk på bok");
            Console.WriteLine("[6] Lån bok");
            Console.WriteLine("[7] Returner bok");
            Console.WriteLine("[8] Registrer bok");
            Console.WriteLine("[9] Vis aktive lån");
            Console.WriteLine("[10] Vis lånehistorikk");
            Console.WriteLine("[0] Avslutt");
        }

        /*
         * Reads course data from the user and creates a new course.
         */
        static void CreateCourse(UniversitySystem system)
        {
            Console.Write("Course code: ");
            string code = Console.ReadLine();

            Console.Write("Course name: ");
            string name = Console.ReadLine();

            Console.Write("Credits: ");
            int credits = int.Parse(Console.ReadLine());

            Console.Write("Max students: ");
            int maxStudents = int.Parse(Console.ReadLine());

            system.CreateCourse(code, name, credits, maxStudents);
        }

        /*
         * Reads student ID and course code,
         * then tries to enroll the student in the course.
         */
        static void EnrollStudent(UniversitySystem system)
        {
            Console.Write("Student ID: ");
            string studentId = Console.ReadLine();

            Console.Write("Course code: ");
            string courseCode = Console.ReadLine();

            system.EnrollStudentInCourse(studentId, courseCode);
        }

        /*
         * Reads a search query and searches for courses.
         */
        static void SearchCourse(UniversitySystem system)
        {
            Console.Write("Search course by code or name: ");
            string query = Console.ReadLine();

            system.SearchCourse(query);
        }

        /*
         * Reads a search query and searches for books.
         */
        static void SearchBook(UniversitySystem system)
        {
            Console.Write("Search book by id or title: ");
            string query = Console.ReadLine();

            system.SearchBook(query);
        }

        /*
         * Reads user ID and book ID,
         * then tries to borrow the selected book.
         */
        static void BorrowBook(UniversitySystem system)
        {
            Console.Write("User ID: ");
            string userId = Console.ReadLine();

            Console.Write("Book ID: ");
            string bookId = Console.ReadLine();

            system.BorrowBook(userId, bookId);
        }

        /*
         * Reads user ID and book ID,
         * then tries to return the selected book.
         */
        static void ReturnBook(UniversitySystem system)
        {
            Console.Write("User ID: ");
            string userId = Console.ReadLine();

            Console.Write("Book ID: ");
            string bookId = Console.ReadLine();

            system.ReturnBook(userId, bookId);
        }

        /*
         * Registers a new book in the system.
         * 
         * A Book object is created and directly added to the Books list.
         * This works well for this assignment.
         */
        static void RegisterBook(UniversitySystem system)
        {
            Console.Write("Book ID: ");
            string id = Console.ReadLine();

            Console.Write("Title: ");
            string title = Console.ReadLine();

            Console.Write("Author: ");
            string author = Console.ReadLine();

            Console.Write("Year: ");
            int year = int.Parse(Console.ReadLine());

            Console.Write("Number of copies: ");
            int copies = int.Parse(Console.ReadLine());

            system.Books.Add(new Book(id, title, author, year, copies));
            Console.WriteLine("Book registered successfully.");
        }
    }
}