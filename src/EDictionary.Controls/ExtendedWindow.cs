using EDictionary.Theme.Animations;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Animation;

namespace EDictionary.Controls
{
	public class ExtendedWindow : Window
	{
		private WindowState lastState = WindowState.Normal;

		private DoubleAnimation openingAnimation;
		private DoubleAnimation closingAnimation;

		public ExtendedWindow()
		{
			openingAnimation = WindowAnimations.FadeInAnimation;
			closingAnimation = WindowAnimations.FadeOutAnimation;

			this.Loaded += FadeIn;
			this.Closing += FadeOut;
			this.StateChanged += FadeInOrOut;
		}

		private void FadeInOrOut(object sender, EventArgs e)
		{
			if (lastState == WindowState.Minimized && this.WindowState == WindowState.Normal)
			{
				BeginAnimation(OpacityProperty, openingAnimation);
			}

			lastState = this.WindowState;
		}

		/// <summary>
		///
		/// XAML alternative:
		/// <Style.Triggers>
		///	<EventTrigger RoutedEvent = "Window.Loaded" >
		///		< BeginStoryboard Storyboard="{StaticResource FadeInAnimation}"/>
		///	</EventTrigger>
		/// </Style.Triggers>
		/// 
		/// </summary>
		private void FadeIn(object sender, RoutedEventArgs e)
		{
			BeginAnimation(OpacityProperty, openingAnimation);
		}

		private void FadeOut(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
			this.Closing -= FadeOut;

			closingAnimation.Completed += (o, a) => this.Close();
			BeginAnimation(OpacityProperty, closingAnimation);
		}

		#region CloseTrigger DP

		public static readonly DependencyProperty CloseTriggerProperty = DependencyProperty.Register(
			"CloseTrigger", typeof(bool),
			typeof(ExtendedWindow),
			new PropertyMetadata(
				false,
				OnCloseTriggerChanged));

		public bool CloseTrigger
		{
			get { return (bool)GetValue(CloseTriggerProperty); }
			set { SetValue(CloseTriggerProperty, value); }
		}

		private static void OnCloseTriggerChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			ExtendedWindow window = source as ExtendedWindow;

			if (window == null)
				return;

			if (window.CloseTrigger)
			{
				window.Close();
			}
		}

		#endregion
	}
}
