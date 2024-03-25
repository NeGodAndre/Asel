// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

#if ASEL_INPUT_SYSTEM
using UnityEngine;
using UnityEngine.EventSystems;
using NeGodAndre.Managers.Input;
using VContainer;

namespace NeGodAndre.UI.Input {
	public class UIVirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {
		public string Name;
		
		[Header("Rect References")] 
		public RectTransform ContainerRect;
		public RectTransform HandleRect;
		
		[Header("Settings")] 
		public float JoystickRange = 50f;

		private InputManager _inputManager;
		
		[Inject]
		private void Init(InputManager inputManager) {
			_inputManager = inputManager;
		}
		
		public void OnEnable() {
			SetupHandle();
		}

		public virtual void OnPointerDown(PointerEventData eventData) {
			SetCoordinates(eventData);
		}

		public virtual void OnPointerUp(PointerEventData eventData) {
			_inputManager.SetAxisUp(Name, Vector2.zero);
			SetupHandle();
		}

		public void OnDrag(PointerEventData eventData) {
			SetCoordinates(eventData);
		}

		private void SetupHandle() {
			HandleRect.anchoredPosition = Vector2.zero;
		}

		private void SetCoordinates(PointerEventData eventData) {
			if ( RectTransformUtility.ScreenPointToLocalPointInRectangle(ContainerRect, eventData.position,
				eventData.pressEventCamera, out var globalPosition) ) {
				HandleRect.localPosition = globalPosition;
			}

			var handleOffset = HandleRect.anchoredPosition;
			if ( handleOffset.magnitude > JoystickRange ) {
				handleOffset = handleOffset.normalized * JoystickRange;
				HandleRect.anchoredPosition = handleOffset;
			}
			_inputManager.SetAxisDown(Name, HandleRect.anchoredPosition.normalized);
		}
	}
}
#endif