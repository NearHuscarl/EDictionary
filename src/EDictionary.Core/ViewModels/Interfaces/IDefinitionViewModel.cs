using EDictionary.Core.Utilities;

namespace EDictionary.Core.ViewModels.Interfaces
{
	public interface IDefinitionViewModel
	{
		bool HeaderVisibility { get; set; }
		bool ResetScrollViewer { get; set; }

		DelegateCommand PlayBrEAudioCommand { get; }
		DelegateCommand PlayNAmEAudioCommand { get; }
	}
}
