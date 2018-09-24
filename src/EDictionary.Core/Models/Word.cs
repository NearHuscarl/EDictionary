using EDictionary.Core.Extensions;
using EDictionary.Core.Models.WordComponents;
using EDictionary.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDictionary.Core.Models
{
	public class Word
	{
		public static readonly string AudioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "audio");
		private static AudioManager audioPlayer = new AudioManager();

		public string ID { get; set; }
		public string[] Similars { get; set; }
		public string Name { get; set; }
		public string Wordform { get; set; }
		public Pronunciation[] Pronunciations { get; set; }
		public Reference[] References { get; set; }
		public DefinitionGroup[] DefinitionsExamples { get; set; }
		public string[] ExtraExamples { get; set; }
		public Idiom[] Idioms { get; set; }

		public string ToDisplayedString()
		{
			List<Task<string>> tasks = new List<Task<string>>();

			tasks.Add(Task.Run(() => new StringBuilder().AppendReferences(References).ToString()));
			tasks.Add(Task.Run(() => new StringBuilder().AppendDefinitionGroups(DefinitionsExamples).ToString()));
			tasks.Add(Task.Run(() => new StringBuilder().AppendExtraExamples(ExtraExamples).ToString()));
			tasks.Add(Task.Run(() => new StringBuilder().AppendIdioms(Idioms).ToString()));

			Task.WaitAll(tasks.ToArray());

			StringBuilder builder = new StringBuilder();

			foreach (var task in tasks)
			{
				builder.Append(task.Result);
			}

			return builder.ToString();
		}

		private string GetFilename(Dialect dialect)
		{
			return Pronunciations
				.Where(x => x.Prefix == dialect.ToString())
				.Select(x => x.Filename)
				.First();
		}

		public void PlayAudio(Dialect dialect)
		{
			string filename = GetFilename(dialect);
			string audioFile = Path.Combine(AudioPath, filename);

			audioPlayer.Play(audioFile);
		}
	}
}
