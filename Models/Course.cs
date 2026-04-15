using System;
using System.Collections.Generic;
using System.Linq;

namespace UniversitySystemApp.Models
{
    public class Course
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public int MaxStudents { get; set; }
        public Staff? Teacher { get; set; }
        public string? Curriculum { get; set; }
        public List<CourseEnrollment> Enrollments { get; set; }

        public Course(string code, string name, int credits, int maxStudents, Staff? teacher = null)
        {
            Code = code;
            Name = name;
            Credits = credits;
            MaxStudents = maxStudents;
            Teacher = teacher;
            Curriculum = null;
            Enrollments = new List<CourseEnrollment>();
        }

        public bool HasCapacity()
        {
            return Enrollments.Count < MaxStudents;
        }

        public bool HasStudent(string studentId)
        {
            return Enrollments.Any(e => e.Student.Id.Equals(studentId, StringComparison.OrdinalIgnoreCase));
        }

        public bool EnrollStudent(Student student)
        {
            if (!HasCapacity())
            {
                return false;
            }

            if (HasStudent(student.Id))
            {
                return false;
            }

            Enrollments.Add(new CourseEnrollment(student));
            student.EnrollInCourse(Code);
            return true;
        }

        public bool UnenrollStudent(string studentId)
        {
            CourseEnrollment? enrollment = Enrollments.FirstOrDefault(e =>
                e.Student.Id.Equals(studentId, StringComparison.OrdinalIgnoreCase));

            if (enrollment == null)
            {
                return false;
            }

            enrollment.Student.UnenrollFromCourse(Code);
            Enrollments.Remove(enrollment);
            return true;
        }

        public bool SetGrade(string studentId, string grade)
        {
            CourseEnrollment? enrollment = Enrollments.FirstOrDefault(e =>
                e.Student.Id.Equals(studentId, StringComparison.OrdinalIgnoreCase));

            if (enrollment == null)
            {
                return false;
            }

            enrollment.Grade = grade;
            enrollment.Student.SetGrade(Code, grade);
            return true;
        }

        public override string ToString()
        {
            string teacherName = Teacher == null ? "No teacher" : Teacher.Name;
            return $"{Code} - {Name} - {Credits} credits - Teacher: {teacherName} - {Enrollments.Count}/{MaxStudents} students";
        }
    }
}