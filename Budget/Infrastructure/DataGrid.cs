#region

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Budget.Presentation;
using System;

#endregion

namespace Budget.Infrastructure {
	public class DataGrid<T> : IDataGrid<T> where T : class {
		private DataGridView view;

		public DataGrid(DataGridView view) {
			this.view = view;

			view.AutoGenerateColumns = false;
			view.ReadOnly = true;
			view.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			view.MultiSelect = false;

			view.CellFormatting += ViewOnCellFormatting;
		}

		private void ViewOnCellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {
			var pe = (PEBudgetRow)view.Rows[e.RowIndex].DataBoundItem;
			if (pe.BackgroundColor != PEBudgetRow.Default) { // (e.State & DataGridViewElementStates.Selected) != DataGridViewElementStates.Selected
				e.CellStyle.BackColor = pe.BackgroundColor;
			}
		}

		public IEnumerable<T> DataSource {
			get { return (IEnumerable<T>)view.DataSource; }
			set {
				var index = view.FirstDisplayedScrollingRowIndex;

				view.DataSource = value;
				
				if (index < value.Count()) {
					view.FirstDisplayedScrollingRowIndex = index;
				}
			}
		}

		public bool IsScrolledDown {
			get { return view.FirstDisplayedScrollingRowIndex != 0; }
		}

		public T SelectedItem {
			get { return (T)view.SelectedRows[0].DataBoundItem; }
			set {
				var rowHeight = view.Rows[0].Height;
				var gridHeight = view.ClientSize.Height;
				var rowsPerScreen = gridHeight / rowHeight;
				var shift = rowsPerScreen / 2;

				var selectedItemPosition = DataSource.ToList().IndexOf(value);

				view.FirstDisplayedScrollingRowIndex = Math.Max(selectedItemPosition - shift, 0);
				view.Rows.Cast<DataGridViewRow>().First(row => Equals(row.DataBoundItem, value)).Selected = true;
			}
		}

		public void AddColumn(string key, string header) {
			if (!view.Columns.Contains(key)) {
				view.Columns.Add(key, header);
				view.Columns[key].DataPropertyName = key;
			}
		}
	}
}
