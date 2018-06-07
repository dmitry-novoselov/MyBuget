using System;

namespace Budget.Domain {
	public interface IBudgetService {
		IBudget CalculateBudget();
	}
}
