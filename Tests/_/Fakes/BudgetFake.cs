#region Usings

using System;
using System.Collections.Generic;
using Budget.Domain;

#endregion

namespace Tests.Fakes {
	internal class BudgetFake : IBudget {
		private int freeMoneyOnNotDefinedDay;
		private Dictionary<DateTime, int> freeMoney = new Dictionary<DateTime, int>();

		private IEnumerable<BudgetWeek> weeks;
		private IEnumerable<CashStatement> remainders;
		private IEnumerable<CashStatement> investments;
		private IEnumerable<CashStatement> purchases;
		private IEnumerable<MonthlyCashStatement> monthlyExpenses;

		public IEnumerable<BudgetWeek> Weeks {
			get { return weeks ?? new List<BudgetWeek>(); }
			set { weeks = value; }
		}

		public IEnumerable<CashStatement> Remainders {
			get { return remainders ?? new List<CashStatement>(); }
			set { remainders = value; }
		}

		public IEnumerable<CashStatement> Investments {
			get { return investments ?? new List<CashStatement>(); }
			set { investments = value; }
		}

		public IEnumerable<CashStatement> Expenses {
			get { return purchases ?? new List<CashStatement>(); }
			set { purchases = value; }
		}

		public IEnumerable<MonthlyCashStatement> MonthlyCashMovements {
			get { return monthlyExpenses ?? new List<MonthlyCashStatement>(); }
			set { monthlyExpenses = value; }
		}

		public int MonthlyBalance { get; set; }
		public MonthlyActualBalances MonthlyActualBalances { get; }

		public int GetRemainder(DateTime date) {
			throw new NotImplementedException();
		}

		public int GetFreeMoney(DateTime date) {
			return freeMoney.ContainsKey(date) ? freeMoney[date] : freeMoneyOnNotDefinedDay;
		}

		internal void SetFreeMoney(int amount) {
			freeMoneyOnNotDefinedDay = amount;
		}

		internal void SetFreeMoney(DateTime date, int amount) {
			freeMoney[date] = amount;
		}
	}
}
