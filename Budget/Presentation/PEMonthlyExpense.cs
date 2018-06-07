using System;
using System.Collections.Generic;

namespace Budget.Presentation {
	public class PEMonthlyExpense {
		private string description;

		public List<PESelectableExpenseItem> ExpenseItems { get; set; }

		public PESelectableExpenseItem ExpenseItem { get; set; }
		public DateTime Month { get; set; }
		public DateTime Date { get; set; }
        public int Amount { get; set; }
        public bool IsFinal { get; set; }

		public string Description {
			get { return description ?? ""; }
			set { description = value ?? ""; }
		}

		internal bool CategoryIsNegative {
			get {return ExpenseItem.ExpenseItem.IsNegative;}
		}
	}
}
