#region Usings

using System;

#endregion

namespace Budget.Presentation {
	public class PEEditableExpenseItem {
		public int DayOfMonth { get; set; }
		public int Amount { get; set; }
		public string Name { get; set; }
		public DateTime From { get; set; }
		public DateTime To { get; set; }
	}
}
