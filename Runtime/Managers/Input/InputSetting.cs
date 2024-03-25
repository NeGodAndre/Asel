// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

#if ASEL_INPUT_SYSTEM
using UnityEngine;
using UnityEngine.InputSystem;

namespace NeGodAndre.Managers.Input {
	[CreateAssetMenu(fileName = "InputSetting", menuName = "NeGodAndre/Settings/InputSetting")]
	public class InputSetting : ScriptableObject {
		public PlayerInput PlayerInput;
		public TypeUpdate  TypeUpdate;
	}

	public enum TypeUpdate {
		None,
		Update,
		FixedUpdate,
		LateUpdate,
	}
}
#endif
