using WPF_EF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Windows;

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

        public static void CreateMoneyIncome(decimal sum)
        {
            using (AppDbContext db = new AppDbContext())
            {
                MoneyIncome moneyIncome = new MoneyIncome { Sum = sum, Remainder = sum, Date = DateTime.Now };
                db.Incomes.Add(moneyIncome);
                db.SaveChanges();
            }
        }

        public static void OrderPayment(decimal sum, MoneyIncome moneyIncome, Order order)
        {
            using (AppDbContext db = new AppDbContext())
            {
                var _income = db.Incomes.FirstOrDefault(x => x.Id == moneyIncome.Id);
                var _order = db.Orders.FirstOrDefault(x => x.Id == order.Id);
                Transaction transaction = new Transaction { Income = _income, Order = _order, Sum = sum };

                try
                {
                    db.Transactions.Add(transaction);
                    db.SaveChanges();
                    MessageBox.Show($"Номер заказа: {order.Id}\nСумма платежа: {sum}", "Платеж завершен", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show(ex.InnerException.Message, "Платеж невозможен", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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

        public static List<Transaction> GetAllTransactions()
        {
            List<Transaction> result = new List<Transaction>();
            using (AppDbContext db = new AppDbContext())
            {
                result = db.Transactions.Include(o => o.Order).Include(i => i.Income).ToList();
            }

            return result;
        }
    }
}
