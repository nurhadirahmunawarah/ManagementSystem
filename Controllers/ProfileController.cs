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
            return View();
        }

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
            return View(tb_user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IC,Name,Email,Contact")] tb_user tb_user)
        {
            if (ModelState.IsValid)
            {   // only update nama pelajar, tarikh, waktu mula, durasi, pakej
                db.Entry(tb_user).State = EntityState.Modified;
                db.Entry(tb_user).Property(c => c.Role).IsModified = false;
                db.Entry(tb_user).Property(c => c.Status).IsModified = false;
                db.Entry(tb_user).Property(c => c.BatchID).IsModified = false;
      
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(tb_user);
        }
    }
}
