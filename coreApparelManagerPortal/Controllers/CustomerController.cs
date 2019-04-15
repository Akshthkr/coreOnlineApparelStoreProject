using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreApparelManagerPortal.Models;
using Microsoft.AspNetCore.Mvc;

namespace coreApparelManagerPortal.Controllers
{
    public class CustomerController : Controller
    {
        MainApparelDbContext context;
        public CustomerController(MainApparelDbContext _context)
        {
            context = _context;
        }
        public ViewResult Index()
        {
            var cat = context.Customers.ToList();
            return View(cat);
        }

        public ActionResult Details(int id)
        {
            Customers c = context.Customers.Where(x => x.CustomerId == id).SingleOrDefault();
            return View(c);
        }
    }
}