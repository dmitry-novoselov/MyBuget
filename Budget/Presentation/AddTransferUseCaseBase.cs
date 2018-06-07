using System;
using Budget.Domain;
using Budget.Presentation.ShowCalculationUseCase;
using StructureMap;

namespace Budget.Presentation {
	public abstract class AddTransferUseCaseBase {
		private readonly ICalculationDataProvider dataProvider;
		private readonly string caption;
		private readonly IEditTransferView view;
		private readonly int defaultAmount;
		private readonly Action<PETransfer> doAddTransfer;
		private PETransfer transfer;

		public AddTransferUseCaseBase(ICalculationDataProvider dataProvider, string caption, IEditTransferView view, Action<PETransfer> doAddTransfer)
			: this(dataProvider, caption, view, 0, doAddTransfer) { }

		public AddTransferUseCaseBase(ICalculationDataProvider dataProvider, string caption, IEditTransferView view, int defaultAmount, Action<PETransfer> doAddTransfer) {
			this.dataProvider = dataProvider;
			this.caption = caption;
			this.view = view;
			this.defaultAmount = defaultAmount;
			this.doAddTransfer = doAddTransfer;
		}

		public void Run() {
			transfer = new PETransfer { Date = DateTime.Today, Amount = defaultAmount };

			view.Text = caption;
			view.Transfer = transfer;
			view.OnOK = OnTransferEdited;

			view.Show();
		}

		private void OnTransferEdited() {
			doAddTransfer(transfer);
			RunShowCalculationUseCase();
		}

		private static void RunShowCalculationUseCase() {
			ObjectFactory.GetInstance<IShowCalculationUseCase>().Run();
		}
	}
}
