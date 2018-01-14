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
using Roasts_and_Rants.Binders;
using System.Collections.Generic;

namespace Roasts_and_Rants.Controllers {
	public class RestaurantController : Controller {
		private RestaurantReviewContext db = new RestaurantReviewContext();

		// GET: Restaurant
		public ActionResult Index(string sortOrder, string searchString) {

			ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			ViewBag.RatingSortParam = sortOrder == "Rating" ? "rating_desc" : "Rating";
			var restaurants = from r in db.Restaurants
							  select r;

			// Calculate the average rating for restaurant
			foreach (Restaurant rest in db.Restaurants) {
				if (rest.Reviews.Count > 0) {
					rest.AverageRating = rest.Reviews.Average(r => r.Rating);
				}
			}

			if (!String.IsNullOrEmpty(searchString)) {
				restaurants = restaurants.Where(r => r.Name.Contains(searchString)
				|| r.Phone.Contains(searchString)
				|| r.Address.Street.Contains(searchString)
				|| r.Address.City.Contains(searchString)
				|| r.Address.State.Contains(searchString)
				|| r.Address.Country.Contains(searchString)
				|| r.Address.PostalCode.Contains(searchString));
			}
			

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

		// GET: Restaurant/Create
		public ActionResult Create() {
			ViewBag.Message = "Create new restaurant";
			return View();
		}

		// POST: Restaurant/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		//public ActionResult Create([ModelBinder(typeof(RestaurantCustomDataBinder))] Restaurant restaurant) {
		public ActionResult Create([Bind(Include = "Name,Address,Phone")]Restaurant restaurant) { 
			try {
				if (ModelState.IsValid) {
					db.Restaurants.Add(restaurant);
					db.SaveChanges();
					return RedirectToAction("Index");
				}
			} catch (DataException dex) {
				ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
			}
			

			return View(restaurant);
		}

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

		// POST: Restaurant/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost, ActionName("Edit")]
		[ValidateAntiForgeryToken]
		//public ActionResult EditPost(int? id) {
		public ActionResult EditPost([Bind(Include = "Name,Address,Phone")]Restaurant restaurant) {

			if (ModelState.IsValid) {
				if (TryUpdateModel(restaurant, "", new string[] { "Name", "Address", "Phone" })) {
					db.SaveChanges();
					return RedirectToAction("Index");
				}
			}
			

			//if (id == null) {
			//	return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			//}

			//var restaurantToUpdate = db.Restaurants.Find(id);
			//var AddressToUpdate = db.Addresses.Find(id);
			//if (TryUpdateModel(AddressToUpdate, "", new string[] { "Street", "City", "State", "PostalCode", "Country" })) {
			//	try {
			//		db.SaveChanges();
			//		if (TryUpdateModel(restaurantToUpdate, "", new string[] { "Name", "Address", "Phone" })) {
			//			try {
			//				db.SaveChanges();

			//				return RedirectToAction("Index");
			//			} catch (DataException) {
			//				ModelState.AddModelError("", "Error updating.");
			//			}
			//		}
			//	} catch (DataException) {
			//		ModelState.AddModelError("", "Error updating.");
			//	}
			//}

			//if (ModelState.IsValid) {
			//	if (TryUpdateModel(restaurant)) {
			//		db.Restaurants.Attach(restaurant);
			//		db.Entry(restaurant).State = EntityState.Modified;
			//		db.SaveChanges();
			//		return RedirectToAction("Index");
			//	} else {
			//		ModelState.AddModelError("", "Error Updating.");
			//	}
			//}
			return View(restaurant);
		}

		// GET: Restaurant/Delete/5
		public ActionResult Delete(int? id, bool? saveChangesError=false) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			if (saveChangesError.GetValueOrDefault()) {
				ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
			}
			Restaurant restaurant = db.Restaurants.Find(id);
			if (restaurant == null) {
				return HttpNotFound();
			}
			return View(restaurant);
		}

		// POST: Restaurant/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id) {
			try {
				Restaurant restaurant = db.Restaurants.Find(id);
				Address restaurantAddress = db.Addresses.Find(id);
				db.Addresses.Remove(restaurantAddress);
				db.Restaurants.Remove(restaurant);
				db.SaveChanges();
			} catch (DataException dex) {
				return RedirectToAction("Delete", new { id = id, saveChangesError = true });
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
