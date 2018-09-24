using EDictionary.Core.DataLogic;
using EDictionary.Core.Learner.Extensions;
using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using EDictionary.Core.ViewModels;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace EDictionary.Core.Learner.ViewModels
{
	public enum Status
	{
		// Stop spawning timer
		Stop,

		// Stop active timer
		Pause,

		// Else
		Run,
	}

	public partial class TaskIconViewModel : ViewModelBase, ITaskIconViewModel
	{
		#region Fields

		private SettingsLogic settingsLogic;
		private HistoryLogic historyLogic;
		private WordLogic wordLogic;

		private Status prevStatus;
		private Status currentStatus;
		private Status nextStatus;

		private string statusIcon;

		#endregion

		#region Properties

		public Status CurrentStatus
		{
			get { return currentStatus; }

			private set
			{
				if (prevStatus != CurrentStatus)
					prevStatus = CurrentStatus;

				if (value == Status.Run)
					NextStatus = Status.Stop;
				if (value == Status.Stop)
					NextStatus = Status.Run;

				SetProperty(ref currentStatus, value);
			}
		}

		public Status NextStatus
		{
			get { return nextStatus; }

			private set
			{
				if (value == Status.Run)
					StatusIcon = "ToggleOnIcon";
				else if (value == Status.Stop)
					StatusIcon = "ToggleOffIcon";

				SetPropertyAndNotify(ref nextStatus, value);
			}
		}

		public string StatusIcon
		{
			get { return statusIcon; }
			set { SetPropertyAndNotify(ref statusIcon, value); }
		}

		#endregion

		#region Actions

		public Action ShowSettingsWindowAction { get; set; }
		public Action ShowAboutWindowAction { get; set; }
		public Action ShowLearnerBalloonAction { get; set; }
		public Action HideLearnerBalloonAction { get; set; }
		public Action ShowDynamicPopupAction { get; set; }

		public void OpenMainDictionary() => Process.Start(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EDictionary.exe"));
		public void OpenSettings()
		{
			ShowSettingsWindowAction.Invoke();
			ReloadSettings();
		}
		public void OpenAbout() => ShowAboutWindowAction.Invoke();
		public void CloseApp() => System.Windows.Application.Current.Shutdown();
		public void OpenLearnerBalloon() => DispatchIfNecessary(ShowLearnerBalloonAction);
		public void CloseLearnerBalloon() => DispatchIfNecessary(HideLearnerBalloonAction);
		public void OpenDynamicPopup() => DispatchIfNecessary(ShowDynamicPopupAction);

		#endregion

		#region Constructor

		public TaskIconViewModel()
		{
			settingsLogic = new SettingsLogic();
			historyLogic = new HistoryLogic();
			wordLogic = new WordLogic();

			InitLearner();
			InitDynamic();

			LoadCommands();
		}

		private void LoadCommands()
		{
			OpenMainDictionaryCommand = new DelegateCommand(OpenMainDictionary);
			ToggleActiveCommand = new DelegateCommand(ToggleActive);
			OpenSettingsCommand = new DelegateCommand(OpenSettings);
			OpenAboutCommand = new DelegateCommand(OpenAbout);
			ExitAppCommand = new DelegateCommand(CloseApp);

			OpenLearnerBalloonCommand = new DelegateCommand(OpenLearnerBalloon);
			CloseLearnerBalloonCommand = new DelegateCommand(CloseLearnerBalloon);

			PauseCommand = new DelegateCommand(() => CurrentStatus = Status.Pause);
			ContinueCommand = new DelegateCommand(() => CurrentStatus = prevStatus);
		}

		#endregion

		#region Commands

		public DelegateCommand OpenMainDictionaryCommand { get; private set; }
		public DelegateCommand ToggleActiveCommand { get; private set; }
		public DelegateCommand OpenSettingsCommand { get; private set; }
		public DelegateCommand OpenAboutCommand { get; private set; }
		public DelegateCommand ExitAppCommand { get; private set; }
		public DelegateCommand OpenLearnerBalloonCommand { get; private set; }
		public DelegateCommand CloseLearnerBalloonCommand { get; private set; }
		public DelegateCommand PauseCommand { get; private set; }
		public DelegateCommand ContinueCommand { get; private set; }

		#endregion

		private void ReloadSettings()
		{
			Settings settings = settingsLogic.LoadSettings();

			CurrentStatus = settings.IsLearnerEnabled ? Status.Run : Status.Stop;

			spawnInterval = TimeSpan.FromMinutes(settings.MinInterval);
			spawnInterval += TimeSpan.FromSeconds(settings.SecInterval);
			activeInterval = TimeSpan.FromSeconds(settings.Timeout);

			spawnCounter = (int)spawnInterval.TotalSeconds;
			activeCounter = (int)activeInterval.TotalSeconds;

			useCustomWordlist = settings.UseCustomWordlist;
			useHistoryWordlist = settings.UseHistoryWordlist;

			option = settings.VocabularySource;

			if (option == VocabularySource.Full)
			{
				wordList = wordLogic.WordList;
			}
			else if (option == VocabularySource.Custom)
			{
				wordList = settings.CustomWordList
					.Where(word => wordLogic.NameToIDs.ContainsKey(word))
					.ToList();

				if (useHistoryWordlist)
				{
					historyWordlist = historyLogic.GetCollection<string>().Distinct().ToList();
				}
			}

			if (settings.IsDynamicEnabled)
				EnableDynamic();
			else
				DisableDynamic();

			autoCopyToClipboard = settings.AutoCopyToClipboard;
			useTriggerKey = settings.UseTriggerKey;
			triggerKey = settings.TriggerKey.ToKey();
		}
	}
}
