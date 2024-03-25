// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Text;

namespace NeGodAndre.Cryptography {
	public static class Base64 {
		public static string Encrypt(string dataStr) {
			var data = Encoding.UTF8.GetBytes(dataStr);
			return Convert.ToBase64String(data);
		}

		public static string Decrypt(string dataStr) {
			var data = Convert.FromBase64String(dataStr);
			return Encoding.UTF8.GetString(data);
		}
	}
}