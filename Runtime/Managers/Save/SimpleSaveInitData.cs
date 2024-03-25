// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using NeGodAndre.Cryptography;
using NeGodAndre.Managers.Save.Interfaces;

namespace NeGodAndre.Managers.Save {
	public struct SimpleSaveInitData {
		public SaveDataContainer       Container;
		public BaseCryptographySetting CryptographySetting;
		public IStorage                Storage;
		public ISaveMigration          Migration;

		public SimpleSaveInitData(SaveDataContainer container, BaseCryptographySetting cryptographySetting, IStorage storage) {
			Container = container;
			CryptographySetting = cryptographySetting;
			Storage = storage;
			Migration = null;
		}
		
		public SimpleSaveInitData(SaveDataContainer container, BaseCryptographySetting cryptographySetting, IStorage storage, ISaveMigration saveMigration) 
			: this(container, cryptographySetting, storage) {
			Migration = saveMigration;
		}
	}
}