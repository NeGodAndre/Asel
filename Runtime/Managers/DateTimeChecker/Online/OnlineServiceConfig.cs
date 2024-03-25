// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;

namespace NeGodAndre.Managers.DateTimeChecker.Online {
	[Serializable]
	public struct OnlineServiceConfig {
		public string URL;
		public string FieldDateTime;
		public string FieldError;
	}
}