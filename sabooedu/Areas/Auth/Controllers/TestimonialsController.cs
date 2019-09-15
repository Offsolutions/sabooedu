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
    public class TestimonialsController : Controller
    {
        private dbcontext db = new dbcontext();
        public static string img = "";
        // GET: Auth/Testimonials
        public async Task<ActionResult> Index()
        {
            return View(await db.Testimonials.ToListAsync());
        }

        // GET: Auth/Testimonials/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testimonials testimonials = await db.Testimonials.FindAsync(id);
            if (testimonials == null)
            {
                return HttpNotFound();
            }
            return View(testimonials);
        }

        // GET: Auth/Testimonials/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auth/Testimonials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Tid,Message,Name,Designation,Image")] Testimonials testimonials, HttpPostedFileBase file, Helper help)
        {
            if (ModelState.IsValid)
            {
                testimonials.Image = help.uploadfile(file);
                db.Testimonials.Add(testimonials);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(testimonials);
        }

        // GET: Auth/Testimonials/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testimonials testimonials = await db.Testimonials.FindAsync(id);
            img = testimonials.Image;
            if (testimonials == null)
            {
                return HttpNotFound();
            }
            return View(testimonials);
        }

        // POST: Auth/Testimonials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Tid,Message,Name,Designation,Image")] Testimonials testimonials, HttpPostedFileBase file, Helper help)
        {
            if (ModelState.IsValid)
            {
                testimonials.Image = file != null ? help.uploadfile(file) : img;
                db.Entry(testimonials).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(testimonials);
        }

        // GET: Auth/Testimonials/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testimonials testimonials = await db.Testimonials.FindAsync(id);
            if (testimonials == null)
            {
                return HttpNotFound();
            }
            return View(testimonials);
        }

        // POST: Auth/Testimonials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Testimonials testimonials = await db.Testimonials.FindAsync(id);
            db.Testimonials.Remove(testimonials);
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
