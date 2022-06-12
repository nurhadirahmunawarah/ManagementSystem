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
    public class SalaryController : Controller
    {
        private ManagementSystemEntities db = new ManagementSystemEntities();

        // GET: Salary
        public ActionResult Index()
        {
            var tb_salary = db.tb_salary.Include(t => t.tb_user);
            return View(tb_salary.ToList());
        }

        // GET: Salary/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_salary tb_salary = db.tb_salary.Find(id);
            if (tb_salary == null)
            {
                return HttpNotFound();
            }
            return View(tb_salary);
        }

        // GET: Salary/Create
        public ActionResult Create()
        {
            ViewBag.TutorID = new SelectList(db.tb_user, "ID", "IC");
            

            var clients = db.tb_user
                .Select(s => new
                {
                    Text = s.Name + " - " + s.IC,
                    Value = s.ID
                })
                .ToList();

            ViewBag.TutorID = new SelectList(clients, "Value", "Text");
            return View();
        }

        // POST: Salary/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Amount,TutorID,month,Status,Date")] tb_salary tb_salary)
        {
            var listClasses = db.tb_class.Where(s => s.Date.Month == tb_salary.month && s.TutorID == tb_salary.TutorID).Select(s=>s.Duration).DefaultIfEmpty().Sum();
            var rateSalary = db.tb_salaryRate.OrderByDescending(x => x.DateCreated).FirstOrDefault().SalaryRate;

            if(listClasses==0)
            {
                return RedirectToAction("Index");

            }

            var SalaryAmount = (decimal)listClasses * rateSalary / 60;

            if (ModelState.IsValid)
            {
                tb_salary.Amount = (double?)SalaryAmount;
                db.tb_salary.Add(tb_salary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TutorID = new SelectList(db.tb_user, "ID", "IC", tb_salary.TutorID);
            return View(tb_salary);
        }

        // GET: Salary/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_salary tb_salary = db.tb_salary.Find(id);
            if (tb_salary == null)
            {
                return HttpNotFound();
            }
            ViewBag.TutorID = new SelectList(db.tb_user, "ID", "IC", tb_salary.TutorID);
            return View(tb_salary);
        }

        // POST: Salary/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Amount,TutorID,month,Status,Date")] tb_salary tb_salary)
        {
            var listClasses = db.tb_class.Where(s => s.Date.Month == tb_salary.month && s.TutorID == tb_salary.TutorID).Select(s => s.Duration).DefaultIfEmpty().Sum();
            var rateSalary = db.tb_salaryRate.OrderByDescending(x => x.DateCreated).FirstOrDefault().SalaryRate;

            if (listClasses==0)
            {
                return RedirectToAction("Index");
            }

            var salaryAmount = (decimal)listClasses * rateSalary / 60;
            if(tb_salary.Status==null)
            {
                ModelState.AddModelError("Status", "Status tidak Diisi");
                return View(tb_salary);
            }
            if (ModelState.IsValid)
            {
                db.Entry(tb_salary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TutorID = new SelectList(db.tb_user, "ID", "IC", tb_salary.TutorID);
            return View(tb_salary);
        }

        // GET: Salary/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_salary tb_salary = db.tb_salary.Find(id);
            if (tb_salary == null)
            {
                return HttpNotFound();
            }
            return View(tb_salary);
        }

        public ActionResult ViewInvoice (int?id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var invoice = db.tb_salary.Include(t => t.tb_user).FirstOrDefault(x => x.ID == id);
            return View(invoice);

        }
        // POST: Salary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_salary tb_salary = db.tb_salary.Find(id);
            db.tb_salary.Remove(tb_salary);
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
