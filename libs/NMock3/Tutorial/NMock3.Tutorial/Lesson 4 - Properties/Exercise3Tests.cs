#region Using
#if NUNIT
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestContext = System.Object;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Category = Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute;
#endif

using System;
using NMock;
#endregion

namespace NMock3.Tutorial.Lesson4
{
	/// <summary>
	/// 
	/// In the last exercise you saw how to test property getters.  Now lets look at
	/// property setters.  When setting a property, the expectation can specify the
	/// expected value.  The mocking framework will check that the value matches.
	/// 
	/// </summary>
	[TestClass]
	public class Exercise3Tests
	{
		MockFactory _factory = new MockFactory();
		private Mock<IContactManagementView> _mockView;

		[TestInitialize]
		public void TestInit()
		{
			_mockView = _factory.CreateMock<IContactManagementView>();
		}

		[TestMethod]
		public void SetPropertyTest()
		{
			//Create an expectation that the property will be set
			_mockView.Expects.One.SetProperty(v => v.ContactType).To(ContactType.Employee);

			//Set the property
			_mockView.MockObject.ContactType = ContactType.Employee;

			//The Assert is left out on purpose because checking the value of
			//_mockView.MockObject.Id causes a "get" and we don't
			//want to set up that expectation as well for the purposes of this test
		}

		[TestMethod]
		public void MySetPropertyTest()
		{
			//Create an expectation for the Name property of the _mockView using SetProperty
		  _mockView.Expects.One.SetProperty(v => v.Name).To(new Name());
      _mockView.MockObject.Name = new Name();

		  //Set the property
		}
	}
}
