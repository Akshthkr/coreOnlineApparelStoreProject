﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreOnlineApparelStoreAdminPortal.Models
{
    public class DbContextClass:DbContext
    {
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Customer> Customers{ get; set; }
        public DbSet<Order> Orders{ get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Cart> Carts{ get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Admin> Admins{ get; set; }

        public DbContextClass(DbContextOptions<DbContextClass> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //String ConString = "Data Source= TRD-502; Initial Catalog=DbOnlineApparelStore; Integrated Security=True;";
            //optionsBuilder.UseSqlServer(ConString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderProduct>(Build =>
            {
                Build.HasKey(t => new { t.OrderId, t.Productid});
            });
            modelBuilder.Entity<Cart>(Build =>
            {
                Build.HasKey(t => new { t.CustomerId, t.ProductId });
            });
            modelBuilder.Entity<Feedback>(Build =>
            {
                Build.HasKey(t => new { t.ProductId, t.OrderId});
            });
        }
    }
}
