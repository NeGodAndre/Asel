// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

#if ASEL_DOTWEEN_SUPPORT
using UnityEngine;
using UnityEngine.SceneManagement;

// ReSharper disable once CheckNamespace
// ReSharper disable once IdentifierTypo
namespace DG.Tweening {
	// ReSharper disable once InconsistentNaming
	public static class DOTweenHelper {
		private static bool _isSubscribe;

		public static Sequence CreateSequence(MonoBehaviour masterAnim) {
			var seq = DOTween.Sequence();
			if ( masterAnim ) {
				seq.SetId(string.Format("Scene {0}, script {1}", masterAnim.gameObject.scene, masterAnim));
			}
			return seq;
		}

		public static void ResetSequence(Sequence seq, bool complete = true, bool withCallbacks = false) {
			if ( seq == null ) {
				return;
			}
			seq.SetAutoKill(false);
			if ( complete ) {
				seq.Complete(withCallbacks);
			}
			seq.Kill();
		}

		public static Sequence ReplaceSequence(Sequence seq, MonoBehaviour masterAnim, bool complete = true, bool withCallbacks = false) {
			ResetSequence(seq, complete, withCallbacks);
			return CreateSequence(masterAnim);
		}

		[RuntimeInitializeOnLoadMethod]
		private static void TrySubscribe() {
			if ( _isSubscribe ) {
				return;
			}
			SceneManager.sceneUnloaded += OnSceneUnload;
			_isSubscribe = true;
		}

		private static void OnSceneUnload(Scene arg0) {
			var seqs = DOTween.PlayingTweens();
			if ( (seqs == null) || (seqs.Count == 0) ) {
				return;
			}
			foreach ( var seq in seqs ) {
				if ( !string.IsNullOrEmpty(seq.stringId) && seq.stringId.Contains(arg0.name) ) {
					Debug.LogWarningFormat("Sequence {0} don't killing!!! It's murdered!!!", seq.stringId);
					seq.Kill();
				}
			}
		}
	}
}
#endif
