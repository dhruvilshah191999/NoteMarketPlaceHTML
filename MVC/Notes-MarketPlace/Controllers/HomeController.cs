using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Notes_MarketPlace.Models;
using System.Net;
using System.Net.Mail;
using PagedList;
using Notes_MarketPlace.Database;

namespace Notes_MarketPlace.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseFile db = new DatabaseFile();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Search(int ? i,string search,string rating,string country,string category,string type,string course,string university)
        {
            DBEntities db = new DBEntities();
            ManageSystem ms = GetManageData.GetData();
            ViewData["Country"] = db.Countries.ToList<Country>();
            ViewData["Category"] = db.Categories.ToList<Category>();
            ViewData["Type"] = db.Types.ToList<Notes_MarketPlace.Models.Type>();
            ViewData["University"] = db.Notes.Where(e => e.InstitutionName != null).Select(e => e.InstitutionName).Distinct().ToList();
            ViewData["Course"] = db.Notes.Where(e => e.CourseName != null).Select(e => e.CourseName).Distinct().ToList();
            List<Note> notes = db.Notes.Where(e=>e.IsActive==true).ToList();
            ViewBag.Photo = ms.NoteDisplayPicture;
            foreach(Note n in notes)
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
            if(rating!= "Select rating" && rating != null)
            {
                notes = notes.Where(e => e.Avgrate==Convert.ToInt32(rating)).ToList();
            }
            if (country != "Select country" && !String.IsNullOrEmpty(country))
            {
                notes = notes.Where(e => e.Country!=null && e.Country.ToLower().Contains(country.ToLower())).ToList();
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
            ViewBag.NodeCount = notes.Count();
            return View(notes.ToList().ToPagedList(i ?? 1, 9));
        }

        [HttpGet]
        public ActionResult Contact()
        {
            Contactu contact = new Contactu();
            return View(contact);
        }

        [HttpPost]
        public ActionResult Contact(Contactu contact)
        {
            bool contactbool = db.contactus(contact);
            if (contactbool)
            {
                return RedirectToAction("Contact", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult FAQ()
        {
            return View();
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