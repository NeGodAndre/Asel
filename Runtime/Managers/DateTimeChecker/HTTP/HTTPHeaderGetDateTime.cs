// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using NeGodAndre.Managers.Logger;
using NeGodAndre.Utils;

namespace NeGodAndre.Managers.DateTimeChecker.HTTP {
	internal class HTTPHeaderGetDateTime : BaseGetDateTime {
		public          bool IsUpdateTime { get { return _isUpdate; } }
		public override bool IsReliable   { get { return true;      } }

		private bool             _isUpdate;
		private HTTPHeaderConfig _config;
		
		private const int COUNT_ATTEMPT = 3;

		public HTTPHeaderGetDateTime(HTTPHeaderConfig config) {
			_config = config;
		}

		public override void TryUpdateDateTime() {
			Download().Forget();
		}

		private async UniTaskVoid Download() {
			try {
				var data = await DownloadUtils.DownloadHeaders(_config.URL, COUNT_ATTEMPT);
				DownloadComplete(data);
			}
			catch (Exception error)
			{
				LoggerManager.LogError("HTTPHeaderGetDateTime: " + error.Message);
				OnFailTimeUpdate?.Invoke();
			}
		}

		private void DownloadComplete(Dictionary<string, string> result) {
			if ( (result == null) || (result.Count == 0) ) { 
				LoggerManager.LogError("HTTPHeaderGetDateTime: Result is null!");
				OnFailTimeUpdate?.Invoke();
				return;
			}
			if ( !result.TryGetValue("Date", out var header) ) {
				LoggerManager.LogError("HTTPHeaderGetDateTime: Result is null!");
				OnFailTimeUpdate?.Invoke();
				return;
			}
			if ( !DateTime.TryParse(header, out var dateTime) ) {
				LoggerManager.LogError("HTTPHeaderGetDateTime: Error parse date \"{0}\"!!!", result);
				OnFailTimeUpdate?.Invoke();
				return;
			}
			DateTime = dateTime.ToUniversalTime();
			_isUpdate = true;
			OnSuccessTimeUpdate?.Invoke();
		}
	}
}
