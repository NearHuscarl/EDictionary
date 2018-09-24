using EDictionary.Core.Utilities;
using EDictionary.Core.ViewModels.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace EDictionary.Core.ViewModels
{
	public class DynamicSettingsViewModel : SettingsViewModelBase, IDynamicSettingsViewModel
	{
		private bool canEditTriggerKey;
		private bool autoCopyToClipboard;
		private bool useTriggerKey;
		private List<string> triggerKeys;
		private string selectedKey;

		public bool CanEditTriggerKey
		{
			get { return canEditTriggerKey; }
			set
			{
				SetPropertyAndNotify(ref canEditTriggerKey, value);
				OnSettingsChanged();
			}
		}

		public bool AutoCopyToClipboard
		{
			get { return autoCopyToClipboard; }
			set
			{
				SetPropertyAndNotify(ref autoCopyToClipboard, value);
				OnSettingsChanged();
			}
		}

		public bool UseTriggerKey
		{
			get { return useTriggerKey; }
			set
			{
				if (value)
					CanEditTriggerKey = true;
				else
					CanEditTriggerKey = false;

				SetPropertyAndNotify(ref useTriggerKey, value);
				OnSettingsChanged();
			}
		}

		public List<string> TriggerKeys
		{
			get { return triggerKeys; }
		}

		public string SelectedKey
		{
			get { return selectedKey; }
			set
			{
				SetPropertyAndNotify(ref selectedKey, value);
				OnSettingsChanged();
			}
		}

		public DynamicSettingsViewModel()
		{
			triggerKeys = new List<string>()
			{
				"LControl",
				"RControl",
				"LAlt",
				"RAlt",
				"LShift",
				"RShift",
			};
		}
	}
}
