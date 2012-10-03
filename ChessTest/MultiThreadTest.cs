﻿using System;
using System.Collections.Generic;
using System.Threading;

using Microsoft.Concurrency.TestTools.UnitTesting;
using Microsoft.Concurrency.TestTools.UnitTesting.Chess;

namespace ChessTest
{
    [ContextsProperty("StandardChessContexts")]
    public class MultiThreadTest
    {
        public static IEnumerable<ITestContext> StandardChessContexts
        {
            get
            {
                yield return new ChessTestContext("csb1", null, null, new MChessOptions { MaxPreemptions = 1 });
            }
        }
        
        [ChessTestMethod]
        [ExpectedChessResult("csb1", ChessExitCode.Success)]
        public void Test1()
        {
        	Assert.AreEqual(true, true);
        }
    }
}