using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GameReview.Models;

namespace GameReview.Controllers
{
    public class UserViewModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserViewModels/Details/5
        //public ActionResult Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    UserViewModel userViewModel = db.UserViewModels.Find(id);
        //    if (userViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(userViewModel);
        //}

        // GET: UserViewModels/Edit/5
        public ActionResult Edit(string userName = null)
        {
            if (userName == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var db = new ApplicationDbContext();
            var user = db.Users.First(u => u.UserName == userName);
            var model = new UserViewModel(user);

            if (user == null)
                return HttpNotFound();
            return View(model);
        }

        // POST: UserViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserName,Email,FirstName,LastName,AccountCreationDate")] UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userViewModel);
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
