using System.Windows.Input;

namespace Xamarin.Forms
{
	public static class ICommandExtensions
	{
		/// <summary>
		///     Calls a command checking CanExecute method
		/// </summary>
		public static void Run(this ICommand command)
		{
			if (command != null && command.CanExecute(null))
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
			if (command != null && command.CanExecute(parameter))
			{
				command.Execute(parameter);
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
		public static void Run<T>(this ICommand command, T parameter)
		{
			if (command != null && command.CanExecute(parameter))
			{
				command.Execute(parameter);
			}
		}
	}
}