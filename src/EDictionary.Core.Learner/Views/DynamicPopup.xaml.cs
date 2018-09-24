using EDictionary.Core.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace EDictionary.Core.Learner.Views
{
	/// <summary>
	/// Interaction logic for DefinitionPopup.xaml
	/// </summary>
	public partial class DynamicPopup : Window
	{
		private enum WindowPositionToCursor
		{
			Top,
			Bottom,
			Left,
			Right,
		}


		public DynamicPopup(ViewModelBase viewModel)
		{
			InitializeComponent();

			DataContext = viewModel;

			Activated += (s, e) =>
			{
				Popup();
				Focus();
			};
		}

		protected override void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e)
		{
			if (!(bool)e.NewValue)
				Visibility = Visibility.Hidden;

			base.OnIsKeyboardFocusWithinChanged(e);
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			Popup();

			base.OnSourceInitialized(e);
		}

		public void Popup()
		{
			int offset = 25;
			Point mouse = GetGlobalMousePosition();

			bool isOffTop = mouse.Y - ActualHeight - offset < 0;
			bool isOffBottom = mouse.Y + offset > SystemParameters.WorkArea.Height;
			bool isOffLeft = mouse.X - ActualWidth - offset < 0 - 140;
			bool isOffRight = mouse.X + offset > SystemParameters.WorkArea.Width - 140;

			if (isOffTop && !isOffBottom && !isOffLeft && !isOffRight)
				MoveWindow(WindowPositionToCursor.Bottom, offset);

			else if (!isOffTop && isOffBottom && !isOffLeft && !isOffRight)
				MoveWindow(WindowPositionToCursor.Top, offset);

			else if (!isOffTop && !isOffBottom && isOffLeft && !isOffRight)
				MoveWindow(WindowPositionToCursor.Right, offset);

			else if (!isOffTop && !isOffBottom && !isOffLeft && isOffRight)
				MoveWindow(WindowPositionToCursor.Left, offset);

			else
				MoveWindow(WindowPositionToCursor.Top, offset);
		}

		private void MoveWindow(WindowPositionToCursor windowPositionToCursor, int offset=0)
		{
			Point mouse = GetGlobalMousePosition();

			switch (windowPositionToCursor)
			{
				case WindowPositionToCursor.Top:
					Left = mouse.X - ActualWidth / 2;
					Top = mouse.Y - ActualHeight - offset;
					break;

				case WindowPositionToCursor.Bottom:
					Left = mouse.X - ActualWidth / 2;
					Top = mouse.Y + offset;
					break;

				case WindowPositionToCursor.Left:
					Left = mouse.X - ActualWidth - offset;
					Top = mouse.Y - ActualHeight / 2;
					break;

				case WindowPositionToCursor.Right:
					Left = mouse.X + offset;
					Top = mouse.Y - ActualHeight / 2;
					break;
			}
		}

		private Point GetGlobalMousePosition()
		{
			var transform = PresentationSource.FromVisual(this).CompositionTarget.TransformFromDevice;
			var mousePosition = transform.Transform(GetMousePosition());
			return mousePosition;
		}

		public Point GetMousePosition()
		{
			System.Drawing.Point point = System.Windows.Forms.Control.MousePosition;
			return new Point(point.X, point.Y);
		}
	}
}
