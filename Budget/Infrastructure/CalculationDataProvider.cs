#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Budget.Domain;

#endregion

namespace Budget.Infrastructure {
	[Serializable]
	public class CalculationDataProvider : ICalculationDataProvider, IDataDeletionService, IDataSavingService {
		private readonly IMemento memento;
		private readonly DataContainer2 data;

		public CalculationDataProvider(IMemento memento, IMemento backupMemento) {
			this.memento = memento;
			
			data = (DataContainer2)memento.Get(new DataContainer2());
			backupMemento.Set(data);
		}

		public Period CalculationPeriod {
			get { return data.CalculationPeriod; }
			set {
				data.CalculationPeriod = value;
				CleanupOldData();
				Save();
			}
		}

		public int EnvelopeSize {
			get { return data.EnvelopeSize; }
			set {
				data.EnvelopeSize = value;
				Save();
			}
		}

		public void SetRemainder(DateTime date, int amount) {
			data.Remainders.RemoveAll(r => r.Date == date);
			AddStatement(data.Remainders, date, amount, "");
		}

		public List<CashStatement> GetRemainders() {
			return data.Remainders;
		}

		public void AddCashMovement(DateTime date, int amount, string description) {
			AddStatement(data.CashMovements, date, amount, description);
		}

		public List<CashStatement> GetCashMovements() {
			return data.CashMovements;
		}

		public void AddMonthlyCashStatementCategory(int dayOfMonth, int amount, string name, DateTime from, DateTime to) {
			Add(data.MonthlyCashStatementCategories, new MonthlyCashStatementCategory(dayOfMonth, amount, name) { Effective = new Period(from, to) });
		}

		public List<MonthlyCashStatementCategory> GetMonthlyCashStatementCategories() {
			return data.MonthlyCashStatementCategories;
		}

		public void AddMonthlyCashStatement(MonthlyCashStatementCategory category, YearMonth month, DateTime date, int amount, string description, bool isFinal = true) {
            Add(data.MonthlyCashMovements, new MonthlyCashStatement(category, month, date, amount, description) { IsFinalPayment = isFinal });
		}

		public List<MonthlyCashStatement> GetMonthlyCashMovements() {
			return data.MonthlyCashMovements;
		}

		public void DeleteRemainder(CashStatement remainder) {
			Delete(data.Remainders, remainder);
		}

		public void DeleteCashMovement(CashStatement movement) {
			Delete(data.CashMovements, movement);
		}

		public void DeleteMonthlyCashMovement(MonthlyCashStatement movement) {
			Delete(data.MonthlyCashMovements, movement);
		}

		public string WalletRemainders {
			get { return data.WalletRemainders; }
			set {
				data.WalletRemainders = value;
				Save();
			}
		}

		public void Save() {
			memento.Set(data);
		}

		private void AddStatement(List<CashStatement> statements, DateTime date, int amount, string description) {
			Add(statements, new CashStatement(date, amount, description));
		}

		private void Add<T>(List<T> list, T item) {
			list.Add(item);
			Save();
		}

		private void Delete<T>(List<T> list, T item) {
			list.Remove(item);
			Save();
		}

		private void CleanupOldData()
		{
			var cutoffDate = data.CalculationPeriod.From.AddDays(-1);

			data.Remainders.RemoveAll(x => x.Date < cutoffDate);
			data.CashMovements.RemoveAll(x => x.Date < cutoffDate);
			data.MonthlyCashMovements.RemoveAll(x => x.Date < cutoffDate);
			data.MonthlyCashStatementCategories.RemoveAll(x => !data.MonthlyCashMovements.Any(y => y.Category == x));
		}
	}
}
