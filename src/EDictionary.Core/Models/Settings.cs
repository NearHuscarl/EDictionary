using EDictionary.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace EDictionary.Core.Models
{
	public enum VocabularySource
	{
		Full,
		Custom,
	}

	[Serializable]
	public class Settings
	{
		[XmlIgnore]
		public static readonly string Directory = Path.Combine(ApplicationPath.ApplicationData, "data");

		[XmlIgnore]
		public static readonly string FullPath = Path.Combine(Directory, "settings.xml");

		[XmlIgnore]
		public static Settings Default = new Settings()
		{
			RunAtStartup = true,
			IsLearnerEnabled = true,
			IsDynamicEnabled = true,

			MinInterval = 20,
			SecInterval = 0,
			VocabularySource = VocabularySource.Full,
			CustomWordList = new List<string>(),
			UseHistoryWordlist = true,
			UseCustomWordlist = false,
			Timeout = 12,

			AutoCopyToClipboard = true,
			UseTriggerKey = true,
			TriggerKey = "LControl",
		};

		public bool RunAtStartup { get; set; }
		public bool IsLearnerEnabled { get; set; }
		public bool IsDynamicEnabled { get; set; }

		public int MinInterval { get; set; }
		public int SecInterval { get; set; }

		public VocabularySource VocabularySource { get; set; }

		public List<string> CustomWordList { get; set; }

		public bool UseHistoryWordlist { get; set; }
		public bool UseCustomWordlist { get; set; }

		public int Timeout { get; set; }

		public bool AutoCopyToClipboard { get; set; }
		public bool UseTriggerKey { get; set; }
		public string TriggerKey { get; set; } = "";

		public Settings()
		{
			CustomWordList = new List<string>();
		}
	}
}
