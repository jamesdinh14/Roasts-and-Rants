using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Roasts_and_Rants.Models {

	/// <summary>
	/// Represents a User Review for a Restaurant
	/// Association class for User and Review
	/// </summary>
	public class Review {

		/// <summary>
		/// Auto-incrementing field in the database
		/// Serves as the primary key
		/// </summary>
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ReviewID { get; set; }

		/// <summary>
		/// Holds the rating the user gives a restaurant
		/// Should be limited to a range, 0-5, 0-10, etc.
		/// </summary>
		public decimal Rating { get; set; }

		/// <summary>
		/// Holds the comments that a user has about a restaurant
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// Reference to Restaurant
		/// </summary>
		[ForeignKey("Restaurant")]
		public int RestaurantID { get; set; }
		public virtual Restaurant Restaurant { get; set; }

		/// <summary>
		/// Reference to User
		/// </summary>
		[ForeignKey("User")]
		public string UserEmail { get; set; }
		public virtual User User { get; set; }
		
	}
}