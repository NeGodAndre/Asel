// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System.Collections.Generic;
using NeGodAndre.Managers.DateTimeChecker.HTTP;
using NeGodAndre.Managers.DateTimeChecker.Online;

namespace NeGodAndre.Managers.DateTimeChecker {
	public struct DateTimeCheckerConfig {
		public List<OnlineServiceConfig> OnlineServiceConfigs;
		// ReSharper disable once InconsistentNaming
		public List<HTTPHeaderConfig>    HTTPHeaderConfigs;
	}
}