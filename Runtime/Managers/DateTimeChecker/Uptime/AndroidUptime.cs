// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

#if !UNITY_EDITOR && UNITY_ANDROID
using UnityEngine;

namespace NeGodAndre.Managers.DateTimeChecker.Uptime {
	internal class AndroidUptime {
		public static double GetUptime() {
			var jc = new AndroidJavaClass("android.os.SystemClock");
			var time = jc.CallStatic<long>("elapsedRealtime");
			return time / (double)1000;
		}
	}
}
#endif