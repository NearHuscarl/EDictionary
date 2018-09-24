using EDictionary.Core.Models.WordComponents;
using EDictionary.Theme.Utilities;
using System.Drawing;
using System.Text;

namespace EDictionary.Core.Extensions
{
	public static class StringBuilderExtension
	{
		public static StringBuilder AppendHeadline(this StringBuilder builder, string str)
		{
			builder.AppendLine(str);
			builder.AppendLine();

			return builder;
		}

		public static StringBuilder AppendReferences(this StringBuilder builder, Reference[] references)
		{
			if (references == null)
				return builder;

			foreach (var reference in references)
				builder.AppendLine("🡒 " + reference.Name);

			builder.AppendLine();

			return builder;
		}

		public static StringBuilder AppendDefinitionGroups(this StringBuilder builder, DefinitionGroup[] defGroups)
		{
			int nsIndex = 0;

			foreach (var group in defGroups)
			{
				if (group.Namespace != null)
					builder.AppendHeadline($"{++nsIndex}. {group.Namespace}");

				builder.AppendDefinitions(group.Definitions);
			}

			return builder;
		}

		public static StringBuilder AppendDefinitions(this StringBuilder builder, Definition[] definitions)
		{
			foreach (var definition in definitions)
				builder.AppendDefinition(definition);

			return builder;
		}

		public static StringBuilder AppendDefinition(this StringBuilder builder, Definition definition)
		{
			builder.Append("  ");

			if (definition.Label != null)
			{
				if (definition.Label[0] != '(')
					builder.Append($"({definition.Label}) ");
				else
					builder.Append(definition.Label + " ");
			}

			if (definition.Refer != null)
			{
				builder.Append(definition.Refer);
			}

			if (definition.Property != null)
			{
				builder.Append(definition.Property + " ");
			}

			if (definition.Description != null)
				builder.AppendLine(definition.Description);

			builder.AppendExamples(definition.Examples);
			builder.AppendReferences(definition.References);

			return builder;
		}

		public static StringBuilder AppendExamples(this StringBuilder builder, string[] examples)
		{
			if (examples.Length == 0)
				return builder;

			foreach (var example in examples)
			{
				builder.AppendLine("  • " + example);
			}

			builder.AppendLine();

			return builder;
		}

		public static StringBuilder AppendExtraExamples(this StringBuilder builder, string[] extraExamples)
		{
			if (extraExamples == null || extraExamples.Length == 0)
				return builder;

			builder.AppendHeadline("Other Examples");

			string bullet = " ◦ ";

			foreach (var example in extraExamples)
				builder.AppendLine(bullet + example);

			builder.AppendLine();

			return builder;
		}

		public static StringBuilder AppendIdioms(this StringBuilder builder, Idiom[] idioms)
		{
			if (idioms == null || idioms.Length == 0)
				return builder;

			builder.AppendHeadline("Idioms");

			foreach (var idiom in idioms)
			{
				builder.AppendLine(idiom.Name);

				foreach (var definition in idiom.Definitions)
					builder.AppendDefinition(definition);
			}

			return builder;
		}
	}
}
