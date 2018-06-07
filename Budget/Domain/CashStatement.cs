#region Usings

using System;

#endregion

namespace Budget.Domain {
	[Serializable]
	public class CashStatement : IDated, IEquatable<CashStatement> {
		public CashStatement(DateTime date, int value)
			: this(date, value, "") { }

		public CashStatement(DateTime date, int value, string description) {
			Date = date;
			Amount = value;
			Description = description;
		}

		public DateTime Date { get; set; }
		public int Amount { get; set; }
		public string Description { get; set; }

		public bool Equals(CashStatement other) {
			return
				!ReferenceEquals(other, null) &&
				Date == other.Date &&
				Amount == other.Amount &&
				Description == other.Description;
		}

		public override bool Equals(object obj) {
			return Equals(obj as CashStatement);
		}

		public override int GetHashCode() {
			return (Date + Description).GetHashCode();
		}

		public static bool operator ==(CashStatement x, CashStatement y) {
			if (ReferenceEquals(x, y))
				return true;

			if (ReferenceEquals(x, null))
				return false;

			return x.Equals(y);
		}

		public static bool operator !=(CashStatement x, CashStatement y) {
			return !(x == y);
		}
	}
}
