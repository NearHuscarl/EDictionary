using EDictionary.Core.Models;
using Newtonsoft.Json;

namespace EDictionary.Core.Utilities
{
	public static class JsonHelper
	{
		static JsonHelper()
		{
			JsonConvert.DefaultSettings = () => new JsonSerializerSettings
			{
				ContractResolver = new WordContractResolver()
			};
		}

		public static Word Deserialize(string strJson)
		{
			return JsonConvert.DeserializeObject<Word>(strJson);
		}
	}
}
