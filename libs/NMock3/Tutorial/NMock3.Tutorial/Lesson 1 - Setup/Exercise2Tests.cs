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

using System.Collections;
using System.Data;
using NMock;
#endregion

namespace NMock3.Tutorial.Lesson1
{
	/// <summary>
	/// 
	/// Now that you know how to create a <see cref="NMock.MockFactory"/>, lets see what this factory creates.
	/// <see cref="NMock.MockFactory"/>'s creat mock objects.  
	/// 
	/// All Users:
	/// In this version of NMock, generics are now used exclusively to return a mock object of the type that is
	/// requested.
	/// 
	/// Mock objects, from now on referred to as Mocks, are the object that you will use to establish what you 
	/// expect to happen when the code is exercised.  These will now be referred to as Expectations.  So, Mocks
	/// have Expectations.  What does that mean?  When your unit test is exercising some code that performs an
	/// operation on an object that is being accessed through an interface, you will want to define exactly what
	/// interactions are allowed and what the interface implementation should do under those conditions.
	/// 
	/// We will start simply by creating a <see cref="NMock.Mock{T}"/> and get into the stuff explained above
	/// later.
	/// 
	/// </summary>
	[TestClass]
	public class Exercise2Tests
	{
		/// <summary>
		/// Creating your first Mock
		/// </summary>
		[TestMethod]
		public void MockExampleTest()
		{
			//Create a MockFactory
			MockFactory factory = new MockFactory();

			//create a Mock<>
			Mock<IList> mock = factory.CreateMock<IList>();

			//Assert that the mock is not null
			Assert.IsNotNull(mock, "mock was null");

			//Assert that the mock is not of the interface type
			Assert.IsFalse(typeof(IList).IsInstanceOfType(mock), "mock is not an IList, it is a Mock<IList>");

			//The purpose of this assertion is to prove that the mock is not an IList
			//it is a "Mock of IList".
		}

		/// <summary>
		/// When you are comfortable with the code above, try writing the test below.
		/// </summary>
		[TestMethod]
		public void MyFirstMockTest()
		{
			//First, collapse the test above so you can't see the code

			//Create a MockFactory

			//create a Mock<> of an interface from the System.Data namespace (such as IDbCommand)

			//Assert that the mock is not null

			//Assert that the mock is not of the interface type

		}
	}
}
