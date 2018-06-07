#region Usings

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#endregion

namespace Budget.Infrastructure {
	public class Memento : IMemento {
		private readonly string filePath;

		public Memento(string filePath) {
			this.filePath = filePath;
		}

		public void Set(object obj) {
			var file = File.Create(filePath);
			try {
				new BinaryFormatter().Serialize(file, obj);
			} finally {
				file.Close();
			}
		}

		public object Get(object defaultValue) {
			if (!File.Exists(filePath))
				return defaultValue;

			var file = File.OpenRead(filePath);
			try {
				return new BinaryFormatter().Deserialize(file);
			} finally {
				file.Close();
			}
		}
	}
}
