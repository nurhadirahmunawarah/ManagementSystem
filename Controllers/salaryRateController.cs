using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManagementSystem.Models;

namespace ManagementSystem.Controllers
{
    public class salaryRateController : Controller
    {
        private ManagementSystemEntities db = new ManagementSystemEntities();

        // GET: salaryRate
        public ActionResult Index()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            return View(db.tb_salaryRate.ToList());
        }

        // GET: salaryRate/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_salaryRate tb_salaryRate = db.tb_salaryRate.Find(id);
            if (tb_salaryRate == null)
            {
                return HttpNotFound();
            }
            return View(tb_salaryRate);
        }

        // GET: salaryRate/Create
        public ActionResult Create()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            return View();
        }

        // POST: salaryRate/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DateCreated,SalaryRate")] tb_salaryRate tb_salaryRate)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            if (ModelState.IsValid)
            {
                tb_salaryRate.DateCreated = DateTime.Now;
                db.tb_salaryRate.Add(tb_salaryRate);
                db.SaveChanges();
                TempData["AlertMessage"] = "Rekod kadar gaji berjaya disimpan.";
                return RedirectToAction("Index");
            }

            return View(tb_salaryRate);
        }

        // GET: salaryRate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_salaryRate tb_salaryRate = db.tb_salaryRate.Find(id);
            if (tb_salaryRate == null)
            {
                return HttpNotFound();
            }
            return View(tb_salaryRate);
        }

        // POST: salaryRate/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DateCreated,SalaryRate")] tb_salaryRate tb_salaryRate)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            if (ModelState.IsValid)
            {
                db.Entry(tb_salaryRate).State = EntityState.Modified;
                db.SaveChanges();
                TempData["AlertMessage"] = "Rekod kadar gaji berjaya dikemaskini.";
                return RedirectToAction("Index");
            }
            return View(tb_salaryRate);
        }

        // GET: salaryRate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_salaryRate tb_salaryRate = db.tb_salaryRate.Find(id);
            if (tb_salaryRate == null)
            {
                return HttpNotFound();
            }
            return View(tb_salaryRate);
        }

        // POST: salaryRate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            tb_salaryRate tb_salaryRate = db.tb_salaryRate.Find(id);
            db.tb_salaryRate.Remove(tb_salaryRate);
            db.SaveChanges();
            TempData["AlertMessage"] = "Rekod kadar gaji berjaya dipadam.";
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
