#region Usings

using System;
using Budget.Infrastructure;
using NUnit.Framework;

#endregion

namespace Tests.Infrastructure {
	[TestFixture]
	public class DataContainerTests {
		[Test]
		public void ShouldSetDefaultCalculationPeriodFromMinToMin() {
			var container = new DataContainer2();

			var today = DateTime.Today;

			Assert.AreEqual(today, container.CalculationPeriod.From);
			Assert.AreEqual(new DateTime(today.Year, 12, 31), container.CalculationPeriod.To);
		}
	}
}
