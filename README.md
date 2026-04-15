# University System Console Application

This project is a C# console application that simulates a simple university system.
The goal of the project is to demonstrate object-oriented programming (OOP) principles such as inheritance, abstraction, and separation of concerns, while also implementing a basic login system and role-based functionality.

## Features

The application manages three main parts of a university system:

### Users

The system supports different types of users:

- Student
- Exchange Student
- Staff (Teacher / Librarian)

Exchange students inherit from the Student class and include additional information such as home university, country, and exchange period.

The system also includes:

- Login functionality (username and password)
- Registration for new students
- Role-based access (different menus for students, teachers, and librarians)

### Courses

The course system allows:

- Creating new courses (by teachers)
- Enrolling and unenrolling students
- Preventing duplicate course registration
- Preventing students from enrolling in the same course multiple times
- Searching for courses by code or name
- Assigning grades to students
- Adding curriculum (pensum) to courses
- Viewing course participants

### Library

The library system supports:

- Registering new books (by librarians)
- Searching for books
- Borrowing and returning books
- Preventing borrowing when no copies are available
- Viewing active loans
- Viewing loan history

Both students and staff can borrow and return books.




### Error Handling

The application includes basic error handling to improve stability:

- Input validation using TryParse
- Null checks to avoid runtime errors
- Validation for duplicate data (courses, users, enrollments)
- Clear feedback messages when operations fail


### Unit Testing

The project includes 4 unit tests that verify important functionality:

- Prevent creating duplicate courses
- Prevent enrolling a student in the same course twice
- Ensure borrowing a book reduces available copies
- Ensure returning a book increases available copies

These tests help ensure that the core logic of the system works as expected.


## Project Structure

The project is organized into separate folders to keep the code clean and structured:

Models/
    Contains data models such as User, Student, Course, Book, and Loan.

Services/
    Contains the main business logic (UniversitySystem class).

Interfaces/
    Contains interfaces such as IUserAuthentication.

Program.cs
    Handles the console menu and user interaction.

This structure separates responsibilities and makes the code easier to read, maintain, and extend

   ## Running the Application

Run the program using: dotnet run
You will be presented with a menu where you can:

- Log in as an existing user
- Register a new student
- Perform actions based on your role (student, teacher, or librarian)

### Notes

This project was developed as part of an assignment and focuses on demonstrating core programming concepts rather than building a complete production system.