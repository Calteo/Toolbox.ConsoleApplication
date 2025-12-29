using Toolbox.ConsoleApplication;

namespace Toolbox.ConsoleApplication.Demo
{
	internal class Program(string[] args) : ConsoleApplication(args)
	{
		static int Main(string[] args)
		{
			return new Program(args)
				// using a handler with interface implementation
				// .AddOption<DemoOptions, DemoHandlerWithInterface>()

				// using a handler with default implementation
				.AddHandler<DemoHandler>()
				.Run();
		}
	}
}
