using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Toolbox.CommandLine;

namespace Toolbox.ConsoleApplication
{
    public class ConsoleApplication
	{
		public ConsoleApplication(string[] args)
		{
			Arguments = args;
			Builder = Host.CreateDefaultBuilder(Arguments);
		}

		public int Run()
        {
			try
			{
				Console.WriteLine(ConsoleColor.Green, "ConsoleApplication is running...");

				var parser = new Parser([.. OptionsHandlers.Keys]);
				var result = parser.Parse(Arguments);

				var rc = result
					.OnError(OnArgumentError)
					.OnHelp(OnArgumentHelp)
					.On(OnRunHandler)
					.Return;

				Console.WriteLine(ConsoleColor.Green, $"ConsoleApplication is done. (rc={rc})");

				return rc;
			}
			catch (Exception exception)
			{
				if (exception is TargetInvocationException && exception.InnerException != null)
				{
					exception = exception.InnerException;
				}

				Console.WriteLine(ConsoleColor.Red, "An unhandled exception occurred:");
				Console.WriteLine(ConsoleColor.Red, exception.ToString());

				return 3;
			}			
		}

		private Dictionary<Type, Type> OptionsHandlers { get; } = [];
		public IHostBuilder Builder { get; }
		public string[] Arguments { get; }

		public ConsoleApplication AddOption<TOptions, THandler>()
			where THandler : IConsoleHandler<TOptions>
			where TOptions : class
		{
			OptionsHandlers.Add(typeof(TOptions), typeof(THandler));
			return this;
		}

		public ConsoleApplication AddHandler<T>() where T : IConsoleHandler
		{
			var handlerType = typeof(T);
			var optionsType = handlerType
				.GetInterfaces()
				.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IConsoleHandler<>))
				.Select(i => i.GetGenericArguments()[0])
				.FirstOrDefault()
				?? throw new InvalidOperationException($"Handler type {handlerType.Name} does not implement IConsoleHandler<TOptions> interface.");

			OptionsHandlers.Add(optionsType, handlerType);

			return this;
		}

		protected virtual int OnArgumentError(ParseResult result)
		{
			Console.WriteLine(ConsoleColor.Red, "An error occurred while parsing the command line arguments.");
			Console.WriteLine(ConsoleColor.Red, result.Text);

			return 2;
		}

		protected int OnArgumentHelp(ParseResult result)
		{
			Console.WriteLine(ConsoleColor.Yellow, result.GetHelpText());
			return 1;
		}

		private int OnRunHandler(object options)
		{
			var optionsType = options.GetType();
			
			if (OptionsHandlers.TryGetValue(optionsType, out var handlerType))
			{
				var host = Builder.ConfigureServices((context, services) =>
				{
					services.AddSingleton(optionsType, options);
					services.AddSingleton(handlerType);

					if (options is IServiceConfigurator configurator)
					{
						configurator.ConfigureServices(context, services);
					}
				})					
				// .UseSerilog()
				.Build();

				var handler = (IConsoleHandler)host.Services.GetRequiredService(handlerType);

				return handler.Execute();
			}
			else
			{
				Console.WriteLine(ConsoleColor.Red, $"No handler found for options of type {optionsType.Name}.");
				return 3;
			}
		}

	}
}