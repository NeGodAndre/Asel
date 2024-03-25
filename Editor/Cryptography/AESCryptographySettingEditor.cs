// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

#if UNITY_EDITOR
using System;
using System.Reflection;
using System.Security.Cryptography;
using NeGodAndre.Cryptography;
using UnityEditor;
using UnityEngine;

namespace NeGodAndre.Editor.Cryptography {
	[CustomEditor(typeof(AESCryptographySetting), false)]
	[CanEditMultipleObjects]
	public class AESCryptographySettingEditor : UnityEditor.Editor {
		private AESCryptographySetting _setting;

		void OnEnable() {
			_setting = target as AESCryptographySetting;
		}

		public override void OnInspectorGUI() {
			DrawDefaultInspector();
			if ( GUILayout.Button("Create Key") ) {
				var type = typeof(AESCryptographySetting);
				var keyField = type.GetField("_key", BindingFlags.NonPublic | BindingFlags.Instance);
				var aes = Aes.Create();
				aes.GenerateKey();
				keyField?.SetValue(_setting, Convert.ToBase64String(aes.Key));
			}
			if ( GUILayout.Button("Create IV") ) {
				var type = typeof(AESCryptographySetting);
				var keyField = type.GetField("_iv", BindingFlags.NonPublic | BindingFlags.Instance);
				var aes = Aes.Create();
				aes.GenerateIV();
				keyField?.SetValue(_setting,  Convert.ToBase64String(aes.IV));
			}
			serializedObject.ApplyModifiedProperties ();
		}
	}
}
#endif
