using Moq;
using NUnit.Framework;

namespace Tests.Presentation.EditPlanningSettingsUseCaseTests {
	[TestFixture]
	public class WhenEditedEnvelopeSize : EditPlanningSettingsUseCaseTestsBase {
		[Test]
		public void ShouldSaveData() {
			Run();

			view.ViewModel.EnvelopeSize = 50;

			Assert.AreEqual(50, dataProvider.EnvelopeSize);
		}

		[Test]
		public void ShouldRecalculateBudget() {
			Run();

			view.ViewModel.EnvelopeSize = 50;

			showCalculationUseCaseMock.Verify(x => x.Run(), Times.Exactly(1));
		}
	}
}
