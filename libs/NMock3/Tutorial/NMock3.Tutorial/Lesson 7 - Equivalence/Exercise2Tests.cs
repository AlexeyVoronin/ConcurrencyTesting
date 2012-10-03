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

namespace NMock3.Tutorial.Lesson7
{
	[TestClass]
	public class Exercise2Tests
	{
		/// <summary>
		/// 
		/// </summary>
		[TestMethod]
		public void SaveAddressTest()
		{
			//Create a MockFactory named factory
			MockFactory factory = new MockFactory();

			//Create an instance of an IContactManagementDataSource Mock named dataSource
			Mock<IContactManagementDataSource> dataSource = factory.CreateMock<IContactManagementDataSource>();
			//Create an instance of an IStandardizationService Mock named standardizationService
			Mock<IStandardizationService> standardizationService = factory.CreateMock<IStandardizationService>();

			//Create an instance of the ContactManagementBusinessLogic class with 2 constructor arguments
			//named businessLogic
			ContactManagementBusinessLogic businessLogic = new ContactManagementBusinessLogic(standardizationService.MockObject, dataSource.MockObject);

			//Declare the contactId variable
			int contactId = 1234;
			//Declare the addressIN variable
			Address addressIN = new Address(0, "123 S. 201st St.", string.Empty, "Omaha", "NE", "68154", "USA", AddressType.Home);
			//Declare the addressOUT variable
			Address addressOUT = new Address(0, "123 S 201 ST", string.Empty, "Omaha", "NE", "68154", "USA", AddressType.Home);
			//Declare the rowsAffected variable
			int rowsAffected = 1;

			//Create standardizationService expectation for the StandardizePhone method
			//It expects to be called WITH the addressIN variable and it RETURNS the addressOUT variable
			standardizationService.Expects.One
				.Method(s => s.StandardizeAddress(null))
				.With(addressIN)
				.Will(Return.Value(addressOUT));

			//Create the dataSource expectation for the SaveAddress method
			//It expects to be called WITH the addressOUT variable and it RETURNS the rowsAffected variable
			dataSource.Expects.One
				.Method(d => d.SaveAddress(0, null))
				.With(contactId, addressOUT)
				.Will(Return.Value(rowsAffected));

			//Call the SaveAddress method on the businessLogic object and store the result
			int i = businessLogic.SaveAddress(contactId, addressIN);

			//Test the rows affected are equal to the result of the SavePhone call
			Assert.AreEqual(rowsAffected, i);
		}

		[TestMethod]
		public void SaveContactTest()
		{
			//Assert.Ignore("Ignore for now"); //TODO: remove later in the exercise
			//Create a MockFactory
			MockFactory factory = new MockFactory();

			//Create an instance of an IAddressStandardizationService Mock and IContactManagementDataSource Mock
			Mock<IStandardizationService> addressService = factory.CreateMock<IStandardizationService>();
			Mock<IContactManagementDataSource> dataSource = factory.CreateMock<IContactManagementDataSource>();

			//Create the ContactManagementBusinessLogic
			ContactManagementBusinessLogic businessLogic = new ContactManagementBusinessLogic(addressService.MockObject, dataSource.MockObject);

			//Contact to be passed to the businesslogic
			Contact contactIN = new Contact(123);

			//SaveContactResult to be returned by the businessLogic
			SaveContactResult result = new SaveContactResult();
			result.ContactId = 123;
			result.RecordsAffected = 1;

			//Address to be passed in to the addressService
			Address addressIN = new Address(0, "123 S. 201st St.", string.Empty, "Omaha", "NE", "68154", "USA", AddressType.Home);
			contactIN.Addresses.Add(addressIN);

			//Address to be returned by the addressService
			Address addressOUT = new Address(0, "123 S 201 ST", string.Empty, "Omaha", "NE", "68154", "USA", AddressType.Home);

			int rowsAffected = 2;

			//Add the expectations
			dataSource.Expects.One
				.Method(d => d.SaveContact(null)) //this method param is needed for the compiler
				.With(contactIN)
				.Will(Return.Value(result));

			addressService.Expects.One
				.Method(a => a.StandardizeAddress(null)) //this method param is needed for the compiler
				.With(addressIN)
				.Will(Return.Value(addressOUT));

			dataSource.Expects.One
				.Method(d => d.SaveAddress(0, null))
				.With(contactIN.Id, addressOUT)
				.Will(Return.Value(1));

			//Call the method
			int i = businessLogic.SaveContact(contactIN);

			//Add the unit test assertion
			Assert.AreEqual(rowsAffected, i);

		}

	}
}
