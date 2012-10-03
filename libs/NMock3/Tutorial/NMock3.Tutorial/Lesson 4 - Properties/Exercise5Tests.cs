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

namespace NMock3.Tutorial.Lesson4
{
	/// <summary>
	/// 
	/// Review: The shorthand methods:
	/// mockView.Expects.One.GetProperty(m => m.ContactType, ContactType.Employee);
	/// mockView.Expects.One.SetPropertyTo(m => m.ContactType = ContactType.Employee);
	/// 
	/// Sometimes the GetProperty or SetProperty methods won't work.  If a property is defined
	/// as ReadOnly or WriteOnly, the lambda expression will fail because the compiler won't 
	/// allow you to do a Get or Set action.
	/// 
	/// Lets look at 2 examples.
	/// 
	/// </summary>
	[TestClass]
	public class Exercise5Tests
	{
		MockFactory _factory = new MockFactory();
		private Mock<IContactManagementView> _mockView;

		[TestInitialize]
		public void TestInit()
		{
			_mockView = _factory.CreateMock<IContactManagementView>();
		}

		[TestMethod]
		public void SetPropertyTest()
		{
			//The property on SuffixDataSource on IContactManagementView is WriteOnly
			//Try setting up a SetProperty expectation for the SuffixDataSource (not a SetPropertyTo)
			#region Hint
			//_mockView.Expects.One.SetProperty(m => m.SuffixDataSource).To(null);
			#endregion

		  _mockView.Expects.One.SetPropertyTo(v => v.SuffixDataSource = null);



		  //It doesn't work because SuffixDataSource is WriteOnly
		  //Even though you should expect to be able to set it, there
		  //is no getter so the Lambda can't be compiled.

		  //The fix is to use the SetPropertyTo

		  #region Hint

		  //_mockView.Expects.One.SetPropertyTo(m => m.SuffixDataSource = null);

		  #endregion
		}


		[TestMethod]
		public void SetPropertyToTest()
		{
			//Try setting up a SetPropertyTo expectation for the Id property
			#region Hint
			//_mockView.Expects.One.SetPropertyTo(m => m.Id = 123);
			#endregion



			//It doesn't work because Id is readonly


			//This line lookes like it works to set up an expectation even though
			//there is no setter but it will throw an exception at runtime
			//uncomment it to see
			_mockView.Expects.One.SetProperty(m => m.Id).To(123);
		}
	}
}
