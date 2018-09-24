using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EDictionary.Controls
{
	public class ExtendedListView : ListView
	{
		// https://www.wpftutorial.net/DependencyProperties.html

		public ExtendedListView()
		{
			// Use base class style
			SetResourceReference(StyleProperty, typeof(ListView));
		}

		#region TopIndex DP

		public static readonly DependencyProperty TopIndexProperty = DependencyProperty.Register(
			"TopIndex",
			typeof(int),
			typeof(ExtendedListView),
			new FrameworkPropertyMetadata(-1,
				OnTopIndexChangedProperty,
				OnCoerceTopIndexProperty));

		public int TopIndex
		{
			get
			{
				return (int)GetValue(TopIndexProperty);
			}
			set
			{
				SetValue(TopIndexProperty, value);
			}
		}

		private static void OnTopIndexChangedProperty(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			ExtendedListView listView = source as ExtendedListView;

			if (listView.Items.Count == 0)
			{
				return;
			}

			ScrollViewer scrollViewer = GetChildOfType<ScrollViewer>(listView);

			if (scrollViewer != null)
			{
				scrollViewer.ScrollToBottom();
				listView.ScrollIntoView(listView.Items[listView.TopIndex]);
			}
		}

		private static object OnCoerceTopIndexProperty(DependencyObject sender, object data)
		{
			ExtendedListView listView = sender as ExtendedListView;
			int topIndex = (int)data;

			if (topIndex > listView.Items.Count - 1)
				return listView.Items.Count - 1;

			if (topIndex < 0)
				return 0;

			return topIndex;
		}

		#endregion

		private static T GetChildOfType<T>(DependencyObject depObj) where T : DependencyObject
		{
			if (depObj == null)
				return null;

			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
			{
				var child = VisualTreeHelper.GetChild(depObj, i);

				var result = (child as T) ?? GetChildOfType<T>(child);
				if (result != null)
					return result;
			}
			return null;
		}
	}
}
