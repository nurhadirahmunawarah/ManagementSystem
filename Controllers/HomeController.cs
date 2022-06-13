
using ManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private ManagementSystemEntities db = new ManagementSystemEntities();

        public ActionResult Index()
        {

            int test = (int)Session["ID"];

            var numStudent = db.tb_student.Count();
            var numTutor = db.tb_user.Count();
            decimal salaryRate = db.tb_salaryRate.OrderByDescending(x => x.DateCreated).FirstOrDefault().SalaryRate.Value;
            var ClassNow = db.tb_class.Where(a => a.Date >= DateTime.Now && a.TutorID== test).Count();
            var ClassEnded = db.tb_class.Where(a => a.Date < DateTime.Now && a.TutorID == test).Count();

            ViewBag.numStudent = numStudent;
            ViewBag.numTutor = numTutor;
            ViewBag.salaryRate = salaryRate.ToString("0.00");
            ViewBag.MonthNow = DateTime.Now.ToString("MMM");
            ViewBag.DateNow = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
            ViewBag.ClassNow = ClassNow;
            ViewBag.ClassEnded = ClassEnded;

            return View();
        }

        public ActionResult About()
        {
            int studentid = (int)Session["ID"];

            var ClassNow = db.tb_class.Where(a => a.Date >= DateTime.Now && a.StudentID == studentid).Count();
            var ClassEnded = db.tb_class.Where(a => a.Date < DateTime.Now && a.StudentID == studentid).Count();

            ViewBag.MonthNow = DateTime.Now.ToString("MMM");
            ViewBag.DateNow = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
            ViewBag.ClassNow = ClassNow;
            ViewBag.ClassEnded = ClassEnded;


            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult TestChart()
        {

            return View();
        }
    }
}