using System.Windows.Input;

namespace Xamarin.Forms.Core.Internals
{
	public static class CommandExtensions
	{
		/// <summary>
		///     Calls a command checking CanExecute method
		/// </summary>
		public static void Run(this ICommand command)
		{
			if (command.CanExecute(null))
			{
				command.Execute(null);
			}
		}

		/// <summary>
		///     Calls a command checking CanExecute method
		/// </summary>
		/// <param name="command">
		///     ICommand
		/// </param>
		/// <param name="parameter">
		///     Data used by the command. If the command does not require data to be passed, this object can be
		///     set to null.
		/// </param>
		public static void Run(this ICommand command, object parameter)
		{
			if (command.CanExecute(parameter))
			{
				command.Execute(parameter);
			}
		}
	}
}