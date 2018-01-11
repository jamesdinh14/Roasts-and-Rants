using System.Web;
using System.Web.Mvc;

namespace Roasts_and_Rants {
	public class FilterConfig {
		public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
			filters.Add(new HandleErrorAttribute());
		}
	}
}
