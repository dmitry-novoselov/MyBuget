using Budget.Infrastructure;
using Budget.Presentation.AddExpenseItemUseCase;
using Budget.Presentation.ShowCalculationUseCase;
using Moq;
using NUnit.Framework;
using StructureMap;
using Tests.Presentation.Fakes;
using Tests.Fakes;
using Budget.Domain;

namespace Tests.Presentation {
	[TestFixture]
	public class AddMonthlyCashStatementCategoryUseCaseTests : FixtureBase {
		private Mock<IShowCalculationUseCase> showCalculationUseCaseMock;

		private EditExpenseItemViewFake view;
		private CalculationDataProvider dataProvider;

		private AddExpenseItemUseCase useCase;

		[SetUp]
		public void SetUp() {
			showCalculationUseCaseMock = new Mock<IShowCalculationUseCase>();

			ObjectFactory.Initialize(x => {
				x.For<IShowCalculationUseCase>().Use(showCalculationUseCaseMock.Object);
			});

			view = new EditExpenseItemViewFake();
			dataProvider = new CalculationDataProvider(new PersistentStorageFake(), new PersistentStorageFake());
		}

		private void Run() {
			useCase = new AddExpenseItemUseCase(dataProvider, view);
			useCase.Run();
		}

		[Test]
		public void ShouldShowView() {
			Run();

			Assert.IsTrue(view.IsShown);
		}

		[Test]
		public void ShouldSetFirstDayOfMonthByDefault() {
			Run();

			Assert.AreEqual(1, view.MonthlyExpense.DayOfMonth);
		}

		[Test]
		public void ShouldSetEffective() {
			DateTimeService.Now = () => 5.10.of2009();
			
			Run();

			AreEqual(1.10.of2009(), view.MonthlyExpense.From);
			AreEqual(DateTimeService.MaxValue.AddDays(-1), view.MonthlyExpense.To);
		}

		[Test]
		public void ShouldSetViewCaption() {
			Run();

			Assert.AreEqual("Добавить статью расходов", view.Text);
		}

		[Test]
		public void ShouldAddMonthlyExpense() {
			Run();

			view.MonthlyExpense.DayOfMonth = 5;
			view.MonthlyExpense.Amount = 100;
			view.MonthlyExpense.Name = "Internet";
			view.MonthlyExpense.From = 1.02.of2009();
			view.MonthlyExpense.To = 3.04.of2010();
			view.OnOK();

			AreEqual(1, dataProvider.GetMonthlyCashStatementCategories().Count);

			var expense = dataProvider.GetMonthlyCashStatementCategories()[0];
			Assert.AreEqual(5, expense.DayOfMonth);
			Assert.AreEqual(100, expense.Amount);
			Assert.AreEqual("Internet", expense.Name);
			AreEqual(1.02.of2009() - 4.04.of2010(), expense.Effective);

			showCalculationUseCaseMock.Verify(x => x.Run(), Times.Exactly(1));
		}
	}
}
