using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace EDictionary.Core.Learner.Utilities
{
	public delegate void MouseClickEventHandler(object sender, Point position);

	public class GlobalMouseHookInternal
	{
		private const int WH_MOUSE_LL = 14;

		private enum MouseMessages
		{
			WM_MOUSEMOVE = 0x0200,
			WM_LBUTTONDOWN = 0x0201,
			WM_LBUTTONUP = 0x0202,
			WM_LBUTTONDBLCLK = 0x0203,
			WM_RBUTTONDOWN = 0x0204,
			WM_RBUTTONUP = 0x0205,
			WM_RBUTTONDBLCLK = 0x0206,
			WM_MBUTTONDOWN = 0x0207,
			WM_MBUTTONUP = 0x0208,
			WM_MBUTTONDBLCLK = 0x0209,

			WM_MOUSEWHEEL = 0x020A,
			WM_MOUSEHWHEEL = 0x020E,

			WM_NCMOUSEMOVE = 0x00A0,
			WM_NCLBUTTONDOWN = 0x00A1,
			WM_NCLBUTTONUP = 0x00A2,
			WM_NCLBUTTONDBLCLK = 0x00A3,
			WM_NCRBUTTONDOWN = 0x00A4,
			WM_NCRBUTTONUP = 0x00A5,
			WM_NCRBUTTONDBLCLK = 0x00A6,
			WM_NCMBUTTONDOWN = 0x00A7,
			WM_NCMBUTTONUP = 0x00A8,
			WM_NCMBUTTONDBLCLK = 0x00A9
		}

		public event MouseClickEventHandler LeftButtonDown = delegate { };
		public event MouseClickEventHandler LeftButtonUp = delegate { };
		public event MouseClickEventHandler RightButtonDown = delegate { };
		public event MouseClickEventHandler RightButtonUp = delegate { };

		public GlobalMouseHookInternal()
		{
			proc = new LowLevelMouseProc(HookCallback);
		}

		public void Start()
		{
			hookID = SetHook(proc);
		}

		public void Stop()
		{
			UnhookWindowsHookEx(hookID);
		}

		private LowLevelMouseProc proc;
		private IntPtr hookID = IntPtr.Zero;

		private IntPtr SetHook(LowLevelMouseProc proc)
		{
			using (Process curProcess = Process.GetCurrentProcess())
			using (ProcessModule curModule = curProcess.MainModule)
			{
				return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
			}
		}

		private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

		private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode >= 0 && MouseMessages.WM_LBUTTONDOWN == (MouseMessages)wParam)
			{
				MouseMessages mouseState = (MouseMessages)wParam;
				MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

				switch (mouseState)
				{
					case MouseMessages.WM_LBUTTONDOWN:
						LeftButtonDown(this, new Point(hookStruct.pt.x, hookStruct.pt.y));
						break;

					case MouseMessages.WM_LBUTTONUP:
						LeftButtonUp(this, new Point(hookStruct.pt.x, hookStruct.pt.y));
						break;

					case MouseMessages.WM_RBUTTONDOWN:
						RightButtonDown(this, new Point(hookStruct.pt.x, hookStruct.pt.y));
						break;

					case MouseMessages.WM_RBUTTONUP:
						RightButtonUp(this, new Point(hookStruct.pt.x, hookStruct.pt.y));
						break;
				}
			}
			return CallNextHookEx(hookID, nCode, wParam, lParam);
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct POINT
		{
			public int x;
			public int y;
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct MSLLHOOKSTRUCT
		{
			public POINT pt;
			public uint mouseData;
			public uint flags;
			public uint time;
			public IntPtr dwExtraInfo;
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)] [return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool UnhookWindowsHookEx(IntPtr hhk);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetModuleHandle(string lpModuleName);
	}
}
