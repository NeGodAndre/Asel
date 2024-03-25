// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using UnityEngine;

namespace NeGodAndre.Utils.SettingPath {
	[Serializable]
	public class SettingPath {
		public RuntimePlatform Platform;
		public string          Folder;
		public PathType        PathType = PathType.PersistentData;
		public string          CustomPath;
	}
}