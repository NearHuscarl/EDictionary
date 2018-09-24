using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace EDictionary.Core.Behaviors
{
	/// <summary>
	/// Child elements of scrollviewer preventing scrolling with mouse wheel
	/// https://stackoverflow.com/a/16110178/9449426
	/// </summary>
	public sealed class IgnoreMouseWheelBehavior : Behavior<UIElement>
	{
		protected override void OnAttached()
		{
			base.OnAttached();
			AssociatedObject.PreviewMouseWheel += AssociatedObject_PreviewMouseWheel;
		}

		protected override void OnDetaching()
		{
			AssociatedObject.PreviewMouseWheel -= AssociatedObject_PreviewMouseWheel;
			base.OnDetaching();
		}

		void AssociatedObject_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
		{
			e.Handled = true;

			var e2 = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
			{
				RoutedEvent = UIElement.MouseWheelEvent
			};

			AssociatedObject.RaiseEvent(e2);
		}
	}
}
