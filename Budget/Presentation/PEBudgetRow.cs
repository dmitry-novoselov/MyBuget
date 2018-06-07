using System;
using System.Drawing;

namespace Budget.Presentation {
	public class PEBudgetRow : IEquatable<PEBudgetRow> {
		public static readonly Color Default = Color.White;
		public static readonly Color CurrentWeekMonthlyIncome = Color.LightGreen;
		public static readonly Color CurrentWeekIncome = Color.LawnGreen;
		public static readonly Color CurrentWeekMonthlyOutcome = Color.Khaki;
		public static readonly Color CurrentWeekOutcome = Color.DarkKhaki;

		public PEBudgetRow() {
			Event = "";
			Date = "";
			Amount = "";
			BackgroundColor = Color.White;

			OnEdit = () => { };
			OnConfigure = () => { };
			OnDelete = () => { };
		}

		public string Event { get; set; }
		public string Date { get; set; }
		public string Amount { get; set; }
		public Color BackgroundColor { get; set; }

		internal Action OnEdit { private get; set; }
		internal Action OnDelete { private get; set; }
		internal Action OnConfigure { private get; set; }

		public void Edit() {
			OnEdit();
		}

		public void Configure() {
			OnConfigure();
		}

		public void Delete() {
			OnDelete();
		}

		public bool Equals(PEBudgetRow other) {
			return
				ReferenceEquals(this, other) ||
				!ReferenceEquals(other, null) &&
				Event == other.Event &&
				Date == other.Date &&
				Amount == other.Amount &&
				BackgroundColor == other.BackgroundColor;
		}

		public override bool Equals(object obj) {
			return Equals(obj as PEBudgetRow);
		}

		public override int GetHashCode() {
			return (Event + Date + Amount).GetHashCode();
		}

		public override string ToString() {
			return string.Format("{0} {1} {2} {3}", Event, Date, Amount, BackgroundColor);
		}

		public static bool operator==(PEBudgetRow x, PEBudgetRow y){
			if (ReferenceEquals(x, y)) return true;
			if (ReferenceEquals(x, null)) return false;

			return x.Equals(y);
		}

		public static bool operator !=(PEBudgetRow x, PEBudgetRow y) {
			return !(x == y);
		}
	}
}
