#region Usings

using Budget.Domain;
using NUnit.Framework;
using Moq;

#endregion

namespace Tests.Presentation.EditPlanningSettingsUseCaseTests {
	[TestFixture]
	public class WhenEditedCalculationPeriod : EditPlanningSettingsUseCaseTestsBase {
		[Test]
		public void ShouldSaveCalculationPeriod() {
			Run();

			view.ViewModel.CalculationPeriodFrom = 01.02.of2009();
			view.ViewModel.CalculationPeriodTo = 03.04.of2010();

			Assert.AreEqual(new Period(01.02.of2009(), 03.04.of2010()), dataProvider.CalculationPeriod);
		}

		[Test]
		public void ShouldRecalculateBudgetAfterFromIsChanged() {
			Run();

			view.ViewModel.CalculationPeriodFrom = 01.02.of2009();

			showCalculationUseCaseMock.Verify(x => x.Run(), Times.Exactly(1));
		}

		[Test]
		public void ShouldRecalculateBudgetAfterToIsChanged() {
			Run();

			view.ViewModel.CalculationPeriodTo = 01.02.of2009();

			showCalculationUseCaseMock.Verify(x => x.Run(), Times.Exactly(1));
		}
	}
}
