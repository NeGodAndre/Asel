// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using NeGodAndre.Managers.Save.Interfaces;
using UnityEngine;

namespace NeGodAndre.Managers.Save.Storage {
	public class PlayerPrefsStorage : IStorage {
		private const string SERVICE_STRING = "files_storage_service";
		
		public UniTask Write(string name, string data) {
			var rawFiles = PlayerPrefs.GetString(SERVICE_STRING, string.Empty).Split("|").ToHashSet();
			rawFiles.Add(name);
			PlayerPrefs.SetString(SERVICE_STRING, string.Join("|", rawFiles));
			PlayerPrefs.SetString(name, data);
			PlayerPrefs.Save();
			return UniTask.CompletedTask;
		}

		public UniTask<string> Read(string name) {
			var data = PlayerPrefs.GetString(name, string.Empty);
			return UniTask.FromResult(data);
		}

		public List<string> GetNameSaveList() {
			var rawFiles = PlayerPrefs.GetString(SERVICE_STRING, string.Empty);
			return rawFiles.Split("|").ToList();
		}
	}
}