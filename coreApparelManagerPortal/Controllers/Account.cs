using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreApparelManagerPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace coreApparelManagerPortal.Controllers
{
    [Route("Account")]
    public class Account : Controller
    {
        MainApparelDbContext context;
        public Account(MainApparelDbContext _context)
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
            Managers a = context.Managers.Where(x => x.ManagerEmail == username).SingleOrDefault();

            if (a != null && password.Equals(a.ManagerPassword))
            {
                HttpContext.Session.SetString("uname", a.ManagerFirstName + " " + a.ManagerLastName);
                return View("Home");
            }
            else
            {
                ViewBag.Error = "Invalid Credentials";
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
