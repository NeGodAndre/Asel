// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using UnityEngine;

namespace NeGodAndre.Cryptography {
	public static class CryptographyHelper {
		public static string Encrypt(string data, BaseCryptographySetting setting) {
			if ( setting == null ) {
				return data;
			}
			try {
				var base64 = setting as Base64CryptographySetting;
				if ( base64 != null ) {
					return Base64.Encrypt(data);
				}
				var aes = setting as AESCryptographySetting;
				if ( aes != null ) {
					return AES.Encrypt(data, aes);
				}
				var rsa = setting as RSACryptographySetting;
				if ( rsa != null ) {
					return RSA.Encrypt(data, rsa);
				}
			} catch ( Exception e ) {
				Debug.LogException(e);
			}
			return data;
		}

		public static string Decrypt(string data, BaseCryptographySetting setting) {
			if ( setting == null ) {
				return data;
			}
			try {
				var base64 = setting as Base64CryptographySetting;
				if ( base64 != null ) {
					return Base64.Decrypt(data);
				}
				var aes = setting as AESCryptographySetting;
				if ( aes != null ) {
					return AES.Decrypt(data, aes);
				}
				var rsa = setting as RSACryptographySetting;
				if ( rsa != null ) {
					return RSA.Decrypt(data, rsa);
				}
			} catch ( Exception e ) {
				Debug.LogException(e);
			}
			return data;
		}
	}
}