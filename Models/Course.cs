using System.Collections.Generic;

namespace UniversitySystemApp.Models
{
    /*
     * Course represents a university course.
     * 
     * Required data:
     * - Code
     * - Name
     * - Credits
     * - MaxStudents
     * 
     * We also store the list of enrolled students.
     */
    public class Course
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public int MaxStudents { get; set; }

        public List<Student> Students { get; set; }

        public Course(string code, string name, int credits, int maxStudents)
        {
            Code = code;
            Name = name;
            Credits = credits;
            MaxStudents = maxStudents;
            Students = new List<Student>();
        }

        /*
         * Returns true if there is still space in the course.
         */
        public bool HasCapacity()
        {
            return Students.Count < MaxStudents;
        }

        public override string ToString()
        {
            return $"{Code} - {Name} - {Credits} credits - {Students.Count}/{MaxStudents} students";
        }
    }
}