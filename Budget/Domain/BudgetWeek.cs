#region Usings

using System;

#endregion

namespace Budget.Domain {
	public class BudgetWeek : Week {
		public BudgetWeek(DateTime weekBegin)
			: base(weekBegin) { }

		public int Remainder { get; set; }
		// todo : remove
		public int DayEnvelopeSize { get; set; }
	}
}
