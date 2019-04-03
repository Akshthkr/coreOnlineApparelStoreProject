using coreOnlineApparelStoreAdminPortal.Controllers;
using coreOnlineApparelStoreAdminPortal.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace AdminPortalTesting
{
    public class AccountTest
    {
        AccountController controller;
        int a;
        DbContextClass context;
        public AccountTest()
        {
            controller = new AccountController(context);
        }
        [Trait("Account", "Index")]
        [Fact(DisplayName = "IndexTestMethod")]
        public void AccountIndex()
        {
            //Arrange
            //Act
            IActionResult result = (ViewResult)controller.Index();
            //Assert
            Assert.NotNull(result);
           
        }
        [Trait("Account", "Logout")]
        [Fact(DisplayName = "LogoutTestMethod")]
        public void AccountLogin()
        {
            //Arrange
            //Act
            IActionResult result = controller.Logout();
            //Assert
            Assert.NotNull(result);
            Assert.Equal(result, "Index");
        }

        //[Route("login")]
        //[HttpPost]
        //public IActionResult Login(string username, string password)
        //{
        //    Admin a = context.Admins.Where(x => x.AdminEmail == username).SingleOrDefault();

        //    if (a != null && password.Equals(a.AdminPassword))
        //    {
        //        HttpContext.Session.SetString("uname", a.AdminFirstName + " " + a.AdminLastName);
        //        return View("Home");
        //    }
        //    else
        //    {
        //        ViewBag.Error = "Invalid Credentials";
        //        return View("Index");
        //    }

        //}
        //[Route("logout")]
        //[HttpGet]
        //public IActionResult Logout()
        //{
        //    HttpContext.Session.Remove("uname");
        //    return RedirectToAction("Index");
        //}
    }
}
