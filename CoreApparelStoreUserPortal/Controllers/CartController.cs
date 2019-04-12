using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApparelStoreUserPortal.Helpers;
using CoreApparelStoreUserPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApparelStoreUserPortal.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        MainApparelDbContext context = new MainApparelDbContext();

        [Route("index")]
        public IActionResult Index()
        {

           
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


                    ViewBag.cart = cart;
                    ViewBag.total = cart.Sum(item => item.Products.ProductPrice * item.Quantity);
                    if (SessionHelper.GetObectFromJson<Customers>(HttpContext.Session, "cus") == null)
                        ViewBag.i = 0;
                    else
                        ViewBag.i = 1;
                    return View();
                }
            }
            return View("Backhome");
        }
        [Route("buy{id}")]
        public IActionResult Buy(int id)
          {
            
            if (SessionHelper.GetObectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item
                {
                    Products = context.Products.Find(id),
                    Quantity = 1
                });
                if (HttpContext.Session.GetString("name") != null)
                {
                    Carts c = new Carts();
                    c.ProductId = id;
                    c.Quantity = 1;
                    Customers cus = SessionHelper.GetObectFromJson<Customers>(HttpContext.Session, "cus");
                    c.CustomerId = cus.CustomerId;
                    context.Carts.Add(c);
                    context.SaveChanges();
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.GetObectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;

                }
                else
                {
                    cart.Add(new Item
                    {
                        Products = context.Products.Find(id),
                        Quantity = 1
                    });
                    if (HttpContext.Session.GetString("name") != null)
                    {
                        Carts c = new Carts();
                        c.ProductId = id;
                        c.Quantity = 1;
                        Customers cus = SessionHelper.GetObectFromJson<Customers>(HttpContext.Session, "cus");
                        c.CustomerId = cus.CustomerId;
                        context.Carts.Add(c);
                        context.SaveChanges();
                    }
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
           
            return RedirectToAction("Index","Home");
            }

        
        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<Item> cart = SessionHelper.GetObectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            if (HttpContext.Session.GetString("name") != null)
            {
               
                Customers cus = SessionHelper.GetObectFromJson<Customers>(HttpContext.Session, "cus");
                List<Carts> crt = context.Carts.Where(x => x.CustomerId == cus.CustomerId).ToList();
                foreach(var item in crt)
                {
                    if (item.ProductId == id)
                    {

                        Carts c = item;
                        context.Carts.Remove(c);
                        context.SaveChanges();
                    }
                }
               
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            int j = int.Parse(HttpContext.Session.GetString("cartitem"));

            int i= 0; 
            foreach(var item in cart)
            {
                i++;
            }
            if (i != 0)
            {
                j--;
                
                HttpContext.Session.SetString("cartitem", j.ToString());
            }
            else
            {
                HttpContext.Session.Remove("cartitem");
                return View("Backhome");
            }
                
                return RedirectToAction("Index");
        }
        private int isExist(int id)
        {
            List<Item> cart = SessionHelper.GetObectFromJson<List<Item>>(HttpContext.Session, "cart");
            for(int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Products.ProductId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
        [HttpGet]
        public IActionResult Checkout()
        {
            
            int i = 0;
            ViewBag.i = i;
            var cart = SessionHelper.GetObectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Products.ProductPrice * item.Quantity);                   
            Customers cus= SessionHelper.GetObectFromJson<Customers>(HttpContext.Session, "cus");
            ViewBag.cus = cus;
            //int CustId = int.Parse(TempData["cust"].ToString());
            //ViewBag.cus = context.Customers.Where(x => x.CustomerId == CustId).SingleOrDefault();
            TempData["total"] = ViewBag.total;
            return View();


        }
        [HttpPost]
        public IActionResult Checkout(Customers customers)
        {
            var c = context.Customers.Where(x => x.CustomerEmail == customers.CustomerEmail).SingleOrDefault();
            //context.Customers.Add(customers);
            
            c.CustomerFirstName = customers.CustomerFirstName;
            c.CustomerLastName = customers.CustomerLastName;
            c.CustomerUserName = customers.CustomerUserName;
            c.CustomerEmail = customers.CustomerEmail;
            c.CustomerPhoneNumber = customers.CustomerPhoneNumber;
            c.CustomerCountry = customers.CustomerCountry;
            c.CustomerState = customers.CustomerState;
            c.CustomerGender = customers.CustomerGender;
            c.CustomerZipNumber = customers.CustomerZipNumber;
            c.CustomerAddress1 = customers.CustomerAddress1;
            c.CustomerAddress2 = customers.CustomerAddress2;
            c.CustomerPhoneNumber2 = customers.CustomerPhoneNumber2;
            c.CustomerCountry2 = customers.CustomerCountry2;
            c.CustomerState2 = customers.CustomerState2;
            c.CustomerZipNumber2 = customers.CustomerZipNumber2;
            c.SameAddress = customers.SameAddress;
            
            context.SaveChanges();
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cus", c);
            var amount = TempData["total"];
            Orders orders = new Orders()
            {
                OrderAmount = Convert.ToSingle(amount),
                OrderDate = DateTime.Now,
                CustomerId = c.CustomerId
            };
            context.Orders.Add(orders);
            context.SaveChanges();
            var cart = SessionHelper.GetObectFromJson<List<Item>>(HttpContext.Session, "cart");
            List<OrderProducts> orderProducts = new List<OrderProducts>();
            for (int i = 0; i < cart.Count; i++)
            {
                OrderProducts orderProduct = new OrderProducts()
                {
                    OrderId = orders.OrderId,
                    Productid = cart[i].Products.ProductId,
                    Quantity = cart[i].Quantity
                };
                orderProducts.Add(orderProduct);
            }
            orderProducts.ForEach(n => context.OrderProducts.Add(n));
            context.SaveChanges();
            TempData["cust"] = c.CustomerId;
            //TempData["flag"] = 0;


            return RedirectToAction("Status", "cart");
        }
        [Route("Login")]
        [HttpPost]
        public IActionResult Login(string username, string password)
        {

            if (username != null && password != null)
            {

                var cus = context.Customers.Where(x => x.CustomerEmail == username).SingleOrDefault();
                if (cus != null && password.Equals(cus.CustomerPassword))
                {

                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cus", cus);
                    HttpContext.Session.SetString("cusid", cus.CustomerId.ToString());
                    HttpContext.Session.SetString("name", cus.CustomerFirstName + " " + cus.CustomerLastName);

                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", null);
                    
                    HttpContext.Session.Remove("cartitem");


                    List<Carts> crt = context.Carts.ToList();

                    foreach(var item in crt)
                    {
                        if (item.CustomerId == cus.CustomerId)
                        {


                            int id = item.ProductId;
                            if (SessionHelper.GetObectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
                            {
                                List<Item> cart = new List<Item>();
                                cart.Add(new Item
                                {
                                    Products = context.Products.Find(id),
                                    Quantity = 1
                                });
                                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                            }
                            else
                            {
                                List<Item> cart = SessionHelper.GetObectFromJson<List<Item>>(HttpContext.Session, "cart");
                                int index = isExist(id);
                                if (index != -1)
                                {
                                    cart[index].Quantity++;
                                }
                                else
                                {
                                    cart.Add(new Item
                                    {
                                        Products = context.Products.Find(id),
                                        Quantity = 1
                                    });
                                }
                                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                            }
                        }
                    }



                    return RedirectToAction("index", "home");


                }
                else
                {
                    ViewBag.Error = "Register Email First";

                }



            }
            return RedirectToAction("Index");

        }
        [Route("Login1")]
        [HttpPost]
        public IActionResult Login1(string username, string password)
        {

            if (username != null && password != null)
            {

                var cus = context.Customers.Where(x => x.CustomerEmail == username).SingleOrDefault();
                if (cus != null && password.Equals(cus.CustomerPassword))
                {

                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cus", cus);
                    

                    HttpContext.Session.SetString("cusid", cus.CustomerId.ToString());
                    HttpContext.Session.SetString("name", cus.CustomerFirstName + " " + cus.CustomerLastName);

                    



                    HttpContext.Session.Remove("cartitem");

                    List<Carts> crt = context.Carts.Where(x => x.CustomerId == cus.CustomerId).ToList();
                    foreach(var item in crt)
                    {
                        context.Carts.Remove(item);
                        context.SaveChanges();
                    }
                    if (SessionHelper.GetObectFromJson<List<Item>>(HttpContext.Session, "cart") != null)
                    {


                        var cart = SessionHelper.GetObectFromJson<List<Item>>(HttpContext.Session, "cart");
                        foreach (var item in cart)
                        {
                            Carts c = new Carts();
                            c.CustomerId = cus.CustomerId;
                            c.ProductId = item.Products.ProductId;
                            c.Quantity = item.Quantity;
                            c.ItemCreated = item.ItemCreated;
                            context.Carts.Add(c);
                            context.SaveChanges();
                        }
                    }
                    return RedirectToAction("Checkout");


                }
                else
                {
                    ViewBag.Error = "Register Email First";

                }



            }
            return RedirectToAction("Index");

        }

        [Route("register")]
        [HttpPost]
        public IActionResult Regiter(string username, string password,string firstname,string lastname)
        {
           
                if (username != null && password != null)
                {

                    var cus = context.Customers.Where(x => x.CustomerEmail == username).SingleOrDefault();
                    if (cus != null)
                    {

                        ViewBag.Error = "Alredy register";


                    }
                    else
                    {
                        Customers c = new Customers();
                        c.CustomerEmail = username;
                    c.CustomerPassword = password;

                        c.CustomerFirstName = firstname;
                        c.CustomerLastName = lastname;
                        context.Customers.Add(c);
                        context.SaveChanges();
                    Customers cus1 = context.Customers.Where(x => x.CustomerEmail == username).SingleOrDefault();
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cus", cus1);

                    HttpContext.Session.Remove("cartitem");
                    HttpContext.Session.SetString("name", c.CustomerFirstName + " " + c.CustomerLastName);
                    HttpContext.Session.SetString("cusid", cus1.CustomerId.ToString());
                    
                    if (SessionHelper.GetObectFromJson<List<Item>>(HttpContext.Session, "cart") != null)
                    {


                        var cart = SessionHelper.GetObectFromJson<List<Item>>(HttpContext.Session, "cart");
                        foreach (var item in cart)
                        {
                            Carts c1 = new Carts();
                            c1.CustomerId = cus1.CustomerId;
                            c1.ProductId = item.Products.ProductId;
                            c1.Quantity = item.Quantity;
                            c1.ItemCreated = item.ItemCreated;
                            context.Carts.Add(c1);
                            context.SaveChanges();
                        }
                    }
                    return RedirectToAction("index", "home");
                  

                    }

                }
            return RedirectToAction("Index");



        }
        [Route("register1")]
        [HttpPost]
        public IActionResult Regiter1(string username, string password, string firstname, string lastname)
        {

            if (username != null && password != null)
            {

                var cus = context.Customers.Where(x => x.CustomerEmail == username).SingleOrDefault();
                if (cus != null)
                {

                    ViewBag.Error = "Alredy register";


                }
                else
                {
                    Customers c = new Customers();
                    c.CustomerEmail = username;
                    c.CustomerPassword = password;

                    c.CustomerFirstName = firstname;
                    c.CustomerLastName = lastname;
                    context.Customers.Add(c);
                    context.SaveChanges();
                    Customers cus1 = context.Customers.Where(x => x.CustomerEmail == username).SingleOrDefault();
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cus", cus1);
                    
                    HttpContext.Session.Remove("cartitem");
                    HttpContext.Session.SetString("name", c.CustomerFirstName + " " + c.CustomerLastName);
                    HttpContext.Session.SetString("cusid", cus1.CustomerId.ToString());
                   var cart = SessionHelper.GetObectFromJson<List<Item>>(HttpContext.Session, "cart");
                    foreach (var item in cart)
                    {
                        Carts c1 = new Carts();

                        c1.CustomerId = cus1.CustomerId;
                        c1.ProductId = item.Products.ProductId;
                        c1.Quantity = item.Quantity;
                        c1.ItemCreated = item.ItemCreated;
                        context.Carts.Add(c1);
                        context.SaveChanges();
                    }
                    return RedirectToAction("checkout");

                }

            }
            return RedirectToAction("Index");



        }
        [Route("Status")]
        public IActionResult Status()
        {
            int CustId = int.Parse(TempData["cust"].ToString());
            Customers customers = context.Customers.Where(x => x.CustomerId == CustId).SingleOrDefault();
            ViewBag.cust = customers;

            //int custId = int.Parse(TempData["cust"].ToString());
            //Orders ord = context.Orders.Where(x => x.CustomerId == custId).SingleOrDefault();
            //ViewBag.order = ord;

            var cart = SessionHelper.GetObectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            foreach(var Item1 in cart)
            {
                Products p = context.Products.Find(Item1.Products.ProductId);
                p.ProductQuantity = p.ProductQuantity - Item1.Quantity;
                context.SaveChanges();
            }
            ViewBag.total = cart.Sum(item => item.Products.ProductPrice * item.Quantity);
            cart=null;
            List<Carts> crt = context.Carts.Where(x => x.CustomerId == customers.CustomerId).ToList();
            foreach (var item in crt)
            {
                context.Carts.Remove(item);
                context.SaveChanges();
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            HttpContext.Session.Remove("cartitem");
            return View();
        }
        [Route("Plus")]
        [HttpGet]
        public IActionResult Plus( int id)
        {
            List<Item> cart = SessionHelper.GetObectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            if (index != -1)
            {
                cart[index].Quantity++;
            }
            else
            {
                cart.Add(new Item
                {
                    Products = context.Products.Find(id),
                    Quantity = 1
                });
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }
        [Route("Minus")]
        [HttpGet]
        public IActionResult Minus(int id)
        {
            List<Item> cart = SessionHelper.GetObectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            if (index != -1)
            {
                if (cart[index].Quantity != 1)
                {
                    cart[index].Quantity--;
                }
                else
                    return RedirectToAction("Remove","Cart",new {@id=id });
                
            }
           
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }
      

    }

}
