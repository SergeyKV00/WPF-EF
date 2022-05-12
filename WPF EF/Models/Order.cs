using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_EF.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public decimal SumPay { get; set; }
    }
}
