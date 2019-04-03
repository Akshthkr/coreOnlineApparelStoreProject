using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using coreOnlineApparelStoreUserPanel1.Models;

namespace coreOnlineApparelStoreUserPanel1.Controllers
{
    public class HomeController : Controller
    {
        OnlineApperelStoreContext context = new OnlineApperelStoreContext();
        public IActionResult Index()
        {
            var products = context.Products.ToList();
            return View(products);
        }

        
    }
}
