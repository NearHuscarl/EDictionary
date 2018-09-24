using System.Diagnostics;

namespace EDictionary.Core.Utilities
{
	public class Watcher
	{
		private Stopwatch watch;

		public Watcher()
		{
			Start();
		}

		public void Start()
		{
			watch = Stopwatch.StartNew();
		}

		public void Print(string prompt)
		{
			Debug.WriteLine(prompt + ": " + watch.ElapsedMilliseconds + "ms");
			watch.Restart();
		}
	}
}
