// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using VitalRouter;

namespace NeGodAndre.Managers.Ad {
	public struct InterstitialOpenEvent : ICommand {
		public string Placement;

		public InterstitialOpenEvent(string placement) {
			Placement = placement;
		}
	}
	
	public struct InterstitialCompleteEvent : ICommand {
		public string Placement;
		public bool   IsShow;
		public bool   IsClick;

		public InterstitialCompleteEvent(string placement, bool isShow, bool isClick) {
			Placement = placement;
			IsShow = isShow;
			IsClick = isClick;
		}
	}
	
	public struct RewardedOpenEvent : ICommand {
		public string Placement;

		public RewardedOpenEvent(string placement) {
			Placement = placement;
		}
	}

	public struct RewardedCompleteEvent : ICommand {
		public string Placement;
		public bool   IsShow;
		public bool   IsClick;

		public RewardedCompleteEvent(string placement, bool isShow, bool isClick) {
			Placement = placement;
			IsShow = isShow;
			IsClick = isClick;
		}
	}

	public struct RewardedLoadedEvent : ICommand { }

	public struct BannerClickEvent : ICommand {
		public string Placement;

		public BannerClickEvent(string placement) {
			Placement = placement;
		}
	}

	// ReSharper disable once InconsistentNaming
	public struct MRECClickEvent : ICommand {
		public string Placement;

		public MRECClickEvent(string placement) {
			Placement = placement;
		}
	}
}