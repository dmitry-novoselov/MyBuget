#region Usings

using System;
using System.Collections.Generic;
using Budget.Domain;

#endregion

namespace Budget.Infrastructure {
	[Serializable]
	public class DataContainer2 {
		public int EnvelopeSize;
		public Period CalculationPeriod = DefaultPeriod;

		public readonly List<CashStatement> Remainders = new List<CashStatement>();
		public readonly List<CashStatement> CashMovements = new List<CashStatement>();
		public readonly List<MonthlyCashStatementCategory> MonthlyCashStatementCategories = new List<MonthlyCashStatementCategory>();
		public readonly List<MonthlyCashStatement> MonthlyCashMovements = new List<MonthlyCashStatement>();

		public string WalletRemainders;

		private static Period DefaultPeriod {
			get {
				var today = DateTime.Today;
				return new Period(today, new DateTime(today.Year, 12, 31));
			}
		}
	}
}
