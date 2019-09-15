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
    public class TheBlogsController : Controller
    {
        private dbcontext db = new dbcontext();
        public static string img, thumb;
        // GET: Auth/TheBlogs
        public async Task<ActionResult> Index()
        {
            var theBlogs = db.TheBlogs.Include(t => t.BlogCategories);
            return View(await theBlogs.ToListAsync());
        }

        // GET: Auth/TheBlogs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TheBlog theBlog = await db.TheBlogs.FindAsync(id);
            if (theBlog == null)
            {
                return HttpNotFound();
            }
            return View(theBlog);
        }

        // GET: Auth/TheBlogs/Create
        public ActionResult Create()
        {
            ViewBag.Bid = new SelectList(db.BlogCategories, "Bid", "Name");
            return View();
        }

        // POST: Auth/TheBlogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "lid,Bid,Title,Date,ShortDescription,Description,Thumbnail,Image")] TheBlog theBlog, HttpPostedFileBase file, HttpPostedFileBase thumb, Helper help)
        {
            if (ModelState.IsValid)
            {
                theBlog.Image = help.uploadfile(file);
                theBlog.Thumbnail = help.uploadfile(thumb);
                db.TheBlogs.Add(theBlog);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Bid = new SelectList(db.BlogCategories, "Bid", "Name", theBlog.Bid);
            return View(theBlog);
        }

        // GET: Auth/TheBlogs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TheBlog theBlog = await db.TheBlogs.FindAsync(id);
            img = theBlog.Image;
            thumb = theBlog.Thumbnail;
            if (theBlog == null)
            {
                return HttpNotFound();
            }
            ViewBag.Bid = new SelectList(db.BlogCategories, "Bid", "Name", theBlog.Bid);
            return View(theBlog);
        }

        // POST: Auth/TheBlogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "lid,Bid,Title,Date,ShortDescription,Description,Thumbnail,Image")] TheBlog theBlog, HttpPostedFileBase file, HttpPostedFileBase thumb1, Helper help)
        {
            if (ModelState.IsValid)
            {
                theBlog.Image = file != null ? help.uploadfile(file) : img;
                theBlog.Thumbnail = thumb != null ? help.uploadfile(thumb1) : thumb;
                db.Entry(theBlog).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Bid = new SelectList(db.BlogCategories, "Bid", "Name", theBlog.Bid);
            return View(theBlog);
        }

        // GET: Auth/TheBlogs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TheBlog theBlog = await db.TheBlogs.FindAsync(id);
            if (theBlog == null)
            {
                return HttpNotFound();
            }
            return View(theBlog);
        }

        // POST: Auth/TheBlogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TheBlog theBlog = await db.TheBlogs.FindAsync(id);
            db.TheBlogs.Remove(theBlog);
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
