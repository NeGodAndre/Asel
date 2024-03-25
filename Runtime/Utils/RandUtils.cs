// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System.Collections.Generic;
using UnityEngine;

namespace NeGodAndre.Utils {
	public static class RandUtils {   
		/// <summary>
		///   <para>Return a random int within [min..max).</para>
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		public static int Range(int min, int max) {
			if ( max < min ) {
				Debug.LogError("RandUtils: min more max!!!");
				return min;
			}
			return Random.Range(min, max);
		}

		/// <summary>
		///   <para>Return a random float within [min..max).</para>
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		public static float Range(float min, float max) {
			if ( max < min ) {
				Debug.LogError("RandUtils: min more max!!!");
				return min;
			}
			return Random.Range(min, max);
		}

		/// <summary>
		///   <para>Return a random float within [vector.x..vector.y).</para>
		/// </summary>
		/// <param name="vector"></param>
		public static float Range(Vector2 vector) {
			return Range(vector.x, vector.y);
		}

		/// <summary>
		///   <para>Return a random int within [vector.x..vector.y).</para>
		/// </summary>
		/// <param name="vector"></param>
		public static int Range(Vector2Int vector) {
			return Range(vector.x, vector.y);
		}

		/// <summary>
		///   <para>Return a random bool.</para>
		/// </summary>
		public static bool Range() {
			return Range(0, 2) == 0;
		}

		/// <summary>
		///   <para>Return a random element from values.</para>
		/// </summary>
		/// <param name="values"></param>
		public static T Range<T>(List<T> values) {
			if ( (values == null) || (values.Count == 0) ) {
				Debug.LogError("List is null or empty!!!");
				return default;
			}
			if ( values.Count == 1 ) {
				return values[0];
			}
			return values[Range(0, values.Count - 1)];
		}

		/// <summary>
		///   <para>Return a random element from values.</para>
		/// </summary>
		/// <param name="values"></param>
		public static T Range<T>(T[] values){
			if ( (values == null) || (values.Length == 0) ){
				Debug.LogError("List is null or empty!!!");
				return default;
			}
			if ( values.Length == 1 ){
				return values[0];
			}
			return values[Range(0, values.Length - 1)];
		}

		/// <summary>
		///   <para>Returns a random element from values based on arrayWeight.</para>
		/// </summary>
		/// <param name="values"></param>
		/// <param name="arrayWeight"></param>
		public static T Range<T>(List<T> values, List<float> arrayWeight) {
			float sumWeight = 0;
			foreach ( var weight in arrayWeight ) {
				sumWeight += weight;
			}
			if ( Mathf.Approximately(sumWeight, 0f) ) {
				var index = Range(0, values.Count - 1);
				return values[index];
			}
			var low = 0;
			var high = arrayWeight.Count - 1;
			var needly = Range(0, sumWeight);
			var probe = 0;

			while ( low < high ) {
				probe = (low + high) / 2;
				if ( SumValues(arrayWeight, probe) < needly ) {
					low++;
				} else if ( SumValues(arrayWeight, probe) > needly ) {
					high--;
				} else {
					return values[probe];
				}
			}
			if ( low == high ) {
				probe = arrayWeight[low] >= needly ? low :
					((low + 1) < values.Count)     ? low + 1
					                                 : low;
				return values[probe];
			} else {
				return values[probe];
			}
		}

		private static float SumValues(List<float> values, int lastIndex) {
			var value = 0f;
			for ( var i = 0; (i < values.Count) && (i <= lastIndex); i++ ) {
				value += values[i];
			}
			return value;
		}
	}
}
