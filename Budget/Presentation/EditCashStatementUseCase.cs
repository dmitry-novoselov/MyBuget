#region Usings

using Budget.Domain;
using Budget.Presentation.ShowCalculationUseCase;
using StructureMap;
using Budget.Presentation.EditRemainderUseCase;
using Budget.Presentation.EditCashMovementUseCase;
using System;

#endregion

namespace Budget.Presentation {
	public class EditCashStatementUseCase : IEditCashMovementUseCase, IEditRemainderUseCase {
		private readonly IDataSavingService dataSaver;
		private readonly IEditTransferView view;

		private Func<int, int> negateAmount;
		private CashStatement transfer;
		private PETransfer peTransfer;

		public EditCashStatementUseCase(IDataSavingService dataSaver, IEditTransferView view) {
			this.dataSaver = dataSaver;
			this.view = view;

			negateAmount = amount => amount;
		}

		void IEditCashMovementUseCase.Run(CashStatement statement) {
			if (statement.Amount > 0) {
				Edit("доход", statement);
			} else {
				NegateAmountWhen().Edit("трату", statement);
			}
		}

		private EditCashStatementUseCase NegateAmountWhen() {
			negateAmount = amount => -amount;
			return this;
		}

		void IEditRemainderUseCase.Run(CashStatement statement) {
			Edit("остаток", statement);
		}

		private void Edit(string transferName, CashStatement transfer) {
			this.transfer = transfer;

			view.Transfer = peTransfer = new PETransfer {
				Date = transfer.Date,
				Amount = negateAmount(transfer.Amount),
				Description = transfer.Description,
			};

			view.Text = "Изменить " + transferName;
			view.OnOK = OnTransferIsEdited;

			view.Show();
		}

		private void OnTransferIsEdited() {
			transfer.Date = peTransfer.Date;
			transfer.Amount = negateAmount(peTransfer.Amount);
			transfer.Description = peTransfer.Description;

			dataSaver.Save();

			ObjectFactory.GetInstance<IShowCalculationUseCase>().Run();
		}
	}
}
