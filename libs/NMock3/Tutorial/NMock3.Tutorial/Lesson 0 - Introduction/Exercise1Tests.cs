/*
 * In this tutorial uses the followng conventions:
 * 
 *	1. You can run the unit tests with NUnit or MSTest.  To test with Nunit, declare a Conditional 
 *	Compilation Symbol named "NUNIT" on Build tab in this project's property pages.  To use MSTest
 *	do not define this symbol.  All tests in this project use the "using" header below to switch 
 *	between the two types.  To experience "Zero Friction" unit testing, I recommend either the
 *	TestDriven.Net or Resharper Visual Studio add-ins.
 */

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

/*
 *	2. Why is there only a "using NMock;" line?  Where is the "using NMock2;" or "using NMock3;" line?
 *	There is no NMock3 namespace because it is strongly recommended that you don't version at the 
 *	namespace level.  With this in mind, NMock3 removed the version number from the namespace but 
 *	versioned at the assembly level to aline with the recommendation.
 */

using NMock;

namespace NMock3.Tutorial.Lesson0
{
	/// <summary>
	/// 
	/// Exercises will describe what you will learn about in the summary comment before the class.
	/// 
	/// </summary>
	[TestClass]
	public class Exercise1Tests
	{
		/// <summary>
		/// Tests will describe what you will do in the summary comment before the test.
		/// The first test is typically done for you
		/// </summary>
		[TestMethod]
		public void Test1()
		{
			//tests will have comments that explain the next line of code

			Assert.IsTrue(true);
		}

		/// <summary>
		/// The second test will have boilerplate comments to guide you through the test.
		/// </summary>
		[TestMethod]
		public void Test2()
		{
			//tests will also have C# regions named "Hint" that has a line of code that you could
			//	look at or uncomment to see what to do next.
			#region Hint
			//LEAVE HINTS COLLAPSED UNTIL YOU ARE READY TO LOOK AT THEM!
			#endregion

			Assert.IsTrue(true);
		}

		//PLEASE TRY RUNNING THESE TESTS RIGHT NOW TO CHECK FOR UNTRUSTED DLLS.
		//SEE THE ReadMe.txt TO FIX
	}
}
