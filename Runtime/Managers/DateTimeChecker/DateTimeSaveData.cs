// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System.Collections.Generic;
using NeGodAndre.Managers.Save;

namespace NeGodAndre.Managers.DateTimeChecker {
	public class DateTimeSaveData : ISaveData {
		public Dictionary<string, string> DateTimeDatas;
	}
}