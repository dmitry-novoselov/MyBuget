#region

using System;
using System.Collections.Generic;
using System.Linq;
using Budget.Domain;
using Budget.Presentation.ShowCalculationUseCase;
using StructureMap;

#endregion

namespace Budget.Presentation.AddMonthlyExpenseUseCase {
	public class AddMonthlyExpenseUseCase : IAddMonthlyExpenseUseCase {
		private readonly ICalculationDataProvider dataProvider;
		private readonly IEditMonthlyExpenseView view;
		private PEMonthlyExpense expense;

		public AddMonthlyExpenseUseCase(ICalculationDataProvider dataProvider, IEditMonthlyExpenseView view) {
			this.dataProvider = dataProvider;
			this.view = view;
		}

		public void Run() {
			var expenseItems = PESelectableExpenseItem.CreateListFrom(dataProvider);
			expense = new PEMonthlyExpense {
				Date = DateTime.Today,
				Month = DateTime.Today,
				ExpenseItems = expenseItems,
				ExpenseItem = expenseItems.FirstOrDefault(),
                IsFinal = false,
			};

			view.Text = "Добавить трату по статье";
			view.Expense = expense;
			view.OnOK = OnMonthlyExpenseEdited;

			view.Show();
		}

		private void OnMonthlyExpenseEdited() {
			AddExpense();
			RunShowCalculationUseCase();
		}

		private void AddExpense() {
			var negate = expense.ExpenseItem.ExpenseItem.Amount < 0;

			dataProvider.AddMonthlyCashStatement(
				expense.ExpenseItem.ExpenseItem,
				new YearMonth(expense.Month.Month, expense.Month.Year),
				expense.Date,
				expense.Amount.NegateIf(expense.CategoryIsNegative),
				expense.Description,
                expense.IsFinal);
		}

		private static void RunShowCalculationUseCase() {
			ObjectFactory.GetInstance<IShowCalculationUseCase>().Run();
		}
	}
}
