using System;

namespace Budget.Presentation {
	public interface IEditExpenseItemView : IView {
		PEEditableExpenseItem MonthlyExpense { set; }

		Action OnOK { set; }
	}
}
