using System;

namespace Budget.Domain {
	[Serializable]
	public class YearMonth : IEquatable<YearMonth> {
		public YearMonth(int month, int year) {
			Month = month;
			Year = year;
		}

		public int Month { get; private set; }
		public int Year { get; private set; }

		public DateTime GetDate(int dayOfMonth) {
			return new DateTime(Year, Month, Math.Min(dayOfMonth, DateTime.DaysInMonth(Year, Month)));
		}

		public bool Equals(YearMonth other) {
			return
				ReferenceEquals(this, other) ||
				!ReferenceEquals(other, null) &&
				Month == other.Month && Year == other.Year;
		}

		public override bool Equals(object obj) {
			return Equals(obj as YearMonth);
		}

		public override int GetHashCode() {
			return Year * Month;
		}

		public static bool operator ==(YearMonth x, YearMonth y) {
			if (ReferenceEquals(x, y)) {
				return true;
			}

			if (ReferenceEquals(x, null)) {
				return false;
			}

			return x.Equals(y);
		}

		public static bool operator !=(YearMonth x, YearMonth y) {
			return !(x == y);
		}

		public static implicit operator YearMonth(DateTime month) {
			return new YearMonth(month.Month, month.Year);
		}
	}
}
