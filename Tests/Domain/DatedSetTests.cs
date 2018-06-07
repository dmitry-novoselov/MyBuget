#region Usings

using System.Collections.Generic;
using System.Linq;
using Budget.Domain;
using NUnit.Framework;

#endregion

namespace Tests.Domain {
	[TestFixture]
	public class DatedSetTests {
		[Test]
		public void GetByIndex() {
			var transfers = new[] { new CashStatement(1.01.of2009(), 10, "1"), new CashStatement(2.01.of2009(), 20, "2") };
			var set = new DatedSet<CashStatement>(transfers);

			Assert.AreSame(transfers[0], set.ElementAt(0));
			Assert.AreSame(transfers[1], set.ElementAt(1));
		}

		[Test]
		public void SearchInPeriod() {
			var transfers = new[] {
				new CashStatement(1.01.of2009(), 10, "1"),
				new CashStatement(2.01.of2009(), 20, "2"),
				new CashStatement(3.01.of2009(), 30, "3"),
				new CashStatement(4.01.of2009(), 40, "4"),
			};

			var set = new DatedSet<CashStatement>(transfers)[2.01.of2009() - 4.01.of2009()];

			CollectionAssert.AreEquivalent(
				new[] { transfers[1], transfers[2] },
				set);
		}

		[Test]
		public void SearchInPeriodOfEqual() {
			var transfers = new[] {
				new CashStatement(1.01.of2009(), 10, "1"),
				new CashStatement(2.01.of2009(), 20, "2"),
				new CashStatement(2.01.of2009(), 20, "2"),
				new CashStatement(2.01.of2009(), 20, "2"),
				new CashStatement(3.01.of2009(), 30, "3"),
				new CashStatement(3.01.of2009(), 30, "3"),
				new CashStatement(3.01.of2009(), 30, "3"),
				new CashStatement(4.01.of2009(), 40, "4"),
			};

			var set = new DatedSet<CashStatement>(transfers)[2.01.of2009() - 4.01.of2009()];

			CollectionAssert.AreEquivalent(
				new[] { transfers[1], transfers[2], transfers[3], transfers[4], transfers[5], transfers[6] },
				set);
		}

		[Test]
		public void SearchWithOverlappingPeriod() {
			var transfers = new[] {
				new CashStatement(3.01.of2009(), 10, "1"),
				new CashStatement(4.01.of2009(), 40, "4"),
			};

			var set = new DatedSet<CashStatement>(transfers)[2.01.of2009() - 5.01.of2009()];

			CollectionAssert.AreEquivalent(
				new[] { transfers[0], transfers[1] },
				set);
		}

		[Test]
		public void SearchWithJustCoveringPeriod() {
			var transfers = new[] {
				new CashStatement(3.01.of2009(), 10, "1"),
				new CashStatement(4.01.of2009(), 40, "4"),
			};

			var set = new DatedSet<CashStatement>(transfers)[3.01.of2009() - 4.01.of2009()];

			CollectionAssert.AreEquivalent(
				new[] { transfers[0] },
				set);
		}
	}
}
