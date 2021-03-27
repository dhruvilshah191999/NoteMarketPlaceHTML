using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Notes_MarketPlace.Models;
using System.Net;
using System.Net.Mail;
using PagedList;
using System.Text;
using System.Data;
using System.Security.Cryptography;

namespace Notes_MarketPlace.Controllers
{
    public class NotesController : Controller
    {
        public static string Base64DecodingMethod(string Data)
        {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(Data);
            string returnValue = System.Text.Encoding.UTF32.GetString(encodedDataAsBytes);
            return returnValue;
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
                id = 0;
            }
            return id;
        }

        [HttpGet]
        public ActionResult Index()
        {
            int id = GetId();
            if (id == 0)
            {
                return RedirectToAction("Index", "Auth");
            }
            using(DBEntities db = new DBEntities())
            {
                List<Buyer> b = db.Buyers.ToList();
                ViewBag.Sold = b.Where(e => e.OwnerId == id && e.Status == true).Count();
                ViewBag.Money = b.Where(e => e.OwnerId == id && e.Status == true).Sum(e => e.Price);
                ViewBag.Buyer = b.Where(e => e.OwnerId == id && e.Status == false).Count();
                ViewBag.Download = b.Where(e => e.MemberId == id && e.Status == true).Count();
                ViewBag.Rejected = db.Notes.Where(e => e.OwnerId == id && e.Status == "Rejected").Count();
            }
            return View();
        }

        [HttpGet]
        public ActionResult Progess(string search,int ? i,string sortBy)
        {
            int id = GetId();
            if (id == 0)
            {
                return RedirectToAction("Index", "Auth");
            }
            ViewBag.SortTitle = String.IsNullOrEmpty(sortBy) ? "Title desc" : "";
            ViewBag.SortCategory = String.IsNullOrEmpty(sortBy) ? "Category desc" : "";
            ViewBag.SortStatus= String.IsNullOrEmpty(sortBy) ? "Status desc" : "";

            DBEntities db = new DBEntities();
            List<Note> notes=db.Notes.Where(e=>(e.Status!="Rejected" && e.Status!="Published") && e.OwnerId==id && e.IsActive==true).ToList();
            if (!String.IsNullOrEmpty(search))
            {
                notes = notes.Where(e => e.NoteTitle.ToLower().Contains(search.ToLower()) || e.Category.ToLower().Contains(search.ToLower()) || e.Status.ToLower().Contains(search.ToLower())).ToList();
            }

            switch (sortBy)
            {
                case "Title desc":
                    notes = notes.OrderBy(e => e.NoteTitle).ToList();
                    break;
                case "Category desc":
                    notes = notes.OrderBy(e => e.Category).ToList();
                    break;
                case "Status desc":
                    notes = notes.OrderBy(e => e.Status).ToList();
                    break;
                default:
                    notes = notes.OrderByDescending(e => e.CreatedDate).ToList();
                    break;
            }
            return PartialView(notes.ToList().ToPagedList(i ?? 1, 5));
        }

        [HttpGet]
        public ActionResult Published(string search2, int? i2 ,string sortBy2)
        {
            int id = GetId();
            if (id == 0)
            {
                return RedirectToAction("Index", "Auth");
            }

            ViewBag.SortTitle = String.IsNullOrEmpty(sortBy2) ? "Title desc" : "";
            ViewBag.SortCategory = String.IsNullOrEmpty(sortBy2) ? "Category desc" : "";
            ViewBag.SortType = String.IsNullOrEmpty(sortBy2) ? "Type desc" : "";
            ViewBag.SortPrice = String.IsNullOrEmpty(sortBy2) ? "Price desc" : "";

            DBEntities db = new DBEntities();
            List<Note> notes = db.Notes.Where(e => e.Status == "Published" && e.OwnerId == id && e.IsActive == true).ToList();
            if (!String.IsNullOrEmpty(search2))
            {
                notes = notes.Where(e => e.NoteTitle.ToLower().Contains(search2.ToLower()) || e.Category.ToLower().Contains(search2.ToLower()) || e.SellType.ToLower().Contains(search2.ToLower()) || e.SellPrice.ToString() == search2).ToList();
            }
            switch (sortBy2)
            {
                case "Title desc":
                    notes = notes.OrderBy(e => e.NoteTitle).ToList();
                    break;
                case "Category desc":
                    notes = notes.OrderBy(e => e.Category).ToList();
                    break;
                case "Type desc":
                    notes = notes.OrderBy(e => e.SellType).ToList();
                    break;
                case "Price desc":
                    notes = notes.OrderBy(e => e.SellPrice).ToList();
                    break;
                default:
                    notes = notes.OrderByDescending(e => e.CreatedDate).ToList();
                    break;
            }
            return PartialView(notes.ToList().ToPagedList(i2 ?? 1, 5));
        }

