#region

using Budget.Domain;
using Budget.Infrastructure;
using Budget.Presentation;
using Budget.Presentation.EditMonthlyExpenseUseCase;
using Budget.Presentation.ShowCalculationUseCase;
using Moq;
using NUnit.Framework;
using StructureMap;
using Tests.Fakes;
using Tests.Presentation.Fakes;

#endregion

namespace Tests.Presentation.EditingTests {
	[TestFixture]
	public class WhenEditMonthlyCashMovement : FixtureBase {
		private Mock<IShowCalculationUseCase> showCalculationUseCaseMock;
		private Mock<IDataSavingService> dataSavingServiceMock;

		private EditMonthlyExpenseViewFake view;
		private CalculationDataProvider dataProvider;

		[SetUp]
		public void SetUp() {
			showCalculationUseCaseMock = new Mock<IShowCalculationUseCase>();
			dataSavingServiceMock = new Mock<IDataSavingService>();

			view = new EditMonthlyExpenseViewFake();

			ObjectFactory.Initialize(x => {
				x.ForRequestedType<IShowCalculationUseCase>().TheDefault.IsThis(showCalculationUseCaseMock.Object);
			});

			dataProvider = new CalculationDataProvider(new PersistentStorageFake());
		}

		private void Run() {
			Run(CreateCashMovement());
		}

		private void Run(MonthlyCashStatement expense) {
			var useCase = new EditMonthlyExpenseUseCase(dataProvider, dataSavingServiceMock.Object, view);
			useCase.Run(expense);
		}

		private MonthlyCashStatementCategory CreateExpenseItem() {
			return new MonthlyCashStatementCategory(1, -1, "");
		}

		private MonthlyCashStatementCategory CreateEarningCategory() {
			return new MonthlyCashStatementCategory(1, 1, "");
		}

		private MonthlyCashStatement CreateCashMovement() {
			return new MonthlyCashStatement(CreateExpenseItem(), month(1), 02.02.of2009(), 1, "");
		}

		[Test]
		public void ShouldSetViewCaption() {
			var expenseItem = CreateExpenseItem();
			var expense = new MonthlyCashStatement(expenseItem, month(1), 02.02.of2009(), 10, "expense item");

			Run(expense);

			Assert.AreEqual("Изменить трату по статье", view.Text);
		}

		[Test]
		public void ShouldDisplayDataOfMonthlyExpenseBeingEdited() {
			var expenseItem = CreateExpenseItem();
			var expense = new MonthlyCashStatement(expenseItem, month(1), 02.02.of2009(), 10, "expense item") { IsFinalPayment = true };

			Run(expense);
			//

			AreEqual(expenseItem, view.Expense.ExpenseItem.ExpenseItem);
			AreEqual(01.01.of2009(), view.Expense.Month);
			AreEqual(02.02.of2009(), view.Expense.Date);
			AreEqual(-10, view.Expense.Amount);
			AreEqual("expense item", view.Expense.Description);
			AreEqual(true, view.Expense.IsFinal);
		}

		[Test]
		public void ShouldBindExpenseItemList() {
			dataProvider.AddExpenseItem(1, 10, "A");
			dataProvider.AddExpenseItem(1, 10, "B");

			Run(CreateCashMovement());

			var expenseItemNames = view.Expense.ExpenseItems.ConvertAll(e => e.ExpenseItem.Name);

			CollectionAssert.AreEquivalent(
				new[] { "A", "B" },
				expenseItemNames);
		}

		[Test]
		public void ShouldShowEditMonthlyExpenseView() {
			var expense = CreateCashMovement();

			Run(expense);

			Assert.IsTrue(view.IsShown);
		}

		[Test]
		public void ShouldReplaceDataOfExpenseWhenOkClicked() {
			var expenseItem = CreateExpenseItem();
			var expense = CreateCashMovement();

			Run(expense);

			view.Expense.Month = 10.10.of2009();
			view.Expense.ExpenseItem = new PESelectableExpenseItem(expenseItem);
			view.Expense.Date = 11.11.of2009();
			view.Expense.Amount = 100;
			view.Expense.Description = "100 rubles";
			view.Expense.IsFinal = true;

			view.OnOK();

			AreEqual(new YearMonth(10, 2009), expense.Month);
			AreSame(expenseItem, expense.Category);
			AreEqual(11.11.of2009(), expense.Date);
			AreEqual(-100, expense.Amount);
			AreEqual("100 rubles", expense.Description);
			AreEqual(true, expense.IsFinalPayment);
		}

		[Test]
		public void ShouldReplaceDataOfEarningWhenOkClicked() {
			var earningCategory = CreateEarningCategory();
			var earning = CreateCashMovement();

			Run(earning);

			view.Expense.ExpenseItem = new PESelectableExpenseItem(earningCategory);
			view.Expense.Amount = 100;

			view.OnOK();

			AreEqual(100, earning.Amount);
		}

		[Test]
		public void ShouldSaveDataWhenOkClicked() {
			Run();

			view.OnOK();

			dataSavingServiceMock.Verify(x => x.Save(), Times.Exactly(1));
		}

		[Test]
		public void ShouldRunShowCalculationUseCaseWhenOkClicked() {
			Run();

			view.OnOK();

			showCalculationUseCaseMock.Verify(x => x.Run(), Times.Exactly(1));
		}
	}
}
