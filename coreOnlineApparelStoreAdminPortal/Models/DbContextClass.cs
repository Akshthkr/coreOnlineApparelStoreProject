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

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryName)
                .HasColumnName("categoryName").HasMaxLength(15).IsUnicode(false);
            });
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.Property(e => e.BrandName)
                .HasColumnName("BrandName").HasMaxLength(15).IsUnicode(false);
            });
            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.Property(e => e.VendorName)
                .HasColumnName("VendorName").HasMaxLength(15).IsUnicode(false);
            });
            //modelBuilder.Entity<Customer>(entity =>
            //{
            //    entity.Property(e => e.CustomerFirstName)
            //    .HasColumnName("CustomerFirstName").HasMaxLength(15).IsUnicode(false);
            //});
        }
    }
}
