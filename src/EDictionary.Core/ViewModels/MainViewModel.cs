using EDictionary.Core.DataLogic;
using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using EDictionary.Core.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace EDictionary.Core.ViewModels
{
	public class MainViewModel : ViewModelBase, IMainViewModel
	{
		#region Fields

		private WordLogic wordLogic;
		private HistoryLogic historyLogic;
		private History<string> history;
		private SettingsLogic settingsLogic;

		private DefinitionViewModel definitionVM;

		private bool isTextBoxFocus;

		private int wordListTopIndex;
		private string searchedWord = "";
		private string highlightedWord;
		private Dictionary<string, string> otherResultNameToID;
		private string highlightedOtherResult;
		private object searchIcon;

		#endregion

		#region Properties

		public int QueryMaxLength { get; set; } = 30;

		public double WindowMinimumHeight { get; set; } = 350;

		public double WindowMinimumWidth { get; set; } = 450;

		public DefinitionViewModel DefinitionVM
		{
			get { return definitionVM; }
			protected set { SetPropertyAndNotify(ref definitionVM, value); }
		}

		public bool IsTextBoxFocus
		{
			get { return isTextBoxFocus; }
			set { SetPropertyAndNotify(ref isTextBoxFocus, value); }
		}

		public List<string> WordList { get; set; }

		public int WordListTopIndex
		{
			get { return wordListTopIndex; }
			set { SetPropertyAndNotify(ref wordListTopIndex, value); }
		}

		public string SearchedWord
		{
			get { return searchedWord; }

			set
			{
				if (searchedWord.Length > 30)
					searchedWord = searchedWord.Substring(0, QueryMaxLength);

				if (SetPropertyAndNotify(ref searchedWord, value))
				{
					SearchFromInputCommand.RaiseCanExecuteChanged();
				}
			}
		}

		public string HighlightedWord
		{
			get { return highlightedWord; }

			set
			{
				if (SetProperty(ref highlightedWord, value))
				{
					SearchedWord = HighlightedWord;
					IsTextBoxFocus = true;
				}
			}
		}

		public List<string> OtherResults
		{
			get { return otherResultNameToID.Keys.ToList(); }
		}

		public string HighlightedOtherResult
		{
			get { return highlightedOtherResult; }
			set { SetProperty(ref highlightedOtherResult, value); }
		}

		public object SearchIcon
		{
			get { return searchIcon; }

			set
			{
				App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Send,
					new Action(() => SetPropertyAndNotify(ref searchIcon, value)
				));
				
			}
		}

		#endregion

		#region Actions

		public Action ShowSettingsWindowAction { get; set; }
		public Action ShowAboutWindowAction { get; set; }

		#endregion

		#region Constructor

		public MainViewModel()
		{
			DefinitionVM = new DefinitionViewModel()
			{
				DoubleClickCommand = new DelegateCommand(SearchFromSelection, CanSearchFromSelection),
			};

			otherResultNameToID = new Dictionary<string, string>();

			LoadCommands();
			LoadLogic();

			SearchIcon = "SearchIcon";
		}

		private void LoadCommands()
		{
			SearchFromInputCommand = new DelegateCommand(SearchFromInput, CanSearchFromInput);
			SearchFromHighlightCommand = new DelegateCommand(SearchFromHighlight);
			UpdateWordlistTopIndexCommand = new DelegateCommand(UpdateWordlistTopIndex);

			NextHistoryCommand = new DelegateCommand(NextHistory, CanGoToNextHistory);
			PreviousHistoryCommand = new DelegateCommand(PreviousHistory, CanGoToPreviousHistory);

			AddToWordlistCommand = new DelegateCommand(AddToWordlist);

			OpenSettingCommand = new DelegateCommand(OpenSettings);
			OpenAboutCommand = new DelegateCommand(OpenAbout);

			SearchHighlightedOtherResultCommand = new DelegateCommand(SearchHighlightedOtherResult, CanSearchHighlightedOtherResult);
			CloseCommand = new DelegateCommand(OnCloseWindow);
		}

		private void LoadLogic()
		{
			settingsLogic = new SettingsLogic();

			wordLogic = new WordLogic();
			WordList = wordLogic.WordList;

			historyLogic = new HistoryLogic();
			history = historyLogic.LoadHistory<string>();

			Word word;
			if (history.Count > 0)
				word = wordLogic.Search(history.Current);
			else
				word = wordLogic.Search(WordList.FirstOrDefault());

			if (word != null)
				ShowDefinition(word);

			NotifyHistoryChange();
		}

		#endregion

		#region Commands

		public DelegateCommand SearchFromInputCommand { get; private set; }
		public DelegateCommand SearchFromSelectionCommand { get; private set; }
		public DelegateCommand SearchFromHighlightCommand { get; private set; }
		public DelegateCommand UpdateWordlistTopIndexCommand { get; private set; }
		public DelegateCommand NextHistoryCommand { get; private set; }
		public DelegateCommand PreviousHistoryCommand { get; private set; }
		public DelegateCommand AddToWordlistCommand { get; private set; }
		public DelegateCommand OpenSettingCommand { get; private set; }
		public DelegateCommand OpenAboutCommand { get; private set; }
		public DelegateCommand SearchHighlightedOtherResultCommand { get; private set; }
		public DelegateCommand CloseCommand { get; private set; }

		private void OpenSettings()
		{
			ShowSettingsWindowAction.Invoke();
		}

		private void OpenAbout()
		{
			ShowAboutWindowAction.Invoke();
		}

		#endregion

		#region Wordlist

		public async void UpdateWordlistTopIndex()
		{
			await Task.Run(() => WordListTopIndex = Search.Prefix(SearchedWord, WordList));
		}

		private void ShowDefinition(Word word)
		{
			DefinitionVM.SetContent(word);
		}

		private void CorrectWord(string wrongWord)
		{
			var suggestions = wordLogic.GetSuggestions(wrongWord);
			DefinitionVM.SetContent(suggestions);
		}

		#endregion

		#region SearchFromInput

		/// <summary>
		/// Called on enter or doubleclick event on wordlist
		/// Run spellcheck for similar word when word not found
		/// </summary>
		public void SearchFromInput()
		{
			//SearchIcon = "SpinnerIcon";
			//NotifyPropertyChanged("SearchIcon");

			Word word = wordLogic.Search(SearchedWord);

			if (word == null)
			{
				var newWord = wordLogic.Normalize(SearchedWord);

				if (newWord != null)
					word = wordLogic.Search(newWord);
			}

			if (word != null)
				ShowDefinition(word);
			else
				CorrectWord(SearchedWord);

			UpdateHistory(word);
		}

		public bool CanSearchFromInput()
		{
			if (string.IsNullOrEmpty(SearchedWord))
				return false;

			return true;
		}

		#endregion

		#region SearchFromSelection

		/// <summary>
		/// Called when select highlight word in definition window
		/// Stem a word when word not found instead of running spellcheck
		/// </summary>
		public void SearchFromSelection()
		{
			Word word = wordLogic.Search(DefinitionVM.SelectedWord);

			if (word == null)
			{
				var newWord = wordLogic.Normalize(DefinitionVM.SelectedWord);

				if (newWord != null)
					word = wordLogic.Search(newWord);
			}

			if (word != null)
				ShowDefinition(word);

			UpdateHistory(word);
		}

		public bool CanSearchFromSelection()
		{
			if (DefinitionVM.Word == null)
				return false;

			return true;
		}

		#endregion

		#region SearchFromHighlight

		/// <summary>
		/// Called when double click highlighted word in listview
		/// Therefore, there is no need to run stemmer or spellcheck
		/// </summary>
		public void SearchFromHighlight()
		{
			Word word = wordLogic.Search(HighlightedWord);

			if (word != null)
				ShowDefinition(word);

			UpdateHistory(word);
		}

		#endregion

		#region History

		private async void UpdateHistory(Word word)
		{
			await Task.Run(() =>
			{
				if (word == null)
					return;

				if (word.Name != history.Current)
					history.Add(word.Name);

				UpdateOtherResultList();

				Application.Current.Dispatcher.Invoke(() => NotifyHistoryChange());
			});
		}

		private void NotifyHistoryChange()
		{
			PreviousHistoryCommand.RaiseCanExecuteChanged();
			NextHistoryCommand.RaiseCanExecuteChanged();
		}

		public void NextHistory()
		{
			Word word = wordLogic.Search(history.Next());
			ShowDefinition(word);

			NotifyHistoryChange();
		}

		public void PreviousHistory()
		{
			Word word = wordLogic.Search(history.Previous());
			ShowDefinition(word);

			NotifyHistoryChange();
		}

		public bool CanGoToNextHistory()
		{
			if (history == null || history.Count == 0)
				return false;

			return !history.IsLast;
		}

		public bool CanGoToPreviousHistory()
		{
			if (history == null || history.Count == 0)
				return false;

			return !history.IsFirst;
		}

		#endregion

		#region AddToWordlist

		private async void AddToWordlist()
		{
			await Task.Run(() =>
			{
				settingsLogic.AddToWordlist(history.Current);
			});
		}

		#endregion

		#region OtherResult

		public void UpdateOtherResultList()
		{
			if (DefinitionVM.Word.Similars == null)
				return;

			otherResultNameToID.Clear();

			foreach (var similarWord in DefinitionVM.Word.Similars)
			{
				otherResultNameToID.Add(similarWord.Replace('_', ' '), similarWord);
			}
			NotifyPropertyChanged("OtherResults");
		}

		public bool CanSearchHighlightedOtherResult()
		{
			if (HighlightedOtherResult == null)
				return false;

			return true;
		}

		public void SearchHighlightedOtherResult()
		{
			Word word = wordLogic.SearchID(otherResultNameToID[HighlightedOtherResult]);

			if (word != null)
				ShowDefinition(word);

			UpdateHistory(word);
		}

		#endregion

		private void OnCloseWindow()
		{
			historyLogic.SaveHistory(history);
		}
	}
}
