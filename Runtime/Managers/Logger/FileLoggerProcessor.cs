// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System.IO;
using Cysharp.Text;

namespace NeGodAndre.Managers.Logger {
	public class FileLoggerProcessor : ILoggerProcessor {
		private StreamWriter _streamWriter;
		
		public FileLoggerProcessor(string filePath) {
			OpenFile(filePath);
		}
		
		public FileLoggerProcessor(string filePath, long maxSize, long keepSize) {
			CheckFileSize(filePath, maxSize, keepSize);
			OpenFile(filePath);
		}

		public void Dispose() {
			CloseFile();
		}
		
		public void Push(LoggerEntry entry) {
			var str = string.Empty;
			if ( entry.Context ) {
				str = ZString.Format("{0}\nType: {1}\nContext: {2}\nMessage: {3}\n{4}", entry.DateTime, entry.LogType, entry.Context, entry.Message, entry.Stacktrace);
			} else {
				str = ZString.Format("{0}\nType: {1}\nMessage: {2}\n{3}", entry.DateTime, entry.LogType, entry.Message, entry.Stacktrace);
			}
			_streamWriter.WriteLine(str);
		}

		private void OpenFile(string filePath) {
			if ( !File.Exists(filePath) ) {
				_streamWriter = File.CreateText(filePath);
			} else {
				_streamWriter = new StreamWriter(filePath, true);
			}
			_streamWriter.WriteLine("\n--------------Begin Session--------------");
		}

		private void CloseFile() {
			if ( _streamWriter == null ) {
				return;
			}
			_streamWriter.WriteLine("---------------End Session---------------");
			_streamWriter.Dispose();
		}

		private void CheckFileSize(string filePath, long maxSize, long keepSize) {
			if ( !File.Exists(filePath) ) {
				return;
			}
			var fileInfo = new FileInfo(filePath);
			var fileSizeInBytes = fileInfo.Length;
			if ( fileSizeInBytes < maxSize ) {
				return;
			}
			using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
			var buffer = new byte[keepSize];
			fileStream.Seek(fileSizeInBytes - keepSize, SeekOrigin.Begin);
			fileStream.Read(buffer, 0, buffer.Length);
			fileStream.Seek(0, SeekOrigin.Begin);
			fileStream.Write(buffer, 0, buffer.Length);
			fileStream.SetLength(buffer.Length);
		}
	}
}