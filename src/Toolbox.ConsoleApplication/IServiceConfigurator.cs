using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Toolbox.ConsoleApplication
{
	public  interface IServiceConfigurator
	{
		void ConfigureServices(HostBuilderContext context, IServiceCollection services);
	}
}
