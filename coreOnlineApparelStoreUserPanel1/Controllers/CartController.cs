using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreOnlineApparelStoreUserPanel1.Helper;
using coreOnlineApparelStoreUserPanel1.Models;
using Microsoft.AspNetCore.Mvc;

namespace coreOnlineApparelStoreUserPanel1.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        OnlineApperelStoreContext context = new OnlineApperelStoreContext();
        [Route("index")]
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Products.ProductPrice * item.Quantity);
            return View();
        }
        [Route("Buy/{id}")]
        public IActionResult Buy(int id)
        {
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { Products = context.Products.Find(id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { Products = context.Products.Find(id), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }
        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }
        private int isExist(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Products.ProductId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
        [Route("details /{id}")]
        public IActionResult details(int id)
        {
            Products p = context.Products.Where(x => x.ProductId == id).SingleOrDefault();
            return View(p);
        }
        [Route("CategoryAvail")]
        public IActionResult CategoryAvail()
        {
            var cat = context.Categories.ToList();
            return View(cat);
        }
        [Route("BrandAvail")]
        public IActionResult BrandAvail()
        {
            var bran = context.Brands.ToList();
            return View(bran);
        }
        [Route("DisplayByCategory")]
        public IActionResult DisplayByCategory(int id)
        {
            var product = context.Products.Where(x => x.CategoryId == id).ToList();
            return View(product);
        }
        [Route("DisplayByBrand")]
        public IActionResult DisplayByBrand(int id)
        {
            var product = context.Products.Where(x => x.BrandId == id).ToList();
            return View(product);
        }
        [HttpGet]
        public IActionResult Checkout(int id)
        {
            int i = 0;
            ViewBag.i = i;
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Products.ProductPrice * item.Quantity);
            TempData["total"] = ViewBag.total;
            return View();           
        }
        [HttpPost]
        public IActionResult checkout(Customers customers)
        {
            context.Customers.Add(customers);
            context.SaveChanges();
            var amount = TempData["total"];
            Orders order = new Orders()
            {
                OrderAmount = Convert.ToSingle(amount),
                OrderDate = DateTime.Now,
                CustomerId = customers.CustomerId
            };
            context.Orders.Add(order);
            context.SaveChanges();
            return RedirectToAction("OrderStatus");

        }
        public IActionResult OrderStatus()
        {
            return View();
        }
    }
}