using System;
using System.Collections.Generic;
using System.Linq;

namespace EDictionary.Core.Utilities
{
	public class SpellCheck
	{
		public HashSet<string> Vocabulary { get; set; }
		private const string alphabet = "abcdefghijklmnopqrstuvwxyz";
		private Func<string, bool> IsValidWord;

		/// <summary>
		/// initialize Vocabulary for SpellCheck class
		/// </summary>
		public SpellCheck(IEnumerable<string> wordList)
		{
			Vocabulary = new HashSet<string>(wordList);
			IsValidWord = word => Vocabulary.Contains(word);

			foreach (var word in wordList)
				Vocabulary.Add(word.ToLower());
		}

		/// <summary>
		/// Return an IEnumarable of words (valid or not) that are one edit away from the given word
		/// <para/>
		/// Example: 'strike' would return {'trike', 'tsrike', 'atrike', 'astrike', ...}
		/// </summary>
		public IEnumerable<string> Edits1(string word)
		{
			var splits = from i in Enumerable.Range(0, word.Length)
				select new {a = word.Substring(0, i), b = word.Substring(i)};

			var deletes = from s in splits
				where s.b != "" // we know it can't be null
				select s.a + s.b.Substring(1);

			var transposes = from s in splits
				where s.b.Length > 1
				select s.a + s.b[1] + s.b[0] + s.b.Substring(2);

			var replaces = from s in splits
				from c in alphabet
				where s.b != ""
				select s.a + c + s.b.Substring(1);

			var inserts = from s in splits
				from c in alphabet
				select s.a + c + s.b;

			return deletes
				.Union(transposes) // union translates into a set
				.Union(replaces)
				.Union(inserts);
		}

		/// <summary>
		/// Return an IEnumarable of words that are two edits away from the given word
		/// See Edits1(string)
		/// </summary>
		private IEnumerable<string> Edits2(string word)
		{
			return (
					from e1 in Edits1(word)
					from e2 in Edits1(e1)
					select e2
					).Distinct();
		}

		/// <summary>
		/// return a list of valid words from another wordlist
		/// </summary>
		private IEnumerable<string> Known(IEnumerable<string> words)
		{
			return words.Where(word => IsValidWord(word));
		}

		/// <summary>
		/// return a list of candidates for the wrong spelling word
		/// </summary>
		public IEnumerable<string> Candidates(string word)
		{
			var candidates = new[] {
					Known(new[] {word}),
					Known(Edits1(word)),
					Known(Edits2(word)),
					new[] {word},
					}.First(knowns => knowns.Any());

			return candidates;
		}

		public void ReadFromStdIn()
		{
			string word;

			while (!string.IsNullOrEmpty(word = Console.ReadLine().Trim()))
			{
				foreach (var candidate in Candidates(word))
				{
					Console.Write(candidate + " ");
				}
				Console.WriteLine();
			}
		}
	}
}
