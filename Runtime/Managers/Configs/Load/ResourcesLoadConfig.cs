// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System.IO;
using Cysharp.Threading.Tasks;
using NeGodAndre.Utils;
using UnityEngine;

namespace NeGodAndre.Managers.Configs.Load {
	public sealed class ResourcesLoadConfig : ILoadConfig {
		public int Priority { get; }
		
		private readonly string _folder;
		
		public ResourcesLoadConfig(string folder, int priority = 0) {
			Priority = priority;
			_folder = folder;
		}

		public async UniTask<string> Load(string name) {
			try {
				var textAsset = await ResourcesUtils.LoadAsync<TextAsset>(Path.Combine(_folder, name));
				return textAsset ? textAsset.text : string.Empty;
			} catch {
				Debug.LogErrorFormat("ResourcesLoadConfig: Config {0} doesn't find!!!", name);
				return string.Empty;
			}
		}
	}
}