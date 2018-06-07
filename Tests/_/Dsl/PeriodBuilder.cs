using System;
using Budget.Domain;

namespace Tests.Dsl {
	public class PeriodBuilder {
		public PeriodBuilder(int day, int month, int year) {
			AsDateTime = new DateTime(year, month, day);
		}

		public DateTime AsDateTime { get; private set; }

		public static Period operator -(PeriodBuilder from, PeriodBuilder to) {
			return new Period(from.AsDateTime, to.AsDateTime);
		}

		public static implicit operator DateTime(PeriodBuilder builder) {
			return builder.AsDateTime;
		}
	}
}
