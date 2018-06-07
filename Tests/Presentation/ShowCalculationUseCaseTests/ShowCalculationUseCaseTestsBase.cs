#region

using System;
using System.Collections.Generic;
using System.Linq;
using Budget;
using Budget.Domain;
using Budget.Presentation.ShowCalculationUseCase;
using Moq;
using Tests.Fakes;
using Tests.Presentation.Fakes;

#endregion

namespace Tests.Presentation.ShowCalculationUseCaseTests {
	public class ShowCalculationUseCaseTestsBase : FixtureBase {
		internal Mock<IDataDeletionService> deletionServiceMock;
		internal BudgetServiceFake budgetService;
		internal ShowCalculationViewFake view;

		internal BudgetFake budget;

		protected void MakeUseCaseRunnable() {
			budgetService = new BudgetServiceFake();
			view = new ShowCalculationViewFake();

			budget = budgetService.Budget;

			deletionServiceMock = new Mock<IDataDeletionService>(MockBehavior.Strict);
		}

		protected void ArrangeWeeks(params DateTime[] firstDaysOfWeeks) {
			budget.Weeks = firstDaysOfWeeks.Convert(d => new BudgetWeek(d));
		}

		protected void Run() {
			new ShowCalculationUseCase(budgetService, view, deletionServiceMock.Object).Run();
		}

		protected void ArrangeReminders(DateTime date, int amount) {
			var current = budget.Remainders ?? Enumerable.Empty<CashStatement>();
			budget.Remainders = current.Union(new[] { new CashStatement(date, amount) }).ToList();
		}

		protected void ArrangeCashMovement(string description, DateTime date, int amount) {
			if (amount <= 0) {
				budget.Expenses = ArrangeMovement(description, date, amount, budget.Expenses);
			} else {
				budget.Investments = ArrangeMovement(description, date, amount, budget.Investments);
			}
		}

		private IEnumerable<CashStatement> ArrangeMovement(string description, DateTime date, int amount, IEnumerable<CashStatement> movements) {
			var current = movements ?? Enumerable.Empty<CashStatement>();
			return current.Union(new[] {new CashStatement(date, amount, description)}).ToList();
		}

		protected void ArrangeMonthlyMovement(string name, YearMonth month, DateTime date, int amount, string description = "") {
			var current = budget.MonthlyCashMovements ?? Enumerable.Empty<MonthlyCashStatement>();
			budget.MonthlyCashMovements = current.Union(new[] { new MonthlyCashStatement(new MonthlyCashStatementCategory(1, amount, name), month, date, amount, description) }).ToList();
		}
	}
}
