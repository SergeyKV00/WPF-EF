using WPF_EF.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WPF_EF.Models
{
    public static class DataWorker
    {
        // Create an order
        public static void CreateOrder(decimal sum)
        {
            using (AppDbContext db = new AppDbContext())
            {
                Order order = new Order { Sum = sum, SumPay = 0, Date = DateTime.Now };
                db.Orders.Add(order);
                db.SaveChanges();
            }
        }

        // Create a money income
        public static void CreateMoneyIncome(decimal sum, DateTime date)
        {
            using (AppDbContext db = new AppDbContext())
            {
                MoneyIncome moneyIncome = new MoneyIncome { Sum = sum, Remainder = sum, Date = date};
                db.Incomes.Add(moneyIncome);
                db.SaveChanges();
            }
        }

        // Get orders
        public static List<Order> GetAllOrders()
        {
            List<Order> result;
            using (AppDbContext db = new AppDbContext())
            {
                result = db.Orders.ToList();
            }
            return result;
        }

        // Get money incomes 
        public static List<MoneyIncome> GetAllIncomes()
        {
            List<MoneyIncome> result;
            using (AppDbContext db = new AppDbContext())
            {
                result = db.Incomes.ToList();
            }
            return result;
        }
    }
}
