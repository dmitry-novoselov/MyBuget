using Budget.Domain;

namespace Budget.Presentation.AddExpenseUseCase {
	public class AddExpenseUseCase : AddTransferUseCaseBase, IAddExpenseUseCase {
		public AddExpenseUseCase(ICalculationDataProvider dataProvider, IEditTransferView view)
			: base(dataProvider, "Добавить трату", view, transfer => dataProvider.AddCashMovement(transfer.Date, -transfer.Amount, transfer.Description)) { }
	}
}
