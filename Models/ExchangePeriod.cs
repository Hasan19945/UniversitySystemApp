using System;

namespace UniversitySystemApp.Models
{
    public struct ExchangePeriod
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public ExchangePeriod(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }

        public override string ToString()
        {
            return $"{From:yyyy-MM-dd} - {To:yyyy-MM-dd}";
        }
    }
}