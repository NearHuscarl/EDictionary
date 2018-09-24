using System.Collections.Generic;

namespace EDictionary.Core.ViewModels.Interfaces
{
	public interface IDynamicSettingsViewModel
	{
		bool CanEditTriggerKey { get; set; }

		bool AutoCopyToClipboard { get; set; }
		bool UseTriggerKey { get; set; }
		List<string> TriggerKeys { get; }
		string SelectedKey { get; set; }
	}
}
