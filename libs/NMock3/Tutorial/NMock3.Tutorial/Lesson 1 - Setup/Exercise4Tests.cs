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

namespace NMock3.Tutorial.Lesson1
{
	/// <summary>
	/// 
	/// Class Level <see cref="NMock.Mock{T}"/>
	/// 
	/// One <see cref="NMock.Mock{T}"/> can also be use per test class.  It is similar to how the 
	/// <see cref="NMock.MockFactory"/> is initialized.
	/// Instead of initializing it in the constructor, it is done in the TestInitialize.  Using the
	/// TestInitialize will initialize it on a per test basis so that operations performed on it or
	/// with it are not held over between tests.  Another reason for doing this will be shown later
	/// when the unit test needs to gather results.
	/// 
	/// </summary>
	[TestClass]
	public class Exercise4Tests
	{
		private MockFactory _factory;
		private Mock<IList> _mock;

		//Constructor
		public Exercise4Tests()
		{
			//initialize the factory
			_factory = new MockFactory();
		}

		/// <summary>
		/// This method is executed before each test is run
		/// </summary>
		[TestInitialize]
		public void TestInitialize()
		{
			_mock = _factory.CreateMock<IList>();
		}

		/// <summary>
		/// I am using a class level factory and mock.
		/// </summary>
		[TestMethod]
		public void ClassLevelMockExampleTest()
		{
			//Assert that the mock is not null
			Assert.IsNotNull(_mock);

			//Assert that the mock is not of the interface type
			Assert.IsFalse(typeof(IList).IsInstanceOfType(_mock));

			_mock = null; //prove that it is inited each test
		}

		/// <summary>
		/// When you are comfortable with the code above, try writing the test below.
		/// </summary>
		[TestMethod]
		public void MyFirstClassLevelMockTest()
		{
			//First, collapse the test above so you can't see the code

			//Assert that the mock is not null

			//Assert that the mock is not of the interface type

		}
	}
}
