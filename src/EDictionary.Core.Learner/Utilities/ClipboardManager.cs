using EDictionary.Core.Learner.Extensions;
using System;
using System.Windows;

namespace EDictionary.Core.Learner.Utilities
{
	public class ClipboardManager
	{
		public void Clear()
		{
			Clipboard.Clear();
		}

		public string GetCurrentText()
		{
			try
			{
				return Clipboard.GetText().RemoveSpecialCharacters().ToLowerInvariant();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return "";
			}
		}

		public bool IsContainsText()
		{
			try
			{
				return Clipboard.ContainsText() && !string.IsNullOrEmpty(Clipboard.GetText().Trim());
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}
	}
}
