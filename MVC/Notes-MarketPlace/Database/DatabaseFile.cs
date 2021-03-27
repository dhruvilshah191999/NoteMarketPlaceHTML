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

namespace Notes_MarketPlace.Database
{
    public class DatabaseFile
    {
        public string Encrypt(string clearText)
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

        public string Decrypt(string cipherText)
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

        public int GetId()
        {
            int id;
            if (HttpContext.Current.Session["MemberId"] != null)
            {
                id = Convert.ToInt32(Decrypt(HttpContext.Current.Session["MemberId"].ToString()));
            }
            else
            {
                id = 0;
            }
            return id;
        }

        DBEntities db = new DBEntities();
        ManageSystem ms = GetManageData.GetData();
        public bool contactus(Contactu contact)
        {
            try
            {
                db.Contactus.Add(contact);
                db.SaveChanges();
                MailMessage mm = new MailMessage(ms.SupportEmail, ms.EmailAddress);
                mm.Subject = contact.Name + " - Query";
                mm.Body = "Hello,<br/><br/>" + contact.Comment + "<br/><br/>Regards,<br/>" + contact.Name;
                mm.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;

                NetworkCredential nc = new NetworkCredential(ms.SupportEmail, ms.EmailPassword);
                smtp.EnableSsl = true;
                smtp.Credentials = nc;
                smtp.Send(mm);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        } 

        public List<Note> search(string search, string rating, string country, string category, string type, string course, string university)
        {
            List<Note> notes = db.Notes.Where(e => e.IsActive == true).ToList();
            foreach (Note n in notes)
            {
                if (n.DisplayPicture == null)
                {
                    List<Review> reviews = db.Reviews.Where(e => e.IsActive == true && e.NoteId == n.NoteId).ToList<Review>();
                    List<Inappropriate> reports = db.Inappropriates.Where(e => e.NoteId == n.NoteId && e.IsActive == true).ToList<Inappropriate>();
                    n.report = reports.Count;
                    if (reviews.Count != 0)
                    {
                        n.Avgrate = Convert.ToInt32(reviews.Average(e => e.Rate));
                    }
                    else
                    {
                        n.Avgrate = 0;
                    }
                }
            }
            if (search != null)
            {
                notes = notes.Where(e => e.NoteTitle.ToLower().Contains(search.ToLower())).ToList();
            }
            if (rating != "Select rating" && rating != null)
            {
                notes = notes.Where(e => e.Avgrate == Convert.ToInt32(rating)).ToList();
            }
            if (country != "Select country" && !String.IsNullOrEmpty(country))
            {
                notes = notes.Where(e => e.Country != null && e.Country.ToLower().Contains(country.ToLower())).ToList();
            }
            if (category != "Select category" && !String.IsNullOrEmpty(category))
            {
                notes = notes.Where(e => e.Category != null && e.Category.ToLower().Contains(category.ToLower())).ToList();
            }
            if (type != "Select type" && !String.IsNullOrEmpty(type))
            {
                notes = notes.Where(e => e.Type.ToLower().Contains(type.ToLower())).ToList();
            }
            if (university != "Select university" && !String.IsNullOrEmpty(university))
            {
                notes = notes.Where(e => e.InstitutionName != null && e.InstitutionName.ToLower().Contains(university.ToLower())).ToList();
            }
            if (course != "Select course" && !String.IsNullOrEmpty(course))
            {
                notes = notes.Where(e => e.CourseName != null && e.CourseName.Trim().ToLower().Contains(course.ToLower())).ToList();
            }
            return notes;
        }

        public List<Country> CountryData()
        {
            return db.Countries.Where(e=>e.IsActive==true).ToList<Country>();
        }

        public List<Category> CategoryData()
        {
            return db.Categories.Where(e => e.IsActive == true).ToList<Category>();
        }

        public List<Notes_MarketPlace.Models.Type> TypeData()
        {
            return db.Types.Where(e => e.IsActive == true).ToList<Notes_MarketPlace.Models.Type>();
        }

        public List<String> UniversityData()
        {
            return db.Notes.Where(e => e.InstitutionName != null).Select(e => e.InstitutionName).Distinct().ToList();
        }

        public List<String> CourseData()
        {
            return db.Notes.Where(e => e.CourseName != null).Select(e => e.CourseName).Distinct().ToList();
        }

        public List<Note> Rejected(string search,string sortBy,int id)
        {
            List<Note> notes= db.Notes.Where(e => e.OwnerId == id && e.Status == "Rejected" && e.IsActive == true).ToList();
            if (!String.IsNullOrEmpty(search))
            {
                notes = db.Notes.Where(e => (e.NoteTitle.ToLower().Contains(search.ToLower()) || e.Category.ToLower().Contains(search.ToLower()) || e.AdminRemarks.ToLower().Contains(search.ToLower())) && e.OwnerId == id && e.Status == "Rejected" && e.IsActive == true).ToList();
            }
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
            return notes;
        }

        public MemberProfile ProfileGet(int id)
        {
            MemberProfile mp = db.MemberProfiles.Where(e => e.MemberId == id).FirstOrDefault();
            if (mp == null)
            {
                Member member = db.Members.Where(e => e.MemberId == id).FirstOrDefault();
                MemberProfile mp1 = new MemberProfile();
                mp1.Member = member;
                return mp1;
            }
            else
            {
                return mp;
            }
        }

        public bool ProfilePost(int id, MemberProfile mb)
        {
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
                        return false;
                    }
                    else
                    {
                        string filename = System.IO.Path.GetFileNameWithoutExtension(mb.ImageUrl.FileName);
                        filename = mb.Member.FirstName + "_" + DateTime.Now.ToString("yymmssfff") + extension;
                        mb.ProfilePicture = filename;
                        filename = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/UploadFiles/MemberProfile/"), filename);
                        mb.ImageUrl.SaveAs(filename);
                        HttpContext.Current.Session["Profile"] = mb.ProfilePicture;
                    }
                }

