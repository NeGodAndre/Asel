// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.IO;
using System.Security.Cryptography;

namespace NeGodAndre.Cryptography {
	// ReSharper disable once InconsistentNaming
	public static class AES {
		public static string Encrypt(string dataStr, AESCryptographySetting setting) {
			using var aes = Aes.Create();
			aes.Key = setting.Key;
			aes.IV = setting.IV;
			var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
			using var msEncrypt = new MemoryStream();
			using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
			using ( var swEncrypt = new StreamWriter(csEncrypt) ) {
				swEncrypt.Write(dataStr);
			}
			var result = msEncrypt.ToArray();
			return Convert.ToBase64String(result);
		}

		public static string Decrypt(string dataStr, AESCryptographySetting setting) {
			var dataByte = Convert.FromBase64String(dataStr);
			using var aes = Aes.Create();
			aes.Key = setting.Key;
			aes.IV = setting.IV;
			var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
			using var msDecrypt = new MemoryStream(dataByte);
			using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
			using var srDecrypt = new StreamReader(csDecrypt);
			var result = srDecrypt.ReadToEnd();
			return result;
		}
	}
}