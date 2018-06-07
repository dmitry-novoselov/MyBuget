using System.IO;
using Budget.Infrastructure;
using NUnit.Framework;

namespace Tests.Infrastructure {
	[TestFixture]
	public class MementoTests {
		private Memento memento;

		[SetUp]
		public void SetUp() {
			File.Delete("data.dat");

			memento = new Memento("data.dat");
		}

		[Test]
		public void ReturnsDefaultValueIfFileDoesntExist() {
			var defaultValue = new object();

			Assert.AreSame(defaultValue, memento.Get(defaultValue));
		}

		[Test]
		public void LoadDataFromFile() {
			var stored = "value";
			memento.Set(stored);
			var loaded = (string)memento.Get(null);

			Assert.AreNotSame(stored, loaded);
			Assert.AreEqual(stored, loaded);
		}
	}
}
