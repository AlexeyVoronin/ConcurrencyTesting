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
	/// Class Level <see cref="NMock.MockFactory"/>
	/// 
	/// One <see cref="NMock.MockFactory"/> can be used for multiple tests.  It is declared at the class level and
	/// initialized in the constructor.
	/// </summary>
	[TestClass]
	public class Exercise3Tests
	{
		private MockFactory _factory;

		//Constructor
		public Exercise3Tests()
		{
			//initialize the factory
			_factory = new MockFactory();
		}

		/// <summary>
		/// I am using a class level MockFactory
		/// </summary>
		[TestMethod]
		public void ClassMockFactoryExampleTest()
		{
			//create a Mock<>
			Mock<IList> mock = _factory.CreateMock<IList>();

			//Assert that the mock is not null
			Assert.IsNotNull(mock);

			//Assert that the mock is not of the interface type
			Assert.IsFalse(typeof(IList).IsInstanceOfType(mock));
		}

		/// <summary>
		/// When you are comfortable with the code above, try writing the test below.
		/// </summary>
		[TestMethod]
		public void MyFirstClassMockFactoryTest()
		{
			//First, collapse the test above so you can't see the code

			//create a mock for an interface found in the .NET framework using the class level MockFactory

			//Assert that the mock is not null

			//Assert that the mock is not of the interface type
			
		}
	}
}
