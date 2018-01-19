using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Roasts_and_Rants.DAL;
using Roasts_and_Rants.Models;

namespace Roasts_and_Rants.Controllers {

	public class RestaurantController : Controller {

		private RestaurantReviewContext db = new RestaurantReviewContext();

		// GET: Restaurant
		public ActionResult Index(string sortOrder, string searchString) {

			// Calculate the average of the ratings of each restaurant
			foreach (Restaurant rest in db.Restaurants) {
				if (rest.Reviews.Count > 0) {
					rest.AverageRating = CalculateAverageRating(rest);
				}
			}

			// Decide sort order
			ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			ViewBag.RatingSortParam = sortOrder == "rating" ? "rating_desc" : "rating";
			var restaurants = from r in db.Restaurants
							  select r;

			// Check to see if the Name, Phone, or Address fields contains the search string
			if (!String.IsNullOrEmpty(searchString)) {
				restaurants = restaurants.Where(r => r.Name.Contains(searchString)
					|| r.Phone.Contains(searchString) || r.Street.Contains(searchString)
					|| r.State.Contains(searchString)  || r.City.Contains(searchString)
					|| r.Country.Contains(searchString) || r.PostalCode.Contains(searchString));
			}

			// Decide how the list should be ordered
			IEnumerable<Restaurant> orderedRestaurants;
			switch (sortOrder) {
				case "name_desc":
					orderedRestaurants = restaurants.OrderByDescending(rest => rest.Name);
					break;
				case "rating":
					orderedRestaurants = restaurants.ToList().OrderBy(rest => rest.AverageRating);
					break;
				case "rating_desc":
					orderedRestaurants = restaurants.ToList().OrderByDescending(rest => rest.AverageRating);
					break;
				default:
					orderedRestaurants = restaurants.OrderBy(rest => rest.Name);
					break;
			}

			return View(orderedRestaurants.ToList());
		}

		// Helper method to calculate the Average rating of a restaurant
		private decimal CalculateAverageRating(Restaurant restaurant) {
			return restaurant.Reviews.Average(r => r.Rating);
		}

		// Not in use
		// Redirect to Reviews
		public ActionResult Details(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			return View();
			//return RedirectToAction("Reviews", new { id = id });
			//return RedirectToAction("Index", "Review", new { restaurantId = id });
		}

		// GET Restaurant/Reviews/5
		// Displays the list of reviews associated with the selected restaurant
		public ActionResult Reviews(int? id, string sortOrder) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			ViewBag.RatingSortParam = String.IsNullOrEmpty(sortOrder) ? "rating" : "rating_desc";
			Restaurant restaurant = db.Restaurants.SingleOrDefault(r => r.RestaurantID == id);

			if (restaurant == null) {
				return HttpNotFound();
			}

			switch (sortOrder) {
				case "rating":
					restaurant.Reviews = restaurant.Reviews.OrderBy(r => r.Rating).ToList();
					break;
				case "rating_desc":
					restaurant.Reviews = restaurant.Reviews.OrderByDescending(r => r.Rating).ToList();
					break;
				default:
					restaurant.Reviews = restaurant.Reviews.OrderBy(r => r.ModifiedDate).ToList();
					break;
			}

			return View(restaurant);
		}

		// GET: Restaurant/Create
		[Authorize(Roles = "admin")]
		public ActionResult Create() {
			return View();
		}

		// POST: Restaurant/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
		public ActionResult Create([Bind(Include = "Name,Phone,Street,City,State,Country,PostalCode")] Restaurant restaurant) {
			try {
				if (ModelState.IsValid) {
					db.Restaurants.Add(restaurant);
					db.SaveChanges();
					return RedirectToAction("Index");
				}
			} catch (DataException) {
				ModelState.AddModelError("", "Unable to save changes.");
			}

			return View(restaurant);
		}

		// GET: Restaurant/Edit/5
		[Authorize(Roles = "admin")]
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

		// POST: Restaurant/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
		public ActionResult Edit([Bind(Include = "RestaurantID,Name,Phone,Street,City,State,Country,PostalCode")] Restaurant restaurant) {

			
			if (ModelState.IsValid) {
				db.Entry(restaurant).State = EntityState.Modified;
				db.SaveChanges();
				return View("Index");
			}
				
			return View(restaurant);
		}

		// GET: Restaurant/Delete/5
		[Authorize(Roles = "admin")]
		public ActionResult Delete(int? id, bool? saveChangesError=false) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			if (saveChangesError.GetValueOrDefault()) {
				ViewBag.ErrorMessage = "Delete failed.";
			}
			Restaurant restaurant = db.Restaurants.Find(id);
			if (restaurant == null) {
				return HttpNotFound();
			}
			return View(restaurant);
		}

		// POST: Restaurant/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
		public ActionResult Delete(int id) {
			try {
				Restaurant restaurant = db.Restaurants.Find(id);
				db.Reviews.RemoveRange(restaurant.Reviews);
				db.Restaurants.Remove(restaurant);
				db.SaveChanges();
			} catch (DataException) {
				return RedirectToAction("Delete", new { id = id, saveChangesError = false });
			}
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
