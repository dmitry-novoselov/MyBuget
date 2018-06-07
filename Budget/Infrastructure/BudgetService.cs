using Budget.Domain;

namespace Budget.Infrastructure {
	public class BudgetService : IBudgetService {
		private readonly ICalculationDataProvider dataProvider;

		public BudgetService(ICalculationDataProvider dataProvider) {
			this.dataProvider = dataProvider;
		}

		public IBudget CalculateBudget() {
			return new BudgetCalculation(new CalculationDataPreprocessor(dataProvider)).CalculateBudget();
		}
	}
}
