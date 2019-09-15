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
    public class FullCoursesController : Controller
    {
        private dbcontext db = new dbcontext();
        public static string img = "";
        // GET: Auth/FullCourses
        public async Task<ActionResult> Index()
        {
            return View(await db.FullCourses.ToListAsync());
        }

        // GET: Auth/FullCourses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FullCourse fullCourse = await db.FullCourses.FindAsync(id);
            if (fullCourse == null)
            {
                return HttpNotFound();
            }
            return View(fullCourse);
        }

        // GET: Auth/FullCourses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auth/FullCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Cid,Name,Cover")] FullCourse fullCourse, HttpPostedFileBase file, Helper help)
        {
            if (ModelState.IsValid)
            {
                fullCourse.Cover = help.uploadfile(file);
                db.FullCourses.Add(fullCourse);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(fullCourse);
        }

        // GET: Auth/FullCourses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FullCourse fullCourse = await db.FullCourses.FindAsync(id);
            img = fullCourse.Cover;
            if (fullCourse == null)
            {
                return HttpNotFound();
            }
            return View(fullCourse);
        }

        // POST: Auth/FullCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Cid,Name,Cover")] FullCourse fullCourse, HttpPostedFileBase file, Helper help)
        {
            if (ModelState.IsValid)
            {
                fullCourse.Cover = file != null ? help.uploadfile(file) : img;
                db.Entry(fullCourse).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(fullCourse);
        }

        // GET: Auth/FullCourses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FullCourse fullCourse = await db.FullCourses.FindAsync(id);
            if (fullCourse == null)
            {
                return HttpNotFound();
            }
            return View(fullCourse);
        }

        // POST: Auth/FullCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FullCourse fullCourse = await db.FullCourses.FindAsync(id);
            db.FullCourses.Remove(fullCourse);
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
