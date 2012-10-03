﻿using System;
using System.Threading;

namespace ChessTest
{
    public class ChessTest
    {
    	
    	static public void Main()
    	{
    		try
    		{
    			
    			Run();
    		}
    		catch (Exception ex)
    		{
    			Console.WriteLine(ex);
    		}
    		
    		Console.ReadKey();
    	}
        static public bool Run()
        {
        	Console.WriteLine(MSyncVarOp.CHOICE);
            new MultiThreadTest().Test1();
            return true;
        }
    }
}