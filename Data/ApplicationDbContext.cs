using Expense_Tracker.Models;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    Title = "Test",
                    Icon = "test-icon",
                    Type = "Expense"
                },
                new Category
                {
                    CategoryId = 2,
                    Title = "Test1",
                    Icon = "test1-icon",
                    Type = "Expense"
                },
                new Category
                {
                    CategoryId = 3,
                    Title = "Test2",
                    Icon = "test2-icon",
                    Type = "Expense"
                }
            );
        }
    }
}

