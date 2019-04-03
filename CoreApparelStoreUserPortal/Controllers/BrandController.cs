using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApparelStoreUserPortal.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreApparelStoreUserPortal.Controllers
{
    public class BrandController : Controller
    {
        MainApparelDbContext context = new MainApparelDbContext();
        public IActionResult Index()
        {

            var brand = context.Brands.ToList();
            return View(brand);
        }
        public IActionResult ProductDisplay(int id)
        {
            var p = context.Products.Where(x=>x.BrandId==id).ToList();
            return View(p);
        }
    }
}