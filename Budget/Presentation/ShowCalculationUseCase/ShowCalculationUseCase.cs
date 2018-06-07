#region Usings

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using Budget.Domain;
using Budget.Presentation.EditCashMovementUseCase;
using Budget.Presentation.EditExpenseItemUseCase;
using Budget.Presentation.EditMonthlyExpenseUseCase;
using Budget.Presentation.EditRemainderUseCase;
using StructureMap;

#endregion

namespace Budget.Presentation.ShowCalculationUseCase {
	public class ShowCalculationUseCase : IShowCalculationUseCase {
		private readonly IBudgetService budgetService;
		private readonly IShowCalculationView view;
		private readonly IDataDeletionService deletionService;

		private const int MONTH_NAME_ROW_RANK = 1;
		private const int WEEK_ROW_RANK = 2;
		private const int MONTHLY_INCOME_ROW_RANK = 3;
		private const int INCOME_ROW_RANK = 4;
		private const int MONTHLY_OUTCOME_ROW_RANK = 5;
		private const int OUTCOME_ROW_RANK = 6;
		private const int REMAINDER_RANK = 7;

		private int position;

		public ShowCalculationUseCase(IBudgetService budgetService, IShowCalculationView view, IDataDeletionService deletionService) {
			this.budgetService = budgetService;
			this.view = view;
			this.deletionService = deletionService;
		}

		public void Run() {
			ResetCalculationResultColumns();
			Bind();
		}

		private void ResetCalculationResultColumns() {
			view.CalculationResults.AddColumn("Event", "Событие");
			view.CalculationResults.AddColumn("Date", "Дата");
			view.CalculationResults.AddColumn("Amount", "Сумма / Остаток");
		}

		private void Bind() {
			var budget = budgetService.CalculateBudget();

			var currentWeek = BindDataOf(budget);
			SelectLastRemainderOf(budget, currentWeek);
		}

		private BudgetWeek BindDataOf(IBudget budget) {
			var dataSource = new SortedDictionary<PEOrderKey, PEBudgetRow>();

			var currentWeekMarker = CurrentWeekMarker(budget);

			BudgetWeek currentWeek = null;
			var lastWeekMonth = 0;
			foreach (var week in budget.Weeks) {
				if (week.Contains(currentWeekMarker)) {
					currentWeek = week;
				}

				if (week.Month != lastWeekMonth) {
					dataSource.Add(
						new PEOrderKey(week.FirstDay.MonthFirstDay(), MONTH_NAME_ROW_RANK),
						new PEBudgetRow { Date = week.FirstDay.ToString("MMMM")});
				}

				dataSource.Add(
					new PEOrderKey(week.FirstDay, WEEK_ROW_RANK),
					new PEBudgetRow {
						Date = string.Format("Неделя {0:d} - {1:d}", week.FirstDay, week.LastDay),
						Amount = string.Format("Свободные {0}", budget.GetFreeMoney(week.FirstDay).ToString("D")),
					});

				// todo : performance
				var monthlyCashStatements = budget.MonthlyCashMovements.Within(week).ToList();
				AddExpenses(week, dataSource, MONTHLY_INCOME_ROW_RANK, monthlyCashStatements.Where(_ => _.Amount > 0), budget, currentWeek, PEBudgetRow.CurrentWeekMonthlyIncome);
				AddExpenses(week, dataSource, MONTHLY_OUTCOME_ROW_RANK, monthlyCashStatements.Where(_ => _.Amount <= 0), budget, currentWeek, PEBudgetRow.CurrentWeekMonthlyOutcome);
				AddTransfers(week, dataSource, INCOME_ROW_RANK, budget.Investments.Within(week), deletionService.DeleteCashMovement, OnEditCashMovement, currentWeek, PEBudgetRow.CurrentWeekIncome);
				AddTransfers(week, dataSource, OUTCOME_ROW_RANK, budget.Expenses.Within(week), deletionService.DeleteCashMovement, OnEditCashMovement, currentWeek, PEBudgetRow.CurrentWeekOutcome);
				AddReminders(week, dataSource, REMAINDER_RANK, budget.Remainders.Within(week), budget, deletionService.DeleteRemainder, OnEditRemainder, currentWeek, PEBudgetRow.Default);

				lastWeekMonth = week.Month;
			}

			view.CalculationResults.DataSource = new List<PEBudgetRow>(dataSource.Values);
			view.MonthlyBalance = budget.MonthlyBalance.ToString("+#;#");

			return currentWeek;
		}

		private static DateTime CurrentWeekMarker(IBudget budget) {
			var lastReminder = budget.Remainders.LastOrDefault();
			var now = DateTimeService.Now();
			return lastReminder != null && lastReminder.Date < now ? lastReminder.Date : now;
		}

		private void SelectLastRemainderOf(IBudget budget, BudgetWeek currentWeek) {
			if (view.CalculationResults.IsScrolledDown) return;

			var lastRemainder = budget.Remainders
				.OrderBy(r => r.Date)
				.LastOrDefault();

			if (lastRemainder != null) {
				view.CalculationResults.SelectedItem = CreatePEBudgetRow(lastRemainder, "Остаток", r => { }, r => { }, ExplainReminder(budget, currentWeek), currentWeek, PEBudgetRow.Default);
			}
		}

