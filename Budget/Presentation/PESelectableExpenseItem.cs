#region

using System;
using System.Linq;
using System.Collections.Generic;
using Budget.Domain;

#endregion

namespace Budget.Presentation {
	public class PESelectableExpenseItem : IEquatable<PESelectableExpenseItem> {
		public static List<PESelectableExpenseItem> CreateListFrom(ICalculationDataProvider dataProvider) {
			return dataProvider.GetMonthlyCashStatementCategories()
				.OrderBy(x => x.Name)
				.Convert(expenseItem => new PESelectableExpenseItem(expenseItem))
				.ToList();
		}

		private readonly MonthlyCashStatementCategory expenseItem;

		public PESelectableExpenseItem(MonthlyCashStatementCategory expenseItem) {
			this.expenseItem = expenseItem;
		}

		public MonthlyCashStatementCategory ExpenseItem {
			get { return expenseItem; }
		}

		public override string ToString() {
			return expenseItem.Name;
		}

		public bool Equals(PESelectableExpenseItem other) {
			return !ReferenceEquals(other, null) &&
				Equals(expenseItem, other.expenseItem);
		}

		public override bool Equals(object obj) {
			return Equals(obj as PESelectableExpenseItem);
		}

		public override int GetHashCode() {
			return ToString().GetHashCode();
		}
	}
}
