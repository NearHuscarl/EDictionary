using EDictionary.Core.ViewModels.Interfaces;

namespace EDictionary.Core.ViewModels
{
	public class GeneralSettingsViewModel : SettingsViewModelBase, IGeneralSettingsViewModel
	{
		private bool runAtStartup;
		private bool isLearnerEnabled;
		private bool isDynamicEnabled;

		public bool RunAtStartup
		{
			get { return runAtStartup; }
			set
			{
				SetPropertyAndNotify(ref runAtStartup, value);
				OnSettingsChanged();
			}
		}

		public bool IsLearnerEnabled
		{
			get { return isLearnerEnabled; }
			set
			{
				SetPropertyAndNotify(ref isLearnerEnabled, value);
				OnSettingsChanged();
			}
		}

		public bool IsDynamicEnabled
		{
			get { return isDynamicEnabled; }
			set
			{
				SetPropertyAndNotify(ref isDynamicEnabled, value);
				OnSettingsChanged();
			}
		}
	}
}
