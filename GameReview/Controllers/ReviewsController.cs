using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GameReview.Models;
using Microsoft.AspNet.Identity;

namespace GameReview.Controllers
{
    public class ReviewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reviews
        public ActionResult Index()
        {
            return View(db.Reviews.ToList());
        }

        public ActionResult IndexByGame(int id)
        {
            ViewBag.GameID = id;
            var game = db.Games.Find(id);
            db.Entry(game).Collection(x => x.Reviews).Load();
            var userReviews = db.Users.Find(User.Identity.GetUserId()).Reviews;
            var gameReviews = game.Reviews;

            Review userReview = gameReviews.Intersect(userReviews).FirstOrDefault();

            ViewBag.UserReviewID = null;

            if (userReview != null)
            {
                gameReviews.Remove(userReview);
                ViewBag.UserReviewID = userReview.ID;
            }
                
            return PartialView(gameReviews);
        }

        public ActionResult UserReviewPartial(int? id, int gameID)
        {
            ViewBag.GameID = gameID;
            Review review = (id != null) ? db.Reviews.Find(id) : null;                       

            return PartialView(review);
        }

        public ActionResult IndexByUser(int id)
        {
            var user = db.Users.Find(id);
            db.Entry(user).Collection(x => x.Reviews).Load();

            return PartialView(user.Reviews);
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int GameID, string Content, bool Recommended)
        {
            Review userReview = new Review()
            {
                Content = Content,
                DateCreated = DateTime.Now,
                Recommended = Recommended,
                UserName = User.Identity.Name
            };
            var user = db.Users.Find(User.Identity.GetUserId());
            var game = db.Games.Find(GameID);
            user.Reviews.Add(userReview);
            game.Reviews.Add(userReview);

            db.Reviews.Add(userReview);                      
            db.Entry(user).State = EntityState.Modified;
            db.Entry(game).State = EntityState.Modified;
            db.SaveChanges();

            return PartialView("UserReviewPartial", userReview);
            
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Content,DateCreated,Recommended,HelpfulCount,NotHelpfulCount,UserName")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> ViewSinglePartial(int id)
        {
            return PartialView(await db.Reviews.FindAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult> EditPartial(int id)
        {
            var review = await db.Reviews.FindAsync(id);

            return PartialView(review);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> EditPartial([Bind(Include ="ID,Content,DateCreated,Recommended,HelpfulCount,NotHelpfulCount,UserName")]Review review)
        {
            db.Entry(review).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return RedirectToAction("ViewSinglePartial", new { id = review.ID});
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
