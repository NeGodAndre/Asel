// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using UnityEngine;

namespace NeGodAndre.Managers.Ad {
	public interface IAdManager {
		public bool IsInterstitialReady { get; }
		public bool IsRewardedReady     { get; }
		public bool IsBannerReady       { get; }
		public bool IsMRECReady         { get; }

		public void TryShowInterstitial(string placement);

		public void TryShowRewarded(string placement);

		public void ShowBanner(BannerPosition position, string placement);

		public void HideBanner();

		// ReSharper disable once InconsistentNaming
		public void ShowMREC(Vector2 position, string placement);

		// ReSharper disable once InconsistentNaming
		public void HideMREC();
	}
}