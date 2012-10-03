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
	/// There is a shorthand for specifying the return value of a SetProperty expectation.
	/// 
	/// Previously you used the SetProperty(...).To(...) syntax.  The shorthand method is to
	/// use the SetPropertyTo(...) method.  Notice that it is like moving the "To" from the end
	/// to the name of the method.  By using the shorthand, you must specify the setter action
	/// within the lambda expression.  The value you supply will be used in the expectation.
	/// 
	/// Check it out below.
	/// 
	/// </summary>
	[TestClass]
	public class Exercise4Tests
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
			_mockView.Expects.One.SetPropertyTo(v => v.ContactType = ContactType.Employee);

			//Set the property
			_mockView.MockObject.ContactType = ContactType.Employee;

			//The Assert is left out on purpose because checking the value of
			//_mockView.MockObject.Id causes a "get" and we don't
			//want to set up that expectation as well for the purposes of this test
		}

		[TestMethod]
		public void MySetPropertyTest()
		{
			//Create an expectation for the Name property of the _mockView using SetPropertyTo

			//Set the property
		}
	}
}
