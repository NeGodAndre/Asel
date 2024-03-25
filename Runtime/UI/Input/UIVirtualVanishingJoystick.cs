// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

#if ASEL_INPUT_SYSTEM
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NeGodAndre.UI.Input {
	[RequireComponent(typeof(CanvasGroup))]
	public class UIVirtualVanishingJoystick : UIVirtualJoystick {
		public CanvasGroup CanvasGroup;

		private Coroutine _disableJoystickCoroutine;
		
		public override void OnPointerDown(PointerEventData eventData) {
			ContainerRect.position = new Vector3(eventData.pressPosition.x, eventData.pressPosition.y, 0);
			if ( _disableJoystickCoroutine != null ) {
				StopCoroutine(_disableJoystickCoroutine);
			}
			CanvasGroup.alpha = 1f;
			base.OnPointerDown(eventData);
		}

		public override void OnPointerUp(PointerEventData eventData) {
			base.OnPointerUp(eventData);
			_disableJoystickCoroutine = StartCoroutine((WaitDisableJoystick()));
		}

		private IEnumerator WaitDisableJoystick() {
			yield return 0.2f;
			CanvasGroup.alpha = 0f;
		}
	}
}
#endif