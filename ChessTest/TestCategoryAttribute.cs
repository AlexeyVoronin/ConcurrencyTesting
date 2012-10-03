
using System;

namespace ChessTest
{
	/// <summary>
	/// Description of TestCategoryAttribute.
	/// </summary>
	public class TestCategoryAttribute : Attribute
	{
		public TestCategoryAttribute()
		{
		}
		
		public string Category { get; set; }
	}
}
