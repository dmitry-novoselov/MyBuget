using Budget.Infrastructure;

namespace Tests.Fakes {
	internal class PersistentStorageFake : IMemento {
		public void Set(object obj) { }
		public object Get(object defaultValue) { return new DataContainer2(); }
	}
}
