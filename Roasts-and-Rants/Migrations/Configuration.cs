namespace Roasts_and_Rants.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
	using Roasts_and_Rants.Models;
	using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<Roasts_and_Rants.DAL.RestaurantReviewContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Roasts_and_Rants.DAL.RestaurantReviewContext";
        }

        protected override void Seed(Roasts_and_Rants.DAL.RestaurantReviewContext context)
        {
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.
			//
			//    context.People.AddOrUpdate(
			//      p => p.FullName,
			//      new Person { FullName = "Andrew Peters" },
			//      new Person { FullName = "Brice Lambson" },
			//      new Person { FullName = "Rowan Miller" }
			//    );
			//

			// Insert test data
			//var users = new List<User>() {
			//	new User() { Email = "Chase@example.com", Username = "ChaseVisa", Password = "123456" },
			//	new User() { Email = "John@hotmail.com", Username = "John with a G", Password = "Adoboseasoning" },
			//	new User() { Email = "James@nugget.com", Username = "MenG", Password = "Runwithgold" },
			//	new User() { Email = "PlasmaxTravis@gmail.com", Username = "Plasmax167", Password = "Injustice2" },
			//	new User() { Email = "Preparations@complete.net", Username = "Erika", Password = "Youmustdie" },
			//	new User() { Email = "Albert@Levin.saber", Username = "Albert", Password = "SkyKnightsPrepareforBattle" },
			//};

			//users.ForEach(user => context.Users.AddOrUpdate(p => p.Email, user));
			//context.SaveChanges();

			var restaurants = new List<Restaurant>() {
				new Restaurant() { Name = "Five Guys", Phone = "654-908-8872", Street = "11730 Plaza America Dr", City = "Reston", State = "VA", Country = "US", PostalCode = "20190" },
				new Restaurant() { Name = "Whole Foods", Phone = "654-908-3267", Street = "11660 Plaza America Dr", City = "Reston", State = "VA", Country = "US", PostalCode = "20190" },
				new Restaurant() { Name = "In-N-Out Burger", Phone = "714-887-2218", Street = "890 Beach Blvd", City = "Huntington Beach", State = "CA", Country = "US", PostalCode = "92690" }
			};

			restaurants.ForEach(rest => context.Restaurants.AddOrUpdate(p => p.Name, rest));
			context.SaveChanges();

			var reviews = new List<Review>() {
				new Review() { RestaurantID = restaurants.Single(r => r.Name == "Five Guys").RestaurantID, Rating = 7.3M, Content = "Good burgers. Too greasy.", ModifiedDate = DateTime.UtcNow },
				new Review() { RestaurantID = restaurants.Single(r => r.Name == "Whole Foods").RestaurantID, Rating = 10.0M, Content = "It's organic.", ModifiedDate = DateTime.UtcNow },
				new Review() { RestaurantID = restaurants.Single(r => r.Name == "In-N-Out Burger").RestaurantID, Rating = 10.0M, Content = "Best fast food burgers EVAR!!", ModifiedDate = DateTime.UtcNow },
				new Review() { RestaurantID = restaurants.Single(r => r.Name == "Whole Foods").RestaurantID, Rating = 2.7M, Content = "Everything's too expensive.", ModifiedDate = DateTime.UtcNow },
				new Review() { RestaurantID = restaurants.Single(r => r.Name == "Five Guys").RestaurantID, Rating = 9.0M, Content = "Best burgers you could ask for. Fries are a little disappointing though.", ModifiedDate = DateTime.UtcNow },
				new Review() { RestaurantID = restaurants.Single(r => r.Name == "Five Guys").RestaurantID, Rating = 9.0M, Content = "THUNDEROUS FURY!!", ModifiedDate = DateTime.UtcNow }
			};

			reviews.ForEach(review => context.Reviews.AddOrUpdate(r => r.Content, review));

			//foreach (Review r in reviews) {
			//	var reviewInDatabase = context.Reviews.Where(rev => r.RestaurantID == rev.RestaurantID);

			//	if (reviewInDatabase == null) {
			//		context.Reviews.Add(r);
			//	}
			//}
			context.SaveChanges();
			
		}
    }
}
