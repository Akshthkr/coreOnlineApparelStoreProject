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
        public IActionResult logout()
        {
            HttpContext.Session.Remove("cartitem");

            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", null);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cus", null);
            HttpContext.Session.Remove("Logout");

            return RedirectToAction("index");

        }
        public IActionResult userprofile()
        {
            Customers c =  SessionHelper.GetObectFromJson<Customers>(HttpContext.Session, "cus");
            int a = c.CustomerId;
            ViewBag.id = a;
            return View(c);
            
        }
        public IActionResult CustomerHistory()
        {
            Customers c= SessionHelper.GetObectFromJson<Customers>(HttpContext.Session, "cus");
            List<Orders> a = context.Orders.Where(x => x.CustomerId == c.CustomerId).ToList();
            ViewBag.a = a;
           
            return View(); 


        }
        public IActionResult orderdetail(int id)
        {
            List<OrderProducts> op = new List<OrderProducts>();
            List<Products> products = new List<Products>();
            op = context.OrderProducts.Where(x => x.OrderId == id).ToList();
            foreach(var item in op)
            {
                Products c = context.Products.Where(x => x.ProductId == item.Productid).SingleOrDefault();
                products.Add(c);
            }
            ViewBag.p = products;
            return View();
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

        public ActionResult cusEdit()
        {
            Customers cus = SessionHelper.GetObectFromJson<Customers>(HttpContext.Session, "cus");
            return View(cus);
        }
        [HttpPost]
        public ActionResult cusEdit(int id, Customers customers)
        {
            Customers c = context.Customers.Where(x => x.CustomerEmail == customers.CustomerEmail).SingleOrDefault();
            c.CustomerFirstName = customers.CustomerFirstName;
            c.CustomerLastName = customers.CustomerLastName;
            c.CustomerPhoneNumber = customers.CustomerPhoneNumber;
            c.CustomerCountry = customers.CustomerCountry;
            c.CustomerState = customers.CustomerState;
            c.CustomerZipNumber = customers.CustomerZipNumber;
            c.CustomerAddress1 = customers.CustomerAddress1;
            c.CustomerPhoneNumber2 = customers.CustomerPhoneNumber2;
            c.SameAddress = customers.SameAddress;
            context.SaveChanges();
           SessionHelper.SetObjectAsJson(HttpContext.Session, "cus",c);
            return RedirectToAction("userprofile","home",new { @id = customers.CustomerEmail });

        }
        [HttpGet]
        public ActionResult changepassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult changepassword(string oldpassword, string newpassword, string newpassword1)
        {
            
            Customers c= SessionHelper.GetObectFromJson<Customers>(HttpContext.Session, "cus");
            if(oldpassword==c.CustomerPassword && newpassword == newpassword1)
            {
                Customers cus = context.Customers.Where(x => x.CustomerEmail == c.CustomerEmail).SingleOrDefault();
                cus.CustomerPassword = newpassword;
                context.SaveChanges();

            }
           
            return RedirectToAction("index");
        }
        public IActionResult Feedback()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Feedback(Feedbacks feedBacks)
        {
            Customers c = SessionHelper.GetObectFromJson<Customers>(HttpContext.Session, "cus");
            feedBacks.CustomerId = c.CustomerId;
            feedBacks.Message = feedBacks.Message;
            context.Feedbacks.Add(feedBacks);
            context.SaveChanges();
            return RedirectToAction("UserProfile", "Cart");
        }




    }

}
