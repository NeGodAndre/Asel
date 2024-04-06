// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using NeGodAndre.Managers.Logger;
using UnityEngine;
using VitalRouter;

namespace NeGodAndre.Managers.Ad {
	public sealed class FakeAdManager : IAdManager {
		public bool IsInterstitialReady { get { return _isWork; } }
		public bool IsRewardedReady     { get { return _isWork; } }
		public bool IsBannerReady       { get { return _isWork; } }
		public bool IsMRECReady         { get { return _isWork; } }

		private readonly bool              _isWork;
		private readonly ICommandPublisher _publisher;

		public FakeAdManager(ICommandPublisher publisher, bool isWork) {
			_publisher = publisher;
			_isWork = isWork;
		}
		
		public void TryShowInterstitial(string placement) {
			if ( !_isWork ) {
				return;
			}
			LoggerManager.LogInfo("Fake Ad Show");
			_publisher.PublishAsync(new InterstitialOpenEvent(placement));
			_publisher.PublishAsync(new InterstitialCompleteEvent(placement, _isWork, false));
		}

		public void TryShowRewarded(string placement) {
			if ( !_isWork ) {
				return;
			}
			LoggerManager.LogInfo("Fake Reward Ad Show");
			_publisher.PublishAsync(new RewardedOpenEvent(placement));
			_publisher.PublishAsync(new RewardedCompleteEvent(placement, _isWork, false));
			_publisher.PublishAsync(new RewardedLoadedEvent());
		}

		public void ShowBanner(BannerPosition position, string placement) {
			if ( !_isWork ) {
				return;
			}
			LoggerManager.LogInfo("Fake Banner Show");
		}

		public void HideBanner() { }

		public void ShowMREC(Vector2 position, string placement) {
			if ( !_isWork ) {
				return;
			}
			LoggerManager.LogInfo("Fake MREC Show");}

		public void HideMREC() { }
	}
}