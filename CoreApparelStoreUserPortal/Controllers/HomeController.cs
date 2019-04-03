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
            HttpContext.Session.SetString("homecart", "Login");
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
        public IActionResult logout()
        {
            HttpContext.Session.Remove("logout");
            HttpContext.Session.Remove("cartitem");

            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", null);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cus", null);
            HttpContext.Session.SetString("log", "Logout");
            return RedirectToAction("index");

        }
        public IActionResult userprofile(string id)
        {
            Customers c = context.Customers.Where(x => x.CustomerEmail == id).SingleOrDefault();
            int a = c.CustomerId;
            ViewBag.id = a;
            return View(c);

        }
        public IActionResult CustomerHistory(int id)
        {
            List<Orders> a = context.Orders.Where(x => x.CustomerId == id).ToList();
            int i = 0;
            var cart = SessionHelper.GetObectFromJson<List<History>>(HttpContext.Session, "hist");

            List<History> b = new List<History>();
            List<object> c=new List<object>();
            foreach (var item in a)
            {
                var query = (from o in a
                             join op in context.OrderProducts 
                         on o.OrderId equals op.OrderId into ocop
                         from y in ocop.DefaultIfEmpty()
                             join p in context.Products
                               on y.Productid equals p.ProductId
                             
                             select new
                             {
                                 o.OrderDate,
                                 o.OrderAmount,
                                 p.ProductImage,
                                 p.ProductName,
                                 y.Quantity
                             }).ToList(); ;
               
                   foreach(var item1 in query)
                {
                    History h = new History();
                    h.OrderDate = item1.OrderDate;
                    h.OrderAmount = item1.OrderAmount;
                    h.ProductImage = item1.ProductImage;
                    h.ProductName = item1.ProductName;
                    h.Quantity = item1.Quantity;
                    b.Add(h);
                }
                    
                
                //q.Add(query.ToList());
               
            }
            ViewBag.history = b;
            return View(b);


        }
        [Route("Search")]
        [HttpPost]
        public IActionResult Search(string search)
        {
            List<Products> prod = new List<Products>();
            var product = context.Products.Where(x => x.ProductName == search).ToList();
            if (context.Brands.Where(x => x.BrandName == search).SingleOrDefault() != null)
            {
                Brands b = context.Brands.Where(x => x.BrandName == search).SingleOrDefault();
                var brand = context.Products.Where(x => x.BrandId == b.BrandId).ToList();
                foreach (var item in brand)
                {
                    prod.Add(item);

                }
            }
            if (context.Categories.Where(x => x.CategoryName == search).SingleOrDefault() != null)
            {
                Categories c = context.Categories.Where(x => x.CategoryName == search).SingleOrDefault();
                var category = context.Products.Where(x => x.CategoryId == c.CategoryId).ToList();
                foreach (var item in category)
                {
                    prod.Add(item);

                }
            }

            foreach (var item in product)
            {
                prod.Add(item);

            }


            return View(prod);
        }

        public ActionResult cusEdit(int id)
        {
            Customers ban = context.Customers.Where(x => x.CustomerId == id).SingleOrDefault();
            return View(ban);
        }
        [HttpPost]
        public ActionResult cusEdit(int id, Customers b1)
        {
            Customers b = context.Customers.Where(x => x.CustomerId == b1.CustomerId).SingleOrDefault();
            context.Entry(b).CurrentValues.SetValues(b1);
            context.SaveChanges();
            return RedirectToAction("userprofile");

        }

    }
}
