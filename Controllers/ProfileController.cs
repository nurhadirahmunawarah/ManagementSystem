using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManagementSystem.Models;


namespace ManagementSystem.Controllers
{
    public class ProfileController : Controller
    {
        private ManagementSystemEntities db = new ManagementSystemEntities();

        // GET: Profile
        public ActionResult Index()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }
            return View();
        }

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
            tb_user tb_user = db.tb_user.Find(id);
            if (tb_user == null)
            {
                return HttpNotFound();
            }
            return View(tb_user);
        }

        // GET: User/Edit/5
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
            tb_user tb_user = db.tb_user.Find(id);
            if (tb_user == null)
            {
                return HttpNotFound();
            }
            return View(tb_user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IC,Name,Role,Password,Email,Contact,Status,BatchID")] tb_user tb_user)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            if (ModelState.IsValid)
            {
                db.Entry(tb_user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Profile", new { id = Session["ID"] });
            }
           
            ViewBag.Status = new SelectList(db.tb_status, "ID", "Description", tb_user.Status);

            return View(tb_user);
        }

        public ActionResult DetailsStudent(int? id)
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

        public ActionResult EditStudent(int? id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStudent([Bind(Include = "ID,Name,IC,Address,Date,Package,BatchID,RefNo")] tb_student tb_student)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            if (ModelState.IsValid)
            {
                db.Entry(tb_student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DetailsStudent", "Profile", new { id = Session["ID"] });
             
            }
            ViewBag.Package = new SelectList(db.tb_package, "ID", "Name", tb_student.Package);
            return View(tb_student);
        }
    }
}

