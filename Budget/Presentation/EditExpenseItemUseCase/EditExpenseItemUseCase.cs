#region Usings

using System;
using Budget.Domain;
using StructureMap;
using Budget.Presentation.ShowCalculationUseCase;

#endregion

namespace Budget.Presentation.EditExpenseItemUseCase {
	public class EditExpenseItemUseCase : IEditExpenseItemUseCase {
		private IDataSavingService dataSavingService;
		private IEditExpenseItemView view;
		private MonthlyCashStatementCategory expenseItem;
		private PEEditableExpenseItem peExpenseItem;
		
		public EditExpenseItemUseCase(IDataSavingService dataSavingService, IEditExpenseItemView view) {
			this.dataSavingService = dataSavingService;
			this.view = view;
		}

		public void Run(MonthlyCashStatementCategory expenseItem) {
			this.expenseItem = expenseItem;

			peExpenseItem = new PEEditableExpenseItem {
				DayOfMonth = expenseItem.DayOfMonth,
				Amount = expenseItem.Amount,
				Name = expenseItem.Name,
				From = expenseItem.Effective.From,
				To = expenseItem.Effective.To.AddDays(-1),
			};

			view.Text = "Править статью расходов";
			view.OnOK = OnOK;
			view.MonthlyExpense = peExpenseItem;

			view.Show();
		}

		private void OnOK() {
			StashChanges();
			RunShowCalculationUseCase();
		}

		private void StashChanges() {
			expenseItem.DayOfMonth = peExpenseItem.DayOfMonth;
			expenseItem.Amount = peExpenseItem.Amount;
			expenseItem.Name = peExpenseItem.Name;
			expenseItem.Effective = new Period(peExpenseItem.From, peExpenseItem.To.AddDays(1));

			dataSavingService.Save();
		}

		private static void RunShowCalculationUseCase() {
			ObjectFactory.GetInstance<IShowCalculationUseCase>().Run();
		}
	}
}
