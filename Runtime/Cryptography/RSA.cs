// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Security.Cryptography;
using System.Text;

namespace NeGodAndre.Cryptography {
	// ReSharper disable once InconsistentNaming
	public class RSA {
		public static string Encrypt(string dataStr, RSACryptographySetting setting) {
			using var rsa = System.Security.Cryptography.RSA.Create();
			rsa.FromXmlString(setting.Key);
			var result = rsa.Encrypt(Encoding.UTF8.GetBytes(dataStr), RSAEncryptionPadding.Pkcs1);
			return Convert.ToBase64String(result);
		}

		public static string Decrypt(string dataStr, RSACryptographySetting setting) {
			var dataByte = Convert.FromBase64String(dataStr);
			using var rsa = System.Security.Cryptography.RSA.Create();
			rsa.FromXmlString(setting.Key);
			var result = Encoding.UTF8.GetString(rsa.Decrypt(dataByte, RSAEncryptionPadding.Pkcs1));
			return result;
		}
	}
}