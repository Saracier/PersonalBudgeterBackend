using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBudgeterBackend
{
    internal class BudgeterContext : DbContext { 

        //public BudgeterContext(DbContextOptions<BudgeterContext> options) : base(options) { }

        public DbSet<Expense> Budgeter { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=BudgeterDb;Trusted_Connection=True;"
            );
        }
    }
}
