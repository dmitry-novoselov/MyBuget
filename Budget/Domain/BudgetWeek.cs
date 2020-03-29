#region Usings

using System;
using System.Diagnostics;

#endregion

namespace Budget.Domain {
	[DebuggerDisplay("{FirstDay.ToString(\"dd MMM\")}—{LastDay.ToString(\"dd MMM\")}")]
	public class BudgetWeek : Week {
		public BudgetWeek(DateTime weekBegin)
			: base(weekBegin) { }

		public int Balance { get; set; }
		public int Remainder { get; set; }
	}
}
