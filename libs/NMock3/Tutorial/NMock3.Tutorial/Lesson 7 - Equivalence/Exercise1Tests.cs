using NMock;

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


namespace NMock3.Tutorial.Lesson7
{
	/// <summary>
	/// These tests are testing a Business component for a CRM (Contact Relationship Management) system.  The
	/// Data Layer is being mocked so that the business layer can be tested in isolation.  The data layer classes
	/// don't even exist!
	/// </summary>
	[TestClass]
	public class Exercise1Tests
	{
		/// <summary>
		/// In this test, read through the comments and code.  In the next test you will be implementing a test for Phone.
		/// The purpose of this test is exercise the logic of saving an Internet Handle (email, IM address, etc.).
		/// </summary>
		[TestMethod]
		public void SaveInternetHandleTest()
		{
			//Create a MockFactory
			MockFactory factory = new MockFactory();

			//Create an instance of an IContactManagementDataSource Mock
			Mock<IContactManagementDataSource> dataSource = factory.CreateMock<IContactManagementDataSource>();

			//Create the ContactManagementBusinessLogic with 1 constructor argument
			ContactManagementBusinessLogic businessLogic = new ContactManagementBusinessLogic(dataSource.MockObject);

			int contactId = 1234;
			InternetHandle handle = new InternetHandle(0, "john.doe@some.emailaddress.com", "John Doe (Email)", HandleType.PersonalEmail);
			int rowsAffected = 1;

			//Create an expectation that the dataSource will have its SaveInternetHandle method called
			//with contactId, and handle and will return the value of rowsAffected
			dataSource.Expects.One
				//note how the lambda specifies the method to call
				.MethodWith(d => d.SaveInternetHandle(contactId, handle)) //these are the parameters to match
				.WillReturn(rowsAffected); //the return value of the mocked method

			//Note how the expectation reads like an English sentence.
			//This is called *syntactic sugar*.

			//Call the method
			int i = businessLogic.SaveInternetHandle(contactId, handle);

			//Add the unit test assertion
			Assert.AreEqual(rowsAffected, i);
		
		}

		/// <summary>
		/// When you are comfortable with the code above, try writing the test for Phone Number below.
		/// 
		/// COLLAPSE THE HINTS BEFORE PROCEEDING :-)
		/// 
		/// The purpose of this test is exercise the logic of saving a phone number.
		/// </summary>
		[TestMethod]
		public void SavePhoneNumberTest()
		{
			//Create a MockFactory named factory
			#region Hint
			//MockFactory factory = new MockFactory();
			#endregion

			//Create an instance of an IContactManagementDataSource Mock named dataSource
			#region Hint
			//Mock<IContactManagementDataSource> dataSource = factory.CreateMock<IContactManagementDataSource>();
			#endregion

			//Create an instance of an IStandardizationService Mock named standardizationService
			#region Hint
			//Mock<IStandardizationService> standardizationService = factory.CreateMock<IStandardizationService>();
			#endregion

			//Create an instance of the ContactManagementBusinessLogic class with 2 constructor arguments
			//named businessLogic
			#region Hint
			//ContactManagementBusinessLogic businessLogic = new ContactManagementBusinessLogic(standardizationService.MockObject, dataSource.MockObject);
			#endregion

			//Declare the contactId variable
			int contactId = 1234;
			//Declare the phoneIN variable
			Phone phoneIN = new Phone(0, "1", "402", "981", "5551234", string.Empty, PhoneType.Home);
			//Declare the phoneOUT variable
			Phone phoneOUT = new Phone(0, "1", "402", "981", "555-1234", string.Empty, PhoneType.Home);
			//Declare the rowsAffected variable
			int rowsAffected = 1;

			//Create standardizationService expectation for the StandardizePhone method
			//It expects to be called WITH the phoneIN variable and it RETURNS the phoneOUT variable
			#region Hint
			//standardizationService.Expects.One
			//	.MethodWith(s => s.StandardizePhone(phoneIN))
			//	.WillReturn(phoneOUT);
			#endregion

			//Create the dataSource expectation for the SavePhone method
			//It expects to be called WITH the phoneOUT variable and it RETURNS the rowsAffected variable
			#region Hint
			//dataSource.Expects.One
			//	.MethodWith(d => d.SavePhone(contactId, phoneOUT))
			//	.WillReturn(rowsAffected);
			#endregion

			//Call the SavePhone method on the businessLogic object and store the result
			#region Hint
			//int i = businessLogic.SavePhone(contactId, phoneIN);
			#endregion

			//Test the rows affected are equal to the result of the SavePhone call
			#region Hint
			//Assert.AreEqual(rowsAffected, i);
			#endregion


			Assert.Inconclusive("Exercise Not Complete");
		}
	}
}