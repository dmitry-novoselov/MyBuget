using System;
using System.Collections.Generic;
using System.Linq;

namespace Budget.Domain {
	public class Budget : IBudget {
		private readonly List<BudgetWeek> weeks = new List<BudgetWeek>();
		private readonly Dictionary<DateTime, int> calculatedRemainders = new Dictionary<DateTime, int>();
		private readonly List<CashStatement> freeMoney = new List<CashStatement>();

		public Budget() {
			Remainders = new List<CashStatement>();
			Investments = new List<CashStatement>();
			Expenses = new List<CashStatement>();
			MonthlyCashMovements = new List<MonthlyCashStatement>();
		}

		public IEnumerable<BudgetWeek> Weeks {
			get { return weeks; }
		}

		public IEnumerable<CashStatement> Remainders { get; internal set; }
		public IEnumerable<CashStatement> Investments { get; internal set; }
		public IEnumerable<CashStatement> Expenses { get; internal set; }
		public IEnumerable<MonthlyCashStatement> MonthlyCashMovements { get; internal set; }

		public int MonthlyBalance { get; internal set; }
		public MonthlyActualBalances MonthlyActualBalances { get; internal set; }

		public int GetRemainder(DateTime date) {
			var remainder = Remainders.SingleOrDefault(r => r.Date == date);
			return remainder != null ? remainder.Amount : calculatedRemainders[date];
		}

		public int GetFreeMoney(DateTime date) {
			return freeMoney.Where(m => m.Date <= date).Sum(m => m.Amount);
		}

		internal void AddWeek(BudgetWeek week) {
			weeks.Add(week);
		}

		internal void SetRemainder(DateTime date, int amount) {
			calculatedRemainders[date] = amount;
		}

		internal void AddFreeMoney(DateTime date, int amount) {
			freeMoney.Add(new CashStatement(date, amount));
		}
	}
}