using System;
using System.Windows.Forms;

namespace EDictionary.Core.Learner.Utilities
{
	public class GlobalKeyboardHook
	{
		private GlobalKeyboardHookInternal keyboardHook = new GlobalKeyboardHookInternal();

		public event KeyEventHandler KeyDown
		{
			add { keyboardHook.KeyDown += value; }
			remove { keyboardHook.KeyDown -= value; }
		}

		public event KeyEventHandler KeyUp
		{
			add { keyboardHook.KeyUp += value; }
			remove { keyboardHook.KeyUp -= value; }
		}

		public event KeyEventHandler KeyPressed
		{
			add { keyboardHook.KeyPressed += value; }
			remove { keyboardHook.KeyPressed -= value; }
		}

		public GlobalKeyboardHook()
		{
			SetupHooks();
		}

		private void SetupHooks()
		{
			// Track all keys
			foreach (Keys key in Enum.GetValues(typeof(Keys)))
			{
				keyboardHook.HookedKeys.Add(key);
			}
		}

		private void RemoveHooks() => keyboardHook.HookedKeys.Clear();

		public void StartHook()
		{
			SetupHooks();
			keyboardHook.Start();
		}

		public void StopHook()
		{
			RemoveHooks();
			keyboardHook.Stop();
		}

		public void Assign(KeyCombination combination, Action action)
		{
			CreateHookIfNecessary(combination);

			KeyPressed += (sender, e) =>
			{
				foreach (var modifier in combination.Modifiers)
				{
					if (!keyboardHook.KeysHold.Contains(modifier))
						return;
				}

				if (combination.TriggerKey == e.KeyData)
				{
					action?.Invoke();
				}
			};
		}

		public void CreateHookIfNecessary(KeyCombination combination)
		{
			if (!keyboardHook.HookedKeys.Contains(combination.TriggerKey))
				keyboardHook.HookedKeys.Add(combination.TriggerKey);

			foreach (var modifier in combination.Modifiers)
			{
				if (!keyboardHook.HookedKeys.Contains(modifier))
					keyboardHook.HookedKeys.Add(modifier);
			}
		}
	}
}
