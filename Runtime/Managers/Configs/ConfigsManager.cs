// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using NeGodAndre.Managers.Configs.Load;
using NeGodAndre.Managers.Logger;
using VContainer.Unity;
using VitalRouter;

namespace NeGodAndre.Managers.Configs {
	public sealed class ConfigsManager : IInitializable {
		public bool IsConfigLoad { get; private set; }
		
		private Dictionary<string, string> _stringConfigs;

		private readonly ICommandPublisher _publisher;
		private readonly List<ILoadConfig> _loadConfigs;
		private readonly HashSet<string>   _keys = new HashSet<string>();

		public ConfigsManager(ICommandPublisher publisher, ConfigInitData configInitData) {
			_publisher = publisher;
			_loadConfigs = configInitData.LoadConfigs;
			_loadConfigs.Sort((a, b) => b.Priority.CompareTo(a.Priority));
			_stringConfigs = new Dictionary<string, string>();
			_keys = new HashSet<string>(configInitData.ConfigsSetting.NameConfigs);
		}
		
		public void Initialize() {
			LoadConfigs();
		}

		public string GetConfig(string name) {
			if ( _stringConfigs.TryGetValue(name, out var config) ) {
				return config;
			}
			LoggerManager.LogError("ConfigsManager: Config type {0} isn't registered!!!", name);
			return string.Empty;
		}

		public void UpdateConfigs() {
			LoadConfigs();
		}
		
		private void LoadConfigs() {
			AsyncLoadConfigs().Forget();
		}

		private async UniTask AsyncLoadConfigs() {
			if ( (_loadConfigs == null) || (_loadConfigs.Count == 0) ) {
				LoggerManager.LogError("ConfigsManager: Manager doesn't have loaders!!!");
				return;
			}
			foreach ( var key in _keys ) {
				var result = string.Empty;
				foreach ( var loadConfig in _loadConfigs ) {
					result = await loadConfig.Load(key);
					if ( !string.IsNullOrEmpty(result) ) {
						break;
					}
				}
				if ( string.IsNullOrEmpty(result) ) {
					continue;
				}
				if ( _stringConfigs.ContainsKey(key) ) {
					_stringConfigs[key] = result;
				} else {
					_stringConfigs.Add(key, result);
				}
			}
			IsConfigLoad = true;
			_publisher.PublishAsync(new UpdateConfigsEvent()).Forget();
		}
	}
}
