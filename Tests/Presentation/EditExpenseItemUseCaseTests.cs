#region Usings

using Budget.Infrastructure;
using Budget.Presentation.EditExpenseItemUseCase;
using Budget.Presentation.ShowCalculationUseCase;
using Moq;
using NUnit.Framework;
using StructureMap;
using Tests.Fakes;
using Tests.Presentation.Fakes;
using Budget.Domain;
using System;

#endregion

namespace Tests.Presentation {
	[TestFixture]
	public class EditExpenseItemUseCaseTests : FixtureBase {
		private Mock<IShowCalculationUseCase> showCalculationUseCaseMock;
		private Mock<IDataSavingService> dataSavingServiceMock;

		private EditExpenseItemViewFake view;
		private CalculationDataProvider dataProvider;

		private EditExpenseItemUseCase useCase;

		[SetUp]
		public void SetUp() {
			showCalculationUseCaseMock = new Mock<IShowCalculationUseCase>();
			dataSavingServiceMock = new Mock<IDataSavingService>();

			ObjectFactory.Initialize(x => {
				x.For<IShowCalculationUseCase>().Use(showCalculationUseCaseMock.Object);
			});

			view = new EditExpenseItemViewFake();
			dataProvider = new CalculationDataProvider(new PersistentStorageFake());
		}

		private void Run(MonthlyCashStatementCategory expenseItem) {
			useCase = new EditExpenseItemUseCase(dataSavingServiceMock.Object, view);
			useCase.Run(expenseItem);
		}

		private MonthlyCashStatementCategory CreateExpenseItem() {
			return CreateExpense(1, 1, "1", 1.01.of2009() - 1.02.of2009());
		}

		private MonthlyCashStatementCategory CreateExpense(int dayOfMonth, int amount, string name, Period effective) {
			return new MonthlyCashStatementCategory(dayOfMonth, amount, name) { Effective = effective };
		}

		[Test]
		public void ShouldShowView() {
			Run(CreateExpenseItem());

			Assert.IsTrue(view.IsShown);
		}

		[Test]
		public void ShouldDisplayExpenseItemData() {
			var expense = CreateExpense(1, 2, "3", 4.05.of2009() - 6.07.of2010());
			Run(expense);

			AreEqual(1, view.MonthlyExpense.DayOfMonth);
			AreEqual(2, view.MonthlyExpense.Amount);
			AreEqual("3", view.MonthlyExpense.Name);
			AreEqual(4.05.of2009(), view.MonthlyExpense.From);
			AreEqual(5.07.of2010(), view.MonthlyExpense.To);
		}

		[Test]
		public void ShouldEditExpenseItemData() {
			var expenseItem = CreateExpense(1, 2, "3", 4.05.of2009() - 6.07.of2010());
			Run(expenseItem);

			view.MonthlyExpense.DayOfMonth = 9;
			view.MonthlyExpense.Amount = 8;
			view.MonthlyExpense.Name = "7";
			view.MonthlyExpense.From = 6.05.of2009();
			view.MonthlyExpense.To = 4.03.of2010();
			view.OnOK();

			AreEqual(9, expenseItem.DayOfMonth);
			AreEqual(8, expenseItem.Amount);
			AreEqual("7", expenseItem.Name);
			AreEqual(6.05.of2009() - 5.03.of2010(), expenseItem.Effective);
		}

		[Test]
		public void ShouldSetViewCaption() {
			Run(CreateExpenseItem());

			Assert.AreEqual("Править статью расходов", view.Text);
		}

		[Test]
		public void ShouldRefreshCalculationsAfterFinish() {
			Run(CreateExpenseItem());

			view.OnOK();

			showCalculationUseCaseMock.Verify(x => x.Run(), Times.Exactly(1));
		}

		[Test]
		public void ShouldSaveDataWhenOkClicked() {
			Run(CreateExpenseItem());

			view.OnOK();

			dataSavingServiceMock.Verify(x => x.Save(), Times.Exactly(1));
		}
	}
}
