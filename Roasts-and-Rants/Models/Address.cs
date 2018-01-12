using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Roasts_and_Rants.Models {

	/// <summary>
	/// Represents an address in the USA
	/// 
	/// Has a one-to-one relationship with Restaurant
	/// 
	/// </summary>
	public class Address {

		[ForeignKey("Restaurant")]
		public int AddressID { get; set; }

		public string Street { get; set; }
		public string City { get; set; }
		public string State { get; set; }

		[DataType(DataType.PostalCode)]
		[RegularExpression(@"^(?!0+$)[0-9]{5,5}$", ErrorMessage = "Please enter a valid zipcode")]
		public string PostalCode { get; set; }
		public string Country { get; set; }

		/// <summary>
		/// Reference to restaurant
		/// </summary>
		public virtual Restaurant Restaurant { get; set; }

		/// <summary>
		/// Formats an Address like the example below
		/// 
		/// 1234 Example Street
		/// Reston, VA 20190
		/// USA
		/// </summary> 
		public override string ToString() {
			return Street + "\n" + City + ", "  + State + " " + PostalCode + "\n" + Country;
		}
	}
}