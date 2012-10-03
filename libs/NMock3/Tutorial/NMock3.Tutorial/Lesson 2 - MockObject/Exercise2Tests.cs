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
	/// Let's look at an example where the MockObject is needed in a presenter and business logic
	/// classes.
	/// 
	/// </summary>
	[TestClass]
	public class Exercise2Tests
	{
		private MockFactory _factory;
		private Mock<IViewBase> _mock;

		public Exercise2Tests()
		{
			//initialize the factory
			_factory = new MockFactory();
		}

		[TestInitialize]
		public void TestInitialize()
		{
			_mock = _factory.CreateMock<IViewBase>();
		}

		/// <summary>
		/// Check the <see cref="NMock.Mock{T}.MockObject"/> property that it is not null and it is
		/// of the interface type
		/// </summary>
		[TestMethod]
		public void MockObjectTest()
		{
			//Notice how the presenter requires a type of IViewBase as a constructor argument
			//MockObject is of that type
			PresenterBase presenter = new PresenterBase(_mock.MockObject);

			Assert.IsNotNull(presenter);
		}

		/// <summary>
		/// When you are comfortable with the code above, try writing the test below.
		/// </summary>
		[TestMethod]
		public void MyMockObjectTest()
		{
			//Below the "_mock" declaration above, create your own mock of the interface type "IContactManagementDataSource"
			//Initalize it in the TestInitialize

			//Create an instance of the "ContactManagementBusinessLogic" using your mock
			#region Hint
			//ContactManagementBusinessLogic logic = new ContactManagementBusinessLogic(_mockDataSource);
			#endregion

			//Check that the variable you created is not null
			#region Hint
			//Assert.IsNotNull(logic);
			#endregion

		}
	}
}
