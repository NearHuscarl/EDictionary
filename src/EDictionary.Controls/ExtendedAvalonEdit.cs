using EDictionary.Theme.Utilities;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace EDictionary.Controls
{
	public class ExtendedAvalonEdit : TextEditor
	{
		public ExtendedAvalonEdit()
		{
			// Use base class style
			SetResourceReference(StyleProperty, typeof(TextEditor));

			TextArea.Caret.CaretBrush = Brushes.Transparent;

			// Remove all default InputBindings like CTRL+A to select all
			TextArea.InputBindings.Clear();

			// A very dirty hack to make InputBindings work in TextEditor
			// TextEditor have a TextArea control which will always be focused
			// instead of the TextEditor itself so input binded to it will not work
			// but the input binding in TextArea will work instead
			this.Loaded += AssignInputBindingsToTextArea;

			TextArea.SelectionChanged += HideSelection;

			// TODO [Need a rework]: MouseDoubleClick will work but it's fired after
			// the InputBinding's LeftDoubleClick gesture so that wont work
			// MouseUp is not working like in richtextbox btw
			// MouseMove works because it updates SelectedWord repeatedly but
			// very inefficient
			//TextArea.MouseUp += OnMouseMove;
			TextArea.MouseMove += OnMouseMove;
		}

		private void OnMouseMove(object sender, MouseEventArgs e)
		{
			SelectedWord = GetWordAtMousePosition(e);
			//Debug.Print(SelectedWord);

			if (SelectedWord != string.Empty)
			{
				TextArea.Cursor = Cursors.Hand;
			}
			else
			{
				TextArea.Cursor = Cursors.Arrow;
			}
		}

		/// <summary>
		/// Do I really need to hide it though?
		/// </summary>
		private void HideSelection(object sender, EventArgs e)
		{
			var textArea = sender as TextArea;

			if (textArea == null)
				return;

			textArea.ClearSelection();
		}

		private void AssignInputBindingsToTextArea(object sender, RoutedEventArgs e)
		{
			foreach (InputBinding inputBinding in this.InputBindings)
			{
				TextArea.InputBindings.Add(inputBinding);
			}
		}

		#region BindingText DP

		/// <summary>
		/// TextEditor.Text is not a dependency property so it cannot be binded
		/// This is a wrapper DP to bind content to TextEditor.Text
		/// </summary>
		public static readonly DependencyProperty BindingTextProperty =
			DependencyProperty.Register(
					"BindingText",
					typeof(string),
					typeof(ExtendedAvalonEdit),
					new PropertyMetadata(
						"",
						OnBindingTextChangedProperty)
					);

		public string BindingText
		{
			get { return (string)GetValue(BindingTextProperty); }
			set { SetValue(BindingTextProperty, value); }
		}

		private static void OnBindingTextChangedProperty(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			ExtendedAvalonEdit editor = source as ExtendedAvalonEdit;

			editor.Text = editor.BindingText;
		}

		#endregion

		#region BindingSelectedText DP

		/// <summary>
		/// TextEditor.SelectedText is not a dependency property so it cannot be binded
		/// This is a wrapper DP to bind content to TextEditor.SelectedText
		/// </summary>
		public static readonly DependencyProperty SelectedWordProperty =
			DependencyProperty.Register(
					"SelectedWord",
					typeof(string),
					typeof(ExtendedAvalonEdit),
					new PropertyMetadata(defaultValue: "")
					);

		public string SelectedWord
		{
			get { return (string)GetValue(SelectedWordProperty); }
			set { SetValue(SelectedWordProperty, value); }
		}

		#endregion

		private string GetWordAtMousePosition(MouseEventArgs e)
		{
			var mousePosition = this.GetPositionFromPoint(e.GetPosition(this));

			if (mousePosition == null)
				return string.Empty;

			var line = mousePosition.Value.Line;
			var column = mousePosition.Value.Column;
			var offset = Document.GetOffset(line, column);

			if (offset >= Document.TextLength)
				offset--;

			int offsetStart = TextUtilities.GetNextCaretPosition(Document, offset, LogicalDirection.Backward, CaretPositioningMode.WordBorder);
			int offsetEnd = TextUtilities.GetNextCaretPosition(Document, offset, LogicalDirection.Forward, CaretPositioningMode.WordBorder);

			if (offsetEnd == -1 || offsetStart == -1)
				return string.Empty;

			var currentChar = Document.GetText(offset, 1);

			if (string.IsNullOrWhiteSpace(currentChar))
				return string.Empty;

			return Document.GetText(offsetStart, offsetEnd - offsetStart);
		}
	}
}

