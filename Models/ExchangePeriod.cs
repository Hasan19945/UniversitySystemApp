namespace UniversitySystemApp.Models
{
    /*
     * ExchangePeriod stores the start and end of an exchange stay.
     * 
     * A separate class makes the design cleaner and easier to understand.
     */
    public class ExchangePeriod
    {
        public string From { get; set; }
        public string To { get; set; }

        public ExchangePeriod(string from, string to)
        {
            From = from;
            To = to;
        }

        public override string ToString()
        {
            return $"{From} - {To}";
        }
    }
}