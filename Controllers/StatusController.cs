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
    public class StatusController : Controller
    {
        private ManagementSystemEntities db = new ManagementSystemEntities();

        // GET: Status
        public ActionResult Index()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            return View(db.tb_status.ToList());
        }

        // GET: Status/Details/5
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
            tb_status tb_status = db.tb_status.Find(id);
            if (tb_status == null)
            {
                return HttpNotFound();
            }
            return View(tb_status);
        }

        // GET: Status/Create
        public ActionResult Create()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            return View();
        }

        // POST: Status/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description")] tb_status tb_status)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            if (ModelState.IsValid)
            {
                db.tb_status.Add(tb_status);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_status);
        }

        // GET: Status/Edit/5
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
            tb_status tb_status = db.tb_status.Find(id);
            if (tb_status == null)
            {
                return HttpNotFound();
            }
            return View(tb_status);
        }

        // POST: Status/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description")] tb_status tb_status)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            if (ModelState.IsValid)
            {
                db.Entry(tb_status).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_status);
        }

        // GET: Status/Delete/5
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
            tb_status tb_status = db.tb_status.Find(id);
            if (tb_status == null)
            {
                return HttpNotFound();
            }
            return View(tb_status);
        }

        // POST: Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            tb_status tb_status = db.tb_status.Find(id);
            db.tb_status.Remove(tb_status);
            db.SaveChanges();
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
