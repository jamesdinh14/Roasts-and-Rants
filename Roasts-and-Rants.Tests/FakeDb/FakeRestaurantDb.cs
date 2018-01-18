using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roasts_and_Rants.Models;

namespace Roasts_and_Rants.Tests.FakeDb {

	class FakeRestaurantDb : IRestaurantRepository {

		public Dictionary<Type, object> Sets = new Dictionary<Type, object>();
		public List<object> Added = new List<object>();
		public List<object> Updated = new List<object>();
		public List<object> Removed = new List<object>();
		public bool Saved = false;
		private bool Disposed = false;

		public void Add<T>(T entity) where T : class {
			Added.Add(entity);
		}

		public IQueryable<T> Query<T>() where T : class {
			return Sets[typeof(T)] as IQueryable<T>;
		}

		public void Remove<T>(T entity) where T : class {
			Removed.Remove(entity);
		}

		public void SaveChanges() {
			Saved = true;
		}

		public void Update<T>(T entity) where T : class {
			Updated.Add(entity);
		}

		protected virtual void Dispose(bool disposing) {
			if (!Disposed) {
				if (disposing) {

				}

				Disposed = true;
			}
		}

		public void Dispose() {
			Dispose(true);
		}
	}
}
