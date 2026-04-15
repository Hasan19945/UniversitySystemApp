namespace UniversitySystemApp.Models
{
    public class ExchangeStudent : Student
    {
        public string HomeUniversity { get; set; }
        public string Country { get; set; }
        public ExchangePeriod Period { get; set; }

        public ExchangeStudent(
            string id,
            string name,
            string email,
            string username,
            string password,
            string homeUniversity,
            string country,
            ExchangePeriod period)
            : base(id, name, email, username, password)
        {
            HomeUniversity = homeUniversity;
            Country = country;
            Period = period;
        }

        public override string ToString()
        {
            return $"Exchange Student: {Id} - {Name} - {Email} - {HomeUniversity} - {Country} - {Period}";
        }
    }
}