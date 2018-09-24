using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace EDictionary.Core.Utilities
{
	public class WordContractResolver : DefaultContractResolver
	{
		private Dictionary<string, string> PropertyMappings { get; set; }

		public WordContractResolver()
		{
			PropertyMappings = new Dictionary<string, string>
			{
				{"ID", "id"},
				{"Similars", "similars"},
				{"Name", "name"},
				{"Wordform", "wordform"},
				{"Pronunciations", "pronunciations"},
				{"References", "references"},
				{"DefinitionsExamples", "definitions"},
				{"ExtraExamples", "extra_examples"},
				{"Idioms", "idioms"},

				{"Prefix", "prefix"},
				{"Ipa", "ipa"},
				{"Filename", "filename"},

				{"Namespace", "namespace"},
				{"Definitions", "definitions"},
				{"Property", "property"},
				{"Label", "label"},
				{"Refer", "refer"},
				{"Description", "description"},
				{"Examples", "examples"},
			};
		}

		protected override string ResolvePropertyName(string propertyName)
		{
			string resolvedName = null;

			PropertyMappings.TryGetValue(propertyName, out resolvedName);
			return resolvedName ?? base.ResolvePropertyName(propertyName);
		}
	}

}
