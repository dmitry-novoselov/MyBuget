using System;
using Budget.Presentation;

namespace Tests.Presentation.Fakes {
	internal class EditMonthlyExpenseViewFake : IEditMonthlyExpenseView {
		public PEMonthlyExpense Expense { get; set; }

		public Action OnOK { get; set; }

		public string Text { get; set; }
		public void Show() { IsShown = true; }

		public bool IsShown { get; set; }
	}
}
