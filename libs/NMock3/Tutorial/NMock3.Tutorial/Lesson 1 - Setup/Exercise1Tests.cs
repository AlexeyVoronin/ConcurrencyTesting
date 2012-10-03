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

using NMock;
#endregion

namespace NMock3.Tutorial.Lesson1
{
	/// <summary>
	/// 
	/// The purpose of mocking is to isolate the testing of one class without involving other classes that
	/// make up a complete solution.  There are several reasons to test classes in isolation.
	/// 	
	/// 	1. Focus - It is easier to focus on testing a single component for bugs.
	/// 	2. Less work - Creating supporting classes to test one class creates more work.
	/// 	3. Dependencies - Data from a live database could change, causing the changes in the test
	/// 	4. Expectations - Creating tests on one class for all posibilities 
	/// 
	/// First Time Users:
	/// To begin, you start with a <see cref="NMock.MockFactory"/>.  A <see cref="NMock.MockFactory"/> is
	/// responsible for creating all of the mock objects of interfaces or classes that will be used in your
	/// unit tests.  It is able to keep track of everything going on with all mock objects.
	/// 
	/// Returning Users:
	/// If you have used NMock in the past, <see cref="NMock.MockFactory"/> is similar to a Mockery
	/// so it has all of the capabilities that you would expect of a Mockery.
	/// 
	/// All Users:
	/// After we cover some basic concepts, we will explore the power of the <see cref="NMock.MockFactory"/>.
	/// 
	/// </summary>
	[TestClass]
	public class Exercise1Tests
	{
		/// <summary>
		/// Let's start by creating your first <see cref="NMock.MockFactory"/>.
		/// </summary>
		[TestMethod]
		public void MockFactoryExampleTest()
		{
			//Create a MockFactory
			MockFactory factory = new MockFactory();

			//Assert that the MockFactory is not null
			Assert.IsNotNull(factory);
		}

		/// <summary>
		/// When you are comfortable with the code above, try writing the test below.
		/// </summary>
		[TestMethod]
		public void MyFirstMockFactoryTest()
		{
			//First, collapse the test above so you can't see the code

			//Create a MockFactory
			#region Hint
			//MockFactory factory = new MockFactory();
			#endregion

			//Assert that the MockFactory is not null
			#region Hint
			//Assert.IsNotNull(factory);
			#endregion
		}
	}
}
