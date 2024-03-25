// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

#if UNITY_EDITOR
using System.Security.Cryptography;
using NeGodAndre.Cryptography;
using UnityEditor;
using UnityEngine;

namespace NeGodAndre.Editor.Cryptography {
	[CustomEditor(typeof(RSACryptographySetting), false)]
	[CanEditMultipleObjects]
	// ReSharper disable once InconsistentNaming
	public class RSACryptographySettingEditor  : UnityEditor.Editor {
		private RSACryptographySetting _setting;

		void OnEnable() {
			_setting = target as RSACryptographySetting;
		}
		
		public override void OnInspectorGUI() {
			DrawDefaultInspector();
			if ( GUILayout.Button("Create Key") ) {
				using (var rsa = new RSACryptoServiceProvider(2048))
				{
					_setting.Key = rsa.ToXmlString(true);
				}
			}
			serializedObject.ApplyModifiedProperties ();
		}
	}
}
#endif