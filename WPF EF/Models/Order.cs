using System;
using System.ComponentModel.DataAnnotations;

namespace WPF_EF.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [ConcurrencyCheck]
        public decimal Sum { get; set; }

        public decimal SumPay { get; set; }
    }
}
