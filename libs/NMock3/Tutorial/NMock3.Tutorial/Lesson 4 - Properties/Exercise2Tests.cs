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
	/// There is a shorthand for specifying the return value of a GetProperty expectation.
	/// Check it out below.
	/// 
	/// </summary>
	[TestClass]
	public class Exercise2Tests
	{
		MockFactory _factory = new MockFactory();
		private Mock<IContactManagementView> _mockView;

		[TestInitialize]
		public void TestInit()
		{
			_mockView = _factory.CreateMock<IContactManagementView>();
		}

		/// <summary>
		/// A test demonstrating how to create a get property expectation.
		/// </summary>
		[TestMethod]
		public void GetPropertyTest()
		{
			//Create an expectation that the Id property will be called and it will return the value 123
			_mockView.Expects.One.GetProperty(v => v.ContactType, ContactType.Employee); //The WillReturn has been left off

			//Check the value
			Assert.AreEqual(ContactType.Employee, _mockView.MockObject.ContactType);
		}

		/// <summary>
		/// 
		/// </summary>
		[TestMethod]
		public void MyGetPropertyTest()
		{
		  _mockView.Expects.One.GetProperty(v => v.Name, new Name());
      Assert.AreEqual(new Name(), _mockView.MockObject.Name);
		  //Create an expectation for the Name property of the _mockView
		  //GetProperty is overloaded several times.  Use the same overload as above.

		  //Check that it returns the correct value with an Assertion
		}
	}
}
