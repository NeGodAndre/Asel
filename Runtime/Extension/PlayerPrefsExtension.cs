// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

// ReSharper disable once CheckNamespace
namespace UnityEngine {
	public static class PlayerPrefsExtension {
		public static void SetBool(string key, bool value) {
			PlayerPrefs.SetInt(key, value ? 1 : 0);
		}

		public static bool GetBool(string key) {
			return PlayerPrefs.GetInt(key) == 1;
		}
	}
}
