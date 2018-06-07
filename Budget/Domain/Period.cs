using System;

namespace Budget.Domain {
	[Serializable]
	public class Period : IEquatable<Period> {
		public Period(DateTime from, DateTime to) {
			From = from;
			To = to;
		}

		public DateTime From { get; private set; }
		public DateTime To { get; private set; }

		public bool Contains(DateTime date) {
			return From <= date && date < To;
		}

		public bool Equals(Period other) {
			return !ReferenceEquals(other, null) &&
					(ReferenceEquals(this, other) ||
					 From == other.From && To == other.To);
		}

		public override bool Equals(object obj) {
			return Equals(obj as Period);
		}

		public override int GetHashCode() {
			return From.GetHashCode();
		}
	}
}
