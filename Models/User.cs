namespace UniversitySystemApp.Models
{
    /*
     * User is the abstract base class for all users in the system.
     * 
     * Both Student and Staff share these common properties:
     * - Id
     * - Name
     * - Email
     * 
     * This is a good use of inheritance because it avoids duplicated code.
     */
    public abstract class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        protected User(string id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public override string ToString()
        {
            return $"{Id} - {Name} - {Email}";
        }
    }
}