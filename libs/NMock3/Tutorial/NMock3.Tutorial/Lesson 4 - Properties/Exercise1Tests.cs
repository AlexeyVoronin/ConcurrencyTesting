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
	/// Now that you have seen some expectations in action, lets move on to more
	/// complex scenarios that involve properties.
	/// 
	/// Mocking properties is very similar to events.  You will set up an expectation
	/// that a property will be accessed through its getter or setter.  With this in
	/// mind, you can predict that there are two property expectation types: get and
	/// set.
	/// 
	/// Lets explore getting property values.  When code is calling the getter of a
	/// property the code expects some value back.  Think about how that would work
	/// if the code was interacting with an interface.  What actual value would be 
	/// returned?  The mocking framework provides the ability to specify the value
	/// to be returned to the calling code when the expectation is created.
	/// 
	/// </summary>
	[TestClass]
	public class Exercise1Tests
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
			_mockView.Expects.One.GetProperty(v => v.ContactType).WillReturn(ContactType.Employee);

			//Check the value
			Assert.AreEqual(ContactType.Employee, _mockView.MockObject.ContactType);
		}

		/// <summary>
		/// 
		/// </summary>
		[TestMethod]
		public void MyGetPropertyTest()
		{
		  _mockView.Expects.One.GetProperty(m => m.Name).WillReturn(new Name());
      Assert.AreEqual(new Name(), _mockView.MockObject.Name);
		  //Create an expectation for the Name property of the _mockView
		  //GetProperty is overloaded several times.  Use the same overload as above.

		  //Check that it returns the correct value with an Assertion
		}
	}
}
