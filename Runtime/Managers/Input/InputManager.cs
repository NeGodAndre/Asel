// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

#if ASEL_INPUT_SYSTEM
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace NeGodAndre.Managers.Input {
	public sealed class InputManager : IInitializable, ITickable, IFixedTickable, ILateTickable {
		public Action OnControlsChanged;
		public Action OnDeviceRegained;
		public Action OnDeviceLost;

		public Action<string, float> OnButton;
		public Action<string, float> OnButtonUp;
		public Action<string, float> OnButtonDown;

		public Action<string, Vector2> OnAxis;

		private Dictionary<string, Vector2> _axisActives   = new Dictionary<string, Vector2>();
		private Dictionary<string, float>   _buttonActives = new Dictionary<string, float>();
		
		private PlayerInput _input;

		private readonly InputSetting _setting;
		
		public InputManager(InputSetting setting) {
			_setting = setting;
		}
		
		public void Initialize() {
			// ReSharper disable once AccessToStaticMemberViaDerivedType
			_input = PlayerInput.Instantiate(_setting.PlayerInput);
			_input.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;
			_input.onActionTriggered += ActionTriggeredHandler;
			_input.onControlsChanged += ControlsChangedHandler;
			_input.onDeviceLost += DeviceLostHandler;
			_input.onDeviceRegained += DeviceRegainedHandler;
		}

		public void Tick() {
			if ( _setting.TypeUpdate != TypeUpdate.Update ) {
				return;
			}
			UpdateEvent();
		}

		public void FixedTick() {
			if ( _setting.TypeUpdate != TypeUpdate.FixedUpdate ) {
				return;
			}
			UpdateEvent();
		}

		public void LateTick() {
			if ( _setting.TypeUpdate != TypeUpdate.LateUpdate ) {
				return;
			}
			UpdateEvent();
		}

		public bool IsButtonPush(string name) {
			return _buttonActives.ContainsKey(name);
		}

		public float GetButtonPush(string name) {
			return _buttonActives.ContainsKey(name) ? _buttonActives[name] : 0f;
		}

		public Vector2 GetAxis(string name) {
			return _axisActives.ContainsKey(name) ? _axisActives[name] : Vector2.zero;
		}

		public void SetActionMap(string actionMap) {
			_input.SwitchCurrentActionMap(actionMap);
		}

		public void SetDisableAutoSwitchControls(bool isDisable) {
			_input.neverAutoSwitchControlSchemes = isDisable;
		}

		public void SetButtonUp(string name, float value) {
			OnButtonDown?.Invoke(name, value);
			_axisActives.Remove(name);
		}

		public void SetButtonDown(string name, float value) {
			OnButtonUp?.Invoke(name, value);
			if ( _buttonActives.ContainsKey(name) ) {
				_buttonActives[name] = value;
			} else {
				_buttonActives.Add(name, value);
			}
		}

		public void SetAxisUp(string name, Vector2 value) {
			OnAxis?.Invoke(name, value);
			_axisActives.Remove(name);
		}

		public void SetAxisDown(string name, Vector2 value) {
			OnAxis?.Invoke(name, value);
			if ( _axisActives.ContainsKey(name) ) {
				_axisActives[name] = value;
			} else {
				_axisActives.Add(name, value);
			}
		}

		private void UpdateEvent() {
			foreach ( var axis in _axisActives ) {
				OnAxis?.Invoke(axis.Key, axis.Value);
			}
			foreach ( var button in _buttonActives ) {
				OnButton?.Invoke(button.Key, button.Value);
			}
		}

		private void ButtonTrigger(InputAction.CallbackContext obj) {
			switch ( obj.phase ) {
				case InputActionPhase.Started: {
					OnButtonUp?.Invoke(obj.action.name, obj.ReadValue<float>());
					if ( _buttonActives.ContainsKey(obj.action.name) ) {
						_buttonActives[obj.action.name] = obj.ReadValue<float>();
					} else {
						_buttonActives.Add(obj.action.name, obj.ReadValue<float>());
					}
					break;
				}
				case InputActionPhase.Performed: {
					OnButton?.Invoke(obj.action.name, obj.ReadValue<float>());
					if ( _buttonActives.ContainsKey(obj.action.name) ) {
						_buttonActives[obj.action.name] = obj.ReadValue<float>();
					} else {
						_buttonActives.Add(obj.action.name, obj.ReadValue<float>());
					}
					break;
				}
				case InputActionPhase.Canceled: {
					OnButtonDown?.Invoke(obj.action.name, obj.ReadValue<float>());
					_axisActives.Remove(obj.action.name);
					break;
				}
				default: {
					break;
				}
			}
		}

		private void AxisTrigger(InputAction.CallbackContext obj) {
			switch ( obj.phase ) {
				case InputActionPhase.Started:
				case InputActionPhase.Performed: {
					OnAxis?.Invoke(obj.action.name, obj.ReadValue<Vector2>());
					if ( _axisActives.ContainsKey(obj.action.name) ) {
						_axisActives[obj.action.name] = obj.ReadValue<Vector2>();
					} else {
						_axisActives.Add(obj.action.name, obj.ReadValue<Vector2>());
					}
					break;
				}
				case InputActionPhase.Canceled: {
					OnAxis?.Invoke(obj.action.name, obj.ReadValue<Vector2>());
					_axisActives.Remove(obj.action.name);
					break;
				}
				default: {
					break;
				}
			}
		}

		private void ActionTriggeredHandler(InputAction.CallbackContext obj) {
			if ( obj.valueType == typeof(Vector2) ) {
				AxisTrigger(obj);
			} else if ( obj.valueType == typeof(float) ) {
				ButtonTrigger(obj);
			} else {
				Debug.LogErrorFormat("InputManager: {0} is unknow type!!!", obj.valueType);
			}
		}

		private void ControlsChangedHandler(PlayerInput playerInput) {
			OnControlsChanged?.Invoke();
		}

		private void DeviceRegainedHandler(PlayerInput obj) {
			OnDeviceRegained?.Invoke();
		}

		private void DeviceLostHandler(PlayerInput obj) {
			OnDeviceLost?.Invoke();
		}
	}
}
#endif