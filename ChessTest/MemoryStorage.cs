
using System;
using System.Collections.Generic;
namespace ChessTest
{
	/// <summary>
	/// Description of MemoryStorage.
	/// </summary>
	public class MemoryStorage : IStorage
	{
		public MemoryStorage()
		{
		}
		
		public void Add(Category category)
		{
			dic[category.Name] = category;
		}
		
		public Category Get(string categoryName)
		{
			return dic[categoryName];
		}
		
		private readonly IDictionary<string, Category> dic = new Dictionary<string, Category>();
	}
}
