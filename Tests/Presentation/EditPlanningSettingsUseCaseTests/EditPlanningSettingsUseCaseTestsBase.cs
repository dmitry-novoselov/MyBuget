#region Usings

using Budget.Domain;
using Budget.Infrastructure;
using Budget.Presentation.EditPlanningSettingsUseCase;
using NUnit.Framework;
using Tests.Fakes;
using Tests.Presentation.Fakes;
using System;
using Moq;
using Budget.Presentation.ShowCalculationUseCase;
using StructureMap;

#endregion

namespace Tests.Presentation.EditPlanningSettingsUseCaseTests {
	public class EditPlanningSettingsUseCaseTestsBase : FixtureBase {
		protected ModelViewFake<EditPlanningSettingsViewModel> view;
		protected ICalculationDataProvider dataProvider;

		protected Mock<IShowCalculationUseCase> showCalculationUseCaseMock;

		[SetUp]
		public void MakeUseCaseRunnable() {
			view = new ModelViewFake<EditPlanningSettingsViewModel>();
			dataProvider = new CalculationDataProvider(new PersistentStorageFake());

			showCalculationUseCaseMock = new Mock<IShowCalculationUseCase>();

			ObjectFactory.Initialize(x => {
				x.ForRequestedType<IShowCalculationUseCase>().TheDefault.IsThis(showCalculationUseCaseMock.Object);
			});
		}

		protected EditPlanningSettingsViewModel ViewModel {
			get { return view.ViewModel;  }
		}

		protected void Run() {
			new EditPlanningSettingsUseCase(view, dataProvider).Run();
		}
	}
}
