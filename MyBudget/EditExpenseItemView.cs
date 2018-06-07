using System;
using System.Windows.Forms;
using Budget.Infrastructure;
using Budget.Presentation;
using Budget.Presentation.AddExpenseItemUseCase;

namespace MyBudget {
	public partial class EditExpenseItemView : Form, IEditExpenseItemView {
		private Binder<PEEditableExpenseItem> binder = new Binder<PEEditableExpenseItem>();

		public EditExpenseItemView() {
			InitializeComponent();

			binder.Bind(dayOfMonth, x => x.DayOfMonth);
			binder.Bind(amount, x => x.Amount);
			binder.Bind(name, x => x.Name);
			binder.Bind(from, x => x.From);
			binder.Bind(to, x => x.To);
		}

		public PEEditableExpenseItem MonthlyExpense {
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
