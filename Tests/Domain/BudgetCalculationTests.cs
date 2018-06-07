using System;
using System.Linq;
using Budget;
using Budget.Domain;
using Budget.Infrastructure;
using NUnit.Framework;
using Tests.Dsl;
using Tests.Fakes;

namespace Tests.Domain {
	[TestFixture]
	public class BudgetCalculationTests : FixtureBase {
		private ICalculationDataProvider dataProvider;

		[SetUp]
		public void SetUp() {
			dataProvider = new CalculationDataProvider(new PersistentStorageFake());
		}

		private int EnvelopeSize {
			get { return dataProvider.EnvelopeSize;  }
			set { dataProvider.EnvelopeSize = value; }
		}

		private void SetCalculatedPeriod(Period period) {
			dataProvider.CalculationPeriod = period;
		}

		private void SetRemainder(DateTime date, int amount) {
			dataProvider.SetRemainder(date, amount);
		}

		private void AddInvestment(DateTime date, int amount) {
			dataProvider.AddCashMovement(date, amount, "");
		}

        private MonthlyCashStatementCategory AddMonthlyExpense(int dayOfMonth, int amount, string name)
        {
            dataProvider.AddExpenseItem(dayOfMonth, -amount, name);
            return dataProvider.GetMonthlyCashStatementCategories().Single(_ => _.Name == name);
        }

        private void AddMonthlyExpenseInstance(DateTime date, int amount, MonthlyCashStatementCategory category, bool isFinal)
        {
            dataProvider.AddMonthlyCashStatement(category, new YearMonth(date.Month, date.Year), date, amount, "", isFinal);
        }

		private void AddPurchase(DateTime date, int amount) {
			AddPurchase(date, amount, "");
		}

		private void AddPurchase(DateTime date, int amount, string description) {
			dataProvider.AddCashMovement(date, -amount, description);
		}

		private Budget.Domain.Budget CalculateBudget() {
			return new BudgetCalculation(new CalculationDataPreprocessor(dataProvider)).CalculateBudget();
		}

		[Test]
		public void WeekRemainderEqualsEnvelopeSize() {
			EnvelopeSize = 70;
			SetRemainder(1.02.of2009(), 140);
			SetCalculatedPeriod(2.02.of2009() - 16.02.of2009());
			//

			var budget = CalculateBudget();

			var weeks = budget.Weeks.ToList();
			Assert.AreEqual(2, weeks.Count);
			Assert.AreEqual(70, weeks[0].Remainder);
			Assert.AreEqual(70, weeks[1].Remainder);
		}

		[Test]
		public void PureCalculatedTotalRemainder() {
			EnvelopeSize = 70;
			SetRemainder(1.02.of2009(), 140);
			SetCalculatedPeriod(2.02.of2009() - 16.02.of2009());
			//

			var budget = CalculateBudget();

			Assert.AreEqual(2, budget.Weeks.Count());
			Assert.AreEqual(70, budget.GetRemainder(8.02.of2009()));
			Assert.AreEqual(0, budget.GetRemainder(15.02.of2009()));
		}

		[Test]
		public void WeekRemainderIsAdjustedDueToRemainder() {
			EnvelopeSize = 70;
			SetRemainder(1.02.of2009(), 140);
			SetCalculatedPeriod(2.02.of2009() - 16.02.of2009());

			SetRemainder(3.02.of2009(), 85);
			//

			var budget = CalculateBudget();

			var weeks = budget.Weeks.ToList();
			Assert.AreEqual(15, weeks[0].Remainder);
			Assert.AreEqual(85, budget.GetRemainder(3.02.of2009()));
			Assert.AreEqual(70, budget.GetRemainder(8.02.of2009()));

			Assert.AreEqual(70, weeks[1].Remainder);
			Assert.AreEqual(0, budget.GetRemainder(15.02.of2009()));
		}

