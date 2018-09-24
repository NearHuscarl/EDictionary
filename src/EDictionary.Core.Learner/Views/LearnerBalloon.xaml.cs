using EDictionary.Core.Learner.ViewModels;
using Hardcodet.Wpf.TaskbarNotification;
using System.Windows;
using System.Windows.Controls;

namespace EDictionary.Core.Learner.Views
{
	/// <summary>
	/// Interaction logic for LearnerBaloon.xaml
	/// </summary>
	public partial class LearnerBalloon : UserControl
	{
		public LearnerBalloon()
		{
			InitializeComponent();

			TaskbarIcon.AddBalloonClosingHandler(this, OnBalloonClosing);
		}

		/// <summary>
		/// By subscribing to the <see cref="TaskbarIcon.BalloonClosingEvent"/>
		/// and setting the "Handled" property to true, we suppress the popup
		/// from being closed in order to display the custom fade-out animation.
		/// </summary>
		private void OnBalloonClosing(object sender, RoutedEventArgs e)
		{
			e.Handled = true; //suppresses the popup from being closed immediately
		}
	}
}
