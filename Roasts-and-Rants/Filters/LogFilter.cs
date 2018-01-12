using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Web.Routing;

namespace Roasts_and_Rants.Filters {

	public class LogAttribute : ActionFilterAttribute, IActionFilter {

		public string Message { get; set; }

		public LogAttribute(string message = "") {
			Message = message;
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext) {
			Log("OnActionExecuting", filterContext.RouteData);
		}

		public override void OnActionExecuted(ActionExecutedContext filterContext) {
			Log("OnActionExecuted", filterContext.RouteData);
		}

		public override void OnResultExecuted(ResultExecutedContext filterContext) {
			Log("OnResultExecuted", filterContext.RouteData);
		}

		public override void OnResultExecuting(ResultExecutingContext filterContext) {
			Log("OnResultExecuting", filterContext.RouteData);
		}

		private void Log(string methodName, RouteData data) {
			Debug.WriteLine($"Log called from {methodName}" + Message);
		}
	}
}