		[Test]
		public void WeekRemainderCalculationHandlesPurchases() {
			EnvelopeSize = 70;
			SetRemainder(1.02.of2009(), 140);
			SetCalculatedPeriod(2.02.of2009() - 16.02.of2009());

			AddPurchase(4.02.of2009(), 50);
			SetRemainder(4.02.of2009(), 80);
			//

			var budget = CalculateBudget();

			var weeks = budget.Weeks.ToList();
			Assert.AreEqual(60, weeks[0].Remainder);
			Assert.AreEqual(70, weeks[1].Remainder);
		}

		[Test]
		public void WeekRemainderCalculationHandlesMonthlyExpenses() {
			EnvelopeSize = 70;
			SetRemainder(1.02.of2009(), 140);
			SetCalculatedPeriod(2.02.of2009() - 16.02.of2009());

			AddMonthlyExpense(4, 50, "Internet");
			SetRemainder(4.02.of2009(), 80);
			//

			var budget = CalculateBudget();

			var weeks = budget.Weeks.ToList();
			Assert.AreEqual(60, weeks[0].Remainder);
			Assert.AreEqual(70, weeks[1].Remainder);
		}

		[Test]
		public void WeekRemainderCalculationHandlesInvestments() {
			EnvelopeSize = 70;
			SetRemainder(1.02.of2009(), 140);
			SetCalculatedPeriod(2.02.of2009() - 16.02.of2009());

			AddInvestment(4.02.of2009(), 50);
			SetRemainder(4.02.of2009(), 180);
			//

			var budget = CalculateBudget();

			var weeks = budget.Weeks.ToList();
			Assert.AreEqual(60, weeks[0].Remainder);
			Assert.AreEqual(70, weeks[1].Remainder);
		}

		[Test]
		public void WeekRemainderCalculationUsesLastRemainder() {
			EnvelopeSize = 70;
			SetRemainder(1.02.of2009(), 140);
			SetCalculatedPeriod(2.02.of2009() - 16.02.of2009());

			SetRemainder(2.02.of2009(), 140);
			SetRemainder(3.02.of2009(), 170); // have found some money in one of the pockets, for example
			//

			var budget = CalculateBudget();

			Assert.AreEqual(140, budget.GetRemainder(2.02.of2009()));
			Assert.AreEqual(170, budget.GetRemainder(3.02.of2009()));
			Assert.AreEqual(70, budget.GetRemainder(8.02.of2009()));

			var weeks = budget.Weeks.ToList();
			Assert.AreEqual(100, weeks[0].Remainder);
			Assert.AreEqual(20, weeks[0].DayEnvelopeSize);
		}

		[Test]
		public void LastDayRemainderMakeDayEnvelopeSizeEqualsWeekRemainder() {
			EnvelopeSize = 70;
			SetRemainder(1.02.of2009(), 140);
			SetCalculatedPeriod(2.02.of2009() - 16.02.of2009());

			SetRemainder(8.02.of2009(), 100);
			//

			var budget = CalculateBudget();

			var weeks = budget.Weeks.ToList();
			Assert.AreEqual(30, weeks[0].Remainder);
			Assert.AreEqual(30, weeks[0].DayEnvelopeSize);
		}

		[Test]
		public void InvestmentCoversFollowedWeeks_Remainders() {
			EnvelopeSize = 70;
			SetRemainder(1.02.of2009(), 140);
			SetCalculatedPeriod(2.02.of2009() - 23.02.of2009());

			AddInvestment(10.02.of2009(), 100);
			//

			var budget = CalculateBudget();

			Assert.AreEqual(3, budget.Weeks.Count());
			Assert.AreEqual(130, budget.GetRemainder(2.02.of2009()));
			Assert.AreEqual(70, budget.GetRemainder(8.02.of2009()));
			Assert.AreEqual(150, budget.GetRemainder(10.02.of2009()));
			Assert.AreEqual(100, budget.GetRemainder(15.02.of2009()));
			Assert.AreEqual(90, budget.GetRemainder(16.02.of2009()));
			Assert.AreEqual(30, budget.GetRemainder(22.02.of2009()));
		}

