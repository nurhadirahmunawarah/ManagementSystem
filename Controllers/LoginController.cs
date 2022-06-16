using ManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using ManagementSystem.Services;
using System.Net;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Web.Security;

namespace ManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly EmailService emailService = new EmailService();
        private readonly ManagementSystemEntities db = new ManagementSystemEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(ManagementSystem.Models.tb_user userModel)
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
                Session["ID"] = userDetails.ID;
                return RedirectToAction("Index", "Home");
            }
        }


        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Forgot(ManagementSystem.Models.tb_user userModel)
        {
            var userEmail = db.tb_user.Where(x => x.Email == userModel.Email).FirstOrDefault();
            if(userEmail == null)
            {
                userModel.LoginErrorMessage = "Emel tidak wujud";
                return View("ForgotPassword", userModel);
            }
            else
            {
                string emelText = "Pautan untuk mengubah kata laluan akaun anda: https://localhost:44368/Login/Reset/" + userEmail.ID.ToString();
                emailService.Send("mengaji121@yahoo.com",userEmail.Email,"Ubah Kata Laluan Sistem", emelText);
            }
            return RedirectToAction("Index","Login");
        }

        public ActionResult Reset(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            tb_user tb_User = db.tb_user.Find(id);

            if(tb_User == null)
            {
                return HttpNotFound();
            }

            return View();
        }

        [HttpPost, ActionName("Reset")]
        public ActionResult ResetPasswordPost(int? id, string Password)
        {
            var passToUpdate = db.tb_user.FirstOrDefault(s => s.ID == id);
            
            var unhashedPass = Password;
            Password = HashPassword(unhashedPass);

            passToUpdate.Password = Password;

            if (TryUpdateModel<tb_user>(passToUpdate,"",null,new [] {"Password"}))
            {
                try{
                    db.SaveChanges();
                    return RedirectToAction("Index", "Login");
                }catch (DbUpdateException /* ex */){
                    return RedirectToAction("Index", "Forgot");
                }
            }
            return View(passToUpdate);
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



        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["IC"] = null;
            Session["Name"] = null;
            Session["Role"] = null;
            Session["ID"] = null;
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index", "Login");

        }
    }
}