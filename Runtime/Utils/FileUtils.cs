// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.IO;
using System.Text;
using Cysharp.Threading.Tasks;
using NeGodAndre.Managers.Logger;

namespace NeGodAndre.Utils {
	public static class FileUtils {
		public static async UniTask<string> ReadStringAsync(string path) {
			if ( !File.Exists(path) ) {
				return string.Empty;
			}
			return await File.ReadAllTextAsync(path, Encoding.UTF8);
		}
		
		public static async UniTask<byte[]> ReadBytesAsync(string path) {
			if ( !File.Exists(path) ) {
				return Array.Empty<byte>();
			}
			return await File.ReadAllBytesAsync(path);
		}

		public static string ReadString(string path) {
			if ( !File.Exists(path) ) {
				return string.Empty;
			}
			return File.ReadAllText(path, Encoding.UTF8);
		}
		
		public static byte[] ReadBytes(string path) {
			if ( !File.Exists(path) ) {
				return new byte[] {};
			}
			return File.ReadAllBytes(path);
		}

		public static async UniTask WriteStringAsync(string path, string data) {
			if ( !File.Exists(path) ) {
				try {
					var directory = Directory.GetParent(path).FullName;
					if ( !Directory.Exists(directory) ) {
						Directory.CreateDirectory(directory);
					}
					await File.Create(path).DisposeAsync();
				} catch ( Exception e ) {
					throw new Exception(e.ToString());
				}
			}
			await File.WriteAllTextAsync(path, data, Encoding.UTF8);
		}
		
		public static async UniTask WriteBytesAsync(string path, byte[] data) {
			if ( !File.Exists(path) ) {
				try {
					var directory = Directory.GetParent(path).FullName;
					if ( !Directory.Exists(directory) ) {
						Directory.CreateDirectory(directory);
					}
					await File.Create(path).DisposeAsync();
				} catch ( Exception e ) {
					throw new Exception(e.ToString());
				}
			}
			await File.WriteAllBytesAsync(path, data);
		}

		public static void WriteString(string path, string data) {
			if ( !File.Exists(path) ) {
				try {
					var directory = Directory.GetParent(path).FullName;
					if ( !Directory.Exists(directory) ) {
						Directory.CreateDirectory(directory);
					}
					File.Create(path).Dispose();
				} catch ( Exception e ) {
					LoggerManager.LogException(e);
				}
			}
			File.WriteAllText(path, data, Encoding.UTF8);
		}
		
		public static void WriteBytes(string path, byte[] data) {
			if ( !File.Exists(path) ) {
				try {
					var directory = Directory.GetParent(path).FullName;
					if ( !Directory.Exists(directory) ) {
						Directory.CreateDirectory(directory);
					}
					File.Create(path).Dispose();
				} catch ( Exception e ) {
					LoggerManager.LogException(e);
				}
			}
			File.WriteAllBytesAsync(path, data);
		}
	}
}