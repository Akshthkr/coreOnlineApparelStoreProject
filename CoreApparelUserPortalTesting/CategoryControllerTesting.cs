using CoreApparelStoreUserPortal.Controllers;
using CoreApparelStoreUserPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CoreApparelStoreUserPortalTesting
{
    public class CategoryControllerTesting
    {
        private MainApparelDbContext context;

        public static DbContextOptions<MainApparelDbContext> dbContextOptions { get; set; }
        public static string connectionString = "Data Source=TRD-501; Initial Catalog=MainApparelDb;Integrated Security=True;";
        static CategoryControllerTesting()
        {
            dbContextOptions = new DbContextOptionsBuilder<MainApparelDbContext>().UseSqlServer(connectionString).Options;
        }
        public CategoryControllerTesting()
        {
            context = new MainApparelDbContext(dbContextOptions);
        }
        [Fact]
        public void Task_CategoryIndex_Return_ViewResult()
        {
            var controller = new CategoryController(context);

            IActionResult data = controller.Index();
            Assert.IsType<ViewResult>(data);
        }

        [Fact]
        public void Task_CategoryuProductDisplay_Return_ViewResult()
        {
            var controller = new CategoryController(context);
            var CategoryId = 1;
            IActionResult data = controller.ProductDisplay(CategoryId);
            Assert.IsType<ViewResult>(data);
        }
    }
}