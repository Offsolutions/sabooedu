using sabooedu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sabooedu.Controllers
{
    public class BlogsController : Controller
    {
        dbcontext db = new dbcontext();
        // GET: Blogs
        public ActionResult Blog()
        {
            return View(db.TheBlogs.ToList());
        }
        public ActionResult BlogDetails(int id)
        {
            return View(db.TheBlogs.Find(id));
        }
        public ActionResult BlogCategory(int id)
        {
            return View(db.TheBlogs.Where(x => x.Bid == id));
        }
    }
}