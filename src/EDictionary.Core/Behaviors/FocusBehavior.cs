using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EDictionary.Core.Behaviors
{
	public static class FocusBehavior
	{
		public static bool GetIsFocused(DependencyObject obj)
		{
			return (bool)obj.GetValue(IsFocusedProperty);
		}

		public static void SetIsFocused(DependencyObject obj, bool value)
		{
			obj.SetValue(IsFocusedProperty, value);
		}

		public static readonly DependencyProperty IsFocusedProperty = DependencyProperty.RegisterAttached(
			"IsFocused", typeof(bool),
			typeof(FocusBehavior),
			new UIPropertyMetadata(false, null, OnCoerceValue));

		private static object OnCoerceValue(DependencyObject d, object baseValue)
		{
			if ((bool)baseValue)
			{
				var uie = (UIElement)d;
				var action = new Action(() => uie.Dispatcher.BeginInvoke(
					(Action)(() =>
					{
						uie.Focus();

						var textBox = uie as TextBox;

						if (textBox != null)
							textBox.CaretIndex = textBox.Text.Length;
					})));

				Task.Factory.StartNew(action);
			}
			else if (((UIElement)d).IsFocused)
			{
				Keyboard.ClearFocus();
			}

			return ((bool)baseValue);
		}
	}
}
