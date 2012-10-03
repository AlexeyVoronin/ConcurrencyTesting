using System;
using Castle;
using Castle.Core;
using NMock;
using NUnit.Framework;

namespace ChessTest
{
	[TestFixture]
	[TestCategory(Category = "Integration")]
	public class MemoryStorageTests
	{
		[Test]
		public void AddTest()
		{
			var memoryStorage = new MemoryStorage();
			
			var category = new Category() { Name = "test" };
			
			memoryStorage.Add(category);
			
			var storageCategory = memoryStorage.Get(category.Name);
			
			Assert.IsNotNull(storageCategory);
			Assert.AreEqual(category.Name, storageCategory.Name);
		}
	}
}