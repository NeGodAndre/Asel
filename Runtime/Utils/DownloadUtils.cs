// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace NeGodAndre.Utils {
	public static class DownloadUtils {
		public static async UniTask<string> DownloadText(string url) {
			var request = UnityWebRequest.Get(url);
			request.timeout = 3;
			await request.SendWebRequest();
			if ( (request.result != UnityWebRequest.Result.ProtocolError) && (request.result != UnityWebRequest.Result.ConnectionError) ) {
				return request.downloadHandler.text;
			} else {
				throw new OperationCanceledException(request.error);
			}
		}

		public static async UniTask<string> DownloadText(string url, int countAttempt) {
			try {
				return await DownloadText(url);
			} catch ( Exception error ) when ( error is OperationCanceledException ) {
				if ( countAttempt <= 0 ) {
					throw new OperationCanceledException(string.Format("Download {0} have error: {1}", url, error.Message));
				}
				return await DownloadText(url, countAttempt - 1);
			}
		}

		public static async UniTask<byte[]> DownloadData(string url) {
			var request = UnityWebRequest.Get(url);
			request.timeout = 3;
			await request.SendWebRequest();
			if ( (request.result != UnityWebRequest.Result.ProtocolError) && (request.result != UnityWebRequest.Result.ConnectionError) ) {
				return request.downloadHandler.data;
			} else {
				throw new OperationCanceledException(request.error);
			}
		}

		public static async UniTask<byte[]> DownloadData(string url, int countAttempt) {
			try {
				return await DownloadData(url);
			} catch ( Exception error ) when ( error is OperationCanceledException ) {
				if ( countAttempt <= 0 ) {
					throw new OperationCanceledException(string.Format("Download {0} have error: {1}", url, error.Message));
				}
				return await DownloadData(url, countAttempt - 1);
			}
		}

		public static async UniTask<Dictionary<string, string>> DownloadHeaders(string url) {
			var request = UnityWebRequest.Get(url);
			request.timeout = 3;
			await request.SendWebRequest();
			if ( (request.result != UnityWebRequest.Result.ProtocolError) && (request.result != UnityWebRequest.Result.ConnectionError) ) {
				return request.GetResponseHeaders();
			} else {
				throw new OperationCanceledException(request.error);
			}
		}

		public static async UniTask<Dictionary<string, string>> DownloadHeaders(string url, int countAttempt) {
			try {
				return await DownloadHeaders(url);
			} catch ( Exception error ) when ( error is OperationCanceledException ) {
				if ( countAttempt <= 0 ) {
					throw new OperationCanceledException(string.Format("Download {0} have error: {1}", url, error.Message));
				}
				return await DownloadHeaders(url, countAttempt - 1);
			}
		}
	}
}