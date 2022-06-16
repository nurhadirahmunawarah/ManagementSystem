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
    public class PerformanceController : Controller
    {
        private ManagementSystemEntities db = new ManagementSystemEntities();

        // GET: Performance
        public ActionResult Index()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            var tb_performance = db.tb_performance.Include(t => t.tb_student);
            return View(tb_performance.ToList());
        }

        // GET: Performance/Details/5
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
            tb_performance tb_performance = db.tb_performance.Find(id);
            if (tb_performance == null)
            {
                return HttpNotFound();
            }
            return View(tb_performance);
        }

        // GET: Performance/Create
        public ActionResult Create()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            ViewBag.StudentID = new SelectList(db.tb_student, "ID", "Name");
            return View();
        }

        // POST: Performance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Remark,StudentID,DateCreated,ratingStudent")] tb_performance tb_performance)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            if (ModelState.IsValid)
            {
                db.tb_performance.Add(tb_performance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentID = new SelectList(db.tb_student, "ID", "Name", tb_performance.StudentID);
            return View(tb_performance);
        }

        // GET: Performance/Edit/5
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
            tb_performance tb_performance = db.tb_performance.Find(id);
            if (tb_performance == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentID = new SelectList(db.tb_student, "ID", "Name", tb_performance.StudentID);
            return View(tb_performance);
        }

        // POST: Performance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Remark,StudentID,ratingStudent,ratingStudent")] tb_performance tb_performance)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            if (ModelState.IsValid)
            {
                db.Entry(tb_performance).State = EntityState.Modified;
                db.Entry(tb_performance).Property(p => p.DateCreated).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentID = new SelectList(db.tb_student, "ID", "Name", tb_performance.StudentID);
            return View(tb_performance);
        }

        // GET: Performance/Delete/5
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
            tb_performance tb_performance = db.tb_performance.Find(id);
            if (tb_performance == null)
            {
                return HttpNotFound();
            }
            return View(tb_performance);
        }

        // POST: Performance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            tb_performance tb_performance = db.tb_performance.Find(id);
            db.tb_performance.Remove(tb_performance);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ViewPerformance()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            var tb_performance = db.tb_performance.Include(t => t.tb_student);
            return View(tb_performance.ToList());
        }

        public ActionResult ViewPerDetails(int? id)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_performance tb_performance = db.tb_performance.Find(id);
            if (tb_performance == null)
            {
                return HttpNotFound();
            }
            return View(tb_performance);
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
