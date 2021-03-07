using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Notes_MarketPlace.Models;
using PagedList;
using System.IO;
using System.Text;
using System.Data;
using System.Configuration;
using System.Security.Cryptography;

namespace Notes_MarketPlace.Controllers
{
    public class ProfileController : Controller
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

        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212DhruvilShah";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        private int GetId()
        {
            int id;
            if (Session["MemberId"] != null)
            {
                id = Convert.ToInt32(Decrypt(Session["MemberId"].ToString()));
            }
            else
            {
                id=0;
            }
            return id;
        }

        [HttpGet]
        public ActionResult Index()
        {
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
                ViewBag.SucessMessage = TempData["SucessMessage"];
            }
            else
            {
                ViewBag.ErrorStatus = false;
                ViewBag.SucessStatus = false;
            }
            DBEntities db = new DBEntities();

            int id = GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Auth", new { url = path });
            }

            MemberProfile mp = db.MemberProfiles.Where(e => e.MemberId == id).FirstOrDefault();
            if (mp == null)
            {
                Member member = db.Members.Where(e => e.MemberId == id).FirstOrDefault();
                MemberProfile mp1 = new MemberProfile();
                mp1.Member = member;
                ViewData["Country"] = db.Countries.ToList<Country>().OrderBy(e=>e.CountryCode);
                return View(mp1);
            }
            else
            {
                ViewData["Country"] = db.Countries.ToList<Country>().OrderBy(e => e.CountryCode);
                return View(mp);
            }
        }

        [HttpPost]
        public ActionResult Index(MemberProfile mb)
        {
            using (DBEntities db = new DBEntities())
            {
                int id = GetId();
                if (id == 0)
                {
                    string path = Request.Url.PathAndQuery;
                    return RedirectToAction("Index", "Auth", new { url = path });
                }

                MemberProfile mp = db.MemberProfiles.Where(e => e.MemberId == id).FirstOrDefault();
                if (mp == null)
                {
                    Member member = db.Members.Where(e => e.MemberId == id).FirstOrDefault();
                    if (mb.Member.FirstName != member.FirstName)
                    {
                        if (mb.Member.LastName != member.LastName)
                        {
                            member.FirstName = mb.Member.FirstName;
                            member.LastName = mb.Member.LastName;
                            member.ModifiedDate = DateTime.Now;
                        }
                        else
                        {
                            member.FirstName = mb.Member.FirstName;
                            member.ModifiedDate = DateTime.Now;
                        }
                    }
                    else if (mb.Member.LastName != member.LastName)
                    {
                        member.LastName = mb.Member.LastName;
                        member.ModifiedDate = DateTime.Now;
                    }

                    if (mb.ImageUrl != null)
                    {
                        var supportedTypes = new[] { ".jpg", ".jpeg", ".png" };
                        string extension = System.IO.Path.GetExtension(mb.ImageUrl.FileName);
                        if (!supportedTypes.Contains(extension) || mb.ImageUrl.ContentLength > 10485760)
                        {
                            TempData["ErrorMessage"] = "Profile Photo less than 10MB or Only image are accepted";
                            return RedirectToAction("Index", "Profile");
                        }
                        else
                        {
                            string filename = System.IO.Path.GetFileNameWithoutExtension(mb.ImageUrl.FileName);
                            filename = mb.Member.FirstName + "_" + DateTime.Now.ToString("yymmssfff") + extension;
                            mb.ProfilePicture = filename;
                            filename = System.IO.Path.Combine(Server.MapPath("~/UploadFiles/MemberProfile/"), filename);
                            mb.ImageUrl.SaveAs(filename);
                        }
                    }

                    mb.CreatedDate = DateTime.Now;
                    mb.MemberId = id;
                    mb.Member = member;
                    db.MemberProfiles.Add(mb);
                    db.SaveChanges();
                    TempData["SucessMessage"] = "Your Profile has been updated";
                    return RedirectToAction("Index", "Profile");
                }
                else
                {
                    mp.Gender = mb.Gender;
                    mp.CountryCode = mb.CountryCode;
                    mp.PhoneNumber = mb.PhoneNumber;
                    mp.AddressLine1 = mb.AddressLine1;
                    mp.AddressLine2 = mb.AddressLine2;
                    mp.City = mb.City;
                    mp.State = mb.State;
                    mp.Zipcode = mb.Zipcode;
                    mp.Country = mb.Country;
                    mp.University = mb.University;
                    mp.College = mb.College;
                    mp.ModifiedDate = DateTime.Now;

                    if (mb.BirthDate != null)
                    {
                        mp.BirthDate = mb.BirthDate;
                    }

                    if(mb.ImageUrl != null)
                    {
                        if (mp.ProfilePicture != null)
                        {
                            string fullPath = Request.MapPath("~/UploadFiles/MemberProfile/" + mp.ProfilePicture);
                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }
                        }

                        var supportedTypes = new[] { ".jpg", ".jpeg", ".png" };
                        string extension = System.IO.Path.GetExtension(mb.ImageUrl.FileName);
                        if (!supportedTypes.Contains(extension) || mb.ImageUrl.ContentLength > 10485760)
                        {
                            TempData["ErrorMessage"] = "Profile Photo less than 10MB or Only image are accepted";
                            return RedirectToAction("Index", "Profile");
                        }
                        else
                        {
                            string filename = System.IO.Path.GetFileNameWithoutExtension(mb.ImageUrl.FileName);
                            filename = mb.Member.FirstName + "_" + DateTime.Now.ToString("yymmssfff") + extension;
                            mp.ProfilePicture = filename;
                            filename = System.IO.Path.Combine(Server.MapPath("~/UploadFiles/MemberProfile/"), filename);
                            mb.ImageUrl.SaveAs(filename);
                        }
                    }

                    if (mb.Member.FirstName != mp.Member.FirstName)
                    {
                        if (mb.Member.LastName != mp.Member.LastName)
                        {
                            mp.Member.FirstName = mb.Member.FirstName;
                            mp.Member.LastName = mb.Member.LastName;
                            mp.Member.ModifiedDate = DateTime.Now;
                        }
                        else
                        {
                            mp.Member.FirstName = mb.Member.FirstName;
                            mp.Member.ModifiedDate = DateTime.Now;
                        }
                    }
                    else if(mb.Member.LastName != mp.Member.LastName)
                    {
                        mp.Member.LastName = mb.Member.LastName;
                        mp.Member.ModifiedDate = DateTime.Now;
                    }

                    db.SaveChanges();
                    TempData["SucessMessage"] = "Your Profile has been updated";
                    return RedirectToAction("Index", "Profile");
                }
            }
        }

        public ActionResult Rejected(string search,int ? i,string sortBy)
        {
            ViewBag.SortTitle = String.IsNullOrEmpty(sortBy) ? "Title desc" : "";
            ViewBag.SortCategory = String.IsNullOrEmpty(sortBy) ? "Category desc" : "";
            int id=GetId();
            if (id == 0) 
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Auth",new { url=path });
            }
            DBEntities db = new DBEntities();
            List<Note> notes;
            if (search != null && search!="")
            {
                notes = db.Notes.Where(e=>(e.NoteTitle.ToLower().Contains(search.ToLower()) || e.Category.ToLower().Contains(search.ToLower()) || e.AdminRemarks.ToLower().Contains(search.ToLower())) && e.OwnerId==id && e.Status == "Rejected" && e.IsActive == true).ToList();
            }
            else
            {
                notes = db.Notes.Where(e=>e.OwnerId==id && e.Status=="Rejected" && e.IsActive==true).ToList();
            }
            ViewBag.Viewpage = 5;
            switch (sortBy)
            {
                case "Title desc":
                    notes = notes.OrderBy(e => e.NoteTitle).ToList();
                    break;
                case "Category desc":
                    notes = notes.OrderBy(e => e.Category).ToList();
                    break;
                default:
                    notes = notes.OrderByDescending(e => e.CreatedDate).ToList();
                    break;
            }
            return View(notes.ToList().ToPagedList(i??1,5));
        }
    }
}