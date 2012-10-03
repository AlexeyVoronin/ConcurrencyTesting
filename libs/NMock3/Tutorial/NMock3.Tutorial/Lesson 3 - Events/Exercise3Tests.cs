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

namespace NMock3.Tutorial.Lesson3
{
	/// <summary>
	/// 
	/// Nice Job!
	/// 
	/// Now, I am sure you are thinking, "Event expectations are nice, but how do I fire the event?"
	/// 
	/// Let's look at how that works.  The EventBinding method returns a <see cref="NMock.EventInvoker"/>
	/// that can be used to invoke the event.  This is very handy.
	/// 
	/// </summary>
	[TestClass]
	public class Exercise3Tests
	{
		private MockFactory _factory;
		private Mock<IContactManagementView> _mockView;
		private ContactManagementPresenter _presenter;
		private EventInvoker _initEvent;
		private EventInvoker<ContactEventArgs> _loadEvent;


		public Exercise3Tests()
		{
			_factory = new MockFactory();
		}

		[TestInitialize]
		public void TestInitialize()
		{
			_mockView = _factory.CreateMock<IContactManagementView>();

			//store the result of EventBinding into an "Invoker"
			//this EventBinding is using the default EventArgs
			_initEvent = _mockView.Expects.One.EventBinding(v => v.Init += null);

			//notice how this EventBinding requires a specific type of EventArgs
			_loadEvent = _mockView.Expects.One.EventBinding<ContactEventArgs>(v => v.Load += null);

			_presenter = new ContactManagementPresenter(_mockView.MockObject);

			_presenter.BindEventsInternal();
		}

		/// <summary>
		/// Invoking an event
		/// </summary>
		[TestMethod]
		public void EventInvokingExampleTest()
		{
			//To raise or fire the event, call the Invoke method of the MockEventInvoker
			_initEvent.Invoke();

			//Check that the event was fired
			Assert.AreEqual("Init", _presenter.Status);
		}

		/// <summary>
		/// In this test you will raise the other event.  It will not work at this time so the 
		/// ExpectedException attribute is used so the test will pass.  Through the next exercises
		/// the Load event will start to work.
		/// </summary>
		[Ignore] //TODO:remove this before testing
		[TestMethod]
		[ExpectedException(typeof(NullReferenceException))] //leave this here for now; check out the presenter code to see why
		public void MyEventInvokingTest()
		{
		  _loadEvent.Invoke(new ContactEventArgs(3));
      Assert.AreEqual("Load", _presenter.Status);
			//Invoke the load event
			#region Hint
			//_loadEvent.Invoke(new ContactEventArgs(3));
			#endregion

			//check that the status of the presenter changed
			#region Hint
			//Assert.AreEqual("Load", _presenter.Status);
			#endregion
		}
	}
}
