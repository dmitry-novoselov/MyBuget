using Budget.Domain;

namespace Budget.Presentation.SetRemainderUseCase {
	public class SetRemainderUseCase : AddTransferUseCaseBase, ISetRemainderUseCase {
		public SetRemainderUseCase(ICalculationDataProvider dataProvider, IEditTransferView view)
			: base(dataProvider, "Задать остаток", view, dataProvider.WalletRemainders.OveralAmount(), transfer => dataProvider.SetRemainder(transfer.Date, transfer.Amount)) {
		}
	}
}
