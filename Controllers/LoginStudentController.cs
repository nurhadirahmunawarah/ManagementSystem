using ManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace ManagementSystem.Controllers
{
    public class LoginStudentController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize1(ManagementSystem.Models.tb_student userModel)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var userDetails = db.tb_student.Where(x => x.RefNo == userModel.RefNo && x.IC == userModel.IC).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Salah nombor kod pelajar atau IC untuk kata laluan";
                    return View("Index", userModel);
                }
                else
                {
                    Session["ID"] = userDetails.ID;
                    Session["RefNo"] = userDetails.RefNo;
                    Session["Name"] = userDetails.Name;
                    Session["Role"] = 3;

                    return RedirectToAction("About", "Home");
                }
            }

        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            System.Web.Security.FormsAuthentication.SignOut();
            this.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            this.Response.Cache.SetNoStore();


            //Session.Abandon();
            //Session.Clear();
            return RedirectToAction("Index", "LoginStudent");
        }
    }
}