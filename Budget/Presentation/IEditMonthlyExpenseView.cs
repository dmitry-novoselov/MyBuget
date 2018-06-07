using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Budget.Presentation {
	public interface IEditMonthlyExpenseView : IView {
		PEMonthlyExpense Expense { set; }

		Action OnOK { set; }
	}
}
