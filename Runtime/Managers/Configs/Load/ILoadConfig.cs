// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using Cysharp.Threading.Tasks;

namespace NeGodAndre.Managers.Configs.Load {
	public interface ILoadConfig {
		public int Priority { get; }
		
		UniTask<string> Load(string name);
	}
}