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
		public MainForm() {
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e) {
//			var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "MyBudget");
			var pathProgramLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			var pathDataLocation = Path.Combine(pathProgramLocation, "Data");
			var pathBackupLocation = Path.Combine(pathDataLocation, "Backup");

			var dataMemento = new VersionedMemento(pathDataLocation);
			var backupMemento = new VersionedMemento(pathBackupLocation);

			var dataProvider = new CalculationDataProvider(dataMemento, backupMemento);
			ConfigureStructureMap(dataProvider);

			Run();
			SetColumnWidths();
		}

		private static void Run() {
			ObjectFactory.GetInstance<IShowCalculationUseCase>().Run();
			ObjectFactory.GetInstance<IEditPlanningSettingsUseCase>().Run();
		}

		private void ConfigureStructureMap(CalculationDataProvider dataProvider) {
			ObjectFactory.Initialize(x => {
				x.For<ICalculationDataProvider>().Use(dataProvider);
				x.For<IDataDeletionService>().Use(dataProvider);
				x.For<IDataSavingService>().Use(dataProvider);
				x.For<IBudgetService>().TheDefault.Is.OfConcreteType<BudgetService>();

				x.For<IEditExpenseItemView>().TheDefaultIsConcreteType<EditExpenseItemView>();
				x.For<IEditMonthlyExpenseView>().TheDefaultIsConcreteType<EditMonthlyExpenseView>();
				x.For<IEditTransferView>().TheDefaultIsConcreteType<EditTransferView>();
				x.For<IShowCalculationView>().Use(this);
				x.For<IModelView<EditPlanningSettingsViewModel>>().Use(this);

				x.For<IAddExpenseItemUseCase>().TheDefaultIsConcreteType<AddExpenseItemUseCase>();
				x.For<IAddExpenseUseCase>().TheDefaultIsConcreteType<AddExpenseUseCase>();
				x.For<IAddInvestmentUseCase>().TheDefaultIsConcreteType<AddInvestmentUseCase>();
				x.For<IAddMonthlyExpenseUseCase>().TheDefaultIsConcreteType<AddMonthlyExpenseUseCase>();
				x.For<ISetRemainderUseCase>().TheDefaultIsConcreteType<SetRemainderUseCase>();
				x.For<IShowCalculationUseCase>().TheDefaultIsConcreteType<ShowCalculationUseCase>();

				x.For<IEditPlanningSettingsUseCase>().TheDefaultIsConcreteType<EditPlanningSettingsUseCase>();
				x.For<IEditCashMovementUseCase>().TheDefaultIsConcreteType<EditCashStatementUseCase>();
				x.For<IEditRemainderUseCase>().TheDefaultIsConcreteType<EditCashStatementUseCase>();
				x.For<IEditMonthlyExpenseUseCase>().TheDefaultIsConcreteType<EditMonthlyExpenseUseCase>();
				x.For<IEditExpenseItemUseCase>().TheDefaultIsConcreteType<EditExpenseItemUseCase>();
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
			set { monthlyBalance.Text = value; }
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
