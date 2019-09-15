using sabooedu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sabooedu.Controllers
{
    public class AboutController : Controller
    {
        dbcontext db = new dbcontext();
        // GET: About
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View(db.Pages.Find(2));
        }
        public ActionResult WhyUs()
        {
            return View(db.Pages.Find(4));
        }
        public ActionResult OurVision()
        {
            return View(db.Pages.Find(3));
        }
    }
}