#region Usings

using Budget.Presentation;
using Budget.Presentation.ShowCalculationUseCase;

#endregion

namespace Tests.Presentation.Fakes {
	internal class ShowCalculationViewFake : IShowCalculationView {
		private readonly DataGridFake<PEBudgetRow> calculationResults = new DataGridFake<PEBudgetRow>();

		public IDataGrid<PEBudgetRow> CalculationResults {
			get { return CalculationResultsFake; }
		}

		public DataGridFake<PEBudgetRow> CalculationResultsFake {
			get { return calculationResults; }
		}

		public string MonthlyBalance { get; set; }
	}
}
