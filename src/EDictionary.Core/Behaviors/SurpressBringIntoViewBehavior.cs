using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace EDictionary.Core.Behaviors
{
	/// <summary>
	/// This control will be used inside a ScrollViewer so we need to disable
	/// BringIntoView event to prevent the ScrollViewer from automatically scrolling
	/// to perceived content
	/// https://stackoverflow.com/a/8387765/9449426
	/// </summary>
	public sealed class SurpressBringIntoViewBehavior : Behavior<FrameworkElement>
	{
		protected override void OnAttached()
		{
			base.OnAttached();
			AssociatedObject.RequestBringIntoView += SupressBringIntoView;
		}

		protected override void OnDetaching()
		{
			AssociatedObject.RequestBringIntoView -= SupressBringIntoView;
			base.OnDetaching();
		}

		private void SupressBringIntoView(object sender, RequestBringIntoViewEventArgs e)
		{
			e.Handled = true;
		}
	}
}