                mb.CreatedDate = DateTime.Now;
                mb.MemberId = id;
                mb.Member = member;
                db.MemberProfiles.Add(mb);
                db.SaveChanges();
                return true;
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

                if (mb.ImageUrl != null)
                {
                    var supportedTypes = new[] { ".jpg", ".jpeg", ".png" };
                    string extension = System.IO.Path.GetExtension(mb.ImageUrl.FileName);
                    if (!supportedTypes.Contains(extension) || mb.ImageUrl.ContentLength > 10485760)
                    {
                        return false;
                    }
                    else
                    {
                        if (mp.ProfilePicture != null)
                        {
                            string fullPath = HttpContext.Current.Request.MapPath("~/UploadFiles/MemberProfile/" + mp.ProfilePicture);
                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }
                        }
                        
                        string filename = System.IO.Path.GetFileNameWithoutExtension(mb.ImageUrl.FileName);
                        filename = mb.Member.FirstName + "_" + DateTime.Now.ToString("yymmssfff") + extension;
                        mp.ProfilePicture = filename;
                        filename = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/UploadFiles/MemberProfile/"), filename);
                        mb.ImageUrl.SaveAs(filename);
                        HttpContext.Current.Session["Profile"] = mp.ProfilePicture;
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
                else if (mb.Member.LastName != mp.Member.LastName)
                {
                    mp.Member.LastName = mb.Member.LastName;
                    mp.Member.ModifiedDate = DateTime.Now;
                }

                db.SaveChanges();
                return true;
            }
        }