		private void AddTransfers(Week week, SortedDictionary<PEOrderKey, PEBudgetRow> dataSource, int rowRank, IEnumerable<CashStatement> transfers, Action<CashStatement> delete, Action<CashStatement> edit, Week currentWeek, Color currentWeekColor) {
			foreach (var transfer in transfers) {
				dataSource.Add(
					new PEOrderKey(week.FirstDay, rowRank, transfer.Date, position++),
					CreatePEBudgetRow(transfer, null, delete, edit, _ =>  _.Amount.ToString(), currentWeek, currentWeekColor));
			}
		}

		private void AddReminders(BudgetWeek week, SortedDictionary<PEOrderKey, PEBudgetRow> dataSource, int rowRank, IEnumerable<CashStatement> transfers, IBudget budget, Action<CashStatement> delete, Action<CashStatement> edit, Week currentWeek, Color currentWeekColor) {
			foreach (var transfer in transfers) {
				dataSource.Add(
					new PEOrderKey(week.FirstDay, rowRank, transfer.Date, position++),
					CreatePEBudgetRow(transfer, "Остаток", delete, edit, ExplainReminder(budget, week), currentWeek, currentWeekColor));
			}
		}

		private Func<CashStatement, string> ExplainReminder(IBudget budget, BudgetWeek week) {
			return statement => string.Format("Конверт {0}, Остатки {1}, Свободные {2}", week != null ? week.Remainder.ToString() : "?", statement.Amount, budget.GetFreeMoney(statement.Date));
		}

		private static PEBudgetRow CreatePEBudgetRow(CashStatement transfer, string description, Action<CashStatement> delete, Action<CashStatement> edit, Func<CashStatement, string> amount, Week currentWeek, Color currentWeekColor) {
			return new PEBudgetRow {
				OnEdit = () => edit(transfer),
				OnDelete = () => delete(transfer),
				Event = description ?? transfer.Description,
				Date = transfer.Date.ToString("d"),
				Amount = amount(transfer),
				BackgroundColor = currentWeek != null && currentWeek.Contains(transfer.Date) ? currentWeekColor : PEBudgetRow.Default,
			};
		}

		private void AddExpenses(BudgetWeek week, SortedDictionary<PEOrderKey, PEBudgetRow> dataSource, int rowRank, IEnumerable<MonthlyCashStatement> expenses, IBudget budget, Week currentWeek, Color currentWeekColor) {
			foreach (var expense in expenses) {
				var theExpense = expense;
				dataSource.Add(
					new PEOrderKey(week.FirstDay, rowRank, expense.Date, position++),
					new PEBudgetRow {
						OnEdit = () => OnEditMonthlyExpense(theExpense),
						OnConfigure = () => OnEditExpenseItem(theExpense),
						OnDelete = () => deletionService.DeleteMonthlyCashMovement(theExpense),
						Event = DescriptionOf(expense),
						Date = expense.Date.ToString("d"),
						Amount = expense.Amount.ToString(),
						BackgroundColor = currentWeek != null && currentWeek.Contains(expense.Date) ? currentWeekColor : PEBudgetRow.Default,
					});
			}
		}

		private void OnEditExpenseItem(MonthlyCashStatement expense) {
			ObjectFactory.GetInstance<IEditExpenseItemUseCase>().Run(expense.Category);
		}

		private void OnEditCashMovement(CashStatement investment) {
			ObjectFactory.GetInstance<IEditCashMovementUseCase>().Run(investment);
		}

		private void OnEditRemainder(CashStatement remainder) {
			ObjectFactory.GetInstance<IEditRemainderUseCase>().Run(remainder);
		}

		private void OnEditMonthlyExpense(MonthlyCashStatement expense) {
			ObjectFactory.GetInstance<IEditMonthlyExpenseUseCase>().Run(expense);
		}

		private static string DescriptionOf(MonthlyCashStatement expense) {
			return expense.Description == ""
				? expense.Category.Name
				: string.Format("{0} : {1}", expense.Category.Name, expense.Description);
		}

		private class PEOrderKey : IComparable<PEOrderKey>, IEquatable<PEOrderKey> {
			private readonly DateTime weekFirstDay;
			private readonly DateTime date;
			private readonly int rank;
			private readonly int position;

			public PEOrderKey(DateTime date, int rank)
				: this(date, rank, date, 0) { }

			public PEOrderKey(DateTime weekFirstDay, int rank, DateTime date, int position) {
				this.weekFirstDay = weekFirstDay;
				this.rank = rank;
				this.date = date;
				this.position = position;
			}

			public int CompareTo(PEOrderKey other) {
				if (ReferenceEquals(other, null)) {
					return 1;
				}

				var result = weekFirstDay.CompareTo(other.weekFirstDay);
				if (result != 0) {
					return result;
				}

				result = rank.CompareTo(other.rank);
				if (result != 0) {
					return result;
				}

				result = date.CompareTo(other.date);
				if (result != 0) {
					return result;
				}

				return position.CompareTo(other.position);
			}

			public bool Equals(PEOrderKey other) {
				return CompareTo(other) == 0;
			}

			public override bool Equals(object obj) {
				return Equals(obj as PEOrderKey);
			}

			public override int GetHashCode() {
				return date.GetHashCode();
			}
		}
	}
}
