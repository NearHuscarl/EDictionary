namespace EDictionary.Core.ViewModels.Interfaces
{
	public interface IGeneralSettingsViewModel
	{
		bool RunAtStartup { get; set; }
		bool IsLearnerEnabled { get; set; }
		bool IsDynamicEnabled { get; set; }
	}
}