		[Test]
		public void TwoInvestmentsOnOneDayAreAggregated() {
			EnvelopeSize = 70;
			SetRemainder(1.02.of2009(), 140);
			SetCalculatedPeriod(2.02.of2009() - 16.02.of2009());

			AddInvestment(3.02.of2009(), 100);
			AddInvestment(3.02.of2009(), 100);
			//

			var budget = CalculateBudget();

			Assert.AreEqual(320, budget.GetRemainder(3.02.of2009()));
			Assert.AreEqual(270, budget.GetRemainder(8.02.of2009()));
		}

		[Test]
		public void MultipleInvestments_RemaindersAccumulateAllPreviousInvestments() {
			EnvelopeSize = 70;
			SetRemainder(1.02.of2009(), 140);
			SetCalculatedPeriod(2.02.of2009() - 23.02.of2009());

			AddInvestment(10.02.of2009(), 50);
			AddInvestment(11.02.of2009(), 50);
			AddInvestment(12.02.of2009(), 50);
			//

			var budget = CalculateBudget();

			Assert.AreEqual(100, budget.GetRemainder(10.02.of2009()));
			Assert.AreEqual(140, budget.GetRemainder(11.02.of2009()));
			Assert.AreEqual(180, budget.GetRemainder(12.02.of2009()));
		}

		[Test]
		public void MonthlyExpensesCalculatedInRemainders() {
			EnvelopeSize = 70;
			SetRemainder(1.02.of2009(), 140);
			SetCalculatedPeriod(2.02.of2009() - 16.02.of2009());

			AddMonthlyExpense(13, 100, "Свет");
			AddMonthlyExpense(13, 200, "Газ");
			AddMonthlyExpense(14, 350, "Телефон");

			AddInvestment(12.02.of2009(), 600);
			//

			var budget = CalculateBudget();

			Assert.AreEqual(70, budget.GetRemainder(8.02.of2009()));
			Assert.AreEqual(630, budget.GetRemainder(12.02.of2009()));
			Assert.AreEqual(320, budget.GetRemainder(13.02.of2009()));
			Assert.AreEqual(-40, budget.GetRemainder(14.02.of2009()));
			Assert.AreEqual(-50, budget.GetRemainder(15.02.of2009()));
		}

		[Test]
		public void MonthlyExpensesOfVeryEndOfMonthAreCalculated() {
			EnvelopeSize = 70;
			SetRemainder(1.02.of2009(), 140);
			SetCalculatedPeriod(2.02.of2009() - 2.03.of2009());

			AddMonthlyExpense(28, 100, "Gaz");
			//

			var budget = CalculateBudget();

			Assert.AreEqual(-240, budget.GetRemainder(1.03.of2009()));
		}

		[Test]
		public void MonthlyExpenseIsShiftedOnTheEndOfMonthIfItsShorterThenDateOfExpense() {
			EnvelopeSize = 70;
			SetRemainder(1.02.of2009(), 140);
			SetCalculatedPeriod(2.02.of2009() - 2.03.of2009());

			AddMonthlyExpense(31, 100, "Gaz");
			//

			var budget = CalculateBudget();

			Assert.AreEqual(-240, budget.GetRemainder(1.03.of2009()));
		}

		[Test]
		public void FreeMoneyAvailableOnEachDate() {
			EnvelopeSize = 70;
			SetRemainder(1.02.of2009(), 140);
			SetCalculatedPeriod(2.02.of2009() - 16.02.of2009());

			AddInvestment(3.02.of2009(), 50);
			AddInvestment(4.02.of2009(), 50);
			AddInvestment(5.02.of2009(), 50);
			//

			var budget = CalculateBudget();

			Assert.AreEqual(70, budget.GetFreeMoney(1.02.of2009()));
			Assert.AreEqual(70, budget.GetFreeMoney(2.02.of2009()));
			Assert.AreEqual(120, budget.GetFreeMoney(3.02.of2009()));
			Assert.AreEqual(150, budget.GetFreeMoney(4.02.of2009()));
			Assert.AreEqual(150, budget.GetFreeMoney(5.02.of2009()));
			Assert.AreEqual(150, budget.GetFreeMoney(6.02.of2009()));
			Assert.AreEqual(150, budget.GetFreeMoney(15.02.of2009()));
		}

