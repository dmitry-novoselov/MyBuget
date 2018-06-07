using Budget.Domain;

namespace Tests.Fakes {
	internal class BudgetServiceFake : IBudgetService {
		public readonly BudgetFake Budget = new BudgetFake();

		public IBudget CalculateBudget() {
			return Budget;
		}
	}
}
