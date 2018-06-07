using System;
using System.Collections.Generic;
using System.Linq;
using Budget.Utils;

namespace Budget.Domain {
	public class BudgetCalculation {
		private readonly CalculationDataPreprocessor calculationData;

		private readonly Budget budget = new Budget();
		private readonly Dictionary<BudgetWeek, DateTime> lastRemainderDate = new Dictionary<BudgetWeek, DateTime>();

		private IEnumerable<CashStatement> invesments;
		private DatedSet<CashStatement> expenses;
		private IEnumerable<MonthlyCashStatement> montlyInvestments;
		private DatedSet<MonthlyCashStatement> monthlyExpenses;
		private DatedSet<BudgetWeek> budgetWeeks;

		public BudgetCalculation(CalculationDataPreprocessor calculationData) {
			this.calculationData = calculationData;
		}

		public Budget CalculateBudget() {
			if (InitialRemainderIsSet) {
				SetupCollections();
				CalculateTotalRemainders();
				CalculateWeekOutcomes();
				CalculateWeekRemainders();
				CalculateCoverages();
				CalculateMonthlyBalance();
			}

			return budget;
		}

		private void CalculateMonthlyBalance() {
            var monthlyCategoriesBalance = calculationData.MonthlyCashMovementCategories
                .Where(c => c.Effective.To == DateTimeService.MaxValue)
                .Sum(c => c.Amount);
            budget.MonthlyBalance = monthlyCategoriesBalance - DayEnvelopeSize * 31;
		}

		private bool InitialRemainderIsSet {
			get {
				var initialRemainderDate = calculationData.CalculationPeriod.From.AddDays(-1);
				return Remainders.Any(r => r.Date == initialRemainderDate);
			}
		}

		private Period Period {
			get { return calculationData.CalculationPeriod; }
		}

		private int EnvelopeSize {
			get { return calculationData.EnvelopeSize; }
		}

		private int DayEnvelopeSize {
			get { return EnvelopeSize / 7; }
		}

		private DatedSet<CashStatement> Remainders {
			get { return calculationData.Remainders; }
		}

		private DatedSet<CashStatement> CashMovements {
			get { return calculationData.CashMovements; }
		}

		private DatedSet<MonthlyCashStatement> MonthlyCashMovements {
			get { return calculationData.MonthlyCashMovements; }
		}

		private IEnumerable<CashStatement> Investments {
			get { return invesments ?? (invesments = calculationData.CashMovements.Where(m => m.Amount > 0)); }
		}

		private DatedSet<CashStatement> Expenses {
			get { return expenses ?? (expenses = new DatedSet<CashStatement>(calculationData.CashMovements.Where(m => m.Amount <= 0))); }
		}

		private IEnumerable<MonthlyCashStatement> MonthlyInvestments {
			get { return montlyInvestments ?? (montlyInvestments = calculationData.MonthlyCashMovements.Where(m => m.Amount > 0)); }
		}

		private DatedSet<MonthlyCashStatement> MonthlyExpenses {
			get { return monthlyExpenses ?? (monthlyExpenses = new DatedSet<MonthlyCashStatement>(calculationData.MonthlyCashMovements.Where(m => m.Amount <= 0))); }
		}

		private int PriorWeekRemainder(BudgetWeek week) {
			return budget.GetRemainder(week.FirstDay.AddDays(-1));
		}

		private void SetupCollections() {
			budget.Remainders = Remainders;
			budget.Investments = Investments;
			budget.Expenses = Expenses;
			budget.MonthlyCashMovements = calculationData.MonthlyCashMovements;
		}

		private void CalculateTotalRemainders() {
			for (var week = new BudgetWeek(Period.From); week.From < Period.To; week = new BudgetWeek(week.To)) {
				budget.AddWeek(week);

				SetLastRemainderDateOf(week);

				var daysToCalculate = new Set<DateTime> { week.FirstDay, week.LastDay };
				daysToCalculate.AddRange(CashInvestmentDates(week));
				daysToCalculate.AddRange(MonthlyExpensesDates(week));
				CalculateRemaindersForDates(week, daysToCalculate);
			}

			budgetWeeks = new DatedSet<BudgetWeek>(budget.Weeks);
		}

		private IEnumerable<DateTime> CashInvestmentDates(BudgetWeek week) {
			return DatesWithin(week, CashMovements);
		}

