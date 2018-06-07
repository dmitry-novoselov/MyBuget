using Budget.Domain;

namespace Budget.Presentation.EditRemainderUseCase {
	public interface IEditRemainderUseCase {
		void Run(CashStatement remainder);
	}
}
