// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using UnityEngine;

namespace NeGodAndre.Cryptography {
	[CreateAssetMenu(fileName = "RSACryptographySetting", menuName = "NeGodAndre/Cryptography/RSACryptographySetting")]
	public class RSACryptographySetting : BaseCryptographySetting {
		[TextArea(1, 200)] public string Key;
		
#if UNITY_EDITOR
		private void OnValidate() {
			var document = new System.Xml.XmlDocument();
			document.LoadXml(Key);
		}
#endif
	}
}