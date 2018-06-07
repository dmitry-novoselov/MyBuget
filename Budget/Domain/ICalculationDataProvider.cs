#region Usings

using System;
using System.Collections.Generic;

#endregion

namespace Budget.Domain {
	public interface ICalculationDataProvider {
		Period CalculationPeriod { get; set; }
		int EnvelopeSize { get; set; }

		void SetRemainder(DateTime dateTime, int amount);
		List<CashStatement> GetRemainders();

		void AddCashMovement(DateTime date, int amount, string description);
		List<CashStatement> GetCashMovements();

		void AddMonthlyCashStatementCategory(int dayOfMonth, int amount, string name, DateTime from, DateTime to);
		List<MonthlyCashStatementCategory> GetMonthlyCashStatementCategories();

        void AddMonthlyCashStatement(MonthlyCashStatementCategory category, YearMonth month, DateTime date, int amount, string description, bool isFinal);
		List<MonthlyCashStatement> GetMonthlyCashMovements();

		string WalletRemainders { get; set; }
	}
}
