using GameReview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameReview.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");
            UserViewModel model;
            ApplicationUser user;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                user = db.Users.First(x => x.UserName == User.Identity.Name);
                ViewBag.FavoritesCount = user.Favorites.Count();
                model = new UserViewModel(user);
            }
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}