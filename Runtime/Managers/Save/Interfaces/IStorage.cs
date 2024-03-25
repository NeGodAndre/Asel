// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace NeGodAndre.Managers.Save.Interfaces {
	public interface IStorage {
		public UniTask         Write(string name, string data);
		public UniTask<string> Read(string  name);
		public List<string>    GetNameSaveList();
	}
}