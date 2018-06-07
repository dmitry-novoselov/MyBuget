
namespace Budget.Presentation.ShowCalculationUseCase {
	public interface IShowCalculationView {
		IDataGrid<PEBudgetRow> CalculationResults { get; }
		string MonthlyBalance { set; }
	}
}
