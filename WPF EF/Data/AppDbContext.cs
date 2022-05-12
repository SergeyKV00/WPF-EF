using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WPF_EF.Models;

namespace WPF_EF.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order>? Orders { get; set; }

        public DbSet<MoneyIncome>? Incomes{ get; set; }

        public DbSet<Transaction>? Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=SHCHEM-THINKPAD; Initial Catalog=AppDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        } 
    }
}
