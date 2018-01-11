using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Roasts_and_Rants.Models;

namespace Roasts_and_Rants.DAL {

	public class RestaurantReviewContext : DbContext {

		public RestaurantReviewContext() : base("RestaurantReviewDb") {

		}

		public DbSet<Restaurant> Restaurants { get; set; }
		public DbSet<Address> Addresses { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Review> Reviews { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);
		}
	}
}