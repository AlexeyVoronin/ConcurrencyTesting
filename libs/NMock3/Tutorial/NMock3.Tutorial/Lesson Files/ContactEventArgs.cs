using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NMock3.Tutorial
{
	public class ContactEventArgs : EventArgs
	{
		public int ContactId { get; set; }

		public ContactEventArgs(int contactId)
		{
			ContactId = contactId;
		}
	}
}
