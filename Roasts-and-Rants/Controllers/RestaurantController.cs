using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Roasts_and_Rants.DAL;
using Roasts_and_Rants.Models;
using Roasts_and_Rants.Filters;
using System.Collections.Generic;

namespace Roasts_and_Rants.Controllers {
	public class RestaurantController : Controller {
		private RestaurantReviewContext db = new RestaurantReviewContext();

		// GET: Restaurant
		public ActionResult Index(string sortOrder) {

			// Calculate the average rating for restaurant
			foreach (Restaurant rest in db.Restaurants) {
				rest.AverageRating = rest.Reviews.Average(r => r.Rating);
			}

			ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			ViewBag.RatingSortParam = sortOrder == "Rating" ? "rating_desc" : "Rating";
			var restaurants = from r in db.Restaurants
							  select r;

			IEnumerable<Restaurant> orderedRestaurant;
			switch (sortOrder) {
				case "name_desc":
					orderedRestaurant = restaurants.OrderByDescending(r => r.Name);
					break;
				case "rating_desc":
					orderedRestaurant = restaurants.ToList().OrderByDescending(r => r.AverageRating);
					break;
				case "Rating":
					orderedRestaurant = restaurants.ToList().OrderBy(r => r.AverageRating);
					break;
				default:
					orderedRestaurant = restaurants.OrderBy(r => r.Name);
					break;
			}

			return View(orderedRestaurant.ToList());
		}

		// Sends the RestaurantId to the ReviewController
		// Get Restaurant/Reviews
		[Log]
		public ActionResult Reviews(int? id) {

			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			return RedirectToAction("Index", "Review", new { id = id });
		}

		// Currently not in use
		// GET: Restaurant/Create
		public ActionResult Create() {
			ViewBag.Message = "Create new restaurant";
			return View();
		}

		// Currently not in use
		// POST: Restaurant/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "RestaurantID,Name")] Restaurant restaurant) {
			if (ModelState.IsValid) {
				db.Restaurants.Add(restaurant);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(restaurant);
		}

		// Currently not in use
		// GET: Restaurant/Edit/5
		public ActionResult Edit(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Restaurant restaurant = db.Restaurants.Find(id);
			if (restaurant == null) {
				return HttpNotFound();
			}
			return View(restaurant);
		}

		// Currently not in use
		// POST: Restaurant/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "RestaurantID,Name")] Restaurant restaurant) {
			if (ModelState.IsValid) {
				db.Entry(restaurant).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(restaurant);
		}

		// Currently not in use
		// GET: Restaurant/Delete/5
		public ActionResult Delete(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Restaurant restaurant = db.Restaurants.Find(id);
			if (restaurant == null) {
				return HttpNotFound();
			}
			return View(restaurant);
		}

		// Currently not in use
		// POST: Restaurant/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id) {
			Restaurant restaurant = db.Restaurants.Find(id);
			db.Restaurants.Remove(restaurant);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing) {
			if (disposing) {
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
