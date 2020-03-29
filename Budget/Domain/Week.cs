using System;

namespace Budget.Domain {
	public class Week : Period, IDated {
		public Week(DateTime weekBegin)
			: base(weekBegin, weekBegin.AddDays(7)) { }

		public DateTime FirstDay {
			get { return From; }
		}

		public DateTime LastDay {
			get { return FirstDay.AddDays(6); }
		}

		public int Month {
			get { return FirstDay.Month; }
		}

		DateTime IDated.Date {
			get { return FirstDay; }
		}
	}
}
