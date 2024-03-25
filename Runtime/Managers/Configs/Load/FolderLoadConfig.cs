// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using NeGodAndre.Utils;
using NeGodAndre.Utils.SettingPath;
using UnityEngine;

namespace NeGodAndre.Managers.Configs.Load {
	public sealed class FolderLoadConfig : ILoadConfig {
		public int Priority { get; }
		
		private readonly string _localPath = string.Empty;
		
		public FolderLoadConfig(List<SettingPath> localSettingPaths, int priority = 0) {
			Priority = priority;
			if ( Application.platform == RuntimePlatform.WebGLPlayer ) {
				return;
			}
			var localSettingPath = SettingPathHelper.ChooseSettingPath(localSettingPaths);
			if ( localSettingPath == null ) {
				_localPath = string.Empty;
				return;
			}
			_localPath = SettingPathHelper.GetPath(localSettingPath);
		}

		public async UniTask<string> Load(string name) {
			if ( Application.platform == RuntimePlatform.WebGLPlayer ) {
				return string.Empty;
			}
			try {
				return await FileUtils.ReadStringAsync(Path.Combine(_localPath, name));
			} catch {
				return string.Empty;
			}
		}
	}
}