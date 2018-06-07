#region Usings

using System;
using Budget.Domain;
using NUnit.Framework;

#endregion

namespace Tests.Domain {
	[TestFixture]
	public class ExpenseItemTests : FixtureBase {
		[Test]
		public void ShouldBeEffectiveStartingCurrentMonthByDefault() {
			NowIs(5.02.of2009());

			var category = new MonthlyCashStatementCategory(5, 0, "jam");

			Assert.AreEqual(new DateTime(2009, 2, 1), category.Effective.From);
		}

		[Test]
		public void ShouldBeEffectiveByTheEndOfTimeByDefault() {
			var category = new MonthlyCashStatementCategory(5, 0, "jam");

			Assert.AreEqual(DateTimeService.MaxValue, category.Effective.To);
		}
	}
}
