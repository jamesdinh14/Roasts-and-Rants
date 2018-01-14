using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Roasts_and_Rants.Models;

namespace Roasts_and_Rants.Binders {

	public class RestaurantDataBinder : IModelBinder {
		
		public object BindModel(ControllerContext controllerContext, ModelBindingContext modelBindingContext) {

			if (modelBindingContext.ModelType == typeof(Restaurant)) {
				HttpRequestBase request = controllerContext.HttpContext.Request;
				string name = request.Form.Get("Name");
				string phone = request.Form.Get("Phone");
				string street = request.Form.Get("Address.Street");
				string city = request.Form.Get("Address.City");
				string state = request.Form.Get("Address.State");
				string postalCode = request.Form.Get("Address.PostalCode");
				string country = request.Form.Get("Address.Country");

				return new Restaurant() {
					Name = name,
					Phone = phone,
					Address = new Address() {
						Street = street,
						City = city,
						State = state,
						PostalCode = postalCode,
						Country = country
					}
				};
			}

			return null;
		}
	}
}