using Gma.System.MouseKeyHook;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace EDictionary.Core.Learner.Utilities
{
	public class GlobalMouseHook
	{
		/// <summary>
		/// We will not use Hook.GlobalEvents().MouseDoubleClick event because when the
		/// event fires, it will select all the text before the cursor in Chrome browser,
		/// so we will only use MouseDown and MouseUp event to make our own improvised
		/// DoubleClick event (or we'll miss the deadline)
		/// </summary>
		private IKeyboardMouseEvents mouseHook;

		private static float doubleClickInterval = 0.5f; // in seconds

		private DateTime lastMouseDownTime;
		private DateTime mouseDownTime;

		private Point mouseFirstPosition;
		private Point mouseSecondPosition;

		public event MouseEventHandler MouseDown
		{
			add { mouseHook.MouseDown += value; }
			remove { mouseHook.MouseDown -= value; }
		}

		public event MouseEventHandler MouseUp
		{
			add { mouseHook.MouseUp += value; }
			remove { mouseHook.MouseUp -= value; }
		}

		public event MouseEventHandler MouseDoubleClick
		{
			add { mouseHook.MouseDoubleClick += value; }
			remove { mouseHook.MouseDoubleClick -= value; }
		}

		/// <summary>
		/// A double click event will fire when there are 2 clicks at
		/// the same position and the time between two click is < 0.5s
		/// </summary>
		public event MouseEventHandler DoubleClick;

		public GlobalMouseHook()
		{
			mouseHook = Hook.GlobalEvents();

			mouseHook.MouseDown += OnMouseDown;
			mouseHook.MouseUp += OnMouseUp;
			//mouseHook.Start();

			DoubleClick += Test;
		}

		private void Test(object sender, MouseEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("DoubleClick");
		}

		private void OnMouseDown(object sender, MouseEventArgs e)
		{
			mouseFirstPosition = e.Location;

			mouseDownTime = DateTime.Now;
		}

		private void OnMouseUp(object sender, MouseEventArgs e)
		{
			if (mouseSecondPosition != mouseFirstPosition)
			{
				mouseSecondPosition = e.Location;
			}
			else if (DateTime.Now.Subtract(lastMouseDownTime) <= TimeSpan.FromSeconds(doubleClickInterval))
			{
				DoubleClick(this, e);
			}

			lastMouseDownTime = mouseDownTime;
		}

		public void Dispose()
		{
			mouseHook.Dispose();
		}
	}
}
