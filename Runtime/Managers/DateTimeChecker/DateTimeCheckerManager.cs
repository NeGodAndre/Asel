// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Collections.Generic;
using UnityEngine;
using NeGodAndre.Managers.Configs;
using NeGodAndre.Managers.DateTimeChecker.HTTP;
using NeGodAndre.Managers.DateTimeChecker.Online;
using NeGodAndre.Managers.DateTimeChecker.Uptime;
using NeGodAndre.Managers.Save;
using NeGodAndre.Managers.Save.Interfaces;
using NeGodAndre.Utils.Serializer;
using VContainer.Unity;
using VitalRouter;

namespace NeGodAndre.Managers.DateTimeChecker {
	public sealed class DateTimeCheckerManager : IInitializable, IDisposable, ITickable {
		public bool     IsTimeValidate { get; private set; }
		public DateTime GetDateTime    { get { return _dateTime; } }
		public DateTime GetDate        { get { return _dateTime.Date; } }

		private DateTimeSaveData      _saveData;
		private DateTimeCheckerConfig _config;
		
		private List<BaseGetDateTime> _getDateTimes;
		private int                   _indexActive;
		private DateTime              _dateTime;

		private readonly Router           _router;
		private readonly ISaveManager     _saveManager;
		private readonly IHandSaveManager _handSaveManager;
		private readonly ConfigsManager   _configsManager;

		public DateTimeCheckerManager(Router router, ISaveManager saveManager, ConfigsManager configsManager) {
			_router = router;
			_saveManager = saveManager;
			_configsManager = configsManager;
		}

		public DateTimeCheckerManager(Router router, ISaveManager saveManager, IHandSaveManager handSaveManager, ConfigsManager configsManager)
			: this(router, saveManager, configsManager) {
			_handSaveManager = handSaveManager;
		}

		public void Initialize() {
			_saveData = _saveManager.GetSaveData<DateTimeSaveData>();
			_dateTime = DateTime.UtcNow;
			IsTimeValidate = false;
			if ( _saveManager.IsInit && _configsManager.IsConfigLoad ) {
				UpdateConfig();
				InitGetDateTime();
				LoadData();
				StartLoad();
			}
			_router.Subscribe<UpdateConfigsEvent>(UpdateConfigsHandler);
			_router.Subscribe<LoadCompleteSaveEvent>(LoadCompleteSaveHandler);
			
		}

		public void Dispose() {
		}

		public void Tick() {
			if ( !IsTimeValidate ) {
				return;
			}
			_dateTime = GetDateTime.AddSeconds(Time.unscaledDeltaTime);
		}

		private void InitGetDateTime() {
			if ( (_getDateTimes != null) && (_getDateTimes.Count > 0) ) {
				foreach ( var getTine in _getDateTimes ) {
					getTine.OnSuccessTimeUpdate -= SuccessGetTimeHandler;
					getTine.OnFailTimeUpdate -= FailGetTimeHandler;
				}
			}
			_getDateTimes = new List<BaseGetDateTime>();
			if ( _config.OnlineServiceConfigs != null ) {
				foreach ( var config in _config.OnlineServiceConfigs ) {
					_getDateTimes.Add(new OnlineServiceGetDateTime(config));
				}
			}
			if ( _config.HTTPHeaderConfigs != null ) {
				foreach ( var config in _config.HTTPHeaderConfigs ) {
					_getDateTimes.Add(new HTTPHeaderGetDateTime(config));
				}
			}
			_getDateTimes.Add(new UptimeGetDateTime());
			foreach ( var getTine in _getDateTimes ) {
				getTine.OnSuccessTimeUpdate += SuccessGetTimeHandler;
				getTine.OnFailTimeUpdate += FailGetTimeHandler;
			}
		}

		private void StartLoad() {
			_indexActive = 0;
			_getDateTimes[_indexActive].TryUpdateDateTime();
		}
		
		private void PostInitLoad() {
			IsTimeValidate = true;
			_router.PublishAsync(new DateTimeCheckerUpdateEvent());
		}

		private void UseLocalTime() {
			_dateTime = DateTime.UtcNow;
			PostInitLoad();
		}

		private void LoadData() {
			if ( (_saveData.DateTimeDatas == null) || (_saveData.DateTimeDatas.Count == 0) ) {
				return;
			}
			foreach ( var getDataTime in _getDateTimes ) {
				if ( _saveData.DateTimeDatas.TryGetValue(getDataTime.GetType().Name, out var data) ) {
					getDataTime.SetDataForSave(data);
				}
			}
		}

		private void SaveData() {
			_saveData.DateTimeDatas = new Dictionary<string, string>();
			foreach ( var getDataTime in _getDateTimes ) {
				var data = getDataTime.GetDataForSave(GetDateTime);
				if ( !string.IsNullOrEmpty(data) ) {
					_saveData.DateTimeDatas.Add(getDataTime.GetType().Name, data);
				}
			}
			_handSaveManager?.Save();
		}

		private void UpdateConfig() {
			var raw = _configsManager.GetConfig(typeof(DateTimeCheckerConfig).Name);
			_config = JsonSerializer.Deserialize<DateTimeCheckerConfig>(raw);
		}
		
		private void SuccessGetTimeHandler() {
			_dateTime = _getDateTimes[_indexActive].DateTime;
			PostInitLoad();
			if ( _getDateTimes[_indexActive].IsReliable ) {
				SaveData();
			}
		}

		private void FailGetTimeHandler() {
			_indexActive++;
			if ( _indexActive >= _getDateTimes.Count ) {
				UseLocalTime();
				return;
			}
			_getDateTimes[_indexActive].TryUpdateDateTime();
		}

		private void UpdateConfigsHandler(UpdateConfigsEvent e, PublishContext context) {
			UpdateConfig();
			if ( !_saveManager.IsInit ) {
				return;
			}
			if ( IsTimeValidate ) {
				return;
			}
			InitGetDateTime();
			StartLoad();
		}

		private void LoadCompleteSaveHandler(LoadCompleteSaveEvent obj, PublishContext context) {
			var saveData = _saveManager.GetSaveData<DateTimeSaveData>();
			if ( IsTimeValidate ) {
				saveData.DateTimeDatas = _saveData.DateTimeDatas;
				_handSaveManager?.Save();
				return;
			}
			_saveData = saveData;
			if ( !_configsManager.IsConfigLoad ) {
				return;
			}
			LoadData();
			StartLoad();
		}
	}
}
