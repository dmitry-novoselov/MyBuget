using NUnit.Framework;
using Tests.Dsl;
using System;
using Budget.Domain;

namespace Tests {
	public class FixtureBase {
		[SetUp]
		public void ResetEnvironment() {
			NowIs(DateTime.MinValue);
		}

		internal static void NowIs(DateTime dateTime) {
			DateTimeService.Now = () => dateTime;
		}

		internal static YearMonth month(int month) {
			return new YearMonth(month, 2009);
		}

		internal static void AreEqual<T>(T expected, T actual) {
			Assert.AreEqual(expected, actual);
		}

		internal static void AreNotEqual<T>(T expected, T actual) {
			Assert.AreNotEqual(expected, actual);
		}

		internal static void AreSame(object expected, object actual) {
			Assert.AreSame(expected, actual);
		}
	}
}
