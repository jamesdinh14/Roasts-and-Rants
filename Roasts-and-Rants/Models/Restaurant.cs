using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Roasts_and_Rants.Models {

	/// <summary>
	/// Represents a restaurant
	/// Holds a reference to Address
	/// Has a many-to-many relationship with User
	///    Association class: Review
	///    Has a one-to-many relationship with Review
	/// 
	/// Has a one-to-one relationship with Address
	/// </summary>
	public class Restaurant {

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int RestaurantID { get; set; }

		public string Name { get; set; }

		/// <summary>
		/// Reference to the Address of a restaurant
		/// </summary>
		public virtual Address Address { get; set; }

		/// <summary>
		/// Represents the one-to-many relationship between Restaurant and Review
		/// </summary>
		public virtual List<Review> Reviews { get; set; }
	}
}