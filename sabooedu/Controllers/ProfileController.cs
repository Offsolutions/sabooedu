using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sabooedu.Models;
namespace sabooedu.Controllers
{
    public class ProfileController : Controller
    {
        dbcontext db = new dbcontext();
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Profiles()
        {
            var a = Convert.ToInt32(HttpContext.User.Identity.Name);
            Registration rr = db.Registrations.Find(a);
            return View(rr);
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword([Bind(Include ="Rid,Password")]Registration Regis)
        {
            var a =Convert.ToInt32(HttpContext.User.Identity.Name);

            Registration rr = db.Registrations.SingleOrDefault(x => x.Rid == a);
            rr.Password = Regis.Password;
            db.SaveChanges();
            return View();
        }
        public ActionResult Courses()
        {
            var a = Convert.ToInt32(HttpContext.User.Identity.Name);
            return View(db.Assigns.Where(x=>x.Rid==a).ToList());
        }
        public ActionResult CoursesDetails(int id)
        {
            return View(db.AllLessons.Where(x => x.Cid == id).ToList());
        }

    }
}