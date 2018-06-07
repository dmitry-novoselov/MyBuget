#region Usings

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Budget.Domain {
	public class CalculationDataPreprocessor {
		public CalculationDataPreprocessor(ICalculationDataProvider dataProvider) {
			EnvelopeSize = dataProvider.EnvelopeSize;
			CalculationPeriod = dataProvider.CalculationPeriod;

			CashMovements = new DatedSet<CashStatement>(dataProvider.GetCashMovements());
			Remainders = new DatedSet<CashStatement>(dataProvider.GetRemainders());
			MonthlyCashMovements = new DatedSet<MonthlyCashStatement>(ConvertToExpenses(dataProvider));
			MonthlyCashMovementCategories = new List<MonthlyCashStatementCategory>(dataProvider.GetMonthlyCashStatementCategories());
		}

		public int EnvelopeSize { get; private set; }
		public Period CalculationPeriod { get; private set; }

		public DatedSet<CashStatement> CashMovements { get; private set; }
		public DatedSet<CashStatement> Remainders { get; private set; }
		public DatedSet<MonthlyCashStatement> MonthlyCashMovements { get; private set; }
		public List<MonthlyCashStatementCategory> MonthlyCashMovementCategories { get; private set; }

		private List<MonthlyCashStatement> ConvertToExpenses(ICalculationDataProvider dataProvider) {
			var result = new List<MonthlyCashStatement>();

			for (var month = CalculationPeriod.From.MonthFirstDay(); month <= CalculationPeriod.To.MonthFirstDay(); month = month.NextMonth()) {
				foreach (var expense in dataProvider.GetMonthlyCashStatementCategories()) {
					AddExpense(result, month, expense, dataProvider.GetMonthlyCashMovements());
				}
			}

			result.AddRange(dataProvider.GetMonthlyCashMovements());

			return result;
		}

		private void AddExpense(List<MonthlyCashStatement> result, YearMonth month, MonthlyCashStatementCategory category, List<MonthlyCashStatement> movements) {
            var actualMovements = movements.Where(m => m.Category == category && m.Month == month).ToList();

            var paidAmount = actualMovements.Sum(_ => _.Amount);
            var wasPaid = actualMovements.Any(_ => _.IsFinalPayment) || Math.Abs(category.Amount) <= Math.Abs(paidAmount);
            var expenseDate = month.GetDate(category.DayOfMonth);

            if (!wasPaid && CalculationPeriod.Contains(expenseDate) && category.Effective.Contains(expenseDate)) {
                result.Add(new MonthlyCashStatement(category, month, expenseDate, category.Amount - paidAmount, "<план>"));
            }
		}
	}
}
