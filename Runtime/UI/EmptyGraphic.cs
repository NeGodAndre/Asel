// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using UnityEngine;
using UnityEngine.UI;

namespace NeGodAndre.UI {
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(CanvasRenderer))]
	public class EmptyGraphic : Graphic {
		public override void SetMaterialDirty() { }

		public override void SetVerticesDirty() { }

		protected override void OnPopulateMesh(VertexHelper vh) {
			vh.Clear();
		}
	}
}
