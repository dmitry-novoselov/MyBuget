using System;
using System.Linq;
using Budget.Presentation.AddExpenseUseCase;
using Moq;
using NUnit.Framework;

namespace Tests.Presentation {
	[TestFixture]
	public class AddExpenseUseCaseTests : EditDataUseCaseFixtureBase {
		private AddExpenseUseCase useCase;

		private void Run() {
			useCase = new AddExpenseUseCase(dataProvider, view);
			useCase.Run();
		}

		[Test]
		public void ShouldShowView() {
			Run();

			Assert.IsTrue(view.IsShown);
		}

		[Test]
		public void ShouldSetTodayAsExpenseDateByDefault() {
			Run();

			Assert.AreEqual(DateTime.Today, view.Transfer.Date);
		}

		[Test]
		public void ShouldSetViewCaption() {
			Run();

			Assert.AreEqual("Добавить трату", view.Text);
		}

		[Test]
		public void ShouldAddRegularExpense() {
			Run();

			view.Transfer.Date = 01.02.of2009();
			view.Transfer.Amount = 10;
			view.Transfer.Description = "Found pocket";
			view.OnOK();

			var expense = dataProvider.GetCashMovements().Single();
			AreEqual(01.02.of2009(), expense.Date);
			Assert.AreEqual(-10, expense.Amount);
			Assert.AreEqual("Found pocket", expense.Description);

			showCalculationUseCaseMock.Verify(x => x.Run(), Times.Exactly(1));
		}
	}
}
