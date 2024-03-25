// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;

namespace NeGodAndre.Managers.DateTimeChecker {
	public abstract class BaseGetDateTime {
		public Action OnSuccessTimeUpdate { get; set; }
		public Action OnFailTimeUpdate    { get; set; }

		public         DateTime DateTime   { get; protected set; }
		public virtual bool     IsReliable { get { return false; } }

		public abstract void TryUpdateDateTime();

		public virtual void SetDataForSave(string rawData) { }

		public virtual string GetDataForSave(DateTime dateTime) {
			return string.Empty;
		}
	}
}