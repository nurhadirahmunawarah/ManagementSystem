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
    public class StudentController : Controller
    {
        private ManagementSystemEntities db = new ManagementSystemEntities();

        // GET: Student
        public ActionResult Index()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            var tb_student = db.tb_student.Include(t => t.tb_package);
            return View(tb_student.ToList());
        }

        // GET: Student/Details/5
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
            tb_student tb_student = db.tb_student.Find(id);
            if (tb_student == null)
            {
                return HttpNotFound();
            }
            return View(tb_student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            ViewBag.Package = new SelectList(db.tb_package, "ID", "Name");
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,IC,Address,Date,Package,BatchID,RefNo")] tb_student tb_student)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            if (ModelState.IsValid)
            {
                db.tb_student.Add(tb_student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Package = new SelectList(db.tb_package, "ID", "Name", tb_student.Package);
            return View(tb_student);
        }

        // GET: Student/Edit/5
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
            tb_student tb_student = db.tb_student.Find(id);
            if (tb_student == null)
            {
                return HttpNotFound();
            }
        
            ViewBag.Package = new SelectList(db.tb_package, "ID", "Name", tb_student.Package);
            return View(tb_student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,IC,Address,Date,Package,BatchID,RefNo")] tb_student tb_student)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            if (ModelState.IsValid)
            {
                db.Entry(tb_student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Package = new SelectList(db.tb_package, "ID", "Name", tb_student.Package);
            return View(tb_student);
        }

        // GET: Student/Delete/5
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
            tb_student tb_student = db.tb_student.Find(id);
            if (tb_student == null)
            {
                return HttpNotFound();
            }
            return View(tb_student);
        }
 

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            tb_student tb_student = db.tb_student.Find(id);
            var classes = db.tb_class.Where(x => x.StudentID == id);
            var performances = db.tb_performance.Where(x => x.StudentID == id);
            db.tb_class.RemoveRange(classes);
            db.tb_performance.RemoveRange(performances);
            db.tb_student.Remove(tb_student);
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

        public ActionResult ViewReport(int? id)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var Sreport = db.tb_student.Include(p => p.tb_performance).Include(c => c.tb_class).FirstOrDefault(x => x.ID == id);

            return View(Sreport);
        }
        public ActionResult ViewClass(int? id)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var Sclass = db.tb_student.Include(c => c.tb_class).FirstOrDefault(x => x.ID == id);

            return View(Sclass);
        }
        public ActionResult MyPerformance(int? id)
        {
             if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var Sperformance = db.tb_student.Include(c => c.tb_performance).FirstOrDefault(x => x.ID == id);

            return View(Sperformance);
        }





    }
}
