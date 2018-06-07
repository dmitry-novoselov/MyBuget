using System;
using Budget.Presentation;
using Budget.Presentation.AddExpenseItemUseCase;

namespace Tests.Presentation.Fakes {
	internal class EditExpenseItemViewFake : IEditExpenseItemView {
		public PEEditableExpenseItem MonthlyExpense { get; set; }
		public Action OnOK { get; set; }

		public string Text { get; set; }
		public void Show() { IsShown = true; }

		public bool IsShown { get; set; }
	}
}
