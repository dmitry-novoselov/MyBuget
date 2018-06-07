using Budget.Domain;
using Budget.Presentation.ShowCalculationUseCase;
using StructureMap;
using System;

namespace Budget.Presentation.AddExpenseItemUseCase {
	public class AddExpenseItemUseCase : IAddExpenseItemUseCase {
		private readonly ICalculationDataProvider dataProvider;
		private readonly IEditExpenseItemView view;

		private PEEditableExpenseItem expense;

		public AddExpenseItemUseCase(ICalculationDataProvider dataProvider, IEditExpenseItemView view) {
			this.dataProvider = dataProvider;
			this.view = view;
		}

		public void Run() {
			expense = new PEEditableExpenseItem {
				DayOfMonth = 1,
				From = DateTimeService.CurrentMonthFirstDay,
				To = DateTimeService.MaxValue.AddDays(-1)
			};

			view.Text = "Добавить статью расходов";
			view.MonthlyExpense = expense;
			view.OnOK = OnMonthlyExpenseEdited;

			view.Show();
		}

		private void OnMonthlyExpenseEdited() {
			AddMonthlyExpense();
			RunShowCalculationUseCase();
		}

		private void AddMonthlyExpense() {
			dataProvider.AddMonthlyCashStatementCategory(expense.DayOfMonth, expense.Amount, expense.Name, expense.From, expense.To.AddDays(1));
		}

		private static void RunShowCalculationUseCase() {
			ObjectFactory.GetInstance<IShowCalculationUseCase>().Run();
		}

	}
}
