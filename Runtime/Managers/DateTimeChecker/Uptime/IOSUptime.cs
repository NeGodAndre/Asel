// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

#if !UNITY_EDITOR && UNITY_IOS
using System.Runtime.InteropServices;

namespace NeGodAndre.Managers.DateTimeChecker.Uptime {
	internal static class IOSUptime {
		[DllImport("__Internal")]
		private static extern double _Uptime();

		public static double GetUptime() {
			return _Uptime();
		}
	}
}
#endif