// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Collections.Generic;

namespace NeGodAndre.Managers.Save {
	public class SaveDataContainer {
		public Dictionary<Type, ISaveData> SaveDatas   = new Dictionary<Type, ISaveData>();
		public Dictionary<string, string>  UnusedDatas = new Dictionary<string, string>();
	}
}