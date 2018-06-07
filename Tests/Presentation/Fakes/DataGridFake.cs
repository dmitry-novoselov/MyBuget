#region

using System.Collections.Generic;
using System.Linq;
using Budget.Presentation;
using NUnit.Framework;

#endregion

namespace Tests.Presentation.Fakes {
	internal class DataGridFake<TDataItem> : IDataGrid<TDataItem> {
		public readonly Dictionary<string, string> Columns = new Dictionary<string, string>();

		public IEnumerable<TDataItem> DataSource { get; set; }
		public TDataItem SelectedItem { get; set; }
		public bool IsScrolledDown { get; set; }

		public void AddColumn(string key, string header) {
			AssertDataItemContainsProperty(key);
			Columns[key] = header;
		}

		private void AssertDataItemContainsProperty(string key) {
			Assert.IsNotNull(typeof(TDataItem).GetProperty(key));
		}

		internal void Select(TDataItem selectedItem) {
			SelectedItem = DataSource.First(dataItem => Equals(dataItem, selectedItem));
		}
	}
}
