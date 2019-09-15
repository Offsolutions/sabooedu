using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sabooedu.Areas.Auth.Models;
using sabooedu.Models;

namespace sabooedu.Areas.Auth.Controllers
{
    public class AllLessonsController : Controller
    {
        private dbcontext db = new dbcontext();

        // GET: Auth/AllLessons
        public async Task<ActionResult> Index()
        {
            var allLessons = db.AllLessons.Include(a => a.FullCourses);
            return View(await allLessons.ToListAsync());
        }

        // GET: Auth/AllLessons/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllLesson allLesson = await db.AllLessons.FindAsync(id);
            if (allLesson == null)
            {
                return HttpNotFound();
            }
            return View(allLesson);
        }

        // GET: Auth/AllLessons/Create
        public ActionResult Create()
        {
            ViewBag.Cid = new SelectList(db.FullCourses, "Cid", "Name");
            return View();
        }

        // POST: Auth/AllLessons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Chid,Cid,Name")] AllLesson allLesson)
        {
            if (ModelState.IsValid)
            {
                db.AllLessons.Add(allLesson);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Cid = new SelectList(db.FullCourses, "Cid", "Name", allLesson.Cid);
            return View(allLesson);
        }

        // GET: Auth/AllLessons/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllLesson allLesson = await db.AllLessons.FindAsync(id);
            if (allLesson == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cid = new SelectList(db.FullCourses, "Cid", "Name", allLesson.Cid);
            return View(allLesson);
        }

        // POST: Auth/AllLessons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Chid,Cid,Name")] AllLesson allLesson)
        {
            if (ModelState.IsValid)
            {
                db.Entry(allLesson).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Cid = new SelectList(db.FullCourses, "Cid", "Name", allLesson.Cid);
            return View(allLesson);
        }

        // GET: Auth/AllLessons/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllLesson allLesson = await db.AllLessons.FindAsync(id);
            if (allLesson == null)
            {
                return HttpNotFound();
            }
            return View(allLesson);
        }

        // POST: Auth/AllLessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AllLesson allLesson = await db.AllLessons.FindAsync(id);
            db.AllLessons.Remove(allLesson);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
