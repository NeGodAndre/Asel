﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

namespace NeGodAndre.Managers.Save.Interfaces {
	public interface ISaveMigration {
		public string Migration(string rawSave);
	}
}