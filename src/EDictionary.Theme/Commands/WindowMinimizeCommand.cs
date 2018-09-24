using EDictionary.Theme.Animations;
using System;
using System.Windows;
using System.Windows.Input;

namespace EDictionary.Theme.Commands
{
	public class WindowMinimizeCommand : ICommand
	{

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public event EventHandler CanExecuteChanged;

		public void Execute(object parameter)
		{
			var window = parameter as Window;
			var openingAnimation = WindowAnimations.FadeOutAnimation;

			if (window != null)
			{
				// Fade out then minimize the window
				openingAnimation.Completed += (o, a) => window.WindowState = WindowState.Minimized;
				window.BeginAnimation(UIElement.OpacityProperty, openingAnimation);
			}
		}
	}
}
