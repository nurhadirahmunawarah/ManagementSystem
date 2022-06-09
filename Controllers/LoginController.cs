using ManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
                
                var userDetails = db.tb_user.Where(x => x.IC == userModel.IC).FirstOrDefault(); // x.Password == userModel.Password VerifyHashedPassword(x.Password, userModel.Password)
                var PasswordCorrect = VerifyHashedPassword(userDetails.Password, userModel.Password);


                if(PasswordCorrect == false)
                {
                    userModel.LoginErrorMessage = "Pengguna tidak dijumpa";
                    return View("Index", userModel);
                }

                if (userDetails ==null)
                {
                    userModel.LoginErrorMessage = "Salah nombor kad pengenalan atau kata laluan";
                    return View("Index", userModel);
                }
                else
                {
                    Session["IC"] = userDetails.IC;
                    Session["Name"] = userDetails.Name;
                    Session["Role"] = userDetails.Role;
                    return RedirectToAction("Index", "Home");
                }
               

            }
           
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }

        public static bool ByteArraysEqual(byte[] b1, byte[] b2)
        {
            if (b1 == b2) return true;
            if (b1 == null || b2 == null) return false;
            if (b1.Length != b2.Length) return false;
            for (int i = 0; i < b1.Length; i++)
            {
                if (b1[i] != b2[i]) return false;
            }
            return true;
        }


        public ActionResult Logout()
        {
           
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index","Login");
        }
    }
}