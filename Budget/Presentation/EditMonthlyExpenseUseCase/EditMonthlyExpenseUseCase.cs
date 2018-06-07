#region

using System;
using Budget.Domain;
using Budget.Presentation.ShowCalculationUseCase;
using StructureMap;

#endregion

namespace Budget.Presentation.EditMonthlyExpenseUseCase {
	public class EditMonthlyExpenseUseCase : IEditMonthlyExpenseUseCase {
		private readonly ICalculationDataProvider dataProvider;
		private readonly IDataSavingService savingService;
		private readonly IEditMonthlyExpenseView view;

		private MonthlyCashStatement expense;
		private PEMonthlyExpense peExpense;

		public EditMonthlyExpenseUseCase(ICalculationDataProvider dataProvider, IDataSavingService savingService, IEditMonthlyExpenseView view) {
			this.dataProvider = dataProvider;
			this.savingService = savingService;
			this.view = view;
		}

		public void Run(MonthlyCashStatement expense) {
			this.expense = expense;

			peExpense = new PEMonthlyExpense {
				ExpenseItem = new PESelectableExpenseItem(expense.Category),
				ExpenseItems = PESelectableExpenseItem.CreateListFrom(dataProvider),
				Month = expense.Month.GetDate(1),
				Date = expense.Date,
				Amount = expense.Amount.NegateIf(expense.Category.IsNegative),
				Description = expense.Description,
				IsFinal = expense.IsFinalPayment,
			};

			view.Text = "Изменить трату по статье";
			view.Expense = peExpense;
			view.OnOK = OnExpenseEdited;
			view.Show();
		}

		private void OnExpenseEdited() {
			expense.Month = new YearMonth(peExpense.Month.Month, peExpense.Month.Year);
			expense.Category = peExpense.ExpenseItem.ExpenseItem;
			expense.Date = peExpense.Date;
			expense.Amount = peExpense.Amount.NegateIf(peExpense.CategoryIsNegative);
			expense.Description = peExpense.Description;
			expense.IsFinalPayment = peExpense.IsFinal;

			savingService.Save();

			ObjectFactory.GetInstance<IShowCalculationUseCase>().Run();
		}
	}
}
