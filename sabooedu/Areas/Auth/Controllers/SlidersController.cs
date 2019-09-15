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
    public class SlidersController : Controller
    {
        private dbcontext db = new dbcontext();
        public static string img = "";
        // GET: Auth/Sliders
        public async Task<ActionResult> Index()
        {
            return View(await db.Sliders.ToListAsync());
        }

        // GET: Auth/Sliders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = await db.Sliders.FindAsync(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: Auth/Sliders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auth/Sliders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,Name,Description,Image")] Slider slider, HttpPostedFileBase file, Helper help)
        {
            if (ModelState.IsValid)
            {
                slider.Image = help.uploadfile(file);
                db.Sliders.Add(slider);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(slider);
        }

        // GET: Auth/Sliders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = await db.Sliders.FindAsync(id);
            img = slider.Image;
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Auth/Sliders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,Name,Description,Image")] Slider slider, HttpPostedFileBase file, Helper help)
        {
            if (ModelState.IsValid)
            {
                slider.Image = file != null ? help.uploadfile(file) : img;
                db.Entry(slider).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        // GET: Auth/Sliders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = await db.Sliders.FindAsync(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Auth/Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Slider slider = await db.Sliders.FindAsync(id);
            db.Sliders.Remove(slider);
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
