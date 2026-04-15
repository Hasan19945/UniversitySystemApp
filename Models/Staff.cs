namespace UniversitySystemApp.Models
{
    public class Staff : User
    {
        public string Position { get; set; }
        public string Department { get; set; }

        public Staff(
            string id,
            string name,
            string email,
            string username,
            string password,
            UserRole role,
            string position,
            string department)
            : base(id, name, email, username, password, role)
        {
            Position = position;
            Department = department;
        }

        public override string ToString()
        {
            return $"Staff: {Id} - {Name} - {Email} - {Role} - {Position} - {Department}";
        }
    }
}