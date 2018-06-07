#region Usings

using System;
using System.Linq;
using Budget.Domain;
using Budget.Presentation.ShowCalculationUseCase;
using StructureMap;

#endregion

namespace Budget.Presentation.EditPlanningSettingsUseCase {
	public class EditPlanningSettingsViewModel {
		private ICalculationDataProvider dataProvider;

		public EditPlanningSettingsViewModel(ICalculationDataProvider dataProvider) {
			this.dataProvider = dataProvider;

			DateOfTicketsPurchase = DateTime.Today;
		}

		public int EnvelopeSize {
			get { return dataProvider.EnvelopeSize; }
			set { Set(() => dataProvider.EnvelopeSize = value); }
		}

		public DateTime CalculationPeriodFrom {
			get { return dataProvider.CalculationPeriod.From; }
			set { Set(() => dataProvider.CalculationPeriod = new Period(value, CalculationPeriodTo)); }
		}

		public DateTime CalculationPeriodTo {
			get { return dataProvider.CalculationPeriod.To; }
			set { Set(() => dataProvider.CalculationPeriod = new Period(CalculationPeriodFrom, value)); }
		}

		public int InitialRemainder {
			get {
				var initialRemainderDate = CalculationPeriodFrom.AddDays(-1);
				var initialRemainder = dataProvider.GetRemainders().FirstOrDefault(r => r.Date == initialRemainderDate);
				return initialRemainder != null ? initialRemainder.Amount : 0;
			}

			set { Set(() => dataProvider.SetRemainder(CalculationPeriodFrom.AddDays(-1), value)); }
		}

		public string WalletRemainders {
			get { return dataProvider.WalletRemainders; }
			set { dataProvider.WalletRemainders = value; }
		}

		public int BottomLine {
			get { return WalletRemainders.OveralAmount(); }
		}

		public DateTime DateOfLeave {
			get { return DateOfTicketsPurchase.AddDays(44); }
			set { DateOfTicketsPurchase = value.AddDays(-44); }
		}

		public DateTime DateOfTicketsPurchase { get; set; }

		private void Set(Action stashData) {
			stashData();
			ObjectFactory.GetInstance<IShowCalculationUseCase>().Run();
		}
	}
}
