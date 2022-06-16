
using ManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

            if(Session["Role"] == null)
{
                return RedirectToAction("Index", "MainPage");
            }
            int test = (int)Session["ID"];

            var numStudent = db.tb_student.Count();
            var numTutor = db.tb_user.Count();
            decimal salaryRate = db.tb_salaryRate.OrderByDescending(x => x.DateCreated).FirstOrDefault().SalaryRate.Value;
            var ClassNow = db.tb_class.Where(a => a.Date >= DateTime.Now && a.TutorID == test).Count();
            var ClassEnded = db.tb_class.Where(a => a.Date < DateTime.Now && a.verifyStatus == 1 && a.TutorID == test).Count();
            var totalstu = db.tb_student.Where(a => a.Date.Value.Month == DateTime.Now.Month).Count();

            ViewBag.numStudent = numStudent;
            ViewBag.numTutor = numTutor;
            ViewBag.salaryRate = salaryRate.ToString("0.00");
            ViewBag.MonthNow = DateTime.Now.ToString("MMM");
            ViewBag.DateNow = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
            ViewBag.ClassNow = ClassNow;
            ViewBag.ClassEnded = ClassEnded;
            ViewBag.totalstu= totalstu;

            return View();
        }

        public ActionResult About()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }

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
            if (Session["Role"] == null)
            {
                return RedirectToAction("Index", "MainPage");
            }


            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult TestChart()
        {

            return View();
        }

        public ActionResult GetData()
        {


            int adminCount = db.tb_user.Where(x => x.Role == 1).Count();
            int tutorCount = db.tb_user.Where(x => x.Role == 2).Count();
            int stuCount = db.tb_student.Count();

            Ratio obj = new Ratio();
            obj.Admin = adminCount;
            obj.Tutor = tutorCount;
            obj.Student = stuCount;


            return Json(obj, JsonRequestBehavior.AllowGet);

        }
        public class Ratio
        {
            public int Admin
            { get; set; }
            public int Tutor
            { get; set; }
            public int Student
            { get; set; }
        }

        public ActionResult GetData1()
        {
           
           
                var query = db.tb_student.GroupBy(s => s.Date.Value.Day).Select(g => new { date = g.Key, count = g.Count() }).ToList();

                return Json(query, JsonRequestBehavior.AllowGet);
            
        }
      
        public ActionResult GetData2()
        {
            int rating1 = db.tb_class.Where(x => x.RatingTutor == 1).Count();
            int rating2 = db.tb_class.Where(x => x.RatingTutor == 2).Count();
            int rating3 = db.tb_class.Where(x => x.RatingTutor == 3).Count();
            int rating4 = db.tb_class.Where(x => x.RatingTutor == 4).Count();
            int rating5 = db.tb_class.Where(x => x.RatingTutor == 5).Count();
            Ratio1 obj = new Ratio1();
            obj.rating1 = rating1;
            obj.rating2 = rating2;
            obj.rating3 = rating3;
            obj.rating4 = rating4;
            obj.rating5 = rating5;


            return Json(obj, JsonRequestBehavior.AllowGet);


        }

        public class Ratio1
        {
            public int rating1
            { get; set; }
            public int rating2
            { get; set; }
            public int rating3
            { get; set; }
            public int rating4
            { get; set; }
            public int rating5
            { get; set; }
        }
        public ActionResult GetData3()
        {
            int test = (int)Session["ID"];
            var rating1s = db.tb_performance.Where( a=> a.StudentID== test && a.ratingStudent==1 ).Count();
            var rating2s = db.tb_performance.Where(a => a.StudentID == test && a.ratingStudent == 2).Count();
            var rating3s = db.tb_performance.Where(a => a.StudentID == test && a.ratingStudent == 3).Count();
            var rating4s = db.tb_performance.Where(a => a.StudentID == test && a.ratingStudent == 4).Count();
            var rating5s = db.tb_performance.Where(a => a.StudentID == test && a.ratingStudent == 5).Count();


            Ratio1 obj = new Ratio1();
            obj.rating1 = rating1s;
            obj.rating2 = rating2s;
            obj.rating3 = rating3s;
            obj.rating4 = rating4s;
            obj.rating5 = rating5s;

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    




    }
}