// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System.Collections.Generic;
using NeGodAndre.Managers.DateTimeChecker;
using NeGodAndre.Managers.DateTimeChecker.HTTP;
using NeGodAndre.Managers.DateTimeChecker.Online;
using NeGodAndre.Utils;
using Newtonsoft.Json;
using UnityEditor;

namespace NeGodAndre.Editor {
	public static class SampleConfigsCreator {
#if UNITY_EDITOR
		[MenuItem("Tools/NeGodAndre/ConfigSamples/DateTimeCheckerConfig")]
		private static void CreateDateTimeCheckerConfig() {
			var config = new DateTimeCheckerConfig();
			config.OnlineServiceConfigs = new List<OnlineServiceConfig> { new OnlineServiceConfig() };
			config.HTTPHeaderConfigs = new List<HTTPHeaderConfig> { new HTTPHeaderConfig() };
			FileUtils.WriteString("Assets/SampleConfigs/DateTimeCheckerConfig", 
				JsonConvert.SerializeObject(config));
		}
#endif
	}
}