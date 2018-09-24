using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace EDictionary.Theme.Animations
{
	public static class WindowAnimations
	{
		private static float duration = 0.2f;

		public static DoubleAnimation FadeInAnimation => new DoubleAnimation()
		{
			Duration = new Duration(TimeSpan.FromSeconds(duration)),
			From = 0.0,
			To = 1.0,
			FillBehavior = FillBehavior.Stop
		};

		public static DoubleAnimation FadeOutAnimation => new DoubleAnimation()
		{
			Duration = new Duration(TimeSpan.FromSeconds(duration)),
			From = 1.0,
			To = 0.0,
			FillBehavior = FillBehavior.Stop
		};
	}
}
