// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Security.Cryptography;
using UnityEngine;

namespace NeGodAndre.Cryptography {
	[CreateAssetMenu(fileName = "AesCryptographySetting", menuName = "NeGodAndre/Cryptography/AesCryptographySetting")]
	public class AESCryptographySetting : BaseCryptographySetting {
		public byte[] Key { get { return Convert.FromBase64String(_key); } }
		// ReSharper disable once InconsistentNaming
		public byte[] IV  { get { return Convert.FromBase64String(_iv);  } }

		[SerializeField] private string _key;
		[SerializeField] private string _iv;

#if UNITY_EDITOR
		private void OnValidate() {
			var aes = Aes.Create();
			var ks = aes.LegalKeySizes;
			var byteKey = Convert.FromBase64String(_key);
			if ( ((ks[0].MinSize / 8) > byteKey.Length) || (byteKey.Length > (ks[0].MaxSize / 8)) ) {
				Debug.LogError("AESCryptoSetting: Key have incorrect size!!!");
			}

			ks = aes.LegalBlockSizes;
			var byteIV = Convert.FromBase64String(_iv);
			if ( (ks[0].MinSize / 8) != byteIV.Length ) {
				Debug.LogError("AESCryptoSetting: IV have incorrect size!!!");
			}
		}
#endif
	}
}