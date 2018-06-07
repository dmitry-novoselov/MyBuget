#region Usings

using System;

#endregion

namespace Budget.Domain {
	public class DateTimeService {
		public static Func<DateTime> Now = () => DateTime.Now;
		public static readonly DateTime MaxValue = new DateTime(3000, 1, 1);

		public static DateTime CurrentMonthFirstDay {
			get {
				var now = Now();
				return new DateTime(now.Year, now.Month, 1);
			}
		}
	}
}
