using EDictionary.Core.Data;
using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using System.Linq;
using System.Threading.Tasks;

namespace EDictionary.Core.DataLogic
{
	public class SettingsLogic
   {
		private SettingsAccess settingsAccess;

		public SettingsLogic()
		{
			settingsAccess = new SettingsAccess();
		}

		public void SaveSettings(Settings settings)
		{
			settings.CustomWordList = settings.CustomWordList
				.Select(word => word.ToLower())
				.Where(word => word != "")
				.Distinct()
				.ToList();

			settingsAccess.SaveSettings(settings);
		}

		public Settings LoadSettings()
		{
			var result = settingsAccess.LoadSettings();

			if (result.Status == Status.Success)
			{
				return result.Data;
			}

			return Settings.Default;
		}

		public void AddToWordlist(string word)
		{
			Settings settings = LoadSettings();

			if (!settings.CustomWordList.Contains(word))
				settings.CustomWordList.Add(word);

			SaveSettings(settings);
		}

		public async Task<Settings> LoadSettingsAsync()
		{
			return await Task.Run(() => this.LoadSettings());
		}
	}
}
