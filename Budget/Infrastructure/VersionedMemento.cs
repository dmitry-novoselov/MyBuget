#region Usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#endregion

namespace Budget.Infrastructure {
	public class VersionedMemento : IMemento {
		private readonly string path;

		public VersionedMemento(string path) {
			this.path = path;
			
			Directory.CreateDirectory(path);
		}

		public void Set(object obj) {
			Store(obj);
			DeleteOld();
		}

		public object Get(object defaultValue) {
			var filePath = FilePaths.FirstOrDefault();

			if (filePath == null) {
				return defaultValue;
			}

			return new Memento(filePath).Get(defaultValue);
		}

		private IEnumerable<string> FilePaths {
			get {
				return Directory
					.GetFiles(path, "budget*.data")
					.OrderByDescending(filePath => filePath);
			}
		}

		private void Store(object obj) {
			var fileName = string.Format("budget {0:yyyy-MM-dd HH-mm}.data", DateTime.Now);
			var filePath = Path.Combine(path, fileName);
			
			new Memento(filePath).Set(obj);
		}

		private void DeleteOld() {
			foreach (var filePathToDelete in FilePaths.Skip(10)) {
				File.Delete(filePathToDelete);
			}
		}
	}
}
