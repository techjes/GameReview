using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using GameReview.Models;
using PagedList;
using System.Net;
using GameReview.CustomAttributes;

namespace GameReview.Controllers
{
    public class ModeratorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AuthorizeOrRedirectAttribute(Roles = "Administrator,Moderator")]
        public ActionResult Index(string sortOrder, string currentFilter,string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.UserNameSort = String.IsNullOrEmpty(sortOrder) ? "userNameDesc" : "";
            ViewBag.DateSort = sortOrder == "date" ? "dateDesc" : "date";
            ViewBag.HelpSort = sortOrder == "help" ? "helpDesc" : "help";
            ViewBag.NotHelpSort = sortOrder == "notHelp" ? "notHelpDesc" : "notHelp";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            List<Review> Reviews;

            switch (sortOrder)
            {
                case "userNameDesc":
                    Reviews = db.Reviews.OrderByDescending(x => x.UserName).ToList();
                    break;
                case "date":
                    Reviews = db.Reviews.OrderBy(x => x.DateCreated).ToList();
                    break;
                case "dateDesc":
                    Reviews = db.Reviews.OrderByDescending(x => x.DateCreated).ToList();
                    break;
                case "help":
                    Reviews = db.Reviews.OrderBy(x => x.HelpfulCount).ToList();
                    break;
                case "helpDesc":
                    Reviews = db.Reviews.OrderByDescending(x => x.HelpfulCount).ToList();
                    break;
                case "notHelp":
                    Reviews = db.Reviews.OrderBy(x => x.NotHelpfulCount).ToList();
                    break;
                case "notHelpDesc":
                    Reviews = db.Reviews.OrderByDescending(x => x.NotHelpfulCount).ToList();
                    break;
                default:
                    Reviews = db.Reviews.OrderBy(x => x.UserName).ToList();
                    break;
            }

            int pageSize = 25;
            int pageNumber = page ?? 1;
            return View(Reviews.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        [AuthorizeOrRedirectAttribute(Roles = "Administrator,Moderator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var review = db.Reviews.Find(id);

            if (review == null)
                return HttpNotFound();

            return View(review);
        }
        [AuthorizeOrRedirectAttribute(Roles = "Administrator,Moderator")]
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}