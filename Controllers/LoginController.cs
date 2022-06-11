using ManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace ManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(ManagementSystem.Models.tb_user userModel)
        {
            using(ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var userDetails = db.tb_user.Where(x => x.IC == userModel.IC && x.Password == userModel.Password).FirstOrDefault();
                if(userDetails ==null)
                {
                    userModel.LoginErrorMessage = "Salah nombor kad pengenalan atau kata laluan";
                    return View("Index", userModel);
                }
                else
                {
                    Session["IC"] = userDetails.IC;
                    Session["Name"] = userDetails.Name;
                    Session["Role"] = userDetails.Role;
                    Session["ID"] = userDetails.ID;
                    return RedirectToAction("Index", "Home");
                }
            }
           
        }

        public ActionResult Logout()
        {
           
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index","Login");
        }
    }
}