## Порядок расположение

Active и делегаты

### Публичные поля

```c#
public string Name;
```

### Публичные своиства

```c#
public string NameCompany { get; set; } 
public int    Id          { get; private set; }
```

### Unity поля c приватным модификатором

```c#
[SerializeField] private Vector3 startPosition;
```

### Приветные своиства


```c#
private string NameCompany { get; set; } 
private int    Id          { get; private set; }
```

### Приватные поля

```c#
private float _temp;
```

### Константы

```c#
private const string NAME_CONFIG = "config";
```

---

### Example

```C#
using System;
using NeGodAndre.Managers.Configs;
using NeGodAndre.Managers.Events;
using NeGodAndre.Managers.Save.Interfaces;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DefaultNamespace {
	public class Example : MonoBehaviour, IInitializable {
		public Action OnInit;
		
		public string Name;
		
		public string NameCompany { get; set; } 
		public int    Id          { get; private set; }
		
		[SerializeField] private Vector3 _startPosition;
		
		[Inject] private IHandSaveManager _handSaveManager;

		private int _foo { get; set; }
		
		private float _temp;

		private readonly EventManager     _eventManager;
		private readonly ISaveManager     _saveManager;
		private readonly ConfigsManager   _configsManager;
		
		private const string NAME_CONFIG = "config";

		public Example() { }

		public void Awake() { }

		public void OnEnable() { }

		public void OnDisable() { }

		public void OnDestroy() { }

		public void OnCollisionEnter(Collision other) { }

		public void Initialize() { }

		public int Count(string name) {
			return 0;
		}

		public void UpdatePosition() { }

		private float LastTemp() {
			return -1;
		}
		
		private void UpdateRotation() { }
		
		private void UpdateScreenHandler() { }
	}
}
```