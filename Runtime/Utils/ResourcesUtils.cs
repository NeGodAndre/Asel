// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using Cysharp.Threading.Tasks;
using UnityEngine;

namespace NeGodAndre.Utils {
	public static class ResourcesUtils {
		public static async UniTask<Object> LoadAsync(string path) {
			var result = await Resources.LoadAsync(path);
			return result;
		}

		public static async UniTask<T> LoadAsync<T>(string path) where T : Object {
			var result = await Resources.LoadAsync<T>(path);
			var obj = result as T;
			if ( !obj ) {
				throw new System.OperationCanceledException(string.Format("File \"{0}\" have not type \"{1}\"!", path, typeof(T).FullName));
			}
			return obj;
		}
	}
}