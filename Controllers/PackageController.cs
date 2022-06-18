using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManagementSystem.Models;
using static ManagementSystem.Models.tb_package;

namespace ManagementSystem.Controllers
{
    public class PackageController : Controller
    {
        private ManagementSystemEntities db = new ManagementSystemEntities();

        // GET: Package
        public ActionResult Index()
        {

            var test = db.tb_student.GroupBy(t => t.Package).Select(packageGroup => new testPackage { Package1 = (int)packageGroup.Key, StudentCount = packageGroup.Count() }).ToList();
            ViewBag.test = test;

            var tb_package = db.tb_package.Include(t => t.tb_student);

            return View(tb_package.ToList());
        }

        // GET: Package/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_package tb_package = db.tb_package.Find(id);
            if (tb_package == null)
            {
                return HttpNotFound();
            }
            return View(tb_package);
        }

        // GET: Package/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Package/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Masa,Sesi,Fee,Description")] tb_package tb_package)
        {
            if (ModelState.IsValid)
            {
                db.tb_package.Add(tb_package);
                db.SaveChanges();
                TempData["AlertMessage"] = "Rekod pakej berjaya disimpan.";
                return RedirectToAction("Index");
            }

            return View(tb_package);
        }

        // GET: Package/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_package tb_package = db.tb_package.Find(id);
            if (tb_package == null)
            {
                return HttpNotFound();
            }
            return View(tb_package);
        }

        // POST: Package/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Masa,Sesi,Fee,Description")] tb_package tb_package)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_package).State = EntityState.Modified;
                db.SaveChanges();
                TempData["AlertMessage"] = "Rekod pakej berjaya dikemaskini.";
                return RedirectToAction("Index");
            }
            return View(tb_package);
        }

        // GET: Package/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_package tb_package = db.tb_package.Find(id);
            if (tb_package == null)
            {
                return HttpNotFound();
            }
            return View(tb_package);
        }

        // POST: Package/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_package tb_package = db.tb_package.Find(id);
            db.tb_package.Remove(tb_package);
            db.SaveChanges();
            TempData["AlertMessage"] = "Rekod pakej berjaya dipadam.";
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
