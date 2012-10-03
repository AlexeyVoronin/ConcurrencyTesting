using System;
using System.Collections;
using System.Collections.Generic;

namespace NMock3.Tutorial
{
	public interface IContactManagementView : IViewBase
	{
		event EventHandler<ContactEventArgs> Load;

		int Id { get; }
		Name Name { get; set; }
		ContactType ContactType { get;  set; }
		IEnumerable SuffixDataSource { set; }

		List<Address> Addresses { get; set; }
		List<Phone> PhoneNumbers { get; set; }
		List<InternetHandle> InternetHandles { get; set; }
	}
}
