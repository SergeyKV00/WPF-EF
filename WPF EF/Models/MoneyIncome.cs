using System;
using System.ComponentModel.DataAnnotations;

namespace WPF_EF.Models
{
    public class MoneyIncome
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [ConcurrencyCheck]
        public decimal Sum { get; set; }

        public decimal Remainder { get; set; }
    }
}
