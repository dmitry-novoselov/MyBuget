using Budget.Infrastructure;
using Budget.Presentation.ShowCalculationUseCase;
using Moq;
using NUnit.Framework;
using StructureMap;
using Tests.Fakes;
using Tests.Presentation.Fakes;

namespace Tests.Presentation {
	public class EditDataUseCaseFixtureBase : FixtureBase {
		internal Mock<IShowCalculationUseCase> showCalculationUseCaseMock;

		internal EditTransferViewFake view;
		internal CalculationDataProvider dataProvider;

		[SetUp]
		public void SetUp() {
			showCalculationUseCaseMock = new Mock<IShowCalculationUseCase>();

			ObjectFactory.Initialize(x => {
				x.ForRequestedType<IShowCalculationUseCase>().TheDefault.IsThis(showCalculationUseCaseMock.Object);
			});

			view = new EditTransferViewFake();
			dataProvider = new CalculationDataProvider(new PersistentStorageFake());
		}
	}
}
