using System;
using Budget.Infrastructure;
using Budget.Presentation.SetRemainderUseCase;
using Budget.Presentation.ShowCalculationUseCase;
using Moq;
using NUnit.Framework;
using StructureMap;
using Tests.Presentation.Fakes;

namespace Tests.Presentation {
	[TestFixture]
	public class SetRemainderUseCaseTests : EditDataUseCaseFixtureBase {
		private SetRemainderUseCase useCase;

		private void Run() {
			useCase = new SetRemainderUseCase(dataProvider, view);
			useCase.Run();
		}

		[Test]
		public void ShouldShowView() {
			Run();

			Assert.IsTrue(view.IsShown);
			Assert.AreEqual(DateTime.Today, view.Transfer.Date);
		}

		[Test]
		public void ShouldSetViewCaption() {
			Run();

			Assert.AreEqual("Задать остаток", view.Text);
		}

		[Test]
		public void ShouldSetTodayAsRemaiderDateByDefault() {
			Run();

			Assert.AreEqual(DateTime.Today, view.Transfer.Date);
		}

		[Test]
		public void ShouldSetBottomLineAsRemaiderValueByDefault() {
			dataProvider.WalletRemainders = "50";

			Run();

			Assert.AreEqual(50, view.Transfer.Amount);
		}

		[Test]
		public void ShouldSetRemainder() {
			Run();

			view.Transfer.Date = 1.02.of2009();
			view.Transfer.Amount = 10;
			view.OnOK();

			Assert.AreEqual(1, dataProvider.GetRemainders().Count);
			
			var transfer = dataProvider.GetRemainders()[0];
			AreEqual(1.02.of2009(), transfer.Date);
			Assert.AreEqual(10, transfer.Amount);

			showCalculationUseCaseMock.Verify(x => x.Run(), Times.Exactly(1));
		}
	}
}
