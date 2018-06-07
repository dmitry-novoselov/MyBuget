#region Usings

using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Budget.Domain;
using Budget.Infrastructure;
using Budget.Presentation;
using Budget.Presentation.AddExpenseItemUseCase;
using Budget.Presentation.AddExpenseUseCase;
using Budget.Presentation.AddInvestmentUseCase;
using Budget.Presentation.AddMonthlyExpenseUseCase;
using Budget.Presentation.EditCashMovementUseCase;
using Budget.Presentation.EditExpenseItemUseCase;
using Budget.Presentation.EditMonthlyExpenseUseCase;
using Budget.Presentation.EditPlanningSettingsUseCase;
using Budget.Presentation.EditRemainderUseCase;
using Budget.Presentation.SetRemainderUseCase;
using Budget.Presentation.ShowCalculationUseCase;
using StructureMap;

#endregion

namespace MyBudget {
	public partial class MainForm : Form, IShowCalculationView, IModelView<EditPlanningSettingsViewModel> {
		private CalculationDataProvider dataProvider;

		public MainForm() {
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e) {
//			var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "MyBudget");
			var programLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			var path = Path.Combine(programLocation, "Data");

			var memento = new VersionedMemento(path);
			dataProvider = new CalculationDataProvider(memento);

			ConfigureStructureMap();

			Run();
			SetColumnWidths();
		}

		private static void Run() {
			ObjectFactory.GetInstance<IShowCalculationUseCase>().Run();
			ObjectFactory.GetInstance<IEditPlanningSettingsUseCase>().Run();
		}

		private void ConfigureStructureMap() {
			ObjectFactory.Initialize(x => {
				x.ForRequestedType<ICalculationDataProvider>().TheDefault.IsThis(dataProvider);
				x.ForRequestedType<IDataDeletionService>().TheDefault.IsThis(dataProvider);
				x.ForRequestedType<IDataSavingService>().TheDefault.IsThis(dataProvider);
				x.ForRequestedType<IBudgetService>().TheDefault.Is.OfConcreteType<BudgetService>();

				x.ForRequestedType<IEditExpenseItemView>().TheDefaultIsConcreteType<EditExpenseItemView>();
				x.ForRequestedType<IEditMonthlyExpenseView>().TheDefaultIsConcreteType<EditMonthlyExpenseView>();
				x.ForRequestedType<IEditTransferView>().TheDefaultIsConcreteType<EditTransferView>();
				x.ForRequestedType<IShowCalculationView>().TheDefault.IsThis(this);
				x.ForRequestedType<IModelView<EditPlanningSettingsViewModel>>().TheDefault.IsThis(this);

				x.ForRequestedType<IAddExpenseItemUseCase>().TheDefaultIsConcreteType<AddExpenseItemUseCase>();
				x.ForRequestedType<IAddExpenseUseCase>().TheDefaultIsConcreteType<AddExpenseUseCase>();
				x.ForRequestedType<IAddInvestmentUseCase>().TheDefaultIsConcreteType<AddInvestmentUseCase>();
				x.ForRequestedType<IAddMonthlyExpenseUseCase>().TheDefaultIsConcreteType<AddMonthlyExpenseUseCase>();
				x.ForRequestedType<ISetRemainderUseCase>().TheDefaultIsConcreteType<SetRemainderUseCase>();
				x.ForRequestedType<IShowCalculationUseCase>().TheDefaultIsConcreteType<ShowCalculationUseCase>();

				x.ForRequestedType<IEditPlanningSettingsUseCase>().TheDefaultIsConcreteType<EditPlanningSettingsUseCase>();
				x.ForRequestedType<IEditCashMovementUseCase>().TheDefaultIsConcreteType<EditCashStatementUseCase>();
				x.ForRequestedType<IEditRemainderUseCase>().TheDefaultIsConcreteType<EditCashStatementUseCase>();
				x.ForRequestedType<IEditMonthlyExpenseUseCase>().TheDefaultIsConcreteType<EditMonthlyExpenseUseCase>();
				x.ForRequestedType<IEditExpenseItemUseCase>().TheDefaultIsConcreteType<EditExpenseItemUseCase>();
			});
		}

		private void SetColumnWidths() {
			calculation.Columns[0].Width = 300;
			calculation.Columns[1].Width = 180;
			calculation.Columns[2].Width = 300;
		}

		[Category("MyBudget")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IDataGrid<PEBudgetRow> CalculationResults {
			get { return new DataGrid<PEBudgetRow>(calculation); }
		}

		[Category("MyBudget")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string MonthlyBalance {
			set { monthlyBalance.Text = "Месячный баланс: " + value; }
		}

		[Category("MyBudget")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public EditPlanningSettingsViewModel ViewModel {
			get { return (EditPlanningSettingsViewModel)planningSettings.DataSource; }
			set {
				if (value == null) return;

				planningSettings.DataSource = value;
			}
		}

		private void setRemainder_Click(object sender, EventArgs e) {
			ObjectFactory.GetInstance<ISetRemainderUseCase>().Run();
		}

		private void addInvestment_Click(object sender, EventArgs e) {
			ObjectFactory.GetInstance<IAddInvestmentUseCase>().Run();
		}

		private void addExpense_Click(object sender, EventArgs e) {
			ObjectFactory.GetInstance<IAddExpenseUseCase>().Run();
		}

		private void addExpenseItem_Click(object sender, EventArgs e) {
			ObjectFactory.GetInstance<IAddExpenseItemUseCase>().Run();
		}

		private void editExpenseItem_Click(object sender, EventArgs e) {
			CalculationResults.SelectedItem.Configure();
		}

		private void addMothlyExpense_Click(object sender, EventArgs e) {
			ObjectFactory.GetInstance<IAddMonthlyExpenseUseCase>().Run();
		}

		private void edit_Click(object sender, EventArgs e) {
			CalculationResults.SelectedItem.Edit();
		}

		private void delete_Click(object sender, EventArgs e) {
			CalculationResults.SelectedItem.Delete();
			ObjectFactory.GetInstance<IShowCalculationUseCase>().Run();
		}
	}
}
