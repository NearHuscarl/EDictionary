using EDictionary.Core.Learner.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EDictionary.Core.Learner.Utilities
{
	public class KeyCombination
	{
		public List<Keys> Modifiers { get; private set; }
		public Keys TriggerKey { get; private set; }

		private KeyCombination(Keys triggerKey, List<Keys> modifiers)
		{
			TriggerKey = triggerKey;
			Modifiers = modifiers;
		}

		/// <summary>
		///     TriggeredBy a chord from any string like this 'Alt+Shift+R'.
		///     Nothe that the trigger key must be the last one.
		/// </summary>
		public static KeyCombination FromString(string keyCombination)
		{
			var parts = keyCombination
					.Split('+')
					.Select(p => p.ToKey());

			var keys = new List<Keys>(parts);
			var triggerKey = keys.Pop();

			return new KeyCombination(triggerKey, keys);
		}
	}
}
