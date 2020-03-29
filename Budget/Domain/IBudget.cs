#region Usings

using System;
using System.Collections.Generic;

#endregion

namespace Budget.Domain {
	public interface IBudget {
		IEnumerable<BudgetWeek> Weeks { get; }
		IEnumerable<CashStatement> Remainders { get; }
		IEnumerable<CashStatement> Investments { get; }
		IEnumerable<CashStatement> Expenses { get; }
		IEnumerable<MonthlyCashStatement> MonthlyCashMovements { get; }
		int MonthlyBalance { get; }
		MonthlyActualBalances MonthlyActualBalances { get; }

		int GetRemainder(DateTime date);
		int GetFreeMoney(DateTime date);
	}
}
