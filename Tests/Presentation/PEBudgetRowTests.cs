using Budget.Presentation;
using NUnit.Framework;

namespace Tests.Presentation {
	[TestFixture]
	public class PEBudgetRowTests {
		[Test]
		public void EditingOfNonInitializedObjectDoesNothing() {
			new PEBudgetRow().Edit();
		}

		[Test]
		public void DeletingOfNonInitializedObjectDoesNothing() {
			new PEBudgetRow().Delete();
		}
	}
}