        [HttpGet]
        public ActionResult Add()
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
            Note newnote = new Note();
            ViewData["Type"] = db.Types.ToList<Notes_MarketPlace.Models.Type>();
            ViewData["Country"] = db.Countries.ToList<Country>();
            ViewData["Category"] = db.Categories.ToList<Category>();
            return View(newnote);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Note getnote)
        {
            using (DBEntities db = new DBEntities())
            {
                int id = GetId();
                if (id == 0)
                {
                    return RedirectToAction("Index", "Auth");
                }

                if (getnote.DisplayNote == null || getnote.DisplayPhotoPreview == null)
                {
                    TempData["ErrorMessage"] =  "Notes or Preview PDF Must be Required";
                    return RedirectToAction("Add", "Notes");
                }
                else
                {
                    try
                    {
                        if (!Directory.Exists(Server.MapPath("~/UploadFiles/Notes/")+id))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/UploadFiles/Notes/") + id);
                        }
                        getnote.OwnerId = id;
                        string date2 = DateTime.Now.ToString("yymmssfff");
                        Directory.CreateDirectory(Server.MapPath("~/UploadFiles/Notes/") + id + "/" + date2);
                        Directory.CreateDirectory(Server.MapPath("~/UploadFiles/Notes/") + id + "/" + date2 + ("/Displaypicture"));
                        Directory.CreateDirectory(Server.MapPath("~/UploadFiles/Notes/") + id + "/" + date2 +  ("/UploadedNotes"));
                        Directory.CreateDirectory(Server.MapPath("~/UploadFiles/Notes/") + id + "/" + date2 + ("/NotesPreview"));

                        if (getnote.DisplayPhoto != null)
                        {
                            var supportedTypes = new[] { ".jpg", ".jpeg", ".png" };
                            string extension = Path.GetExtension(getnote.DisplayPhoto.FileName);
                            if (!supportedTypes.Contains(extension) || getnote.DisplayPhoto.ContentLength > 10485760)
                            {
                                TempData["ErrorMessage"] = "Profile Photo must be less than 10MB or Only image are accepted";
                                return RedirectToAction("Add", "Notes");
                            }
                            else
                            {
                                string filename = Path.GetFileNameWithoutExtension(getnote.DisplayPhoto.FileName);
                                filename = filename + "_" + DateTime.Now.ToString("yymmssfff") + extension;
                                getnote.DisplayPicture = date2 + "/Displaypicture/" + filename;
                                filename = Path.Combine(Server.MapPath("~/UploadFiles/Notes/") + id + "/" + date2 + ("/Displaypicture"), filename);
                                getnote.DisplayPhoto.SaveAs(filename);
                            }
                        }

                        if (getnote.DisplayNote != null)
                        {
                            var supportedTypes = new[] { ".pdf", ".zip" };
                            string extension = Path.GetExtension(getnote.DisplayNote.FileName);
                            if (!supportedTypes.Contains(extension) || getnote.DisplayNote.ContentLength > 10485760)
                            {
                                TempData["ErrorMessage"] = "Note must be less than 10MB or Only pdf and zip file are accepted";
                                return RedirectToAction("Add", "Notes");
                            }
                            else
                            {
                                string filename = Path.GetFileNameWithoutExtension(getnote.DisplayNote.FileName);
                                filename = filename + "_" + DateTime.Now.ToString("yymmssfff") + extension;
                                getnote.UploadNotes = date2 + "/UploadedNotes/" + filename;
                                filename = Path.Combine(Server.MapPath("~/UploadFiles/Notes/") + id + "/" + date2 + ("/UploadedNotes"), filename);
                                getnote.DisplayNote.SaveAs(filename);
                            }
                        }

                        if (getnote.DisplayPhotoPreview != null)
                        {
                            var supportedTypes = new[] { ".pdf", ".zip" };
                            string extension = Path.GetExtension(getnote.DisplayPhotoPreview.FileName);
                            if (!supportedTypes.Contains(extension) || getnote.DisplayPhotoPreview.ContentLength > 10485760)
                            {
                                TempData["ErrorMessage"] = "Note must be less than 10MB or Only pdf and zip file are accepted";
                                return RedirectToAction("Add", "Notes");
                            }
                            else
                            {
                                string filename = Path.GetFileNameWithoutExtension(getnote.DisplayPhotoPreview.FileName);
                                filename = filename + "_" + DateTime.Now.ToString("yymmssfff") + extension;
                                getnote.NotePreview = date2 + "/NotesPreview/" + filename;
                                filename = Path.Combine(Server.MapPath("~/UploadFiles/Notes/") + id + "/" + date2 + ("/NotesPreview"), filename);
                                getnote.DisplayPhotoPreview.SaveAs(filename);
                            }
                        }

                        db.Notes.Add(getnote);
                        db.SaveChanges();


                        Member member = db.Members.Where(m => m.MemberId == id).FirstOrDefault();
                        ManageSystem ms = GetManageData.GetData();
                        MailMessage mm = new MailMessage(ms.SupportEmail, ms.EmailAddress);
                        mm.Subject = member.FirstName+" "+ member.LastName + " sent his note for review";
                        mm.Body = "Hello Admins,<br/><br/>We Want to inform you that,"+ member.FirstName + " " + member.LastName + " sent his note<br/><br/>"+getnote.NoteTitle+" for review.Please look at the notes and take required actions.<br/><br/>Regards,<br/>Notes Marketplace"; 
                        mm.IsBodyHtml = true;

                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.UseDefaultCredentials = false;

                        NetworkCredential nc = new NetworkCredential(ms.SupportEmail, ms.EmailPassword);
                        smtp.EnableSsl = true;
                        smtp.Credentials = nc;
                        smtp.Send(mm);

                        TempData["SucessMessage"] = "Notes Send to the admin for review .We will inform you.";
                        return RedirectToAction("Add", "Notes");
                    }
                    catch (Exception e)
                    {
                        TempData["ErrorMessage"] = "Something is wrong. Please try again.";
                        return RedirectToAction("Add", "Notes");
                    }
                }
            }
        }

        [HttpGet]
        public ActionResult Notedetails(string note)
        {
            int id = GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Auth", new { url = path });
            }
            int id2 = Convert.ToInt32(Base64DecodingMethod(note));
            ViewBag.Reviews = true;
            ViewBag.reviewcount = 0;
            ViewBag.reviews = 0;
            ViewBag.CheckOwner = false;
            ViewBag.CheckApproved = false;

            using (DBEntities db=new DBEntities())
            {
                ViewBag.FirstName = db.Members.Where(m => m.MemberId == id && m.IsActive == true).Select(m => m.FirstName).FirstOrDefault();
                ManageSystem ms = GetManageData.GetData();
                Buyer b = db.Buyers.Where(m => m.NoteId == id2 && m.MemberId == id && m.IsActive == true).FirstOrDefault();
                if (b != null)
                {
                    ViewBag.CheckOwner = true;
                    if (!b.Status)
                    {
                        ViewBag.CheckApproved = true;
                    }
                }
                Note notedetails = db.Notes.Where(e => e.NoteId == id2 && e.IsActive == true).FirstOrDefault();
                if (notedetails.OwnerId == id)
                {
                    ViewBag.CheckOwner = true;
                }
                List<Review> reviews= db.Reviews.Where(e => e.NoteId == id2 && e.IsActive == true).OrderByDescending(e => e.CreatedDate).ToList<Review>();
                if (reviews.Count == 0)
                {
                    ViewBag.Reviews = false;
                }
                else
                {
                    foreach(Review re in reviews)
                    {
                        MemberProfile mp=db.MemberProfiles.Where(e=>e.MemberId==re.MemberId).FirstOrDefault();
                        re.Member1 = mp.Member;
                        re.ProfilePhoto = mp.ProfilePicture;
                        if (re.ProfilePhoto == null)
                        {
                            re.ProfilePhoto = ms.MemberDisplayPicture;
                        }
                    }
                    ViewBag.reviewcount = Convert.ToInt32(reviews.Average(e => e.Rate));
                    ViewBag.reviews = Convert.ToInt32(reviews.Count());
                }
                List<Inappropriate> reports = db.Inappropriates.Where(e => e.NoteId == notedetails.NoteId && e.IsActive == true).ToList<Inappropriate>();
                notedetails.report = reports.Count;
                ViewBag.Photo = ms.NoteDisplayPicture;
                ViewData["Review"] = reviews;
                return View(notedetails);
            }
        }

        public ActionResult AddDetails(int noteid)
        {
            int id = GetId();
            if (id == 0)
            {
                return RedirectToAction("Index", "Auth");
            }
            using(DBEntities db=new DBEntities())
            {
                Buyer b = new Buyer();
                Note note = db.Notes.Where(e => e.NoteId == noteid).FirstOrDefault();
                b.NoteId = noteid;
                b.OwnerId = note.OwnerId;
                b.MemberId = id;
                Member m = db.Members.Where(e => e.MemberId == id).FirstOrDefault();
                if (note.SellType == "Free")
                {
                    b.Status = true;
                    b.IsPaid = true;
                }
                else
                {
                    b.Status = false;
                    b.IsPaid = false;
                }
                b.AttachmentPath = note.UploadNotes;
                b.Price = note.SellPrice;
                b.NoteTitle = note.NoteTitle;
                b.NoteCategory = note.Category;
                
                try
                {
                    db.Buyers.Add(b);
                    db.SaveChanges();
                    if (note.SellType == "Free")
                    {
                        ManageSystem ms = GetManageData.GetData();
                        MailMessage mm = new MailMessage(ms.SupportEmail, m.Email);
                        mm.Subject = note.Member.FirstName + " " + note.Member.LastName + "  Allows you to download a note";
                        mm.Body = "Hello " + m.FirstName + " " + m.LastName + "<br/></br/>We would like to inform you that, " + note.Member.FirstName + " " + note.Member.LastName + " Allows you to download a note.Please login and see My Download tabs to download particular note.<br/><br/>Regards,<br/>Notes Marketplace";
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
                    else
                    {
                        ManageSystem ms = GetManageData.GetData();
                        MailMessage mm = new MailMessage(ms.SupportEmail, note.Member.Email);
                        mm.Subject = m.FirstName + " " + m.LastName + " wants to purchase your notes";
                        mm.Body = "Hello " + note.Member.FirstName + " " + note.Member.LastName + "<br/></br/>We would like to inform you that, " + m.FirstName + " " + m.LastName + " wants to purchase your notes. Please see Buyer Requests tab and allow download access to Buyer if you have received the payment from him.<br/><br/>Regards,<br/>Notes Marketplace";
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
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return RedirectToAction("Search","Home");
        }

        public ActionResult BuyerRequest(string search, int? i,string sortBy)
        {
            int id = GetId();
            if (id == 0)
            {
                return RedirectToAction("Index", "Auth");
            }
            ViewBag.SortTitle = String.IsNullOrEmpty(sortBy) ? "Title desc" : "";
            ViewBag.SortCategory = String.IsNullOrEmpty(sortBy) ? "Category desc" : "";
            ViewBag.SortEmail = String.IsNullOrEmpty(sortBy) ? "Email desc" : "";
            DBEntities db = new DBEntities();
            List<Buyer> buyer= db.Buyers.Where(e => e.OwnerId == id && e.Status == false).ToList();

            foreach (Buyer b in buyer)
            {
                MemberProfile mp = db.MemberProfiles.Where(e => e.MemberId == b.MemberId).FirstOrDefault();
                b.CountryCode = mp.CountryCode;
                b.PhoneNumber = mp.PhoneNumber;
            }

            if (!String.IsNullOrEmpty(search))
            {
                buyer = buyer.Where(e => e.NoteTitle.ToLower().Contains(search.ToLower()) || e.NoteCategory.ToLower().Contains(search.ToLower()) || e.Price.ToString()==search || e.PhoneNumber.Contains(search) || e.Member1.FirstName.ToLower().Contains(search.ToLower()) || e.Member1.LastName.ToLower().Contains(search.ToLower()) || e.Member1.Email.ToLower().Contains(search.ToLower())).ToList();
            }

            switch (sortBy)
            {
                case "Title desc":
                    buyer = buyer.OrderBy(e => e.NoteTitle).ToList();
                    break;
                case "Category desc":
                    buyer = buyer.OrderBy(e => e.NoteCategory).ToList();
                    break;
                case "Email desc":
                    buyer = buyer.OrderBy(e => e.Member1.Email).ToList();
                    break;
                default:
                    buyer = buyer.OrderByDescending(e => e.RequestDate).ToList();
                    break;
            }

            ViewBag.Viewpage = 5;
            return View(buyer.ToList().ToPagedList(i ?? 1, 5));
        }

        public ActionResult Accept(int id)
        {
            DBEntities db = new DBEntities();
            Buyer buyer = db.Buyers.Where(e => e.Id == id).FirstOrDefault();
            buyer.ApprovedDate = DateTime.Now;
            buyer.Status = true;
            buyer.IsPaid = true;
            try
            {
                db.SaveChanges();
                ManageSystem ms = GetManageData.GetData();
                MailMessage mm = new MailMessage(ms.SupportEmail, buyer.Member1.Email);
                mm.Subject = buyer.Member.FirstName + " " + buyer.Member.LastName + "  Allows you to download a note";
                mm.Body = "Hello " + buyer.Member1.FirstName + " " + buyer.Member1.LastName + "<br/></br/>We would like to inform you that, " + buyer.Member.FirstName + " " + buyer.Member.LastName + " Allows you to download a note.Please login and see My Download tabs to download particular note.<br/><br/>Regards,<br/>Notes Marketplace";
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
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return RedirectToAction("BuyerRequest","Notes");
        }

        public ActionResult Deletenote(int noteid)
        {
            DBEntities db = new DBEntities();
            Note note = db.Notes.Where(e => e.NoteId == noteid).FirstOrDefault();
            note.IsActive = false;
            note.ModifiedDate = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Index","Notes");
        }

        [HttpGet]
        public ActionResult EditNote(int noteid)
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
            Note newnote = db.Notes.Where(e => e.NoteId == noteid && e.IsActive == true).FirstOrDefault();
            ViewData["Type"] = db.Types.ToList<Notes_MarketPlace.Models.Type>();
            ViewData["Country"] = db.Countries.ToList<Country>();
            ViewData["Category"] = db.Categories.ToList<Category>();
            return View(newnote);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNote(Note note)
        {
            using(DBEntities db = new DBEntities())
            {
                Note newnote = db.Notes.Where(e => e.NoteId == note.NoteId && e.IsActive == true).FirstOrDefault();
                newnote.NoteTitle = note.NoteTitle;
                newnote.Category = note.Category;
                newnote.Type = note.Type;
                newnote.TotalPages = note.TotalPages;
                newnote.Description = note.Description;
                newnote.Country = note.Country;
                newnote.InstitutionName = note.InstitutionName;
                newnote.CourseName = note.CourseName;
                newnote.CourseCode = note.CourseCode;
                newnote.Professor = note.Professor;
                newnote.SellType = note.SellType;
                newnote.SellPrice = note.SellPrice;
                string path = newnote.UploadNotes.Split('/')[0].ToString();

                if (note.DisplayPhoto != null)
                {
                    var supportedTypes = new[] { ".jpg", ".jpeg", ".png" };
                    string extension = Path.GetExtension(note.DisplayPhoto.FileName);
                    if (!supportedTypes.Contains(extension) || note.DisplayPhoto.ContentLength > 10485760)
                    {
                        TempData["ErrorMessage"] = "Profile Photo must be less than 10MB or Only image are accepted";
                        return RedirectToAction("EditNote", "Notes");
                    }
                    else
                    {
                        string filename = Path.GetFileNameWithoutExtension(note.DisplayPhoto.FileName);
                        filename = filename + "_" + DateTime.Now.ToString("yymmssfff") + extension;
                        newnote.DisplayPicture = path + "/Displaypicture/" + filename;
                        filename = Path.Combine(Server.MapPath("~/UploadFiles/Notes/") + newnote.OwnerId + "/" + path + ("/Displaypicture"), filename);
                        note.DisplayPhoto.SaveAs(filename);
                    }
                }

                if (note.DisplayNote != null)
                {
                    var supportedTypes = new[] { ".pdf", ".zip" };
                    string extension = Path.GetExtension(note.DisplayNote.FileName);
                    if (!supportedTypes.Contains(extension) || note.DisplayNote.ContentLength > 10485760)
                    {
                        TempData["ErrorMessage"] = "Note must be less than 10MB or Only pdf and zip file are accepted";
                        return RedirectToAction("EditNote", "Notes");
                    }
                    else
                    {
                        string filename = Path.GetFileNameWithoutExtension(note.DisplayNote.FileName);
                        filename = filename + "_" + DateTime.Now.ToString("yymmssfff") + extension;
                        newnote.UploadNotes = path + "/UploadedNotes/" + filename;
                        filename = Path.Combine(Server.MapPath("~/UploadFiles/Notes/") + newnote.OwnerId + "/" + path + ("/UploadedNotes"), filename);
                        note.DisplayNote.SaveAs(filename);
                    }
                }

                if (note.DisplayPhotoPreview != null)
                {
                    var supportedTypes = new[] { ".pdf", ".zip" };
                    string extension = Path.GetExtension(note.DisplayPhotoPreview.FileName);
                    if (!supportedTypes.Contains(extension) || note.DisplayPhotoPreview.ContentLength > 10485760)
                    {
                        TempData["ErrorMessage"] = "Note must be less than 10MB or Only pdf and zip file are accepted";
                        return RedirectToAction("EditNote", "Notes");
                    }
                    else
                    {
                        string filename = Path.GetFileNameWithoutExtension(note.DisplayPhotoPreview.FileName);
                        filename = filename + "_" + DateTime.Now.ToString("yymmssfff") + extension;
                        newnote.NotePreview = path + "/NotesPreview/" + filename;
                        filename = Path.Combine(Server.MapPath("~/UploadFiles/Notes/") + newnote.OwnerId + "/" + path + ("/NotesPreview"), filename);
                        note.DisplayPhotoPreview.SaveAs(filename);
                    }
                }

                newnote.ModifiedDate = DateTime.Now;
                db.SaveChanges();
            }
            return RedirectToAction("Index","Notes");
        }
    }
}