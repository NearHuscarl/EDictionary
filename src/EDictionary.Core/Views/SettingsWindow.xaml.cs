using EDictionary.Controls;
using EDictionary.Core.ViewModels;

namespace EDictionary.Core.Views
{
	/// <summary>
	/// Interaction logic for SettingWindow.xaml
	/// </summary>
	public partial class SettingsWindow : ExtendedWindow
	{
		public SettingsWindow()
		{
			InitializeComponent();
			DataContext = new SettingsViewModel();
		}
	}
}
