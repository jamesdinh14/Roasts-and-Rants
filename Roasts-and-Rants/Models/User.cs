using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Roasts_and_Rants.Models {

	/// <summary>
	/// Represents a User of the web application
	/// Has a many-to-many relationship with Restaurant
	///		Association class: Review
	///		Has a one-to-many relationship with Review
	/// </summary>
	public class User {

		/// <summary>
		/// Unique identifier for a User
		/// Each Email should be associated with one and only one account
		/// </summary>
		[Key]
		public string Email { get; set; }

		/// <summary>
		/// Username does not have to be unique
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// Security issues with a public password and no data hiding
		/// Avoid this for deployment
		/// </summary>
		public string Password { get; set; }
		
		/// <summary>
		/// Represents the one-to-many relationship between User and Review
		/// </summary>
		public virtual List<Review> Reviews { get; set; }
	}
}