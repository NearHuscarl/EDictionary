using EDictionary.Core.Models;
using System.Collections.Generic;

namespace EDictionary.Core.ViewModels.Interfaces
{
	public interface ILearnerSettingsViewModel
	{
		bool CanEditCustomOptions { get; set; }

		int MinInterval { get; set; }
		int SecInterval { get; set; }

		int Timeout { get; set; }

		VocabularySource Option { get; set; }
		List<string> CustomWordList { get; set; }

		bool UseHistoryWordlist { get; set; }
		bool UseCustomWordlist { get; set; }
	}
}
