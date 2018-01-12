using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Roasts_and_Rants {

	/// <summary>
	/// Extension helper methods
	/// </summary>
	public static class HtmlExtensions {

		/// <summary>
		/// Replaces the C# newline character with HTML line break character
		/// 
		/// Idea and implementation  credit goes to user cwills from stackoverflow
		/// </summary>
		public static MvcHtmlString DisplayWithBreaksFor<TModel, TValue>(this HtmlHelper<TModel> html,
			Expression<Func<TModel, TValue>> expression) {
			var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
			var model = html.Encode(metadata.Model).Replace("\n", "<br />");

			if (String.IsNullOrEmpty(model))
				return MvcHtmlString.Empty;

			return MvcHtmlString.Create(model);
		}
	}
}