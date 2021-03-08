using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Notes_MarketPlace.Models;
using System.Net;
using System.Net.Mail;
using PagedList;

namespace Notes_MarketPlace.Database
{
    public class DatabaseFile
    {
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
                    if (reviews.Count != 0)
                    {
                        n.Avgrate = Convert.ToInt32(reviews.Average(e => e.Rate));
                        if (n.Avgrate == 0)
                        {
                            n.Avgrate = 5;
                        }
                    }
                    else
                    {
                        n.Avgrate = 5;
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
            return db.Countries.ToList<Country>();
        }

        public List<Category> CategoryData()
        {
            return db.Categories.ToList<Category>();
        }

        public List<Notes_MarketPlace.Models.Type> TypeData()
        {
            return db.Types.ToList<Notes_MarketPlace.Models.Type>();
        }

        public List<String> UniversityData()
        {
            return db.Notes.Where(e => e.InstitutionName != null).Select(e => e.InstitutionName).Distinct().ToList();
        }

        public List<String> CourseData()
        {
            return db.Notes.Where(e => e.CourseName != null).Select(e => e.CourseName).Distinct().ToList();
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