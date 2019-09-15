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
    public class SocialMediasController : Controller
    {
        private dbcontext db = new dbcontext();

        // GET: Auth/SocialMedias
        public async Task<ActionResult> Index()
        {
            return View(await db.SocialMedias.ToListAsync());
        }

        // GET: Auth/SocialMedias/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialMedia socialMedia = await db.SocialMedias.FindAsync(id);
            if (socialMedia == null)
            {
                return HttpNotFound();
            }
            return View(socialMedia);
        }

        // GET: Auth/SocialMedias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auth/SocialMedias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,Platform,Icon,Url")] SocialMedia socialMedia)
        {
            if (ModelState.IsValid)
            {
                db.SocialMedias.Add(socialMedia);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(socialMedia);
        }

        // GET: Auth/SocialMedias/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialMedia socialMedia = await db.SocialMedias.FindAsync(id);
            if (socialMedia == null)
            {
                return HttpNotFound();
            }
            return View(socialMedia);
        }

        // POST: Auth/SocialMedias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,Platform,Icon,Url")] SocialMedia socialMedia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(socialMedia).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(socialMedia);
        }

        // GET: Auth/SocialMedias/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialMedia socialMedia = await db.SocialMedias.FindAsync(id);
            if (socialMedia == null)
            {
                return HttpNotFound();
            }
            return View(socialMedia);
        }

        // POST: Auth/SocialMedias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SocialMedia socialMedia = await db.SocialMedias.FindAsync(id);
            db.SocialMedias.Remove(socialMedia);
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
