using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TechShopCFAPI.Models
{
    public class TechShopDbContext : DbContext
    {
        public TechShopDbContext() : base("name=TechShopDbContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<TechShopDbContext>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           // modelBuilder.Entity<Customer>().HasKey<int>(i => i.CustomerId).hasco;
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ShippingData> ShippingDatas { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}