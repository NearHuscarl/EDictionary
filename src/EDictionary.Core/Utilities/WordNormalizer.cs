using Iveonik.Stemmers;
using LemmaSharp.Classes;
using System.IO;

namespace EDictionary.Core.Utilities
{
	public class WordNormalizer
	{
		private IStemmer stemmer;
		private Lemmatizer lemmatizer;

		public WordNormalizer()
		{
			stemmer = new EnglishStemmer();

			var path = Path.Combine(ApplicationPath.BaseDirectory, "lemmatizer-en.lem");
			var stream = File.OpenRead(path);

			lemmatizer = new Lemmatizer(stream);
		}

		public string Stem(string word)
		{
			return stemmer.Stem(word);
		}

		public string Lemmatize(string word)
		{
			return lemmatizer.Lemmatize(word);
		}
	}
}
