using EDictionary.Core.Extensions;
using EDictionary.Core.Models;
using EDictionary.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace EDictionary.Core.Learner.ViewModels
{
	public partial class TaskIconViewModel
	{
		#region Fields

		private DefinitionViewModel learnerVM;

		private TimeSpan spawnInterval;
		private TimeSpan activeInterval;

		private int spawnCounter;
		private int activeCounter;

		private DispatcherTimer spawnTimer;
		private DispatcherTimer activeTimer;

		private VocabularySource option;

		private bool useCustomWordlist;
		private bool useHistoryWordlist;

		private List<string> wordList;
		private List<string> historyWordlist;

		#endregion

		#region Properties

		public DefinitionViewModel LearnerVM
		{
			get { return learnerVM; }
			protected set { SetPropertyAndNotify(ref learnerVM, value); }
		}

		#endregion

		private void InitLearner()
		{
			wordList = new List<string>();
			historyWordlist = new List<string>();

			LearnerVM = new DefinitionViewModel();

			spawnTimer = new DispatcherTimer();
			spawnTimer.Tick += OnSpawnTimerTick;
			spawnTimer.Interval = new TimeSpan(hours: 0, minutes: 0, seconds: 1);

			activeTimer = new DispatcherTimer();
			activeTimer.Tick += OnActiveTimerTick;
			activeTimer.Interval = new TimeSpan(hours: 0, minutes: 0, seconds: 1);
		}

		private string GetRandomWordFromWordLists(params List<string>[] wordListArray)
		{
			var wordLists = new List<List<string>>(wordListArray);

			// Remove any empty wordlists
			foreach (var list in wordLists.ToList())
			{
				if (!list.Any())
					wordLists.Remove(list);
			}

			if (!wordLists.Any())
				return null;

			List<string> wordList = new List<List<string>>(wordLists).PickRandom();

			return wordList.PickRandom();
		}

		/// <summary>
		/// Return true on success, false on failure (Word list is empty)
		/// </summary>
		/// <returns></returns>
		private bool SetRandomWord()
		{
			string randWord = null;

			if (option == VocabularySource.Full)
			{
				randWord = GetRandomWordFromWordLists(wordList);
			}
			else if (option == VocabularySource.Custom)
			{
				if (useCustomWordlist && !useHistoryWordlist)
					randWord = GetRandomWordFromWordLists(wordList);

				else if (useHistoryWordlist && !useCustomWordlist)
					randWord = GetRandomWordFromWordLists(historyWordlist);

				else // (useCustomWordlist && useHistoryWordlist)
					randWord = GetRandomWordFromWordLists(wordList, historyWordlist);
			}

			if (randWord == null)
				return false;

			Word word = wordLogic.Search(randWord);

			LearnerVM.Word = word;
			LearnerVM.Definition = word.ToDisplayedString();

			return true;
		}

		private void OnSpawnTimerTick(object sender, EventArgs e)
		{
			if (CurrentStatus == Status.Stop)
				return;

			spawnCounter--;

			if (spawnCounter <= 0)
			{
				if (SetRandomWord())
					OpenLearnerBalloon();

				spawnCounter = (int)spawnInterval.TotalSeconds;

				spawnTimer.Stop();
				activeTimer.Start();
			}
		}

		private void OnActiveTimerTick(object sender, EventArgs e)
		{
			if (CurrentStatus == Status.Pause)
				return;

			activeCounter--;

			if (activeCounter <= 0)
			{
				CloseLearnerBalloon();

				activeCounter = (int)activeInterval.TotalSeconds;

				activeTimer.Stop();
				spawnTimer.Start();
			}
		}

		private void ToggleActive()
		{
			if (NextStatus == Status.Run)
			{
				CurrentStatus = Status.Run;
			}
			else
			{
				CurrentStatus = Status.Stop;
			}
		}

		public async void RunAsync()
		{
			await Task.Run(() =>
			{
				CurrentStatus = Status.Run;

				ReloadSettings();

				spawnTimer.Start();
			});
		}
	}
}
