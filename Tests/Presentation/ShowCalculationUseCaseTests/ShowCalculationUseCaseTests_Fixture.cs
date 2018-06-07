#region

using System.Linq;
using Budget;
using Budget.Domain;
using Budget.Presentation;
using NUnit.Framework;
using System.Linq.Expressions;
using System;

#endregion

namespace Tests.Presentation.ShowCalculationUseCaseTests {
	[TestFixture, SetCulture("ru-RU")]
	public class ShowCalculationUseCaseTests_Fixture : ShowCalculationUseCaseTestsBase {
		[SetUp]
		public void SetUp() {
			MakeUseCaseRunnable();
		}

		[TearDown]
		public void TearDown() {
			deletionServiceMock.VerifyAll();
		}

		private void ExpectDeletion(Expression<Action<IDataDeletionService>> deletion) {
			deletionServiceMock.Setup(deletion);
		}

		[Test]
		public void CanRunUseCaseTwice() {
			ArrangeWeeks(2.02.of2009());
			//

			Run();
			Run();
		}

		[Test]
		public void ShowWeeks() {
			budget.Remainders = new[] {
				new CashStatement(15.03.of2009(), 70),
			};
			budget.Weeks = new[] {
				new BudgetWeek(02.02.of2009()),
				new BudgetWeek(09.02.of2009()),
				new BudgetWeek(16.02.of2009()),
				new BudgetWeek(23.02.of2009()),
				new BudgetWeek(12.03.of2009()),
			};
			budget.SetFreeMoney(10);
			//

			Run();

			CollectionAssert.AreEqual(
				new[] {
					new PEBudgetRow {Date = "Февраль"},
					new PEBudgetRow {Date = "Неделя 02.02.2009 - 08.02.2009", Amount = "Свободные 10"},
					new PEBudgetRow {Date = "Неделя 09.02.2009 - 15.02.2009", Amount = "Свободные 10"},
					new PEBudgetRow {Date = "Неделя 16.02.2009 - 22.02.2009", Amount = "Свободные 10"},
					new PEBudgetRow {Date = "Неделя 23.02.2009 - 01.03.2009", Amount = "Свободные 10"},
					new PEBudgetRow {Date = "Март"},
					new PEBudgetRow {Date = "Неделя 12.03.2009 - 18.03.2009", Amount = "Свободные 10"},
					new PEBudgetRow {Event = "Остаток", Date = "15.03.2009", Amount = "Конверт 0, Остатки 70, Свободные 10"},
				},
				view.CalculationResultsFake.DataSource);
		}

		[Test]
		public void ShowRemainders() {
			budget.Weeks = new[] { new BudgetWeek(2.02.of2009()) };
			budget.Remainders = new[] { new CashStatement(3.02.of2009(), 140) };
			budget.SetFreeMoney(3.02.of2009(), 70);
			//

			Run();

			CollectionAssert.AreEqual(
				new[] {
					new PEBudgetRow {Date = "Февраль"},
					new PEBudgetRow {Date = "Неделя 02.02.2009 - 08.02.2009", Amount = "Свободные 0"},
					new PEBudgetRow {Event = "Остаток", Date = "03.02.2009", Amount = "Конверт 0, Остатки 140, Свободные 70"},
				},
				view.CalculationResultsFake.DataSource);
		}

		[Test]
		public void ShowInvestments() {
			budget.Weeks = new[] { new BudgetWeek(2.02.of2009()) };
			budget.Investments = new[] { new CashStatement(2.02.of2009(), 20, "Salary") };
			budget.SetFreeMoney(2.02.of2009(), 10);
			//

			Run();

			CollectionAssert.AreEqual(
			new[] {
					new PEBudgetRow {Date = "Февраль"},
					new PEBudgetRow {Date = "Неделя 02.02.2009 - 08.02.2009", Amount = "Свободные 10"},
					new PEBudgetRow {Event = "Salary", Date = "02.02.2009", Amount = "20"},
				},
			view.CalculationResultsFake.DataSource);
		}

		[Test]
		public void ShowPurchases() {
			budget.Weeks = new[] { new BudgetWeek(2.02.of2009()) };
			budget.Expenses = new[] { new CashStatement(2.02.of2009(), 20, "Salary Celebration") };
			budget.SetFreeMoney(2.02.of2009(), 10);
			//

			Run();

			CollectionAssert.AreEqual(
			new[] {
					new PEBudgetRow {Date = "Февраль"},
					new PEBudgetRow {Date = "Неделя 02.02.2009 - 08.02.2009", Amount = "Свободные 10"},
					new PEBudgetRow {Event = "Salary Celebration", Date = "02.02.2009", Amount = "20"},
				},
			view.CalculationResultsFake.DataSource);
		}

