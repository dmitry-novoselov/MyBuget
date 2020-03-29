#region Usings

using System;
using Budget.Domain;
using Budget.Presentation;
using Budget.Presentation.EditRemainderUseCase;
using Budget.Presentation.ShowCalculationUseCase;
using Moq;
using NUnit.Framework;
using StructureMap;
using Tests.Presentation.Fakes;
using Budget.Presentation.EditCashMovementUseCase;

#endregion

namespace Tests.Presentation.EditingTests {
	[TestFixture]
	public class WhenEditCashStatement : FixtureBase {
		private Mock<IShowCalculationUseCase> showCalculationUseCaseMock;
		private Mock<IDataSavingService> dataSavingServiceMock;

		private EditTransferViewFake view;

		[SetUp]
		public void SetUp() {
			showCalculationUseCaseMock = new Mock<IShowCalculationUseCase>();
			dataSavingServiceMock = new Mock<IDataSavingService>();

			ObjectFactory.Initialize(x => {
				x.For<IShowCalculationUseCase>().Use(showCalculationUseCaseMock.Object);
			});

			view = new EditTransferViewFake();
		}

		private void Run() {
			Run(new CashStatement(01.01.of2009(), 0, ""));
		}

		private void Run(CashStatement investment) {
			var useCase = (IEditCashMovementUseCase)new EditCashStatementUseCase(dataSavingServiceMock.Object, view);
			useCase.Run(investment);
		}

		private void The<TUseCase>(Func<TUseCase, Action<CashStatement>> run, int amount) {
			var useCase = (TUseCase)(object)new EditCashStatementUseCase(dataSavingServiceMock.Object, view);
			run(useCase)(new CashStatement(01.01.of2009(), amount, ""));
		}

		[Test]
		public void ShouldSetCaptionForRemainder() {
			The<IEditRemainderUseCase>(_ => _.Run, 1);

			Assert.AreEqual("Изменить остаток", view.Text);
		}

		[Test]
		public void ShouldSetCaptionForInvestment() {
			The<IEditCashMovementUseCase>(_ => _.Run, 1);

			Assert.AreEqual("Изменить доход", view.Text);
		}

		[Test]
		public void ShouldSetCaptionForExpense() {
			The<IEditCashMovementUseCase>(_ => _.Run, -1);

			Assert.AreEqual("Изменить трату", view.Text);
		}

		[Test]
		public void ShouldDisplayDataOfInvestmentBeingEdited() {
			var investment = new CashStatement(01.02.of2009(), 10, "salary");

			Run(investment);

			AreEqual(01.02.of2009(), view.Transfer.Date);
			AreEqual(10, view.Transfer.Amount);
			AreEqual("salary", view.Transfer.Description);
		}

		[Test]
		public void ShouldDisplayDataOfExpenseBeingEdited() {
			var expense = new CashStatement(01.02.of2009(), -10, "lost some money");

			Run(expense);

			AreEqual(10, view.Transfer.Amount);
		}

		[Test]
		public void ShouldShowEditExpenseView() {
			var investment = new CashStatement(01.02.of2009(), 10, "salary");

			Run(investment);

			Assert.IsTrue(view.IsShown);
		}

		[Test]
		public void ShouldReplaceDataOfInvestmentWithNewValuesWhenOkClicked() {
			var investment = new CashStatement(01.02.of2009(), 10, "salary");

			Run(investment);

			view.Transfer.Date = 02.03.of2009();
			view.Transfer.Amount = 20;
			view.Transfer.Description = "bonus";
			view.OnOK();

			AreEqual(02.03.of2009(), investment.Date);
			AreEqual(20, investment.Amount);
			AreEqual("bonus", investment.Description);
		}

		[Test]
		public void ShouldReplaceDataOfExpenseWithNewValuesWhenOkClicked() {
			var expense = new CashStatement(01.02.of2009(), -10, "lost some money");

			Run(expense);

			view.Transfer.Amount = 20;
			view.OnOK();

			AreEqual(-20, expense.Amount);
		}

		[Test]
		public void ShouldSaveDataWhenOkClicked() {
			Run();

			view.OnOK();

			dataSavingServiceMock.Verify(x => x.Save(), Times.Exactly(1));
		}

		[Test]
		public void ShouldRunShowCalculationUseCaseWhenOkClicked() {
			Run();

			view.OnOK();

			showCalculationUseCaseMock.Verify(x => x.Run(), Times.Exactly(1));
		}
	}
}
