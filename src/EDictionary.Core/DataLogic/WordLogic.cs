using EDictionary.Core.Data;
using EDictionary.Core.Data.Factory;
using EDictionary.Core.Extensions;
using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDictionary.Core.DataLogic
{
	/// <summary>
	/// Logic to manipulate Word model: Search for definition, play audio...
	/// </summary>
	public class WordLogic
   {
		private DataAccess dataAccess;
		private SpellCheck spellCheck;
		private WordNormalizer wordNormalizer;

		public Dictionary<string, List<string>> NameToIDs { get; set; }
		public List<string> WordList
		{
			get
			{
				var wordList = NameToIDs.Keys.ToList();
				wordList.Sort();

				return wordList;
			}
		}

		public WordLogic()
		{
			dataAccess = DatabaseFactory.CreateWordlistDatabase();

			InitWordList();
			spellCheck = new SpellCheck(WordList);
			wordNormalizer = new WordNormalizer();
		}

		private void InitWordList()
		{
			NameToIDs = new Dictionary<string, List<string>>();
			List<string> wordIDs = GetWordIDList();
			List<string> wordNames = GetWordNameList();

			int currentIndex;

			for (int i = 0; i <= wordNames.Count - 1; i++)
			{
				string currentKey = wordNames[i].Trim().ToLower();
				NameToIDs.Add(currentKey, new List<string>() { wordIDs[i] });
				currentIndex = i;

				while (wordNames[currentIndex] == wordNames.NextItem(i))
				{
					NameToIDs[currentKey].Add(wordIDs[i + 1]);
					i++;
				}
			}
		}

		public List<string> GetWordIDList()
		{
			Result<List<string>> result = dataAccess.SelectID();

			if (result.Status != Status.Success)
			{
				LogWriter.Instance.WriteLine($"Error occured at GetWordIDList in class Dictionary: {result.Message}");
				return null;
			}

			return result.Data;
		}

		public List<string> GetWordNameList()
		{
			Result<List<string>> result = dataAccess.SelectName();

			if (result.Status != Status.Success)
			{
				LogWriter.Instance.WriteLine($"Error occured at GetWordNameList in class Dictionary: {result.Message}");
				return null;
			}

			return result.Data;
		}

		private Word GetWordDefinition(string wordID)
		{
			Result<string> result = dataAccess.SelectDefinitionFrom(wordID);

			if (result.Status != Status.Success)
			{
				LogWriter.Instance.WriteLine($"Error occured at Search in class Dictionary: {result.Message}");
				return null;
			}

			if (result.Data == null)
				return null;

			return JsonHelper.Deserialize(result.Data);
		}

		public Word Search(string word)
		{
			if (word == null)
				return null;

			word = word.Trim().ToLower();

			if (!NameToIDs.ContainsKey(word))
				return null;

			return GetWordDefinition(NameToIDs[word].FirstOrDefault());
		}

		public Word SearchID(string wordID)
		{
			return GetWordDefinition(wordID);
		}

		public string GetSuggestions(string wrongWord)
		{
			IEnumerable<string> candidates = spellCheck.Candidates(wrongWord);
			StringBuilder builder = new StringBuilder();

			builder.AppendLine();
			builder.AppendLine($"No match for \"{wrongWord}\" in the dictionary");
			builder.AppendLine();

			if (candidates.FirstOrDefault() == wrongWord)
				return builder.ToString();

			builder.AppendLine("Did you mean:");
			foreach (var candidate in candidates)
			{
				builder.AppendLine(" • " + candidate);
			}

			return builder.ToString();
		}

		/// <summary>
		/// Attempt to reduce the input word to a basic form
		/// If no word found after normalizing, return null
		/// </summary>
		public string Normalize(string word)
		{
			var normalizedWord = wordNormalizer.Stem(word);

			if (NameToIDs.ContainsKey(normalizedWord))
				return normalizedWord;

			normalizedWord = wordNormalizer.Lemmatize(word);

			if (NameToIDs.ContainsKey(normalizedWord))
				return normalizedWord;

			return null;
		}
	}
}
