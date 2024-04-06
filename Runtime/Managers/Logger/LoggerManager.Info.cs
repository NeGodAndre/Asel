﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using UnityEngine;

namespace NeGodAndre.Managers.Logger {
	public sealed partial class LoggerManager {
		public static void LogInfo(Object context, string message) {
			_instance?.Log(LogType.Log, context, message);
		}
		
		public static void LogInfo<T>(Object context, T message) {
			_instance?.Log(LogType.Log, context, message);
		}
		
		public static void LogInfo<T1>(Object context, string format, T1 arg1) {
			_instance?.Log(LogType.Log, context, format, arg1);
		}
		
		public static void LogInfo<T1, T2>(Object context, string format, T1 arg1, T2 arg2) {
			_instance?.Log(LogType.Log, context, format, arg1, arg2);
		}
		
		public static void LogInfo<T1, T2, T3>(Object context, string format, T1 arg1, T2 arg2, T3 arg3) {
			_instance?.Log(LogType.Log, context, format, arg1, arg2, arg3);
		}
		
		public static void LogInfo<T1, T2, T3, T4>(Object context, string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4) {
			_instance?.Log(LogType.Log, context, format, arg1, arg2, arg3, arg4);
		}
		
		public static void LogInfo<T1, T2, T3, T4, T5>(Object context, string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) {
			_instance?.Log(LogType.Log, context, format, arg1, arg2, arg3, arg4, arg5);
		}
		
		public static void LogInfo(string message) {
			_instance?.Log(LogType.Log, null, message);
		}
		
		public static void LogInfo<T>(T message) {
			_instance?.Log(LogType.Log, null, message);
		}
		
		public static void LogInfo<T1>(string format, T1 arg1) {
			_instance?.Log(LogType.Log, null, format, arg1);
		}
		
		public static void LogInfo<T1, T2>(string format, T1 arg1, T2 arg2) {
			_instance?.Log(LogType.Log, null, format, arg1, arg2);
		}
		
		public static void LogInfo<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3) {
			_instance?.Log(LogType.Log, null, format, arg1, arg2, arg3);
		}
		
		public static void LogInfo<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4) {
			_instance?.Log(LogType.Log, null, format, arg1, arg2, arg3, arg4);
		}
		
		public static void LogInfo<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) {
			_instance?.Log(LogType.Log, null, format, arg1, arg2, arg3, arg4, arg5);
		}
	}
}