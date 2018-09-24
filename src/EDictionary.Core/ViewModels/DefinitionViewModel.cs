using System;
using EDictionary.Core.Models;
using EDictionary.Core.Models.WordComponents;
using EDictionary.Core.Utilities;
using EDictionary.Core.ViewModels.Interfaces;
using EDictionary.Core.DataLogic;

namespace EDictionary.Core.ViewModels
{
	public class DefinitionViewModel : ViewModelBase, IDefinitionViewModel
	{
		private bool headerVisibility;
		private bool resetScrollViewer;

		private Word word;
		private string selectedWord;
		private string definition;

		/// <summary>
		/// true -> Visibility.Visible
		/// false -> Visibility.Collapsed
		/// </summary>
		public bool HeaderVisibility
		{
			get { return headerVisibility; }
			set { SetPropertyAndNotify(ref headerVisibility, value); }
		}

		public bool ResetScrollViewer
		{
			get { return resetScrollViewer; }
			set { SetPropertyAndNotify(ref resetScrollViewer, value); }
		}

		public Word Word
		{
			get { return word; }

			set
			{
				if (value != null)
					HeaderVisibility = true;
				else
					HeaderVisibility = false;

				SetPropertyAndNotify(ref word, value);
			}
		}

		public string SelectedWord
		{
			get { return selectedWord.ToLower(); }
			set { SetProperty(ref selectedWord, value); }
		}

		public string Definition
		{
			get { return definition; }

			set
			{
				if (SetPropertyAndNotify(ref definition, value))
				{
					ResetScrollViewer = true;
					PlayBrEAudioCommand.RaiseCanExecuteChanged();
					PlayNAmEAudioCommand.RaiseCanExecuteChanged();
				}
			}
		}

		public DefinitionViewModel()
		{
			PlayNAmEAudioCommand = new DelegateCommand(PlayNAmEAudio, CanPlayAudio);
			PlayBrEAudioCommand = new DelegateCommand(PlayBrEAudio, CanPlayAudio);
		}

		public DelegateCommand PlayNAmEAudioCommand { get; private set; }
		public DelegateCommand PlayBrEAudioCommand { get; private set; }
		public DelegateCommand DoubleClickCommand { get; set; }

		#region Methods

		public void SetContent(Word word)
		{
			Word = word;
			Definition = word.ToDisplayedString();
		}

		public void SetContent(string text)
		{
			Word = null; // Hide Header
			Definition = text;
		}

		public void PlayBrEAudio()
		{
			Word.PlayAudio(Dialect.BrE);
		}

		public void PlayNAmEAudio()
		{
			Word.PlayAudio(Dialect.NAmE);
		}

		public bool CanPlayAudio()
		{
			if (Word == null)
				return false;

			return true;
		}

		#endregion
	}
}