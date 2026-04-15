using System.Collections.Generic;

namespace UniversitySystemApp.Models
{
    public class Student : User
    {
        public List<string> EnrolledCourses { get; set; }
        public Dictionary<string, string> Grades { get; set; }

        public Student(
            string id,
            string name,
            string email,
            string username,
            string password)
            : base(id, name, email, username, password, UserRole.Student)
        {
            EnrolledCourses = new List<string>();
            Grades = new Dictionary<string, string>();
        }

        public bool IsEnrolledIn(string courseCode)
        {
            return EnrolledCourses.Contains(courseCode);
        }

        public void EnrollInCourse(string courseCode)
        {
            if (!EnrolledCourses.Contains(courseCode))
            {
                EnrolledCourses.Add(courseCode);
            }
        }

        public void UnenrollFromCourse(string courseCode)
        {
            EnrolledCourses.Remove(courseCode);
        }

        public void SetGrade(string courseCode, string grade)
        {
            Grades[courseCode] = grade;
        }

        public override string ToString()
        {
            return $"Student: {Id} - {Name} - {Email} - Username: {Username}";
        }
    }
}