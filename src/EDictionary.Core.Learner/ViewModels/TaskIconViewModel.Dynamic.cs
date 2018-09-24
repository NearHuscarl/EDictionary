using EDictionary.Core.Learner.Utilities;
using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using EDictionary.Core.ViewModels;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDictionary.Core.Learner.ViewModels
{
	public partial class TaskIconViewModel
	{
		#region Fields

		private DefinitionViewModel dynamicVM;

		private GlobalKeyboardHook keyboardHook;
		private GlobalMouseHook mouseHook;
		private ClipboardManager clipboardManager;

		private bool autoCopyToClipboard;
		private bool useTriggerKey;
		private Keys triggerKey;

		#endregion

		#region Properties

		public DefinitionViewModel DynamicVM
		{
			get { return dynamicVM; }
			protected set { SetPropertyAndNotify(ref dynamicVM, value); }
		}

		#endregion

		private void InitDynamic()
		{
			DynamicVM = new DefinitionViewModel();
			DynamicVM.DoubleClickCommand = new DelegateCommand(SearchFromSelection);

			keyboardHook = new GlobalKeyboardHook();

			clipboardManager = new ClipboardManager();
		}

		private void EnableDynamic()
		{
			App.Current.Dispatcher.Invoke(() =>
			{
				keyboardHook.StartHook();
				mouseHook = new GlobalMouseHook();
				mouseHook.DoubleClick += OnMouseDoubleClicked;
				SendKeys.Flush();
			});
		}

		private void DisableDynamic()
		{
			App.Current.Dispatcher.Invoke(() =>
			{
				keyboardHook.StopHook();
				mouseHook.DoubleClick -= OnMouseDoubleClicked;
				mouseHook.Dispose();
				Task.Run(() => SendKeys.Flush());
			});
		}

		private async void OnMouseDoubleClicked(object sender, MouseEventArgs e)
		{
			if (autoCopyToClipboard)
				await SendCopyCommandAsync();

			if (useTriggerKey)
				keyboardHook.KeyPressed += OnKeyPressed;
			else
				OpenPopupFromClipboard();

			System.Diagnostics.Debug.WriteLine(clipboardManager.GetCurrentText());
		}

		private void OnKeyPressed(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != triggerKey)
				return;

			OpenPopupFromClipboard();

			keyboardHook.KeyUp -= OnKeyPressed;
		}

		private void OpenPopupFromClipboard()
		{
			if (clipboardManager.IsContainsText())
			{
				var selectedText = clipboardManager.GetCurrentText();

				if (selectedText.Split(' ').Length > 1)
					return;

				SearchDefinition(selectedText);

				OpenDynamicPopup();

				clipboardManager.Clear();
			}
		}

		private Task SendCopyCommandAsync()
		{
			var thread = new Thread(() =>
			{
				SendKeys.SendWait("^c");
				SendKeys.Flush();
			});

			thread.SetApartmentState(ApartmentState.MTA);
			thread.Start();
			thread.Join();

			return Task.FromResult(0);
		}

		/// <summary>
		/// Called when selecting a word in definition text area
		/// </summary>
		public void SearchFromSelection()
		{
			SearchDefinition(DynamicVM.SelectedWord);
		}

		private void SearchDefinition(string wordName)
		{
			Word word = wordLogic.Search(wordName);

			if (word == null)
			{
				var newWord = wordLogic.Normalize(wordName);

				if (newWord != null)
					word = wordLogic.Search(newWord);
			}

			if (word != null)
				DynamicVM.SetContent(word);
			else
				DynamicVM.SetContent(wordLogic.GetSuggestions(wordName));
		}
	}
}
