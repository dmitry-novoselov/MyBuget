using System;
using System.Collections.Generic;
using System.Linq;
using Budget.Domain;
using Budget.Infrastructure;
using NUnit.Framework;
using Tests.Dsl;
using Tests.Fakes;

namespace Tests.Domain {
	[TestFixture]
	public class CalculationDataPreprocessorTests : FixtureBase {
		private CalculationDataProvider dataProvider;

		[SetUp]
		public void SetUp() {
			dataProvider = new CalculationDataProvider(new PersistentStorageFake());
			dataProvider.CalculationPeriod = new Period(DateTime.MinValue, DateTime.MinValue);
		}

		private CalculationDataPreprocessor CreatePreprocessor() {
			return new CalculationDataPreprocessor(dataProvider);
		}

		[Test]
		public void CashMovementsAreCopied() {
			dataProvider.AddCashMovement(1.02.of2009(), 10, "expense");
			//

			var preprocessor = CreatePreprocessor();

			CollectionAssert.AreEquivalent(dataProvider.GetCashMovements(), preprocessor.CashMovements);
		}

		[Test]
		public void RemaindersAreCopied() {
			dataProvider.SetRemainder(1.02.of2009(), 10);
			//

			var preprocessor = CreatePreprocessor();

			CollectionAssert.AreEquivalent(dataProvider.GetRemainders(), preprocessor.Remainders);
		}

		[Test]
		public void EnvelopeSizeIsCopied() {
			dataProvider.EnvelopeSize = 10;
			//

			var preprocessor = CreatePreprocessor();
			dataProvider.EnvelopeSize = 20;

			Assert.AreEqual(10, preprocessor.EnvelopeSize);
		}

		[Test]
		public void CalculationPeriodIsCopied() {
			dataProvider.CalculationPeriod = 1.01.of2009() - 1.02.of2009();
			//

			var preprocessor = CreatePreprocessor();
			dataProvider.CalculationPeriod = 1.03.of2009() - 1.04.of2009();

			Assert.AreEqual(1.01.of2009() - 1.02.of2009(), preprocessor.CalculationPeriod);
		}

		[Test]
		public void MonthlyExpensesAreCopied() {
			dataProvider.CalculationPeriod = 01.01.of2009() - 30.01.of2009();
			dataProvider.AddExpenseItem(1, 1, "Gaz");
			dataProvider.AddMonthlyCashStatement(dataProvider.GetMonthlyCashStatementCategories().Single(), new YearMonth(1, 2009), 05.01.of2009(), 5, "");
			//

			var preprocessor = CreatePreprocessor();

			CollectionAssert.AreEquivalent(
				dataProvider.GetMonthlyCashMovements(),
				preprocessor.MonthlyCashMovements.ToList());
		}

		[Test]
		public void ExpensesItemsAreConvertedToExpense() {
			dataProvider.CalculationPeriod = 01.01.of2009() - 15.02.of2009();
			dataProvider.AddExpenseItem(1, 1, "Gaz");
			dataProvider.AddExpenseItem(16, 2, "Internet");

			var gaz = dataProvider.GetMonthlyCashStatementCategories()[0];
			var internet = dataProvider.GetMonthlyCashStatementCategories()[1];
			//

			var preprocessor = CreatePreprocessor();

			CollectionAssert.AreEquivalent(
				new[] {
					new MonthlyCashStatement(gaz, new YearMonth(1, 2009), 01.01.of2009(), 1, "<план>"),
					new MonthlyCashStatement(gaz, new YearMonth(2, 2009), 01.02.of2009(), 1, "<план>"),
					new MonthlyCashStatement(internet, new YearMonth(1, 2009), 16.01.of2009(), 2, "<план>"),
				},
				preprocessor.MonthlyCashMovements.ToList());
		}

		[Test]
		public void ExpensesItemsAreBoundedByTheirEffectivePeriodWhenConvertedToExpense() {
			dataProvider.CalculationPeriod = 01.01.of2009() - 1.03.of2009();
			
			dataProvider.AddExpenseItem(5, 1, "Gaz");
			var gaz = dataProvider.GetMonthlyCashStatementCategories()[0];
			gaz.Effective = 01.01.of2009() - 1.02.of2009();
			//

			var preprocessor = CreatePreprocessor();

			CollectionAssert.AreEquivalent(
				new[] { new MonthlyCashStatement(gaz, new YearMonth(1, 2009), 05.01.of2009(), 1, "<план>") },
				preprocessor.MonthlyCashMovements.ToList());
		}

		[Test]
		public void MonthlyExpeseUsesSourceMonth() {
			dataProvider.CalculationPeriod = 01.01.of2009() - 28.02.of2009();
			dataProvider.AddExpenseItem(5, 10, "Gaz");
			var gaz = dataProvider.GetMonthlyCashStatementCategories()[0];
			dataProvider.AddMonthlyCashStatement(gaz, new YearMonth(1, 2009), 10.01.of2009(), 30, "");
			//

			var preprocessor = CreatePreprocessor();

			CollectionAssert.AreEquivalent(
                new[] { new MonthlyCashStatement(gaz, month(1), 10.01.of2009(), 30, ""), new MonthlyCashStatement(gaz, month(2), 05.02.of2009(), 10, "<план>") },
				preprocessor.MonthlyCashMovements.ToList());
		}
	}
}
