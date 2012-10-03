using System;
using Castle;
using Castle.Core;
using NMock;
using NUnit.Framework;

namespace ChessTest
{
	[TestFixture]
	public class CategoryTests
	{
		[Test]
		public void CreateCategory()
		{
			var category = new Category() { Name = "Test Category" };	
			MockFactory mockFactory = new MockFactory();
			var storageMock = mockFactory.CreateMock<IStorage>();
			
			storageMock.Expects.One.MethodWith(o => o.Add(category));
			
			var storage = storageMock.MockObject;
					
			var categoryFacade = new CategoryFacade(storage);
			
			categoryFacade.CreateCategory(category);
			
			mockFactory.VerifyAllExpectationsHaveBeenMet();
		}
	}
}