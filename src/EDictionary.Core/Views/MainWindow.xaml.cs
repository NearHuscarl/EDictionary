using EDictionary.Controls;
using EDictionary.Core.ViewModels;
using System;

namespace EDictionary.Core.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : ExtendedWindow
	{
		private MainViewModel viewModel;

		public MainWindow()
		{
			InitializeComponent();

			viewModel = new MainViewModel
			{
				ShowSettingsWindowAction = new Action(this.ShowSettingsWindow),
				ShowAboutWindowAction = new Action(this.ShowAboutWindow),
			};

			DataContext = viewModel;
		}

		private void ShowSettingsWindow()
		{
			var settingsWindow = new SettingsWindow();

			// Make child window always on top of this window but not all other windows
			settingsWindow.Owner = this;
			settingsWindow.ShowDialog();
		}

		private void ShowAboutWindow()
		{
			var aboutWindow = new AboutWindow();

			aboutWindow.Owner = this;
			aboutWindow.ShowDialog();
		}
	}
}
