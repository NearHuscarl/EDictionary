using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EDictionary.Core.Learner.Utilities
{
	public delegate void KeyHookEventHandler(object sender, KeyInfo keyInfo);

	public struct KeyInfo
	{
		public Keys KeyData;
		public List<Keys> KeysHold;

		public KeyInfo(Keys keyData, List<Keys> keysHold)
		{
			KeyData = keyData;
			KeysHold = keysHold;
		}
	}

	public class GlobalKeyboardHookInternal
	{
		public GlobalKeyboardHookInternal()
		{
			llkh = new LLKeyboardHook(HookProc);
		}

		~GlobalKeyboardHookInternal()
		{
			Stop();
		}

		public delegate int LLKeyboardHook(int Code, int wParam, ref KeyBoardHookStruct lParam);

		public struct KeyBoardHookStruct
		{
			public int vkCode;
			public int scanCode;
			public int flags;
			public int time;
			public int dwExtraInfo;
		}

		const int WH_KEYBOARD_LL = 13;
		const int WM_KEYDOWN = 0x0100;
		const int WM_KEYUP = 0x0101;
		const int WM_SYSKEYDOWN = 0x0104;
		const int WM_SYSKEYUP = 0x0105;

		LLKeyboardHook llkh;
		IntPtr hookID = IntPtr.Zero;

		public List<Keys> HookedKeys = new List<Keys>();

		public List<Keys> KeysHold { get; private set; } = new List<Keys>();
		public List<Keys> Chords { get; private set; } = new List<Keys>();

		public event KeyEventHandler KeyDown;
		public event KeyEventHandler KeyUp;
		public event KeyEventHandler KeyPressed;
		//public event KeyHookEventHandler KeyPressed;

		public void Start()
		{
			IntPtr hInstance = LoadLibrary("User32");
			hookID = SetWindowsHookEx(WH_KEYBOARD_LL, llkh, hInstance, 0);
		}

		public void Stop()
		{
			UnhookWindowsHookEx(hookID);
		}

		private int HookProc(int code, int wParam, ref KeyBoardHookStruct lParam)
		{
			if (code >= 0)
			{
				Keys currentKey = (Keys)lParam.vkCode;

				if (HookedKeys.Contains(currentKey))
				{
					KeyEventArgs kArg = new KeyEventArgs(currentKey);

					if (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN)
					{
						KeyDown?.Invoke(this, kArg);

						if (!KeysHold.Contains(currentKey))
							KeysHold.Add(currentKey);

						Chords = KeysHold.Where(keyHold => keyHold != currentKey).ToList();
					}
					else if (wParam == WM_KEYUP || wParam == WM_SYSKEYUP)
					{
						KeyUp?.Invoke(this, kArg);

						if (!Chords.Contains(currentKey))
							KeyPressed?.Invoke(this, kArg);

						KeysHold.Remove(currentKey);
						Chords.Remove(currentKey);

						//KeyPressed?.Invoke(this, new KeyInfo(key, keysHold));
					}

					if (kArg.Handled)
					{
						return 1;
					}
				}
			}
			return CallNextHookEx(hookID, code, wParam, ref lParam);
		}


		[DllImport("user32.dll")]
		static extern int CallNextHookEx(IntPtr hhk, int code, int wParam, ref KeyBoardHookStruct lParam);

		[DllImport("user32.dll")]
		static extern IntPtr SetWindowsHookEx(int idHook, LLKeyboardHook callback, IntPtr hInstance, uint theardID);

		[DllImport("user32.dll")]
		static extern bool UnhookWindowsHookEx(IntPtr hInstance);

		[DllImport("kernel32.dll")]
		static extern IntPtr LoadLibrary(string lpFileName);
	}
}
