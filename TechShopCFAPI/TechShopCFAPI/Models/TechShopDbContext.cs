using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TechShopCFAPI.Migrations;

namespace TechShopCFAPI.Models
{
    public class TechShopDbContext:DbContext
    {
        public TechShopDbContext() : base("name=TechShopDbContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TechShopDbContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //admin
            modelBuilder.Entity<Admin>().Property(p => p.Id)
                                                                       .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                                                                       .IsRequired();

            modelBuilder.Entity<Admin>().Property(p => p.FullName).HasColumnType("varchar");
            modelBuilder.Entity<Admin>().Property(p => p.UserName).HasColumnType("varchar");
            modelBuilder.Entity<Admin>().Property(p => p.ProfilePic).HasColumnType("varchar");
            modelBuilder.Entity<Admin>().Property(p => p.Email).HasColumnType("varchar");
            modelBuilder.Entity<Admin>().Property(p => p.Phone).HasColumnType("varchar");
            modelBuilder.Entity<Admin>().Property(p => p.Address).HasColumnType("varchar");


            //Credential

            modelBuilder.Entity<Credential>().Property(p => p.UserName).HasColumnType("varchar");
            modelBuilder.Entity<Credential>().Property(p => p.Password).HasColumnType("varchar");
            modelBuilder.Entity<Credential>().Property(p => p.Email).HasColumnType("varchar");


            //Products
            modelBuilder.Entity<Product>().Property(p => p.ProductName).HasColumnType("varchar");
            modelBuilder.Entity<Product>().Property(p => p.ProductDescription).HasColumnType("text");
            modelBuilder.Entity<Product>().Property(p => p.Status).HasColumnType("varchar");
            modelBuilder.Entity<Product>().Property(p => p.BuyingPrice).HasColumnType("decimal").HasPrecision(18, 1);
            modelBuilder.Entity<Product>().Property(p => p.SellingPrice).HasColumnType("decimal").HasPrecision(18, 1);
            modelBuilder.Entity<Product>().Property(p => p.Category).HasColumnType("varchar");
            modelBuilder.Entity<Product>().Property(p => p.Brand).HasColumnType("varchar");
            modelBuilder.Entity<Product>().Property(p => p.Features).HasColumnType("text");
            modelBuilder.Entity<Product>().Property(p => p.Images).HasColumnType("varchar");
        }
        public DbSet<Admin> Admins { set; get; }
        public DbSet<Credential> Credentials { set; get; }
        public DbSet<Product> Products { set; get; }
    }
}