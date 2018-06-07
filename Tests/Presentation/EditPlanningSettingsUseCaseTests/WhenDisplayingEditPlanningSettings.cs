#region Usings

using NUnit.Framework;
using Budget.Domain;
using System;

#endregion

namespace Tests.Presentation.EditPlanningSettingsUseCaseTests {
	[TestFixture]
	public class WhenDisplayingEditPlanningSettings : EditPlanningSettingsUseCaseTestsBase {
		[Test]
		public void ShouldDisplayEnvelopeSize() {
			dataProvider.EnvelopeSize = 100;
			//

			Run();

			AreEqual(100, view.ViewModel.EnvelopeSize);
		}

		[Test]
		public void ShouldDisplayCalculationPeriod() {
			dataProvider.CalculationPeriod = new Period(02.03.of2009(), 04.05.of2010());
			//

			Run();

			AreEqual(02.03.of2009(), view.ViewModel.CalculationPeriodFrom);
			AreEqual(04.05.of2010(), view.ViewModel.CalculationPeriodTo);
		}

		[Test]
		public void ShouldDisplayInitialRemainder() {
			SetInitialRemainder(10);
			//

			Run();

			AreEqual(10, view.ViewModel.InitialRemainder);
		}

		private void SetInitialRemainder(int amount) {
			DateTime from = 02.02.of2009();
			dataProvider.CalculationPeriod = new Period(from, 02.03.of2009());
			dataProvider.SetRemainder(from.AddDays(-1), amount);
		}

		[Test]
		public void ShouldDisplayZeroIfNoInitialRemainder() {
			Run();

			AreEqual(0, view.ViewModel.InitialRemainder);
		}
	}
}
