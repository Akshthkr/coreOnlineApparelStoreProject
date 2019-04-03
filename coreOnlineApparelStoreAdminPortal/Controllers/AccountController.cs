using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreOnlineApparelStoreAdminPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace coreOnlineApparelStoreAdminPortal.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        DbContextClass context;
        public AccountController(DbContextClass _context)
        {
            context = _context;
        }
        [Route("")]
        [Route("index")]
        [Route("~/")]
        [HttpGet]
        public IActionResult Index()
        {
            
            return View();
        }
        [Route("login")]
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            Admin a=context.Admins.Where(x => x.AdminEmail == username).SingleOrDefault();
            
            if (a!=null && password.Equals(a.AdminPassword))
            {
                HttpContext.Session.SetString("uname",a.AdminFirstName+" "+a.AdminLastName);
                return View("Home");
            }
            else
            {
                ViewBag.Error="Invalid Credentials";
                return View("Index");
            }
            
        }
        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("uname");
            return RedirectToAction("Index");         
        }
        
    }
}