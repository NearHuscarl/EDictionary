using EDictionary.Theme.Utilities;
using ICSharpCode.AvalonEdit.Highlighting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace EDictionary.Core.Views
{
	/// <summary>
	/// Interaction logic for DefinitionTextbox.xaml
	/// </summary>
	public partial class DefinitionTextbox : UserControl
	{
		public DefinitionTextbox()
		{
			RegisterCustomHighlight();
			InitializeComponent();
		}

		private void RegisterCustomHighlight()
		{
			IHighlightingDefinition eDictionaryHighlighting;

			// Remember to set Build Action to 'Embedded Resource'
			using (Stream s = typeof(MainWindow).Assembly.GetManifestResourceStream("EDictionary.Core.Views.EDictionary.xshd"))
			{
				if (s == null)
					throw new InvalidOperationException("Could not find embedded resource");

				using (XmlReader reader = new XmlTextReader(s))
				{
					eDictionaryHighlighting = ICSharpCode.AvalonEdit.Highlighting.Xshd.
						HighlightingLoader.Load(reader, HighlightingManager.Instance);
				}
			}

			Dictionary<string, string> groupToColor = new Dictionary<string, string>()
			{
				{ "Header", "Yellow" },
				{ "Label", "Gray" },
				{ "Example", "LightBlue" },
				{ "WrongWord", "LightRed" },
			};

			foreach (var item in groupToColor)
			{
				var highlightingColor = eDictionaryHighlighting.NamedHighlightingColors.First(c => c.Name == item.Key);

				highlightingColor.Foreground = new SimpleHighlightingBrush(ColorPicker.GetMediaColor(item.Value));
			}

			// and register it in the HighlightingManager
			HighlightingManager.Instance.RegisterHighlighting("EDictionary", new string[] { ".edic" }, eDictionaryHighlighting);
		}

		public int NameFontSize
		{
			get { return (int)GetValue(NameFontSizeProperty); }
			set { SetValue(NameFontSizeProperty, value); }
		}

		public static readonly DependencyProperty NameFontSizeProperty = DependencyProperty.Register(
			"NameFontSize",
			typeof(int),
			typeof(DefinitionTextbox),
			new PropertyMetadata(10));

		public int WordformFontSize
		{
			get { return (int)GetValue(WordformFontSizeProperty); }
			set { SetValue(WordformFontSizeProperty, value); }
		}

		public static readonly DependencyProperty WordformFontSizeProperty = DependencyProperty.Register(
			"WordformFontSize",
			typeof(int),
			typeof(DefinitionTextbox),
			new PropertyMetadata(10));

		public int AudioButtonSize
		{
			get { return (int)GetValue(AudioButtonSizeProperty); }
			set { SetValue(AudioButtonSizeProperty, value); }
		}

		public static readonly DependencyProperty AudioButtonSizeProperty = DependencyProperty.Register(
			"AudioButtonSize",
			typeof(int),
			typeof(DefinitionTextbox),
			new PropertyMetadata(10));

		public int PronunciationFontSize
		{
			get { return (int)GetValue(PronunciationFontSizeProperty); }
			set { SetValue(PronunciationFontSizeProperty, value); }
		}

		public static readonly DependencyProperty PronunciationFontSizeProperty = DependencyProperty.Register(
			"PronunciationFontSize",
			typeof(int),
			typeof(DefinitionTextbox),
			new PropertyMetadata(10));

		public int DefinitionFontSize
		{
			get { return (int)GetValue(DefinitionFontSizeProperty); }
			set { SetValue(DefinitionFontSizeProperty, value); }
		}

		public static readonly DependencyProperty DefinitionFontSizeProperty = DependencyProperty.Register(
			"DefinitionFontSize",
			typeof(int),
			typeof(DefinitionTextbox),
			new PropertyMetadata(10));
	}
}
