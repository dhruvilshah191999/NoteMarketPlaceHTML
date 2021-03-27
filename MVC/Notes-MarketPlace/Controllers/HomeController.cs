using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Notes_MarketPlace.Models;
using PagedList;
using Notes_MarketPlace.Database;

namespace Notes_MarketPlace.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseFile db = new DatabaseFile();
        private ManageSystem ms= GetManageData.GetData();
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Search(int ? i,string search,string rating,string country,string category,string type,string course,string university)
        {     
            ViewBag.Photo = ms.NoteDisplayPicture;
            ViewData["Country"] = db.CountryData();
            ViewData["Category"] = db.CategoryData();
            ViewData["Type"] = db.TypeData();
            ViewData["University"] = db.UniversityData();
            ViewData["Course"] = db.CourseData();
            List<Note> notes = db.search(search, rating, country, category, type, course, university);
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
        [ValidateAntiForgeryToken]
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