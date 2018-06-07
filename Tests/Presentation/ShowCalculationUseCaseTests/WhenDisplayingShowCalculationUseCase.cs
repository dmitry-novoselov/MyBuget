using System.Linq;
using Budget;
using Budget.Domain;
using Budget.Presentation;
using NUnit.Framework;

namespace Tests.Presentation.ShowCalculationUseCaseTests {
	[TestFixture, SetCulture("ru-RU")]
	public class WhenDisplayingShowCalculationUseCase : ShowCalculationUseCaseTestsBase {
		[SetUp]
		public void SetUp() {
			MakeUseCaseRunnable();
		}

		[Test]
		public void ShouldPopulateGridWithColumns() {
			ArrangeWeeks(02.02.of2009());
			//

			Run();

			CollectionAssert.AreEqual(
			                          new[] {"Event", "Date", "Amount"},
			                          view.CalculationResultsFake.Columns.Convert(c => c.Key).ToList());

			CollectionAssert.AreEqual(
			                          new[] {"Событие", "Дата", "Сумма / Остаток"},
			                          view.CalculationResultsFake.Columns.Convert(c => c.Value).ToList());
		}

		[Test]
		public void ShouldSelectLastRemainderFound() {
			ArrangeWeeks(02.02.of2009());
			budget.Remainders = new[] {new CashStatement(03.02.of2009(), 10), new CashStatement(04.02.of2009(), 20)};
			//

			Run();

			Assert.AreEqual(
							new PEBudgetRow { Event = "Остаток", Date = "04.02.2009", Amount = "Конверт ?, Остатки 20, Свободные 0" },
			                view.CalculationResults.SelectedItem);
		}

		[Test]
		public void ShouldSelectLastRemainderOnlyIfGridHasNotBeenScrolled() {
			ArrangeWeeks(02.02.of2009());
			budget.Remainders = new[] {new CashStatement(03.02.of2009(), 10)};

			Run();

			budget.Remainders = new[] {new CashStatement(04.02.of2009(), 20)};
			//

			view.CalculationResultsFake.IsScrolledDown = true;
			Run();

			Assert.AreEqual(
							new PEBudgetRow { Event = "Остаток", Date = "03.02.2009", Amount = "Конверт ?, Остатки 10, Свободные 0" },
			                view.CalculationResults.SelectedItem);
		}

		[Test]
		public void ShouldPlaceRemainderAsTheLastItemInDay() {
			ArrangeWeeks(02.02.of2009());
			budget.Expenses = new[] {new CashStatement(03.02.of2009(), 10, "expense")};
			budget.Investments = new[] {new CashStatement(03.02.of2009(), 10, "investment")};
			budget.MonthlyCashMovements = new[] {new MonthlyCashStatement(new MonthlyCashStatementCategory(1, 1, "monthly"), month(2), 03.02.of2009(), 10, "monthly expense")};
			budget.Remainders = new[] {new CashStatement(03.02.of2009(), 10)};
			//

			Run();

			Assert.AreEqual(
							new PEBudgetRow { Event = "Остаток", Date = "03.02.2009", Amount = "Конверт 0, Остатки 10, Свободные 0" },
			                view.CalculationResults.DataSource.Last());
		}

		[Test]
		public void HighlightCurrentWeek() {
			ArrangeWeeks(02.02.of2009(), 09.02.of2009(), 16.02.of2009());
			ArrangeCashMovement("2", 2.02.of2009(), 20);
			ArrangeReminders(8.02.of2009(), 10);
			ArrangeCashMovement("9", 9.02.of2009(), 20);
			ArrangeReminders(15.02.of2009(), 10);
			ArrangeCashMovement("16", 16.02.of2009(), 20);
			
			DateTimeService.Now = () => 16.02.of2009();
			//

			Run();

			var expected = new[] {
				new PEBudgetRow {Date = "Февраль"},
				new PEBudgetRow {Date = "Неделя 02.02.2009 - 08.02.2009", Amount = "Свободные 0"},
				new PEBudgetRow {Event = "2", Date = "02.02.2009", Amount = "20"},
				new PEBudgetRow {Event = "Остаток", Date = "08.02.2009", Amount = "Конверт 0, Остатки 10, Свободные 0"},
				new PEBudgetRow {Date = "Неделя 09.02.2009 - 15.02.2009", Amount = "Свободные 0", BackgroundColor = PEBudgetRow.Default},
				new PEBudgetRow {Event = "9", Date = "09.02.2009", Amount = "20", BackgroundColor = PEBudgetRow.CurrentWeekIncome},
				new PEBudgetRow {Event = "Остаток", Date = "15.02.2009", Amount = "Конверт 0, Остатки 10, Свободные 0", BackgroundColor = PEBudgetRow.Default},
				new PEBudgetRow {Date = "Неделя 16.02.2009 - 22.02.2009", Amount = "Свободные 0"},
				new PEBudgetRow {Event = "16", Date = "16.02.2009", Amount = "20"}
			};
			CollectionAssert.AreEqual(expected, view.CalculationResultsFake.DataSource);
		}

		[Test]
		public void SortOrder_MonthlyIncomes_Incomes_MonthlyOutcomes_Outcomes() {
			ArrangeWeeks(02.02.of2009());
			ArrangeMonthlyMovement("monthly income", month(2), 2.02.of2009(), 11);
			ArrangeMonthlyMovement("monthly outcom", month(2), 2.02.of2009(), -1);
			ArrangeCashMovement("income", 2.02.of2009(), 11);
			ArrangeCashMovement("outcom", 2.02.of2009(), -1);
			ArrangeReminders(03.02.of2009(), 10);

			DateTimeService.Now = () => 03.02.of2009();
			//

			Run();

			var expected = new[] {
				new PEBudgetRow {Date = "Февраль"},
				new PEBudgetRow {Date = "Неделя 02.02.2009 - 08.02.2009", Amount = "Свободные 0"},
				new PEBudgetRow {Event = "monthly income", Date = "02.02.2009", Amount = "11", BackgroundColor = PEBudgetRow.CurrentWeekMonthlyIncome},
				new PEBudgetRow {Event = "income", Date = "02.02.2009", Amount = "11", BackgroundColor = PEBudgetRow.CurrentWeekIncome},
				new PEBudgetRow {Event = "monthly outcom", Date = "02.02.2009", Amount = "-1", BackgroundColor = PEBudgetRow.CurrentWeekMonthlyOutcome},
				new PEBudgetRow {Event = "outcom", Date = "02.02.2009", Amount = "-1", BackgroundColor = PEBudgetRow.CurrentWeekOutcome},
				new PEBudgetRow {Event = "Остаток", Date = "03.02.2009", Amount = "Конверт 0, Остатки 10, Свободные 0"},
			};
			CollectionAssert.AreEqual(expected, view.CalculationResultsFake.DataSource);
		}
	}
}