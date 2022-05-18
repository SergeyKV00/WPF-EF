using WPF_EF.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WPF_EF.Models
{
    public static class DataWorker
    { 
        public static void CreateOrder(decimal sum)
        {
            using (AppDbContext db = new AppDbContext())
            {
                Order order = new Order { Sum = sum, SumPay = 0, Date = DateTime.Now };
                db.Orders.Add(order);
                db.SaveChanges();
            }
        }

        public static void CreateMoneyIncome(decimal sum, DateTime date)
        {
            using (AppDbContext db = new AppDbContext())
            {
                MoneyIncome moneyIncome = new MoneyIncome { Sum = sum, Remainder = sum, Date = date};
                db.Incomes.Add(moneyIncome);
                db.SaveChanges();
            }
        }

        public static void OrderPayment(decimal sum, MoneyIncome moneyIncome, Order order)
        {
            using(AppDbContext db = new AppDbContext())
            {
                Transaction transaction = new Transaction { Income = moneyIncome, Order = order, Sum = sum };
                db.Transactions.Add(transaction);
                db.SaveChanges();
            }    
        }

        public static List<Order> GetAllOrders()
        {
            List<Order> result;
            using (AppDbContext db = new AppDbContext())
            {
                result = db.Orders.ToList();
            }
            return result;
        }

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
