
namespace Budget.Infrastructure {
	public interface IMemento {
		void Set(object obj);
		object Get(object defaultValue);
	}
}
