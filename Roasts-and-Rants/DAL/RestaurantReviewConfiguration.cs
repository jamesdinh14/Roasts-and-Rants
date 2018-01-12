using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace Roasts_and_Rants.DAL {

	public class RestaurantReviewConfiguration : DbConfiguration {

		public RestaurantReviewConfiguration() {
			SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
		}
	}
}