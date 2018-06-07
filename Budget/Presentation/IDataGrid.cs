using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Budget.Presentation {
	public interface IDataGrid<TDataItem> {
		IEnumerable<TDataItem> DataSource { get; set; }
		TDataItem SelectedItem { get; set; }
		bool IsScrolledDown { get; }

		void AddColumn(string key, string header);
	}
}
