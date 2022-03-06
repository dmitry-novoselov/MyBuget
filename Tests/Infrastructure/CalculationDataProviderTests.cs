using System.Linq;
using Budget.Infrastructure;
using Moq;
using NUnit.Framework;
using Budget.Domain;
using Tests.Fakes;

namespace Tests.Infrastructure {
	[TestFixture]
	public class CalculationDataProviderTests : FixtureBase {
		private Mock<IMemento> mementoMock;
		private CalculationDataProvider dataProvider;
		private DataContainer2 dataContainer;

		[SetUp]
		public void SetUp() {
			dataContainer = new DataContainer2();

			mementoMock = new Mock<IMemento>(MockBehavior.Loose);
			mementoMock
				.Setup(m => m.Get(It.IsAny<DataContainer2>()))
				.Returns(dataContainer);

			dataProvider = new CalculationDataProvider(mementoMock.Object, new PersistentStorageFake());
		}

		[Test]
		public void UpdateRemainderWhenSetOnSameDate() {
			dataProvider.SetRemainder(1.01.of2009(), 10);
			dataProvider.SetRemainder(1.01.of2009(), 20);

			mementoMock.Verify(x => x.Set(dataContainer), Times.Exactly(2));
			AreEqual(1, dataProvider.GetRemainders().Count);
			AreEqual(1.01.of2009(), dataProvider.GetRemainders()[0].Date);
			AreEqual(20, dataProvider.GetRemainders()[0].Amount);
		}

		[Test]
		public void AddExpenseItem() {
			dataProvider.AddExpenseItem(1, 10, "Gaz");

			mementoMock.Verify(x => x.Set(dataContainer), Times.Exactly(1));
			CollectionAssert.AreEqual(
				new[] { new MonthlyCashStatementCategory(1, 10, "Gaz") },
				dataProvider.GetMonthlyCashStatementCategories());
		}

		[Test]
		public void AddExpense() {
			dataProvider.AddExpenseItem(1, 10, "Gaz");
			var gaz = dataProvider.GetMonthlyCashStatementCategories()[0];
			dataProvider.AddMonthlyCashStatement(gaz, month(1), 01.01.of2009(), 20, "tourne");

			mementoMock.Verify(x => x.Set(dataContainer), Times.Exactly(2));
			CollectionAssert.AreEqual(
				new[] { new MonthlyCashStatement(gaz, month(1), 01.01.of2009(), 20, "tourne") },
			    dataProvider.GetMonthlyCashMovements());
		}

		[Test]
		public void DeleteInvestment() {
			dataProvider.AddCashMovement(01.01.of2009(), 1, "1");
			dataProvider.AddCashMovement(01.01.of2009(), 2, "2");

			var investment1 = dataProvider.GetCashMovements().Single(x => x.Amount == 1);
			var investment2 = dataProvider.GetCashMovements().Single(x => x.Amount == 2);

			dataProvider.DeleteCashMovement(investment1);

			mementoMock.Verify(x => x.Set(dataContainer), Times.Exactly(3));
			CollectionAssert.AreEqual(
				new[] { investment2 },
				dataProvider.GetCashMovements());
		}

		[Test]
		public void AddMonthlyExpense() {
			dataProvider.AddExpenseItem(1, 10, "Gaz");
			var gaz = dataProvider.GetMonthlyCashStatementCategories()[0];
			dataProvider.AddMonthlyCashStatement(gaz, month(1), 01.01.of2009(), 20, "tourne");
			dataProvider.AddMonthlyCashStatement(gaz, month(1), 01.01.of2009(), 20, "tourne2");

			dataProvider.DeleteMonthlyCashMovement(dataProvider.GetMonthlyCashMovements()[0]);

			mementoMock.Verify(x => x.Set(dataContainer), Times.Exactly(4));
			CollectionAssert.AreEqual(
				new[] { new MonthlyCashStatement(gaz, month(1), 01.01.of2009(), 20, "tourne2") },
				dataProvider.GetMonthlyCashMovements());
		}

		[Test]
		public void DeleteRemainder() {
			dataProvider.SetRemainder(01.01.of2009(), 1);
			dataProvider.SetRemainder(02.01.of2009(), 2);

			var remainder1 = dataProvider.GetRemainders().Single(x => x.Amount == 1);
			var remainder2 = dataProvider.GetRemainders().Single(x => x.Amount == 2);

			dataProvider.DeleteRemainder(remainder1);

			mementoMock.Verify(x => x.Set(dataContainer), Times.Exactly(3));
			CollectionAssert.AreEqual(
				new[] { remainder2 },
				dataProvider.GetRemainders());
		}

		[Test]
		public void SetWalletRemainders() {
			dataProvider.WalletRemainders = "100";

			mementoMock.Verify(x => x.Set(dataContainer), Times.Exactly(1));
		}
	}
}
