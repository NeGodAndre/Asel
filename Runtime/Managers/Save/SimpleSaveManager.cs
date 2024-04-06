// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using NeGodAndre.Cryptography;
using NeGodAndre.Managers.Logger;
using NeGodAndre.Managers.Save.Interfaces;
using NeGodAndre.Utils.Serializer;
using VContainer.Unity;
using VitalRouter;

namespace NeGodAndre.Managers.Save {
	public sealed class SimpleSaveManager : IInitializable, ISaveManager, IHandSaveManager {
		private sealed class HelpSaveDatas {
			public Dictionary<string, string> SaveDatas = new Dictionary<string, string>();

			public HelpSaveDatas(SaveDataContainer container) {
				if ( container == null ) {
					return;
				}
				AddRange(container);
			}

			public void AddRange(SaveDataContainer container) {
				foreach ( var data in container.UnusedDatas ) {
					SaveDatas.Add(data.Key, data.Value);
				}
				foreach ( var data in container.SaveDatas ) {
					SaveDatas.Add(data.Key.FullName!, JsonSerializer.Serialize(data.Value));
				}
			}
		}

		public bool IsInit { get; private set; }

		private readonly ICommandPublisher       _publisher;
		private readonly ISaveMigration          _saveMigration;
		private readonly IStorage                _storage;
		private readonly SaveDataContainer       _saveDataContainer;
		private readonly BaseCryptographySetting _cryptographySetting;

		private const string SAVE_NAME = "save";
		
		public SimpleSaveManager(ICommandPublisher publisher, SimpleSaveInitData simpleSaveInitData) {
			_publisher = publisher;
			_saveDataContainer = simpleSaveInitData.Container;
			_cryptographySetting = simpleSaveInitData.CryptographySetting;
			_storage = simpleSaveInitData.Storage;
			_saveMigration = simpleSaveInitData.Migration;
		}

		public void Initialize() {
			LoadLocalSaveDates().Forget();
		}

		public T GetSaveData<T>() where T : ISaveData {
			if ( _saveDataContainer.SaveDatas.TryGetValue(typeof(T), out var saveData) ) {
				return (T)saveData;
			}
			LoggerManager.LogError("SaveData type {0} isn't registered", typeof(T));
			return default(T);
		}

		public void Save() {
			SaveSaveData().Forget();
		}

		private bool ProcessingLoadData(string rawStrJson) {
			if ( _saveMigration != null ) {
				try { // precaution against crooked hands
					rawStrJson = _saveMigration.Migration(rawStrJson);
				} catch ( Exception e ) {
					LoggerManager.LogError(e);
				}
			}
			HelpSaveDatas helpSaveData;
			try {
				helpSaveData = JsonSerializer.Deserialize<HelpSaveDatas>(rawStrJson);
			} catch ( Exception e ) {
				LoggerManager.LogError(e);
				return false;
			}
			if ( helpSaveData == null ) {
				return false;
			}
			foreach ( var saveData in helpSaveData.SaveDatas ) {
				var type = Type.GetType(saveData.Key);
				if ( (type != null) && _saveDataContainer.SaveDatas.ContainsKey(type) ) {
					_saveDataContainer.SaveDatas[type] = JsonSerializer.Deserialize<ISaveData>(saveData.Value, type);
				} else {
					_saveDataContainer.UnusedDatas.Add(saveData.Key, saveData.Value);
					LoggerManager.LogWarning("Unknown key {0}, value : {1}", saveData.Key, saveData.Value);
				}
			}
			return true;
		}

		private async UniTask<string> GetRawSaveData() {
			var data = await _storage.Read(SAVE_NAME);
			if ( string.IsNullOrEmpty(data) ) {
				return string.Empty;
			}
			return CryptographyHelper.Decrypt(data, _cryptographySetting);
		}

		private async UniTask LoadLocalSaveDates() {
			var rawStrJson = await GetRawSaveData();
			if ( !string.IsNullOrEmpty(rawStrJson) ) {
				ProcessingLoadData(rawStrJson);
			}
			IsInit = true;
			_publisher.PublishAsync(new LoadCompleteSaveEvent()).Forget();
		}

		private async UniTask SaveSaveData() {
			try {
				var data = new HelpSaveDatas(_saveDataContainer);
				var strJson = JsonSerializer.Serialize<HelpSaveDatas>(data);
				var dataForWrite = CryptographyHelper.Encrypt(strJson, _cryptographySetting);
				await _storage.Write(SAVE_NAME, dataForWrite);
			} catch ( Exception exception ) {
				LoggerManager.LogError("SaveManager: SaveData saving failed!!! Exception: {0}", exception);
			}
		}
	}
}