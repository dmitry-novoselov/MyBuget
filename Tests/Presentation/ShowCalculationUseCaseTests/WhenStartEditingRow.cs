#region

using System;
using System.Linq;
using Budget.Domain;
using Budget.Presentation.EditCashMovementUseCase;
using Budget.Presentation.EditExpenseItemUseCase;
using Budget.Presentation.EditMonthlyExpenseUseCase;
using Budget.Presentation.EditRemainderUseCase;
using Moq;
using NUnit.Framework;
using StructureMap;

#endregion

namespace Tests.Presentation.ShowCalculationUseCaseTests {
	[TestFixture]
	public class WhenStartEditingRow : ShowCalculationUseCaseTestsBase {
		private Mock<IEditCashMovementUseCase> editCashMovementMock;
		private Mock<IEditRemainderUseCase> editRemainderMock;
		private Mock<IEditMonthlyExpenseUseCase> editMonthlyExpenseMock;
		private Mock<IEditExpenseItemUseCase> editExpenseItemMock;
		
		[SetUp]
		public void SetUp() {
			MakeUseCaseRunnable();

			ArrangeWeeks(02.02.of2009());

			Init(out editCashMovementMock);
			Init(out editRemainderMock);
			Init(out editMonthlyExpenseMock);
			Init(out editExpenseItemMock);
		}

		private void Init<T>(out Mock<T> field) where T : class {
			field = new Mock<T>(MockBehavior.Strict);
			ObjectFactory.Inject<T>(field.Object);
		}

		[TearDown]
		public void TearDown() {
			editCashMovementMock.VerifyAll();
			editRemainderMock.VerifyAll();
			editMonthlyExpenseMock.VerifyAll();
			editExpenseItemMock.VerifyAll();
		}

		private CashStatement CreateTransfer() {
			return new CashStatement(03.02.of2009(), 0, "");
		}

		[Test]
		public void ShouldEditInvestmentWhenOnesEditionClicked() {
			Test(investment => {
				budget.Investments = new[] { investment };
				editCashMovementMock.Setup(x => x.Run(investment));
			});
		}

		[Test]
		public void ShouldEditExpenseWhenOnesEditionClicked() {
			Test(expense => {
				budget.Expenses = new[] { expense };
				editCashMovementMock.Setup(x => x.Run(expense));
			});
		}

		[Test]
		public void ShouldEditRemainderWhenOnesEditionClicked() {
			Test(remainder => {
				budget.Remainders = new[] { remainder };
				editRemainderMock.Setup(x => x.Run(remainder));
			});
		}

		private void Test(Action<CashStatement> setExpectation) {
			setExpectation(CreateTransfer());

			Run();

			view.CalculationResults.DataSource.Last().Edit();
		}

		[Test]
		public void ShouldEditMonthlyExpenseWhenOnesEditionClicked() {
			var expense = CreateMonthlyExpense();
			budget.MonthlyCashMovements = new[] { expense };

			editMonthlyExpenseMock.Setup(x => x.Run(expense));
			//

			Run();

			view.CalculationResults.DataSource.Last().Edit();
		}

		[Test]
		public void ShouldConfigureExpenseItemWhenConfigureButtonClicked() {
			var expense = CreateMonthlyExpense();
			budget.MonthlyCashMovements = new[] { expense };

			editExpenseItemMock.Setup(x => x.Run(expense.Category));
			//

			Run();

			view.CalculationResults.DataSource.Last().Configure();
		}

		private static MonthlyCashStatement CreateMonthlyExpense() {
			return new MonthlyCashStatement(new MonthlyCashStatementCategory(1, 1, ""), month(1), 02.02.of2009(), 1, "");
		}
	}
}
