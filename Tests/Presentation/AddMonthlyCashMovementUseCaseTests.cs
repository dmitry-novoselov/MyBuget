#region Usings

using System;
using System.Linq;
using Budget.Domain;
using Budget.Infrastructure;
using Budget.Presentation;
using Budget.Presentation.AddMonthlyExpenseUseCase;
using Budget.Presentation.ShowCalculationUseCase;
using Moq;
using NUnit.Framework;
using StructureMap;
using Tests.Fakes;
using Tests.Presentation.Fakes;

#endregion

namespace Tests.Presentation {
	[TestFixture]
	public class AddMonthlyCashMovementUseCaseTests : FixtureBase {
		private Mock<IShowCalculationUseCase> showCalculationUseCaseMock;

		private EditMonthlyExpenseViewFake view;
		private CalculationDataProvider dataProvider;

		private AddMonthlyExpenseUseCase useCase;

		[SetUp]
		public void SetUp() {
			showCalculationUseCaseMock = new Mock<IShowCalculationUseCase>();

			ObjectFactory.Initialize(x => {
				x.For<IShowCalculationUseCase>().Use(showCalculationUseCaseMock.Object);
			});

			view = new EditMonthlyExpenseViewFake();
			dataProvider = new CalculationDataProvider(new PersistentStorageFake(), new PersistentStorageFake());
		}

		private void Run() {
			useCase = new AddMonthlyExpenseUseCase(dataProvider, view);
			useCase.Run();
		}

		[Test]
		public void ShouldShowView() {
			Run();

			Assert.IsTrue(view.IsShown);
		}

		[Test]
		public void ShouldSetViewCaption() {
			Run();

			Assert.AreEqual("Добавить трату по статье", view.Text);
		}

		[Test]
		public void ShouldSetDefaultDates() {
			Run();

			Assert.AreEqual(DateTime.Today, view.Expense.Date);
			Assert.AreEqual(DateTime.Today, view.Expense.Month);
		}

		[Test]
		public void ShouldSetFirstExpenseItemByDefault() {
			dataProvider.AddExpenseItem(1, 10, "2");
			dataProvider.AddExpenseItem(1, 10, "1");

			var first = (from e in dataProvider.GetMonthlyCashStatementCategories() where e.Name == "1" select e).Single();
			//

			Run();

			Assert.AreSame(first, view.Expense.ExpenseItem.ExpenseItem);
		}

		[Test]
		public void ShouldSortExpenseItems() {
			dataProvider.AddExpenseItem(1, 10, "C");
			dataProvider.AddExpenseItem(1, 10, "A");
			dataProvider.AddExpenseItem(1, 10, "B");

			var a = (from e in dataProvider.GetMonthlyCashStatementCategories() where e.Name == "A" select e).Single();
			var b = (from e in dataProvider.GetMonthlyCashStatementCategories() where e.Name == "B" select e).Single();
			var c = (from e in dataProvider.GetMonthlyCashStatementCategories() where e.Name == "C" select e).Single();
			//

			Run();

			var peExpenseItems = view.Expense.ExpenseItems.ToList();
			CollectionAssert.AreEqual(
				new[] {
					new PESelectableExpenseItem(a),
					new PESelectableExpenseItem(b),
					new PESelectableExpenseItem(c),
				},
				peExpenseItems);
		}

		[Test]
		public void ShouldAddMonthlyExpense() {
			dataProvider.AddExpenseItem(1, -10, "Gaz");

			var gaz = (from e in dataProvider.GetMonthlyCashStatementCategories() where e.Name == "Gaz" select e).Single();
			//

			Run();

			view.Expense.Date = 01.02.of2009();
			view.Expense.Amount = 20;
			view.Expense.Month = 01.03.of2009();
			view.Expense.Description = "Found pocket";
			view.Expense.IsFinal = true; ;
			view.Expense.ExpenseItem = new PESelectableExpenseItem(gaz);
			view.OnOK();

			var expense = dataProvider.GetMonthlyCashMovements().Single();
			Assert.AreSame(gaz, expense.Category);
			AreEqual(01.02.of2009(), expense.Date);
			Assert.AreEqual(-20, expense.Amount);
			Assert.AreEqual(new YearMonth(3, 2009), expense.Month);
			Assert.AreEqual("Found pocket", expense.Description);
			Assert.AreEqual(true, expense.IsFinalPayment);

			showCalculationUseCaseMock.Verify(x => x.Run(), Times.Exactly(1));
		}

		[Test]
		public void ShouldAddMonthlyEarning() {
			dataProvider.AddExpenseItem(1, 10, "Gaz");

			var gaz = (from e in dataProvider.GetMonthlyCashStatementCategories() where e.Name == "Gaz" select e).Single();
			//

			Run();

			view.Expense.Amount = 20;
			view.OnOK();

			var earning = dataProvider.GetMonthlyCashMovements().Single();
			Assert.AreEqual(20, earning.Amount);
		}
	}
}