		[Test]
		public void Bug_MonthlyExpensesAreCalculatedOnce() {
			EnvelopeSize = 70;
			SetRemainder(1.02.of2009(), 140);
			SetCalculatedPeriod(2.02.of2009() - 16.02.of2009());

			AddMonthlyExpense(2, 20, "Gaz");
			AddMonthlyExpense(9, 200, "Phone");
			//

			var budget = CalculateBudget();

			Assert.AreEqual(110, budget.GetRemainder(2.02.of2009()));
			Assert.AreEqual(50, budget.GetRemainder(8.02.of2009()));
			Assert.AreEqual(-160, budget.GetRemainder(9.02.of2009()));
			Assert.AreEqual(-220, budget.GetRemainder(15.02.of2009()));
		}

		[Test]
		public void PurchasesAreCalculatedInRemainders() {
			EnvelopeSize = 70;
			SetRemainder(1.02.of2009(), 140);
			SetCalculatedPeriod(2.02.of2009() - 16.02.of2009());

			AddPurchase(2.02.of2009(), 10);
			AddPurchase(2.02.of2009(), 10);
			AddPurchase(3.02.of2009(), 10);
			AddPurchase(9.02.of2009(), 10);
			//

			var budget = CalculateBudget();

			Assert.AreEqual(110, budget.GetRemainder(2.02.of2009()));
			Assert.AreEqual(90, budget.GetRemainder(3.02.of2009()));
			Assert.AreEqual(40, budget.GetRemainder(8.02.of2009()));
			Assert.AreEqual(20, budget.GetRemainder(9.02.of2009()));
			Assert.AreEqual(-40, budget.GetRemainder(15.02.of2009()));
		}

		[Test]
		public void PurchasesReduceFreeMoney() {
			EnvelopeSize = 70;
			SetRemainder(1.02.of2009(), 140);
			SetCalculatedPeriod(2.02.of2009() - 16.02.of2009());

			AddInvestment(6.02.of2009(), 20);
			AddPurchase(9.02.of2009(), 10, "4");
			//

			var budget = CalculateBudget();

			Assert.AreEqual(10, budget.GetFreeMoney(1.02.of2009()));
			Assert.AreEqual(10, budget.GetFreeMoney(6.02.of2009()));
		}

		[Test]
		public void BudgetContainsRemainders() {
			EnvelopeSize = 70;
			SetRemainder(1.02.of2009(), 140);
			SetCalculatedPeriod(2.02.of2009() - 16.02.of2009());

			SetRemainder(1.06.of2009(), 100);
			//

			var budget = CalculateBudget();

			CollectionAssert.AreEquivalent(
				new[] { new CashStatement(1.02.of2009(), 140), new CashStatement(1.06.of2009(), 100) },
				budget.Remainders);
		}

		[Test]
		public void BudgetContainsInvestments() {
			EnvelopeSize = 70;
			SetRemainder(1.02.of2009(), 140);
			SetCalculatedPeriod(2.02.of2009() - 16.02.of2009());

			AddInvestment(2.02.of2009(), 100);
			AddInvestment(2.06.of2009(), 150);
			//

			var budget = CalculateBudget();

			CollectionAssert.AreEquivalent(
				new[] { new CashStatement(2.02.of2009(), 100), new CashStatement(2.06.of2009(), 150) },
				budget.Investments);
		}

		[Test]
		public void BudgetContainsPurchases() {
			EnvelopeSize = 70;
			SetRemainder(1.02.of2009(), 140);
			SetCalculatedPeriod(2.02.of2009() - 16.02.of2009());

			AddPurchase(2.02.of2009(), 100, "Bots");
			AddPurchase(2.06.of2009(), 150);
			//

			var budget = CalculateBudget();

			CollectionAssert.AreEquivalent(
				new[] { new CashStatement(2.02.of2009(), -100, "Bots"), new CashStatement(2.06.of2009(), -150) },
				budget.Expenses);
		}

