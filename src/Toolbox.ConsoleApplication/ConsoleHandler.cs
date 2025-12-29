namespace Toolbox.ConsoleApplication
{
	public abstract class ConsoleHandler<TO> : IConsoleHandler<TO> where TO : class
	{
		#region IConsoleHandler<TO>
		protected ConsoleHandler(TO options)
		{
			Options = options;
		}

		public TO Options { get; }

		public abstract int Execute();

		#endregion
	}
}
