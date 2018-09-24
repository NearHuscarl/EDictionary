using EDictionary.Controls;
using EDictionary.Core.ViewModels;

namespace EDictionary.Core.Views
{
	/// <summary>
	/// Interaction logic for AboutWindow.xaml
	/// </summary>
	public partial class AboutWindow : ExtendedWindow
	{
		private AboutViewModel viewModel;

		public AboutWindow()
		{
			InitializeComponent();

			viewModel = new AboutViewModel();

			DataContext = viewModel;
		}
	}
}
