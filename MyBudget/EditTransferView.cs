using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Budget.Infrastructure;
using Budget.Presentation;

namespace MyBudget {
	public partial class EditTransferView : Form, IEditTransferView {
		private Binder<PETransfer> binder = new Binder<PETransfer>();

		public EditTransferView() {
			InitializeComponent();

			binder.Bind(date, x => x.Date);
			binder.Bind(amount, x => x.Amount);
			binder.Bind(description, x => x.Description);
		}

		public PETransfer Transfer {
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
