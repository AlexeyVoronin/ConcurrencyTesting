
using System;

namespace ChessTest
{
	/// <summary>
	/// Description of CategoryFacade.
	/// </summary>
	public class CategoryFacade
	{
		private readonly IStorage _storage;
		
		public CategoryFacade(IStorage storage)
		{
			_storage = storage;
		}
		
		public void CreateCategory(Category category)
		{
			_storage.Add(category);
		}
	}
}
