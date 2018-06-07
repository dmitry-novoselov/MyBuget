#region Usings

using System;

#endregion

namespace Budget.Domain {
	[Serializable]
	public class MonthlyCashStatementCategory {
		public MonthlyCashStatementCategory(int dayOfMonth, int expense, string name) {
			DayOfMonth = dayOfMonth;
			Amount = expense;
			Name = name;
			Effective = new Period(DateTimeService.CurrentMonthFirstDay, DateTimeService.MaxValue);
		}

		public string Name { get; set; }
		public int DayOfMonth { get; set; }
		public int Amount { get; set; }
		public Period Effective { get; set; }

		public bool IsNegative {
			get { return Amount <= 0; }
		}

		public override bool Equals(object obj) {
			var other = obj as MonthlyCashStatementCategory;
			return !ReferenceEquals(other, null) &&
				DayOfMonth == other.DayOfMonth &&
				Amount == other.Amount &&
				Name == other.Name;
		}

		public override int GetHashCode() {
			return DayOfMonth;
		}
	}
}
