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
    public class UserController : Controller
    {
        private ManagementSystemEntities db = new ManagementSystemEntities();

        // GET: User
        public ActionResult Index()
        {
            var tb_user = db.tb_user.Include(t => t.tb_status).Include(t => t.tb_batches);
            return View(tb_user.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_user tb_user = db.tb_user.Find(id);
            if (tb_user == null)
            {
                return HttpNotFound();
            }
            return View(tb_user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            ViewBag.Status = new SelectList(db.tb_status, "ID", "Description");
            ViewBag.BatchID = new SelectList(db.tb_batches, "ID", "ID");
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IC,Name,Role,Password,Email,Contact,Status,BatchID")] tb_user tb_user)
        {
            if (ModelState.IsValid)
            {
                db.tb_user.Add(tb_user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Status = new SelectList(db.tb_status, "ID", "Description", tb_user.Status);
            ViewBag.BatchID = new SelectList(db.tb_batches, "ID", "ID", tb_user.BatchID);
            return View(tb_user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_user tb_user = db.tb_user.Find(id);
            if (tb_user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Status = new SelectList(db.tb_status, "ID", "Description", tb_user.Status);
            ViewBag.BatchID = new SelectList(db.tb_batches, "ID", "ID", tb_user.BatchID);
            return View(tb_user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IC,Name,Role,Password,Email,Contact,Status,BatchID")] tb_user tb_user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Status = new SelectList(db.tb_status, "ID", "Description", tb_user.Status);
            ViewBag.BatchID = new SelectList(db.tb_batches, "ID", "ID", tb_user.BatchID);
            return View(tb_user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_user tb_user = db.tb_user.Find(id);
            if (tb_user == null)
            {
                return HttpNotFound();
            }
            return View(tb_user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_user tb_user = db.tb_user.Find(id);
            db.tb_user.Remove(tb_user);
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
