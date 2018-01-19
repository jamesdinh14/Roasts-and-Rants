using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
		/// Should be limited to a range 1-10
		/// </summary>
		private const string error_message = "Please give a rating from 1.00 to 10.00";
		[Required(ErrorMessage = error_message)]
		[Range(1.00, 10.00)]
		[RegularExpression(@"^[0-9]{1,2}(.[0-9]{1,2})?$", ErrorMessage = "Please enter a valid rating.")]
		public decimal Rating { get; set; }

		/// <summary>
		/// Holds the comments that a user has about a restaurant
		/// </summary>
		[StringLength(500)]
		public string Content { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}")]
		public DateTime ModifiedDate { get; set; }

		/// <summary>
		/// Reference to Restaurant
		/// </summary>
		[ForeignKey("Restaurant")]
		public int RestaurantID { get; set; }
		public virtual Restaurant Restaurant { get; set; }

		/// <summary>
		/// Reference to the user that created the review
		/// </summary>
		public string UserID { get; set; }
		public string UserName { get; set; }

		[NotMapped]
		// Reviews can be anonymous
		// Username will be displayed by default
		public bool Anonymous { get; set; } = false;
	}
}