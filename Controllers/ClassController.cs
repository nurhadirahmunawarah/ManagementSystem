using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManagementSystem.Models;

namespace ManagementSystem.Controllers
{
    public class ClassController : Controller
    {
        private ManagementSystemEntities db = new ManagementSystemEntities();

        //public static void Main()
        //{
        //    CultureInfo cultures = new CultureInfo("en-US");
        //    String val = "11/11/2019";
        //    Console.WriteLine("Converted DateTime value...");
        //    DateTime res = Convert.ToDateTime(val, cultures);
        //    Console.Write("{0}", res);
        //}

        // GET: Class
        public ActionResult Index()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            var tb_class = db.tb_class.Include(t => t.tb_package).Include(t => t.tb_user).Include(t => t.tb_student).OrderByDescending(t => t.Date).ThenByDescending(t => t.StartTime);

            return View(tb_class.ToList());
        }

        // GET: Class/Details/5
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
            tb_class tb_class = db.tb_class.Find(id);
            if (tb_class == null)
            {
                return HttpNotFound();
            }
            return View(tb_class);
        }

        // GET: Class/Create
        public ActionResult Create()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            ViewBag.Package = new SelectList(db.tb_package, "ID", "Name");
            ViewBag.TutorID = new SelectList(db.tb_user, "ID", "IC");
            ViewBag.StudentID = new SelectList(db.tb_student, "ID", "Name");
            return View();
        }

        // POST: Class/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Date,Duration,Package,TutorID,StudentID,Description,StartTime,RatingTutor, verifyStatus")] tb_class tb_class, int ID)
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            if (ModelState.IsValid)
            {

                if (tb_class.Description == null)
                {
                    tb_class.Description = "Tiada keterangan";
                }
                tb_class.verifyStatus = 2;
                tb_class.TutorID = ID;
                db.tb_class.Add(tb_class);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Package = new SelectList(db.tb_package, "ID", "Name", tb_class.Package);
            ViewBag.TutorID = new SelectList(db.tb_user, "ID", "IC", tb_class.TutorID);
            ViewBag.StudentID = new SelectList(db.tb_student, "ID", "Name", tb_class.StudentID);
            return View(tb_class);
        }

        // GET: Class/Edit/5
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
            tb_class tb_class = db.tb_class.Find(id);
            if (tb_class == null)
            {
                return HttpNotFound();
            }
            ViewBag.Package = new SelectList(db.tb_package, "ID", "Name", tb_class.Package);
            ViewBag.TutorID = new SelectList(db.tb_user, "ID", "IC", tb_class.TutorID);
            ViewBag.StudentID = new SelectList(db.tb_student, "ID", "Name", tb_class.StudentID);
            return View(tb_class);
        }

        // POST: Class/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Date,Duration,Description,Package,StartTime,StudentID")] tb_class tb_class)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            if (ModelState.IsValid)
            {   // only update nama pelajar, tarikh, waktu mula, durasi, pakej
                db.Entry(tb_class).State = EntityState.Modified;
                db.Entry(tb_class).Property(c => c.TutorID).IsModified = false;
                db.Entry(tb_class).Property(c => c.CheckIn).IsModified = false;
                db.Entry(tb_class).Property(c => c.CheckOut).IsModified = false;
                db.Entry(tb_class).Property(c => c.RatingTutor).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Package = new SelectList(db.tb_package, "ID", "Name", tb_class.Package);
            ViewBag.TutorID = new SelectList(db.tb_user, "ID", "IC", tb_class.TutorID);
            ViewBag.StudentID = new SelectList(db.tb_student, "ID", "Name", tb_class.StudentID);
            return View(tb_class);
        }

        // GET: Class/Delete/5
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
            tb_class tb_class = db.tb_class.Find(id);
            if (tb_class == null)
            {
                return HttpNotFound();
            }
            return View(tb_class);
        }

        // POST: Class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            tb_class tb_class = db.tb_class.Find(id);
            db.tb_class.Remove(tb_class);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("CheckIn")]
        public ActionResult CheckIn(int? id)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var c = new tb_class()
            {
                ID = (int)id,
                CheckIn = DateTime.Now
            };

            using (var db = new ManagementSystemEntities())
            {
                db.tb_class.Attach(c);
                db.Entry(c).Property(x => x.CheckIn).IsModified = true;
                db.SaveChanges();
            }

            return Json(new { checkIn = DateTime.Now.ToString("hh:mm tt") });
        }

        [HttpPost, ActionName("CheckOut")]
        public ActionResult CheckOut(int? id)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var c = new tb_class()
            {
                ID = (int)id,
                CheckOut = DateTime.Now
            };

            using (var db = new ManagementSystemEntities())
            {
                db.tb_class.Attach(c);
                db.Entry(c).Property(x => x.CheckOut).IsModified = true;
                db.SaveChanges();
            }

            return Json(new { checkOut = DateTime.Now.ToString("hh:mm tt") });
        }
        [HttpGet, ActionName("GetStatus")]
        public ActionResult GetStatus(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tb_class tb_class = db.tb_class.Find(id);

            if (tb_class.CheckIn == null)
            {
                return Json(new { status = "Belum Selesai" });
            }
            else
            {
                return Json(new { status = "Selesai" });
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost, ActionName("verify")]
        public ActionResult Verify(int? id)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var c = new tb_class()
            {
                ID = (int)id,
                verifyStatus = 1
            };

            using (var db = new ManagementSystemEntities())
            {
                db.tb_class.Attach(c);
                db.Entry(c).Property(x => x.verifyStatus).IsModified = true;
                db.SaveChanges();
            }

            return Json(new { verify = 1 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RateTutor2([Bind(Include = "ID,Date,Duration,Package,TutorID,StudentID,RatingTutor")] tb_class tb_class)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_class).State = EntityState.Modified;
                db.Entry(tb_class).Property(p => p.verifyStatus).IsModified = false;
                db.Entry(tb_class).Property(p => p.Description).IsModified = false;
                db.Entry(tb_class).Property(p => p.CheckOut).IsModified = false;
                db.Entry(tb_class).Property(p => p.CheckIn).IsModified = false;
                db.Entry(tb_class).Property(p => p.StartTime).IsModified = false;

                db.SaveChanges();
                return RedirectToAction("ViewClass", "Student", new { id = tb_class.StudentID });
            }
            ViewBag.Package = new SelectList(db.tb_package, "ID", "Name", tb_class.Package);
            ViewBag.TutorID = new SelectList(db.tb_user, "ID", "IC", tb_class.TutorID);
            ViewBag.StudentID = new SelectList(db.tb_student, "ID", "Name", tb_class.StudentID);
            return View(tb_class);
        }
        public ActionResult RateTutor2(int? id)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_class tb_class = db.tb_class.Find(id);
            if (tb_class == null)
            {
                return HttpNotFound();
            }
            ViewBag.Package = new SelectList(db.tb_package, "ID", "Name", tb_class.Package);
            ViewBag.TutorID = new SelectList(db.tb_user, "ID", "IC", tb_class.TutorID);
            ViewBag.StudentID = new SelectList(db.tb_student, "ID", "Name", tb_class.StudentID);
            return View(tb_class);
        }
    }
}
