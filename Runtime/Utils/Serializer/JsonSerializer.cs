// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using Newtonsoft.Json;
using UnityEngine;

namespace NeGodAndre.Utils.Serializer {
	public static class JsonSerializer {
		private static readonly JsonSerializerSettings Setting = new JsonSerializerSettings {
			Formatting = Formatting.Indented, 
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
		};

		public static string Serialize<T>(T obj) {
			return JsonConvert.SerializeObject(obj, Setting);
		}

		public static T Deserialize<T>(string strJson) {
			if ( string.IsNullOrEmpty(strJson) ) {
				Debug.LogError("String for loading is null or empty!!!");
				return default(T);
			}
			try {
				return JsonConvert.DeserializeObject<T>(strJson, Setting);
			} catch ( Exception e ) {
				Debug.LogError("Deserialize Json Exception " + e);
				return default(T);
			}
		}
		
		public static T Deserialize<T>(string strJson, T obj) {
			if ( string.IsNullOrEmpty(strJson) ) {
				Debug.LogError("String for loading is null or empty!!!");
				return default(T);
			}
			try {
				return JsonConvert.DeserializeAnonymousType(strJson, obj, Setting);
			} catch ( Exception e ) {
				Debug.LogError("Deserialize Json Exception " + e);
				return default(T);
			}
		}
		
		public static T Deserialize<T>(string strJson, Type type) {
			if ( string.IsNullOrEmpty(strJson) ) {
				Debug.LogError("String for loading is null or empty!!!");
				return default(T);
			}
			try {
				return (T)JsonConvert.DeserializeObject(strJson, type, Setting);
			} catch ( Exception e ) {
				Debug.LogError("Deserialize Json Exception " + e);
				return default(T);
			}
		}
	}
}