using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Mvc;
using Notes_MarketPlace.Models;
using Notes_MarketPlace.Database;

namespace Notes_MarketPlace.Controllers
{
    public class AdminController : Controller
    {
        private AdminDatabase db = new AdminDatabase();
        private ManageSystem ms = GetManageData.GetData();

        [HttpGet]
        public ActionResult Index()
        {
            Admin admin = new Admin();
            if (Request.Cookies["AdminUsername"] != null && Request.Cookies["AdminPassword"] != null)
            {
                admin.Email = Request.Cookies["AdminUsername"].Value;
                admin.Password = Request.Cookies["AdminPassword"].Value;
                admin.Remember = true;
            }
            ViewBag.ErrorStatus = false;
            ViewBag.SucessStatus = false;
            return View(admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Admin admin, string url)
        {
            String result = db.Adminlogin(admin);
            if (result == "UserException")
            {
                ViewBag.ErrorStatus = true;
                ViewBag.ErrorMessage = "Username or Password Invalid";
                ViewBag.SucessStatus = false;
                return View(admin);
            }
            else
            {
                if (!String.IsNullOrEmpty(url))
                {
                    return Redirect(url);
                }
                else
                {
                    return RedirectToAction("DashBoard", "Admin");
                }
            }
        }

        [HttpGet]
        public ActionResult ManageCountry(int ? i, string search, string sortBy)
        {
            ViewBag.SortName = String.IsNullOrEmpty(sortBy) ? "Name desc" : sortBy.Equals("Name asc") ? "Name desc" : "Name asc";
            ViewBag.SortCode = String.IsNullOrEmpty(sortBy) ? "Code desc" : sortBy.Equals("Code asc") ? "Code desc" : "Code asc";
            ViewBag.SortAdded = String.IsNullOrEmpty(sortBy) ? "Added desc" : sortBy.Equals("Added asc") ? "Added desc" : "Added asc";
            ViewBag.SortActive = String.IsNullOrEmpty(sortBy) ? "Active desc" : sortBy.Equals("Active asc") ? "Active desc" : "Active asc";
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            ViewBag.Viewpage = 5;
            List<Country> country = db.CountryData(sortBy,search);
            return View(country.ToList().ToPagedList(i ?? 1,5));
        }

        [HttpGet]
        public ActionResult Country(int ? countryid)
        {
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }

            Country c;
            if (String.IsNullOrEmpty(countryid.ToString()))
            {
                c = new Country();
            }
            else
            {
                c = db.getCountry(countryid.Value);
            }
            return View(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Country(string countryid,Country c)
        {
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            bool check;
            if (String.IsNullOrEmpty(countryid))
            {
                check = db.addCountry(c, id);
            }
            else
            {
                check=db.updateCountry(Convert.ToInt32(countryid),c,id);
            }
            return RedirectToAction("ManageCountry","Admin");
        }

        [HttpGet]
        public ActionResult Remove(int ? countryid,int ? typeid,int ? categoryid)
        {
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }

            if (!String.IsNullOrEmpty(countryid.ToString()))
            {
                bool check = db.removecountry(countryid.Value, id);
                return RedirectToAction("ManageCountry", "Admin");
            }
            else if (!String.IsNullOrEmpty(typeid.ToString()))
            {
                bool check = db.removetype(typeid.Value, id);
                return RedirectToAction("ManageType", "Admin");
            }
            else if (!String.IsNullOrEmpty(categoryid.ToString()))
            {
                bool check = db.removecategory(categoryid.Value, id);
                return RedirectToAction("ManageCategory", "Admin");
            }
            else
            {
                return RedirectToAction("ManageCountry", "Admin");
            }
        }

        [HttpGet]
        public ActionResult ManageType(int? i, string search, string sortBy)
        {
            ViewBag.SortName = String.IsNullOrEmpty(sortBy) ? "Name desc" : sortBy.Equals("Name asc") ? "Name desc" : "Name asc";
            ViewBag.SortDescription = String.IsNullOrEmpty(sortBy) ? "Description desc" : sortBy.Equals("Description asc") ? "Description desc" : "Description asc";
            ViewBag.SortAdded = String.IsNullOrEmpty(sortBy) ? "Added desc" : sortBy.Equals("Added asc") ? "Added desc" : "Added asc";
            ViewBag.SortActive = String.IsNullOrEmpty(sortBy) ? "Active desc" : sortBy.Equals("Active asc") ? "Active desc" : "Active asc";
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            ViewBag.Viewpage = 5;
            List<Notes_MarketPlace.Models.Type> type = db.TypeData(sortBy, search);
            return View(type.ToList().ToPagedList(i ?? 1, 5));
        }

        [HttpGet]
        public ActionResult Type(int? typeid)
        {
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }

            Notes_MarketPlace.Models.Type t;
            if (String.IsNullOrEmpty(typeid.ToString()))
            {
                t = new Notes_MarketPlace.Models.Type();
            }
            else
            {
                t = db.getType(typeid.Value);
            }
            return View(t);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Type(string typeid, Notes_MarketPlace.Models.Type t)
        {
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            bool check;
            if (String.IsNullOrEmpty(typeid))
            {
                check = db.addtype(t, id);
            }
            else
            {
                check = db.updatetype(Convert.ToInt32(typeid), t, id);
            }
            return RedirectToAction("ManageType", "Admin");
        }

        [HttpGet]
        public ActionResult ManageCategory(int? i, string search, string sortBy)
        {
            ViewBag.SortName = String.IsNullOrEmpty(sortBy) ? "Name desc" : sortBy.Equals("Name asc") ? "Name desc" : "Name asc";
            ViewBag.SortDescription = String.IsNullOrEmpty(sortBy) ? "Description desc" : sortBy.Equals("Description asc") ? "Description desc" : "Description asc";
            ViewBag.SortAdded = String.IsNullOrEmpty(sortBy) ? "Added desc" : sortBy.Equals("Added asc") ? "Added desc" : "Added asc";
            ViewBag.SortActive = String.IsNullOrEmpty(sortBy) ? "Active desc" : sortBy.Equals("Active asc") ? "Active desc" : "Active asc";
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            ViewBag.Viewpage = 5;
            List<Category> category = db.CategoryData(sortBy, search);
            return View(category.ToList().ToPagedList(i ?? 1, 5));
        }

        [HttpGet]
        public ActionResult Category(int? categoryid)
        {
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }

            Category c; ;
            if (String.IsNullOrEmpty(categoryid.ToString()))
            {
                c = new Category();
            }
            else
            {
                c = db.getCategory(categoryid.Value);
            }
            return View(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Category(string categoryid, Category c)
        {
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            bool check;
            if (String.IsNullOrEmpty(categoryid))
            {
                check = db.addcategory(c, id);
            }
            else
            {
                check = db.updatecategory(Convert.ToInt32(categoryid), c, id);
            }
            return RedirectToAction("ManageCategory", "Admin");
        }

        [HttpGet]
        public ActionResult SpamReports(int? i, string search, string sortBy)
        {
            ViewBag.SortName = String.IsNullOrEmpty(sortBy) ? "Name desc" : sortBy.Equals("Name asc") ? "Name desc" : "Name asc";
            ViewBag.SortTitle = String.IsNullOrEmpty(sortBy) ? "Title desc" : sortBy.Equals("Title asc") ? "Title desc" : "Title asc";
            ViewBag.SortCategory = String.IsNullOrEmpty(sortBy) ? "Category desc" : sortBy.Equals("Category asc") ? "Category desc" : "Category asc";
            ViewBag.SortRemark = String.IsNullOrEmpty(sortBy) ? "Remark desc" : sortBy.Equals("Remark asc") ? "Remark desc" : "Remark asc";
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            ViewBag.Viewpage = 5;
            List<Inappropriate> reports = db.ReportsData(sortBy, search);
            return View(reports.ToList().ToPagedList(i ?? 1, 5));
        }

        [HttpGet]
        public ActionResult RemoveReport(int Id)
        {
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }

            bool check = db.removereports(Id, id);
            return RedirectToAction("SpamReports", "Admin");
        }

        [HttpGet]
        public ActionResult Member(int? i, string search, string sortBy)
        {
            ViewBag.SortFName = String.IsNullOrEmpty(sortBy) ? "FName desc" : sortBy.Equals("FName asc") ? "FName desc" : "FName asc";
            ViewBag.SortLName = String.IsNullOrEmpty(sortBy) ? "LName desc" : sortBy.Equals("LName asc") ? "LName desc" : "LName asc";
            ViewBag.SortEmail = String.IsNullOrEmpty(sortBy) ? "Email desc" : sortBy.Equals("Email asc") ? "Email desc" : "Email asc";
            ViewBag.SortURN = String.IsNullOrEmpty(sortBy) ? "URN desc" : sortBy.Equals("URN asc") ? "URN desc" : "URN asc";
            ViewBag.SortPN = String.IsNullOrEmpty(sortBy) ? "PN desc" : sortBy.Equals("PN asc") ? "PN desc" : "PN asc";
            ViewBag.SortDN = String.IsNullOrEmpty(sortBy) ? "DN desc" : sortBy.Equals("DN asc") ? "DN desc" : "DN asc";
            ViewBag.SortExpenses = String.IsNullOrEmpty(sortBy) ? "Expenses desc" : sortBy.Equals("Expenses asc") ? "Expenses desc" : "Expenses asc";
            ViewBag.SortEarnings = String.IsNullOrEmpty(sortBy) ? "Earnings desc" : sortBy.Equals("Earnings asc") ? "Earnings desc" : "Earnings asc";

            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            ViewBag.Viewpage = 5;
            List<Member> member = db.MemberData(sortBy, search);
            return View(member.ToList().ToPagedList(i ?? 1, 5));
        }

        [HttpGet]
        public ActionResult MemberDetails(int MemberId,int ? i,string sortBy)
        {
            ViewBag.SortTitle = String.IsNullOrEmpty(sortBy) ? "Title desc" : sortBy.Equals("Title asc") ? "Title desc" : "Title asc";
            ViewBag.SortCategory = String.IsNullOrEmpty(sortBy) ? "Category desc" : sortBy.Equals("Category asc") ? "Category desc" : "Category asc";
            ViewBag.SortStatus = String.IsNullOrEmpty(sortBy) ? "Status desc" : sortBy.Equals("Status asc") ? "Status desc" : "Status asc";
            ViewBag.SortDN = String.IsNullOrEmpty(sortBy) ? "DN desc" : sortBy.Equals("DN asc") ? "DN desc" : "DN asc";
            ViewBag.SortEarnings = String.IsNullOrEmpty(sortBy) ? "Earnings desc" : sortBy.Equals("Earnings asc") ? "Earnings desc" : "Earnings asc";
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            ViewBag.Viewpage = 5;
            MemberData md = db.GetMemberData(MemberId, i ,sortBy);
            if(md.member==null || md.mp == null)
            {
                return HttpNotFound("Your request did not find."); ;
            }
            else
            {
                return View(md);
            }
        }

        [HttpGet]
        public ActionResult Notedetails(int noteId)
        {
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            Note notes = db.getnoteData(noteId);
            if (notes == null)
            {
                return HttpNotFound("Your request did not find."); ;
            }
            else
            {
                ViewBag.Photo = ms.NoteDisplayPicture;
                ViewBag.ProfilePhoto = ms.MemberDisplayPicture;
                return View(notes);
            }
        }

        [HttpGet]
        public ActionResult RemoveReview(int ReviewId)
        {
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            bool check = db.removereview(ReviewId,id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpGet]
        public ActionResult RemoveMember(int MemberId)
        {
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            bool check = db.removemember(MemberId, id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpGet]
        public ActionResult Rejected(int? i, string name, string search, string sortBy)
        {
            ViewBag.SortTitle = String.IsNullOrEmpty(sortBy) ? "Title desc" : sortBy.Equals("Title asc") ? "Title desc" : "Title asc";
            ViewBag.SortCategory = String.IsNullOrEmpty(sortBy) ? "Category desc" : sortBy.Equals("Category asc") ? "Category desc" : "Category asc";
            ViewBag.SortSeller = String.IsNullOrEmpty(sortBy) ? "Seller desc" : sortBy.Equals("Seller asc") ? "Seller desc" : "Seller asc";
            ViewBag.SortRejected = String.IsNullOrEmpty(sortBy) ? "Rejected desc" : sortBy.Equals("Rejected asc") ? "Rejected desc" : "Rejected asc";
            ViewBag.SortRemark = String.IsNullOrEmpty(sortBy) ? "Remark desc" : sortBy.Equals("Remark asc") ? "Remark desc" : "Remark asc";
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            ViewBag.Viewpage = 5;
            ViewData["Name"] = db.getRejectedName();
            List<Note> notes = db.getRejectedNote(sortBy, search, name);
            return View(notes.ToList().ToPagedList(i ?? 1,5));
        }

        [HttpGet]
        public ActionResult ApprovedNote(int NoteId)
        {
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            bool check = db.approvednote(NoteId, id);
            return RedirectToAction("Rejected","Admin");
        }

        [HttpGet]
        public ActionResult Download(int? i, string sellername, string notename, string buyername, string search, string sortBy)
        {
            ViewBag.SortTitle = String.IsNullOrEmpty(sortBy) ? "Title desc" : sortBy.Equals("Title asc") ? "Title desc" : "Title asc";
            ViewBag.SortCategory = String.IsNullOrEmpty(sortBy) ? "Category desc" : sortBy.Equals("Category asc") ? "Category desc" : "Category asc";
            ViewBag.SortSeller = String.IsNullOrEmpty(sortBy) ? "Seller desc" : sortBy.Equals("Seller asc") ? "Seller desc" : "Seller asc";
            ViewBag.SortBuyer = String.IsNullOrEmpty(sortBy) ? "Buyer desc" : sortBy.Equals("Buyer asc") ? "Buyer desc" : "Buyer asc";
            ViewBag.SortSellType = String.IsNullOrEmpty(sortBy) ? "SellType desc" : sortBy.Equals("SellType asc") ? "SellType desc" : "SellType asc";
            ViewBag.SortPrice = String.IsNullOrEmpty(sortBy) ? "Price desc" : sortBy.Equals("Price asc") ? "Price desc" : "Price asc";
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            ViewBag.Viewpage = 5;
            ViewData["NoteName"] = db.getNoteTitle();
            ViewData["SellerName"] = db.getSellerName();
            ViewData["BuyerName"] = db.getBuyerName();
            List<Buyer> buyers = db.getdownload(sortBy, search, sellername, notename, buyername);
            return View(buyers.ToList().ToPagedList(i ?? 1, 5));
        }

        [HttpGet]
        public ActionResult Published(int? i, string name, string search, string sortBy)
        {
            ViewBag.SortTitle = String.IsNullOrEmpty(sortBy) ? "Title desc" : sortBy.Equals("Title asc") ? "Title desc" : "Title asc";
            ViewBag.SortCategory = String.IsNullOrEmpty(sortBy) ? "Category desc" : sortBy.Equals("Category asc") ? "Category desc" : "Category asc";
            ViewBag.SortSeller = String.IsNullOrEmpty(sortBy) ? "Seller desc" : sortBy.Equals("Seller asc") ? "Seller desc" : "Seller asc";
            ViewBag.SortSellType = String.IsNullOrEmpty(sortBy) ? "SellType desc" : sortBy.Equals("SellType asc") ? "SellType desc" : "SellType asc";
            ViewBag.SortPrice = String.IsNullOrEmpty(sortBy) ? "Price desc" : sortBy.Equals("Price asc") ? "Price desc" : "Price asc";
            ViewBag.SortApprove = String.IsNullOrEmpty(sortBy) ? "Approve desc" : sortBy.Equals("Approve asc") ? "Approve desc" : "Approve asc";
            ViewBag.SortDownload = String.IsNullOrEmpty(sortBy) ? "Download desc" : sortBy.Equals("Download asc") ? "Download desc" : "Download asc";


            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            ViewBag.Viewpage = 5;
            ViewData["Name"] = db.getNotePublished();
            List<Note> notes = db.getPublished(sortBy, search, name);
            return View(notes.ToList().ToPagedList(i ?? 1, 5));
        }

        [HttpGet]
        public ActionResult MyProfile()
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

            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            ViewData["Country"] = db.CountryData();
            Admin admin = db.getprofile(id);
            return View(admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyProfile(Admin admin)
        {
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            bool check = db.updateprofile(admin,id);
            if (!check)
            {
                TempData["ErrorMessage"] = "Profile Photo less than 10MB or Only image are accepted";
                return RedirectToAction("MyProfile", "Admin");
            }
            else
            {
                TempData["SucessMessage"] = "Your Profile has been updated";
                return RedirectToAction("MyProfile", "Admin");
            }
            
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            ViewBag.ErrorStatus = false;
            Admin admin = new Admin();
            return View(admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(Admin admin)
        {
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Auth", new { url = path });
            }

            String result = db.ChangePassword(admin, id);
            if (result == "ConfirmException")
            {
                ViewBag.ErrorStatus = true;
                ViewBag.ErrorMessage = "New Password Not Match to Confirm Password";
                return View();
            }
            else if (result == "OldPasswordException")
            {
                ViewBag.ErrorStatus = true;
                ViewBag.ErrorMessage = "Old Password Not Match";
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Admin");
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public ActionResult UnderReview(int? i, string name, string search, string sortBy)
        {
            ViewBag.SortTitle = String.IsNullOrEmpty(sortBy) ? "Title desc" : sortBy.Equals("Title asc") ? "Title desc" : "Title asc";
            ViewBag.SortCategory = String.IsNullOrEmpty(sortBy) ? "Category desc" : sortBy.Equals("Category asc") ? "Category desc" : "Category asc";
            ViewBag.SortSeller = String.IsNullOrEmpty(sortBy) ? "Seller desc" : sortBy.Equals("Seller asc") ? "Seller desc" : "Seller asc";
            ViewBag.SortStatus = String.IsNullOrEmpty(sortBy) ? "Status desc" : sortBy.Equals("Status asc") ? "Status desc" : "Status asc";

            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            ViewBag.Viewpage = 5;
            ViewData["Name"] = db.getNoteUnderReview();
            List<Note> notes = db.getUnderReview(sortBy, search, name);
            return View(notes.ToList().ToPagedList(i ?? 1, 5));
        }

        public ActionResult ChangeToReview(int noteId)
        {
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            bool check = db.changetoReview(noteId,id);
            return RedirectToAction("UnderReview", "Admin");
        }

        public ActionResult ApproveNote(int noteId)
        {
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            bool check = db.ApproveNote(noteId,id);
            return RedirectToAction("UnderReview", "Admin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RejectNote(int Id,string remarks)
        {
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            bool check = db.notereject(remarks, Id, id);
            return RedirectToAction("UnderReview", "Admin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UnPublish(int Id, string remarks)
        {
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            bool check = db.unPublish(Id, id,remarks);
            return RedirectToAction("UnderReview", "Admin");
        }

        [HttpGet]
        public ActionResult DashBoard(int? i, string month, string search, string sortBy)
        {
            ViewBag.SortTitle = String.IsNullOrEmpty(sortBy) ? "Title desc" : sortBy.Equals("Title asc") ? "Title desc" : "Title asc";
            ViewBag.SortCategory = String.IsNullOrEmpty(sortBy) ? "Category desc" : sortBy.Equals("Category asc") ? "Category desc" : "Category asc";
            ViewBag.SortSeller = String.IsNullOrEmpty(sortBy) ? "Seller desc" : sortBy.Equals("Seller asc") ? "Seller desc" : "Seller asc";
            ViewBag.SortSellType = String.IsNullOrEmpty(sortBy) ? "SellType desc" : sortBy.Equals("SellType asc") ? "SellType desc" : "SellType asc";
            ViewBag.SortPrice = String.IsNullOrEmpty(sortBy) ? "Price desc" : sortBy.Equals("Price asc") ? "Price desc" : "Price asc";
            ViewBag.SortFile = String.IsNullOrEmpty(sortBy) ? "Filesize desc" : sortBy.Equals("Filesize asc") ? "Filesize desc" : "Filesize asc";
            ViewBag.SortDownload = String.IsNullOrEmpty(sortBy) ? "Download desc" : sortBy.Equals("Download asc") ? "Download desc" : "Download asc";

            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            ViewBag.Viewpage = 5;
            ViewBag.ReviewNote = db.getReviewnote();
            ViewBag.DownloadNote = db.getDownloadNotes();
            ViewBag.AddMember = db.getaddMember();
            List<Note> notes = db.DashboardPublished(sortBy, search, month);
            return View(notes.ToList().ToPagedList(i ?? 1, 5));
        }

        [HttpGet]
        public ActionResult ManageAdmin(int? i, string search, string sortBy)
        {
            ViewBag.SortFName = String.IsNullOrEmpty(sortBy) ? "FName desc" : sortBy.Equals("FName asc") ? "FName desc" : "FName asc";
            ViewBag.SortLName = String.IsNullOrEmpty(sortBy) ? "LName desc" : sortBy.Equals("LName asc") ? "LName desc" : "LName asc";
            ViewBag.SortEmail = String.IsNullOrEmpty(sortBy) ? "Email desc" : sortBy.Equals("Email asc") ? "Email desc" : "Email asc";
            ViewBag.SortPhone = String.IsNullOrEmpty(sortBy) ? "Phone desc" : sortBy.Equals("Phone asc") ? "Phone desc" : "Phone asc";
            ViewBag.SortActive = String.IsNullOrEmpty(sortBy) ? "Active desc" : sortBy.Equals("Active asc") ? "Active desc" : "Active asc";
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            else if (Session["Type"].ToString() != "SuperAdmin")
            {
                return RedirectToAction("DashBoard", "Admin");
            }
            ViewBag.Viewpage = 5;
            List<Admin> admin = db.TypeAdminData(sortBy, search);
            return View(admin.ToList().ToPagedList(i ?? 1, 5));
        }

        [HttpGet]
        public ActionResult Administrator(int? adminId)
        {
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            else if (Session["Type"].ToString() != "SuperAdmin")
            {
                return RedirectToAction("DashBoard", "Admin");
            }
            ViewData["Country"] = db.CountryData();
            Admin admin=new Admin();

            if (String.IsNullOrEmpty(adminId.ToString()))
            {
                admin = new Admin();
            }
            else
            {
                admin = db.getadmin(adminId.Value);
            }
            return View(admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Administrator(string adminId, Admin admin)
        {
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            else if (Session["Type"].ToString() != "SuperAdmin")
            {
                return RedirectToAction("DashBoard", "Admin");
            }
            bool check;
            if (String.IsNullOrEmpty(adminId))
            {
                check = db.addadmin(admin, id);
            }
            else
            {
                check = db.updateadmin(Convert.ToInt32(adminId), admin, id);
            }
            return RedirectToAction("ManageAdmin", "Admin");
        }

        [HttpGet]
        public ActionResult DeleteAdmin(int adminId)
        {
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            else if (Session["Type"].ToString() != "SuperAdmin")
            {
                return RedirectToAction("DashBoard", "Admin");
            }
            bool check = db.deleteadmin(adminId, id);
            return RedirectToAction("ManageAdmin", "Admin");
        }

        [HttpGet]
        public ActionResult ManageConfig()
        {
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            else if (Session["Type"].ToString() != "SuperAdmin")
            {
                return RedirectToAction("DashBoard", "Admin");
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
                ViewBag.SucessMessage = TempData["SucessMessage"];
            }
            else
            {
                ViewBag.ErrorStatus = false;
                ViewBag.SucessStatus = false;
            }
            ManageSystem ms = db.getconfig();
            return View(ms);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageConfig(ManageSystem ms)
        {
            int id = db.GetId();
            if (id == 0)
            {
                string path = Request.Url.PathAndQuery;
                return RedirectToAction("Index", "Admin", new { url = path });
            }
            else if (Session["Type"].ToString() != "SuperAdmin")
            {
                return RedirectToAction("DashBoard", "Admin");
            }

            bool check = db.updateconfig(ms, id);
            if (!check)
            {
                TempData["ErrorMessage"] = "Profile Photo less than 10MB or Only image are accepted";
                return RedirectToAction("ManageConfig", "Admin");
            }
            else
            {
                TempData["SucessMessage"] = "Your Profile has been updated";
                return RedirectToAction("ManageConfig", "Admin");
            }

        }
    }
}