#region Usings

using NUnit.Framework;
using Moq;

#endregion

namespace Tests.Presentation.EditPlanningSettingsUseCaseTests {
	[TestFixture]
	public class WhenEditedInitialRemainder : EditPlanningSettingsUseCaseTestsBase {
		[Test]
		public void ShouldSetRemainderOnDayBeforeCalculationPeriodBegining() {
			Run();
			//

			view.ViewModel.CalculationPeriodFrom = 02.02.of2009();
			view.ViewModel.InitialRemainder = 50;

			var remainders = dataProvider.GetRemainders();
			AreEqual(1, remainders.Count);

			var remainder = remainders[0];
			AreEqual(50, remainder.Amount);
			AreEqual(01.02.of2009(), remainder.Date);
		}

		[Test]
		public void ShouldRecalculateBudget() {
			Run();

			view.ViewModel.InitialRemainder = 50;

			showCalculationUseCaseMock.Verify(x => x.Run(), Times.Exactly(1));
		}
	}
}
