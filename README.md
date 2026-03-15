# University System Console Application

This project is a C# console application that simulates a simple university system.
The goal of the project is to demonstrate object-oriented programming (OOP) concepts such as inheritance, class design, and separation of concerns.

## Features

The application manages three main parts of a university system:

### Users

The system supports different user roles:

Student

Exchange Student

Staff

Exchange students inherit from the Student class and include additional information such as home university, country, and exchange period.

### Courses

The course system allows users to:

Create new courses

Enroll students in courses

Remove students from courses

List courses and participants

Search for courses by code or name

### Library

The library system supports:

Registering books

Searching for books

Borrowing books

Returning books

Viewing active loans and loan history

Loans are connected to users, meaning that both students and staff can borrow books

Project Structure

The project is organized into the following folders:

## Project Structure

The project is organized into the following folders:
Models/
    Contains the data models such as User, Student, Course, Book, and Loan.

Services/
    Contains the main business logic of the application (UniversitySystem class).

Program.cs
    Handles the console menu and user interaction.

    This structure separates data models, business logic, and user interface, which improves maintainability and readability.

   ## Running the Application

Run the program using: dotnet run
The application will display a menu where users can perform operations related to courses and the library.