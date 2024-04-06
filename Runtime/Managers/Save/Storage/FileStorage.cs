// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using NeGodAndre.Managers.Logger;
using NeGodAndre.Managers.Save.Interfaces;
using NeGodAndre.Utils;
using NeGodAndre.Utils.SettingPath;

namespace NeGodAndre.Managers.Save.Storage {
	public class FileStorage : IStorage {
		private readonly SettingPath _settingPath;
		private readonly string      _extension;

		public FileStorage(SettingPath settingPath, string extension) {
			_settingPath = settingPath;
			_extension = extension;
		}
		
		public async UniTask Write(string name, string data) {
			try {
				await FileUtils.WriteStringAsync(Path.Combine(SettingPathHelper.GetPath(_settingPath), name + _extension), data);
			} catch (Exception e) {
				LoggerManager.LogError(e);
			}
		}

		public async UniTask<string> Read(string name) {
			try {
				return await FileUtils.ReadStringAsync(Path.Combine(SettingPathHelper.GetPath(_settingPath), name + _extension));
			} catch ( Exception e ) {
				LoggerManager.LogError(e);
				return string.Empty;
			}
		}

		public List<string> GetNameSaveList() {        
			var fileNamesWithoutExtension = new List<string>();
			var path = SettingPathHelper.GetPath(_settingPath);
			if ( !Directory.Exists(path) ) {
				return new List<string>();
			}
			var files = Directory.GetFiles(path, "*" + _extension);
			foreach (var filePath in files)
			{
				var fileName = Path.GetFileNameWithoutExtension(filePath);
				fileNamesWithoutExtension.Add(fileName);
			}
			return fileNamesWithoutExtension;
		}
	}
}