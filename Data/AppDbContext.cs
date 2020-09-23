﻿using Microsoft.AspNet.Identity.EntityFramework;
using Models.Entities;
using System.Data.Entity;

namespace Data
{
    //Enable-Migrations
    //Add-Migration Init
    //Update-Database
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext() : base("SqlServerConnection")
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Message> Messages { get; set; }
        public DbSet<SystemConfig> SystemConfigs { get; set; }



        public static AppDbContext Create()
        {
            return new AppDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<IdentityUserClaim>().ToTable("AppUserClaims").HasKey(x => x.Id);
            builder.Entity<IdentityUserClaim>().ToTable("AppRoleClaims").HasKey(x => x.Id);
            builder.Entity<IdentityUserLogin>().ToTable("AppUserLogins").HasKey(x => x.UserId);
            builder.Entity<IdentityUserRole>().ToTable("AppUserRoles").HasKey(x => new { x.RoleId, x.UserId });

            builder.Entity<Message>()
                .HasRequired(m => m.UserReceived)
                .WithMany(t => t.MessagesReceived)
                .HasForeignKey(m => m.UserReceivedId)
                .WillCascadeOnDelete(false);

            builder.Entity<Contact>()
                .HasRequired(m => m.ContactReceivedRequest)
                .WithMany(t => t.ContactReceived)
                .HasForeignKey(m => m.ContactReceivedId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(builder);
        }
    }
}