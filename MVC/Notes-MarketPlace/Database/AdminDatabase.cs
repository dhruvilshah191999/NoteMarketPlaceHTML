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
using PagedList;

namespace Notes_MarketPlace.Database
{
    public class AdminDatabase
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
            if (HttpContext.Current.Session["AdminId"] != null)
            {
                id = Convert.ToInt32(Decrypt(HttpContext.Current.Session["AdminId"].ToString()));
            }
            else
            {
                id = 0;
            }
            return id;
        }

        DBEntities db = new DBEntities();
        ManageSystem ms = GetManageData.GetData();

        public String Adminlogin(Admin admin)
        {
            Admin mb = db.Admins.Where(x => x.Email == admin.Email && x.Password == admin.Password && x.IsActive == true).FirstOrDefault();
            if (mb == null)
            {
                return "UserException";
            }
            else
            {
                if (admin.Remember == true)
                {
                    if (HttpContext.Current.Request.Cookies["AdminUsername"] == null && HttpContext.Current.Request.Cookies["AdminPassword"] == null)
                    {
                        HttpCookie ck = new HttpCookie("AdminUsername", admin.Email);
                        ck.Expires = DateTime.Now.AddYears(1);
                        HttpContext.Current.Response.Cookies.Add(ck);

                        HttpCookie ck2 = new HttpCookie("AdminPassword", admin.Password);
                        ck2.Expires = DateTime.Now.AddYears(1);
                        HttpContext.Current.Response.Cookies.Add(ck2);
                    }

                    if (HttpContext.Current.Request.Cookies["AdminUsername"].Value != admin.Email)
                    {
                        HttpContext.Current.Response.Cookies["AdminUsername"].Value = admin.Email;
                    }

                    if (HttpContext.Current.Request.Cookies["AdminPassword"].Value != admin.Password)
                    {
                        HttpContext.Current.Response.Cookies["AdminPassword"].Value = admin.Password;
                    }

                    HttpContext.Current.Session["AdminId"] = Encrypt(mb.AdminId.ToString());
                    if (admin.ProfilePicture == null)
                    {
                        HttpContext.Current.Session["Profile"] = ms.MemberDisplayPicture;
                    }
                    else
                    {
                        HttpContext.Current.Session["Profile"] = admin.ProfilePicture;
                    }
                    HttpContext.Current.Session.Timeout = 40;
                }
                else
                {
                    if (HttpContext.Current.Request.Cookies["AdminUsername"] != null)
                    {
                        HttpCookie myCookie = new HttpCookie("AdminUsername");
                        myCookie.Expires = DateTime.Now.AddDays(-1d);
                        HttpContext.Current.Response.Cookies.Add(myCookie);
                    }

                    if (HttpContext.Current.Request.Cookies["AdminPassword"] != null)
                    {
                        HttpCookie myCookie = new HttpCookie("AdminPassword");
                        myCookie.Expires = DateTime.Now.AddDays(-1d);
                        HttpContext.Current.Response.Cookies.Add(myCookie);
                    }

                    HttpContext.Current.Session["AdminId"] = Encrypt(mb.AdminId.ToString());
                    if (admin.ProfilePicture == null)
                    {
                        HttpContext.Current.Session["Profile"] = ms.MemberDisplayPicture;
                    }
                    else
                    {
                        HttpContext.Current.Session["Profile"] = admin.ProfilePicture;
                    }
                    HttpContext.Current.Session.Timeout = 40;
                }
                return "Success";
            }
        }

        public List<Country> CountryData(string sortBy, string search)
        {
            List<Country> country = db.Countries.ToList<Country>();

            if (!String.IsNullOrEmpty(search))
            {
                country = country.Where(e => e.CountryName.ToLower().Contains(search.ToLower()) || e.CountryCode.ToString() == search || e.Admin.FirstName.ToLower().Contains(search.ToLower()) || e.Admin.LastName.ToLower().Contains(search.ToLower())).ToList();
            }

            switch (sortBy)
            {
                case "Name asc":
                    country = country.OrderBy(e => e.CountryName).ToList();
                    break;
                case "Name desc":
                    country = country.OrderByDescending(e => e.CountryName).ToList();
                    break;
                case "Code asc":
                    country = country.OrderBy(e => e.CountryCode).ToList();
                    break;
                case "Code desc":
                    country = country.OrderByDescending(e => e.CountryCode).ToList();
                    break;
                case "Added asc":
                    country = country.OrderBy(e => e.Admin.FirstName).ToList();
                    break;
                case "Added desc":
                    country = country.OrderByDescending(e => e.Admin.FirstName).ToList();
                    break;
                case "Active asc":
                    country = country.OrderBy(e => e.IsActive).ToList();
                    break;
                case "Active desc":
                    country = country.OrderByDescending(e => e.IsActive).ToList();
                    break;
                default:
                    country = country.OrderByDescending(e => e.CreatedDate).ToList();
                    break;
            }
            return country;
        }

        public Country getCountry(int id)
        {
            return db.Countries.Where(e => e.Id == id).FirstOrDefault();
        }

        public bool updateCountry(int countryid,Country c,int id)
        {
            try
            {
                Country country = db.Countries.Where(e => e.Id == countryid).FirstOrDefault();
                if (country.CountryName != c.CountryName)
                {
                    List<Note> notes = db.Notes.Where(e => e.Country == country.CountryName).ToList();
                    foreach (Note n in notes)
                    {
                        n.Country = c.CountryName;
                    }
                }
                country.CountryName = c.CountryName;
                country.CountryCode = c.CountryCode;
                country.ModifiedBy = id;
                country.ModifiedDate = DateTime.Now;
                country.IsActive = true;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public bool addCountry(Country c, int id)
        {
            try
            {
                c.CreatedBy = id;
                db.Countries.Add(c);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool removecountry(int countryid, int id)
        {
            try
            {
                Country country = db.Countries.Where(e => e.Id == countryid).FirstOrDefault();
                country.IsActive = false;
                country.ModifiedDate = DateTime.Now;
                country.ModifiedBy = id;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool removetype(int typeid, int id)
        {
            try
            {
                Notes_MarketPlace.Models.Type type = db.Types.Where(e => e.Id == typeid).FirstOrDefault();
                type.IsActive = false;
                type.ModifiedDate = DateTime.Now;
                type.ModifiedBy = id;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool removecategory(int categoryid, int id)
        {
            try
            {
                Category category = db.Categories.Where(e => e.Id == categoryid).FirstOrDefault();
                category.IsActive = false;
                category.ModifiedDate = DateTime.Now;
                category.ModifiedBy = id;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<Notes_MarketPlace.Models.Type> TypeData(string sortBy, string search)
        {
            List<Notes_MarketPlace.Models.Type> type = db.Types.ToList<Notes_MarketPlace.Models.Type>();

            if (!String.IsNullOrEmpty(search))
            {
                type = type.Where(e => e.Name.ToLower().Contains(search.ToLower()) || e.Description.ToLower().Contains(search.ToLower()) || e.Admin.FirstName.ToLower().Contains(search.ToLower()) || e.Admin.LastName.ToLower().Contains(search.ToLower())).ToList();
            }

            switch (sortBy)
            {
                case "Name asc":
                    type = type.OrderBy(e => e.Name).ToList();
                    break;
                case "Name desc":
                    type = type.OrderByDescending(e => e.Name).ToList();
                    break;
                case "Description asc":
                    type = type.OrderBy(e => e.Description).ToList();
                    break;
                case "Description desc":
                    type = type.OrderByDescending(e => e.Description).ToList();
                    break;
                case "Added asc":
                    type = type.OrderBy(e => e.Admin.FirstName).ToList();
                    break;
                case "Added desc":
                    type = type.OrderByDescending(e => e.Admin.FirstName).ToList();
                    break;
                case "Active asc":
                    type = type.OrderBy(e => e.IsActive).ToList();
                    break;
                case "Active desc":
                    type = type.OrderByDescending(e => e.IsActive).ToList();
                    break;
                default:
                    type = type.OrderByDescending(e => e.CreatedDate).ToList();
                    break;
            }
            return type;
        }

        public Notes_MarketPlace.Models.Type getType(int id)
        {
            return db.Types.Where(e => e.Id == id).FirstOrDefault();
        }

        public bool addtype(Notes_MarketPlace.Models.Type t, int id)
        {
            try
            {
                t.CreatedBy = id;
                db.Types.Add(t);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool updatetype(int typeid, Notes_MarketPlace.Models.Type t, int id)
        {
            try
            {
                Notes_MarketPlace.Models.Type type = db.Types.Where(e => e.Id == typeid).FirstOrDefault();
                if (type.Name != t.Name)
                {
                    List<Note> notes = db.Notes.Where(e => e.Type == type.Name).ToList();
                    foreach (Note n in notes)
                    {
                        n.Type = t.Name;
                    }
                }
                type.Name = t.Name;
                type.Description = t.Description;
                type.ModifiedBy = id;
                type.ModifiedDate = DateTime.Now;
                type.IsActive = true;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<Category> CategoryData(string sortBy, string search)
        {
            List<Category> category = db.Categories.ToList<Category>();

            if (!String.IsNullOrEmpty(search))
            {
                category = category.Where(e => e.Name.ToLower().Contains(search.ToLower()) || e.Description.ToLower().Contains(search.ToLower()) || e.Admin.FirstName.ToLower().Contains(search.ToLower()) || e.Admin.LastName.ToLower().Contains(search.ToLower())).ToList();
            }

            switch (sortBy)
            {
                case "Name asc":
                    category = category.OrderBy(e => e.Name).ToList();
                    break;
                case "Name desc":
                    category = category.OrderByDescending(e => e.Name).ToList();
                    break;
                case "Description asc":
                    category = category.OrderBy(e => e.Description).ToList();
                    break;
                case "Description desc":
                    category = category.OrderByDescending(e => e.Description).ToList();
                    break;
                case "Added asc":
                    category = category.OrderBy(e => e.Admin.FirstName).ToList();
                    break;
                case "Added desc":
                    category = category.OrderByDescending(e => e.Admin.FirstName).ToList();
                    break;
                case "Active asc":
                    category = category.OrderBy(e => e.IsActive).ToList();
                    break;
                case "Active desc":
                    category = category.OrderByDescending(e => e.IsActive).ToList();
                    break;
                default:
                    category = category.OrderByDescending(e => e.CreatedDate).ToList();
                    break;
            }
            return category;
        }

        public Category getCategory(int id)
        {
            return db.Categories.Where(e => e.Id == id).FirstOrDefault();
        }

        public bool addcategory(Category c, int id)
        {
            try
            {
                c.CreatedBy = id;
                db.Categories.Add(c);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool updatecategory(int categoryid, Category c, int id)
        {
            try
            {
                Category category = db.Categories.Where(e => e.Id == categoryid).FirstOrDefault();
                if (category.Name != c.Name)
                {
                    List<Note> notes = db.Notes.Where(e => e.Category == category.Name).ToList();
                    foreach (Note n in notes)
                    {
                        n.Category = c.Name;
                    }

                    List<Buyer> buyers = db.Buyers.Where(e => e.NoteCategory == category.Name).ToList();
                    foreach (Buyer b in buyers)
                    {
                        b.NoteCategory = c.Name;
                    }
                }
                category.Name = c.Name;
                category.Description = c.Description;
                category.ModifiedBy = id;
                category.ModifiedDate = DateTime.Now;
                category.IsActive = true;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<Inappropriate> ReportsData(string sortBy, string search)
        {
            List<Inappropriate> reports = db.Inappropriates.Where(e=>e.IsActive==true).ToList<Inappropriate>();

            if (!String.IsNullOrEmpty(search))
            {
                reports = reports.Where(e => e.Member1.FirstName.ToLower().Contains(search.ToLower()) || e.Member1.LastName.ToLower().Contains(search.ToLower()) || e.Note.NoteTitle.ToLower().Contains(search.ToLower()) || e.Note.Category.ToLower().Contains(search.ToLower()) || e.Remarks.ToLower().Contains(search.ToLower())).ToList();
            }

            switch (sortBy)
            {
                case "Name asc":
                    reports = reports.OrderBy(e => e.Member1.FirstName).ToList();
                    break;
                case "Name desc":
                    reports = reports.OrderByDescending(e => e.Member1.FirstName).ToList();
                    break;
                case "Title asc":
                    reports = reports.OrderBy(e => e.Note.NoteTitle).ToList();
                    break;
                case "Title desc":
                    reports = reports.OrderByDescending(e => e.Note.NoteTitle).ToList();
                    break;
                case "Category asc":
                    reports = reports.OrderBy(e => e.Note.Category).ToList();
                    break;
                case "Category desc":
                    reports = reports.OrderByDescending(e => e.Note.Category).ToList();
                    break;
                case "Remark asc":
                    reports = reports.OrderBy(e => e.Remarks).ToList();
                    break;
                case "Remark desc":
                    reports = reports.OrderByDescending(e => e.Remarks).ToList();
                    break;
                default:
                    reports = reports.OrderByDescending(e => e.Date).ToList();
                    break;
            }
            return reports;
        }

        public bool removereports(int reportId,int id)
        {
            Inappropriate report = db.Inappropriates.Where(e => e.Id == reportId && e.IsActive == true).FirstOrDefault();
            report.DeactivateBy = id;
            report.DeactivateDate = DateTime.Now;
            report.IsActive = false;
            db.SaveChanges();
            return true;
        }

        public List<Member> MemberData(string sortBy, string search)
        {
            List<Member> members = db.Members.Where(e => e.IsActive == true).ToList<Member>();

            if (!String.IsNullOrEmpty(search))
            {
                members = members.Where(e => e.FirstName.ToLower().Contains(search.ToLower()) || e.LastName.ToLower().Contains(search.ToLower()) || e.Email.ToLower().Contains(search.ToLower()) || e.Notes.Where(e1 => e1.Status == "Published").ToList().Count().ToString()==search || e.Notes.Where(e1 => e1.Status != "Published" && e1.Status != "Rejected").ToList().Count().ToString() == search || e.Buyers1.Where(e1 => e1.Status == true).ToList().Sum(e1 => e1.Price).ToString() == search || e.Buyers1.Where(e1 => e1.Status == true).ToList().Count().ToString() == search || e.Buyers.Where(e1 => e1.Status == true).ToList().Sum(e1 => e1.Price).ToString() == search).ToList();
            }

            switch (sortBy)
            {
                case "FName asc":
                    members = members.OrderBy(e => e.FirstName).ToList();
                    break;
                case "FName desc":
                    members = members.OrderByDescending(e => e.FirstName).ToList();
                    break;
                case "LName asc":
                    members = members.OrderBy(e => e.LastName).ToList();
                    break;
                case "LName desc":
                    members = members.OrderByDescending(e => e.LastName).ToList();
                    break;
                case "Email asc":
                    members = members.OrderBy(e => e.Email).ToList();
                    break;
                case "Email desc":
                    members = members.OrderByDescending(e => e.Email).ToList();
                    break;
                case "URN asc":
                    members = members.OrderBy(e=>e.Notes.Where(e1 => e1.Status != "Published" && e1.Status != "Rejected").ToList().Count()).ToList();
                    break;
                case "URN desc":
                    members = members.OrderByDescending(e => e.Notes.Where(e1 => e1.Status != "Published" && e1.Status != "Rejected").ToList().Count()).ToList();
                    break;
                case "PN asc":
                    members = members.OrderBy(e => e.Notes.Where(e1 => e1.Status == "Published").ToList().Count()).ToList();
                    break;
                case "PN desc":
                    members = members.OrderByDescending(e => e.Notes.Where(e1 => e1.Status == "Published").ToList().Count()).ToList();
                    break;
                case "DN asc":
                    members = members.OrderBy(e => e.Buyers1.Where(e1 => e1.Status == true).ToList().Count()).ToList();
                    break;
                case "DN desc":
                    members = members.OrderByDescending(e => e.Buyers1.Where(e1 => e1.Status == true).ToList().Count()).ToList();
                    break;
                case "Expenses asc":
                    members = members.OrderBy(e => e.Buyers1.Where(e1 => e1.Status == true).ToList().Sum(e1 => e1.Price)).ToList();
                    break;
                case "Expenses desc":
                    members = members.OrderByDescending(e => e.Buyers1.Where(e1 => e1.Status == true).ToList().Sum(e1 => e1.Price)).ToList();
                    break;
                case "Earnings asc":
                    members = members.OrderBy(e => e.Buyers.Where(e1 => e1.Status == true).ToList().Sum(e1 => e1.Price)).ToList();
                    break;
                case "Earnings desc":
                    members = members.OrderByDescending(e => e.Buyers.Where(e1 => e1.Status == true).ToList().Sum(e1 => e1.Price)).ToList();
                    break;
                default:
                    members = members.OrderByDescending(e => e.CreatedDate).ToList();
                    break;
            }
            return members;
        }

        public MemberData GetMemberData(int MemberId,int ? i, string sortBy)
        {
            MemberData md1 = new MemberData();
            md1.member = db.Members.Where(e => e.MemberId == MemberId && e.IsActive == true).FirstOrDefault();
            if (md1.member != null)
            {
                List<Note> notes = md1.member.Notes.ToList();
                switch (sortBy)
                {
                    case "Title asc":
                        notes = notes.OrderBy(e => e.NoteTitle).ToList();
                        break;
                    case "Title desc":
                        notes = notes.OrderByDescending(e => e.NoteTitle).ToList();
                        break;
                    case "Category asc":
                        notes = notes.OrderBy(e => e.Category).ToList();
                        break;
                    case "Category desc":
                        notes = notes.OrderByDescending(e => e.Category).ToList();
                        break;
                    case "Status asc":
                        notes = notes.OrderBy(e => e.Status).ToList();
                        break;
                    case "Status desc":
                        notes = notes.OrderByDescending(e => e.Status).ToList();
                        break;
                    case "DN asc":
                        notes = notes.OrderBy(e => e.Buyers.Where(e1 => e1.Status == true).ToList().Count()).ToList();
                        break;
                    case "DN desc":
                        notes = notes.OrderByDescending(e => e.Buyers.Where(e1 => e1.Status == true).ToList().Count()).ToList();
                        break;
                    case "Earnings asc":
                        notes = notes.OrderBy(e => e.Buyers.Where(e1 => e1.Status == true).ToList().Sum(e1 => e1.Price)).ToList();
                        break;
                    case "Earnings desc":
                        notes = notes.OrderByDescending(e => e.Buyers.Where(e1 => e1.Status == true).ToList().Sum(e1 => e1.Price)).ToList();
                        break;
                    default:
                        notes = notes.OrderByDescending(e => e.CreatedDate).ToList();
                        break;
                }
                md1.notes = notes.ToPagedList(i ?? 1, 5);
            }
            md1.mp = db.MemberProfiles.Where(e => e.MemberId == MemberId).FirstOrDefault();
            md1.manage = ms;
            return md1;
        }

        public Note getnoteData(int id)
        {
            return db.Notes.Where(e => e.NoteId == id && e.IsActive == true).FirstOrDefault();
        }

        public bool removereview(int Reviewid,int id)
        {
            Review review = db.Reviews.Where(e => e.Id == Reviewid && e.IsActive == true).FirstOrDefault();
            review.IsActive = false;
            review.ModifiedDate = DateTime.Now;
            review.ModifiedBy = id;
            db.SaveChanges();
            return true;
        }

        public bool removemember(int MemberId, int id)
        {
            Member member = db.Members.Where(e => e.MemberId == MemberId && e.IsActive == true).FirstOrDefault();
            member.IsActive = false;
            member.DeactivateDate = DateTime.Now;
            member.DeactivateBy = id;
            db.SaveChanges();
            return true;
        }

        public List<String> getRejectedName()
        {
            return db.Notes.Where(e => e.Status == "Rejected" && e.IsActive==true).Select(e => e.Member.FirstName).Distinct().ToList();
        } 

        public List<Note> getRejectedNote(string sortBy, string search,string name)
        {
            List<Note> notes = db.Notes.Where(e => e.Status == "Rejected" && e.IsActive == true).ToList();
            foreach(Note n in notes)
            {
                string fname = db.Admins.Where(e => e.AdminId == n.ModifiedBy).Select(e => e.FirstName).FirstOrDefault();
                string lname = db.Admins.Where(e => e.AdminId == n.ModifiedBy).Select(e => e.LastName).FirstOrDefault();
                n.rejectedName = fname + " " + lname;
            }
            if (!String.IsNullOrEmpty(name))
            {
                notes = notes.Where(e => e.Member.FirstName.ToLower().Contains(name.ToLower())).ToList();
            }
            if (!String.IsNullOrEmpty(search))
            {
                notes = notes.Where(e => e.NoteTitle.ToLower().Contains(search.ToLower()) || e.Category.ToLower().Contains(search.ToLower()) || e.Member.FirstName.ToLower().Contains(search.ToLower()) || e.Member.LastName.ToLower().Contains(search.ToLower()) || (e.AdminRemarks != null && e.AdminRemarks.ToLower().Contains(search.ToLower())) || e.rejectedName.ToLower().Contains(search.ToLower())).ToList();
            }

            switch (sortBy)
            {
                case "Title asc":
                    notes = notes.OrderBy(e => e.NoteTitle).ToList();
                    break;
                case "Title desc":
                    notes = notes.OrderByDescending(e => e.NoteTitle).ToList();
                    break;
                case "Category asc":
                    notes = notes.OrderBy(e => e.Category).ToList();
                    break;
                case "Category desc":
                    notes = notes.OrderByDescending(e => e.Category).ToList();
                    break;
                case "Seller asc":
                    notes = notes.OrderBy(e => e.Member.FirstName).ToList();
                    break;
                case "Seller desc":
                    notes = notes.OrderByDescending(e => e.Member.FirstName).ToList();
                    break;
                case "Rejected asc":
                    notes = notes.OrderBy(e => e.rejectedName).ToList();
                    break;
                case "Rejected desc":
                    notes = notes.OrderByDescending(e => e.rejectedName).ToList();
                    break;
                case "Remark asc":
                    notes = notes.OrderBy(e => e.AdminRemarks).ToList();
                    break;
                case "Remark desc":
                    notes = notes.OrderByDescending(e => e.AdminRemarks).ToList();
                    break;
                default:
                    notes = notes.OrderByDescending(e => e.ModifiedDate).ToList();
                    break;
            }
            return notes;
        }

        public bool approvednote(int NoteId, int id)
        {
            Note n = db.Notes.Where(e => e.NoteId == NoteId && e.IsActive == true).FirstOrDefault();
            n.ModifiedBy = null;
            n.ApprovedBy = id;
            n.ApprovedDate = DateTime.Now;
            n.Status = "Published";
            db.SaveChanges();
            return true;
        }

        public List<String> getNoteTitle()
        {
            return db.Buyers.Where(e => e.Status==true && e.IsActive==true).Select(e => e.NoteTitle).Distinct().ToList();
        }

        public List<String> getSellerName()
        {
            return db.Buyers.Where(e => e.Status == true && e.IsActive == true).Select(e => e.Member.FirstName).Distinct().ToList();
        }

        public List<String> getBuyerName()
        {
            return db.Buyers.Where(e => e.Status == true && e.IsActive == true).Select(e => e.Member1.FirstName).Distinct().ToList();
        }

        public List<Buyer> getdownload(string sortBy, string search, string sellername, string notename, string buyername)
        {
            List<Buyer> buyers = db.Buyers.Where(e => e.Status == true && e.IsActive == true).ToList();
            if (!String.IsNullOrEmpty(sellername))
            {
                buyers = buyers.Where(e => e.Member.FirstName.ToLower().Contains(sellername.ToLower())).ToList();
            }
            if (!String.IsNullOrEmpty(buyername))
            {
                buyers = buyers.Where(e => e.Member1.FirstName.ToLower().Contains(buyername.ToLower())).ToList();
            }
            if (!String.IsNullOrEmpty(notename))
            {
                buyers = buyers.Where(e => e.NoteTitle.ToLower().Contains(notename.ToLower())).ToList();
            }
            if (!String.IsNullOrEmpty(search))
            {
                buyers = buyers.Where(e => e.NoteTitle.ToLower().Contains(search.ToLower()) || e.NoteCategory.ToLower().Contains(search.ToLower()) || e.Member.FirstName.ToLower().Contains(search.ToLower()) || e.Member.LastName.ToLower().Contains(search.ToLower()) || e.Member1.FirstName.ToLower().Contains(search.ToLower()) || e.Member1.LastName.ToLower().Contains(search.ToLower()) || e.Note.SellType.ToLower().Contains(search.ToLower()) || e.Price.ToString()==search).ToList();
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
                case "Seller asc":
                    buyers = buyers.OrderBy(e => e.Member.FirstName).ToList();
                    break;
                case "Seller desc":
                    buyers = buyers.OrderByDescending(e => e.Member.FirstName).ToList();
                    break;
                case "Buyer asc":
                    buyers = buyers.OrderBy(e => e.Member1.FirstName).ToList();
                    break;
                case "Buyer desc":
                    buyers = buyers.OrderByDescending(e => e.Member1.FirstName).ToList();
                    break;
                case "SellType asc":
                    buyers = buyers.OrderBy(e => e.Note.SellType).ToList();
                    break;
                case "SellType desc":
                    buyers = buyers.OrderByDescending(e => e.Note.SellType).ToList();
                    break;
                case "Price asc":
                    buyers = buyers.OrderBy(e => e.Price).ToList();
                    break;
                case "Price desc":
                    buyers = buyers.OrderByDescending(e => e.Price).ToList();
                    break;
                default:
                    buyers = buyers.OrderByDescending(e => e.ApprovedDate).ToList();
                    break;
            }
            return buyers;
        }

        public List<String> getNotePublished()
        {
            return db.Notes.Where(e => e.Status == "Published" && e.IsActive == true).Select(e => e.Member.FirstName).Distinct().ToList();
        }

        public List<Note> getPublished(string sortBy, string search, string name)
        {
            List<Note> notes = db.Notes.Where(e => e.Status == "Published" && e.IsActive == true).ToList();
            foreach (Note n in notes)
            {
                string fname = db.Admins.Where(e => e.AdminId == n.ApprovedBy).Select(e => e.FirstName).FirstOrDefault();
                string lname = db.Admins.Where(e => e.AdminId == n.ApprovedBy).Select(e => e.LastName).FirstOrDefault();
                n.rejectedName = fname + " " + lname;
            }
            if (!String.IsNullOrEmpty(name))
            {
                notes = notes.Where(e => e.Member.FirstName.ToLower().Contains(name.ToLower())).ToList();
            }
            if (!String.IsNullOrEmpty(search))
            {
                notes = notes.Where(e => e.NoteTitle.ToLower().Contains(search.ToLower()) || e.Category.ToLower().Contains(search.ToLower()) || e.Member.FirstName.ToLower().Contains(search.ToLower()) || e.Member.LastName.ToLower().Contains(search.ToLower()) || e.SellType.ToLower().Contains(search.ToLower()) || e.SellPrice.ToString()==search || e.rejectedName.ToLower().Contains(search.ToLower()) || e.Buyers.Where(e1=>e1.Status==true).ToList().Count().ToString()==search).ToList();
            }
            
            switch (sortBy)
            {
                case "Title asc":
                    notes = notes.OrderBy(e => e.NoteTitle).ToList();
                    break;
                case "Title desc":
                    notes = notes.OrderByDescending(e => e.NoteTitle).ToList();
                    break;
                case "Category asc":
                    notes = notes.OrderBy(e => e.Category).ToList();
                    break;
                case "Category desc":
                    notes = notes.OrderByDescending(e => e.Category).ToList();
                    break;
                case "Seller asc":
                    notes = notes.OrderBy(e => e.Member.FirstName).ToList();
                    break;
                case "Seller desc":
                    notes = notes.OrderByDescending(e => e.Member.FirstName).ToList();
                    break;
                case "SellType asc":
                    notes = notes.OrderBy(e => e.SellType).ToList();
                    break;
                case "SellType desc":
                    notes = notes.OrderByDescending(e => e.SellType).ToList();
                    break;
                case "Price asc":
                    notes = notes.OrderBy(e => e.SellPrice).ToList();
                    break;
                case "Price desc":
                    notes = notes.OrderByDescending(e => e.SellPrice).ToList();
                    break;
                case "Approve asc":
                    notes = notes.OrderBy(e => e.rejectedName).ToList();
                    break;
                case "Approve desc":
                    notes = notes.OrderByDescending(e => e.rejectedName).ToList();
                    break;
                case "Download asc":
                    notes = notes.OrderBy(e => e.Buyers.Where(e1 => e1.Status == true).ToList().Count()).ToList();
                    break;
                case "Download desc":
                    notes = notes.OrderByDescending(e => e.Buyers.Where(e1 => e1.Status == true).ToList().Count()).ToList();
                    break;
                default:
                    notes = notes.OrderByDescending(e => e.ApprovedDate).ToList();
                    break;
            }
            return notes;
        }

        public Admin getprofile(int id)
        {
            Admin admin = db.Admins.Where(e => e.AdminId == id && e.IsActive == true).FirstOrDefault();
            return admin;
        }

        public List<Country> CountryData()
        {
            return db.Countries.Where(e => e.IsActive == true).ToList<Country>();
        }

        public bool updateprofile(Admin admin,int id)
        {
            Admin getadmin = db.Admins.Where(e => e.AdminId == id && e.IsActive == true).FirstOrDefault();
            if (admin.ImageUrl != null)
            {
                var supportedTypes = new[] { ".jpg", ".jpeg", ".png" };
                string extension = System.IO.Path.GetExtension(admin.ImageUrl.FileName);
                if (!supportedTypes.Contains(extension) || admin.ImageUrl.ContentLength > 10485760)
                {
                    return false;
                }
                else
                {
                    if (getadmin.ProfilePicture != null)
                    {
                        string fullPath = HttpContext.Current.Request.MapPath("~/UploadFiles/AdminPhoto/" + getadmin.ProfilePicture);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                    }

                    string filename = System.IO.Path.GetFileNameWithoutExtension(admin.ImageUrl.FileName);
                    filename = admin.FirstName + "_" + DateTime.Now.ToString("yymmssfff") + extension;
                    getadmin.ProfilePicture = filename;
                    filename = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/UploadFiles/AdminPhoto/"), filename);
                    admin.ImageUrl.SaveAs(filename);
                    HttpContext.Current.Session["Profile"] = admin.ProfilePicture;
                }
            }
            getadmin.FirstName = admin.FirstName;
            getadmin.LastName = admin.LastName;
            getadmin.CountryCode = admin.CountryCode;
            getadmin.PhoneNumber = admin.PhoneNumber;
            getadmin.SecondaryEmail = admin.SecondaryEmail;
            getadmin.ModifiedDate = DateTime.Now;
            db.SaveChanges();
            return true;
        }

        public String ChangePassword(Admin b, int id)
        {
            Admin admin = db.Admins.Where(e => e.AdminId == id && e.IsActive == true).FirstOrDefault();
            if (admin.Password == b.Password)
            {
                if (b.NPassword == b.CPassword)
                {
                    admin.Password = b.NPassword;
                    admin.ModifiedDate = DateTime.Now;
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
    }
}