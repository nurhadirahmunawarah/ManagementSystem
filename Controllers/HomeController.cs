
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

            string queryStudent = "SELECT COUNT(*) FROM tb_student;";
            string queryTutor = "SELECT COUNT(*) FROM tb_tutor;";
            string queryBatch = "SELECT COUNT(*) FROM tb_batch;";

            var numStudent = db.tb_student.Count();
            var numTutor = db.tb_user.Count();
            var numBatch = db.tb_student.Count();

            ViewBag.numStudent = numStudent;
            ViewBag.numTutor = numTutor;
            ViewBag.numBatch = numBatch;
            ViewBag.MonthNow = DateTime.Now.ToString("MMM");

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}