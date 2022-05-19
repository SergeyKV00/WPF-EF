using System;

namespace WPF_EF.Models
{
    public class MoneyIncome
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public decimal Remainder { get; set; }
    }
}