		private IEnumerable<DateTime> MonthlyExpensesDates(BudgetWeek week) {
			return DatesWithin(week, MonthlyCashMovements);
		}

		private static IEnumerable<DateTime> DatesWithin<T>(BudgetWeek week, DatedSet<T> transfers) where T : IDated {
			return transfers[week].Convert(i => i.Date).Distinct();
		}

		private void SetLastRemainderDateOf(BudgetWeek week) {
			var lastRemainder = Remainders[week].LastOrDefault();
			if (lastRemainder != null) {
				lastRemainderDate[week] = lastRemainder.Date;
			} else {
				lastRemainderDate[week] = week.FirstDay.AddDays(-1);
			}
		}

		private void CalculateRemaindersForDates(BudgetWeek week, IEnumerable<DateTime> dates) {
			foreach (var date in dates) {
				var calculatedRemainder =
					PriorWeekRemainder(week) -
					DayEnvelopeSize * ((date - week.FirstDay).Days + 1);

				var period = new Period(week.FirstDay, date.AddDays(1));

				var remainder = calculatedRemainder +
					CashMovements[period].Sum(_ => _.Amount) +
					MonthlyCashMovements[period].Sum(_ => _.Amount);

				budget.SetRemainder(date, remainder);
			}
		}

		private void CalculateWeekOutcomes() {
			foreach (var week in budget.Weeks) {
				var calculatedRemainder =
					PriorWeekRemainder(week) -
					EnvelopeSize +
					CashMovements[week].Sum(i => i.Amount) +
					MonthlyCashMovements[week].Sum(p => p.Amount);

				var remainder = Remainders.SingleOrDefault(r => r.Date == week.LastDay);

				if (remainder != null) {
					budget.AddFreeMoney(week.LastDay, remainder.Amount - calculatedRemainder);
				}
			}
		}

		private void CalculateWeekRemainders() {
			foreach (var week in budget.Weeks) {
				var period = new Period(week.FirstDay, lastRemainderDate[week].AddDays(1));

				var balance = budget.GetRemainder(week.FirstDay.AddDays(-1)) -
					budget.GetRemainder(lastRemainderDate[week]) +
					CashMovements[period].Sum(x => x.Amount) +
					MonthlyCashMovements[period].Sum(x => x.Amount);

				week.Remainder = EnvelopeSize - balance;
				week.DayEnvelopeSize = week.Remainder / Math.Max(1, (week.LastDay - lastRemainderDate[week]).Days);
			}
		}

		private void CalculateCoverages() {
			var to = budget.Weeks.Last().To;

			var remainder = 0;
			foreach (var investmentsSet in AllInvestments.GroupBy(i => i.Date).OrderByDescending(g => g.Key)) {
				var from = investmentsSet.Key;
				var coveredPeriod = new Period(from, to);

				var weeksToCover = budgetWeeks[coveredPeriod];

				remainder +=
					investmentsSet.Sum(i => i.Amount) -
					EnvelopeSize * weeksToCover.Count() +
					MonthlyExpenses[coveredPeriod].Sum(e => e.Amount) +
					Expenses[coveredPeriod].Sum(e => e.Amount); ;

				if (remainder >= 0) {
					budget.AddFreeMoney(investmentsSet.Key, remainder);

					remainder = 0;
				}

				to = from;
			}

			var prevWeekRemainder = Remainders.Last(r => r.Date < budget.Weeks.First().From);

			if (prevWeekRemainder.Date <= to) {
				var from = prevWeekRemainder.Date;
				var coveredPeriod = new Period(from, to);

				var weeksToCover = budgetWeeks[coveredPeriod];

				remainder +=
					budget.GetRemainder(prevWeekRemainder.Date) -
					EnvelopeSize * weeksToCover.Count() +
					MonthlyExpenses[coveredPeriod].Sum(e => e.Amount) +
					Expenses[coveredPeriod].Sum(e => e.Amount); ;

				budget.AddFreeMoney(prevWeekRemainder.Date, remainder);
			}
		}

		private IEnumerable<Investment> AllInvestments {
			get {
				return
					(from i in Investments
					 select new Investment { Amount = i.Amount, Date = i.Date })
					.Union
					(from e in MonthlyInvestments
					 select new Investment { Amount = e.Amount, Date = e.Date });
			}
		}

		private class Investment {
			public int Amount;
			public DateTime Date;
		}
	}
}
