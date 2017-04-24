using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameReview.DAL;
using GameReview.Models;
namespace GameReview.Controllers
{
    public class SearchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult Index()
        {

            if (Request.QueryString["q"] == null)
            {
                RedirectToAction("Index", "Home");
            }
            var games = API.Search(Request.QueryString["q"]);

            return View(games);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}