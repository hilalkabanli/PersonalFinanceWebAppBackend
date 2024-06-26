using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceWebApp.Models.Entities;

namespace PersonalFinanceWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
      

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Transfer> Transfers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        // Configure Account - User relationship
        modelBuilder.Entity<Account>()
            .HasOne(a => a.User)
            .WithMany(u => u.Accounts)
            .HasForeignKey(a => a.UserID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Accounts)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserID)
            .OnDelete(DeleteBehavior.NoAction);

        // Configure Transaction - Account relationship
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Account)
            .WithMany(a => a.Transactions)
            .HasForeignKey(t => t.AccountID)
            .OnDelete(DeleteBehavior.NoAction);

        // Configure Transfer - SenderAccount relationship
        modelBuilder.Entity<Transfer>()
            .HasOne(t => t.SenderAccount)
            .WithMany(a => a.SentTransfers)
            .HasForeignKey(t => t.SenderAccountID)
            .OnDelete(DeleteBehavior.NoAction);

        // Configure Transfer - ReceiverAccount relationship
        modelBuilder.Entity<Transfer>()
            .HasOne(t => t.ReceiverAccount)
            .WithMany(a => a.ReceivedTransfers)
            .HasForeignKey(t => t.ReceiverAccountID)
            .OnDelete(DeleteBehavior.NoAction);

        base.OnModelCreating(modelBuilder);
        }
    }
}