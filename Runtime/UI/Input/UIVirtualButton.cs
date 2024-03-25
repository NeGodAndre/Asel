// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

#if ASEL_INPUT_SYSTEM
using UnityEngine;
using UnityEngine.EventSystems;
using NeGodAndre.Managers.Input;
using VContainer;

namespace NeGodAndre.UI.Input {
	public class UIVirtualButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
		public string Name;
		
		private InputManager _inputManager;

		[Inject]
		private void Init(InputManager inputManager) {
			_inputManager = inputManager;
		}

		public void OnPointerDown(PointerEventData eventData) {
			_inputManager.SetButtonDown(Name, 1f);
		}

		public void OnPointerUp(PointerEventData eventData) {
			_inputManager.SetButtonUp(Name, 0f);
		}
	}
}
#endif
