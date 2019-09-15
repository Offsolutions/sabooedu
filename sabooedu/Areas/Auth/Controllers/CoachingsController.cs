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
    public class CoachingsController : Controller
    {
        private dbcontext db = new dbcontext();
        public static string img, thumb;
        // GET: Auth/Coachings
        public async Task<ActionResult> Index()
        {
            return View(await db.Coachings.ToListAsync());
        }

        // GET: Auth/Coachings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coaching coaching = await db.Coachings.FindAsync(id);
            if (coaching == null)
            {
                return HttpNotFound();
            }
            return View(coaching);
        }

        // GET: Auth/Coachings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auth/Coachings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Cid,Name,ShortDescription,Description,Thumbnail,Image,Keyword,MetaDescription")] Coaching coaching, HttpPostedFileBase file, HttpPostedFileBase Thumb, Helper help)
        {
            if (ModelState.IsValid)
            {
                coaching.Image = help.uploadfile(file);
                coaching.Thumbnail = help.Resize(Thumb, 300, 500);
                db.Coachings.Add(coaching);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(coaching);
        }

        // GET: Auth/Coachings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coaching coaching = await db.Coachings.FindAsync(id);
            img = coaching.Image;
            thumb = coaching.Thumbnail;
            if (coaching == null)
            {
                return HttpNotFound();
            }
            return View(coaching);
        }

        // POST: Auth/Coachings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Cid,Name,ShortDescription,Description,Thumbnail,Image,Keyword,MetaDescription")] Coaching coaching, HttpPostedFileBase file, HttpPostedFileBase Thumb, Helper help)
        {
            if (ModelState.IsValid)
            {
                coaching.Image = file != null ? help.uploadfile(file) : img;
                coaching.Thumbnail = Thumb != null ? help.Resize(Thumb,300,500) : thumb;
                db.Entry(coaching).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(coaching);
        }

        // GET: Auth/Coachings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coaching coaching = await db.Coachings.FindAsync(id);
            if (coaching == null)
            {
                return HttpNotFound();
            }
            return View(coaching);
        }

        // POST: Auth/Coachings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Coaching coaching = await db.Coachings.FindAsync(id);
            db.Coachings.Remove(coaching);
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
