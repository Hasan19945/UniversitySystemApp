namespace UniversitySystemApp.Models
{
    public class CourseEnrollment
    {
        public Student Student { get; set; }
        public string? Grade { get; set; }

        public CourseEnrollment(Student student)
        {
            Student = student;
            Grade = null;
        }

        public override string ToString()
        {
            string gradeText = string.IsNullOrWhiteSpace(Grade) ? "No grade" : Grade;
            return $"{Student.Name} ({Student.Id}) - Grade: {gradeText}";
        }
    }
}