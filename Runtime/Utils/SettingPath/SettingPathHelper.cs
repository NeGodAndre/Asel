// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System.Collections.Generic;
using System.IO;
using NeGodAndre.Managers.Logger;
using UnityEngine;

namespace NeGodAndre.Utils.SettingPath {
	public static class SettingPathHelper {
		public static string GetPath(SettingPath settingPath) {
			if ( settingPath == null ) {
				return string.Empty;
			}
			var path = string.Empty;
			switch ( settingPath.PathType ) {
				case PathType.Custom: {
					path = settingPath.CustomPath;
					break;
				}
				case PathType.Data: {
					path = Application.dataPath;
					break;
				}
				case PathType.PersistentData: {
					path = Application.persistentDataPath;
					break;
				}
			}
			return Path.Combine(path, settingPath.Folder);
		}
		
		public static SettingPath ChooseSettingPath(List<SettingPath> settingPaths) {
			var platform = Application.platform;
			var settingPath = settingPaths.Find((value) => value.Platform == platform);
			if ( (Application.platform == RuntimePlatform.WebGLPlayer) && (settingPath?.PathType != PathType.PersistentData) ) {
				settingPath = null;
				LoggerManager.LogError("WebGl support only PersistentData!!!");
			}
			return settingPath;
		}
	}
}