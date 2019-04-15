using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreApparelManagerPortal.Models;
using Microsoft.AspNetCore.Mvc;

namespace coreApparelManagerPortal.Controllers
{
    public class FeedbackController : Controller
    {
        MainApparelDbContext context;
        public FeedbackController(MainApparelDbContext _context)
        {
            context = _context;
        }
        public ViewResult Index()
        {
            var cat = context.Feedbacks.ToList();
            return View(cat);
        }
     
        public ActionResult Details(int id)
        {
            Feedbacks c = context.Feedbacks.Where(x => x.FeedbackId== id).SingleOrDefault();
            return View(c);
        }
    }
}
