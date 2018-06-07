using Budget.Presentation;

namespace Tests.Presentation.Fakes {
	public class ModelViewFake<TViewModel> : IModelView<TViewModel> {
		public TViewModel ViewModel { get; set; }
	}
}
