namespace UniversitySystemApp.Models
{
    /*
     * ExchangeStudent inherits from Student.
     * 
     * This means an exchange student is still a student,
     * but with additional properties:
     * - HomeUniversity
     * - Country
     * - Period
     */
    public class ExchangeStudent : Student
    {
        public string HomeUniversity { get; set; }
        public string Country { get; set; }
        public ExchangePeriod Period { get; set; }

        public ExchangeStudent(
            string id,
            string name,
            string email,
            string homeUniversity,
            string country,
            ExchangePeriod period)
            : base(id, name, email)
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