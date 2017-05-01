using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GameReview.Models;
using GameReview.DAL;

namespace GameReview.Controllers
{
    public class GamesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Games
        public ActionResult Index()
        {
            return View(db.Games.ToList());
        }

        // GET: Games/Details/5
        public ActionResult Details(int id)
        {
            Game game = db.Games.FirstOrDefault(x => x.ApiID == id);

            if (game == null)
            {
                game = API.GetDetails(id);
                db.Games.Add(game);
                db.SaveChanges();
            }         
            db.Entry(game).Collection(x => x.ArtCollection).Load();
            db.Entry(game).Collection(x => x.Reviews).Load();
            

            return View(game);
        }

        public ActionResult DetailSubView(int id, string viewLink)
        {
            var game = db.Games.Find(id);

            switch (viewLink)
            {
                case "detailView":
                    return PartialView("detailView", game);
                case "trailerView":
                    return PartialView("trailerView", game);
                case "mediaView":
                    return PartialView("mediaView", game);
                case "reviewsView":
                    return RedirectToAction("IndexByGame", "Reviews", new { id = id });
                default:
                    return PartialView("detailView", game);
            }
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,GameTitle,Platform,ReleaseDate,Overview,ESRB,Players,CoOp,Youtube,Publisher,Rating")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(game);
        }

        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,GameTitle,Platform,ReleaseDate,Overview,ESRB,Players,CoOp,Youtube,Publisher,Rating")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(game);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FavoriteButton(int id)
        {
            var game = db.Games.Find(id);
            var user = db.Users.First(x => x.UserName == User.Identity.Name);
            db.Entry(user).Collection(x => x.Favorites).Load();

            bool inFavorites = user.Favorites.Contains(game);
            ViewBag.InFavorites = inFavorites;
            ViewBag.GameId = id;
            return PartialView();
        }

        [HttpPost]
        public ActionResult ManageFavorite(int id)
        {
            var game = db.Games.Find(id);
            var user = db.Users.First(x => x.UserName == User.Identity.Name);
            db.Entry(user).Collection(x => x.Favorites).Load();

            bool inFavorites;

            if (user.Favorites.Contains(game))           
                user.Favorites.Remove(game);          
            else
                user.Favorites.Add(game);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("FavoriteButton", new { id = id });

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
