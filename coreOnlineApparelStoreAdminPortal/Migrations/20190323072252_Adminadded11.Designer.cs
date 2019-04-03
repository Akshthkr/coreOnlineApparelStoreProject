﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using coreOnlineApparelStoreAdminPortal.Models;

namespace coreOnlineApparelStoreAdminPortal.Migrations
{
    [DbContext(typeof(DbContextClass))]
    [Migration("20190323072252_Adminadded11")]
    partial class Adminadded11
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("coreOnlineApparelStoreAdminPortal.Models.Brand", b =>
                {
                    b.Property<int>("BrandId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BrandDescription");

                    b.Property<string>("BrandName")
                        .IsRequired();

                    b.HasKey("BrandId");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("coreOnlineApparelStoreAdminPortal.Models.Cart", b =>
                {
                    b.Property<int>("CustomerId");

                    b.Property<int>("ProductId");

                    b.Property<DateTime>("ItemCreated");

                    b.Property<int>("Quantity");

                    b.HasKey("CustomerId", "ProductId");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.HasIndex("ProductId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("coreOnlineApparelStoreAdminPortal.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryDescription");

                    b.Property<string>("CategoryName")
                        .IsRequired();

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("coreOnlineApparelStoreAdminPortal.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CustomerAddress1");

                    b.Property<string>("CustomerAddress2");

                    b.Property<string>("CustomerCountry");

                    b.Property<string>("CustomerCountry2");

                    b.Property<string>("CustomerEmail");

                    b.Property<string>("CustomerFirstName");

                    b.Property<string>("CustomerGender");

                    b.Property<string>("CustomerLastName");

                    b.Property<string>("CustomerPassword");

                    b.Property<long>("CustomerPhoneNumber");

                    b.Property<long>("CustomerPhoneNumber2");

                    b.Property<string>("CustomerState");

                    b.Property<string>("CustomerState2");

                    b.Property<string>("CustomerUserName");

                    b.Property<string>("CustomerZipNumber");

                    b.Property<string>("CustomerZipNumber2");

                    b.Property<bool>("SameAddress");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("coreOnlineApparelStoreAdminPortal.Models.Feedback", b =>
                {
                    b.Property<int>("ProductId");

                    b.Property<int>("OrderId");

                    b.Property<string>("message");

                    b.HasKey("ProductId", "OrderId");

                    b.HasAlternateKey("OrderId", "ProductId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("coreOnlineApparelStoreAdminPortal.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId");

                    b.Property<double>("OrderAmount");

                    b.Property<DateTime>("OrderDate");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("coreOnlineApparelStoreAdminPortal.Models.OrderProduct", b =>
                {
                    b.Property<int>("OrderId");

                    b.Property<int>("Productid");

                    b.Property<int>("Quantity");

                    b.HasKey("OrderId", "Productid");

                    b.HasIndex("Productid");

                    b.ToTable("OrderProducts");
                });

            modelBuilder.Entity("coreOnlineApparelStoreAdminPortal.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrandId");

                    b.Property<int>("CategoryId");

                    b.Property<string>("ProductDescription");

                    b.Property<string>("ProductImage");

                    b.Property<string>("ProductName")
                        .IsRequired();

                    b.Property<float>("ProductPrice");

                    b.Property<int>("ProductQuantity");

                    b.Property<string>("ProductSize")
                        .IsRequired();

                    b.Property<int>("VendorId");

                    b.HasKey("ProductId");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("VendorId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("coreOnlineApparelStoreAdminPortal.Models.Vendor", b =>
                {
                    b.Property<int>("VendorId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("VendorEmail")
                        .IsRequired();

                    b.Property<string>("VendorName")
                        .IsRequired();

                    b.Property<long>("VendorPhoneNo");

                    b.HasKey("VendorId");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("coreOnlineApparelStoreAdminPortal.Models.Cart", b =>
                {
                    b.HasOne("coreOnlineApparelStoreAdminPortal.Models.Customer", "Customer")
                        .WithOne("Cart")
                        .HasForeignKey("coreOnlineApparelStoreAdminPortal.Models.Cart", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("coreOnlineApparelStoreAdminPortal.Models.Product", "Product")
                        .WithMany("Carts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("coreOnlineApparelStoreAdminPortal.Models.Feedback", b =>
                {
                    b.HasOne("coreOnlineApparelStoreAdminPortal.Models.Order", "Order")
                        .WithMany("Feedbacks")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("coreOnlineApparelStoreAdminPortal.Models.Product", "Products")
                        .WithMany("Feedbacks")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("coreOnlineApparelStoreAdminPortal.Models.Order", b =>
                {
                    b.HasOne("coreOnlineApparelStoreAdminPortal.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("coreOnlineApparelStoreAdminPortal.Models.OrderProduct", b =>
                {
                    b.HasOne("coreOnlineApparelStoreAdminPortal.Models.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("coreOnlineApparelStoreAdminPortal.Models.Product", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("Productid")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("coreOnlineApparelStoreAdminPortal.Models.Product", b =>
                {
                    b.HasOne("coreOnlineApparelStoreAdminPortal.Models.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("coreOnlineApparelStoreAdminPortal.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("coreOnlineApparelStoreAdminPortal.Models.Vendor", "Vendor")
                        .WithMany("Products")
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
