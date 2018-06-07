
namespace Budget.Presentation {
	public interface IModelView<TViewModel> {
		TViewModel ViewModel { get; set; }
	}
}
