using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roasts_and_Rants.Tests.FakeDb;
using Roasts_and_Rants.Controllers;

namespace Roasts_and_Rants.Tests.Models {
	/// <summary>
	/// Summary description for RestaurantControllerTest
	/// </summary>
	[TestClass]
	public class RestaurantControllerTest {
		public RestaurantControllerTest() {
			//
			// TODO: Add constructor logic here
			//
		}

		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext {
			get {
				return testContextInstance;
			}
			set {
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		[TestMethod]
		public void CreateRestaurantWhenModelIsValid() {
			//
			// TODO: Add test logic here
			//
			var db = new FakeRestaurantDb();
			var controller = new RestaurantController(db);
			controller.Create(new Roasts_and_Rants.Models.Restaurant() { Name = "Brown Bag" });
			Assert.AreEqual(1, db.Added.Count);
			Assert.AreEqual(true, db.Saved);
		}

		[TestMethod]
		public void CreateRestaurantWhenModelIsInvalid() {
			var db = new FakeRestaurantDb();
			var controller = new RestaurantController(db);
			controller.ModelState.AddModelError("Key", "Invalid Model");
			controller.Create(new Roasts_and_Rants.Models.Restaurant());
			Assert.AreEqual(0, db.Added.Count);
			Assert.AreEqual(false, db.Saved);
		}
	}
}
