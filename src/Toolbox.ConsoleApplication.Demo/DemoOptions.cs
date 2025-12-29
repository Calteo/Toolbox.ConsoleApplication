using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Toolbox.CommandLine;

namespace Toolbox.ConsoleApplication.Demo
{
	internal class DemoOptions : IServiceConfigurator
	{
		[Option("count"), Position(0), DefaultValue(5), Description("number of times to greet.")]
		public int Count { get; set; }

		public void ConfigureServices(HostBuilderContext context, IServiceCollection services)
		{
			services.AddTransient<DemoService>();
		}
	}
}
