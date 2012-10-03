using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace NMock3.Tutorial
{
	[Serializable]
	public struct Name
	{
		public string Title { get; set; }
		public string First { get; set; }
		public string Middle { get; set; }
		public string Last { get; set; }
		public NameSuffix Suffix { get; set; }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			
			if(!string.IsNullOrEmpty(Title))
			{
				sb.Append(Title);
				sb.Append(" ");
			}

			sb.Append(First);
			sb.Append(" ");

			if(!string.IsNullOrEmpty(Middle))
			{
				sb.Append(Middle);
				sb.Append(" ");
			}

			sb.Append(Last);

			if(Suffix != NameSuffix.None)
			{
				sb.Append(" ");
				sb.Append(Suffix.ToString());
			}

			return sb.ToString();
		}
	}

	[Serializable]
	public enum NameSuffix
	{
		None = 0,
		Jr = 1,
		Sr = 2,
		III = 3,
		IV = 4,
		V = 5,
	}

	[Serializable]
	public class Address : IComparable
	{
		public Address()
		{}

		public Address(int id, string line1, string line2, string city, string state, string zip, string country, AddressType addressType)
		{
			Id = id;
			Line1 = line1;
			Line2 = line2;
			City = city;
			State = state;
			Zip = zip;
			Country = country;
			AddressType = addressType;
		}

		public int Id { get; set; }
		public string Line1 { get; set; }
		public string Line2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
		public string Country{ get; set;}
		public AddressType AddressType { get; set; }

		public string AddressLabel
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				sb.Append(Line1);
				sb.Append(Environment.NewLine);

				if(!string.IsNullOrEmpty(Line2))
				{
					sb.Append(Line2);
					sb.Append(Environment.NewLine);
				}

				sb.Append(City);
				sb.Append(", ");
				sb.Append(State);
				sb.Append("  ");
				sb.Append(Zip);
				sb.Append(" ");
				sb.Append(Country);

				return sb.ToString();
			}
		}

		public override string ToString()
		{
			return "Address: " + Id;
		}

		///<summary>
		///Compares the current instance with another object of the same type.
		///</summary>
		///
		///<returns>
		///A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance is less than obj. Zero This instance is equal to obj. Greater than zero This instance is greater than obj. 
		///</returns>
		///
		///<param name="obj">An object to compare with this instance. </param>
		///<exception cref="T:System.ArgumentException">obj is not the same type as this instance. </exception><filterpriority>2</filterpriority>
		public int CompareTo(object obj)
		{
			Address address = obj as Address;

			if(address == null)
				throw new ArgumentException("The object being compared is not an Address.", "obj");

			if(
				this.Id == address.Id
				&& this.Line1 == address.Line1
				&& this.Line2 == address.Line2
				&& this.City == address.City
				&& this.State == address.State
				&& this.Zip == address.Zip
				&& this.Country == address.Country
				&& this.AddressType == address.AddressType
				)
			{
				return 0;
			}

			return 1;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;
			else
			{
				return CompareTo(obj) == 0;
			}
		}

		public static bool operator ==(Address obj1, Address obj2)
		{
			return (object)obj1 == null ? (object)obj2 == null : obj1.Equals(obj2);
		}

		public static bool operator !=(Address obj1, Address obj2)
		{
			return (object)obj1 == null ? (object)obj2 != null : !obj1.Equals(obj2);
		}

		public static bool operator >(Address obj1, Address obj2)
		{
			throw new InvalidOperationException();
		}

		public static bool operator <(Address obj1, Address obj2)
		{
			throw new InvalidOperationException();
		}

		public override int GetHashCode()
		{
			return Id;
		}
	}

	[Serializable]
	public enum AddressType
	{
		None = 0,
		Work = 1, 
		Home = 2,
		Other = 3,
	}

	[Serializable]
	public class Phone
	{
		public Phone()
		{}

		public Phone(int id, string countryCode, string areaCode, string prefix, string number, string extension, PhoneType phoneType)
		{
			Id = id;
			CountryCode = countryCode;
			AreaCode = areaCode;
			Prefix = prefix;
			Number = number;
			Extension = extension;
			PhoneType = phoneType;
		}

		public int Id { get; set;}
		public string CountryCode { get; set; }
		public string AreaCode{ get; set;}
		public string Prefix { get; set; }
		public string Number { get; set; }
		public string Extension { get; set; }
		public PhoneType PhoneType { get; set; }
	}

	[Serializable]
	public enum PhoneType
	{
		None = 0,
		WorkDirect = 1,
		WorkMain = 2,
		WorkFax = 3,
		Home = 4,
		Mobile = 5,
	}

	[Serializable]
	public class InternetHandle
	{
		public InternetHandle()
		{}

		public InternetHandle(int id, string address, string displayAs, HandleType handleType)
		{
			Id = id;
			Address = address;
			DisplayAs = displayAs;
			HandleType = handleType;
		}

		public int Id { get; set; }
		public string Address { get; set; }
		public string DisplayAs { get; set; }
		public HandleType HandleType { get; set; }
	}

	[Serializable]
	public enum HandleType
	{
		None = 0,
		WorkEmail = 1,
		PersonalEmail = 2,
		IM = 3,
		Website = 4,
		Blog = 5,
		Twitter = 6,
	}

	[Serializable]
	public class Contact
	{
		public Contact()
		{
			Addresses = new List<Address>();
			PhoneNumbers = new List<Phone>();
			InternetHandles = new List<InternetHandle>();
		}

		public Contact(int id) : this()
		{
			Id = id;
		}

		public int Id { get; set; }
		public Name Name { get; set; }
		public ContactType ContactType { get; set; }

		public List<Address> Addresses { get; private set; }
		public List<Phone> PhoneNumbers { get; private set; }
		public List<InternetHandle> InternetHandles { get; private set;}
	}

	[Serializable]
	public enum ContactType
	{
		None = 0,
		Employee = 1,
		Partner = 2,
		Customer = 3,
		Vendor = 4,
	}

	[Serializable]
	public class Customer : Contact
	{
		public string Company { get; set; }
		public string Title { get; set; }
	}

	[Serializable]
	public class Employee : Contact
	{
		public DateTime StartDate { get; set; }
		public int ReportsToId { get; set; }
	}

	/// <summary>
	/// This class represents a pair of related values.  One is 
	/// typically the ID of the pair and one is the string value
	/// of the pair.
	/// </summary>
	/// <typeparam name="T">Typically an Enum or Int that is the ID of the Name value.</typeparam>
	[Serializable]
	public class Code<T>
	{
		private T idVal;
		private string nameVal;

		public Code()
		{
		}

		public Code(string name, T id)
		{
			nameVal = name;
			idVal = id;
		}

		/// <summary>
		/// The string representation of the Value
		/// </summary>
		[XmlElement(IsNullable = true)]
		public string Name
		{
			get { return nameVal; }
			set { nameVal = value; }
		}

		/// <summary>
		/// The Enum or Int or DB ID of the Value
		/// </summary>
		[XmlElement]
		public T Id
		{
			get { return idVal; }
			set { idVal = value; }
		}
	}

	[Serializable]
	public class Note
	{
		public int Id { get; set; }
		public DateTime Date{ get; set;}
		public string Text{ get; set;}
	}
}