		[Test]
		public void BudgetContainsMonthlyExpenses() {
			EnvelopeSize = 70;
			SetRemainder(1.02.of2009(), 140);
			SetCalculatedPeriod(2.02.of2009() - 16.02.of2009());

			AddMonthlyExpense(7, 10, "Gaz");
			//

			var budget = CalculateBudget();

			CollectionAssert.AreEquivalent(
				new[] { new MonthlyCashStatement(dataProvider.GetMonthlyCashStatementCategories()[0], new YearMonth(2,2009), 7.02.of2009(), -10, "<план>") },
				budget.MonthlyCashMovements);
		}

		[Test]
		public void LastDayRemainderAdjustFreeMoney() {
			EnvelopeSize = 70;
			SetRemainder(1.02.of2009(), 140);
			SetCalculatedPeriod(2.02.of2009() - 16.02.of2009());

			SetRemainder(8.02.of2009(), 90);
			//

			var budget = CalculateBudget();

			Assert.AreEqual(0, budget.GetFreeMoney(7.02.of2009()));
			Assert.AreEqual(20, budget.GetFreeMoney(8.02.of2009()));
			Assert.AreEqual(20, budget.GetFreeMoney(15.02.of2009()));
		}

		[Test]
		public void ShouldCalculateEmptyBudgetIfNoInitialRemainder() {
			SetCalculatedPeriod(2.02.of2009() - 16.02.of2009());

			AddInvestment(2.06.of2009(), 150);
			AddPurchase(2.06.of2009(), 150);
			AddMonthlyExpense(7, 10, "Gaz");
			SetRemainder(8.02.of2009(), 90);
			//

			var budget = CalculateBudget();

			Assert.AreEqual(0, budget.Expenses.Count());
			Assert.AreEqual(0, budget.Investments.Count());
			Assert.AreEqual(0, budget.MonthlyCashMovements.Count());
			Assert.AreEqual(0, budget.Remainders.Count());
			Assert.AreEqual(0, budget.Weeks.Count());
		}

        [Test]
        public void ShouldCalculateMonthlyBalance()
        {
            EnvelopeSize = 70;
            SetRemainder(1.02.of2009(), 0);
            SetCalculatedPeriod(2.02.of2009() - 16.02.of2009());

            AddMonthlyExpense(5, 100, "gaz");
            AddMonthlyExpense(20, -500, "salary");
            //

            var budget = CalculateBudget();

            Assert.AreEqual(500 - 100 - (70 / 7 * 31), budget.MonthlyBalance);
        }

        [Test]
        public void ShouldCalculateMonthlyExpenseRemainder()
        {
            EnvelopeSize = 70;
            SetRemainder(1.02.of2009(), 500);
            SetCalculatedPeriod(2.02.of2009() - 16.02.of2009());

            var gaz = AddMonthlyExpense(14, 100, "gaz");
            AddMonthlyExpenseInstance(5.02.of2009(), -30, gaz, false);
            //

            var budget = CalculateBudget();

            Assert.AreEqual(500 - 70 - 30, budget.GetRemainder(8.02.of2009()));
            Assert.AreEqual(500 - 70*2 - 100, budget.GetRemainder(15.02.of2009()));
            //
            var monthlyMovements = budget.MonthlyCashMovements.ToList();
            Assert.AreEqual(2, monthlyMovements.Count);

            var plan = monthlyMovements.Single(_ => _.Date == 14.02.of2009());
            Assert.AreEqual(-70, plan.Amount);
        }

        [Test]
        public void ShouldConsiderCalculateMonthlyExpenseRemainderZeroIfItsPaidOut()
        {
            EnvelopeSize = 70;
            SetRemainder(1.02.of2009(), 500);
            SetCalculatedPeriod(2.02.of2009() - 16.02.of2009());

            var gaz = AddMonthlyExpense(14, 100, "gaz");
            AddMonthlyExpenseInstance(5.02.of2009(), -110, gaz, false);
            //

            var budget = CalculateBudget();

            Assert.AreEqual(500 - 70 - 110, budget.GetRemainder(8.02.of2009()));
            Assert.AreEqual(500 - 70 * 2 - 110, budget.GetRemainder(15.02.of2009()));
            //
            var plan = budget.MonthlyCashMovements.Single();
            Assert.AreEqual(-110, plan.Amount);
            Assert.AreEqual((DateTime)5.02.of2009(), plan.Date);
        }
    }
}
