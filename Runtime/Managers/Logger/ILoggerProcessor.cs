// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;

namespace NeGodAndre.Managers.Logger {
	public interface ILoggerProcessor : IDisposable {
		public void Push(LoggerEntry entry);
	}
}