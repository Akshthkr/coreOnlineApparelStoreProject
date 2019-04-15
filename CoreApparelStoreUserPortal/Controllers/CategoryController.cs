using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApparelStoreUserPortal.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreApparelStoreUserPortal.Controllers
{
    public class CategoryController : Controller
    {
        MainApparelDbContext context;
        public CategoryController(MainApparelDbContext _context)
        {
            context = _context;
        }
        public IActionResult Index()
        {

            var cat= context.Categories.ToList();
            return View(cat);
        }
        public IActionResult ProductDisplay(int id)
        {
            var p = context.Products.Where(x => x.CategoryId== id).ToList();
            return View(p);
        }
    }
}