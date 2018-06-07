using Budget.Domain;

namespace Budget.Presentation.AddInvestmentUseCase {
	public class AddInvestmentUseCase : AddTransferUseCaseBase, IAddInvestmentUseCase {
		public AddInvestmentUseCase(ICalculationDataProvider dataProvider, IEditTransferView view)
			: base(dataProvider, "Добавить доход", view, transfer => dataProvider.AddCashMovement(transfer.Date, transfer.Amount, transfer.Description)) { }
	}
}
