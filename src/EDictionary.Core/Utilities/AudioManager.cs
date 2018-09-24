using System;
using System.Windows.Media;

namespace EDictionary.Core.Utilities
{
	public class AudioManager
	{
		private static MediaPlayer mediaPlayer;

		public AudioManager()
		{
			mediaPlayer = new MediaPlayer();
		}

		/// <summary>
		/// This plays the file asynchronously and returns immediately.
		/// </summary>
		/// <param name="filename">path to audio file to play</param>
		public void Play(string filename)
		{
			//MediaPlayer mp = new MediaPlayer();

			//mediaPlayer.MediaEnded += new EventHandler(OnMediaEnded);
			mediaPlayer.Open(new Uri(filename));
			mediaPlayer.Play();
		}

		private void OnMediaEnded(object sender, EventArgs e)
		{
			// Close the player once it finished playing. You could also set a flag here or raise another event.
			((MediaPlayer)sender).Close();
		}
	}
}
