using Budget.Domain;
using Budget.Presentation;
using NUnit.Framework;

namespace Tests.Presentation {
	[TestFixture]
	public class PESelectableExpenseItemTests {
		[Test]
		public void CanSetExpenseItem() {
			var expense = new MonthlyCashStatementCategory(1, 10, "Gaz");
			var peExpense = new PESelectableExpenseItem(expense);

			Assert.AreEqual("Gaz", peExpense.ToString());
			Assert.AreSame(expense, peExpense.ExpenseItem);
		}
	}
}
