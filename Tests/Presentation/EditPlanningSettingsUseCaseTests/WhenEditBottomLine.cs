#region  Usings

using NUnit.Framework;

#endregion

namespace Tests.Presentation.EditPlanningSettingsUseCaseTests {
	[TestFixture]
	public class WhenEditBottomLine : EditPlanningSettingsUseCaseTestsBase {
		[SetUp]
		public void SetUp() {
			Run();
		}

		[Test]
		public void BottomLineIsZeroWhenNoRemainders() {
			ViewModel.WalletRemainders = "";

			Assert.AreEqual(0, ViewModel.BottomLine);
		}

		[Test]
		public void BottomLineIsEqualOneValueRemainders() {
			ViewModel.WalletRemainders = "100";

			Assert.AreEqual(100, ViewModel.BottomLine);
		}

		[Test]
		public void BottomLineIsSumOfRemainders() {
			ViewModel.WalletRemainders = "100 - pocket, 200 - safe";

			Assert.AreEqual(300, ViewModel.BottomLine);
		}

		[Test]
		public void UnderstandNegativeRemainders() {
			ViewModel.WalletRemainders = "-100 - in stash";

			Assert.AreEqual(-100, ViewModel.BottomLine);
		}

		[Test]
		public void StashRemainderWhenEdited() {
			ViewModel.WalletRemainders = "no money";

			Assert.AreEqual("no money", dataProvider.WalletRemainders);
		}

		[Test]
		public void GetRemainderFromDataProvider() {
			dataProvider.WalletRemainders = "still no money";

			Assert.AreEqual("still no money", ViewModel.WalletRemainders);
		}
	}
}
