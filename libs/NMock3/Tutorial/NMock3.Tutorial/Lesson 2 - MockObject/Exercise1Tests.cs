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
using NMock;
#endregion


namespace NMock3.Tutorial.Lesson2
{
	/// <summary>
	/// 
	/// As exercises progress, some comments will drop out because it is assumed that you understand
	/// what is going on and why it is done that way.  If you find a confusing part, go back through
	/// previous exercises to refresh your memory.
	/// 
	/// In the tests from the previous exercise you proved that Mock{T} is not of the type of interface
	/// it is mocking.  There are times where you need an object of the type of mocked interface to
	/// pass into a constructor of a class or method.  This is very common in a MVP model where interface
	/// instances of the View and Model are passed into the constructor of the Presenter and the Presenter
	/// performs operations on these references.  Example:
	///		ConcreteView --- ViewInterface --- Presenter --- ModelInterface --- ConcreteModel
	/// 
	/// To acomplish this, the Mock has a <see cref="NMock.Mock{T}.MockObject"/> property that is of
	/// the mocked interface type.  The reason that the Mock is not of the mocked interface type is
	/// because the Mock provides the ability to set expectations.  That functionality is not in the 
	/// interface, so the interface type must be exposed as this <see cref="NMock.Mock{T}.MockObject"/>
	/// property.
	/// 
	/// Let's see this work.
	/// </summary>
	[TestClass]
	public class Exercise1Tests
	{
		private MockFactory _factory;
		private Mock<IList> _mock;

		public Exercise1Tests()
		{
			//initialize the factory
			_factory = new MockFactory();
		}

		[TestInitialize]
		public void TestInitialize()
		{
			_mock = _factory.CreateMock<IList>();
		}

		/// <summary>
		/// Check the <see cref="NMock.Mock{T}.MockObject"/> property that it is not null and it is
		/// of the interface type
		/// </summary>
		[TestMethod]
		public void MockObjectTest()
		{
			//check that the MockObject is not null
			Assert.IsNotNull(_mock.MockObject);

			//check that the MockObject is of the interface type
			Assert.IsTrue(typeof(IList).IsInstanceOfType(_mock.MockObject));
		}

		/// <summary>
		/// When you are comfortable with the code above, try writing the test below.
		/// </summary>
		[TestMethod]
		public void MyMockObjectTest()
		{
			//Below the "_mock" declaration above, create your own mock
			//Initalize it in the TestInitialize

			//check that the MockObject of your mock is not null

			//check that the MockObject of your mock is of the interface type

		}
	}
}
