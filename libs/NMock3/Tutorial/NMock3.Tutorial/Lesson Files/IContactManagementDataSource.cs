using System;
using System.Collections.Generic;

namespace NMock3.Tutorial
{
	public interface IContactManagementDataSource
	{
		SaveContactResult SaveContact(Contact contact);
		int SaveAddress(int contactId, Address address);
		int SavePhone(int contactId, Phone phone);
		int SaveInternetHandle(int contactId, InternetHandle handle);
		Contact GetContact(int contactId);

		/// <summary>
		/// Call this method to get back a list of search results
		/// </summary>
		/// <param name="lastName">The last name of the person we are searching for</param>
		/// <param name="resultsPerPage">The number of items to return in the results</param>
		/// <param name="pageNumber">The page number the user is on</param>
		/// <returns></returns>
		List<Contact> SearchByLastName(string lastName, int resultsPerPage, int pageNumber);


		/// <summary>
		/// Call this method to get back a list of search results
		/// </summary>
		/// <param name="startDate">The start date of the person we are searching for</param>
		/// <param name="resultsPerPage">The number of items to return in the results</param>
		/// <param name="pageNumber">The page number the user is on</param>
		/// <returns></returns>
		List<Employee> SearchByStartDate(DateTime startDate, int resultsPerPage, int pageNumber);

	}

	public struct SaveContactResult
	{
		public int ContactId;
		public int RecordsAffected;
	}
}
