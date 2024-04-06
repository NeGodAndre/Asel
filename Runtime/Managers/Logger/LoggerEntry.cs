// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using Cysharp.Text;
using UnityEngine;

namespace NeGodAndre.Managers.Logger {
	public struct LoggerEntry : IDisposable {
		public DateTime               DateTime;
		public LogType                LogType;
		public Utf8ValueStringBuilder Message;
		public Utf8ValueStringBuilder Stacktrace;
		public UnityEngine.Object     Context;

		public void Dispose() {
			Message.Dispose();
			Stacktrace.Dispose();
		}
	}
}