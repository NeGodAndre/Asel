// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using UnityEngine;

namespace NeGodAndre.Managers.Logger {
	public sealed class UnityConsoleLoggerProcessor : ILoggerProcessor {
		public void Push(LoggerEntry entry) {
			Debug.unityLogger.Log(
				logType: entry.LogType, 
				message: entry.Message, 
				context: entry.Context);
		}

		public void Dispose() { }
	}
}