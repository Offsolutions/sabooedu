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
    public class PagesController : Controller
    {
        private dbcontext db = new dbcontext();
        public static string img = "";
        // GET: Auth/Pages
        public async Task<ActionResult> Index()
        {
            return View(await db.Pages.ToListAsync());
        }

        // GET: Auth/Pages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pages pages = await db.Pages.FindAsync(id);
            if (pages == null)
            {
                return HttpNotFound();
            }
            return View(pages);
        }

        // GET: Auth/Pages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auth/Pages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Pid,PageName,Description,Image,Keyword,MetaDescription")] Pages pages, HttpPostedFileBase file, Helper help)
        {
            if (ModelState.IsValid)
            {
                pages.Image = help.uploadfile(file);
                db.Pages.Add(pages);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(pages);
        }

        // GET: Auth/Pages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pages pages = await db.Pages.FindAsync(id);
            img = pages.Image;
            if (pages == null)
            {
                return HttpNotFound();
            }
            return View(pages);
        }

        // POST: Auth/Pages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Pid,PageName,Description,Image,Keyword,MetaDescription")] Pages pages, HttpPostedFileBase file, Helper help)
        {
            if (ModelState.IsValid)
            {
                pages.Image = file != null ? help.uploadfile(file) : img;
                db.Entry(pages).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pages);
        }

        // GET: Auth/Pages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pages pages = await db.Pages.FindAsync(id);
            if (pages == null)
            {
                return HttpNotFound();
            }
            return View(pages);
        }

        // POST: Auth/Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Pages pages = await db.Pages.FindAsync(id);
            db.Pages.Remove(pages);
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
