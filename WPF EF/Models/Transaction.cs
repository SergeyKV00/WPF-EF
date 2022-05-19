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
