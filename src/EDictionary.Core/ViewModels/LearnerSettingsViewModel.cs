using EDictionary.Core.Models;
using EDictionary.Core.ViewModels.Interfaces;
using System.Collections.Generic;

namespace EDictionary.Core.ViewModels
{
	public class LearnerSettingsViewModel : SettingsViewModelBase, ILearnerSettingsViewModel
	{
		private bool canEditCustomOptions;

		private int minInterval;
		private int secInterval;

		private int timeout;

		private VocabularySource option;
		private List<string> customWordList = new List<string>();

		private bool useHistoryWordlist;
		private bool useCustomWordlist;

		public bool CanEditCustomOptions
		{
			get { return canEditCustomOptions; }
			set
			{
				SetPropertyAndNotify(ref canEditCustomOptions, value);
				OnSettingsChanged();
			}
		}

		public int MinInterval
		{
			get { return minInterval; }
			set
			{
				SetPropertyAndNotify(ref minInterval, value);
				OnSettingsChanged();
			}
		}

		public int SecInterval
		{
			get { return secInterval; }
			set
			{
				SetPropertyAndNotify(ref secInterval, value);
				OnSettingsChanged();
			}
		}

		public int Timeout
		{
			get { return timeout; }
			set
			{
				SetPropertyAndNotify(ref timeout, value);
				OnSettingsChanged();
			}
		}

		public VocabularySource Option
		{
			get { return option; }

			set
			{
				if (value == VocabularySource.Custom)
					CanEditCustomOptions = true;
				else
					CanEditCustomOptions = false;

				SetPropertyAndNotify(ref option, value);
				OnSettingsChanged();
			}
		}

		public List<string> CustomWordList
		{
			get { return customWordList; }
			set
			{
				SetPropertyAndNotify(ref customWordList, value);
				OnSettingsChanged();
			}
		}

		public bool UseHistoryWordlist
		{
			get { return useHistoryWordlist; }
			set
			{
				SetPropertyAndNotify(ref useHistoryWordlist, value);
				OnSettingsChanged();
			}
		}

		public bool UseCustomWordlist
		{
			get { return useCustomWordlist; }
			set
			{
				SetPropertyAndNotify(ref useCustomWordlist, value);
				OnSettingsChanged();
			}
		}
	}
}
