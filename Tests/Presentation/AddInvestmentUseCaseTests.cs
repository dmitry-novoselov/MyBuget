using System;
using Budget.Presentation.AddInvestmentUseCase;
using Moq;
using NUnit.Framework;

namespace Tests.Presentation {
	[TestFixture]
	public class AddInvestmentUseCaseTests : EditDataUseCaseFixtureBase {
		private AddInvestmentUseCase useCase;

		private void Run() {
			useCase = new AddInvestmentUseCase(dataProvider, view);
			useCase.Run();
		}

		[Test]
		public void ShouldShowView() {
			Run();

			Assert.IsTrue(view.IsShown);
		}

		[Test]
		public void ShouldSetTodayAsInvestmentDayByDefault() {
			Run();

			Assert.AreEqual(DateTime.Today, view.Transfer.Date);
		}

		[Test]
		public void ShouldSetViewCaption() {
			Run();

			Assert.AreEqual("Добавить доход", view.Text);
		}

		[Test]
		public void ShouldAddInvestment() {
			Run();

			view.Transfer.Date = 01.02.of2009();
			view.Transfer.Amount = 10;
			view.Transfer.Description = "Have found pocket";
			view.OnOK();

			AreEqual(1, dataProvider.GetCashMovements().Count);

			var investment = dataProvider.GetCashMovements()[0];
			AreEqual(01.02.of2009(), investment.Date);
			Assert.AreEqual(10, investment.Amount);
			Assert.AreEqual("Have found pocket", investment.Description);

			showCalculationUseCaseMock.Verify(x => x.Run(), Times.Exactly(1));
		}
	}
}
