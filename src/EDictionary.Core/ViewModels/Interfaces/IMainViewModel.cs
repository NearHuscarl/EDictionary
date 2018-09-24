using System.Collections.Generic;

namespace EDictionary.Core.ViewModels.Interfaces
{
	public interface IMainViewModel
	{
		double WindowMinimumHeight { get; set; }
		double WindowMinimumWidth { get; set; }

		bool IsTextBoxFocus { get; set; }

		List<string> WordList { get; }
		int WordListTopIndex { get; set; }

		string SearchedWord { get; set; }
		string HighlightedWord { get; set; }

		List<string> OtherResults { get; }
		string HighlightedOtherResult { get; set; }
	}
}
