// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using Cysharp.Threading.Tasks;
using NeGodAndre.Managers.Logger;
using NeGodAndre.Utils;
using Newtonsoft.Json.Linq;

namespace NeGodAndre.Managers.DateTimeChecker.Online {
	internal class OnlineServiceGetDateTime : BaseGetDateTime {
		public          bool IsUpdateTime { get { return _isUpdate; } }
		public override bool IsReliable   { get { return true;      } }

		private const int COUNT_ATTEMPT = 1;

		private bool                _isUpdate;
		private OnlineServiceConfig _config;

		public OnlineServiceGetDateTime(OnlineServiceConfig config) {
			_config = config;
		}

		public override void TryUpdateDateTime() {
			Download().Forget();
		}
		
		private async UniTaskVoid Download() {
			if ( string.IsNullOrEmpty(_config.URL) ) {
				
				LoggerManager.LogError("OnlineServiceGetDateTime: URL is empty!!!");
				OnFailTimeUpdate?.Invoke();
				return;
			}
			try {
				var data = await DownloadUtils.DownloadText(_config.URL, COUNT_ATTEMPT);
				DownloadComplete(data);
			} catch ( Exception exception ) {
				LoggerManager.LogError("OnlineServiceGetDateTime: Download for {0} fall with error: {1}", _config.URL, exception);
				OnFailTimeUpdate?.Invoke();
			}
		}

		private void DownloadComplete(string result) {
			if ( string.IsNullOrEmpty(result) ) {
				LoggerManager.LogError("OnlineServiceGetDateTime: For {0} Result online time is null!", _config.URL);
				OnFailTimeUpdate?.Invoke();
				return;
			}
			var json = JObject.Parse(result);
			if ( !string.IsNullOrEmpty((string)json[_config.FieldError]) ) {
				LoggerManager.LogError("OnlineServiceGetDateTime: For {0} Result online have error:{1}!!!", _config.URL, (string)json[_config.FieldError]);
				OnFailTimeUpdate?.Invoke();
				return;
			}
			DateTime dateTime;
			try {
				dateTime = (DateTime)json[_config.FieldDateTime];
			} catch {
				LoggerManager.LogError("OnlineServiceGetDateTime: URL: {0}. Format Exception Parse Date. String data: {1} and string for parse {2}!!!", 
					_config.URL, result, _config.FieldDateTime);
				OnFailTimeUpdate?.Invoke();
				return;
			}
			DateTime = dateTime.ToUniversalTime();
			OnSuccessTimeUpdate?.Invoke();
			_isUpdate = true;
		}
	}
}