using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Notes_MarketPlace.Models;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Text;
using System.Data;
using System.Security.Cryptography;

namespace Notes_MarketPlace.Controllers
{
    public class AuthController : Controller
    {
        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212DhruvilShah";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

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
        public ActionResult Index(Member member,string url)
        {
            using (DBEntities db = new DBEntities())
            {
                Member mb = db.Members.Where(x => x.Email == member.Email && x.Password == member.Password && x.IsActive == true).FirstOrDefault();
                if (mb == null)
                {
                    ViewBag.ErrorStatus = true;
                    ViewBag.ErrorMessage = "Username or Password Invalid";
                    ViewBag.SucessStatus = false;
                    return View(member);
                }
                else if (mb.EmailVerified == false)
                {
                    ViewBag.ErrorStatus = true;
                    ViewBag.ErrorMessage = "Please First Verify your Account.";
                    ViewBag.SucessStatus = false;
                    return View(member);
                }
                else
                {
                    MemberProfile mp = db.MemberProfiles.Where(x => x.MemberId == mb.MemberId).FirstOrDefault();
                    if (mp == null)
                    {
                        ManageSystem ms = GetManageData.GetData();
                        if (member.Remember == true)
                        {
                            if(Request.Cookies["Username"] == null && Request.Cookies["Password"] == null)
                            {
                                HttpCookie ck = new HttpCookie("Username", member.Email);
                                ck.Expires = DateTime.Now.AddYears(1);
                                Response.Cookies.Add(ck);

                                HttpCookie ck2 = new HttpCookie("Password", member.Password);
                                ck2.Expires = DateTime.Now.AddYears(1);
                                Response.Cookies.Add(ck2);
                            } 
                            
                            if (Request.Cookies["Username"].Value != member.Email)
                            {
                                Request.Cookies["Username"].Value = member.Email;
                            }

                            if (Request.Cookies["Password"].Value != member.Password)
                            {
                                Request.Cookies["Password"].Value = member.Password;
                            }
                            Session["MemberId"] = Encrypt(mb.MemberId.ToString());
                            Session["Profile"] = ms.MemberDisplayPicture;
                            Session.Timeout = 40;
                        }
                        else
                        {
                            if (Request.Cookies["Username"] != null)
                            {
                                HttpCookie myCookie = new HttpCookie("Username");
                                myCookie.Expires = DateTime.Now.AddDays(-1d);
                                Response.Cookies.Add(myCookie);
                            }

                            if (Request.Cookies["Password"] != null)
                            {
                                HttpCookie myCookie = new HttpCookie("Password");
                                myCookie.Expires = DateTime.Now.AddDays(-1d);
                                Response.Cookies.Add(myCookie);
                            }

                            Session["MemberId"] = Encrypt(mb.MemberId.ToString());
                            Session["Profile"] = ms.MemberDisplayPicture;
                            Session.Timeout = 40;
                        }
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
                        ManageSystem ms = GetManageData.GetData();
                        if (member.Remember == true)
                        {
                            if (Request.Cookies["Username"] == null && Request.Cookies["Password"] == null)
                            {
                                HttpCookie ck = new HttpCookie("Username", member.Email);
                                ck.Expires = DateTime.Now.AddYears(1);
                                Response.Cookies.Add(ck);

                                HttpCookie ck2 = new HttpCookie("Password", member.Password);
                                ck2.Expires = DateTime.Now.AddYears(1);
                                Response.Cookies.Add(ck2);
                            }

                            if (Request.Cookies["Username"].Value != member.Email)
                            {
                                Response.Cookies["Username"].Value = member.Email;
                            }

                            if (Request.Cookies["Password"].Value != member.Password)
                            {
                                Response.Cookies["Password"].Value = member.Password;
                            }

                            Session["MemberId"] = Encrypt(mb.MemberId.ToString());
                            if (mp.ProfilePicture == null)
                            {
                                Session["Profile"] = ms.MemberDisplayPicture;
                            }
                            else
                            {
                                Session["Profile"] = mp.ProfilePicture;
                            }
                            Session.Timeout = 40;
                        }
                        else
                        {
                            if (Request.Cookies["Username"] != null)
                            {
                                HttpCookie myCookie = new HttpCookie("Username");
                                myCookie.Expires = DateTime.Now.AddDays(-1d);
                                Response.Cookies.Add(myCookie);
                            }

                            if (Request.Cookies["Password"] != null)
                            {
                                HttpCookie myCookie = new HttpCookie("Password");
                                myCookie.Expires = DateTime.Now.AddDays(-1d);
                                Response.Cookies.Add(myCookie);
                            }

                            Session["MemberId"] = Encrypt(mb.MemberId.ToString());
                            if (mp.ProfilePicture == null)
                            {
                                Session["Profile"] = ms.MemberDisplayPicture;
                            }
                            else
                            {
                                Session["Profile"] = mp.ProfilePicture;
                            }
                            Session.Timeout = 40;

                        }
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
            }
        }


        [HttpGet]
        public ActionResult Signup()
        {
            Member member = new Member();
            ViewBag.ErrorStatus = false;
            ViewBag.SucessStatus = false;
            return View(member);
        }

        [HttpPost]
        public ActionResult Signup(Member loginModel)
        {
            using (DBEntities db = new DBEntities())
            {
                if (loginModel.CPassword.Equals(loginModel.Password))
                {
                    if (db.Members.Any(x => x.Email == loginModel.Email))
                    {
                        ViewBag.ErrorStatus = true;
                        ViewBag.SucessStatus = false;
                        ViewBag.ErrorMessage = "EmailId Already Exits.";
                        return View(loginModel);
                    }
                    else
                    {
                        try
                        {
                            db.Members.Add(loginModel);
                            db.SaveChanges();
                            ManageSystem ms = GetManageData.GetData();
                            MailMessage mm = new MailMessage(ms.SupportEmail, loginModel.Email);
                            mm.Subject = "Note Marketplace -Email Verification";
                            mm.Body = "Hello " + loginModel.FirstName + " " + loginModel.LastName + "<br/><br/>Thank you for signing up with us. Please click on below link to verify your email address and to do login.<br/><br/>Link:<a href='https://localhost:44354/Auth/Verify?Email=" + loginModel.Email + "'>https://localhost:44354/Auth/Verify?Email=" + loginModel.Email + "</a><br/><br/>Regards,<br>Notes Marketplace";
                            mm.IsBodyHtml = true;

                            SmtpClient smtp = new SmtpClient();
                            smtp.Host = "smtp.gmail.com";
                            smtp.Port = 587;
                            smtp.UseDefaultCredentials = false;

                            NetworkCredential nc = new NetworkCredential(ms.SupportEmail, ms.EmailPassword);
                            smtp.EnableSsl = true;
                            smtp.Credentials = nc;
                            smtp.Send(mm);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                else
                {
                    ViewBag.ErrorStatus = true;
                    ViewBag.SucessStatus = false;
                    ViewBag.ErrorMessage = "Password Not Match";
                    return View(loginModel);
                }

                ViewBag.ErrorStatus = false;
                ViewBag.SucessStatus = true;
                ModelState.Clear();
                return View(new Member());
            }
        }

        [HttpGet]
        public ActionResult Verify(String Email)
        {
            using (DBEntities db= new DBEntities())
            {
                Member mb = db.Members.Where(x => x.Email == Email).FirstOrDefault();
                if (mb == null)
                {
                    TempData["ErrorMessage"] = "Account Not Found";
                    return RedirectToAction("Index", "Auth");
                }
                else
                {
                    return View(mb);
                }
            }
        }

        [HttpPost]
        public ActionResult Verify(Member mb)
        {
            using (DBEntities db = new DBEntities())
            {
                Member mb2 = db.Members.Where(x => x.MemberId == mb.MemberId).FirstOrDefault();
                if (mb == null)
                {
                    TempData["ErrorMessage"] = "Account Not Found";
                    return RedirectToAction("Index", "Auth");
                }
                else
                {
                    mb2.EmailVerified = true;
                    db.SaveChanges();
                    TempData["SucessMessage"] = "Your Accout has been Verified";
                    return RedirectToAction("Index", "Auth");
                }
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
        public ActionResult ForgotPassword(Member mb)
        {
            using (DBEntities db = new DBEntities())
            {
                Member mb2 = db.Members.Where(x => x.Email == mb.Email).FirstOrDefault();
                if (mb2 == null)
                {
                    ViewBag.ErrorStatus = true;
                    ViewBag.ErrorMessage = "Email Not Found";
                    return View(mb);
                }
                else
                {
                    try
                    {
                        string allowedChars = "";
                        allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
                        allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
                        allowedChars += "1,2,3,4,5,6,7,8,9,0,@";
                        char[] sep = { ',' };
                        string[] arr = allowedChars.Split(sep);
                        string passwordString = "";
                        string temp = "";
                        Random rand = new Random();
                        for (int i = 0; i < 8; i++)
                        {
                            temp = arr[rand.Next(0, arr.Length)];
                            passwordString += temp;
                        }

                        ManageSystem ms = GetManageData.GetData();
                        MailMessage mm = new MailMessage(ms.SupportEmail, mb.Email);
                        mm.Subject = "New Temporary Password has been created for you";
                        mm.Body = "Hello,<br/><br/>We have generated a new password for you<br/>Password: " + passwordString + "<br/><br/>Regards,<br/>Notes Marketplace";
                        mm.IsBodyHtml = true;

                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.UseDefaultCredentials = false;

                        NetworkCredential nc = new NetworkCredential(ms.SupportEmail, ms.EmailPassword);
                        smtp.EnableSsl = true;
                        smtp.Credentials = nc;
                        smtp.Send(mm);
                        mb2.Password = passwordString;
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            TempData["SucessMessage"] = "The New Password Send to Your EmailId";
            return RedirectToAction("Index", "Auth");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Auth");
        }
    }
}