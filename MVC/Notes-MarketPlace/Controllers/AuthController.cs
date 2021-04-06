using System;
using System.Collections.Generic;
using System.Linq;
using Notes_MarketPlace.Models;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Text;
using System.Data;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Notes_MarketPlace.Database;

namespace Notes_MarketPlace.Controllers
{
    public class AuthController : Controller
    {
        private DatabaseFile db = new DatabaseFile();

        [HttpGet]
        public ActionResult Index()
        {
            Member member = new Member();
            if(Request.Cookies["Username"] != null && Request.Cookies["Password"] != null)
            {
                member.Email = Request.Cookies["Username"].Value;
                member.Password = Request.Cookies["Password"].Value;
                member.Remember = true;
            }
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorStatus = true;
                ViewBag.SucessStatus = false;
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }
            else if (TempData["SucessMessage"] != null)
            {
                ViewBag.ErrorStatus = false;
                ViewBag.SucessStatus = true;
                ViewBag.SucessMessage= TempData["SucessMessage"];
            }
            else 
            {
                ViewBag.ErrorStatus = false;
                ViewBag.SucessStatus = false;
            }
            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Member member,string url)
        {
            String result = db.login(member);
            if(result == "UserException")
            {
                ViewBag.ErrorStatus = true;
                ViewBag.ErrorMessage = "Username or Password Invalid";
                ViewBag.SucessStatus = false;
                return View(member);
            }
            else if(result == "AccountException")
            {
                ViewBag.ErrorStatus = true;
                ViewBag.ErrorMessage = "Please First Verify your Account.";
                ViewBag.SucessStatus = false;
                return View(member);
            }
            else if(result == "ProfileSuccess")
            {
                if (!String.IsNullOrEmpty(url))
                {
                    return Redirect(url);
                }
                else
                {
                    return RedirectToAction("Index", "Profile");
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(url))
                {
                    return Redirect(url);
                }
                else
                {
                    return RedirectToAction("Index", "Notes");
                }
            }
        }

        [HttpGet]
        public ActionResult Signup()
        {
            if (TempData["Sucess"] != null)
            {
                ViewBag.ErrorStatus = false;
                ViewBag.SucessStatus = true;
            }
            else
            {
                ViewBag.ErrorStatus = false;
                ViewBag.SucessStatus = false;
            }
            Member member = new Member();
            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(Member loginModel)
        {
            String result = db.Signup(loginModel);
            if(result == "EmailException")
            {
                ViewBag.ErrorStatus = true;
                ViewBag.SucessStatus = false;
                ViewBag.ErrorMessage = "EmailId Already Exits.";
                return View(loginModel);
            }
            else
            {
                ViewBag.ErrorStatus = false;
                ViewBag.SucessStatus = true;
                TempData["Sucess"] = true;
                return RedirectToAction("Signup","Auth");
            }
        }

        [HttpGet]
        public ActionResult Verify(String Email)
        {
            Member mb = db.VerfiyGet(Email);
            if (mb!=null)
            {
                return View(mb);
            }
            else
            {
                TempData["ErrorMessage"] = "Account Not Found";
                return RedirectToAction("Index", "Auth");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Verify(Member mb)
        {
            bool result = db.VerfiyPost(mb);
            if (result)
            {
                TempData["SucessMessage"] = "Your Accout has been Verified";
                return RedirectToAction("Index", "Auth");
            }
            else
            {
                TempData["ErrorMessage"] = "Account Not Found";
                return RedirectToAction("Index", "Auth");
            }
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            Member member = new Member();
            ViewBag.ErrorStatus = false;
            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(Member mb)
        {
            bool result = db.forgotpassword(mb);
            if (result)
            {
                TempData["SucessMessage"] = "The New Password Send to Your EmailId";
                return RedirectToAction("Index", "Auth");
            }
            else
            {
                ViewBag.ErrorStatus = true;
                ViewBag.ErrorMessage = "Email Not Found";
                return View(mb);
            }
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Auth", new { url = path });
            }
            ViewBag.ErrorStatus = false;
            Member m = new Member();
            return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(Member b)
        {
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Auth", new { url = path });
            }

            String result = db.ChangePassword(b, id);
            if(result== "ConfirmException")
            {
                ViewBag.ErrorStatus = true;
                ViewBag.ErrorMessage = "New Password Not Match to Confirm Password";
                return View();
            }
            else if(result== "OldPasswordException")
            {
                ViewBag.ErrorStatus = true;
                ViewBag.ErrorMessage = "Old Password Not Match";
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Auth");
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Auth");
        }
    }
}