namespace EDictionary.Core.ViewModels
{
	public class SettingsViewModelBase : ViewModelBase
	{
		public delegate void SettingsChangedHandler();

		public event SettingsChangedHandler SettingsChanged;

		protected virtual void OnSettingsChanged()
		{
			SettingsChanged?.Invoke();
		}
	}
}
