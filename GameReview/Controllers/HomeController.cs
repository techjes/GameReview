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
            ApplicationUser user;

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                user = db.Users.First(x => x.UserName == User.Identity.Name);
                ViewBag.FavoritesCount = user.Favorites.Count();
                db.Entry(user).Collection(x => x.Reviews).Load();
                db.Entry(user).Collection(x => x.Favorites).Load();
            }
            UserViewModel model = new UserViewModel(user);
            if (user.Reviews.Count() > 0)
            {
                ViewBag.Reviews = user.Reviews.OrderBy(x => x.DateCreated).ToList()[0];

            }
            if (user.Favorites.Count >0)
            {
                ViewBag.Favorites = user.Favorites.OrderBy(x => x.GameTitle);

            }

            return View(model);
        }

        public ActionResult ReviewsByUser()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");
            ApplicationUser user;

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                user = db.Users.First(x => x.UserName == User.Identity.Name);
                ViewBag.FavoritesCount = user.Favorites.Count();
                db.Entry(user).Collection(x => x.Reviews).Load();
            }


            return View(user);
        }
    }
}