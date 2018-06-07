using Budget.Domain;

namespace Budget.Presentation.EditCashMovementUseCase {
	public interface IEditCashMovementUseCase {
		void Run(CashStatement movement);
	}
}
