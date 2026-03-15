using System.Collections.Generic;

namespace UniversitySystemApp.Models
{
    /*
     * Student inherits from User.
     * 
     * In addition to the common user properties,
     * a student has a list of enrolled course codes.
     */
    public class Student : User
    {
        public List<string> EnrolledCourses { get; set; }

        public Student(string id, string name, string email)
            : base(id, name, email)
        {
            EnrolledCourses = new List<string>();
        }

        public override string ToString()
        {
            return $"Student: {Id} - {Name} - {Email}";
        }
    }
}