using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Toolbox.ConsoleApplication.Demo
{
	internal class DemoHandlerWithInterface : IConsoleHandler<DemoOptions>
	{
		public DemoHandlerWithInterface(DemoOptions options, DemoService demoService)
		{
			Options = options;
			DemoService = demoService;
		}

		public DemoOptions Options { get; }
		public DemoService DemoService { get; }


		public int Execute()
		{
			Console.WriteLine(ConsoleColor.Yellow, $"{GetType().Name} executing");
			
			for (int i = 0; i < Options.Count; i++)
			{
				DemoService.CallService(i + 1);
			}

			Console.WriteLine(ConsoleColor.Yellow, $"{GetType().Name} done");

			return 0;
		}
	}
}
