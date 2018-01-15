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
		public ActionResult Index() {
			return View(db.Restaurants.ToList());
		}

		// GET: Restaurant/Details/5
		public ActionResult Details(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			return RedirectToAction("Index", "Review", new { id = id });
		}

		// GET: Restaurant/Create
		public ActionResult Create() {
			return View();
		}

		// POST: Restaurant/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
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
		//public ActionResult Edit([Bind(Include = "RestaurantID,Name,Phone,Street,City,State,Country,PostalCode")] Restaurant restaurant) {
		public ActionResult EditPost(int? id) { 

			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var restaurantToUpdate = db.Restaurants.Find(id);

			if (TryUpdateModel(restaurantToUpdate, "", new string[] { "Name", "Phone", "Street", "City", "State", "Country", "PostalCode" })) {
				try {
					db.SaveChanges();
				} catch (DataException) {
					ModelState.AddModelError("", "Unable to save changes.");
				}
			}
			
			return View(restaurantToUpdate);
		}

		// GET: Restaurant/Delete/5
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
		public ActionResult Delete(int id) {
			try {
				Restaurant restaurant = db.Restaurants.Find(id);
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
