using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Web;
using Google.Apis.Util;
using Google.Apis.Util.Store;
using MailKit.Net.Smtp;
using MailKit.Security;
using ManagementSystem.Models;
using MimeKit;
using MimeKit.Text;

namespace ManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private ManagementSystemEntities db = new ManagementSystemEntities();

        // GET: User
        public ActionResult Index()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            var tb_user = db.tb_user.Include(t => t.tb_status);
            return View(tb_user.ToList());
        }

        // GET: User/Details/5
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

        // GET: User/Create
        public ActionResult Create()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            ViewBag.Status = new SelectList(db.tb_status, "ID", "Description");
           
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IC,Name,Role,Password,Email,Contact,Status,BatchID")] tb_user tb_user)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            if (ModelState.IsValid)
            {
                var unhashedPass = tb_user.Password;
                tb_user.Password = HashPassword(unhashedPass);
                db.tb_user.Add(tb_user);
                db.SaveChanges();
                TempData["AlertMessage"] = "Rekod pengguna berjaya disimpan.";
                return RedirectToAction("Index");
            }

            ViewBag.Status = new SelectList(db.tb_status, "ID", "Description", tb_user.Status);

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
            ViewBag.Status = new SelectList(db.tb_status, "ID", "Description", tb_user.Status);
           
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
                TempData["AlertMessage"] = "Rekod pengguna berjaya disimpan.";
                return RedirectToAction("Index");
            }
            ViewBag.Status = new SelectList(db.tb_status, "ID", "Description", tb_user.Status);

            return View(tb_user);
        }

        // GET: User/Delete/5
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
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

            tb_user tb_user = db.tb_user.Find(id);
            var classes = db.tb_class.Where(x => x.TutorID == id);
            var salary = db.tb_salary.Where(x => x.TutorID == id);
            db.tb_class.RemoveRange(classes);
            db.tb_salary.RemoveRange(salary);
            db.tb_user.Remove(tb_user);
            db.SaveChanges();
            TempData["AlertMessage"] = "Rekod pengguna berjaya dipadam.";


            ////email code



            //// create email message
            //var email = new MimeMessage();
            //email.From.Add(MailboxAddress.Parse("mengaji121@yahoo.com"));
            //email.To.Add(MailboxAddress.Parse("arifffansurirazak@gmail.com"));
            //email.Subject = "Test Email Subject";
            //email.Body = new TextPart(TextFormat.Plain) { Text = "Example Plain Text Message Body" };

            //// send email
            //using (var smtp = new SmtpClient())
            //{
            //    smtp.Connect("smtp.mail.yahoo.com", 587, SecureSocketOptions.StartTls);
            //    smtp.Authenticate("mengaji121@yahoo.com", "oypqfmgwbomycrzs");
            //    smtp.Send(email);
            //    smtp.Disconnect(true);
            //}

            ////end email code

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

        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }
    }
}
