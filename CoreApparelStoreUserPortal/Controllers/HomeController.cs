using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreApparelStoreUserPortal.Models;
using CoreApparelStoreUserPortal.Helpers;
using Microsoft.AspNetCore.Http;

namespace CoreApparelStoreUserPortal.Controllers
{
    public class HomeController : Controller
    {
        MainApparelDbContext context = new MainApparelDbContext();
        public IActionResult Index()
        {
            var product = context.Products.ToList();
            int j = 0;
            var cart = SessionHelper.GetObectFromJson<List<Item>>(HttpContext.Session, "cart");
            
            int i = 0;

            if (cart != null)
            {


                foreach (var item in cart)
                {
                    i++;
                }
                if (i != 0)
                {

                    foreach (var i1 in cart)
                    {
                        j++;

                    }
                    HttpContext.Session.SetString("cartitem", j.ToString());

                }

            }   

            return View(product);
        }
        public IActionResult Details(int id)
        {
            Products p= context.Products.Find(id);
            var vid = context.Products.Find(id);
            ViewBag.vname = context.Vendors.Find(vid.VendorId);
            ViewBag.cname = context.Categories.Find(vid.CategoryId);
            ViewBag.bname = context.Brands.Find(vid.BrandId);
            return View(p);
        }
        

    }
}
