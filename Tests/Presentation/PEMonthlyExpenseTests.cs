using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Budget.Presentation;

namespace Tests.Presentation {
	[	TestFixture]
	public class PEMonthlyExpenseTests {
		[Test]
		public void DescriptionIsNeverNull() {
			var pe = new PEMonthlyExpense();

			Assert.AreEqual("", pe.Description);
			pe.Description = null;
			Assert.AreEqual("", pe.Description);
		}
	}
}
