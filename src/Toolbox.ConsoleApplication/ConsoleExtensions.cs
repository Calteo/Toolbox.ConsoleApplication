using System;
using System.Collections.Generic;
using System.Text;

namespace Toolbox.ConsoleApplication
{
	public static class ConsoleExtensions 
	{
		extension(Console)
		{
			public static void WriteLine(ConsoleColor color, string message)
			{
				var previousColor = Console.ForegroundColor;
				Console.ForegroundColor = color;
				Console.WriteLine(message);
				Console.ForegroundColor = previousColor;
			}
		}
	}
}
