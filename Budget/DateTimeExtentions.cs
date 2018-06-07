using System;

// test pushing the code

namespace Budget {
	public static class DateTimeExtentions {
		public static DateTime MonthFirstDay(this DateTime date) {
			return new DateTime(date.Year, date.Month, 1);
		}

		public static DateTime NextMonth(this DateTime month) {
			return month.AddMonths(1);
		}
	}
}
