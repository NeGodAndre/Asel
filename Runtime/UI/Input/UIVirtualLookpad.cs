// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

#if ASEL_INPUT_SYSTEM
using UnityEngine;
using UnityEngine.EventSystems;
using NeGodAndre.Managers.Input;
using VContainer;

namespace NeGodAndre.UI.Input {
	public class UIVirtualLookpad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {
		public string Name;
		
		private InputManager _inputManager;
		private Vector2      _startPos;
		
		[Inject]
		private void Init(InputManager inputManager) {
			_inputManager = inputManager;
		}

		public void OnPointerDown(PointerEventData eventData) {
			_inputManager.SetAxisUp(Name, Vector2.zero);
		}

		public void OnDrag(PointerEventData eventData) {
			var axis = eventData.position - _startPos;
			_startPos = eventData.position;
			_inputManager.SetAxisDown(Name, axis.normalized);
		}

		public void OnPointerUp(PointerEventData eventData) {
			_startPos = eventData.position;
		}
	}
}
#endif