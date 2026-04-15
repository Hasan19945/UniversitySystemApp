namespace UniversitySystemApp.Models
{
    public abstract class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

        protected User(
            string id,
            string name,
            string email,
            string username,
            string password,
            UserRole role)
        {
            Id = id;
            Name = name;
            Email = email;
            Username = username;
            Password = password;
            Role = role;
        }

        public bool CheckPassword(string password)
        {
            return Password == password;
        }

        public override string ToString()
        {
            return $"{Id} - {Name} - {Email} - {Role}";
        }
    }
}