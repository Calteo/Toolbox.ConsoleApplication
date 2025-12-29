using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Toolbox.ConsoleApplication
{
	public interface IConsoleHandler 
	{
		int Execute();
	}

	public interface IConsoleHandler<TOptions> : IConsoleHandler where TOptions : class
	{
		protected TOptions Options { get; }
	}
}
