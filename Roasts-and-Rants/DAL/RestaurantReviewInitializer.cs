using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Roasts_and_Rants.Models;

namespace Roasts_and_Rants.DAL {

	/// <summary>
	/// Initialize the database with test values
	/// </summary>
	public class RestaurantReviewInitializer : DropCreateDatabaseIfModelChanges<RestaurantReviewContext> {

		protected override void Seed(RestaurantReviewContext context) {
			var users = new List<User>() {
				new User() { Email = "Chase@example.com", Username = "ChaseVisa", Password = "123456" },
				new User() { Email = "John@hotmail.com", Username = "John with a G", Password = "Adoboseasoning" },
				new User() { Email = "James@nugget.com", Username = "MenG", Password = "Runwithgold" },
				new User() { Email = "PlasmaxTravis@gmail.com", Username = "Plasmax167", Password = "Injustice2" },
				new User() { Email = "Preparations@complete.net", Username = "Erika", Password = "Youmustdie" },
				new User() { Email = "Albert@Levin.saber", Username = "Albert", Password = "SkyKnightsPrepareforBattle" },
			};

			users.ForEach(u => context.Users.Add(u));
			context.SaveChanges();

			var restaurants = new List<Restaurant>() {
				new Restaurant() { Name = "Five Guys" },
				new Restaurant() { Name = "Whole Foods" },
				new Restaurant() { Name = "In-N-Out Burger" }
			};

			var addresses = new List<Address>() {
				new Address() { AddressID = 1, Street = "11730 Plaza America Dr", City = "Reston", State = "VA", Country = "US", PostalCode = "20190" },
				new Address() { AddressID = 2, Street = "11660 Plaza America Dr", City = "Reston", State = "VA", Country = "US", PostalCode = "20190"},
				new Address() { AddressID = 3, Street = "890 Beach Blvd", City = "Huntington Beach", State = "CA", Country = "US", PostalCode = "92690" }
			};

			restaurants.ForEach(r => context.Restaurants.Add(r));
			context.SaveChanges();

			addresses.ForEach(a => context.Addresses.Add(a));			
			context.SaveChanges();

			var reviews = new List<Review>() {
				new Review() { UserEmail = "Chase@example.com", RestaurantID = 1, Rating = 7.3M, Content = "Good burgers. Too greasy." },
				new Review() { UserEmail = "Chase@example.com", RestaurantID = 2, Rating = 10.0M, Content = "It's organic." },
				new Review() { UserEmail = "James@nugget.com", RestaurantID = 3, Rating = 10.0M, Content = "Best fast food burgers EVAR!!" },
				new Review() { UserEmail = "John@hotmail.com", RestaurantID = 2, Rating = 2.7M, Content = "Everything's too expensive." },
				new Review() { UserEmail = "PlasmaxTravis@gmail.com", RestaurantID = 1, Rating = 9.0M, Content = "Best burgers you could ask for. Fries are a little disappointing though." },
				new Review() { UserEmail = "Albert@Levin.saber", RestaurantID = 1, Rating = 9.0M, Content = "THUNDEROUS FURY!!" }
			};

			reviews.ForEach(r => context.Reviews.Add(r));
			context.SaveChanges();
		}
	}
}