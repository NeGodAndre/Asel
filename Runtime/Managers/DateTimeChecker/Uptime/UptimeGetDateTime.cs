// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using NeGodAndre.Managers.Logger;
using NeGodAndre.Utils.Serializer;
using UnityEngine;

namespace NeGodAndre.Managers.DateTimeChecker.Uptime {
	internal class UptimeGetDateTime : BaseGetDateTime {
		private DateTimeUptimeData _dateTimeUptime;

		public override string GetDataForSave(DateTime dateTime) {
			_dateTimeUptime.DateTime = dateTime;
			_dateTimeUptime.Uptime = GetTimeUptime();
			_dateTimeUptime.DeviceModel = SystemInfo.deviceModel;
			return JsonSerializer.Serialize(_dateTimeUptime);
		}

		public override void SetDataForSave(string rawData) {
			try {
				_dateTimeUptime = JsonSerializer.Deserialize<DateTimeUptimeData>(rawData);
			} catch ( Exception e ) {
				LoggerManager.LogException(e);
				_dateTimeUptime = new DateTimeUptimeData();
			}
		}

		public override void TryUpdateDateTime() {
			if ( !_dateTimeUptime.DeviceModel.Equals(SystemInfo.deviceModel) ) {
				OnFailTimeUpdate?.Invoke();
				return;
			}
			var timeUpdate = GetTimeUptime();
			if ( (timeUpdate <= 0) || (_dateTimeUptime.DateTime == default(DateTime)) || (_dateTimeUptime.Uptime < 0) ) {
				OnFailTimeUpdate?.Invoke();
				return;
			}
			var uptimeTime = timeUpdate - _dateTimeUptime.Uptime;
			if ( uptimeTime < 0 ) {
				OnFailTimeUpdate?.Invoke();
				return;
			}
			DateTime = _dateTimeUptime.DateTime.AddSeconds(uptimeTime);
			OnSuccessTimeUpdate?.Invoke();
		}

		private double GetTimeUptime() {
			try {
#if !UNITY_EDITOR && UNITY_ANDROID
				return AndroidUptime.GetUptime();
#elif !UNITY_EDITOR && UNITY_IOS
				return IOSUptime.GetUptime();
#else
				return -1;
#endif
			} catch {
				return -1;
			}
		}
	}
}
