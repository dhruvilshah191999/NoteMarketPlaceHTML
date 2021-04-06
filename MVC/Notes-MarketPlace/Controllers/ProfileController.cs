using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Notes_MarketPlace.Models;
using PagedList;
using System.Data;
using Notes_MarketPlace.Database;

namespace Notes_MarketPlace.Controllers
{
    public class ProfileController : Controller
    {
        private DatabaseFile db = new DatabaseFile();
        private int GetId()
        {
            int id;
            if (Session["MemberId"] != null)
            {
                id = Convert.ToInt32(db.Decrypt(Session["MemberId"].ToString()));
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
            
            int id = GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Auth", new { url = path });
            }
            ViewData["Country"] = db.CountryData().OrderBy(e => e.CountryCode);
            MemberProfile mp = db.ProfileGet(id);
            return View(mp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(MemberProfile mb)
        {
            int id = GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Auth", new { url = path });
            }

            bool result = db.ProfilePost(id, mb);
            if (result)
            {
                TempData["SucessMessage"] = "Your Profile has been updated";
                return RedirectToAction("Index", "Profile");
            }
            else
            {
                TempData["ErrorMessage"] = "Profile Photo less than 10MB or Only image are accepted";
                return RedirectToAction("Index", "Profile");
            }
        }

        public ActionResult Rejected(string search,int ? i,string sortBy)
        {
            ViewBag.SortTitle = String.IsNullOrEmpty(sortBy) ? "Title desc" : sortBy.Equals("Title asc") ? "Title desc" : "Title asc";
            ViewBag.SortCategory = String.IsNullOrEmpty(sortBy) ? "Category desc" : sortBy.Equals("Category asc") ? "Category desc" : "Category asc";
            ViewBag.SortRemarks = String.IsNullOrEmpty(sortBy) ? "Remarks desc" : sortBy.Equals("Remarks asc") ? "Remarks desc" : "Remarks asc";
            ViewBag.Viewpage = 5;
            int id=GetId();
            if (id == 0) 
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Auth",new { url=path });
            }
            List<Note> notes = db.Rejected(search, sortBy, id);
            return View(notes.ToList().ToPagedList(i??1,5));
        }

        public ActionResult SoldOut(string search, int? i, string sortBy)
        {
            ViewBag.SortTitle = String.IsNullOrEmpty(sortBy) ? "Title desc" : sortBy.Equals("Title asc") ? "Title desc" : "Title asc";
            ViewBag.SortCategory = String.IsNullOrEmpty(sortBy)?"Category desc": sortBy.Equals("Category asc") ? "Category desc" : "Category asc";
            ViewBag.SortEmail = String.IsNullOrEmpty(sortBy) ? "Email desc" : sortBy.Equals("Email asc") ? "Email desc" : "Email asc";
            ViewBag.SortPrice = String.IsNullOrEmpty(sortBy) ? "Price desc" : sortBy.Equals("Price asc") ? "Price desc" : "Price asc";
            ViewBag.SortType = String.IsNullOrEmpty(sortBy) ? "Type desc" : sortBy.Equals("Type asc") ? "Type desc" : "Type asc";
            ViewBag.Viewpage = 5;
            int id = GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Auth", new { url = path });
            }
            List<Buyer> buyers = db.soldout(search, sortBy, id);
            return View(buyers.ToList().ToPagedList(i ?? 1, 5));
        }

        public ActionResult MyDownload(string search, int? i, string sortBy)
        {
            ViewBag.SortTitle = String.IsNullOrEmpty(sortBy) ? "Title desc" : sortBy.Equals("Title asc") ? "Title desc" : "Title asc";
            ViewBag.SortCategory = String.IsNullOrEmpty(sortBy) ? "Category desc" : sortBy.Equals("Category asc") ? "Category desc" : "Category asc";
            ViewBag.SortEmail = String.IsNullOrEmpty(sortBy) ? "Email desc" : sortBy.Equals("Email asc") ? "Email desc" : "Email asc";
            ViewBag.SortPrice = String.IsNullOrEmpty(sortBy) ? "Price desc" : sortBy.Equals("Price asc") ? "Price desc" : "Price asc";
            ViewBag.SortType = String.IsNullOrEmpty(sortBy) ? "Type desc" : sortBy.Equals("Type asc") ? "Type desc" : "Type asc";
            ViewBag.Viewpage = 5;
            int id = GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Auth", new { url = path });
            }
            List<Buyer> buyers = db.mydownload(search, sortBy, id);
            return View(buyers.ToList().ToPagedList(i ?? 1, 5));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Review(string Id,string rate2,string comment)
        {
            bool result = db.review(Id, rate2, comment);
            return RedirectToAction("MyDownload", "Profile");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Report(string Id,string remarks)
        {
            bool result = db.report(Id, remarks);
            return RedirectToAction("MyDownload", "Profile");
        }
    }
}