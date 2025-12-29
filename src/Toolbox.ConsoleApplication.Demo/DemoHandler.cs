using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Toolbox.ConsoleApplication.Demo
{
	internal class DemoHandler : ConsoleHandler<DemoOptions>
	{
		public DemoHandler(DemoOptions options, DemoService demoService) : base(options)
		{
			DemoService = demoService;
		}

		public DemoService DemoService { get; }

		public override int Execute()
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
