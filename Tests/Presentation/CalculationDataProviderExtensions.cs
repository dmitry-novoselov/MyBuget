#region Usings

using System;
using Budget.Domain;

#endregion

namespace Tests {
	public static class CalculationDataProviderExtensions {
		public static void AddExpenseItem(this ICalculationDataProvider dataProvider, int dayOfMonth, int amount, string name) {
			dataProvider.AddMonthlyCashStatementCategory(dayOfMonth, amount, name, DateTimeService.CurrentMonthFirstDay, DateTimeService.MaxValue);
		}
	}
}
