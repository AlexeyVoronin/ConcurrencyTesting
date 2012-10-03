using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Transactions;

namespace NMock3.Tutorial
{
	public class ContactManagementBusinessLogic
	{
		#region Member Variables
		private readonly IStandardizationService _standardizationService;
		private readonly IContactManagementDataSource _dataSource;
		#endregion

		#region Constructor
		public ContactManagementBusinessLogic(IContactManagementDataSource contactManagementDataSource)
		{
			_dataSource = contactManagementDataSource;
		}

		public ContactManagementBusinessLogic(IStandardizationService addressStandardizationService, IContactManagementDataSource contactManagementDataSource)
		{
			_standardizationService = addressStandardizationService;
			_dataSource = contactManagementDataSource;
		}
		#endregion

		public int SaveInternetHandle(int contactId, InternetHandle handle)
		{
			return _dataSource.SaveInternetHandle(contactId, handle);
		}

		public int SavePhone(int contactId, Phone phone)
		{
			Phone standardPhone = _standardizationService.StandardizePhone(phone);
			return _dataSource.SavePhone(contactId, standardPhone);
		}

		public int SaveAddress(int contactId, Address address)
		{
			Address standardAddress = _standardizationService.StandardizeAddress(address);

			#region Simulate the need for overriding Equals on Address

			BinaryFormatter formatter = new BinaryFormatter();
			MemoryStream stream = new MemoryStream();
			formatter.Serialize(stream, standardAddress);
			stream.Position = 0;

			//this is now NOT the same object as was returned by the mock
			standardAddress = (Address) formatter.Deserialize(stream);

			#endregion

			return _dataSource.SaveAddress(contactId, standardAddress);
		}

		public int SaveContact(Contact contact)
		{
			int saveCounter = 0;

			using (TransactionScope ts = new TransactionScope())
			{
				SaveContactResult saveContactResult = _dataSource.SaveContact(contact);

				if (saveContactResult.RecordsAffected != 1)
				{
					throw new InvalidOperationException("An update to a contact returned an invalid result.");
				}
				saveCounter += saveContactResult.RecordsAffected;

				for (int i = 0; i < contact.PhoneNumbers.Count; i++)
				{
					saveCounter += SavePhone(saveContactResult.ContactId, contact.PhoneNumbers[i]);
				}

				for (int i = 0; i < contact.InternetHandles.Count; i++)
				{
					saveCounter += SaveInternetHandle(saveContactResult.ContactId, contact.InternetHandles[i]);
				}

				for (int i = 0; i < contact.Addresses.Count; i++)
				{
					saveCounter += SaveAddress(saveContactResult.ContactId, contact.Addresses[i]);
				}

				ts.Complete();
			}

			return saveCounter;
		}

		public Contact GetContact(int contactId)
		{
			return _dataSource.GetContact(contactId);
		}



		public List<Contact> SearchByLastName(string lastName, int resultsPerPage, int pageNumber)
		{
			return _dataSource.SearchByLastName(lastName, resultsPerPage, pageNumber);
		}

		public List<Employee> SearchByStartDate(DateTime startDate, int resultsPerPage, int pageNumber)
		{
			return _dataSource.SearchByStartDate(startDate, resultsPerPage, pageNumber);
		}

	}
}
