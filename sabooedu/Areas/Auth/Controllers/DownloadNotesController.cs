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
    public class DownloadNotesController : Controller
    {
        private dbcontext db = new dbcontext();

        // GET: Auth/DownloadNotes
        public async Task<ActionResult> Index()
        {
            var downloadNotes = db.DownloadNotes.Include(d => d.Coachings);
            return View(await downloadNotes.ToListAsync());
        }

        // GET: Auth/DownloadNotes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DownloadNotes downloadNotes = await db.DownloadNotes.FindAsync(id);
            if (downloadNotes == null)
            {
                return HttpNotFound();
            }
            return View(downloadNotes);
        }

        // GET: Auth/DownloadNotes/Create
        public ActionResult Create()
        {
            ViewBag.Cid = new SelectList(db.Coachings, "Cid", "Name");
            return View();
        }

        // POST: Auth/DownloadNotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Did,Cid,Notes")] DownloadNotes downloadNotes)
        {
            if (ModelState.IsValid)
            {
                db.DownloadNotes.Add(downloadNotes);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Cid = new SelectList(db.Coachings, "Cid", "Name", downloadNotes.Cid);
            return View(downloadNotes);
        }

        // GET: Auth/DownloadNotes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DownloadNotes downloadNotes = await db.DownloadNotes.FindAsync(id);
            if (downloadNotes == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cid = new SelectList(db.Coachings, "Cid", "Name", downloadNotes.Cid);
            return View(downloadNotes);
        }

        // POST: Auth/DownloadNotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Did,Cid,Notes")] DownloadNotes downloadNotes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(downloadNotes).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Cid = new SelectList(db.Coachings, "Cid", "Name", downloadNotes.Cid);
            return View(downloadNotes);
        }

        // GET: Auth/DownloadNotes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DownloadNotes downloadNotes = await db.DownloadNotes.FindAsync(id);
            if (downloadNotes == null)
            {
                return HttpNotFound();
            }
            return View(downloadNotes);
        }

        // POST: Auth/DownloadNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DownloadNotes downloadNotes = await db.DownloadNotes.FindAsync(id);
            db.DownloadNotes.Remove(downloadNotes);
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
