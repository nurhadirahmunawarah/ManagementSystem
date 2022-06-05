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
    public class BatchesController : Controller
    {
        private ManagementSystemEntities db = new ManagementSystemEntities();

        // GET: Batches
        public ActionResult Index()
        {
            return View(db.tb_batches.ToList());
        }

        // GET: Batches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_batches tb_batches = db.tb_batches.Find(id);
            if (tb_batches == null)
            {
                return HttpNotFound();
            }
            return View(tb_batches);
        }

        // GET: Batches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Batches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CreateDate")] tb_batches tb_batches)
        {
            if (ModelState.IsValid)
            {
                db.tb_batches.Add(tb_batches);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_batches);
        }

        // GET: Batches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_batches tb_batches = db.tb_batches.Find(id);
            if (tb_batches == null)
            {
                return HttpNotFound();
            }
            return View(tb_batches);
        }

        // POST: Batches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CreateDate")] tb_batches tb_batches)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_batches).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_batches);
        }

        // GET: Batches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_batches tb_batches = db.tb_batches.Find(id);
            if (tb_batches == null)
            {
                return HttpNotFound();
            }
            return View(tb_batches);
        }

        // POST: Batches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_batches tb_batches = db.tb_batches.Find(id);
            db.tb_batches.Remove(tb_batches);
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
