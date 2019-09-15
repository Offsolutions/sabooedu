using sabooedu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sabooedu.Controllers
{
    public class CoachingController : Controller
    {
        // GET: Coaching
        dbcontext db = new dbcontext();
        public ActionResult Lecture()
        {
            return View(db.Coachings.ToList());
        }
        public ActionResult Coaching(int id)
        {
            return View(db.Coachings.Find(id));
        }
        public ActionResult VideoLecture(int id)
        {
            return View(db.VideoLectures.Where(x=>x.Cid==id).ToList());
        }
    }
}