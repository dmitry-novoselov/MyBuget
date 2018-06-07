#region Usings

using Budget.Domain;

#endregion

namespace Budget.Presentation.EditPlanningSettingsUseCase {
	public class EditPlanningSettingsUseCase : IEditPlanningSettingsUseCase {
		private IModelView<EditPlanningSettingsViewModel> view;
		private ICalculationDataProvider dataProvider;

		public EditPlanningSettingsUseCase(IModelView<EditPlanningSettingsViewModel> view, ICalculationDataProvider dataProvider) {
			this.view = view;
			this.dataProvider = dataProvider;
		}

		public void Run() {
			view.ViewModel = new EditPlanningSettingsViewModel(dataProvider);
		}
	}
}
