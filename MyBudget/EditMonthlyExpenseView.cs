using System;
using System.Windows.Forms;
using Budget.Infrastructure;
using Budget.Presentation;

namespace MyBudget {
	public partial class EditMonthlyExpenseView : Form, IEditMonthlyExpenseView {
		private readonly Binder<PEMonthlyExpense> binder = new Binder<PEMonthlyExpense>();

		public EditMonthlyExpenseView() {
			InitializeComponent();

			binder.Bind(yearMonth, x => x.Month);
			binder.Bind(expenseItems, x => x.ExpenseItems);
			binder.Bind(expenseItems, x => x.ExpenseItem);
            binder.Bind(date, x => x.Date);
            binder.Bind(isFinal, x => x.IsFinal);
			binder.Bind(amount, x => x.Amount);
			binder.Bind(description, x => x.Description);
		}

		public PEMonthlyExpense Expense {
			set { binder.DataSource = value; }
		}

		public Action OnOK { private get; set; }

		private void ok_Click(object sender, EventArgs e) {
			OnOK();
			Close();
		}

		private void cancel_Click(object sender, EventArgs e) {
			Close();
		}
	}
}
