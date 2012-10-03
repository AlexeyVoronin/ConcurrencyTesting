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

namespace NMock3.Tutorial.Lesson3
{
	/// <summary>
	/// 
	/// Now you know how to set up a MockFactory and Mock and you know how to use the MockObject.
	/// Let's learn about expectations.
	/// 
	/// A Review:
	/// Mocking tests the interactions between an interface (typically) and a class that
	/// uses that interface.  Testing this interaction involves listing the operations that will be 
	/// performed.  For example, if the interface exposes a method and the class calls that method
	/// we would list an expectation in a unit test that the method will be called.  If the class is
	/// refactored and the method is no longer being called, the unit test would fail because the 
	/// expectation is not being met.
	/// 
	/// All Users:
	/// Expectations are set up with lambda expressions.  In all cases, the lambda expressions provide
	/// the mocking framework with the NAME of the property/method/event for which you setting the
	/// expectation.  To repeat, it is using the lambda expression just to grab the name.  IN SOME
	/// CASES the mocking framework can use the lambda expression to also provide arguments for the
	/// expectation.
	/// 
	/// Returning Users:
	/// In the past, Expectations were specified on "object types".  Now that generics are being used,
	/// the Mock knows exactly what properties/methods/events are available.  Lambdas also replace
	/// the string-based expectations, so when a refactor happens you will get compile-time checking
	/// on all of your expectations.  HOORAH!  You will note that the names of the new expectation
	/// methods are placed closely to the names you have used in the past.  You will also note that
	/// there are new names that have combined words that may have been seperate in the past.  Check
	/// out the "Shortcuts" Lession to learn more.
	/// 
	/// All Users:
	/// In this first example of an expectation, you will see a lambda expression used where it is
	/// ONLY getting the NAME of the event we are expecting.  The rest of the lambda expression is
	/// ONLY there so that it will compile.  I agree that "+= null" looks weird but the C# compiler
	/// will only allow an event to be used on the left side of a "+=" or "-=" operation unless the
	/// event is being raised.  
	/// 
	/// Enjoy!
	/// 
	/// </summary>
	[TestClass]
	public class Exercise1Tests
	{
		MockFactory factory = new MockFactory();

		/// <summary>
		/// My first expectation
		/// </summary>
		[TestMethod]
		public void EventBindingExampleTest()
		{
			Mock<IViewBase> mockView = factory.CreateMock<IViewBase>();

			//Create an expectation for an event binder
			mockView.Expects.One.EventBinding(v => v.Init += null);  //the mock expects the Init event to be bound

			//See how the expectation reads like an English sentence.
			//This is called *syntactic sugar*.
			//"Syntax" classes are added in just to make it read easier

			PresenterBase presenter = new PresenterBase(mockView.MockObject);

			presenter.BindEventsInternal();

			Assert.AreEqual("Constructed", presenter.Status);
		}

		/// <summary>
		/// In the test above "Init" was declared with a default EventHandler signature.  This means
		/// that the arguments were the default EventArgs.
		/// 
		/// In this test, the "Load" event uses non-default EventArgs.  Let's see how this works.
		/// You will be helping out.
		/// </summary>
		[TestMethod]
		public void MyEventBindingTest()
		{
			Mock<IContactManagementView> mockView = factory.CreateMock<IContactManagementView>();

			//Create an expectation for the Init event binder on the mockView (exactly like the previous test)
			#region Hint
			//mockView.Expects.One.EventBinding(v => v.Init += null);
			#endregion

			//Create an expectation for the Load event binder on the mockView
			//The event uses the ContactEventArgs so use the generic version of EventBinding
			#region Hint
			//mockView.Expects.One.EventBinding<ContactEventArgs>(v => v.Load += null);
			#endregion

			//Create an instance of the ContactManagementPresenter using the mock
			#region Hint
			//ContactManagementPresenter presenter = new ContactManagementPresenter(mockView.MockObject);
			#endregion

			//call the BindEventsInternal method on the presenter variable
			#region Hint
			//presenter.BindEventsInternal();
			#endregion

			//check that the presenter's status is "Constructed"
			#region Hint
			//Assert.AreEqual("Constructed", presenter.Status);
			#endregion
		}
	}
}
