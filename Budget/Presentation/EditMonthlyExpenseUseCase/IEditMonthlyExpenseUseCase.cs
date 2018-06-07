using Budget.Domain;

namespace Budget.Presentation.EditMonthlyExpenseUseCase {
	public interface IEditMonthlyExpenseUseCase {
		void Run(MonthlyCashStatement expense);
	}
}
