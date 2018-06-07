#region Usings

using Budget.Domain;

#endregion

namespace Budget.Presentation.EditExpenseItemUseCase {
	public interface IEditExpenseItemUseCase {
		void Run(MonthlyCashStatementCategory expense);
	}
}
