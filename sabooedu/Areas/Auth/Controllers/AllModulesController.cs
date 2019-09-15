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
    public class AllModulesController : Controller
    {
        private dbcontext db = new dbcontext();
        public static string img = "";
        // GET: Auth/AllModules
        public async Task<ActionResult> Index()
        {
            return View(await db.AllModules.ToListAsync());
        }

        // GET: Auth/AllModules/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllModule allModule = await db.AllModules.FindAsync(id);
            if (allModule == null)
            {
                return HttpNotFound();
            }
            return View(allModule);
        }

        // GET: Auth/AllModules/Create
        public ActionResult Create()
        {
            ViewBag.Cid = new SelectList(db.FullCourses, "Cid", "Name");
            ViewBag.Lid = new SelectList(db.AllLessons, "Chid", "Name");
            return View();
        }

        // POST: Auth/AllModules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Mid,Cid,Chid,Name,Description,Video,File,Type")] AllModule allModule, HttpPostedFileBase file, Helper help)
        {
            if (ModelState.IsValid)
            {
                allModule.File = help.uploadfile(file);
                db.AllModules.Add(allModule);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Cid = new SelectList(db.FullCourses, "Cid", "Name", allModule.Cid);
            ViewBag.Lid = new SelectList(db.AllLessons, "Chid", "Name", allModule.Chid);
            return View(allModule);
        }

        // GET: Auth/AllModules/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllModule allModule = await db.AllModules.FindAsync(id);
            ViewBag.Cid = new SelectList(db.FullCourses, "Cid", "Name", allModule.Cid);
            ViewBag.Lid = new SelectList(db.AllLessons, "Chid", "Name", allModule.Chid);
            img = allModule.File;
            if (allModule == null)
            {
                return HttpNotFound();
            }
            return View(allModule);
        }

        // POST: Auth/AllModules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Mid,Cid,Chid,Name,Description,Video,File,Type")] AllModule allModule, HttpPostedFileBase file, Helper help)
        {
            if (ModelState.IsValid)
            {
                allModule.File = file != null ? help.uploadfile(file) : img;
                db.Entry(allModule).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Cid = new SelectList(db.FullCourses, "Cid", "Name", allModule.Cid);
            ViewBag.Lid = new SelectList(db.AllLessons, "Chid", "Name", allModule.Chid);
            return View(allModule);
        }

        // GET: Auth/AllModules/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllModule allModule = await db.AllModules.FindAsync(id);
            if (allModule == null)
            {
                return HttpNotFound();
            }
            return View(allModule);
        }

        // POST: Auth/AllModules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AllModule allModule = await db.AllModules.FindAsync(id);
            db.AllModules.Remove(allModule);
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
