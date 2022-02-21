using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Enums;

namespace Technovert.BankApp.Services
{
    public class BankDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseMySQL("server=localhost;user=root;database=bankdb;port=3307;password=Abhiram@28");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bank>()
                .HasMany<Account>(b => b.Accounts)
                .WithOne(a => a.Banks);

            modelBuilder.Entity<Bank>().HasMany<Currency>(b => b.Currencies);

            modelBuilder.Entity<Bank>().HasOne(b => b.DefaultCurrency);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.SourceAccount)
                .WithMany(sa => sa.Transactions)
                .HasForeignKey(t => t.SourceAccountId)
                .OnDelete(DeleteBehavior.Restrict);

           

            //dummy
            Currency currency = new Currency
            {
                Name = "Rupee",
                Code = "INR",
                ExchangeRate = 1
            };
            modelBuilder.Entity<Currency>().HasData(currency);

            Bank b = new Bank
            {
                BankId = "abc",
                Name = "New Bank",
                DefaultCurrencyCode = currency.Code,
                CreatedBy = "admin",
                UpdatedBy = "admin",
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                RTGSToOther = 0.05m,
                RTGSToSame = 0.0m,
                IMPSToOther = 0.07m,
                IMPSToSame = 0.03m
            };
            modelBuilder.Entity<Bank>().HasData(b);


            Account account1 = new Account
            {
                AccountId = "abc",
                Name = "John Doe",
                BankId = b.BankId,
                Balance = 20m,
                Password = "1234",
                Gender = Gender.Male,
                Status = Status.Active,
                Type = AccountType.User
            };
            modelBuilder.Entity<Account>().HasData(account1);

        }
    }
}
