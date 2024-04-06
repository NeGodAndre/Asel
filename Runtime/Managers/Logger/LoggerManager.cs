// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Collections.Generic;
using Cysharp.Text;
using UnityEngine;
using VContainer.Unity;

namespace NeGodAndre.Managers.Logger {
	public sealed partial class LoggerManager : IInitializable, IDisposable {
		private static LoggerManager _instance {
			get {
				if ( _instanceField == null ) {
					Debug.LogException(new Exception("LoggerManager isn't create!!!"));
				}
				return _instanceField;
			}
		}

		private static LoggerManager _instanceField;
		
		private bool _look;
		
		private readonly List<ILoggerProcessor> _processors;
		
		public LoggerManager(List<ILoggerProcessor> processors) {
			_processors = processors;
			_instanceField = this;
		}

		public void Initialize() {
			Application.logMessageReceived += OnLogMessageReceivedThreaded;
		}

		public void Dispose() {
			foreach ( var processor in _processors ) {
				processor.Dispose();
			}
			Application.logMessageReceived -= OnLogMessageReceivedThreaded;
			_instanceField = null;
		}
		
		private void Log(LogType type, UnityEngine.Object context, string message) {
			var msg = ZString.CreateUtf8StringBuilder();
			msg.Append(message);
			Log(type, context, msg);
		}
		
		private void Log<T>(LogType type, UnityEngine.Object context, T message) {
			var msg = ZString.CreateUtf8StringBuilder();
			msg.Append(message);
			Log(type, context, msg);
		}
		
		private void Log<T1>(LogType type, UnityEngine.Object context, string format, T1 arg1) {
			var msg = ZString.CreateUtf8StringBuilder();
			msg.AppendFormat(format, arg1);
			Log(type, context, msg);
		}
		
		private void Log<T1, T2>(LogType type, UnityEngine.Object context, string format, T1 arg1, T2 arg2) {
			var msg = ZString.CreateUtf8StringBuilder();
			msg.AppendFormat(format, arg1, arg2);
			Log(type, context, msg);
		}
		
		private void Log<T1, T2, T3>(LogType type, UnityEngine.Object context, string format, T1 arg1, T2 arg2, T3 arg3) {
			var msg = ZString.CreateUtf8StringBuilder();
			msg.AppendFormat(format, arg1, arg2, arg3);
			Log(type, context, msg);
		}
		
		private void Log<T1, T2, T3, T4>(LogType type, UnityEngine.Object context, string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4) {
			var msg = ZString.CreateUtf8StringBuilder();
			msg.AppendFormat(format, arg1, arg2, arg3, arg4);
			Log(type, context, msg);
		}
		
		private void Log<T1, T2, T3, T4, T5>(LogType type, UnityEngine.Object context, string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) {
			var msg = ZString.CreateUtf8StringBuilder();
			msg.AppendFormat(format, arg1, arg2, arg3, arg4, arg5);
			Log(type, context, msg);
		}

		private void Log(LogType type, UnityEngine.Object context, Utf8ValueStringBuilder message) {
			var stacktrace = ZString.CreateUtf8StringBuilder();
			if ( IsNeedStackTrace(type) ) {
				stacktrace = GetStackTrace();
			}
			Log(type, context, message, stacktrace);
		}

		private void Log(LogType type, UnityEngine.Object context, Utf8ValueStringBuilder message, Utf8ValueStringBuilder stacktrace, bool unityLog = false) {
			var entry = new LoggerEntry() {
				DateTime = DateTime.Now,
				LogType = type,
				Message = message,
				Context = context,
				Stacktrace = stacktrace,
			};
			_look = true;
			foreach ( var processor in _processors ) {
				if ( unityLog && (processor is UnityConsoleLoggerProcessor) ) {
					continue;
				}
				processor.Push(entry);
			}
			_look = false;
		}

		private Utf8ValueStringBuilder GetStackTrace() {
			var stackTrace = ZString.CreateUtf8StringBuilder();
			var stackTraceLines = StackTraceUtility.ExtractStackTrace().Split('\n');
			foreach ( var line in stackTraceLines ) {
				if ( !line.Contains("NeGodAndre.Managers.Logger", StringComparison.Ordinal) 
					&& !line.Contains("UnityEngine.StackTraceUtility", StringComparison.Ordinal) ) {
					stackTrace.AppendLine(line);
				}
			}
			return stackTrace;
		}

		private bool IsNeedStackTrace(LogType logType) {
			return Application.GetStackTraceLogType(logType) != StackTraceLogType.None;
		}

		private void OnLogMessageReceivedThreaded(string condition, string stacktrace, LogType type) {
			if ( _look ) {
				return;
			}
			var msg = ZString.CreateUtf8StringBuilder();
			msg.Append(condition);
			var st = ZString.CreateUtf8StringBuilder();
			st.Append(stacktrace);
			Log(type, null, msg, st, true);
		}
	}
}