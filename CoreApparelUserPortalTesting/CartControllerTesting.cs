using CoreApparelStoreUserPortal.Controllers;
using CoreApparelStoreUserPortal.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CoreApparelStoreUserPortalTesting
{
    public class CartControllerTesting
    {
        private MainApparelDbContext context;

        public static DbContextOptions<MainApparelDbContext> dbContextOptions { get; set; }
        public static string connectionString = "Data Source=TRD-501; Initial Catalog=MainApparelDb;Integrated Security=True;";
        static CartControllerTesting()
        {
            dbContextOptions = new DbContextOptionsBuilder<MainApparelDbContext>().UseSqlServer(connectionString).Options;
        }
        public CartControllerTesting()
        {
            context = new MainApparelDbContext(dbContextOptions);
        }
        [Fact]
        public void Task_Index_Return_ViewResult()
        {

            Assert.Throws<NullReferenceException>(() =>
            {
                var controller = new CartController(context);

                var data = controller.Index();

                Assert.Null(data);

                var viewResult = Assert.IsType<ViewResult>(data);
            });

        }
        [Fact]
        public void Task_Checkout_Return_View()
        {
            Assert.Throws<NullReferenceException>(() =>
            {
                var controller = new CartController(context);
                var CustomerId = 34;
                var customer = new Customers()
                {
                    CustomerId = 34,
                    CustomerFirstName = "Shubhi",
                    CustomerLastName = "Dwivedi",
                    CustomerUserName = "Shivani.dwivedi",
                    CustomerPassword = "123456",
                    CustomerEmail = "shivani@user.com",
                    CustomerAddress1 = "Sector 62",
                    CustomerAddress2 = "Sector 62",
                    CustomerState = "Delhi",
                    CustomerState2 = "Delhi",
                    CustomerCountry = "India",
                    CustomerCountry2 = "India",
                    CustomerPhoneNumber = 7974851362,
                    CustomerPhoneNumber2 = 7974851362,
                    CustomerZipNumber = "6534212",
                    CustomerZipNumber2 = "6534212",
                    CustomerGender = "Female",


                };
                var data = controller.Checkout(customer);
                Assert.IsType<ViewResult>(data);
            });
        }
        [Fact]
        public void Task_remove_Return_View()
        {
            Assert.Throws<NullReferenceException>(() =>
            {
                var controller = new CartController(context);
                var Id = 1;
                var data = controller.Remove(Id);
                Assert.IsType<ViewResult>(data);
            });
        }
        [Fact]
        public void Task_Login_Return_View()
        {
            Assert.Throws<NullReferenceException>(() =>
            {
                var controller = new CartController(context);
                var CustomerEmail = "ak@gmail.com";
                var CustomerPassword = "akash";
                var data = controller.Login(CustomerEmail, CustomerPassword);
                Assert.NotNull(data);
                Assert.IsType<ViewResult>(data);
            });
        }

        [Fact]
        public void Task_Register_Return_View()
        {

            var controller = new CartController(context);

            var CustomerFirstName = "Shubhi";
            var CustomerLastName = "Dwivedi";
            var CustomerEmail = "shivani@user.com";
            var CustomerPassword = "123456";


            var data = controller.Regiter(CustomerEmail, CustomerPassword, CustomerFirstName, CustomerLastName);
            Assert.NotNull(data);


        }


        [Fact]
        public void Task_Status_Return_View()
        {
            Assert.Throws<NullReferenceException>(() =>
            {
                var controller = new CartController(context);
                var data = controller.Status();
                Assert.IsType<ViewResult>(data);
            });
        }
    }
}