using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Roasts_and_Rants.Models {

	/// <summary>
	/// Represents an address in the US
	/// 
	/// 1234 Example Street
	/// Reston, VA, 20190
	/// 
	/// </summary>
	public class Address {

		
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int AddressID { get; set; }

		public string Street { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string PostalCode { get; set; }
		public string Country { get; set; }
	}
}