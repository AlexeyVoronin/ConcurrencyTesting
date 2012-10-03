#region Using
#if NUNIT
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestContext = System.Object;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Category = Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute;
#endif

using System;
using NMock;
#endregion

namespace NMock3.Tutorial.FAQ
{
	public class ExamplesBase
	{
		protected MockFactory Factory;

		[TestInitialize]
		public virtual void TestInit()
		{
			Factory = new MockFactory();
		}

		[TestCleanup]
		public virtual void TestCleanup()
		{
			Factory.VerifyAllExpectationsHaveBeenMet();
		}

	}
}