        public bool forgotpassword(Member mb)
        {
            Member mb2 = db.Members.Where(x => x.Email == mb.Email && x.IsActive==true).FirstOrDefault();
            if (mb2 == null)
            {
                return false;
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
                    mb2.ModifiedDate = DateTime.Now;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public bool VerfiyPost(Member mb)
        {
            Member mb2 = db.Members.Where(x => x.MemberId == mb.MemberId).FirstOrDefault();
            if (mb == null)
            {
                return false;
            }
            else
            {
                mb2.EmailVerified = true;
                mb2.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
        }

        public Member VerfiyGet(String Email)
        {
            Member mb = db.Members.Where(x => x.Email == Email && x.IsActive==true).FirstOrDefault();
            if (mb == null)
            {
                return null;
            }
            else
            {
                return mb;
            }
        }

        public String Signup(Member loginModel)
        {
            if (loginModel.CPassword.Equals(loginModel.Password))
            {
                if (db.Members.Any(x => x.Email == loginModel.Email))
                {
                    return "EmailException";
                }
                else
                {
                    try
                    {
                        var verifyUrl = "/Auth/Verify?Email=" + loginModel.Email;
                        var link = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, verifyUrl);

                        db.Members.Add(loginModel);
                        db.SaveChanges();
                        MailMessage mm = new MailMessage(ms.SupportEmail, loginModel.Email);
                        mm.Subject = "Note Marketplace -Email Verification";
                        mm.Body = "Hello " + loginModel.FirstName + " " + loginModel.LastName + "<br/><br/>Thank you for signing up with us. Please click on below link to verify your email address and to do login.<br/><br/>Link:<a href='" + link + "'>" + link + "</a><br/><br/>Regards,<br>Notes Marketplace";
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
                return "PasswordException";
            }

            return "Success";
        }

        public String login(Member member)
        {
            Member mb = db.Members.Where(x => x.Email == member.Email && x.Password == member.Password && x.IsActive == true).FirstOrDefault();
            if (mb == null)
            {
                return "UserException";
            }
            else if (mb.EmailVerified == false)
            {
                return "AccountException";
            }
            else
            {
                MemberProfile mp = db.MemberProfiles.Where(x => x.MemberId == mb.MemberId).FirstOrDefault();
                if (mp == null)
                {
                    if (member.Remember == true)
                    {
                        if (HttpContext.Current.Request.Cookies["Username"] == null && HttpContext.Current.Request.Cookies["Password"] == null)
                        {
                            HttpCookie ck = new HttpCookie("Username", member.Email);
                            ck.Expires = DateTime.Now.AddYears(1);
                            HttpContext.Current.Response.Cookies.Add(ck);

                            HttpCookie ck2 = new HttpCookie("Password", member.Password);
                            ck2.Expires = DateTime.Now.AddYears(1);
                            HttpContext.Current.Response.Cookies.Add(ck2);
                        }

                        if (HttpContext.Current.Request.Cookies["Username"].Value != member.Email)
                        {
                            HttpContext.Current.Request.Cookies["Username"].Value = member.Email;
                        }

                        if (HttpContext.Current.Request.Cookies["Password"].Value != member.Password)
                        {
                            HttpContext.Current.Request.Cookies["Password"].Value = member.Password;
                        }
                        HttpContext.Current.Session["MemberId"] = Encrypt(mb.MemberId.ToString());
                        HttpContext.Current.Session["Profile"] = ms.MemberDisplayPicture;
                        HttpContext.Current.Session.Timeout = 40;
                    }
                    else
                    {
                        if (HttpContext.Current.Request.Cookies["Username"] != null)
                        {
                            HttpCookie myCookie = new HttpCookie("Username");
                            myCookie.Expires = DateTime.Now.AddDays(-1d);
                            HttpContext.Current.Response.Cookies.Add(myCookie);
                        }

                        if (HttpContext.Current.Request.Cookies["Password"] != null)
                        {
                            HttpCookie myCookie = new HttpCookie("Password");
                            myCookie.Expires = DateTime.Now.AddDays(-1d);
                            HttpContext.Current.Response.Cookies.Add(myCookie);
                        }

                        HttpContext.Current.Session["MemberId"] = Encrypt(mb.MemberId.ToString());
                        HttpContext.Current.Session["Profile"] = ms.MemberDisplayPicture;
                        HttpContext.Current.Session.Timeout = 40;
                    }
                    return "ProfileSuccess";
                }
                else
                {
                    if (member.Remember == true)
                    {
                        if (HttpContext.Current.Request.Cookies["Username"] == null && HttpContext.Current.Request.Cookies["Password"] == null)
                        {
                            HttpCookie ck = new HttpCookie("Username", member.Email);
                            ck.Expires = DateTime.Now.AddYears(1);
                            HttpContext.Current.Response.Cookies.Add(ck);

                            HttpCookie ck2 = new HttpCookie("Password", member.Password);
                            ck2.Expires = DateTime.Now.AddYears(1);
                            HttpContext.Current.Response.Cookies.Add(ck2);
                        }

                        if (HttpContext.Current.Request.Cookies["Username"].Value != member.Email)
                        {
                            HttpContext.Current.Response.Cookies["Username"].Value = member.Email;
                        }

                        if (HttpContext.Current.Request.Cookies["Password"].Value != member.Password)
                        {
                            HttpContext.Current.Response.Cookies["Password"].Value = member.Password;
                        }

                        HttpContext.Current.Session["MemberId"] = Encrypt(mb.MemberId.ToString());
                        if (mp.ProfilePicture == null)
                        {
                            HttpContext.Current.Session["Profile"] = ms.MemberDisplayPicture;
                        }
                        else
                        {
                            HttpContext.Current.Session["Profile"] = mp.ProfilePicture;
                        }
                        HttpContext.Current.Session.Timeout = 40;
                    }
                    else
                    {
                        if (HttpContext.Current.Request.Cookies["Username"] != null)
                        {
                            HttpCookie myCookie = new HttpCookie("Username");
                            myCookie.Expires = DateTime.Now.AddDays(-1d);
                            HttpContext.Current.Response.Cookies.Add(myCookie);
                        }

                        if (HttpContext.Current.Request.Cookies["Password"] != null)
                        {
                            HttpCookie myCookie = new HttpCookie("Password");
                            myCookie.Expires = DateTime.Now.AddDays(-1d);
                            HttpContext.Current.Response.Cookies.Add(myCookie);
                        }

                        HttpContext.Current.Session["MemberId"] = Encrypt(mb.MemberId.ToString());
                        if (mp.ProfilePicture == null)
                        {
                            HttpContext.Current.Session["Profile"] = ms.MemberDisplayPicture;
                        }
                        else
                        {
                            HttpContext.Current.Session["Profile"] = mp.ProfilePicture;
                        }
                        HttpContext.Current.Session.Timeout = 40;
                    }
                    return "Success";
                }
            }
        }

        public String ChangePassword(Member b,int id)
        {
            Member member = db.Members.Where(e => e.MemberId == id && e.IsActive == true).FirstOrDefault();
            if (member.Password == b.Password)
            {
                if (b.NPassword == b.CPassword)
                {
                    member.Password = b.NPassword;
                    member.ModifiedDate = DateTime.Now;
                    db.SaveChanges();
                    return "Success";
                }
                else
                {
                    return "ConfirmException";
                }
            }
            else
            {
                return "OldPasswordException";
            }
        }

        public List<Buyer> soldout(string search, string sortBy, int id)
        {
            List<Buyer> buyers = db.Buyers.Where(e => e.OwnerId == id && e.Status == true && e.IsActive == true).ToList();
            if (!String.IsNullOrEmpty(search))
            {
                buyers = buyers.Where(e => e.NoteTitle.ToLower().Contains(search.ToLower()) || e.NoteCategory.ToLower().Contains(search.ToLower()) || e.Price.ToString()==search || e.Note.SellType.ToLower().Contains(search.ToLower()) ||e.Member1.Email.ToLower().Contains(search.ToLower())).ToList();
            }
            switch (sortBy)
            {
                case "Title asc":
                    buyers = buyers.OrderBy(e => e.NoteTitle).ToList();
                    break;
                case "Title desc":
                    buyers = buyers.OrderByDescending(e => e.NoteTitle).ToList();
                    break;
                case "Category asc":
                    buyers = buyers.OrderBy(e => e.NoteCategory).ToList();
                    break;
                case "Category desc":
                    buyers = buyers.OrderByDescending(e => e.NoteCategory).ToList();
                    break;
                case "Email asc":
                    buyers = buyers.OrderBy(e => e.Member1.Email).ToList();
                    break;
                case "Email desc":
                    buyers = buyers.OrderByDescending(e => e.Member1.Email).ToList();
                    break;
                case "Price asc":
                    buyers = buyers.OrderBy(e => e.Price).ToList();
                    break;
                case "Price desc":
                    buyers = buyers.OrderByDescending(e => e.Price).ToList();
                    break;
                case "Type asc":
                    buyers = buyers.OrderBy(e => e.Note.SellType).ToList();
                    break;
                case "Type desc":
                    buyers = buyers.OrderByDescending(e => e.Note.SellType).ToList();
                    break;
                default:
                    buyers = buyers.OrderByDescending(e => e.ApprovedDate).ToList();
                    break;
            }
            return buyers;
        }

        public List<Buyer> mydownload(string search, string sortBy, int id)
        {
            List<Buyer> buyers = db.Buyers.Where(e => e.MemberId == id && e.Status == true && e.IsActive == true).ToList();
            if (!String.IsNullOrEmpty(search))
            {
                buyers = buyers.Where(e => e.NoteTitle.ToLower().Contains(search.ToLower()) || e.NoteCategory.ToLower().Contains(search.ToLower()) || e.Price.ToString() == search || e.Note.SellType.ToLower().Contains(search.ToLower()) || e.Member.Email.ToLower().Contains(search.ToLower())).ToList();
            }
            switch (sortBy)
            {
                case "Title asc":
                    buyers = buyers.OrderBy(e => e.NoteTitle).ToList();
                    break;
                case "Title desc":
                    buyers = buyers.OrderByDescending(e => e.NoteTitle).ToList();
                    break;
                case "Category asc":
                    buyers = buyers.OrderBy(e => e.NoteCategory).ToList();
                    break;
                case "Category desc":
                    buyers = buyers.OrderByDescending(e => e.NoteCategory).ToList();
                    break;
                case "Email asc":
                    buyers = buyers.OrderBy(e => e.Member.Email).ToList();
                    break;
                case "Email desc":
                    buyers = buyers.OrderByDescending(e => e.Member.Email).ToList();
                    break;
                case "Price asc":
                    buyers = buyers.OrderBy(e => e.Price).ToList();
                    break;
                case "Price desc":
                    buyers = buyers.OrderByDescending(e => e.Price).ToList();
                    break;
                case "Type asc":
                    buyers = buyers.OrderBy(e => e.Note.SellType).ToList();
                    break;
                case "Type desc":
                    buyers = buyers.OrderByDescending(e => e.Note.SellType).ToList();
                    break;
                default:
                    buyers = buyers.OrderByDescending(e => e.ApprovedDate).ToList();
                    break;
            }
            return buyers;
        }

        public bool review(string Id, string rate2, string comment)
        {
            try
            {
                Review add = new Review();
                int id = Convert.ToInt32(Id);
                Buyer buyer = db.Buyers.Where(e => e.Id == id).FirstOrDefault();
                add.NoteId = buyer.NoteId;
                add.OwnerId = buyer.OwnerId;
                add.MemberId = buyer.MemberId;
                add.Rate = Convert.ToInt32(rate2);
                add.Description = comment;

                db.Reviews.Add(add);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool report(string Id, string remarks)
        {
            try
            {
                Inappropriate add = new Inappropriate();
                int id = Convert.ToInt32(Id);
                Buyer buyer = db.Buyers.Where(e => e.Id == id).FirstOrDefault();
                add.NoteId = buyer.NoteId;
                add.OwnerId = buyer.OwnerId;
                add.MemberId = buyer.MemberId;
                add.Remarks = remarks;
                db.Inappropriates.Add(add);
                db.SaveChanges();

                MailMessage mm = new MailMessage(ms.SupportEmail, ms.EmailAddress);
                mm.Subject = buyer.Member1.FirstName+" "+ buyer.Member1.LastName + " Reported an issue for " + buyer.NoteTitle;
                mm.Body = "Hello Admins,<br/><br/>We want to inform you that, " + buyer.Member1.FirstName + " " + buyer.Member1.LastName + " Reported an issue for " + buyer.Member.FirstName + " " + buyer.Member.LastName + "'s Note with title " + buyer.NoteTitle + ".Please look at the notes and take required actions.<br/><br/>Regards,<br/>Notes Marketplace";
                mm.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;

                NetworkCredential nc = new NetworkCredential(ms.SupportEmail, ms.EmailPassword);
                smtp.EnableSsl = true;
                smtp.Credentials = nc;
                smtp.Send(mm);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

    public class GetManageData
    {
        public static ManageSystem GetData()
        {
            DBEntities mdb = new DBEntities();
            ManageSystem mb = mdb.ManageSystems.FirstOrDefault(x => x.Id == 1);
            mb.EmailPassword = "Digi5vgec@2021";
            return mb;
        }
    }
}