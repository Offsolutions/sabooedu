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
    public class SettingsController : Controller
    {
        private dbcontext db = new dbcontext();
        public static string img = "";
        // GET: Auth/Settings
        public async Task<ActionResult> Index()
        {
            return View(await db.Settings.ToListAsync());
        }

        // GET: Auth/Settings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Setting setting = await db.Settings.FindAsync(id);
            if (setting == null)
            {
                return HttpNotFound();
            }
            return View(setting);
        }

        // GET: Auth/Settings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auth/Settings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Sid,ContactPerson,Contact,BusinessContact,Whatsapp,Email,Address,BusinessEmai,Password,Logo")] Setting setting, HttpPostedFileBase file, Helper help)
        {
            if (ModelState.IsValid)
            {
                setting.Logo = help.uploadfile(file);
                db.Settings.Add(setting);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(setting);
        }

        // GET: Auth/Settings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Setting setting = await db.Settings.FindAsync(id);
            img = setting.Logo;
            if (setting == null)
            {
                return HttpNotFound();
            }
            return View(setting);
        }

        // POST: Auth/Settings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Sid,ContactPerson,Contact,BusinessContact,Whatsapp,Email,Address,BusinessEmai,Password,Logo")] Setting setting, HttpPostedFileBase file, Helper help)
        {
            if (ModelState.IsValid)
            {
                setting.Logo = file != null ? help.uploadfile(file) : img;
                db.Entry(setting).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(setting);
        }

        // GET: Auth/Settings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Setting setting = await db.Settings.FindAsync(id);
            if (setting == null)
            {
                return HttpNotFound();
            }
            return View(setting);
        }

        // POST: Auth/Settings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Setting setting = await db.Settings.FindAsync(id);
            db.Settings.Remove(setting);
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
