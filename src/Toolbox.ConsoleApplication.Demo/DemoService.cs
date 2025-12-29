using System;
using System.Collections.Generic;
using System.Text;

namespace Toolbox.ConsoleApplication.Demo
{
	internal class DemoService
	{
		public DemoService()
		{
		}

		public void CallService(int index)
		{
			Console.WriteLine(ConsoleColor.Magenta, $"#{index}: Hello World!");
		}
	}
}
