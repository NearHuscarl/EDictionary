using EDictionary.Core.DataLogic;
using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using EDictionary.Core.ViewModels.Interfaces;
using System;
using System.Linq;
using System.Text;

namespace EDictionary.Core.ViewModels
{
	public class SettingsViewModel : ViewModelBase, ISettingsViewModel
	{
		private SettingsLogic settingsLogic;

		private GeneralSettingsViewModel generalSettingsVM;
		private LearnerSettingsViewModel learnerSettingsVM;
		private DynamicSettingsViewModel dynamicSettingsVM;

		private bool isClose = false;
		private bool canApply = false;

		public bool IsClose
		{
			get { return isClose; }
			set { SetPropertyAndNotify(ref isClose, value); }
		}

		public bool CanApply
		{
			get { return canApply; }
			set { SetPropertyAndNotify(ref canApply, value); }
		}

		public GeneralSettingsViewModel GeneralSettingsVM
		{
			get { return generalSettingsVM; }
			set { SetPropertyAndNotify(ref generalSettingsVM, value); }
		}

		public LearnerSettingsViewModel LearnerSettingsVM
		{
			get { return learnerSettingsVM; }
			set { SetPropertyAndNotify(ref learnerSettingsVM, value); }

		}

		public DynamicSettingsViewModel DynamicSettingsVM
		{
			get { return dynamicSettingsVM; }
			set { SetPropertyAndNotify(ref dynamicSettingsVM, value); }
		}

		#region Constructor

		public SettingsViewModel()
		{
			InitChildViewModels();

			SaveCommand = new DelegateCommand(SaveSettings);
			ApplyCommand = new DelegateCommand(ApplySettings);
		}

		private async void InitChildViewModels()
		{
			settingsLogic = new SettingsLogic();

			Settings settings = await settingsLogic.LoadSettingsAsync();

			GeneralSettingsVM = new GeneralSettingsViewModel()
			{
				RunAtStartup = settings.RunAtStartup,
				IsLearnerEnabled = settings.IsLearnerEnabled,
				IsDynamicEnabled = settings.IsDynamicEnabled,
			};

			LearnerSettingsVM = new LearnerSettingsViewModel()
			{
				MinInterval = settings.MinInterval,
				SecInterval = settings.SecInterval,
				Timeout = settings.Timeout,
				Option = settings.VocabularySource,
				CustomWordList = settings.CustomWordList,
				UseHistoryWordlist = settings.UseHistoryWordlist,
				UseCustomWordlist = settings.UseCustomWordlist,
			};

			DynamicSettingsVM = new DynamicSettingsViewModel()
			{
				AutoCopyToClipboard = settings.AutoCopyToClipboard,
				UseTriggerKey = settings.UseTriggerKey,
				SelectedKey = settings.TriggerKey,
			};

			generalSettingsVM.SettingsChanged += OnSettingsChanged;
			learnerSettingsVM.SettingsChanged += OnSettingsChanged;
			dynamicSettingsVM.SettingsChanged += OnSettingsChanged;
		}

		private void OnSettingsChanged()
		{
			CanApply = true;
		}

		#endregion

		#region Commands

		public DelegateCommand SaveCommand { get; private set; }
		public DelegateCommand ApplyCommand { get; private set; }

		#endregion

		private void ApplySettings()
		{
			try
			{
				LogWriter.Instance.WriteLine("Saving Settings begins");

				Settings settings = new Settings()
				{
					RunAtStartup = GeneralSettingsVM.RunAtStartup,
					IsLearnerEnabled = GeneralSettingsVM.IsLearnerEnabled,
					IsDynamicEnabled = GeneralSettingsVM.IsDynamicEnabled,

					SecInterval = LearnerSettingsVM.SecInterval,
					MinInterval = LearnerSettingsVM.MinInterval,
					VocabularySource = LearnerSettingsVM.Option,
					CustomWordList = LearnerSettingsVM.CustomWordList,
					UseHistoryWordlist = LearnerSettingsVM.UseHistoryWordlist,
					UseCustomWordlist = LearnerSettingsVM.UseCustomWordlist,
					Timeout = LearnerSettingsVM.Timeout,

					AutoCopyToClipboard = DynamicSettingsVM.AutoCopyToClipboard,
					UseTriggerKey = DynamicSettingsVM.UseTriggerKey,
					TriggerKey = DynamicSettingsVM.SelectedKey,
				};

				settingsLogic.SaveSettings(settings);
				CanApply = false;

				LogWriter.Instance.WriteLine("Saving Settings ends");
			}
			catch (Exception ex)
			{
				var errorMsg = new StringBuilder();

				errorMsg.AppendLine("Error occured in saving settings - SettingsViewModel.SaveSettings()");
				errorMsg.AppendLine(ex.Message);

				if (ex.InnerException != null)
				{
					errorMsg.AppendLine(ex.InnerException.Message);
				}

				LogWriter.Instance.WriteLine(errorMsg.ToString());
			}
		}

		private void SaveSettings()
		{
			ApplySettings();
			Close();
		}

		private void Close()
		{
			IsClose = true;
		}
	}
}
