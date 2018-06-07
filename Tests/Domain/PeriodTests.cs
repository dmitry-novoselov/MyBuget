using Budget.Domain;
using NUnit.Framework;
using Tests.Dsl;

namespace Tests.Domain {
	[TestFixture]
	public class PeriodTests {

		private static PeriodBuilder day(int day, int month, int year) {
			return new PeriodBuilder(day, month, year);
		}

		private void AreEqual<T>(T expected, T actual) {
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void FromTo() {
			var period = new Period(1.01.of2009(), 1.02.of2009());

			AreEqual(1.01.of2009(), period.From);
			AreEqual(1.02.of2009(), period.To);
		}

		[Test]
		public void PeriodIsRightOpen() {
			var period = new Period(10.01.of2009(), 20.01.of2009());

			Assert.IsFalse(period.Contains(9.01.of2009()));
			Assert.IsTrue(period.Contains(10.01.of2009()));
			Assert.IsTrue(period.Contains(19.01.of2009()));
			Assert.IsFalse(period.Contains(20.01.of2009()));
		}
	}
}
