using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TechShopCFAPI.Migrations;

namespace TechShopCFAPI.Models
{
    public class TechShopDbContext : DbContext
    {
        public TechShopDbContext() : base("name=TechShopDbContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TechShopDbContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //BuyingAgent
            modelBuilder.Entity<BuyingAgent>().Property(p => p.Id)
                                              .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                                              .IsRequired();

            modelBuilder.Entity<BuyingAgent>().Property(p => p.FullName).HasColumnType("varchar");
            modelBuilder.Entity<BuyingAgent>().Property(p => p.UserName).HasColumnType("varchar");
            modelBuilder.Entity<BuyingAgent>().Property(p => p.ProfilePic).HasColumnType("varchar");
            modelBuilder.Entity<BuyingAgent>().Property(p => p.Password).HasColumnType("varchar");
            modelBuilder.Entity<BuyingAgent>().Property(p => p.Email).HasColumnType("varchar");
            modelBuilder.Entity<BuyingAgent>().Property(p => p.Phone).HasColumnType("varchar");
            modelBuilder.Entity<BuyingAgent>().Property(p => p.Address).HasColumnType("varchar");
            modelBuilder.Entity<BuyingAgent>().Property(p => p.Salary).HasColumnType("decimal").HasPrecision(18,1);
            modelBuilder.Entity<BuyingAgent>().Property(p => p.JoiningDate).HasColumnType("datetime");
            modelBuilder.Entity<BuyingAgent>().Property(p => p.LastUpdated).HasColumnType("datetime");



            //OldProduct
            modelBuilder.Entity<OldProduct>().Property(p => p.Id)
                                              .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                                              .IsRequired();
            modelBuilder.Entity<OldProduct>().Property(p => p.CustomerId).HasColumnType("int");
            modelBuilder.Entity<OldProduct>().Property(p => p.ProductName).HasColumnType("varchar");
            modelBuilder.Entity<OldProduct>().Property(p => p.ProductDescription).HasColumnType("text");
            modelBuilder.Entity<OldProduct>().Property(p => p.Status).HasColumnType("varchar");
            modelBuilder.Entity<OldProduct>().Property(p => p.BuyingPrice).HasColumnType("decimal").HasPrecision(18, 1);
            modelBuilder.Entity<OldProduct>().Property(p => p.SellingPrice).HasColumnType("decimal").HasPrecision(18, 1);
            modelBuilder.Entity<OldProduct>().Property(p => p.Category).HasColumnType("varchar");
            modelBuilder.Entity<OldProduct>().Property(p => p.Brand).HasColumnType("varchar");
            modelBuilder.Entity<OldProduct>().Property(p => p.Features).HasColumnType("text");
            modelBuilder.Entity<OldProduct>().Property(p => p.Quantity).HasColumnType("int");
            modelBuilder.Entity<OldProduct>().Property(p => p.Images).HasColumnType("varchar");
            modelBuilder.Entity<OldProduct>().Property(p => p.Discount).HasColumnType("int");
            modelBuilder.Entity<OldProduct>().Property(p => p.Tax).HasColumnType("int");

            //PurchaseLog
            modelBuilder.Entity<PurchaseLog>().Property(p => p.Id)
                                              .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                                              .IsRequired();
            modelBuilder.Entity<PurchaseLog>().Property(p => p.CustomerId).HasColumnType("int");
            modelBuilder.Entity<PurchaseLog>().Property(p => p.ProductName).HasColumnType("varchar");
            modelBuilder.Entity<PurchaseLog>().Property(p => p.ProductDescription).HasColumnType("text");
            modelBuilder.Entity<PurchaseLog>().Property(p => p.Status).HasColumnType("varchar");
            modelBuilder.Entity<PurchaseLog>().Property(p => p.BuyingPrice).HasColumnType("decimal").HasPrecision(18, 1);
            modelBuilder.Entity<PurchaseLog>().Property(p => p.Category).HasColumnType("varchar");
            modelBuilder.Entity<PurchaseLog>().Property(p => p.Brand).HasColumnType("varchar");
            modelBuilder.Entity<PurchaseLog>().Property(p => p.Features).HasColumnType("text");
            modelBuilder.Entity<PurchaseLog>().Property(p => p.Quantity).HasColumnType("int");
            modelBuilder.Entity<PurchaseLog>().Property(p => p.Images).HasColumnType("varchar");
            modelBuilder.Entity<PurchaseLog>().Property(p => p.PurchasedDate).HasColumnType("datetime");
        }
        public DbSet<BuyingAgent> BuyingAgents  { set; get; }
        public DbSet<OldProduct> OldProducts { set; get; }
        public DbSet<PurchaseLog> PurchaseLogs { set; get; }
    }
}