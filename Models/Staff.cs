namespace UniversitySystemApp.Models
{
    /*
     * Staff also inherits from User.
     * 
     * Staff members have:
     * - Position
     * - Department
     */
    public class Staff : User
    {
        public string Position { get; set; }
        public string Department { get; set; }

        public Staff(string id, string name, string email, string position, string department)
            : base(id, name, email)
        {
            Position = position;
            Department = department;
        }

        public override string ToString()
        {
            return $"Staff: {Id} - {Name} - {Email} - {Position} - {Department}";
        }
    }
}