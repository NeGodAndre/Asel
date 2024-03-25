// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

#if UNITY_EDITOR
using NeGodAndre.UI;
using UnityEditor;
using UnityEngine;

namespace NeGodAndre.Editor.UI {
	[CustomEditor(typeof(EmptyGraphic))]
	public class EmptyGraphicEditor : UnityEditor.Editor {
		public override void OnInspectorGUI() {
			GUI.enabled = false;
			EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((EmptyGraphic) target),
				typeof(MonoScript), false);
			GUI.enabled = true; }
	}
}
#endif