		[Test]
		public void ShowSeveralTransfersOnSameDay() {
			budget.Weeks = new[] { new BudgetWeek(2.02.of2009()) };
			budget.Investments = new[] { new CashStatement(2.02.of2009(), 200, "Salary") };
			budget.Expenses = new[] { new CashStatement(2.02.of2009(), 20, "Salary Celebration") };
			budget.SetFreeMoney(2.02.of2009(), 10);
			//

			Run();

			CollectionAssert.AreEqual(
			new[] {
					new PEBudgetRow {Date = "Февраль"},
					new PEBudgetRow {Date = "Неделя 02.02.2009 - 08.02.2009", Amount = "Свободные 10"},
					new PEBudgetRow {Event = "Salary", Date = "02.02.2009", Amount = "200"},
					new PEBudgetRow {Event = "Salary Celebration", Date = "02.02.2009", Amount = "20"},
				},
			view.CalculationResultsFake.DataSource);
		}

		[Test]
		public void ShowMonthlyExpenses() {
			ArrangeWeeks(02.02.of2009());
			budget.MonthlyCashMovements = new[] { new MonthlyCashStatement(new MonthlyCashStatementCategory(1, 1, "Credit"), month(2), 05.02.of2009(), 20, "") };
			budget.SetFreeMoney(5.02.of2009(), 10);
			//

			Run();

			CollectionAssert.AreEqual(
			new[] {
					new PEBudgetRow {Date = "Февраль"},
					new PEBudgetRow {Date = "Неделя 02.02.2009 - 08.02.2009", Amount = "Свободные 0"},
					new PEBudgetRow {Event = "Credit", Date = "05.02.2009", Amount = "20"},
				},
			view.CalculationResultsFake.DataSource);
		}

		[Test]
		public void MonthlyExpensesShowsDescriptionIfExists() {
			ArrangeWeeks(02.02.of2009());
			budget.MonthlyCashMovements = new[] { new MonthlyCashStatement(new MonthlyCashStatementCategory(1, 1, "Credit"), month(2), 05.02.of2009(), 20, "interest only") };
			budget.SetFreeMoney(5.02.of2009(), 10);
			//

			Run();

			CollectionAssert.AreEqual(
			new[] {
					new PEBudgetRow {Date = "Февраль"},
					new PEBudgetRow {Date = "Неделя 02.02.2009 - 08.02.2009", Amount = "Свободные 0"},
					new PEBudgetRow {Event = "Credit : interest only", Date = "05.02.2009", Amount = "20"},
				},
			view.CalculationResultsFake.DataSource);
		}

		[Test]
		public void DeleteInvestment() {
			ArrangeWeeks(02.02.of2009());
			var transfer1 = new CashStatement(03.02.of2009(), 10, "1");
			var transfer2 = new CashStatement(03.02.of2009(), 20, "2");
			budget.Investments = new[] { transfer1, transfer2 };

			ExpectDeletion(x => x.DeleteCashMovement(transfer1));
			//

			Run();

			view.CalculationResultsFake.DataSource.ElementAt(2).Delete();
		}

		[Test]
		public void DeleteRegularExpense() {
			ArrangeWeeks(02.02.of2009());
			var transfer1 = new CashStatement(03.02.of2009(), 10, "1");
			var transfer2 = new CashStatement(03.02.of2009(), 20, "2");
			budget.Expenses = new[] { transfer1, transfer2 };

			ExpectDeletion(x => x.DeleteCashMovement(transfer1));
			//

			Run();

			view.CalculationResultsFake.DataSource.ElementAt(2).Delete();
		}

		[Test]
		public void DeleteMonthlyExpenses() {
			ArrangeWeeks(02.02.of2009());
			var expenseItem = new MonthlyCashStatementCategory(1, 1, "Gaz");
			var expense1 = new MonthlyCashStatement(expenseItem, month(2), 03.02.of2009(), 10, "1");
			var expense2 = new MonthlyCashStatement(expenseItem, month(2), 03.02.of2009(), 20, "2");
			budget.MonthlyCashMovements = new[] { expense1, expense2 };

			ExpectDeletion(x => x.DeleteMonthlyCashMovement(expense1));
			//

			Run();

			view.CalculationResultsFake.DataSource.ElementAt(2).Delete();
		}

		[Test]
		public void DeleteRemainder() {
			ArrangeWeeks(02.02.of2009());
			var transfer1 = new CashStatement(03.02.of2009(), 10, "1");
			var transfer2 = new CashStatement(04.02.of2009(), 20, "2");
			budget.Remainders = new[] { transfer1, transfer2 };

			ExpectDeletion(x => x.DeleteRemainder(transfer1));
			//

			Run();

			view.CalculationResultsFake.DataSource.ElementAt(2).Delete();
		}

		[Test]
		public void DeletingMonthDoesNothing() {
			ArrangeWeeks(02.02.of2009());
			//

			Run();

			view.CalculationResultsFake.DataSource.ElementAt(1).Delete();
		}

		[Test]
		public void DeletingWeekDoesNothing() {
			ArrangeWeeks(02.02.of2009());
			//

			Run();

			view.CalculationResultsFake.DataSource.ElementAt(1).Delete();
		}
	}
}
