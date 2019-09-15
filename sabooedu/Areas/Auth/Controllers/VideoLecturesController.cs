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
    public class VideoLecturesController : Controller
    {
        private dbcontext db = new dbcontext();
        public static string img;
        // GET: Auth/VideoLectures
        public async Task<ActionResult> Index()
        {
            var videoLectures = db.VideoLectures.Include(v => v.Coachings);
            return View(await videoLectures.ToListAsync());
        }

        // GET: Auth/VideoLectures/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoLecture videoLecture = await db.VideoLectures.FindAsync(id);
            if (videoLecture == null)
            {
                return HttpNotFound();
            }
            return View(videoLecture);
        }

        // GET: Auth/VideoLectures/Create
        public ActionResult Create()
        {
            ViewBag.Cid = new SelectList(db.Coachings, "Cid", "Name");
            return View();
        }

        // POST: Auth/VideoLectures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Vid,Cid,Url,Image")] VideoLecture videoLecture, HttpPostedFileBase file, Helper help)
        {
            if (ModelState.IsValid)
            {
                videoLecture.Image = help.uploadfile(file);
                db.VideoLectures.Add(videoLecture);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Cid = new SelectList(db.Coachings, "Cid", "Name", videoLecture.Cid);
            return View(videoLecture);
        }

        // GET: Auth/VideoLectures/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoLecture videoLecture = await db.VideoLectures.FindAsync(id);
            img = videoLecture.Image;
            if (videoLecture == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cid = new SelectList(db.Coachings, "Cid", "Name", videoLecture.Cid);
            return View(videoLecture);
        }

        // POST: Auth/VideoLectures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Vid,Cid,Url,Image")] VideoLecture videoLecture, HttpPostedFileBase file, Helper help)
        {
            if (ModelState.IsValid)
            {
                videoLecture.Image = file != null ? help.uploadfile(file) : img;
                db.Entry(videoLecture).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Cid = new SelectList(db.Coachings, "Cid", "Name", videoLecture.Cid);
            return View(videoLecture);
        }

        // GET: Auth/VideoLectures/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoLecture videoLecture = await db.VideoLectures.FindAsync(id);
            if (videoLecture == null)
            {
                return HttpNotFound();
            }
            return View(videoLecture);
        }

        // POST: Auth/VideoLectures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VideoLecture videoLecture = await db.VideoLectures.FindAsync(id);
            db.VideoLectures.Remove(videoLecture);
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
