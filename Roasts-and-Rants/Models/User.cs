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
	public sealed class User {

		/// <summary>
		/// Unique identifier for a User
		/// Each Email should be associated with one and only one account
		/// </summary>
		[Key]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		/// <summary>
		/// Username does not have to be unique
		/// </summary>
		[StringLength(32, ErrorMessage = "Username should be less than 32 characters long.")]
		public string Username { get; set; }

		/// <summary>
		/// Security issues with a public password and no data hiding
		/// Avoid this for deployment
		/// </summary>
		[DataType(DataType.Password)]
		[MinLength(16)]
		[MaxLength(56)]
		public string Password { get; set; }
		
		/// <summary>
		/// Represents the one-to-many relationship between User and Review
		/// </summary>
		public List<Review> Reviews { get; set; }
	}
}