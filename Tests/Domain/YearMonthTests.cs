using System;
using Budget.Domain;
using NUnit.Framework;

namespace Tests.Domain {
	[TestFixture]
	public class YearMonthTests : FixtureBase {
		[Test]
		public void Equals() {
			Assert.IsTrue(new YearMonth(1, 2009) == new YearMonth(1, 2009));
			Assert.IsFalse(new YearMonth(1, 2009) != new YearMonth(1, 2009));

			Assert.IsFalse(new YearMonth(1, 2009) == new YearMonth(2, 2009));
			Assert.IsTrue(new YearMonth(1, 2009) != new YearMonth(2, 2009));
		}

		[Test]
		public void ImplicitCastToDateTime() {
			AreEqual(new YearMonth(1, 2009), new DateTime(2009, 1, 1));
			AreEqual(new YearMonth(1, 2009), new DateTime(2009, 1, 31));
			AreNotEqual(new YearMonth(1, 2009), new DateTime(2009, 2, 1));
		}

		[Test]
		public void GetDate() {
			Assert.AreEqual(new DateTime(2009, 1, 1), new YearMonth(1, 2009).GetDate(1));
			Assert.AreEqual(new DateTime(2009, 1, 2), new YearMonth(1, 2009).GetDate(2));
		}

		[Test]
		public void DateMethodDoesNotExceedEndOfMonth() {
			Assert.AreEqual(new DateTime(2009, 2, 1), new YearMonth(2, 2009).GetDate(1));
			Assert.AreEqual(new DateTime(2009, 2, 28), new YearMonth(2, 2009).GetDate(28));
			Assert.AreEqual(new DateTime(2009, 2, 28), new YearMonth(2, 2009).GetDate(29));
		}
	}
}
