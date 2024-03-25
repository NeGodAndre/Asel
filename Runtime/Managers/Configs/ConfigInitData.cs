// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System.Collections.Generic;
using NeGodAndre.Managers.Configs.Load;

namespace NeGodAndre.Managers.Configs {
	public struct ConfigInitData {
		public ConfigsSetting    ConfigsSetting;
		public List<ILoadConfig> LoadConfigs;
	}
}