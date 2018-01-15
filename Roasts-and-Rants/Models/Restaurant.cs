using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
		[ScaffoldColumn(false)]
		public int RestaurantID { get; set; }

		[Required]
		[StringLength(50, ErrorMessage = "Restaurant Name should be within 50 characters")]
		public string Name { get; set; }

		[Display(Name = "Phone Number")]
		[DataType(DataType.PhoneNumber)]
		[RegularExpression(@"^\D?(\d{3})\D?\D?(\d{3})\D?(\d{4})$", ErrorMessage = "Please enter the phone number in a valid format")]
		public string Phone { get; set; }

		/// <summary>
		/// Reference to the Address of a restaurant
		/// </summary>
		//public virtual Address Address { get; set; }

		public string Street { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Country { get; set; }

		[DataType(DataType.PostalCode)]
		[RegularExpression(@"^(?!0+$)[0-9]{5,5}$", ErrorMessage = "Please enter a valid zip code")]
		[Display(Name = "Zip code")]
		public string PostalCode { get; set; }

		/// <summary>
		/// Custom display method for a Restaurant's Address
		/// 
		/// 1234 Example St
		/// Reston, VA, USA 20170
		/// </summary>
		/// <returns></returns>
		public string DisplayAddress() {
			return Street + "<br />" + City + ", " + State + ", " + Country + PostalCode;
		}

		/// <summary>
		/// Represents the one-to-many relationship between Restaurant and Review
		/// </summary>
		public virtual List<Review> Reviews { get; set; }

		/// <summary>
		/// Computed column
		/// </summary>
		[NotMapped]
		[Display(Name = "Average Rating")]
		public decimal AverageRating { get; set; }
	}
}