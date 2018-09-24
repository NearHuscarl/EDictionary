namespace EDictionary.Core.Models.WordComponents
{
	public class Definition
	{
		public string Property { get; set; }
		public string Label { get; set; }
		public string Refer { get; set; }
		public Reference[] References { get; set; }
		public string Description { get; set; }
		public string[] Examples { get; set; }
	}
}
