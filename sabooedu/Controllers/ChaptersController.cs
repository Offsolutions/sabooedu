using sabooedu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sabooedu.Controllers
{
    public class ChaptersController : Controller
    {
        dbcontext db = new dbcontext();
        // GET: Chapters
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AllChapters()
        {
            return View(db.FullCourses.ToList());
        }
        public ActionResult ChapterDetails(int id)
        {
            return View(db.AllLessons.Where(x=>x.Cid==id).ToList());
        }
    }
}