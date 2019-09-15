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
    public class AssignsController : Controller
    {
        private dbcontext db = new dbcontext();

        // GET: Auth/Assigns
        public async Task<ActionResult> Index()
        {
            var assigns = db.Assigns.Include(a => a.FullCourses).Include(a => a.Registrations);
            return View(await assigns.ToListAsync());
        }

        // GET: Auth/Assigns/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assign assign = await db.Assigns.FindAsync(id);
            if (assign == null)
            {
                return HttpNotFound();
            }
            return View(assign);
        }

        // GET: Auth/Assigns/Create
        public ActionResult Create()
        {
            ViewBag.Cid = new SelectList(db.FullCourses, "Cid", "Name");
            ViewBag.Rid = new SelectList(db.Registrations, "Rid", "FirstName");
            return View();
        }

        // POST: Auth/Assigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Aid,Date,Rid,Cid")] Assign assign)
        {
            if (ModelState.IsValid)
            {
                db.Assigns.Add(assign);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Cid = new SelectList(db.FullCourses, "Cid", "Name", assign.Cid);
            ViewBag.Rid = new SelectList(db.Registrations, "Rid", "FirstName", assign.Rid);
            return View(assign);
        }

        // GET: Auth/Assigns/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assign assign = await db.Assigns.FindAsync(id);
            if (assign == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cid = new SelectList(db.FullCourses, "Cid", "Name", assign.Cid);
            ViewBag.Rid = new SelectList(db.Registrations, "Rid", "FirstName", assign.Rid);
            return View(assign);
        }

        // POST: Auth/Assigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Aid,Date,Rid,Cid")] Assign assign)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assign).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Cid = new SelectList(db.FullCourses, "Cid", "Name", assign.Cid);
            ViewBag.Rid = new SelectList(db.Registrations, "Rid", "FirstName", assign.Rid);
            return View(assign);
        }

        // GET: Auth/Assigns/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assign assign = await db.Assigns.FindAsync(id);
            if (assign == null)
            {
                return HttpNotFound();
            }
            return View(assign);
        }

        // POST: Auth/Assigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Assign assign = await db.Assigns.FindAsync(id);
            db.Assigns.Remove(assign);
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
