using CoreApparelStoreUserPortal.Controllers;
using CoreApparelStoreUserPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace CoreApparelStoreUserPortalTesting
{
    public class brandControllerTesting
    {
        private MainApparelDbContext context;

        public static DbContextOptions<MainApparelDbContext> dbContextOptions { get; set; }
        public static string connectionString = "Data Source=TRD-501; Initial Catalog=MainApparelDb;Integrated Security=True;";
        static brandControllerTesting()
        {
            dbContextOptions = new DbContextOptionsBuilder<MainApparelDbContext>().UseSqlServer(connectionString).Options;
        }
        public brandControllerTesting()
        {
            context = new MainApparelDbContext(dbContextOptions);
        }
        [Fact]
        public void Task_Index_Return_ViewResult()
        {
            var controller = new BrandController(context);

            IActionResult data = controller.Index();
            Assert.IsType<ViewResult>(data);
        }

        [Fact]
        public void Task_ProductDisplay_Return_ViewResult()
        {
            var controller = new BrandController(context);
            var BrandId = 1;
            IActionResult data = controller.ProductDisplay(BrandId);
            Assert.IsType<ViewResult>(data);
        }



    }
}