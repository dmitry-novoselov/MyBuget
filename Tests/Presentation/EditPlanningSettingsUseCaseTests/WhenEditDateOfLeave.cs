#region Using

using NUnit.Framework;
using System;

#endregion

namespace Tests.Presentation.EditPlanningSettingsUseCaseTests {
	public class WhenEditDateOfLeave : EditPlanningSettingsUseCaseTestsBase {
		[SetUp]
		public void SetUp() {
			Run();
		}

		[Test]
		public void DateOfTicketsPurchaseIs44daysEarlierThanDateOfLeave() {
			ViewModel.DateOfLeave = 01.06.of2010();

			AreEqual(18.04.of2010(), ViewModel.DateOfTicketsPurchase);
		}

		[Test]
		public void DateOfLeaveIs44daysLaterThanDateOfTicketsPurchase() {
			ViewModel.DateOfTicketsPurchase = 18.04.of2010();

			AreEqual(01.06.of2010(), ViewModel.DateOfLeave);
		}

		[Test]
		public void DateOfTicketsPurchaseIsTodayByDefault() {
			AreEqual(DateTime.Today, ViewModel.DateOfTicketsPurchase);
		}
	}
}
