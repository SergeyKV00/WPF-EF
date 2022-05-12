using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_EF.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public Order? Order { get; set; }

        public MoneyIncome? Income { get; set; }

        public decimal Sum { get; set; }
    }
}
