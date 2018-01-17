using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Roasts_and_Rants.DAL;
using Roasts_and_Rants.Models;

namespace Roasts_and_Rants.Controllers {

	public class ReviewController : Controller {

		private RestaurantReviewContext db = new RestaurantReviewContext();
		private ApplicationDbContext context = new ApplicationDbContext();

		// Receives id from RestaurantController
		// GET: Review/id
		public ActionResult Index(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Restaurant restaurant = db.Restaurants.Find(id);

			if (restaurant == null) {
				return HttpNotFound();
			}

			return View(restaurant);
		}

		// Currently, not in use
		// GET: Review/Details/5
		public ActionResult Details(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Review review = db.Reviews.Find(id);
			if (review == null) {
				return HttpNotFound();
			}
			return View(review);
		}

		// GET: Review/Create
		[Authorize]
		public ActionResult Create(int restaurantID, string username) {
			ViewBag.CurrentRestaurant = restaurantID;
			ViewBag.CurrentUser = username;
			return View();
		}

		// POST: Review/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Rating,Content")] Review review, int RestaurantID) {
			review.UserID = User.Identity.GetUserId();
			review.UserName = User.Identity.GetUserName();
			review.ModifiedDate = DateTime.Now;
			review.RestaurantID = RestaurantID;

			if (ModelState.IsValid) {
				db.Reviews.Add(review);
				db.SaveChanges();
				return RedirectToAction("Index", new { id = RestaurantID });
			}

			//ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "Name", review.RestaurantID);
			//ViewBag.UserEmail = new SelectList(db.Users, "Email", "Username", review.UserEmail);
			return View(review);
		}

		// GET: Review/Edit/5
		public ActionResult Edit(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			if (User.Identity.GetUserId() == null) {
				return RedirectToAction("Login", "Account");
			}

			Review review = db.Reviews.Find(id);
			if (review == null) {
				return HttpNotFound();
			}
			
			return View(review);
		}

		// POST: Review/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "ReviewID,Rating,Content,RestaurantID,UserName")] Review review) {

			review.ModifiedDate = DateTime.Now;
			if (ModelState.IsValid) {
				db.Entry(review).State = EntityState.Modified;

				db.SaveChanges();
				return RedirectToAction("Index", new { id = review.RestaurantID });
			}
			return View("Index", new { id = review.RestaurantID });
		}

		// GET: Review/Delete/5
		public ActionResult Delete(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Review review = db.Reviews.Find(id);
			if (review == null) {
				return HttpNotFound();
			}
			return View(review);
		}

		// POST: Review/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id) {
			Review review = db.Reviews.Find(id);
			db.Reviews.Remove(review);
			db.SaveChanges();
			return RedirectToAction("Index", new { review.RestaurantID });
		}

		protected override void Dispose(bool disposing) {
			if (disposing) {
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
