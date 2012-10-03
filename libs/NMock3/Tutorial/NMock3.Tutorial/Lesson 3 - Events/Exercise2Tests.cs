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

namespace NMock3.Tutorial.Lesson3
{
	/// <summary>
	/// 
	/// Great!  I am glad you came back for more!
	/// 
	/// In the previous exercise you saw how events are bound up.  It is more likely that the "BindEventsInternal"
	/// method would be called on construction of the presenter.  With this in mind you would expect the events
	/// to be bound EVERY TIME.  
	/// 
	/// Now you need to combine what you know about class level MockFactorys, Mocks, and the fact that events
	/// will bind every time into a good test class outline so that there is minimal repeated code per test.
	/// 
	/// </summary>
	[TestClass]
	public class Exercise2Tests
	{
		//create a class level MockFactory variable

		//create a class level Mock of IContactManagementView variable

		//create a class level ContactManagementPresenter variable


		public Exercise2Tests()
		{
			//initialize the MockFactory

		}

		/// <summary>
		/// This method will be called to initialize every test
		/// </summary>
		[TestInitialize]
		public void TestInitialize()
		{
			//initialize the Mock

			//Add the event expectation for Init

			//Add the event expectation for Load

			//initialize the presenter

			//call BindEventsInternal
			
		}

		/// <summary>
		/// Applying what I have learned so far
		/// </summary>
		[TestMethod]
		public void PuttingItAllTogetherTest()
		{
			//Verify that the Status property of the presenter variable is equal to "Constructed"

		}
	}
}
